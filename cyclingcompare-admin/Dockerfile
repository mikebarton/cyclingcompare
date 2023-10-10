FROM node:13.12.0-alpine

# create & set working directory
RUN mkdir -p /usr/src
WORKDIR /usr/src

# copy source files
COPY . /usr/src

ENV REACT_APP_IMPORT_API_HOST="https://import-admin-s65wljneuq-ts.a.run.app"
ENV REACT_APP_PRODUCT_SEARCH_API_HOST="https://product-search-admin-s65wljneuq-ts.a.run.app"
ENV REACT_APP_IDENTITY_API_KEY="AIzaSyBXvPFAdLOrTZzjsbsaK_-lqRiIkgEbDJE"
ENV REACT_APP_IDENTITY_AUTHDOMAIN="cyclingcompare.firebaseapp.com"
# install dependencies
RUN npm cache clean --force
RUN npm install -only=production

# start app
RUN npm run build
EXPOSE 3000
CMD npm run start