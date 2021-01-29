---
title: Testing a Docker Image as part of a Jenkins Pipeline
date: 2021-01-15 09:08:01
tags:
- DevOps
- Jenkins
- Testing
---
One of the things that I have wanted to do is to test the structure of a docker image that I have just built.  This should come right after the build stage so that if the structure is not what I expected then I want to fail the build so that I don't have a bunch of broken images sitting in my docker registry.  I guess that this happens because I am using a docker image to build my project and the result of that output is copied into a clean docker image that will be my final image that can them be run as a docker container.  The builder image should have been removed through this process but I have had things go wrong and then I don't see the problem until I deploy it into my development environment.  However, that then means that my broken image was published to my docker registry which is where the deployment would be pulling it from.

There are a couple of frameworks that you can use to make this happen and the one I am going to talk about today is the one from Google called container-structure-test.  This code runs on a Linux or OS X platform which works out really well for me because I pretty much run all my Jenkins and build activities on Linux machines and containers.  I will assume that you are building a Linux image and that my setup is similar to what you are currently doing.  Later in this post I do touch on running these container tests in Azure DevOps.

# Where to Get the Framework
This is an open source framework hosted on GitHub: https://github.com/GoogleContainerTools/container-structure-test  To install the binaries onto your Linux or OS X environment run the appropriate command:
```
OS X
curl -LO https://storage.googleapis.com/container-structure-test/latest/container-structure-test-darwin-amd64 && chmod +x container-structure-test-darwin-amd64 && sudo mv container-structure-test-darwin-amd64 /usr/local/bin/container-structure-test

Linux
curl -LO https://storage.googleapis.com/container-structure-test/latest/container-structure-test-linux-amd64 && chmod +x container-structure-test-linux-amd64 && mkdir -p $HOME/bin && export PATH=$PATH:$HOME/bin && mv container-structure-test-linux-amd64 $HOME/bin/container-structure-test
```
For those of you that have been following me for a while, I have this binary installed on my Jenkins container.  I added the installation of the binary as an update to my docker jenkins-master image.  Details of this are found in my [Maintaining the Golden Images](/2020/10/Maintaining-the-Golden-Images) post.

# The unit test
The test itself is just a yaml file with some value pairs set that are used to indicate to the testing framework what we are expecting and not expecting to see.  Let me show you a test I have been using for my blog and then go over what this all means.  There are four (4) types of tests that you can run with this tool.
1. **fileExistenceTests**, which check to make sure a specific file (or directory) exist within the file system of the image.  No contents of the files or directories are checked.  These tests can also be used to ensure a file or directory is not present in the file system.
1. **fileContentTests**, open a file on the file system and check its contents.  These tests assume the specified file is a file, and that it exists (if unsure about either or these criteria, se the above fileExistenceTests). Regexes can again be used to check for expected or excluded content in the specified file.
1. **metadataTests**, ensures the container is configured correctly.
1. **licenseTests**, check a list of copyright files and makes sure all licenses are allowed at Google. By default it will look where Debian lists all copyright files, but can also look at an arbitrary list of files.
```
schemaVersion: '2.0.0'
fileExistenceTests:
  - name: 'Docker Entry Point Exists'
    path: '/docker-entrypoint.sh'
    shouldExist: true

  - name: 'index.html'
    path: '/usr/share/nginx/html/index.html'
    shouldExist: true

  - name: 'Source Files should not exist'
    path: '/app/blog/'
    shouldExist: false

  - name: 'Hexo Link should not exist'
    path: '/usr/local/bin/hexo'
    shouldExist: false

  - name: 'Hexo should not exist'
    path: '/usr/local/lib/node_modules/hexo-cli/bin/Hexo'
    shouldExist: false
```
As you can see from this test file that I am just using the **fileExistenceTests** for all my tests as this was good enough to get me started on testing my images and represented the most issues that I would run into when things did not go well.

# What about Azure DevOps
I am glad you asked as there is a task that does just that.  In your Azure pipeline if you search for "Container Structure Test" you should see this task as it is built into the regular tasks that are part of the build.  You don't have to go looking for it in the Marketplace it should already be available to you.