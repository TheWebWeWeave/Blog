#This should send the image up to docker hub...
cat /var/jenkins_home/my_password.txt | docker login --username schulzdl --password-stdin
docker tag donaldonsoftware schulzdl/donaldonsoftware:$1
docker push schulzdl/donaldonsoftware:$1