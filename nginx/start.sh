#!/bin/sh
set -e

DOMAIN="${DOMAIN:-_}"
export DOMAIN

render_config() {
    if [ "$DOMAIN" != "_" ] && [ -f "/etc/letsencrypt/live/${DOMAIN}/fullchain.pem" ]; then
        envsubst '${DOMAIN}' < /etc/nginx/tpl/ssl.conf.template > /etc/nginx/conf.d/default.conf
    else
        envsubst '${DOMAIN}' < /etc/nginx/tpl/http.conf.template > /etc/nginx/conf.d/default.conf
    fi
}

mkdir -p /var/www/certbot
render_config

# Reload nginx after Let's Encrypt certificates appear or renew.
(
    while true; do
        sleep 20
        render_config
        nginx -s reload >/dev/null 2>&1 || true
    done
) &

exec nginx -g "daemon off;"
