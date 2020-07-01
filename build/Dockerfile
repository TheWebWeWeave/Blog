FROM node:lts-stretch AS builder
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
WORKDIR /app/blog
COPY src/package.json .
RUN npm install hexo --save


## expose the HEXO_SERVER_PORT
EXPOSE ${HEXO_SERVER_PORT}

## npm install the latest packages from package.json and run the hexo server
CMD npm install; hexo clean; hexo server -d -p ${HEXO_SERVER_PORT}
