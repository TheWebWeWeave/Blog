param(
    [string]$token, 
    [string]$build,
    [string]$uri,
    [string]$project
)

Import-Module 'NewReleaseWorkItem' -Verbose


#$build = "Empty Test"
#$uri = "https://donald.visualstudio.com/"
#$project = "3WInc"

New-TfsReleaseWorkItem -PersonalAccessToken $token  -BuildName $build -BaseTFSUri $uri -ProjectName $project -Verbose
