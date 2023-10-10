# FROM gcr.io/cloudsql-docker/gce-proxy:1.19.1 as proxy
# COPY  ["bikecompare-dev-2215fb28bad8.json", "bikecompare-dev-2215fb28bad8.json"]
# CMD [ "/cloud_sql_proxy", "-instances=bikecompare-dev:australia-southeast1:bikecompare-dev-sql=tcp:0.0.0.0:3307", "-credential_file=bikecompare-dev-2215fb28bad8.json"]

FROM liquibase/liquibase:latest as base
WORKDIR /liquibase/changelog
COPY ["/changelog/", "../changelog/"]
# COPY ["/liquibase.properties", "."]

CMD ["--url=jdbc:mysql://host.docker.internal:3307/bikecompare", "--changeLogFile=changelogRoot.xml", "--username=root","--password=<PASSWORD>", "update"]