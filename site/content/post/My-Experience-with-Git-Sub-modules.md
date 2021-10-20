---
title: My Experience with Git Sub-modules
date: 2016-09-12 20:43:37
tags: ["git", "Blogs"]
categories: ["technology"]
archives: 2016
menu: "main"
author: "Donald L. Schulz"
---
{{<figure class="left" src="/images/Lumina-950XL.png" width="200" alt="Lumina 950 XL" >}}
I just replaced my phone with a new Microsoft Lumina 950 XL which is a great phone.  In my usual fashion of checking out the new features of my phone I wanted to see how my web sites looked.

The operating system of this phone is the Mobil version of Windows 10 and of course is using the new browser called Edge.  Well it seems that my blog did not look good at all on this new platform and was in fact not even close to being workable.  Even though I had the fonts set to the smallest setting, what was displayed were hugh letters so hardly any words fit on a line and was just crazy looking.  However, I noticed that other web sites looked just fine especially the ones that I recognized and truely being built around the bootstrapper framework.  

I was also surprised as to how many other web sites look badly in this browser with the same problems that I had.  Anyway I may address some of that in a later post but right now, what I wanted to find out is if I changed the syle of this blog would it solve my problem.  If I just changed the theme or something could it be possible that my site would look great again.  This was all very surprising to me as I had tested the responsiveness of this site and it always looked good, just don't know why my new phone made it look so bad.

## New Theme, based on Bootstrapper
Looking for different themes for Hexo was not a problem, there are many of them and most of them are even free.  I am really loving the work that I have done working with the Bootstrapper Framework so when I found a Hexo theme that was built around the Bootsrapper Framework, you know I just had to try it.  Well this theme looked great a lot simpler looking theme than what I was using which was really the default theme with a few customizations.  The new theme was also open source and in another git hub repository.  The instructions said to use some sub-module mumbo jumbo to pull the source into the build.  Well now I was curious as there was something that I saw on the build definition when working with git repositories, a simple check box that says include sub-modules.  Looks like it is time to find out was git sub-modules is all about.

Welcome to another git light bulb moment.
{{<figure class="right" src="/images/git-logo.jpg" width="175" alt="Git" >}}
## What is a git sub module.
The concept of a git sub module is a whole new concept for me as a developer that has been using for the most part, a centralized version control system of one sort or another for most of my career.  I then looked up the help files for these git sub modules and read a few blog posts, and it can get quite complicated but rather then going through all that it can do let me explain how this worked for me to quickly update the theme for my blog.  In short, a git sub module is another git repository that may be used to prove source for certain parts for yet another git repository without being a part of that repository.  
In other words, instead of having to add all that source from this other git repository and adding it to my existing Blog git respoitory it instead has a reference to that repository and will pull down that code so that I can use it during my build both locally and on the build machine.  And the crazy thing is it makes it really easy for me to keep up with the latest changes because I don't have to manage that it is pulling the latest from this other repository through this sub module.

I started from my local git repository and because I wanted this library in my themes folder I navigated to that folder as this is where hexo is going to expect to see themes.  Then using git-posh (PowerShell module for working with git)  I entered the following command.
```
git submodule add https://github.com/cgmartin/hexo-theme-bootstrap-blog.git
```
This created the folder hexo-theme-bootstrap-blog and downloaded all the git repository into my local workspace and added a file called .gitmodules at the root of my Blog bit repository.  Looking
inside the file, it contains the following contents:
```
[submodule "themes/bootstrap-blog"]
	path = themes/bootstrap-blog
	url = https://github.com/cgmartin/hexo-theme-bootstrap-blog.git
```
When I added these changes to my staging area by using the add command:

```
git add .
```

It only added the .gitmodules file and of course the push only added that file as well to my remote git repository in TFS.  Looking at the code of this Blog repository in TFS there is no evidence that this theme has been added to the repository, because it has not.  Instead there is this file that tells the build machine and any other local git repositories where to find this theme and to get it.  The only thing left was to change my _config.yml file to tell it to use the bootstrap-blog theme and run my builds.  Everything works like a charm. 

I really don't think that there is any way that you can do something like this using centralized version control.  Humm, makes me wonder, where else can I use git sub modules?