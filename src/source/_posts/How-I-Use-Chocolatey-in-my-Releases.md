title: How I Use Chocolatey in my Releases
date: 2016-06-10 22:10:44
tags:
- ALM
- DevOps
- NuGet
- PowerShell
---
{% img right /images/Chocolatey.jpg 100 100 "Chocolatey.org" %}
I have been using Chocolatey for a while as an ultra easy way to install software.  It has become the prefered way to install tools and utilities from the open source community.  Recently I have started to explore this technology in more depth just to learn more about Chocolatey and found some really great uses for it that I did not expect to find.  This post is about that adventure and how and what I use Chocolatey for.

## Built on NuGet
First off I guess we should talk about what Chocolatey is.  It is another packaged technology based on NuGet.  In fact it is NuGet with some more features and elements added to it.  If you have been around me over the last couple of years, I have declared that NuGet is probably one of the greatest advancements that we have had in the dot net community in the last 10 years.  Initially introduced back in 2010, it was a package tool to help resolve the dependencies in open source software.  Even back then I could see that this technology had legs and indeed it did as it has proven to resolve so many hard development problems that we have worked on for years to resolve.  That being able to have shared code within multiple projects that does not interfere with the development of the underlying projects that depend on them.  I will delve into this subject in a later post as right now I want to focus on Chocolatey.

While NuGet was really about installing and resolving dependencies at the source code level as in a new Visual Studio project, Chocolatey takes that same package structure and focuses on the Operating System.  In other words I can create NuGet like packages (they have the very same extension as NuGet *.nupkg) that I can install, uninstall or upgrade in Windows.  I have a couple of utility like programs that run on the desktop that I use to support my applications.  These utilities are never distributed or a part of my application that I distribute through click-once but I need up to date version of these on my test lab machines.  It has always been a problem with having some way to get these installed and up to date on these machines.  However, with the use of Chocolatey this is now an easy solution and a problem that I no longer have.

## Install Chocolatey
Let's start with how we would go about installing Chocolatey.  If you go to the [Chocolatey.org web-site](http://chocolatey.org) there are about 3 ways listed to download and install the package all of them using PowerShell.
This first one assumes nothing, as it will Bypass the ExecutionPolicy and has the best chance of installing on your system.
```
@powershell -NoProfile -ExecutionPolicy Bypass -Command "iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))" && SET PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin
```
This next one is going to assume that you are an administrator on your machine and you have set the Execution Policy to at least RemoteSigned
```
iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))
```
Then this last script is going to assume that you are an administrator, have the Execution Policy set to at least RemoteSigned and have PowerShell v3 or higher.
```
iwr https://chocolatey.org/install.ps1 -UseBasicParsing | iex
```
Not sure what version of PowerShell you have?  Well the easiest way to tell is to bring up the PowerShell console (you will want to run with Administrator elevated rights) and enter the following:
```
$PSVersionTable
```
## Making my own Package
Okay so I have Chocolatey installed and I have a product that I want to install so how do I get this package created?  Good question so lets tackle that next.  I start by using file explorer, go to your project and create a new folder.  In my case I was working with a utility program that I called AgpAdmin so at the sibling level of that project I made a folder called AgpAdminInstall and this is where I am going to build my package.

{% asset_img FileStructure.png "The file structure"%}
Now I would bring up PowerShell running as an administrator and navigate over to that new folder that I just created and enter the following Chocolatey command.
```
Choco New AGPAdmin
```
This will create the nuspec file with the same name that I entered in that New command as well as a tools folder which will contain two powershell scripts.  There are a couple of ways that you can build this package as the final bits don't even need to be in this package. They could be referenced in other locations where they can be downloaded and installed. There is a lot of documentation and examples that you can find to do that.  I would say that most of the Chocolatey packages that can be found on Chocolatey.org are done this way.  I found that they mention that the assemblies could be embedded but I never found an example and that was the way that I wanted to package this so that is the guidance I am going to show you here. 

Lets start with the nuspec file.  This is the file that contains the meta data and where all the pieces can be found.  If you are familiar with creating a typical NuGet spec this should all look pretty familiar but there are a couple of things that you must be aware of.  In the Chocolatey version of this spec file you must have a **projectUrl** (in my case I was pointing to my VSTS implementation dashboard page.  You must have a **packageSourceUrl** (in my case I pointed to my source url to my git repository) and a **licenseUrl** which needs to point to a page that describes your license.  I never needed these when building a NuGet package but are required in order to get the Chocolatey package built.  One more thing we need for the nuspec file to be complete is the files section where we tell it what files need to be included in the package.

There will be one entry there already which is to include all the items found in the folder tools and to place it within the nuget package structure of tools.  We want to add one more file entry where we add a relative path from where we are to include the setup file that is being constructured up one folder and then then down 3 folders through the AGPAdminSetup tree and the target also being within the nuget package structure of tools.  This line is what embeds my setup program into the Chocolatey package.

```
<?xml version="1.0" encoding="utf-8"?>
<!-- Do not remove this test for UTF-8: if “Ω” doesn’t appear as greek uppercase omega letter enclosed in quotation marks, you should use an editor that supports UTF-8, not this one. -->
<package xmlns="http://schemas.microsoft.com/packaging/2015/06/nuspec.xsd">
  <metadata>
    <!-- Read this before publishing packages to chocolatey.org: https://github.com/chocolatey/chocolatey/wiki/CreatePackages -->
    <id>agpadmin</id>
    <title>AGPAdmin (Install)</title>
    <version>2.2.0.2</version>
    <authors>Donald L. Schulz</authors>
    <owners>The Web We Weave, Inc.</owners>
    <summary>Admin tool to help support AGP-Maker</summary>
    <description>Setup and Install of the AGP-Admin program</description>
    <projectUrl>https://donald.visualstudio.com/3WInc/AGP-Admin/_dashboards</projectUrl>
    <packageSourceUrl>https://donald.visualstudio.com/DefaultCollection/3WInc/_git/AGPMaker-Admin</packageSourceUrl>
    <tags>agpadmin admin</tags>
    <copyright>2016 The Web We Weave, Inc.</copyright>
    <licenseUrl>http://www.agpmaker.com/AGPMaker.Install/index.htm</licenseUrl>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
  </metadata>
  <files>
    <file src="..\AGPAdminSetup\bin\Release\AGPAdminSetup.exe" target="tools" />
    <file src="tools\**" target="tools" />
  </files>
</package>
```

Before we move on to the automated steps that we want to implement so that we don't even have to think about building this package every time, we will need to make a couple of changes to the PowerShell scripts that are found in the tools folder.  When you open this powershell script it is well commented and the variable names used are pretty clear in describing what they are for.  You will notice that it seems to be ready out of the box to get you to provide a url where it can get your program to install.  I want to use the embedded solution so un-comment the first $fileLocation line and replace the 'NAME_OF_EMBEDDED_INSTALLER_FILE' with the name of the file you want to run and I will also assume that you have it in this same tools folder (in the compiled nupkg file).  In my package I did create an install program using [the wix toolset](http://wixtoolset.org/) which also gives it the capability to uninstall itself automatically.  Next I commented out the default silentArgs and the validExitCodes found right under the #MSI comment.  There is a long string of commented lines that all start with #silentArgs and what I did was un-comment the last one and set the value as '/quiet' and un-comment the validExistCodes line right below that so the line looks like this:
```
silentArgs = '/quiet'
validExitCodes= @(0)
```
That is really all that there is to it.  The rest of this script file should just work.  There are a number of different cmdlet's that you can call and they are all shown in the InstallChocoletey.ps1 file that appeared when you called the Choco new command and they are all commented fairly well.  I was creating the Chocolatey wrapper around an Install program so I chose the cmdlet "Install-ChocolateyInstallPackage".  So to summarize the PowerShell Script ignoring the commented lines the finished PowerShell script looks a lot like this:
```
$ErrorActionPreference = 'Stop';

$packageName= 'MyAdminProg' # arbitrary name for the package, used in messages
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$fileLocation = Join-Path $toolsDir 'MyAdminProgSetup.exe'

$packageArgs = @{
  packageName   = $packageName
  unzipLocation = $toolsDir
  fileType      = 'EXE' #only one of these: exe, msi, msu
  url           = $url
  url64bit      = $url64
  file          = $fileLocation
  silentArgs    = '/quiet'
  softwareName  = 'MyAdminProg*' #part or all of the Display Name as you see it in Programs and Features. It should be enough to be unique
  checksum      = ''
  checksumType  = 'md5' #default is md5, can also be sha1
  checksum64    = ''
  checksumType64= 'md5' #default is checksumType
}

Install-ChocolateyInstallPackage @packageArgs  
```
One thing that we did not cover in all this is the fileType value.  This is going to be an exe, msi or msu depending on how you created your setup file.  I took the extra step in my wix install program to create a bootstrap which takes the initial msi and checks the prerequists such as the correct version of the dot net framework and turns that into an exe.  You will need to set this to the value of your install program what you want to run.

Another advantage to using an install package is that it knows how to uninstall itself.  That means I did not need that other PowerShell script that was in the tools directory which was the chocolateyuninstall.ps1 file.  I deleted mine so that it would use the automatic uninstaller that is managed and controlled by windows (msi).  If this file exists in your package than Chocolatey is going to run that script and if you have not set this up properly will give you issues when you run the Choco uninstall command for the package.

## Automating the Build in TFS 2015
We want to make sure that we place all these two files folders and the nuspec file into source control.  Besides having this is a place where we can repeat this process and keep track of any changes that might happen between changes we will be able to automate the entire operation.  Our goal here is to make a change which when we check in the code change of the actual utility program will kick off a build create the package and publish it to our private Chocolatey feed.

To automate the building of the chocolatey package I started with a Build Definition that I already had that was building all these pieces.  It built the program, then created an AGPAdminPackage.msi file and then turned that into a bootstrapper and gave me the AGPAdminSetup.exe file.  Our nuspec file has indicated where to find the finished AGPAdminSetup.exe file so that it will be embedded into the finished .nupkg file.  Just after the steps that compile the code, run the tests, I add a PowerShell script and switch it to run inline and write the following script:
```
# You can write your powershell scripts inline here. 
# You can also pass predefined and custom variables to this scripts using arguments

cpack
```
This command will find the .nuspec file and create the .nupkg in the same folder as the nuspec file.  From there the things that I do are to copy the pieces I am interested in having in the drop and place them into the staging work space $(Agent.BuildDirectory)\b and then for the Copy Publish Artifacts I just push everything I have in staging.

## Private Feed

Because Chocolatey is based on Nuget technology it works on exactly the same principal of distribution which is a feed but it could also be a network file share.  I have chosen the private feed as I need this to be a feed that I can access from home, the cloud, and when I am on the road.  Okay so you might be in the same or similar situation as myself so how do you setup a Chocolatey Server?  With Chocolatey of course.
```
choco install chocolatey.server -y
```
On the machine that you run this command on, it will create a chocolatey.server folder inside of a folder off of the root drive called tools.  Just point IIS to this folder and you have a Chocolatey feed ready for your packages.  The packages actually go into the App_Data\packages folder that you will find in this ready to go Chocolatey.server.  However I will make another assumption that this server may not be right next to you but on a different machine or even the cloud so you will want to publish your packages.  To do that you will need to make sure that you give the app pool modify permissions to the App_Data folder.  This in the build definition after the Copy Publish Artifact add another PowerShell script to run inline and this time call the following command:
```
# You can write your powershell scripts inline here. 
# You can also pass predefined and custom variables to this scripts using arguments

choco push --source="https://<your server name here>/choco/" --api-key="<your api key here>" --force
```
That is it really you have a package in a feed that can be installed and upgraded with just a simple Chocolatey command.

## Make it even Better
I went one step farther to make this even easier and that was to modify the chocolatey configuration file so that it looks in my private repository first before looking at the public one that is set up by default.  This way I can install and upgrade my private packages just as if they were published and exposed to the whole world but it is not.  You find the chocolatey.config file in the C:\ProgramData\Chocolatey\config folder.  When you open the file you will see an area called sources and probably one source listed.  Just add an additional source file give it an id (I called my Choco) and the value should be where your chocolatey feed can be found and set the priority to 1. That is it but you need to do this to all the machines that are going to be getting your program and all the latest updates.  Now when ever you are doing a build to about to run tests on a virtual machine you can call have a simple powershell script do it for you.
```
choco install agpadmin -y
Write-Output "AGPAdmin Installed"

choco upgrade agpadmin -y
Write-Output "AGPAdmin Upgraded"

Start-Sleep 120
```
The program I am installing is called agpadmin and I pass the -y so that it skips the confirm as this is almost always part of a build.  I call both the install and then the upgrade as it does not seem to do both but it just ignores the install if it is already installed and will then do the upgrade if there is a newer version out there.

Hope you enjoy Chocolatey as much as I do.