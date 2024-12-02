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

4. Restaure as dependências:

    ```bash
    dotnet restore
    ```

5. Atualize o banco de dados:

    ```bash
    dotnet ef database update
    ```

6. Inicie o servidor:

    ```bash
    dotnet run
    ```

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.