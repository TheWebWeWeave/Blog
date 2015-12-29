param(
    [string]$SourcePath, 
    [string]$StagingPath
)

$targetPath = "$StagingPath"
$scriptPath = "$StagingPath\Scripts"
$testPath = "$StagingPath\Tests"

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

Copy-Item "$SourcePath\public" -Recurse -Destination $targetPath -Force
Copy-Item "$SourcePath\blog.zip" -Destination $StagingPath -Force
Copy-Item "$SourcePath\scripts\*.*" -Destination $scriptPath -Force
Copy-Item "$SourcePath\DeployViaFTP\bin\Debug\*.*" -Destination $testPath -Force
