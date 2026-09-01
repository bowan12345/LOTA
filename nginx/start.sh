#!/bin/sh
set -e

DOMAIN="${DOMAIN:-_}"
DOMAIN_ALIASES="${DOMAIN_ALIASES:-}"
ACME_EMAIL="${ACME_EMAIL:-}"
export DOMAIN DOMAIN_ALIASES

render_config() {
    if [ "$DOMAIN" != "_" ] && [ -n "$DOMAIN" ] && [ -f "/etc/letsencrypt/live/${DOMAIN}/fullchain.pem" ]; then
        envsubst '${DOMAIN} ${DOMAIN_ALIASES}' < /etc/nginx/tpl/ssl.conf.template > /etc/nginx/conf.d/default.conf
    else
        envsubst '${DOMAIN} ${DOMAIN_ALIASES}' < /etc/nginx/tpl/http.conf.template > /etc/nginx/conf.d/default.conf
    fi
}

mkdir -p /var/www/certbot
render_config

(
    while true; do
        sleep 20
        render_config
        nginx -s reload >/dev/null 2>&1 || true
    done
) &

(
    if [ -z "$DOMAIN" ] || [ "$DOMAIN" = "_" ] || [ -z "$ACME_EMAIL" ]; then
        echo "DOMAIN or ACME_EMAIL is not set. Let's Encrypt is skipped."
    else
        echo "Waiting for nginx before requesting a certificate..."
        sleep 25

        while true; do
            echo "Running certbot for ${DOMAIN} ${DOMAIN_ALIASES}..."
            CERTBOT_DOMAINS="-d ${DOMAIN}"
            for extra in ${DOMAIN_ALIASES}; do
                CERTBOT_DOMAINS="${CERTBOT_DOMAINS} -d ${extra}"
            done

            if certbot certonly \
                --webroot \
                --webroot-path /var/www/certbot \
                ${CERTBOT_DOMAINS} \
                --email "$ACME_EMAIL" \
                --agree-tos \
                --no-eff-email \
                --keep-until-expiring \
                --non-interactive \
                --expand; then
                echo "Certificate is ready."
                sleep 12h
            else
                echo "certbot failed. Retrying in 5 minutes."
                sleep 300
            fi
        done
    fi
) &

exec nginx -g "daemon off;"
