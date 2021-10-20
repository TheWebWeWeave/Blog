---
title: An Argument against the Date Based Version Number
date: 2017-02-22 21:37:07
tags: ["ALM", "Products"]
categories: ["technology"]
archives: 2017
menu: "main"
author: "Donald L. Schulz"
---
{{<figure class="right" src="/images/version.jpg" width="250" alt="Version Sample">}}
In the past I have followed two types of version numbers for the products that I build and support on the side.  Products that were customer facing all followed the Semantic concept of version control.  If there was a big change but not breaking then the minor number incremented.  If the change could have potential breaking changes then the Major number was incremented.  This concept works well in that every time that code was changed the third digit, the build number was incremented.  We ignored the fourth number which was the revision as that was just a number to keep the build ID which was a makeup of the major, minor, build and revision, unique.  If I have 1 through 18 in revision numbers all for the same build, it means that nothing in the code has changed since revision 1. We are working on changes to the actual build definition and these are just builds of the same code.

## Projects that are not Customer facing
{{<figure class="left" src="/images/internal.png" width="250" alt="Internal Projects">}}
For other types of products, things that I used internally were given a different format because at the time I didn't think it mattered and my only goal was to be able to look at the properties of an assembly and know which build it came from.  For that I used a format that would change automatically for each build and I would never have to change any of the version numbers ever.  This format followed a pattern like YY.MM.DD.RR, the RR representing the revision number that I allow TFS to create automatically to keep the build number unique.  So for a build that was run on say February 23, 2017 that version number could look like: 
```
17.02.23.01
```
I would use a PowerShell script to write this to the assembly just prior to compile time and this would work as my version number.

I have used this format for years and there have been many blog posts on how to do this automatically in TFS ever since the 2010 release.  Back then we were building activates to be used in the xaml builds and since then the ALM rangers have converted this into a PowerShell script as part of the Build Extensions, as well as many others that are available in the TFS Market Place as a build task.  The basic idea is to have this format as part of the build definition and most of these tools will extract the version like number out of the build name and that becomes the version number.
```
Build number format: MyBuildName_v$(Year:yy).$(Month).$(DayOfMonth)$(Rev:.rr)
```
## But I Have Changed My Mind
{{<figure class="right" src="/images/changed.jpg" width="250" alt="Version Sample">}}
Since moving into a more DevOps mindset if you please, I was beginning to see that I was loosing some valuable information about my internal builds.  I had no way of knowing when an actual code change occurred because if I built the product on Feb 23 and then built it again on Feb 24 because I wanted to try something on the build machine there was no way to tell from the build number or the version of the assembly if anything had changed.  This is important stuff, but I also did not want to have to manually tweak the build number every time I did push something new into production and looking back at my old post [My New 3 Rules for Releases](/2016/09/my-new-3-rules-for-releases/) the tools and solution to accomplish this were right at my finger tips.

## But this is done on the Releases
Yes, they are and guess what? I did not have a formal release pipeline for some of these internal products.  Hey some of them were just packaged up as NuGet packages with a wrapper of Chocolate.  You will want to check out my post on [How I Use Chocolatey in my Releases](/2016/06/how-i-use-chocolatey-in-my-releases/) to really understand what I am talking about here.  

After thinking about this for a while and having similar discussions with clients I came up with the idea of having at a minimum a Dev and Prod environment.  The Dev environment would do what I pretty much have always done, it would deploy the application and maybe even run some tests to verify that the build has been successful.  Sometimes I find issues here and I return back to the source, fix it up and send out another build.  

When I am happy with the results I promote it to Production.  The promotion does not do anything to any environment or machine but does lock the build, increment the build number and my newest thing create a Release work item.

## Why Create a Release Work item
I will talk about this feature in more detail with some code samples in a future post. Briefly, the whole reason for the creation of a Release work item when I deploy to Production is to keep track of how many releases I have done in the last quarter.  I love good metrics and this is one that lets me know I am pushing code out into production and not just tweaking it to death.  Remember you can't get good feedback if you don't get it out there.

## In Conclusion
So there you have it, all my products internal or customer facing I have much more clarity as to when a build has new code in it.  I could have gone though source control and found out from the code history and found the latest changeset number and see the first time that this was used in a build but so much work for something that I can see at a glance and not having to look anywhere else for it. 