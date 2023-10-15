# Cheatsheet
docker build -t authentication-image:latest . --no-cache
docker tag authentication-image:latest ghcr.io/dranzture/authentication-image:latest
docker push ghcr.io/username/authentication-image:latest

docker run -p 8080:80 -d ghcr.io/username/orchestrator-image:latest


kubectl create secret docker-registry mysecretname --docker-server=https://ghcr.io --docker-username=mygithubusername --docker-password=mygithubreadtoken --docker-email=mygithubemail

