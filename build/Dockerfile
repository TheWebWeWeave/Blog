FROM node:5.3.0-slim AS builder
MAINTAINER Donald Schulz <donald@schulz.com>

## set HEXO_SERVER_PORT environment default
## Not Sure if we really need this as we are just using this to generate the site.
ENV HEXO_SERVER_PORT=4000

## update the repositories
RUN apt-get update
## install git to get the source
RUN apt-get install git -y

## install hexo-cli globally
RUN npm install -g hexo-cli

## set the workdir
WORKDIR /app
COPY github_clone_using_token.sh .
# Verify that the file is there.
RUN ls -l
#RUN chmod +x /app/gethub_clone_using_token.sh
#RUN /app/github_clone_using_token.sh

## expose the HEXO_SERVER_PORT
EXPOSE ${HEXO_SERVER_PORT}


