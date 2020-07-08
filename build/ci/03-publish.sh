#This should send the image up to docker hub...
#cat /var/jenkins_home/my_password.txt | docker login --username schulzdl --password-stdin
#docker push t3winc/donaldonsoftware:1.11.0-alpha1

#This should send the image up to github packages...
cat /var/jenkins_home/GH_TOKEN.txt | docker login docker.pkg.github.com -u SchulzDL --password-stdin
docker tag $(docker images | awk '{print $3}' | awk 'NR==2') docker.pkg.github.com/t3winc/blog/donaldonsoftware:1.11.0
docker push docker.pkg.github.com/t3winc/blog/donaldonsoftware:1.11.0