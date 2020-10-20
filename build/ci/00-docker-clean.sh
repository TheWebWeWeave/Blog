#!/bin/sh
log=/var/lib/docker/prune.log
date + '=== %Y.%m.%d %H:%M ===' >> $log
docker system prune -af --filter "until=$((7*24))h" >> $log
# docker rm $(docker ps --no-trunc -aq)
# docker rmi $(docker images -q)