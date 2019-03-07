#!/bin/bash

SERVICE_NAME=honoplay_admin_webapi_service_app
IMAGE_NAME=honoplay-admin-webapi-service

if docker service ls | grep -q ${SERVICE_NAME}; then
	echo "$(tput setaf 2) ${SERVICE_NAME} Bulundu Guncelleniyor...$(tput sgr 0)"
	
    docker service update \
        --image omegabigdata/${IMAGE_NAME}:${BUILD_NUMBER}teamcity \
		${SERVICE_NAME}
else
	echo "$(tput setaf 3) ${SERVICE_NAME} Bulunamadi Kuruluyor...$(tput sgr 0)"
	
    docker service create \
        --name ${SERVICE_NAME} \
        --mode replicated \
        --with-registry-auth \
        --network backend \
        --network frontend \
		--env TZ=Asia/Istanbul \
		--env SERVICE_ADDRESS=admin.honoplay.com \
		--env NEW_RELIC_APP_NAME=admin-honoplay-turkey \
		--env DC=DC-1 \
        --mount type=bind,source=/root/honoplay_static_files,destination=/app/wwwroot \
        --constraint 'node.labels.is-application-node == true' \
        omegabigdata/${IMAGE_NAME}:${BUILD_NUMBER}teamcity
fi
