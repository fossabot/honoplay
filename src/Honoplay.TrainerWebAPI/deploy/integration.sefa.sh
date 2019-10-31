#!/bin/bash

SERVICE_NAME=honoplay_trainer_webapi_service_app
IMAGE_NAME=trainer-api-service

# nproc komutu cekirdek sayisini verir.
LIMIT_CPU=$(( $(nproc) / 4 ))
LIMIT_MEMORY=1000m

if docker service ls | grep -q ${SERVICE_NAME}; then
	echo "$(tput setaf 2) ${SERVICE_NAME} Bulundu Guncelleniyor...$(tput sgr 0)"
	
    docker service update \
        --image ${DOCKER_REGISTRY}/${IMAGE_NAME}:${BUILD_NUMBER}teamcity \
        --limit-cpu $LIMIT_CPU \
        --limit-memory $LIMIT_MEMORY \
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
	    --env REDIS_CACHE_SERVICE=honoplay_redis_cache_service \
        --limit-cpu $LIMIT_CPU \
        --limit-memory $LIMIT_MEMORY \
        --constraint 'node.labels.is-application-node == true' \
        ${DOCKER_REGISTRY}/${IMAGE_NAME}:${BUILD_NUMBER}teamcity
fi
