param(
    [string]$SourcePath, 
    [string]$StagingPath
)

$targetPath = "$StagingPath\DoS"
$scriptPath = "$targetPath\Scripts"
$testPath = "$targetPath\Tests"

if (-not(Test-Path($targetPath))) {
        mkdir $targetPath
    }

if (-not(Test-Path($scriptPath))) {
        mkdir $scriptPath
    }

if (-not(Test-Path($testPath))) {
        mkdir $testPath
    }

Copy-Item "$SourcePath\public\*.*" -Destination $targetPath
Copy-Item "$SourcePath\blog.zip -Destination $StagingPath
Copy-Item "$SourcePath\scripts\*.*" -Destination $scriptPath
