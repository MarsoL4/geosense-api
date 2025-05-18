# GeoSense API

## Descrição do Projeto

API RESTful desenvolvida em .NET para gerenciamento de motos e vagas de estacionamento, utilizando arquitetura em camadas, banco de dados Oracle e documentação via Swagger.

---

## Rotas Disponíveis

### Moto
- `GET /api/moto` – Lista todas as Motos
- `GET /api/moto/{id}` – Buscar Moto por ID
- `PUT /api/moto/{id}` – Atualizar Moto
- `DELETE /api/moto/{id}` – Remover Moto

### Vaga
- `GET /api/vaga` – Lista todas as Vagas
- `GET /api/vaga/{id}` – Buscar Vaga por ID
- `POST /api/vaga` – Cadastrar nova Vaga
- `PUT /api/vaga/{id}` – Atualizar Vaga
- `DELETE /api/vaga/{id}` – Remover Vaga

---

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core
- C#
- Entity Framework Core
- Oracle Entity Framework Core (ODP.NET)
- AutoMapper
- Swagger (Swashbuckle.AspNetCore)
- Visual Studio 2022

---

## Instruções de Execução

1. Clone o projeto:
   ```bash
   git clone https://github.com/MarsoL4/geosense-api.git
   cd geosense-api
   ```

2. Configure a conexão Oracle no `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "Oracle": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=SEU_SERVIDOR"
   }
   ```

3. Execute os comandos:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project GeoSense.API
   ```

4. Acesse no navegador:
   - Swagger: Deverá abrir automaticamente após a execução do Projeto.

---

## 👥 Integrantes

- Enzo Giuseppe Marsola – RM: 556310, 2TDSPK
- Rafael de Souza Pinto – RM: 555130, 2TDSPY
- Luiz Paulo F. Fernandes – RM: 555497, 2TDSPF
