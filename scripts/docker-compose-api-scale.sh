scale=$1

cd compose
docker-compose -f nginx-api-compose.yml up --scale rest_hotel=$scale -d


#docker-compose -f nginx-api-compose.yml up --scale rest_hotel=$scale --build -d