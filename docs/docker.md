# [Docker](/README.md)
As we start to move towards build, deploying and using docker as the final resting place for the Blog web site it is time to start to collect the commands that we have been using to build the images.
### About the t3winc/hugo image
This is the initial container that we start with before we dump our source onto the build container to generate the web site.  It is in a separate public github repository called t3winc/docker-hugo.  There you will find the Dockerfile and any supporting components needed to build that image.
### Docker Build Comand
This command will build the image and tag it...
```
docker image build --rm -f .\build\Dockerfile -t schulzdl/donaldonsoftware.com:v1.11.0 .
```
### Docker Run Command
This command will start up the container in iteractive mode and leave you inside of Bash where you can explore and confirm that things are working okay.
When you exit the container, the container will be destroyed just to keep things clean.
```
docker run -it --rm schulzdl/donaldonsoftware.com:v1.11.0 bash
```
