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


# What about Azure DevOps
I am glad you asked as there is a task that does just that.  In your Azure pipeline if you search for "Container Structure Test" you should see this task as it is built into the regular tasks that are part of the build.  You don't have to go looking for it in the Marketplace it should already be available to you.