FROM node:12.10.0-alpine

WORKDIR /usr/src/app

RUN apk update && \
    apk upgrade && \
    apk add bash curl vim

COPY . .

RUN yarn install
RUN export PATH="$PATH:/usr/src/app/node_modules/.bin"

EXPOSE 3000
