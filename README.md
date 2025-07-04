# GeoSense API


## 📌 Descrição do Projeto

API RESTful desenvolvida em .NET para gerenciamento de motos e vagas de estacionamento, utilizando arquitetura em camadas, banco de dados Oracle, e documentação interativa via Swagger. O projeto foi implantado em uma VM Linux no Azure utilizando Docker.

---

## 📡 Rotas Disponíveis

### 🛵 Moto
- `GET /api/moto` – Lista todas as Motos
- `GET /api/moto/{id}` – Buscar Moto por ID
- `PUT /api/moto/{id}` – Atualizar Moto
- `DELETE /api/moto/{id}` – Remover Moto

### 🅿️ Vaga
- `GET /api/vaga` – Lista todas as Vagas
- `GET /api/vaga/{id}` – Buscar Vaga por ID
- `POST /api/vaga` – Cadastrar nova Vaga
- `PUT /api/vaga/{id}` – Atualizar Vaga
- `DELETE /api/vaga/{id}` – Remover Vaga

### 📊 Dashboard
- `GET /api/dashboard` – Dados resumidos: total de motos, motos com problema, vagas livres, ocupadas e disponíveis.

---

## 🛠️ Tecnologias Utilizadas

- .NET 8
- ASP.NET Core
- C#
- Entity Framework Core
- Oracle EF Core (ODP.NET)
- AutoMapper
- Swagger (Swashbuckle.AspNetCore)
- Docker
- Azure CLI
- Visual Studio 2022

---

## 🚀 Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
USER 1000
ENTRYPOINT ["dotnet", "GeoSense.API.dll"]
```

---

## ☁️ Scripts de Deploy (Azure CLI + Docker)

### 🧱 1. Criação do Resource Group e da VM no Azure

```bash
az group create --name geosense-rg --location eastus
az vm create --resource-group geosense-rg --name geosense-vm --image Ubuntu2204 --admin-username geosenseadm --authentication-type password --admin-password GeoSense@123 --public-ip-sku Standard --size Standard_B1s
az vm open-port --resource-group geosense-rg --name geosense-vm --port 80
```

### 🔐 2. Conectar à VM via SSH

```bash
ssh geosenseadm@<ip da vm>
```

### 🐳 3. Instalar Docker na VM

```bash
sudo apt update && sudo apt install docker.io -y && sudo systemctl start docker && sudo systemctl enable docker && sudo usermod -aG docker $USER
```

### 📁 4. Clonar e enviar os arquivos do projeto para a VM (do PowerShell local)

#### Clone o repositório (caso ainda não tenha)
```bash
git clone https://github.com/MarsoL4/geosense-api.git
cd geosense-api
```

#### Envie os arquivos do projeto para a VM
```bash
scp -r . geosenseadm@<ip da vm>:~/GeoSense.API
```

### 🧱 5. Acessar pasta do projeto e construir imagem Docker

```bash
ssh geosenseadm@<ip da vm>
cd GeoSense.API
docker build -t geosense-api .
```

### 🚢 6. Rodar o container

```bash
docker run -d -p 80:80 --name geosense-container geosense-api
```

### ⚙️ 7. Adicionar credenciais do banco no container

```bash
nano appsettings.json
docker cp appsettings.json geosense-container:/app/appsettings.json
docker restart geosense-container
```

---

## 🌐 Acesso

Após o deploy, o Swagger estará disponível em:

http://<ip da vm>/swagger/index.html

---

## 👥 Integrantes

- **Enzo Giuseppe Marsola** – RM: 556310, Turma: 2TDSPK  
- **Rafael de Souza Pinto** – RM: 555130, Turma: 2TDSPY  
- **Luiz Paulo F. Fernandes** – RM: 555497, Turma: 2TDSPF

---
