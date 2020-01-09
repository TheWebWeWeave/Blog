# [Steps](/README.md)
## Jan 8, 2020 - Release Pipeline
I am not sure when this changed but it has been for quite some time that we have not used ftp to deploy these artifacts to the production or any other servers.  There have been several changes over the years since 2016 and I will try to quickly summarize the changes over the next couple of paragraphs.

1. The first change was getting away from doing an ftp deployment by copying the publish folder to an IIS server and then create an msdeploy package.  We then used an MSDeploy task in the Release Pipeline to deploy the code to both Dev and QA environments as well as Production.
1. The **IIS Web App Deploy** was updated and could do a web deploy using a plain old zip file of the contents of the publish folder.  This is part of the continued support of dotnet core as that is how it is deployed.  This allowed us to get rid of the silly steps we needed to take to make an msdeploy package for distrubution.
## Feb 4, 2016 - Change in the Deployment
Up until now we have had to make sure that ALM-Test was up so that we could copy files over to it so that we could then call a coded UI test that would interact with the powershell that it would call to do the FTP to the actual web site.
Today we experimented with a new FTP task which will ftp the location of the drop into the web site so we can do this without having any virtual machines running at all.  This is great but we will still need to use this kind of concept for
some of our other projects like AGP which needs to make a package unique to each environment first and that is not part of the drop or in a share any where.