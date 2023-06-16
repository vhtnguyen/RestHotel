username=$1
password=$2

docker login -u $username -p $password
#docker build -t $username/resthotel:latest .
docker push $username/resthotel:latest