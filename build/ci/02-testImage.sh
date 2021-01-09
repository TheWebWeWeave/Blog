i=0
container-structure-test test --image donaldonsoftware --config ./test/DockerTest/unit-test.yaml || ((i++))

exit $i