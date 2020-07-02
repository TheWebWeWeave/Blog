FROM t3winc/hexo-base:v1.0.1 as builder

# set the workdir
WORKDIR /app/blog
COPY ./src/source/ ./source/
RUN ls -la /app/blog/source/*
COPY ./src/themes/ ./themes
RUN ls -la /app/blog/themes/*
WORKDIR /app/blog
COPY ./src/_config.yml .

# tweak the hexo modules a little
RUN npm install
RUN npm install hexo-generator-feed --save

# generate the website
RUN hexo generate
