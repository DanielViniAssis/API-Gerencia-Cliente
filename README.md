# API Gerência de Clientes

API Restful desenvolvida em **.NET 9** com **Entity Framework**, **DTOs**, **Mapper** e arquitetura organizada para gerenciar clientes, endereços e contatos.  
O projeto inclui testes de endpoints via **Postman**, com exemplos de respostas esperadas.

---

## Tecnologias utilizadas

- .NET 9
- C#
- Entity Framework Core (SQLite)
- AutoMapper
- DTOs (Data Transfer Objects)
- Visual Studio Code
- Postman (para testes)
- SQLite (banco de dados local)

---

## Modelagem dos dados requisitado pelo cliente

<img width="334" height="244" alt="image" src="https://github.com/user-attachments/assets/3d5caf3d-f4ee-4a9e-9857-bf53a4d8b9ea" />

*Modelo de dados e relacionamentos entre Cliente, Endereço e Contatos.*

## Estrutura do projeto

    API-Gerencia-Clientes/

    ├── Controllers/ # Endpoints da API

    ├── DTOs/ # Data Transfer Objects

    ├── Models/ # Entidades do banco de dados

    ├── Profiles/ # Configurações do AutoMapper

    ├── Properties/ # Configurações do Projeto com URL do localhost

    ├── Services/ # Serviços da aplicação ViaCEP

    ├── Data/ # Contexto do EF

    ├── Migrations/ # Migrations do EF

    ├── Docs/ # Documentação e collection do Postman

    ├── SistemaCliente.csproj

    ├── SistemaCliente.sln

    └── README.md

## Como rodar a API localmente

1. Clone o repositório:
```bash
git clone https://github.com/DanielViniAssis/API-Gerencia-Cliente
```
2. Acesse a pasta do projeto:
```bash
cd API-Gerencia-Cliente
```
3. Instale as dependências:
```bash
dotnet restore
```
#### Caso não instale corretamente:
```bash
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package AutoMapper
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```
4. Restaure todas as dependências 
```bash
dotnet restore
```

5. Crie e aplique as migrations
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

6. Execute o projeto
```bash
dotnet run
```

7. A API iniciará:
```bash
http://localhost:5167
```

8. Testando os endpoints

 - Todos os endpoints foram testados no Postman, e a documentação completa está incluída na collection:
```bash
Docs/API Gerência Cliente.postman_collection.json
```
* A collection contém exemplos de:

* POST → Criação de clientes

* PUT → Atualização de clientes

* GET → Consulta de clientes

* DELETE → Exclusão de clientes

* Cada rota inclui exemplos de responses como:

* 200 (OK) → Operação bem-sucedida

* 400 (BadRequest) → Dados inválidos

* 404 (NotFound) → Cliente não encontrado

Lembrando que dentro da collection temos a documentação de todas as requests.

<img width="759" height="655" alt="image" src="https://github.com/user-attachments/assets/9e42b835-2314-455b-ab52-175ffa2c7e3c" />

*Exemplo de requisição POST com responses detalhadas.*

## Detalhes sobre o Collections

Dentro do nosso collection teremos uma variavel de ambiente chamda BaseUrl aonde armazenamos a url base da nossa aplicação sendo a http://localhost:5167 você pode acessar ela no canto direito do postman integrado ao Visual Studio Code.

<img width="403" height="264" alt="Capturar" src="https://github.com/user-attachments/assets/e50538c0-f8c5-4746-a327-5e5a22d6ea4c" />

<img width="953" height="254" alt="BaseUrl" src="https://github.com/user-attachments/assets/4a6754b0-6fa6-4f34-846a-ac545e1e8241" />


*Variavel de ambiente base URL.*

## Abaixo disponibilizo um json para Post (como mencionado o mesmo se encontra na documentação)

<img width="948" height="581" alt="post" src="https://github.com/user-attachments/assets/0f3f2e9a-0c47-4aa2-a919-d0a6b6b9ebfa" />

*Exemplo de POST pelo PostMan.*

```json
{
  "nome": "Cliente 01",
  "endereco": {
    "cep": "08673000",
    "numero": "1323",
    "complemento": "N/A"
  },
  "contatos": [
    {
      "tipo": "telefone",
      "texto": "(11) 1111-1111"
    },
    {
      "tipo": "email",
      "texto": "contato@teste.com.br"
    }
  ]
}
```

## Abaixo um Json de PUT (Update)

<img width="948" height="671" alt="put" src="https://github.com/user-attachments/assets/99969162-e663-4769-b291-807a02f72e7e" />

*Exemplo de PUT pelo PostMan.*

```json
{
    "nome":"Cliente 01",
    "endereco":{
        "cep":"08780060",
        "numero":"1003",
        "complemento":"ao lado do posto"},
        "contatos":[{
            "id":1,
            "tipo":"telefone",
            "texto":"(11) 88888"
            },
            {"id":1,
            "tipo":"email",
            "texto":"contato@daniel.com.br"
            }
            ]
}

```
## Abaixo Exemplo de como executar um delete.

<img width="957" height="554" alt="delete" src="https://github.com/user-attachments/assets/6125eb30-8963-4008-a3af-d99468628449" />

*Exemplo de DELETE pelo PostMan.*

