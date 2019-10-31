param(
    [string]$SourcePath, 
    [string]$StagingPath
)

$targetPath = "$StagingPath"
$scriptPath = "$StagingPath\deployment"
$testPath = "$StagingPath\Tests"
$msDeploy = "$StagingPath\MSDeploy"

Remove-Item $StagingPath -Recurse -Force

if (-not(Test-Path($targetPath))) {
        mkdir $targetPath
    }

if (-not(Test-Path($scriptPath))) {
        mkdir $scriptPath
    }

if (-not(Test-Path($testPath))) {
        mkdir $testPath
    }

if (-not(Test-Path($msDeploy))) {
        mkdir $msDeploy
    }

Copy-Item "$SourcePath\deployment\*.*" -Destination $scriptPath -Force
Copy-Item "$SourcePath\BlogUITests\BlogUITests\bin\Release\*.*" -Destination $testPath -Force
Copy-Item "$SourcePath\MSDeploy\*.*" -Destination $msDeploy -Force