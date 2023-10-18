# Install cluster
Follow the commands:
 - kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/cloud/deploy.yaml
 - kubectl config set-context --current --namespace=eshopping
 - helm upgrade bitnami-apache-kafka oci://registry-1.docker.io/bitnamicharts/kafka --set replicaCount=1 --set listeners.client.protocol=plaintext --set auth.interBrokerProtocol=plaintext