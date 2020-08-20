---
title: The Path to Docker
date: 2020-08-20 10:53:49
tags:
- DevOps
- Docker
---
I have to say that I love Docker and working with Docker Images inside of containers.  However, for the longest time I think I was looking at this from the wrong angel and I am sure that there are others out there doing the same thing.  I think that this is because the explenation of this technology is usually done from an operational point of view and not from a developer.  Before I get into my explanation and how I would recommend approaching docker we need to follow a path that as developers we should have a clear understanding at least by now and how docker is just an extention in our growth to packages or the artifacts.
## What are Packages?
Before there were any kind of packages or package management systems if you wanted to use a certain library in your code you needed to do your research to make sure that you also had all the dependencies and somethings it really mattered what version of that dependancy was needed.  This was especially problematic with open source libraries.  Take for instance squirrel.windows which is a windows click-once alternative that actually works.  When I look at the package description about this libary it has four (4) other libraries that it needs and at least one is a very specific version:
```
DeltaCompressionDotNet (>=1.1.0 && < 2.0.0)
Mono.Cecil (>=0.9.6.1)
SharpCompress (=0.17.1)
Spat (>=1.6.2)
```
