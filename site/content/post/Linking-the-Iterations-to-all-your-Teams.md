---
title: Linking the Iterations to all your Teams
date: 2018-03-02 15:03:17
tags: ["ALM", "PowerShell", "TFS"]
categories: ["technology"]
archives: 2018
menu: "main"
author: "Donald L. Schulz"
---
 I am sure that there are several development teams out there that work similar to me.  I am a big fan of the one TFS Project to rule them all and then using teams to separate the work.  In my case I am working on my own but support several products, so I have a team for each of the products even though I am pretty much the only member on all those teams.  This gives me great visibility at the root where I can see everything that is going on and in many cases I might have several products that I am working on in the same sprint.
 {{<figure class="right" src="/images/vstsLogo.png" width="200" alt="vsts">}}
You are probably a larger organization than I and you might actually have teams associated with the Teams but similar to me you are all sharing the one set of iterations.  This way everyone in the TFS Project are all on the same sprint which really makes the whole one TFS Project to rule them all really powerful.  We can easily jump around and see how progress is going overall and yet still have the individual teams with their separate backlog list and burn down charts.

### Adding New Iterations
The problem appears when ever I add additional iterations to my TFS Project.  This doesn't happen all that often, maybe once a year or so but once I have the Iterations defined I have to go to each team and select them into their Iteration pool.  Doing this manually can be quite tedious, and I did it this way for a while, "I only do this once a year", was my justification.  However I would be thinking that there should be an easy way to automate this.  Well today, I thought I would tackle this and either write some scripts to accomplish this or find a good solution out there that maybe someone else has already accomplished.

### AIT.VSTS.Scripts
Turns out there is an open source project out there that does exactly that.  I just needed to create some scripts to load and then call the functions to go through all my teams and set them up with all the iterations that I have.  The nice thing about this script is that if an iteration already exists for that team it just displays the information without changing it and adds the ones that are missing.  First things first, lets get this open source project which is in GitHub and I would suggest cloning it.
```
git clone https://github.com/AITGmbH/AIT.VSTS.Scripts.git
```
With the repository cloned to our local computer we are ready to start to build our own script that we will run using the scripts that are in this open source project to do the actual work.  Okay, so before we start writing this script lets make sure that we have a bunch of Iterations setup and in case you are not sure what I am talking about here or how to do that, lets cover that first.
### Setting up the Iterations at the Project level
First make sure you are at the Project level of the TFS Project you want to add Iterations to.  You should not be on a team within the Project, you want to be at the root of the Project container.  Then click on the Gear icon and in that drop down select Work.{{<figure src="/images/GettingToWorkArea.png" alt="work menu">}}
While in the Work menu select the Iteration sub menu and then the next step will depend if you are on the top level or on one of the existing Sprints.  As you can see my selection is on the Root of the Iteration (indicated by the light blue background bar through 3WInc) so I would click on the New Child button as all Iterations fall under this root.  If however I was on Sprint 1 or Sprint 2 I would then click on New which would then give me a new iteration at the same level.  If while on one of these Child Iterations and I clicked on the New Child button, the Iteration would be a Child of that Iteration.
{{<figure src="/images/NewIterationChild.png" alt="work menu">}}
Enter the name of your new Iteration, then the Start and End dates, these dates should all ready be known by TFS and as soon as you click on these boxes the dates show up with what {{<figure class="right" src="/images/SaveNewIteration.png" width="300" alt="work menu">}} the next Iteration start should be and then when you click on the end date it should show you what the end date should be as this is based on the pattern you may have started.  If this is your very first Iteration you will need to set this manually and then every Iteration after that TFS will know and understand the pattern.  Click on the Save and Close button.  Add as many Iterations as you need, as you can see by my images I am already making Iterations for next year.

.

.
### Getting a Token from TFS or VSTS
One of the other things that we are going to need before we really get into the custom PowerShell script that we are going to use to apply all these Iterations to all the teams is a Token. The Token will be the way that the PowerShell tool can authenticate against your version of TFS or your instance of VSTS to perform the work.  From your instance of TFS or VSTS click on your Profile.  This is on the right hand side of the web page and represented by your picture if you have one in your profile or it could just be your initials.  Click on it and select Security.

{{<figure src="/images/OpenProfile.png" alt="profile">}}


On the Security page you want to make sure you have Personal Access Token (PAT) selected and then click on the Add button.

{{<figure src="/images/AddNewToken.png" alt="pat">}}

Give your token a name, select the length of time that the Token can last (the maximum is a year).  If you are on VSTS the Account will already be filled in for you.  You can accept the default scope which is not to restrict the scope at all, and keep in mind that this Token is based on your Profile so you can't create a Token that has more permissions than your role provides.  At the bottom of this screen there is a button to Create Token.
{{<figure src="/images/MyNewToken.png" alt="profile">}}
A Guid like string will appear and you want to copy this and store it somewhere because this is the only time that TFS or VSTS will show you this value and this is your Token that we will need in the next step.

### Now We Put Together the Actual Script That we Run
With all these pieces in hand we begin the process of building our PowerShell Script that will iterate through our Teams and update the Iterations.
Create a new PowerShell script, I called mine "UpdateIterations.ps1".  The first line is just a comment that says that we want to load the module that we are going to use into memory.  The second line will do a Change Directory (cd) to the location on your computer where you have the GitHub project cloned to.  The third line will load the module Releate-VstsIteration.ps1 into memory.  It is important that you have written this line exactly as shown. It is a "period space period backslash Relate-VstsIteration.ps1"  The next part of the script is an assignment of the variable $token with the PAT that you stored in the previous step.
```
# This loads the Releate-VstsIteration scripts and modules into memory
cd C:\git\GitHub\AIT.VSTS.Scripts\Iterations\TeamAssignment\
. .\Relate-VstsIteration.ps1

# Before we really get started lets setup some variable that are sure to change like the token.
$token = "<Your Token Goes Here>"
```
All that is left is a line for each team that you want within your TFS Project to update with the list of iterations that you have created in the Project.  In the sample below you would replace the {account} with your actual VSTS account name and the {TFS Project} with the actual name of the TFS Project.  For TFS you would replace that whole connection to the URL of your TFS instance with a forward slash and the name of the TFS Project.  What you have in the -Username parameter does not matter.  For the -TeamList paramter this is where you put the name of your team.
```
Relate-VstsIteration -Projecturi "https://{account}.visualstudio.com/{TFS Project}" -Username "user@example.com" -Token $token -AuthentificationType "Token" -TeamList "Your First Team"
Relate-VstsIteration -Projecturi "https://{account}.visualstudio.com/{TFS Project}" -Username "user@example.com" -Token $token -AuthentificationType "Token" -TeamList "Your Second Team"
```
Make as many lines of this line of each of your Teams, I just have two listed here but my real set of Teams is really around 12.  With all that in place you just need to run this script which will jump into the location where the open sourced AIT.VSTS.Scripts reside, load the module and run your scripts.  In PowerShell you will see messages about Iterations being updated for each team as it goes through the list.  Once the script has completed go to one of your teams and open the Backlog list and refresh the page, and like magic all the iterations appear.