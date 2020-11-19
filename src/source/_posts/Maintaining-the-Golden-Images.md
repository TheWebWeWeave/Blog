---
title: Maintaining the Golden Images
date: 2020-10-31 13:17:51
tags:
- DevOps
---
Today I wanted to take you down an unusual path as I disclose some of the docker structure that I have configured recently in my environment.  It has been my goal for a while now to run everything that I can in a docker container.  What I mean by everything, it is more than just my web sites and applications that I have been working as side projects for years but also includes some of the open source support tools that I use.  This blog post is going to mostly cover these tools and here is a list of just a few of them to give you an idea of what I am talking about.
1. Jenkins Server
1. Portainer
1. Seq Server
1. SonarQube
1. Adminer
1. letsencrypt
1. nginx

Okay, this sounds okay and not that big of a deal as many of these open source projects are released as a docker image that you can run in a docker container.  The nice thing about a docker image is that it will always work, no hunting for missing dependency or solving a conflict with another program because the image has everything it needs to run properly.  Now here is the rub, I am running the Jenkins Server in a Docker container creating a docker image of the build that I am building in my CI/CD environment.  This almost sounds crazy almost as weird as the double hop I used to do when logging into one virtual machine to remote into a second virtual machine.  Feels a little weird but it works although you need to pay close attention to which environment you are actually in.

Jenkins Needs Docker Installed
------------------------------
First off on my Jenkins image I needed a few things to be part of the image so we need to add some layers to the already available Jenkins Image.  The best way to hand this is to create a new repository for this project.  Not like it is going to contain any C# code but it will contain my Dockerfile and Docker-Compose files to put these pieces together.  New software to be installed should be part of the Dockerfile, this way I even have a full history of what was in this image and say that there is a newer version of Jenkins I want to update to I just update the base image in the Dockerfile and I get all the pieces that I have installed are part of that image as well.  I am getting a little ahead of myself so lets look at that Dockerfile.
