---
title: When Should we Move the Work Items to DONE?
date: 2017-10-09 12:36:21
tags: ["ALM", "DevOps", "TFS"]
categories: ["technology"]
menu: "main"
archives: 2017
author: "Donald L. Schulz"
---
{{<figure alt="Done" class="left" width="150" src="/images/Done.jpg">}}
This is a very common question that I get asked by different software development teams as I make my rounds to helping clients with their ALM practises.  There is a common pattern associated with this question and I know this is the practise when I see a lot of columns on their Kanban boards or worse yet a lot of states that they are tracking on the work items.
### Fewer States (keep it down to no more than 4 or 5)
When I see more than the 4 or 5 out of the box states that start in a TFS out of the box template it tells me that the team is trying to micro manage the work items.  They are adding more work to their plates then they need to.  It really gets hard to manage the work when it goes beyond doing the work because then the question comes up with who is responsible for moving the work item to Done and when is it Done?

The goal behind the work items and here I am specifically referring to the Product Backlog Items (the requirement type) and the Bugs is to track the work to complete the described work.  This is in conflict to the pattern way of thinking that I spoke of earlier where the thought is that we need to track this work item through all the environments as we are testing and deploying.  I am telling you that you do not.  Initally when we are in the development cycle we are working closely with the testing team and as soon as we have something ready to test, they can test it right away because we have a proper CI/CD pipeline in place and can approve work that we have completed so that they can have a go at it to confirm that the new functionality or fix works as expected.
{{<figure alt="Done" src="/images/BuildThroughEnvironment.png">}}
If the functionality is correct, the initial tests are passing then we can go ahead and push the code to the parent branch (could be master or develop, depending on the process you are following) which starts the beginning of the code review and a new set of testing can begin as this should trigger yet another CI/CD pipeline but this time we are testing this against other code as well and making sure that all the code in the build is working nicely together.
### The Wrong Assumptions
An incorrect assumption that comes up when testing some of those very same test cases that were passing when we were doing the functional testing the first round are the same bugs.  Or are they?  The first round of testing you were in an almost isolated environment along side the development team but now that we are working from a merged branch such as master or develop. There is a good chance that they are related or it could be some other piece of code that is acting badly and we just happen to have caught it using the test case we used to test that new bit of functionality.
Not having that assumption and instead creating a new bug gives us a cleaner slate from where we can analyise this incorrect behaviour.  Remember that test cases live on until they are no longer useful for the purposes of testing the application.  Bugs and PBI's and Stories do not, they always end after the work has been completed.  They can come back as there are times where we might have missed something, but do not assume that is what happened.
### When Does the State for the Work Item switch to DONE?
The simple answer to that question is when the work is done.  The work is done when the coding and testing have been complete but this is going to be functional testing that we are talking about here and that testing was done from that active branch that was created for the development of this work.  We have developers and testers working side by side and in a CI/CD environment this is a very natural flow.  Work gets checked into source control, the build kicks off and deploys to the development environment (not your laptop) where the developer can give it a quick smoke test.  From there they can approve the build to move on into a QA environment.  If the testing from QA is successful then this could be a good place to implement a Pull Request.
{{<figure alt="New to Done" src="/images/NewToDone.png">}}
The Pull Request does a couple of things, it provides a great opportunity to force a code review and squish the multiple commits into one nice clean commit and to automatically close the work item (set it to DONE).
That Pull Request will then start another Build which then deploys to Dev to QA (this time funtion and regression testing) as this could be a potential candidate for production.
### Work Item is DONE but the testing continues
In a previous post [Let the Test Plan Tell the Story](/2016/04/Let-the-Test-Plan-Tell-the-Story/) I explain how the test plan is the real tool that tells us if the build we are testing is ready for a release into Production.  This is the tool we use to verify that the functionality of the current new changes as well as the older features are working as expected through test cases.  We are not testing the Stories and Bugs directly those are DONE when the work is done.
{{<figure alt="New to Done" src="/images/TestResults.png">}}