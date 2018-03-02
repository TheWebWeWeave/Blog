$Dir= 'C:\Deploy\drop\public'
$ftp = 'ftp://waws-prod-bay-003.ftp.azurewebsites.windows.net/site/wwwroot'
$user = 'DonaldOnSoftware\SchulzDL'
$pass = 'Make!Web@Cloud'
$webclient = New-Object System.Net.WebClient 
$webclient.Credentials = New-Object System.Net.NetworkCredential($user,$pass)

foreach($item in (dir $Dir *.* -Recurse))
{

    #Set default network status to 1
    $onNetwork = "1"
  
    #If upload fails, we set network status at 0
    try
    {
        if(-not( $item.PSIsContainer)) {
            $uri = New-Object System.Uri($ftp+$item.FullName.Substring($Dir.Length))       
            "Uploading $uri"      
            $webclient.UploadFile($uri, $item.FullName) 
        }
        else {
            $ftprequest = [System.Net.FtpWebRequest]::Create($ftp+$item.FullName.Substring($Dir.Length));
            $ftprequest.Method = [System.Net.WebRequestMethods+Ftp]::MakeDirectory
            $ftprequest.UseBinary = $true
            $ftprequest.Credentials = New-Object System.Net.NetworkCredential($user,$pass)

            $response = $ftprequest.GetResponse();
            "Upload File Complete, status $response.StatusDescription"
            $response.Close();            

        }
           
    } 

    catch [Exception] 
    {
        #$onNetwork = "0"
        #write-host $_.Exception.Message;
    }
}