FROM t3winc/hexo-base:v1.0 as builder

# set the workdir
WORKDIR /app/blog/source
COPY ./src/source/ ./source/
RUN ls -la /app/blog/source/*
WORKDIR /app/blog/themes
COPY ./src/themes/ ./themes
RUN ls -la /app/blog/themes/*
WORKDIR /app/blog
COPY _config.yml .

# generate the website
RUN hexo generate
