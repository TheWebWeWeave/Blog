---
title: 'Master Only in Production, an Improvement'
date: 2017-07-05 07:15:07
tags: ["DevOps", "ALM"]
categories: ["technology"]
archives: 2017
menu: "main"
author: "Donald L. Schulz"
---
{{<figure class="right" src="/images/master_branch.png" width="300" alt="Deployment from master">}}
Some time ago I wrote a blog post about [My New 3 Rules for Releases](/2016/09/My-New-3-Rules-for-Releases/) and one of those rules was to only release into production code that was built from the master branch.  In that solution I wrote a PowerShell script that would run first thing on the deployment to only go forward if the branch from the build came from master otherwise it would fail the deployment.  This gave me a guarantee that builds that did not come from master would never get deployed into Production.

This solution worked very well and guaranteed builds that did not come from master would ever get into Production, it was my safety net.  It still is and I will probably continue to use it but there has been an improvement in the process to make this even cleaner.  In my solution it was there as a safety net just to make sure that one day when I was clicking on things so fast and maybe doing more than one thing at a time that I did not cause this kind of error.
### Artifact Condition
The new improvement is what is called an Artifact condition and it can be specific to each environment that you are deploying to.  In this case I have selected my Production environments and said to only trigger a deployment in my Production environment when the Dev deployment succeeded and the branch is master.  Of course it still includes all the approval and acceptance gates but the key to note here is if those first two conditions are not met it is not even going to trigger a Deployment to Production.  In the past when a code from a none master branch was successful in Dev or QA I would have to fail it some where along the way to stop the pipeline and in this case the pipeline just nicely ends.  Much, much cleaner.
### How do you set it up
This is kind of tricky because in the VSTS Microsoft has just deployed a new Release Editor that seems to be missing this piece for now, not to worry as the new Release editor is still in preview and you can easily switch back and forth.  When you go to Releases and click on the Edit link and if the screen looks like the following, click on the Edit (Old Editor) link to switch back to the old style Release editor.
{{<figure src="/images/NewReleaseEditor.png" alt="The New Release Editor is missing this functionality">}}

Next you select your Production environment and click on the ellipse button and select the Deployment conditions link.

{{<figure src="/images/ConditionOption.png" alt="Selecting the Deployment conditions" >}}

### Finally the Configuration Screen
Now we are finally on the configuration page where all the real magic happens.  I have listed 5 simple steps that you follow to setup a deployment that will only trigger when the build came from the master branch and the previous build was successful. 
{{<figure src="/images/ConfigureScreen.png" alt="Configure for master branch only" >}}
1. First make sure that you have the option set to trigger a deployment into production after a successful deployment of the previous environment.
1. Next click on the new checkbox to check it as this sets some conditions to the new deployment
1. Click the Add artifact condition big green plus button.
1. Set the repository to only include the master branch as that condition
1. Finally click the OK button to save all you adjustments.

Now, you won't even be given the opportunity to promote the build into Production if it was not built from the master branch.