i=0
container-structure-test test --image t3winc/donaldonsoftware:$1 --config ./test/DockerTest/unit-test.yaml || ((i++))

exit $i