# [Starting a New Workspace](/README.md)

These are the steps that you would need to follow if you are cloning the Blog repository to a new location for the first time and you want to use the full power of Hexo to write, create and view the web site before we have committed any code which results in an automated build.  These are the parts that are not in the repository but are required to build and basically run Hexo in this new environment or location.

## Make sure you have NPM installed.
The very first prerequisit that is needed is of course git (can't clone the repository without it) and Node.js (Should be at least Node.js 8.10, but recommends 10.0 or higher).
The advantage of using nvs is that you can select and switch between different versions of node.
```
Choco install nvs
nvs add lts
nvs use lts
```
## Install Hexo
Once the requirements are installed, you can install Hexo with npm:
```
npm install hexo-cli -g
```
## Update the Repository with Hexo packages...
Now you want to make sure you are in the Blog repository at the same level as the .git repository.
```
npm install hexo --save
```
After that last step you should be in business, but remember to run the Hexo command inside the src folder as that is the normal start of a hexo project structure that Hexo is going to be expecting.
