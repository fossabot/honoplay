#!/bin/bash

SERVICE_NAME=honoplay_trainee_web_ui_app
IMAGE_NAME=trainee-web-ui

if docker service ls | grep -q ${SERVICE_NAME}; then
	echo "$(tput setaf 2) ${SERVICE_NAME} Bulundu Guncelleniyor...$(tput sgr 0)"
	
    docker service update \
        --image ${DOCKER_REGISTRY}/${IMAGE_NAME}:${BUILD_NUMBER}teamcity \
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
		--env SERVICE_ADDRESS=demo.honoplay.com \
		--env REDIS_CACHE_SERVICE=honoplay_redis_cache_service \
		--env DC=DC-1 \
        --mount type=bind,source=/root/honoplay_static_files,destination=/app/wwwroot \
        --constraint 'node.labels.is-application-node == true' \
        ${DOCKER_REGISTRY}/${IMAGE_NAME}:${BUILD_NUMBER}teamcity
fi