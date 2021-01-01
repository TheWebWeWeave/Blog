# [Workflow](/README.md)

1. We will start by getting a new page to work on by using the Hexo new command
2. We open to the Blog folder using Visual Studio Code
3. Find the new .md file and start editing, any images can be dropped into the folder of this same article name.  The nice thing about using Visual Studio Code is that you can get a preview of the .md pages.
4. Save the pages and go to the git section and check in the pages.
5. When you sync up with the remote git repository on TFS, this will cause the pages to be generated and packaged up
6. The build will trigger the deployment so make sure that ALM-Test is up and running as it uses that for staging.
7. Enjoy the new blog at http://www.donaldonsoftware.com

# Pull Request
Since we moved to GitHub the way that pull request works is way different then in Azure DevOps.  Hopefully, they will eventually get this to work as smoothly as it does in Azure DevOps but in the meantime I have a couple of steps that I do that makes this work better.
```
gh pr create --title "My Roadmap for 2021 is completed" --body "+semver: feature"
```
1. That line is called from powershell in the root of the git repo on my workstation.
1. Next we go to https://github.com/TheWebWeWeave/Blog and you should see that you have a Pull Request waiting.  You click on this and click on the Squash and Merge option to complete the Pull Request and it should delete the topic branch when all has been completed.
1. Now we go to our blog repo on our local laptop and pull from the remote github.com
1. Next run the following cleanup code.
```
git remote update --prune
```
5. That line will remove the remote branch from our local cache and now we can delete that branch locally.
```
git branch -D <name of the branch>
```
