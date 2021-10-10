---
title: Maintaining the Golden Images
date: 2020-10-31 13:17:51
tags: ["DevOps", "Docker", "Jenkins"]
categories: ["technology"]
menu: "main"
author: "Donald L. Schulz"
archives: 2020
---
!["Golden Image"](/images/goldenImage.jpg) 
Today I wanted to take you down an unusual path as I disclose some of the docker structure that I have configured recently in my environment.  It has been my goal for a while now to run everything that I can in a docker container.  What I mean by everything is that it is more than just my web sites and applications that I have been working as side projects for years but also includes some of the open source support tools that I use.  This blog post is going to mostly cover these tools and here is a list of just a few of them to give you an idea of what I am talking about.
1. Jenkins Server
1. Portainer
1. Seq Server
1. SonarQube
1. Adminer
1. letsencrypt
1. nginx

Okay, this sounds okay and not that big of a deal as many of these open source projects are released as a docker image that you can run in a docker container.  The nice thing about a docker image is that it will always work, no hunting for missing dependency or solving a conflict with another program because the image has everything it needs to run properly.  Now here is the rub, I am running the Jenkins Server in a Docker container creating a docker image of the build that I am building in my CI/CD environment.  This almost sounds crazy almost as weird as the double hop I used to do when logging into one virtual machine to remote into a second virtual machine.  Feels a little weird but it works although you need to pay close attention to which environment you are actually in.

Jenkins Needs Docker Installed
------------------------------
!["Jenkins"](/images/Jenkins.png)
First off on my Jenkins image I needed a few things to be part of the image so we need to add some layers to the already available Jenkins Image.  The best way to handle this is to create a new repository for this project.  Not like it is going to contain any C# code but it will contain my Dockerfile and Docker-Compose files to put these pieces together.  New software to be installed should be part of the Dockerfile, this way I even have a full history of what was in this image and say that there is a newer version of Jenkins I want to update to I just update the base image in the Dockerfile and I get all the pieces that I have installed are part of that image as well.  I am getting a little ahead of myself so lets look at that Dockerfile.

Dockerfile
----------
!["Docker"](/images/docker.png) 
The Dockerfile is the key to building a new image and adding additional layers to existing images.  Just before I share and go over the details of what I have in this Dockerfile I want to go over the structure of this project as it sits in my GitHub repository.  This is going to be the base image for my Jenkins Server so I have called my repository Jenkins-Base and in this repository I have three folders:
1. .github/workflows
1. build
1. src

Because this is a project to build my Jenkins-Base I have the Dockerfile in the src folder as this really is the source code for building this project.  Inside the build folder I have the Jenkinsfile and a ci folder that contains a number of shell scripts.  Inside the .github/workflows folder is a single yaml file that I call main.yml and is a github action script that gets the version number and passes it on to Jenkins through the Jenkins API.  That is the basic structure of the project as I try to keep this as close to my standard structure that I maintain for all my repositories.

```
FROM jenkins/jenkins:lts
COPY ./src/executors.groovy /usr/share/jenkins/ref/init.groovy.d/executors.groovy
RUN /usr/local/bin/install-plugins.sh docker-plugin
USER root
RUN apt-get --assume-yes update
RUN apt-get --assume-yes install \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg-agent \
    software-properties-common
RUN apt-get install -y \
    gcc \
    libc-dev \
    libffi-dev \
    make \
    openssl \
    python3 \
    python3-pip
RUN echo "alias python=phthon3 >> ~/.bash_aliases"
#RUN source ~/.bash_aliases
RUN echo "alias pip=pip3 >> ~/.bash_aliases"
#RUN source ~/.bash_aliases

RUN curl -fsSL https://download.docker.com/linux/debian/gpg | apt-key add -
RUN apt-key fingerprint 0EBFCD88
RUN add-apt-repository \
    "deb [arch=amd64] https://download.docker.com/linux/debian \
    $(lsb_release -cs) \
    stable"
RUN apt-get --assume-yes update
RUN apt-get --assume-yes install docker-ce docker-ce-cli containerd.io
RUN pip3 install docker-compose
RUN apt-key adv --keyserver keyserver.ubuntu.com --recv-key C99B11DEB97541F0
RUN apt-add-repository https://cli.github.com/packages
RUN apt update
RUN apt install gh
```
This docker file starts out by getting the official latest stable and official version of Jenkins.  The lts tag indicates to us that we don't just want the latest version of Jenkins but the version that is being supported Long Term.  In the next line I am copying a groovy script into the Jenkins image at **/usr/share/jenkins/ref/init.groovy.d/executors.groovy** location.  This is a very simple groovy file that sets up the Jenkins Server to have 5 agent instances to do the builds.  There are just two lines to this script and the script is called "executors.groovy"  If you wish to skip this that is fine too, just delete that line from the Dockerfile.  If you do create this file make sure to store it in the src folder just like the Dockerfile.
```
import jenkins.model.*
Jenkins.instance.setNumExecutors(5)
```
As you go through the rest of the Dockerfile you see that I am updating the software on this image as well as installing a number of utility software components.  Then about half way down the script you should see a number of lines that involve the install of docker inside this Jenkins docker image and installing docker-compose which is a python package and gets installed using the pip3 command.  Finally near the end of this Dockerfile we install the GitHub-Cli package.  This allows me to run some github commands within the Jenkins pipeline.  See [Take GitHub to the Command Line](https://cli.github.com/) for more information on this tool.
That more or less wraps things up for the source of this image lets now look at the build steps.

Jenkinfile
----------
!["Jenkins"](/images/Jenkins.png)
The Jenkinfile is the only file directly inside the build folder.  Inside the build folder I do have another folder that I have named **ci** which contains a number of shell scripts that are called from the Jenkinsfile which is the pipeline for the operation.  We will get to the contents of the ci folder in a minute, right now here is the contents of my Jenkinfile.
```
pipeline {
  
  agent any
  
  parameters {
    string( name: 'semver', defaultValue: '2.235.4',
            description: 'the resulting semver version number after running gitversion on the source at GitHub')
    string( name: 'branchname', defaultValue: 'master',
            description: 'the actual branch name that triggered the build as precurred from gitVersion')
  }
  
  environment {
    BRANCH_NAME="${params.branchname}"
  }

  stages {

    stage('Initialization'){
      steps {
        buildName "${params.semver}"
        buildDescription "${params.branchname}"
        sh 'chmod +x ./build/ci/00-verify.sh'
        sh './build/ci/00-verify.sh'        
      }
    }

    stage('Build'){
      steps {
        sh 'chmod +x ./build/ci/01-build.sh'
        sh './build/ci/01-build.sh'
      }
    }

    stage('Test'){
      steps {
        sh 'chmod +x ./build/ci/02-test.sh'
        sh "./build/ci/02-test.sh ${params.semver}"
      }
    }

    stage('Publish-Topic'){
      steps {
        sh 'chmod +x ./build/ci/03-publish-topic.sh'
        sh "./build/ci/03-publish-topic.sh ${params.semver}"
      }
    }

    stage('Publish-Master'){
      when {
        environment name: 'BRANCH_NAME', value: 'master'
      }
      steps {
        sh 'chmod +x ./build/ci/03-publish-master.sh'
        sh "./build/ci/03-publish-master.sh ${params.semver}"
      }
    }

    stage('Operations') {
      when {
        environment name: 'BRANCH_NAME', value: 'master'
      }
      steps {
        build job: 'Operations', parameters: [ string(name: 'product', value: "Jenkins:v${semver}") ]
      }
    }

  }
}
```
Jenkins declarative pipelines are pretty easy to follow and I have covered this in an [earlier post](/2020/07/Pipeline-As-Code/).  So let's have a quick look to see what is going on here.  As I mentioned I covered the basics of the Jenkins declarative pipeline in an earlier post but I do want to go over about four (4) of the steps which are a bit different that a typical build and deploy pipeline.

Build
-----
As you can see from the above pipeline that in the Build stage I am calling a shell script called: **01-build.sh**  This is really a shell script that executes Docker and passes it the Dockerfile that is sitting in the src folder.  In this command line right after the -t flag (which is for taggin the image) I have account separated by less than and greater than symbols.  I use the account name that I have established on docker.  This should be some sort of indicator that keeps you unique.
```
docker image build --no-cache --rm -f ./src/Dockerfile -t <account>/jenkins-master .
```
This is pretty simple and all that is in the 01-build.sh file, which basically tells docker that we are building an image and not to use any of the cache items to build this image.  The reason I have chosen this option is that I found that when I was building this multiple time and some of the steps were not quite right to what I wanted them to be, Docker did not see them as being invalid builds so instead of starting from the base it would start from the cache that it already had resulting in having pieces in the image that I did not want.

Publish-Topic
-------------
The next piece that we are going to talk about is what happens in the Publish-Topic.  Publish-Topic refers to any branch that is not master.  In my typical development workflow, before I start working on any thing I create a branch from master and this branch would be referred to as a topic branch.  In an [earlier post](/2020/09/My-Take-on-Software-Versioning) I talk a little bit about this as a way that I also manage my version numbers.  Speaking of version numbers, this is a shell script were we pass in the semver version number that was passed in to start this pipeline.
```
cat /var/jenkins_home/my_password.txt | docker login --username <account> --password-stdin
docker tag $(docker images | awk '{print $3}' | awk 'NR==2') <account>/jenkins-master:$1
docker push <account>/jenkins-master:$1
```
1. In this step we are taking this docker image that we just finished building and pushing this up to a docker repository.  In my case I am publishing my docker image to hub.docker.com which is the default so only needs the name of the account and a password for that docker account.  If you wanted to publish you docker images to your github repository you would need to add something like **docker.pkg.github.com** before you pass in the username and password.
2. In the second line we are going to give this new image a tag with the version number that was passed in to start this Jenkins pipeline.  We tag this image with the docker tag command.  This piece in the middle finds the actual image by starting with the docker images which returns information about all the images it finds in this instance.  The first awk command returns only the 3rd column which is just a list of image ids and not all the other stuff.  The second awk pulls out the latest one and that becomes the image that we tag with our **account-name/image-name:passed-in-semver**.
3. The final line in this shell script is pretty clear it pushes the new tagged image up to your docker repository that you logged into in the first step.

*Note: This command is run no matter what branch you are working with.  Which is different than the Publish-Master shell script.*

Publish-Master
--------------
As you can see in the Jenkins pipeline above we have a condition on this stage.  This stage will only run when the branch is master.  In case you are not familiar with my workflow, work is done on the topic branch which is away from the master branch.  This way I can build and test and do all matter of build and destruction until I get something that I am satisfied with.  Then I do a pull request which in github is going to do a temporary build to test out these changes before it even allows the merges to be approved to go back into the master branch.  Then because the master branch just got updated will kick off what could be the final build for this version and send it through the pipeline.  Here is the content of that shell script.
```
cat /var/jenkins_home/my_password.txt | docker login --username <account> --password-stdin
docker tag <account>/jenkins-master:$1 <account>/jenkins-master:latest
docker push <account>/jenkins-master:latest
```
As you can see this shell script is very similar to the Publish-Topic shell script.  The only difference is that we are re-tagging this tagged image to latest.  This way, your operations docker-compose.yml file would use the latest tag to update the Jenkins image that is complete and not in the middle of development.

Operations
----------
!["Operations"](/images/Operations.jpg)

This last and final stage of the pipeline is the most important part of this process.  As you probably figured out, there is no way that I can call the docker-compose.yml file for my operations infrastructure which is what my Jenkins server is a part of.  I need this to be called from outside this current operation.

What I have done here is created a separate Jenkins pipeline in the Operations repository which is where the docker-compose.yml file lives for all the infrastructure that I am running.  Remember the list of servers that I showed you at the beginning of this post. I will show you the Jenkinsfile for this Operations job shortly, but all that this job does is update a file in Operations and commits that update in the GitHub repository which triggers its own build but this time won't be calling Jenkins.  This Continuous Integration  trigger is fired off by GitHub using an action script, because we need to keep Jenkins out of this step as the whole point here is to replace that image which would be near impossible if we are working from inside the container that needs to be replaced.  First up let's look at that Jenkinsfile for the Operations job.
```
pipeline {
    agent any
    parameters {
        string( name: 'product', defaultValue: 'Jenkins:v2.235.4',
            description: 'the resulting product and version number being updated')
    }
    stages {
        stage("Initialization"){
            steps {
                sh 'docker version'
                sh 'docker-compose version'
            }            
        }
        stage("New Release"){
            steps {
                withCredentials([sshUserPrivateKey(credentialsId: 'jenkins_root_github', keyFileVariable: 'keyfile', usernameVariable: 'username', passphraseVariable: 'passphrase')]) {
                    sh "git checkout master"
                    sh "git config --global user.name 'Jenkins Builder'"
                    sh "git config --global user.email 'jenkins@example.com'"
                    sh "echo `date` ----- ${product} >> Release.md"
                    sh "echo \n >> Release.md"
                    sh "git add Release.md"
                    sh "git commit -m 'Update the Release file with ${product}'"
                    sh "git rev-parse --abbrev-ref HEAD"
                }
                withCredentials([usernamePassword(credentialsId: 'CredentialName', passwordVariable: 'GIT_PASSWORD', usernameVariable: 'GIT_USERNAME')]) {
                    sh('git push https://${GIT_USERNAME}:${GIT_PASSWORD}@github.com/t3winc/operations.git HEAD:master')
                }                
            }
        }
    }
}
```
In this pipeline there is no shell script file, all the shell script commands are written in-line.  The stage of course where things are really happening is in the "New Release" stage.  Here I have a couple of withCredentials blocks and in the first block I am using ssh to check out the master branch, set the configuration for the user name and email.  Then we send the date and product name that was passed into this job and append it to the end of a Release.md file.  Finally we add and commit the change to the local git in the Jenkins build machine.  The second WithCredentials block uses a username password to push this commit to the repository on GitHub.  This push then fires off the ci trigger to start its own build process, but it does not call Jenkins to fulfill this request, this next step is fired off inside of GitHub and carried out by its own GitHub action script.  Now lets take a look at what is in there.

In case you have never heard of GitHub action scripts before here is a [link for a free course on this feature](https://lab.github.com/githubtraining/github-actions:-hello-world), [GitHub Actions feature page](https://github.com/features/actions) and [GitHub Actions Documentation](https://docs.github.com/en/free-pro-team@latest/actions). When you create a new action script by default GitHub will place this script in the .github/workflows directory and the default name for this file unless you change it is **main.yml**
```
# This is a basic workflow to help you get started with Actions
name: CI

# Controls when the action will run. Triggers the workflow on push or pull request
on: [push, pull_request]

# A workflow run is made up of one or more jobs that can run sequentially or in parallel
jobs:
  # This workflow contains a single job called "build"
  build:
    # The type of runner that the job will run on
    runs-on: ubuntu-latest

    # Steps represent a sequence of tasks that will be executed as part of the job
    steps:
      # Checks-out your repository under $GITHUB_WORKSPACE, so your job can access it
      - uses: actions/checkout@v2
      - name: Fetch all history for all tags and branches
        run: |
          git config remote.origin.url https://x-access-token:${{ secrets.GITHUB_TOKEN }}@github.com/${{ github.repository }}
          git fetch --prune --unshallow
          
      - name: copy file via ssh key
        uses: appleboy/scp-action@master
        with:
          host: ${{ secrets.HOST }}
          username: ${{ secrets.USERNAME }}
          port: ${{ secrets.PORT }}
          key: ${{ secrets.KEY }}
          source: "src/docker-compose.yml"
          target: "~/"

      - name: executing remote ssh commands using ssh key
        uses: appleboy/ssh-action@master
        with:
          host: ${{ secrets.HOST }}
          username: ${{ secrets.USERNAME }}
          key:  ${{ secrets.KEY }}
          port: ${{ secrets.PORT }}
          script: |
            whoami
            ls -al
            docker-compose -f src/docker-compose.yml up -d --remove-orphans
```
The first part of this script we give it a name and set the condition for the trigger.  Here we want this to run on every push and pull_request.  Next we setup a single job called build and incase you were wondering where github was running these jobs, they are docker containers and here I have told it to run in the latest version of ubuntu.
Largely these commands that you see in this file are plugins that are downloaded from the [GitHub marketplace](https://github.com/marketplace?type=actions) 
In my first action I am checking out the source control so that I can get at the docker-compose.yml file.  In the second action I am going to copy that docker-compose.yml file over to my docker host which is another Linux virtual machine running in the cloud.  All the secrets are stored in the GitHub secrets area for this repository which you get to from the Settings menu item of the repository and click on Secrets.  The host is going to be the ip address or fully qualified domain name to the server that you are running your operations infrastructure on.  The final action is to run the docker-compose command to get all the containers up and make any updates along the way.