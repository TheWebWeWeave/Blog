---
title: My Roadmap for 2021
date: 2020-11-30 12:08:17
tags: ["DevOps", "Testing", "Soap Box", "Docker"]
categories: ["technology"]
archives: 2020
menu: "main"
author: "Donald L. Schulz"
---
{{<figure alt="Take a trip with me to navigate new things in technology" src="/images/RoadMap.jpg">}}
For my next couple of blog posts I thought I would try something a little bit different then what you might usually see on these pages.  Today I am going to talk about 2021, the topics that interest me right now and what you could be expecting to see in the coming months.  The year 2020 has certainly been an interesting one for many.  This was a massive change for a number of businesses where they were forced to rethink the way they were doing business.  We have gone through shifts like this before that began in the late 70's where the news was about moving into the information age where the use of computers would level the playing ground and flatten the typical business hierarchy where instead of information flowing from the top down through to the direct reports to the supervisors and managers then to the worker bees, information would start to flow upstream.  Even today, you do realize that the status meetings that you have are to let your managers know so that they can tell their bosses what is going on.

Another example of change that came to be was on-line shopping and how it has almost pulverized some of the bigger brick and mortar stores.  Some got into the flow of things by building up their on-line presence and started closing some of their less profitable locations.  These were all changes that came in and forced businesses to change or look at falling apart at the seams.  There has been a lot of changes in every business since the 70's, today there is not a single business that does not have some level of technology driving their business.  Industries that you would not even think now have a whole department of IT staff to manage the infrastructure to writing the custom software that keeps them unique from their competitor.  Which was the real driving force that caused many businesses to take on a more agile and adopt a DevOps approach to their software development as an effort to get something out there faster, instead of big massive releases every few years down to multiple updates in production on a daily basis.  Small manageable pieces at a time.

The current pandemic has forced many companies to rethink their working from home policies.  For the longest time I have been saying that I get more work done when I take it home and work on it then when I am at the office.  The problem is that many business have millions of dollars invested in brick and mortar buildings and wanted them filled and just would not hear of any long term working from home policy.  The businesses that are working well already had working from home policies in place and it was just like another day except that we have the house locked down, don't see or allow anyone to come over and only go out to get the groceries we need about once a week.  Businesses that were able to adapt and make the changes had a bit of a rough start but soon got it figured out we have Zoom, and Microsoft Teams on a speed button and we are moving on.

# What am I Interested in 2021
Okay, my opening monolog took off on a bit of a different tangent then where I was originally going.  Let me come around now and talk about what I really wanted to say.  What am I looking into and what do I want to blog about next year.  You will see if you have been following my blogs for a while that in 2020 I took a real interest in **Jenkins** and **Docker**.  **Docker** was a real game changer for me this past year.  I dabbled in it here and there in previous years but 2020 was a year that I changed everything that I do now comes down to a container.
## Kubernetes
{{<figure class="left" width="200" alt="Kubernetes" src="/images/kubernetes.png">}}

This is a technology that you love what it does but you hate the way it does it (sometimes).  I have spent some time in this technology and the reality is there are just a ton of tools out there that will help you achieve what you need to do.  We will be exploring some of these and provide some insights and tips.  Related to the cluster management of **Kubernetes** is a lesser known technology from **Docker** called **Swarm** and is less complicated.  I will write some posts on this maybe even do a comparison to Kubernetes and when you might want to select **Swarm** instead.

Speaking of **Docker** I want to continue on my **Docker** story that I have covered in a couple of Blog posts already but there is more to this story.  Things like passing in the custom parameters for various environments.  I am a strong believer in build it once and deploy it everywhere and this is especially true for **Docker**.
## Selenium
{{<figure class="right" alt="Selenium" src="/images/selenium.jpg">}}

Testing is an important part of **DevOps** and I have done a fair amount of work with **Selenium** in the past.  Recently there is a number of frameworks that are essentially wrappers around Selenium to make it even more powerful in the area of automated functional testing.  We will be looking at some of those frameworks as well as **Selenium-Grid** and running a whole **Hub** and **Nodes** in **Docker** containers.
## Hugo and Go

{{<figure alt="Go Hugo" src="/images/hugo-logo.png">}} 

My interest in static web sites is making a come back to me.  As I am about to rework a number of web sites that I manage and take care of I realize that they would be better served as static web sites as there is no logic on those pages, as they just need a good navigation system won't need a database and will be really easy to scale up or down.  If you have been following me for a while you will note that I have mentioned it a few times that [**this blog site is a static web site powered by Hexo**](/2016/01/A-New-Start-on-an-Old-Blog/) which has suited me well since 2016.  **Hexo** was super easy to setup but it is completely node/javascript based.  **Hugo** is another static web site generator built in **Go** and although you really don't need to know **Go** to use it, it does use **Go Templating** language to build its themes and customizations, which is going to give me a lot more flexibility in what I can do with the remake of the web sites beyond a blog.

Speaking of **Go**, this is another language that has really got my attention lately.  This language complies code in native code for the platform so no run time is needed.  It looks to be almost as powerful as **C** and **C++** but with a much cleaner structure.  I will blog on this very powerful language that just recently celebrated its 10th anniversary year.
## Conclusion
So, I hope to see you next year as we explore some of these new and exciting technologies.  It should be an adventure and a trip worth taking.  One thing I can promise you is that I will try very hard to keep it interesting.  Take care and as always, comments and thoughts are welcome here.
