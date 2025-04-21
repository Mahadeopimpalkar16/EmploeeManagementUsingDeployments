FROM nginx:latest
# Copy the Angular app files
COPY ./dist/employee-management/browser /usr/share/nginx/html
# Rename index.csr.html to index.html to match Nginx's default behavior
RUN mv /usr/share/nginx/html/index.csr.html /usr/share/nginx/html/index.html
# Add Nginx configuration
COPY ./default.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
