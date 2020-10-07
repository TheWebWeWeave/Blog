---
title: My Take on Software Versioning
date: 2020-09-24 15:18:50
tags:
- DevOps
- SemVer
---
A couple of years ago I was thinking of building my own version generator that would work automatically based on the kind of work item I was working on.  This would have been an api call that would be called from the build taking in the branch information checking if this branch has already been registered and return me the appropriate version number.  I thought it was a pretty cool idea and would have worked for me because I work on a single Product Backlog Item (PBI or Story) or Bug at a time and it gets released as soon as it is ready.  This generator would follow the Semantic versioning.  Before we get to much farther lets talk about what Semantic Versioning (also referred as SemVer) is.
## SemVer Versioning
The principal parts of Semantic Versioning are in 3 parts called Major.Minor.Patch and they follow very simple rules that are as follows:
- **MAJOR** increments by one when this api, assembly breaks compatibility with the previous existing versions.  This tells the consumers of your software that they will likely have to make some changes to consume this version of the software.  Consumers here could even be other internal products that you share these resources.
- **MINOR** increments by one when you have added a new feature to the product but is still backwards compatible to the previously released software, just has more stuff.
- **PATCH** increments by one when you fixed a backwards compatible bug.  In other words no new functionality but a fix and this fix will not break previously released software.
- Any time that the **MAJOR** is incremented the MINOR and PATCH are reset to 0.  If the **MINOR** is incremented then just the PATCH is reset to zero.
That is the basic rules.  However, SemVer also supports the idea of pre-release suffix that gets added to the end of the SemVer version.  Such as **2.2.0-beta.2** which in my case would tell me that this version did not come from the master branch.  

In my world the build and the version number are the same, as I don't want another number to track I want there to be a one to one relationship here. The other thing that this suffix does is preserve the final version number so that when the previous version of a product was say: v1.2.3 and you are adding a new feature you want the final version to be v1.3.0 but if every time you commit code the build number and therefore the version number has to increment so until we are on the final build which would be coming from master we have this suffix that can increment all that it needs to and for the final build we just strip that all off and we have our nicely reserved version/build number.

For more information on SemanticVersioning check out [https://semver.org](https://semver.org)
## Killed the Project
As I was saying at the start of this post, that I was thinking building a version generator with an api call in the build process, well that never materialized.  Instead I looked into the git community a little more and discovered that a number of them used gitversion which is a semver version generator that works specifically in git.  If you are using Azure DevOps for your build pipelines there are tasks for gitVersion that you can just plug in.  Even in GitHub actions, there is a gitVersion that you can install in your action and call.  Now, I don't think that this tool works as good as my idea but I could make it work with some work arounds but it workable so lets look that this tool next.
## GitVersion
GitVersion is a command line tool and an open source project that can be found on GitHub: [https://github.com/GitTools/GitVersion](https://github.com/GitTools/GitVersion)  This is the one that I have been using which is written in C# but I did notice as I was doing a search for it that there are many others that are derived or forked from this project.  However, this is the one that I will be talking about here as it is the one I am most familiar with.  The tool itself can be motivated to work a number of different ways.  If you use tags it will make calculations from the tags or if you use a release branch with a version number it will work with that.  For complete gitVersion documentation checkout their documentation pages: [https://gitversion.net/docs/](https://gitversion.net/docs/)  For the rest of this post I will describe how I have been using it.
## YAML File and Branches
To start I use two types of topic branches.  What I refer to as a topic branch is any branch that is pulled from the master branch to do some work.  When the work has been completed and tested there is a pull request to get it back into master which is where the final build would be created from and then move down the pipeline to possibly getting into Production.  I use two prefixes for my topic branches:
* feature
* hotfix
followed by a forward slash "/" and then a work item number or a short description of what I am doing in this branch.  Finally there is a yaml file that sits in the root of my repository called GitVersion.yml and here is the content of what you might find in there.
```
mode: Mainline
next-version: 1.2.0
branches:
  feature:
    tag: alpha
  hotfix:
    tag: beta
  master:
    tag: ''
```
GitVersion has 3 modes in which it can operate on and if you look at the documents gives you a pretty clear picture of what they are all about.  I am using Mainline mode because I am basically working in a similar flow to GitHubFlow which is working off of master.  I pull my topic branch to do my work and then pull request to get it back into master.  When I first started working with this tool, that was the version that I wanted gitVersion to calculate from.  It makes a calculation from every commit.  In the next section I have my branches defined and what tag (suffix) I should put at the end of the calculated version number.  As you can see master is an empty tag so I don't get any of the pre-release symbols on it.
## Caveat and Simple Solution
One of the things that I did have a problem with using the Mainline mode is getting it to recognize that this was a new branch and therefore a new feature or hotfix but it does not.  I did not want to use tags or a release branch as that is not how I want to work.  I want these features and bug fixes to go out one at a time and as soon as they are ready.

My simple fix is just to have somewhere included in my first commit comment one of the following statements
* +semver: feature
* +semver: fix
* +semver: broken

Feature will increment the minor number, fix will increment the patch number, and broken will increment the Major number.

This is how I use gitVersion to manage my version/build numbers and maybe one day I will still build that api so that I will never have to think about this again but until then this works just fine for me.
