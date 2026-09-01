FROM nginx:alpine
RUN apk add --no-cache gettext
COPY http.conf.template ssl.conf.template /etc/nginx/tpl/
COPY start.sh /start.sh
RUN chmod +x /start.sh \
    && rm -f /etc/nginx/conf.d/default.conf
ENTRYPOINT ["/start.sh"]
