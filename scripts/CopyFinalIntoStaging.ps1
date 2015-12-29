param(
    [string]$SourcePath, 
    [string]$StagingPath
)

$targetPath = "$StagingPath"
$scriptPath = "$StagingPath\Scripts"

if (-not(Test-Path($targetPath))) {
        mkdir $targetPath
    }

if (-not(Test-Path($scriptPath))) {
        mkdir $scriptPath
    }

Copy-Item "$SourcePath\public" -Recurse -Destination $targetPath 
Copy-Item "$SourcePath\blog.zip" -Destination $StagingPath
Copy-Item "$SourcePath\scripts\*.*" -Destination $scriptPath
