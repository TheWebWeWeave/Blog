FROM t3winc/hexo-base:v1.0 as builder

# set the workdir
WORKDIR /app/blog/source
COPY ./src/source/*.* .
WORKDIR /app/blog/themes
COPY ./src/themes/*.* .
WORKDIR /app/blog
COPY _config.yml .

# generate the website
RUN hexo generate
