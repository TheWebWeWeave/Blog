#This should send the image up to github packages...
cat /var/jenkins_home/my_password.txt | docker login --username schulzdl --password-stdin
docker tag donaldonsoftware schulzdl/donaldonsoftware:latest
docker push schulzdl/donaldonsoftware:latest