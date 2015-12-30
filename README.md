# Blog Powered by Hexo
Currently this is the same content that we have always had out there but it uses simple markdown which gets chured through Hexo and generates static web pages which are perfect and wonderful and the best part 
is that the articles and blogs are exactly where I have always wanted them which is in source cotrol.

# Hexo commands
These commands will assume that you are in the Blog directory

1. Hexo new "Title of the new blog" (you will find the new skeleton under Source\_posts
2. Hexo Server (this starts hexo up so that you can preview the pages locally at http://localhost:4000
3. Hexo Publish (this moves the draft blogs into publish mode and will get its pages visible to the world next time we deploy)
4. Hexo Generate

# Blog Workflow
1. We will start by getting a new page to work on by using the Hexo new command
2. We open to the Blog folder using Visual Studio Code
3. Find the new .md file and start editing, any images can be dropped into the folder of this same article name.  The nice thing about using Visual Studio Code is that you can get a preview of the .md pages.
4. Save the pages and go to the git section and check in the pages.
5. When you sync up with the remote git repository on TFS, this will cause the pages to be generated and packaged up
6. The build will trigger the deployment so make sure that ALM-Test is up and running as it uses that for staging.
7. Enjoy the new blog at http://www.donaldonsoftware.com