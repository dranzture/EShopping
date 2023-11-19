# Install cluster 
Follow the commands:
 - kubectl config set-context --current --namespace=local-eshopping
 - helm upgrade bitnami-apache-kafka oci://registry-1.docker.io/bitnamicharts/kafka --set replicaCount=1 --set listeners.client.protocol=plaintext --set auth.interBrokerProtocol=plaintext