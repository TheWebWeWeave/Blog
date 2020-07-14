#This should send the image up to docker hub...
cat /var/jenkins_home/my_password.txt | docker login --username schulzdl --password-stdin
docker tag $(docker images | awk '{print $3}' | awk 'NR==2') t3winc/donaldonsoftware:$1
docker push t3winc/donaldonsoftware:$1