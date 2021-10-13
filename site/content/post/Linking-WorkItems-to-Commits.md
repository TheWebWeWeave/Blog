---
title: Linking WorkItems to Commits
date: 2020-02-22 16:40:03
tags: ["git", "ALM", "TFS"]
categories: ["technology"]
archives: 2020
menu: "main"
author: "Donald L. Schulz"
---
This post is about linking work items to a git commit within Azure DevOps.  Doesn't it do this almost automatically, you may be thinking?  Well that was why I was surprised when I linked a work item using Visual Studio and then also tried to link the work items using the # symbol and the work item Id and found that the commit was not linked.  I am almost always on top of the changes that are released almost every 3 weeks in the release notes for each sprint.  However, this change seemed to have slipped in without me noticing.

A few months ago I created a new Project and then imported the git repositories from an older project.  When I noticed that the work items that I link to from the individual tasks were not creating links to the commits that contained that work.  I am not sure when this change took place but this is now part of the optional settings for the repository.

Start by going to the Project Settings which is found at the bottom of the menu pillars.  After this Project Settings menu opens up go down to the Repositories.  This will expand the list of git repositories in this project.  Each repository has a set of 3 sub menu tabs [Security, Options, Policies]  Click on the Options tab and there you can see that there is an option for linking work items to commits.  Make sure that this is on for each of the git repositories.

{{<figure src="/images/VersionControlSettings.png" alt="Version Control Option Settings">}}