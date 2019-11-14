FROM node:12.4.0 as node

WORKDIR /angular-app

COPY UniHub.Client/Client/dist/Client .

RUN ls

RUN yarn install

FROM nginx

RUN pwd

COPY nginx.conf ../etc/nginx

COPY --from=node /angular-app ./usr/share/nginx/html

RUN pwd
RUN cd ./usr/share/nginx/html
RUN ls