param
(
    [string]$rgName,
    [string]$vmName
)

$vm = (Get-AzureRMVM -ResourceGroupName "$rgName" -Name "$vmName" -Status).Statuses
if(!$vm.Code.Contains("PowerState/running")) 
{ 
    try 
    {
        Write-Output "$vmName is starting"
        Start-AzureRmVM -ResourceGroupName "$rgName" -Name "$vmName" -ErrorAction Ignore
        Start-Sleep -s 180
    }

    finally 
    { 
        Write-Output "$vmName is started" 
    }
}
else { Write-Output "$vmName is running" }