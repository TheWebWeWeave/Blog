---
title: Posting a Release on an Asana Calendar
date: 2021-03-21 14:31:44
tags: [
    "DevOps",
    "Jenkins",
    "Dashboard",
    "Python"]
categories: ["technology"]
archives: 2021
menu: "main"
author: "Donald L. Schulz"
next: "Getting Badges to Update on GitHub Pages"
prev: "Testing a Docker Image as part of a Jenkins Pipeline"
---
One thing that I like to do is to have a Calendar that shows all the versions that I have released to Production.  I don't have a whole lot of programs that I maintain but there is enough of a variety that I do switch around to the various bug fixes and new features and it would be really cool to see them all on a Single Calendar.  This way I can be sure that features and fixes are getting out there and my stakeholders can also be aware of this information.

I have chosen to use [Asana Calendar](https://asana.com) because they have a good [API](https://en.wikipedia.org/wiki/API) that I can use to make this happen right within the final step of my CI/CD pipeline.  When I release a version of my product into Production and it was successful as a final step I call a Python script and an entry is posted on the Calendar.  I don't have to think about it again it is just part of the normal CI/CD pipeline but the Calendar is always up to date for who ever wants to know this information.

## Start with an Asana Account
Asana is a whole project management suite but for this post I am only interested in the Calendar portion of the product.  You can start with the free version of the product which allows collaboration with up to 15 team members.  There are reasonably priced plans if you need more than that.  My wife and I started using Asana a couple of years ago to maintain a common family Calendar.
1. Open up the Asana site [https://asana.com](https://asana.com) and click on the Try for Free button.
2. Enter you email address in the text box provided and press the Try for Free button
3. Make sure that you use a real email address as the next step you will need to verify before you can go on.
4. In your email you should get a message like this so click on the Verify email address button to continue.
5. This will take you back to the Asana web site where you enter your full name and give it a password and press Continue.
6. In this next screen they are collecting some demographics to better understand what kind of industries are using their product. I selected IT, but choose what best describes your line of business and click on the Continue button.
7. Next they want to know what you main objective in Asana, choose what you like, you can even select I'm not sure yet, and then click on Continue.
8. Now we get to the place of setting up your first Project and they want you to give it a name.  This is where I choose to put the name "Release" as this Project will hold my Release Calendar.  Click on the Continue button.
9. Asana tries to be helpful by providing a few sample tasks that you might do.  Just leave these for now and click on the Continue button again.
10. Next it is asking about how you would like to group these tasks, just accept the default and click on the Continue button.
11. Now we get to make a decision on the layout to use.  Select the Calendar and then click on the Continue button.
12. In this last and final stage it is asking who all is working on this project with you.  You can add additional emails if you wish and when you are ready click on the "Take me to my project" button.
13. You will get a message about your 30-day trial and once that ends it will continue with a free Basic plan.  Click on the "Let's get started" button
14. In trying to be helpful we have 3 tasks that we don't need on our calendar.  Right click each of them and choose delete so you end up with a clean calendar.

We will get back to our calendar in a few minutes as we will need a project number and an personal access code but first lets look at the Python script that I wrote to populate this calendar with our releases into Production.

## The Python Script
It just so happens that Asana has a Python library to help with calling the API.  This really helps in simplifying the work we need to do to get this to work.  There are really only three (3) things that we need to pass into script:
1. Product name with the version number
2. The date formatted in YYYY-MM-DD such as **2021-03-21**
3. The project number of the calendar that we want to post this to.

Before we get into where you might find some of these pieces that might not be quite so obvious like the project number lets look at the python code that will make this happen.
```
#!/usr/bin/python

import asana
import sys
from datetime import datetime


def main():
    product = sys.argv[1]
    version = sys.argv[2]
    project = sys.argv[3]

    # get the personal access token
    filename = "/var/jenkins_home/asana_api.txt"
    with open(filename) as f:
        api = f.readlines()
        secret = api[0].strip('\n')
        print(secret)

    # get today's date in the Asana format
    now = datetime.now()
    pattern = "%Y-%m-%d"
    time1 = now.strftime(pattern)

    # put the calendar information together as simple json
    data = {"name": product + " v" + version, "due_on": time1, "projects": project}
    
    # the calls to asana api    
    client = asana.Client.access_token(secret)
    result = client.tasks.create_task(data)

if __name__=="__main__":
    main()
```
I haven't talked too much about Python so far in my posts but it is an incredible language and an absolute must if you are working on any Linux systems which I find I am doing more and more of that every day.  Python is a language that is installed on almost every Linux distro out there you can almost always just count on it being there.  It is also available for Apple (which is another Linux distro) and Windows.  To install Python go to [https://www.python.org/downloads/](https://www.python.org/downloads/)  Download and install the appropriate version for your operating system.

Python has its own package management system that it uses for installing libraries and dependencies called pip.  You may have to use pip3 as both Python 2 and Python 3 can exist on the same system and in recent distros of Python they require the major version number.  In my Linux environments I setup alias so that Python and Pip would always point to my version 3.  After you have installed Python you need to install the Asana library with this command.
```
pip3 install asana
```
There are a number of ways that you could manage the python script, here are a few:
1. Make it part of the solution and put it into source control of the application that you are building.
2. You could put it on a network shared drive where you build agent would be able to point and have access to it.
3. You could make this part of the Jenkins image so when you call it just give the path were it can be found.  For more information on making this part of your docker base image see my previous post [Maintaining the Golden Images](/2020/10/Maintaining-the-Golden-Images/)
4. You could make this python script part of it's own repository and then use [git-subrepo](https://github.com/git-commands/git-subrepo#readme) to pull the script into your project repo when the agent is about to start the process of building and deploying the application.

There are a lot of different ways that you can handle this and all are good choices except for #1 as this would involve coping the python script to all the projects that you release to production and if you ever needed to make a change would be a maintenance nightmare.  Also be careful with #2 if this is a closed network and your Jenkins server is part of this network, this might be okay otherwise there are security concerns.  The other two pretty much assume that this is in a location that you control.

Just before we go over to the Jenkinsfile to put all the pieces together lets step back to our Asana account and grab a Project Id and create an API key as we will need both.  The Project Id tells the anana api where we want to post this information and the API key is needed to access your calendar.

## Back to your Asana Account
When you go to the calendar that you want to post these entries on, take a look at the url.  Just before the word calendar there is a number between the "/"  That number is the Project Id.  Copy this number so we can apply it in our Jenkins pipeline.

Next we need to create an API key.  This is not all that intuitive to get to but here are the steps you need to follow.
1. Over on the right hand side of the Asana page click on your profile photo, it is on the **topbar**
2. Select My **Profile Settings...**
3. Open the **Apps** tab
4. Click **Manage Developer Apps**
5. Click + **New Access Token**

Now the way that I used this new token that was created was to copy this into a text file that I called asana_api.txt and I placed it in the jenkins_home directory.  I am using a docker image and the operating system is linux so although you can see in the script above that it is mapped to 
/var/jenkins_home/asana_api.txt  In reality I have a volume on my docker host mapped to this location.  I drop the file into that location and Jenkins will see that file in its instance of /var/jenkins_home.
## The Jenkinsfile
We are getting near the end of our journey here as it is time to plug this into our Jenkins pipeline so we can see those entries appear on our Asana Release Calendar.  I am basically using the same pipeline as I used in my previous post [Pipeline As Code](/2020/07/Pipeline-As-Code/).  Check out that post if you haven't already and the piece that I am going to show you here is the very small modification that I implemented to get this work.
```
        stage('Release'){
            agent {
                label 'AWS-Jenkins-Slave'
            }
            when {
                environment name: 'BRANCH_NAME', value: 'master'
            }
            steps {
                sh 'chmod +x ./build/ci/05-release.sh'
                sh "./build/ci/05-release.sh ${params.semver}"                
            }
            post {
                success {
                    sh "python3 /var/jenkins_home/workspace/Affirm_Store/build/scripts/src/calendar-api.py AffirmStore ${params.semver} Asana- Project-Id"
                }
            }
        }
```
That is it, when we run the pipeline and the current branch that we are building is master then we deploy this to our production environment.  If that was successful then we run our python script that makes the entry onto our Release Calendar.

Good luck with your Release Calendar and if you have any questions or need further clarification, hit me up on the Discussion/Comment section below.