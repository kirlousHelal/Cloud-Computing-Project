# 5. Load Balancer Image (NGINX): Balances incoming traffic among multiple instances of your .NET Core application (if you plan to scale horizontally).

# Use the official NGINX image
FROM nginx

# Copy custom NGINX configuration
COPY nginx.conf /etc/nginx/nginx.conf

# Expose NGINX ports
EXPOSE 80
