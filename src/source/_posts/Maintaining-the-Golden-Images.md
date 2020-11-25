---
title: Maintaining the Golden Images
date: 2020-10-31 13:17:51
tags:
- DevOps
---
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
First off on my Jenkins image I needed a few things to be part of the image so we need to add some layers to the already available Jenkins Image.  The best way to handle this is to create a new repository for this project.  Not like it is going to contain any C# code but it will contain my Dockerfile and Docker-Compose files to put these pieces together.  New software to be installed should be part of the Dockerfile, this way I even have a full history of what was in this image and say that there is a newer version of Jenkins I want to update to I just update the base image in the Dockerfile and I get all the pieces that I have installed are part of that image as well.  I am getting a little ahead of myself so lets look at that Dockerfile.

Dockerfile
----------
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
Jenkins declaritive pipelines are pretty easy to follow and I have covered this in an [earlier post](/2020/07/Pipeline-As-Code/).