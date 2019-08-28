#!/bin/bash

SERVICE_NAME=honoplay_redis_cache_service
IMAGE_NAME=redis

if docker service ls | grep -q ${SERVICE_NAME}; then
        echo "$(tput setaf 2) ${SERVICE_NAME} Bulundu Guncelleniyor...$(tput sgr 0)"
        
    docker service update --force \
        --image ${IMAGE_NAME} \
                ${SERVICE_NAME}
else
        echo "$(tput setaf 3) ${SERVICE_NAME} Bulunamadi Kuruluyor...$(tput sgr 0)"
        
    docker service create \
        --name ${SERVICE_NAME} \
        --mode replicated \
        --network backend --network frontend \
        --constraint 'node.labels.is-application-node == true' \
        ${IMAGE_NAME}
fi