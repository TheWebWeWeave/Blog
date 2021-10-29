---
title: One Build Definition to Support Multiple Branches
date: 2017-06-01 10:23:26
tags: ["ALM", "Git", "DevOps"]
categories: ["technology"]
author: "Donald L. Schulz"
archives: 2017
menu: "main"
---
Before I moved to git, I had the same situation that many of you have had when it comes to managing build definitions.  I had a build definition for each branch and for a single product this could have been several all doing the same thing.  Yea, sure they were clones of each other and all I really needed to do was to change the path to the source in each case.  Then in order to keep track of what each of these builds was for and what might have triggered it I would develop some sort of naming convention so that I could sort of tell without having to open it up.  This really felt dirty and raised a red flag for me because once again we were introducing something into our environment that was not the same, but sort of the same.  Wouldn't it be better to actually have one build definition that we can use for all these various types of builds and different branches?

### Builds with Git
{{<figure alt="git" class="right" width="200" src="/images/git-logo.jpg">}}

When you really look at git, you learn that a branch is nothing more than a pointer to a changeset.  When you compare this to any of the centralized source control systems out there including VSTF the branch is pointing to a copy of the source control in a different location.  With that said, then I should be able to create one build definition and with a wild card be able to even trigger a Continuous Integration (CI) build by checking in code and it would use the appropriate branch.  That is absolutely true, and for the remainder of this post we will go over the simple steps to make that happen.

### Same Build Definition for All Branches
I will assume that you have a build that is working and your source code for this build is a git repository on TFS or VSTS.  Because it is a Git repo, you can specify path filters to reduce the set of files that you want to trigger a build.  According to the documentation, if you don't set path filters, the the root folder of the repo is implicitly included by default.  When you add an explicit path filter, the implicit include of the root folder is removed.

This is exactly what we want to do but we want to include a couple of different paths.  So lets start by going to the Build Definition and clicking on the Triggers sub-menu.  Make sure that the Continuous Integration switch is turned on and next pay our attention to the Branch Filters.  In my branching schema I use three (3) kinds of paths.  Master of course, as this is where all the finished and releasable code lands up.  I also use features for any new items I am implementing and I usually include the Work Item Number in my branch as well as a short description.  So an example of a feature branch for me would look something like:
```
feature/3660_NewFunctionality
```
With that said I have a similar path for bugs which are things that have an incorrect behavior or something that needs to be fixed.  In my branch Filters I would include 3 paths and the feature and bug would include the wild card to have everything included that is part of a feature or bug branch.
{{<figure alt="CI Branch Filters" src="/images/CIBranchFilters.png">}}

With this in place my commit pushed to the remote repository will kick off a new build for any new features and bugs that I have been working on.  Even better, the very same build definition kicks off when ever I complete a pull requests into Master.  Not a clone or a copy but exactly the same build.  There is never a question about what happened to the build, but rather what code change or merge did we introduce that caused this problem.

Before I discovered this I was happily flipping the branch name between my features and bugs, the definition defaulted to master.  Because of that I wasn't even bothering with CI for the development branch and the trick was to always remember to build from the correct branch.  Now I don't even have to think about that because the branch that triggered the build is the branch that is being built.  Just another thing that I could have easily screwed up is out of the picture.  I don't even have to think about kicking off a build and deployment as this just happens every time I commit my code and push those commits up to the remote Git.