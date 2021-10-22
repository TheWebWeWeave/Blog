# [Commands](/README.md)

These commands will assume that you are in the site directory of this Blog Repository

1. Hugo new post/"Title of the new blog" (you will find the new skeleton under archetypes folder)
2. Hugo Server (this starts hugo up so that you can preview the pages locally at http://localhost:1313
4. Hugo (this generates the static site into the public folder)

## Image Shortcodes
1. Image displays on the right hand side of a post.  Assumption: you have the image stored in the images folder under the static folder. NOTE: All images are stored in this one location. 
```
{{<figure class="right" src="/images/hexo.jpg" width="100" alt="Hexo Logo">}}
```
1. Image displays on the left hand side of a post.  Assumption: you have the image stored in the images folder under the static folder. NOTE: All images are stored in this one location.   
```
{{<figure class="left" src="/images/hexo.jpg" width="100" alt="Hexo Logo">}}
```
3. Image just displays in the center of the post the full size of the image with a max to the amount of space allowed and available for the post.  Images will never be overflowing or clipped as they will be automatically sized to fit.
```
{{<figure src="/images/image.jpg" alt="Time I spent away from my family in September">}}
```

## Implement Tags
I never seem to remember how to implement tags in my blogs so the following example should refresh my memory.  This goes after the tags: and each tag goes in the square brackets (array) and each item is surrounded by quotes and separated by commas.
```
tags: ["ALM", "DevOps", "NuGet", "PowerShell"]
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