﻿# Requirements(Install Helm):
- Download chocolatey on powershell commands by:
  - Set-ExecutionPolicy AllSigned
  - Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
- choco install kubernetes-helm
