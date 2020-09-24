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
That is the basic rules.  However, SemVer also supports the idea of pre-release suffix that gets added to the end of the SemVer version.  Such as **2.2.0-beta.2** which in my case would tell me that this version did not come from the master branch.  For more information on SemanticVersioning check out [https://semver.org](https://semver.org)
## Killed the Project
As I was saying at the start of this post, that I was thinking building a version generator with an api call in the build process, well that never materialized.  Instead I looked into the git community a little more and discovered that a number of them used gitversion which is a semver version generator that works specifically in git.  If you are using Azure DevOps for your build pipelines there are tasks for gitVersion that you can just plug in.  Even in GitHub actions, there is a gitVersion that you can install in your action and call.  Now, I don't think that this tool works as good as my idea but I could make it work with some work arounds but it workable so lets look that this tool next.
## GitVersion