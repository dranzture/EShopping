# Requirements(Install Helm):
- Download chocolatey on powershell commands by:
  - Set-ExecutionPolicy AllSigned
  - Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
- choco install kubernetes-helm
# Create namespace:
- kubectl create namespace kafka
- kubectl config set-context --current --namespace=kafka
# Helm Installation:
- helm repo add confluentinc https://packages.confluent.io/helm
- helm repo update
- helm install eshopping-kafka confluentinc/cp-helm-charts