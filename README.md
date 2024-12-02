# 🍔 Burguer Mania API

A Burguer Mania API é uma aplicação para gerenciar pedidos e produtos de uma hamburgueria. Esta API permite criar, ler, atualizar e deletar informações sobre os produtos e pedidos.

## 🛠️ Tecnologias Utilizadas

- .NET
- ASP.NET Core
- MySQL

## 🚀 Como Executar

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

6. Restaure as dependências:

  ```bash
  dotnet restore
  ```

7. Atualize o banco de dados:

  ```bash
  dotnet ef database update
  ```
  
8. Inicie o servidor:

  ```bash
  dotnet run
  ```

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

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.