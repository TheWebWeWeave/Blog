FROM t3winc/hugo:latest as builder
LABEL stage=builder

FROM nginx
COPY --from=builder /usr/share/nginx/html /usr/share/nginx/html
COPY site/static/favicon.png /usr/share/nginx/html/favicon.png