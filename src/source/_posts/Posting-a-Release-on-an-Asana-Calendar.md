---
title: Posting a Release on an Asana Calendar
date: 2021-03-21 14:31:44
tags:
- DevOps
- Jenkins
- Dashboards
- Python
---
One thing that I would like to do is to have a Calendar that shows all the versions that I have released to Production.  I don't have a whole lot of programs that I maintain but there is enough of a variety that I do switch around to the various bug fixes and new features and it would be really cool to see them all on a Single Calendar.  This way I can be sure that features and fixes are getting out there and my stakeholders can also be aware of this information.

I have chosen to use [Asana Calendar](https://asana.com) because they have a good [API](https://en.wikipedia.org/wiki/API) that I can use to make this happen right within the final step of my CI/CD pipeline.  When I release a version of my product into Production and it was successful as a final step I call a Python script and an entry is posted on the Calendar.  I don't have to think about it again it is just part of the normal CI/CD pipeline but the Calendar is always up to date for who ever wants to know this information.

# Start with an Asana Account
Asana is a whole project management suite but for this post I am only interested in the Calendar portion of the product.  You can start with the free version of the product which allows collaboration with up to 15 team members.  There are reasonably priced plans if you need more than that.  My wife and I started using Asana a couple of years ago to maintain a common family Calendar.
1. Open up the Asana site [https://asana.com](https://asana.com) and click on the Try for Free button.
2. Enter you email address in the text box provided and press the Try for Free button
3. Make sure that you use a real email address as the next step you will need to verify before you can go on.
4. In your email you should get a message like this so click on the Verify email address button to continue.
5. This will take you back to the Asana web site where you enter your full name and give it a password and press Continue.
6. In this next screen they are collecting some demographics to better understand what kind of industries are using their product. I selected IT, but choose what best describes your line of business and click on the Continue button.
7. Next they want to know what you main objective in Asana, choose what you like, you can even select I'm not sure yet, and then click on Continue.
8. Now we get to the place of setting up your first Project and they want you to give it a name.  This is where I choose to put the name "Release" as this Project will hold my Release Calendar.  Click on the Continue button.
9. Asana tries to be helpful by providing a few sample tasks that you might do.  Jest leave these for now and click on the Continue button again.
10. Next it is asking about how you would like to group these tasks, just accept the default and click on the Continue button.
11. Now we get to make a decision on the layout to use.  Select the Calendar and then click on the Continue button.
12. In this last and final stage it is asking who all is working on this project with you.  You can add additional emails if you wish and when you are ready click on the "Take me to my project" button.
13. You will get a message about you 30-day trial and once that ends it will continue with a free Basic plan.  Click on the "Let's get started" button
14. In trying to be helpful we have 3 tasks that we don't need on our calendar.  Right click each of them and choose delete so you end up with a clean calendar.

We will get back to our calendar in a few minutes as we will need a project number and an personal access code but first lets look at the Python script that I wrote to populate this calendar with our releases into Production.

# The Python Script
