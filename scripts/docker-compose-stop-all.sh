post_fix='compose.yml'
cd compose

compose_file=(elk-kibana-$post_fix prometheus-grafana-$post_fix seq-syslog-$post_fix sql-redis-$post_fix zkp-$post_fix nginx-api-$post_fix)
for file in ${compose_file[*]}
do 
    echo =====================================
    echo remove docker container for compose file $file
    echo =====================================

    docker-compose -f $file down
done