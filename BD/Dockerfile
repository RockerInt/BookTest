FROM mcr.microsoft.com/mssql/server:2019-latest
# Install Unzip
RUN apt-get update \
    && apt-get install unzip -y
# Install SQLPackage for Linux and make it executable
RUN wget -progress=bar:force -q -O sqlpackage.zip https://go.microsoft.com/fwlink/?linkid=873926 \
    && unzip -qq sqlpackage.zip -d /opt/sqlpackage \
    && chmod +x /opt/sqlpackage/sqlpackage
# Add the DACPAC to the image
COPY Books\bin\Debug\Books.dacpac /tmp/db.dacpac
# Configure external build arguments to allow configurability.
ARG DBNAME=Books
ARG PASSWORD
# Configure the required environmental variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=12345678a
# Launch SQL Server, confirm startup is complete, deploy the DACPAC, then terminate SQL Server.
RUN ( /opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" \
    && /opt/sqlpackage/sqlpackage /a:Publish /tsn:. /tdn:${DBNAME} /tu:sa /tp:$SA_PASSWORD /sf:/tmp/db.dacpac \
    && rm /tmp/db.dacpac \
    && pkill sqlservr