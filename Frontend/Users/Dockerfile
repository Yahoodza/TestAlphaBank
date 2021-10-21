FROM node:alpine AS build

WORKDIR /front/app
COPY package.json ./
RUN npm install
COPY . .
RUN npm install -g @angular/cli

ENV PATH /front/app/node_modules/.bin:$PATH

CMD npm run start