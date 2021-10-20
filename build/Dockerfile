FROM t3winc/hugo:latest as builder
LABEL stage=builder

FROM nginx
COPY --from=builder /usr/share/nginx/html /usr/share/nginx/html