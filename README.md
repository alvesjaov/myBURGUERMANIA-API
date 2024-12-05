# 🍔 Burguer Mania API
A Burguer Mania API é uma aplicação para gerenciar pedidos e produtos de uma hamburgueria. Esta API permite criar, ler, atualizar e deletar informações sobre os produtos e pedidos.

## 🛠️ Tecnologias Utilizadas

- .NET 8.0
- ASP.NET Core
- MySQL
- Entity Framework Core
- AutoMapper
- Swashbuckle (Swagger)
- Docker
- Docker Compose
- DotNetEnv

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter as seguintes ferramentas instaladas em sua máquina:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [MySQL](https://dev.mysql.com/downloads/mysql/)

## 🚀 Como Executar

### Sem Docker

1. Clone o repositório:

  ```bash
  git clone https://github.com/seu-usuario/myBURGUERMANIA-API.git
  ```

2. Navegue até o diretório do projeto:

  ```bash
  cd myBURGUERMANIA-API
  ```

3. Crie o arquivo `appsettings.json` com as configurações necessárias:

  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=burguer_mania;User Id=root;Password=yourpassword;"
    },
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },
    "AllowedHosts": "*"
  }
  ```

4. Crie um arquivo `.env` na raiz do projeto com base no arquivo `.env.example`:

  ```sh
  cp .env.example .env
  ```

5. Atualize as variáveis de ambiente no arquivo `.env` com suas próprias configurações.

6. Execute a aplicação:

  ```bash
  dotnet run
  ```

### Com Docker

1. Clone o repositório:

  ```bash
  git clone https://github.com/seu-usuario/myBURGUERMANIA-API.git
  ```

2. Navegue até o diretório do projeto:

  ```bash
  cd myBURGUERMANIA-API
  ```

3. Crie um arquivo `.env` na raiz do projeto com base no arquivo `.env.example`:

  ```sh
  cp .env.example .env
  ```

4. Atualize as variáveis de ambiente no arquivo `.env` com suas próprias configurações.

5. Construa a imagem Docker:

  ```bash
  docker-compose build
  ```

6. Inicie o contêiner Docker:

  ```bash
  docker-compose up
  ```

## 🌐 Acessando a API

Localmente, a API pode ser acessada em [http://localhost:8080](http://localhost:8080).

Online, a API pode ser acessada em [https://myburguermania-api.onrender.com](https://myburguermania-api.onrender.com).

## 📚 Rotas da API

### Category

- `GET /api/Category` - Retorna uma lista de todas as categorias.
- `POST /api/Category` - Cria uma nova categoria.
- `GET /api/Category/{id}` - Retorna os detalhes de uma categoria específica pelo seu ID.
- `PUT /api/Category/{id}` - Atualiza uma categoria específica pelo seu ID.
- `DELETE /api/Category/{id}` - Exclui uma categoria específica pelo seu ID.

### Order

- `POST /api/Order` - Cria um novo pedido.
- `GET /api/Order/{id}` - Retorna os detalhes de um pedido específico pelo seu ID.
- `PATCH /api/Order/{id}` - Atualiza parcialmente um pedido específico pelo seu ID.
- `PATCH /api/Order/{id}/cancel` - Cancela um pedido específico pelo seu ID.

### Product

- `GET /api/Product` - Retorna uma lista de todos os produtos.
- `POST /api/Product` - Cria um novo produto.
- `GET /api/Product/{id}` - Retorna os detalhes de um produto específico pelo seu ID.
- `PUT /api/Product/{id}` - Atualiza um produto específico pelo seu ID.
- `DELETE /api/Product/{id}` - Exclui um produto específico pelo seu ID.

### Status

- `GET /api/Status` - Retorna uma lista de todos os status.
- `POST /api/Status` - Cria um novo status.
- `GET /api/Status/{id}` - Retorna os detalhes de um status específico pelo seu ID.
- `PUT /api/Status/{id}` - Atualiza um status específico pelo seu ID.
- `DELETE /api/Status/{id}` - Exclui um status específico pelo seu ID.

### User

- `POST /api/User` - Cria um novo usuário.
- `GET /api/User/cpf/{cpf}` - Retorna os detalhes de um usuário específico pelo seu CPF.
- `GET /api/User/{id}` - Retorna os detalhes de um usuário específico pelo seu ID.
- `PUT /api/User/{id}` - Atualiza um usuário específico pelo seu ID.
- `DELETE /api/User/{id}` - Exclui um usuário específico pelo seu ID.

### Login

- `POST /api/Login` - Autentica um usuário.
- `DELETE /api/Login` - Realiza logout de um usuário.

### SelectedProducts

- `POST /api/SelectedProducts` - Cria uma nova seleção de produtos.
- `GET /api/SelectedProducts/{id}` - Retorna os detalhes de uma seleção de produtos específica pelo seu ID.
- `PUT /api/SelectedProducts/{id}` - Adiciona mais IDs de produtos a uma seleção de produtos existente.
- `DELETE /api/SelectedProducts/{id}` - Exclui uma seleção de produtos específica pelo seu ID.
- `DELETE /api/SelectedProducts/{selectedProductsId}/product/{productId}` - Remove um produto específico de uma seleção de produtos.

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.