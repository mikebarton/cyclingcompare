FROM node:alpine

# create & set working directory
RUN mkdir -p /usr/src
WORKDIR /usr/src

# copy source files
COPY . /usr/src
ENV NEXT_PUBLIC_MY_MESSAGE="the buster"
# install dependencies
RUN npm install -only=production

# start app
RUN npm run build   
EXPOSE 3000
CMD npm run start