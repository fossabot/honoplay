#!/bin/bash

SERVICE_NAME=honoplay_db_service
SERVICE_IMAGE_NAME=omegabigdata/mssql
LIMIT_CPU=$(( $(nproc) / 4 ))
LIMIT_MEMORY=10000m

if docker service ls | grep -q ${SERVICE_NAME}; then
	echo "$(tput setaf 2) ${SERVICE_NAME} Bulundu Guncelleniyor...$(tput sgr 0)"
	
    docker service update --force \
        --image ${SERVICE_IMAGE_NAME} \
        --limit-cpu $LIMIT_CPU \
        --limit-memory $LIMIT_MEMORY \
		${SERVICE_NAME}
else
	echo "$(tput setaf 3) ${SERVICE_NAME} Bulunamadi Kuruluyor...$(tput sgr 0)"
	mkdir /root/database-files/${SERVICE_NAME}
	
    docker service create \
        --name ${SERVICE_NAME} \
        --mode replicated \
        --env SA_PASSWORD=Hedele321? \
        --env ACCEPT_EULA=Y \
        --env MSSQL_PID=Standard \
        --env MSSQL_AGENT_ENABLED=true \
        --env MSSQL_COLLATION=Turkish_CI_AS \
        --env TZ=Etc/GMT-3 \
        --with-registry-auth \
        --network backend \
        --publish 2443:1433 \
        --limit-cpu $LIMIT_CPU \
        --limit-memory $LIMIT_MEMORY \
        --mount type=bind,source=/root/database-files/${SERVICE_NAME},destination=/var/opt/mssql \
        --constraint 'node.labels.is-database-node == true' \
        ${SERVICE_IMAGE_NAME}
fi
