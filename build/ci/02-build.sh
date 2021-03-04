# docker-compose -f docker-compose.yml build --pull
docker image build --rm -f ./build/Dockerfile -t donaldonsoftware .
docker image prune --filter label=stage=builder