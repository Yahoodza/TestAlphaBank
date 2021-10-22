FROM node:14-alpine

COPY package*.json /front/
WORKDIR /front/

RUN npm install
RUN npm install -g @angular/cli

ENV PATH /front/node_modules/.bin:$PATH

COPY . /front/app
WORKDIR /front/app

CMD npm run start