title: A New Start on an Old Blog
tags:
  - Blogs
  - ALM
date: 2016-01-14 20:21:45
---

{% img right /images/V__FD25.jpg 450 450 "Fall colors in California" %}
It has been quite a while since I have posted my last blog so today I thought I would bring you up to speed on what I have been doing with this site.  The last time I did a post like this was back in June of 2008.  Back then I talked about the transition that I made going from City Desk to Microsoft Content Management System which evenually was merged into SharePoint and from there we changed the blog into DotNetNuke.

Since that time we have not created any new content but have moved that material to [BlogEngine.Net](http://dotnetblogengine.net/) and this really is a great tool but not the way I wanted to work.  I really do not want a Content Management system for my blog, I don't want pages that are rendered dynamically and the content pulled from a database.  What I really wanted were static pages and the content for those pages be stored and built the same way that I build all my software, stored in Version Control.

Just before I move on and tell you more about my new blog workflow I thought I would share a picture from my backyard and that tree on the other side of the fence is usually green it does not change colors every fall but this year the weather has been cooler than usual, so yes we sometimes do get fall colors in California and here is the proof.

## Hexo

{% img right /images/hexo.jpg 100 100 "Hexo Logo" %}
[Hexo](https://hexo.io/) is a static page generator program that takes [simple markup](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet) and turns it into static html pages.  This means I can deploy this anywhere from a build that I can generate it just like a regular [ALM](https://en.wikipedia.org/wiki/Application_lifecycle_management) build because all the pieces are in source control.  It fully embrasses git and is a github open source project.  I thought that moving my blog to Hexo would help me in too ways, besides giving me the output that I am really looking for but also to use as a teaching tool on how the new Build system that is part of TFS 2015 fully embraces other technologies outside of dotNet and the Visual Studio family.  From here I check-in my new blogs into source control and that triggers a build which puts the source into a drop folder which is then deployed to my web site which is hosted on Azure.

As of this post I am using FTP in a PowerShell script which is used to deploy the web site which is not ideal.  I am working on creating an MSDeploy package that can then be deployed directly onto the Azure website that is hosting this blog.

## The Work Flow

The process begins when I want to start a new blog.  Because my git repositories are available to me from almost any computer that I am working with I go to the local workspace of my Blog git repository checkout the dev branch and at the command line enter the following command
```
  hexo new Post "A New Start on an Old Blog"
```
This will place a new md file in the _post folder with the same name as the title but the spaces replaced by hyphens ("-").  After that I like to open the folder at the root of my blog workspace using [Visual Studio Code](https://code.visualstudio.com/).  The thing that I like about using Visual Studio Code as my editor is that it understands simple markdown and will give me a pretty good preview as I am working on it and if my screen is wide enough I can even have one half of the screen to type in the raw simple markdown and the other half to see what it looks like.

The other thing that I like about this editor is that it understands and talks git.  Which means I can edit my files and save them and Visual Studio Code is going to inform me that I have uncommitted changes so I can add them to staging and commit them to my local repository as well as push them to my remote git repository.  Above you may have noticed that before I began this process I checked out the dev branch which means that I do not write my new posts in the master branch and the reason for that is that I have a continious integration trigger on the build server that is looking for anything that is checked into the master on the remote git repository.  Because I might start a blog on one machine and finish it on another I need some way to keep all these in sync and that is what I use the dev branch for.  Once I am happy with the post I will then merge the changes from dev into master and this will begin the build process.

## Publishing the Post

Once I am happy with my post all I need to do is to merge the dev branch into Master and this starts the build process.  Which is really just another Hexo command that is called against my source which then generates all the static pages, javascript, images and so on and puts it into a public folder.
```
  hexo generate
```
It is the content of this folder that then becomes my drop artifacts.  Because the Release Manager also has a CI trigger after the build has been sucessful it will begin a Release pipeline to get this drop into my web site.  My goal is to get this wrapped up into an MSDeploy package that can then be deployed directly onto my Azure web site.  I am still working on that and will provide a more detailed post on what I needed to do to get that to happen.  In the meantime, I need to make sure that my Test virtual machine is up and running in Azure as one of the first things that this Release Manager pipeline will do is to copy the contents of the drop onto this machine.  Then it calls a CodedUI test which really is not testing it will run my PowerShell script that will FTP the pages to my Azure web site.  It needs to do this as a user and the easiest way without me having to do this manually is to run the CUI to do it and complete it.

## Summary

So there you have it, I have my blog in source control so I have no dependancy of a database and all the code to generate the web site and my content pages are in source control which makes it really easy if I ever need to make a move to a different site or location or anything like rebuild from a really bad crash.  As an ALM guy I really like this approach and what would be even better was having a new pre-production staging site to go over the site and give it a last and final approval before it goes live to the public site.