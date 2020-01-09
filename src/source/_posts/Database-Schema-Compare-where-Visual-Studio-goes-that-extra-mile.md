title: Database Schema Compare where Visual Studio goes that extra mile
date: 2011-01-27 
tags:
- Database
- SQL
- Compare
- ALM
---
There are a number of good database tools out there for doing database schema comparisons.  I have used different ones over the years at first initially to help me write SQL differencing scripts that I could use when deploying database changes.  If your background is anything like mine where you were namely a Visual Basic or a C# developer and could get by with working on SQL if you could write directly to the database.  There were challenges with being able to script everything out using just SQL.  Today that is not nearly an issue for me and I can do quite a bit with scripting and could build those scripts by hand, but why? 
## WHAT… Visual Studio for database development?
Over the years I have tried to promote SQL development to be done in Visual Studio.  I made a great case, SQL is code just as much as my VB, C#, F# or what ever your favorite language of choice happens to be and should be protected in source control.  Makes sense but it is a really hard sell.  Productivity goes down hill, errors begin to happen because this is not how the SQL teams are used to working on databases.  It was an easier sell for me because I loved working in Visual Studio and found the SQL tools not to be as intuitive to me.  I have never been able to figure out how I could walk through a stored procedure in Query Analyzer or Management Studio but have always been able to do this with stored procedures that I wrote from within Visual Studio and that was long before the data editions of Visual Studio. 

Ever since the release of the Data Dude or its official name back then, Visual Studio Team Edition for Database Professionals, this was what I did and I tried to convince others that this is what we should be doing.  It was never an easy sell, yea the schema comparison was nice but our SQL professionals already had all kinds of comparison tools for SQL and it would be too hard for them to work this way.  They wanted to be able to make changes in a database and see the results of those changes, not have to deploy it somewhere first. 

So as a quick summary of what we figured out so far.  Schema comparison from one database to another, nothing new, your SQL department probably has a number of these and use them to generate their change scripts.  How is Visual Studio schema comparison better than what I already have how is it going to go the extra mile?  That my friend starts with the database project which does a reverse engineering of sorts of what you have in the database and scripts the whole thing out into source files that you can check into source control and compare the changes just like you do with any other source code. 

Now once you have a database project you are able to not just do a schema comparison with two databases but you can also compare from a database and this project.  The extra mile is that I can even go so far as to deploy the differences to your test and production databases.  It gets even better but before I tell you the best part lets go through the actual steps that you would take to create this initial database project. 
## Create the Database Project
I am going to walk you through the very simple steps that it takes to build a database project for the AdventureWorks database.  For this you will need Visual Studio 2010 Premium edition or higher. 

We start by creating a new project and select “SQL Server 2008 Database Project” template from under the Database - SQL Server project types.  Give it a name and set the location.  I called mine AdventureWorks because I am going to work with the sample AdventureWorks database.  Click OK.. 
{% asset_img image1.png "Create a Project" %}
Visual Studio will build a default database project for you, but it is not connected to anything so there is no actual database scripted out here.  We are going to do that now.  Right click on the database project and a context sensitive menu will popup with Import Database Objects and Settings… click on that now. 
{% asset_img image2.png "Import Objects" %}
This opens the Import Database Wizard dialog box.  If you have already connected to this database from Visual Studio then you will find an entry in the dropdown control Source database connection.  If not then you will create a new connection by clicking on the New Connection… button.  
{% asset_img image3.png "Import Wizard" %}
So if you have a ready made connection in the dropdown, choose it and skip the next screen and step as I am going to build my new connection. 
{% asset_img image4.png "New Connection" %}
Because my adventure works database in on my local machine I went with that but this database could be a database that is anywhere on your network, this will all just work provided you do have the necessary permissions to connect to it in this way.  Clicking on OK takes us back to the previous screen with the Source database connection filled in. 

Everyone, click Start which will bring up the following screen and start to import and script out the database.  When it is all done click the Finish button.  Congratulations you have built a Database Project. 
{% asset_img image5.png "Import Wizard Finishing" %}
You can expand the solution under Schema Objects, Schemas, and I am showing the dbo schema and it has 3 table scripts.  All the objects of this database are scripted out here.  You can look at these files right here is Visual Studio. 
{% asset_img image6.png "Solution Explorer" %}
However you might want to use the Schema View tool for looking at the objects which gives you a more Management Studio type of view. 
{% asset_img image7.png "Toolbar" %}
Just click on the icon in the Solution Explorer that has the popup caption that says Database Schema Viewer. 
{% asset_img image8.png "Schema View" %}
## Updating the Visual Studio Project from the database
In the past these were the steps that I would show and demonstrate on how to get a database project scripted out and now that it is code is really easy to get into version control because of the really tight integration from Visual Studio.  My thoughts after that is this is the tool that you should be working in to evolve the database.  Work in Visual Studio and deploy the changes to the database. 
## Light Bulb Moment
Just recently I discovered how the SQL developer does not really need to leave their favorite tool for working on the database, Management Studio.  That’s right, the new workflow is to continue to make your changes in your local or isolated databases so that you can see first hand how the database changes are going to work.  When you are ready to get those changes into version control you use Visual Studio and the Database Schema comparison. 
{% asset_img image9.png "Switch Control" %}
So here we see what I always thought was the normal workflow, with the Project on the left and the database that we are going to deploy to on the right.  If instead we are working on the database and we want to push those change to the Project, then switch the source and target around. 
{% asset_img image10.png "Options" %}
Now when you click the OK button you will get a schema comparison just like you always did but when deployed it will check out the project and update the source files.  This will then give you complete history and the files will move through the system from branch to branch with a perfect snapshot of what the database looked like for a specific build. 
{% asset_img image11.png "Options" %}
1.  Click this button to get the party started. 
2.  This comment will disappear in the project source file. 
3.  The source will be checked out during the update. 
## The Recap of what we have just seen.
This totally changes my opinion on how to go forward with this great tool.  The fact that we can update the project source from the database was probably always there but if I missed the fact that this was possible then I am sure many others might have missed it as well.  It makes SQL development smooth and safe (all schema scripts under version control) and the ready for the next step to smooth and automated deployment. 