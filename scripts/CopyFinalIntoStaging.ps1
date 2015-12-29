param(
    [string]$SourcePath, 
    [string]$StagingPath
)

$targetPath = "$StagingPath\DoS"
$scriptPath = "$StagingPath\Scripts"


if (-not(Test-Path($scriptPath))) {
        mkdir $scriptPath
    }

if (-not(Test-Path($testPath))) {
        mkdir $testPath
    }

Copy-Item "$SourcePath\public\*.*" -Destination $targetPath -Recurse
Copy-Item "$SourcePath\blog.zip" -Destination $StagingPath
Copy-Item "$SourcePath\scripts\*.*" -Destination $scriptPath
