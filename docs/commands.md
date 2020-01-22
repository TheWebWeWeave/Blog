# [Commands](/README.md)

These commands will assume that you are in the src directory of this Blog Repository

1. Hexo new "Title of the new blog" (you will find the new skeleton under Source\_posts
2. Hexo Server (this starts hexo up so that you can preview the pages locally at http://localhost:4000
3. Hexo Publish (this moves the draft blogs into publish mode and will get its pages visible to the world next time we deploy)
4. Hexo Generate

## Image Macros
1. Image displays on the right hand side of a post.  Assumption: you have the image stored in the images folder at the same level as the _posts and in this example we are diplaying the Hexo logo. 
```
{% img right /images/hexo.jpg 100 100 "Hexo Logo" %}
```
2. Image display on the left hand side of a post.  Assumption: you have the image stored in the images folder at the same level as the _posts and in this example we are displaying the Hexo logo.
```
{% img left /images/hexo.jpg 100 100 "Hexo Logo" %}
```
3. Image stored in a folder that matches the name of the post.  Much cleaner way to store and maintain the images for each post but are limited to what you can do with the images like having them appear on the left or right side of the post.  This option only has one and it will be posted in its own space across the width of the post.
```
{% asset_img image.jpg "Time I spent away from my family in September" %}
```

## Implement Tags
I never seem to remember how to implement tags in my blogs so the following example should refresh my memory.  This goes after the tags: and each line starts with a "-"
```
tags:
- ALM
- DevOps
- NuGet
- PowerShell
---
```
## Tag List
* 3WInc
* ALM
* Blogs
* Compare
* Corporation
* Database
* DevOps
* Goal Tracker
* Health
* Lifestyle
* NuGet
* Politics
* PowerShell
* Products
* SQL
* Security
* Soap Box
* TFS
* Testing
* Time Tracker
* User Experience (UX)
* Vegan
* c#
* dotNet
* dotNet Core 
* git
* git-tf 
* vb