username=$1
password=$2

docker login -u $username -p $password
docker image pull $username/hotel.api:latest