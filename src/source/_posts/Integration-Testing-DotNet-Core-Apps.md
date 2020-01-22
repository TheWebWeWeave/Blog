---
title: Integration Testing DotNet Core
date: 2020-01-17 09:25:01
tags:
- Testing
- TFS
- ALM
- DevOps
- dotNet Core
---
It is pretty easy and straight forward to create a dotnet core application and run your unit tests during a build process.  However, I also have some integration tests that I run in my Dev and QA environments where I actually hit my test database.  This is not something that you would run in the build process as I do not have a database on the build machine and this is not something that would be worth the trouble to install one, especially if you are using a hosted build agent.
{% asset_img ASPNetCore.jpg "Integration Testing dotNet Core" %}
## dotnet test
This is a test that I would run as part of my deployment, after I have installed the application in one of my testing environments I want to know that the database is working correctly before I start running even slower tests like my automated functional graphical user interface tests.  The process that you used to run the dotnet core unit tests doesn't work quite the same.  Let's go over the normal steps that you would take in your build process to build and test, then we can cover why running integration tests during a deployment is not going to work quite the same.

```
dotnet build
dotnet test
```
These are the actual commands that are used to build and then test the dotnet core application.  In both of these situations it is expecting source code that it is building and then testing.  However, when you are deploying to an environment we are using the finished packages of the artifact to install the new version.  Remember we build once and deploy to every environment.
```
dotnet test /?
Usage: dotnet test [options] <PROJECT | SOLUTION> [[--] <RunSettings arguments>...]]

Arguments:
   <PROJECT | SOLUTION> The project or solution file to operate on.  If a file is not specified, the command will search the current directory for one.
```

If you look at the help for dotnet test you see that it expecting either a project or a solution which are both part of source not the finished compiled code.  This makes it fairly clear that the dotnet test is not going to be the way to go to run any integration tests during deployment of an environment.

## dotnet vstest
If you run the help on just the dotnet command there is another command that might be the answer to all of this.
```
dotnet -h
Usage: dotnet [runtime-options] [path-to-application] [arguments]
...
SDK commands:
    add             Add a package or reference to a .NET project
    build           Build a .NET project
    build-server    Interact with servers started by a build
    clean           Clean build outputs of a .NET project
    help            Show command line help
    list            List project references of a .NET project
    msbuild         Run Microsoft Build Engine (MSBuild) commands
    new             Create a new .NET project or file
    nuget           Provides additional NuGet commands
    pack            Create a NuGet package
    publish         Publish a .NET project for deployment
    remove          Remove a package or reference from a .NET project
    restore         Restore dependencies specified in a .NET project.
    run             Build and run a .NET project output
    sln             Modify Visual Studio solution files.
    store           Store the specified assemblies in the runtime package store.
    test            Run unit test using the test runner spedified in a .NET project
    tool            Install or manage tools that extend the .NET experience.
    vstest          Run Microsoft Test Engine (VSTest) commands
...
```
Well that looks like it might have some promise.  First thing we should do is just confirm what it is expecting for parameters.
```
dotnet vstest /?
Usage: vstest.console.exe [arguments] [Options] [[--] <RunSettings arguments>...]]
Arguments:
[TestFileNames]
    Run test from the specified files or wild card pattern.  Spearate multiple test file names or pattern by spaces.  Set console logger verbosity to detailed to view matched test files.
    Examples: mytestproject.dll
              mytestproject.dll myothertestproject.dll
              testproject*.dll my*project.dll
```
I am pretty sure that this is going to work so I include the compiled test assemblies in the artifact package of the build and use that command to run them in deployment.  However, that does not seem to work just like that because I am starting to see errors related to missing dependency files.  These are not files that I created but things it is expecting from the framework.

Somewhere along the way I did see some documentation mentioning something about published files.  Which I was not sure what they were getting at.  I run the dotnet publish command against the web site that I am deploying which makes sense as this is something that we have always done in order to get the pieces that are needed for the web site and this is usually zipped up.  Turns out you do the same thing against the test project which includes all those dependencies that I was missing.  My commands would look something like this:
```
dotnet publish <integrationTestLocation> --output <ArtifactStagingLocation>\Tests
```
That folder would then contain all the files I need to run these tests in a new environment.  Then all I would need to do from a command line task in the release pipeline is run the following command.
```
dotnet vstest <integrationTestLocation>\Integration.Test.dll /logger:trx
```
Run integrated test run and reports to Azure DevOps just like any other full framework test has done in the past.