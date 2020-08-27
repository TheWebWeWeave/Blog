---
title: The Path to Docker
date: 2020-08-20 10:53:49
tags:
- DevOps
- Docker
---
I have to say that I love Docker and working with Docker Images inside of containers.  However, for the longest time I think I was looking at this from the wrong angel and I am sure that there are others out there doing the same thing.  I think that this is because the explanation of this technology is usually done from an operational point of view and not from a developer.  Before I get into my explanation and how I would recommend approaching docker we need to follow a path that as developers we should have a clear understanding at least by now and how docker is just an extension in our growth to packages or the artifacts.
## Better than a Virtual Machine
A number of books and articles that cover the introduction of Docker will often start out by how it compares to virtual machines.  A virtual machine takes up all the resources of a whole computer as it will duplicate the operating system all the programs it needs to run.  A docker container on the other hand is a very thin layer that sits on top of a shared operating system and just contains all the dependencies needed to run an application.  I remember when Microsoft was promoting their new line of servers many many years ago, they came up with a slogan that has always been stuck in my mind.  "Do more with less"  Although this was partially the right story as you were able to use fewer servers but if each of them needed about 8 GB to run and you host them as virtual machines on a big host machine, each of those servers would still need the same amount of resources.  That would be 8 GB times the number of servers you are putting in this host.

Docker containers on the other hand does not have this kind of over head. Remember the shared operating system would have enough resources for it to run comfortably and each container would only need enough resources to run its application which would be considerably less.  You truly can do more with less.  Then you start to look at what can be done with either Docker Swarm or Kubernetes where you have several machines in a cluster with several instances of the application running so if one crashes another one is built on the fly really becomes a modern miracle from an operations perspective.

This all is really quite amazing things about Docker Images and while most books and articles come from this angle to teach or bring awareness to the many uses of Docker I think there is a different way to look at this from a DevOps perspective.  This is the part that really got me going on this Docker story and that is in the area of packaging or the result of a build artifact.  To make sure that everyone is on the same page, lets take a little history lesson on the package front.

## What are Packages?
Before there were any kind of packages or package management systems if you wanted to use a certain library in your code you needed to do your research to make sure that you also had all the dependencies and somethings it really mattered what version of that dependency was needed.  This was especially problematic with open source libraries.  Take for instance squirrel.windows which is a windows click-once alternative that actually works.  When I look at the package description about this library it has four (4) other libraries that it needs and at least one is a very specific version:
```
DeltaCompressionDotNet (>=1.1.0 && < 2.0.0)
Mono.Cecil (>=0.9.6.1)
SharpCompress (=0.17.1)
Spat (>=1.6.2)
```
