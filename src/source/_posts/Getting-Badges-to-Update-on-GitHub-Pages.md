---
title: Getting Badges to Update on GitHub Pages
date: 2021-07-06 14:48:07
tags:
- Jenkins
- DevOps
- Dashboard
---
A while back I was looking for a solution that would give me a better picture when it came to deployments and releases then what I was getting from Jenkins.  Jenkins may be the most popular CI build orchestrator in the world but lacks in the area of deployments.  There are different needs when it comes to deployments and releases that are quite different then what you want from a CI tool.  In the context of a CI build it's target is an artifact and in my case these are currently Docker Images but they could be release packages.  In the context of a CD tool I will have several targets that are instances of the artifact that was built.  Now in the interest of testing and promoting the good builds, I could have several builds that are in different stages of development and others are ready to move on.  From a CD context I need to know exactly what versions are in each of my environments of Dev, Test, Stage, Production.

My needs are quite simple as this is my own personal project and do all the development and some of the testing.  The only people that have access to the source and the pipelines are the same ones that take responsibility for the deployments to be turned into releases and approved for Production.  This eliminates the need for an approval process in my world but I do have builds in various states that are in development or currently being tested so the need still exists where I need to know what I have in each of my environments.  I do have a main rule in my Jenkins pipeline to only deploy builds that have come from the master branch.  This saves me from the embarrassment of pushing something into production that is still have baked.  This is intended as a safety gate cause sometimes you are tired and if my CI pipeline has stopped at a stage waiting for me to push the proceed button I might not notice that this might just be an alpha or beta build.  I wrote a blog some time ago called [Master Only in Production, an Improvement](/2017/07/Master-Only-in-Production-an-Improvement/) which was based around Azure DevOps but the concept is the same in Jenkins.

### Badges
I realize that all I really need were some build and deployment badges that would tell me what was successfully deployed at each environment.  If I had that and it was updated automatically this would solve my problem and I could stop looking for a simple CD solution.  I could get everything I needed from the Jenkins pipeline and not feel like I have some process that really just feels like over kill.

In today's post I will show you how a constructed the badges and then how I consume them on the README.md page.
