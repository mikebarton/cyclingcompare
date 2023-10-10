FROM node:alpine

# create & set working directory
RUN mkdir -p /usr/src
WORKDIR /usr/src

# copy source files
COPY . /usr/src

ENV NEXT_PUBLIC_LISTINGS_API_HOST='https://listings-api-s65wljneuq-ts.a.run.app'
ENV NEXT_PUBLIC_API_GATEWAY_HOST='https://cyclingcompare-api-gateway-85e8wc4e.ts.gateway.dev'
ENV NEXT_PUBLIC_GOOGLE_ANALYTICS_ID='G-GY5FQV1KGY'
# install dependencies
RUN npm install -only=production

# start app
RUN npm run build
EXPOSE 3000
CMD npm run start