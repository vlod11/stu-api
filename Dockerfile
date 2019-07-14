# base image
FROM node:12.4.0 as node

WORKDIR /angular-app

COPY UniHub.Client/Client .

RUN npm i yarn

#install packages
# you can change the version of angular CLI to the one you are using in your application
RUN yarn global add @angular/cli@latest
RUN yarn install

#RUN npm install -g @angular/cli 

# start app
RUN ng build --configuration=production

FROM nginx

RUN pwd

COPY nginx.conf ../etc/nginx

COPY --from=node /angular-app/dist/Client ./usr/share/nginx/html

RUN pwd
# VOLUME ["/var/log/nginx"]