username=$1
password=$2

docker login -u $username -p $password
docker build -t $username/hotel.api:latest .
docker push $username/hotel.api:latest