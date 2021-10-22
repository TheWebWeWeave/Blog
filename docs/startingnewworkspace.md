# [Starting a New Workspace](/README.md)

These are the steps that you would need to follow if you are cloning the Blog repository to a new location for the first time and you want to use the full power of Hugo to write, create and view the web site before we have committed any code which results in an automated build.  These are the parts that are not in the repository but are required to build and basically run Hugo in this new environment or location.

## Make sure you have Chocolatey installed.
The very first prerequisit that is needed is of course git (can't clone the repository without it) and Go.  The Go language is not necessary to run Go, but if you want to do things like turning templates into Go modules then you would need this.
Choco install golang -y
```
## Install Hugo
Once the requirements are installed, you can install Hugo with chocolatey:
```
choco install hugo -confirm
choco install hugo-extended  -confirm
```

After that last step you should be in business, but remember to run the Hugo command inside the site folder as that is the normal start of a hugo project structure that Hugo is going to be expecting.
