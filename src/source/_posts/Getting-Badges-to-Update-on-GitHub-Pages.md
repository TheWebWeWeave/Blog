---
title: Getting Badges to Update on GitHub Pages
date: 2021-07-06 14:48:07
tags:
- Jenkins
- DevOps
- Dashboard
---
A while back I was looking for a solution that would give me a better picture when it came to deployments and releases then what I was getting from Jenkins.  Jenkins may be the most popular CI build orchestrator in the world but lacks in the area of deployments.  There are different needs when it comes to deployments and releases that are quite different then what you want from a CI tool.  In the context of a CI build it's target is an artifact and in my case these are currently Docker Images but they could be release packages.  In the context of a CD tool I will have several targets that are instances of the artifact that was built.  Now in the interest of testing and promoting the good builds, I could have several builds that are in different stages of development and others are ready to move on.  From a CD context I need to know exactly what versions are in each of my environments of Dev, Test, Stage, Production.
