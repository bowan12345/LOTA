#!/bin/sh

if [ -z "$DOMAIN" ] || [ "$DOMAIN" = "_" ] || [ "$DOMAIN" = "localhost" ]; then
    echo "DOMAIN is not set. Skipping Let's Encrypt."
    tail -f /dev/null
fi

if [ -z "$ACME_EMAIL" ]; then
    echo "ACME_EMAIL is not set. Skipping Let's Encrypt."
    tail -f /dev/null
fi

echo "Waiting for nginx to serve ACME challenges..."
sleep 25

while true; do
    certbot certonly \
        --webroot \
        --webroot-path /var/www/certbot \
        --domain "$DOMAIN" \
        --email "$ACME_EMAIL" \
        --agree-tos \
        --no-eff-email \
        --keep-until-expiring \
        --non-interactive || echo "certbot failed, will retry later"
    sleep 12h
done
