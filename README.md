# TarefasApp

TarefasApp é uma aplicação desenvolvida em .NET 9 para gerenciamento de tarefas, projetos e usuários. O sistema oferece uma API RESTful para manipulação de tarefas e projetos, integração com banco de dados SQL Server e MongoDB, além de suporte a testes automatizados.

## Funcionalidades

- Cadastro, edição e remoção de projetos
- Gerenciamento de tarefas vinculadas a projetos
- Cadastro e gerenciamento de usuários
- API RESTful para operações CRUD
- Testes automatizados com xUnit
- Mapeamento de entidades com Entity Framework Core
- Suporte a containers Docker

## Estrutura do Projeto

- **TarefasApp.Api**: Camada de apresentação (API)
- **TarefasApp.Domain**: Entidades, DTOs e serviços de domínio
- **TarefasApp.Infra.Data**: Repositórios e mapeamentos de dados
- **TarefasApp.Tests**: Testes automatizados

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (opcional, para execução em containers)

## Como Executar

Execute todos os comandos abaixo em sequência no terminal:

1. Clone o repositório: `git clone <url-do-repositorio> && cd TarefasApp`
2. Restaure os pacotes: `dotnet restore`
3. Execute as migrações e inicie a aplicação: `dotnet build && dotnet run --project TarefasApp.Api`

## Executando os Testes

`dotnet test`

## Endpoints Principais

- `/api/projetos` - Gerenciamento de projetos
- `/api/tarefas` - Gerenciamento de tarefas
- `/api/usuarios` - Gerenciamento de usuários

## Executando com Docker

Para rodar toda a stack (API, SQL Server e MongoDB) em containers Docker:

1. Certifique-se de que o Docker está instalado e em execução.

2. No terminal, navegue até a pasta do projeto.

3. Execute o comando abaixo para construir e iniciar os containers:    docker-compose up --build
 
4. Aguarde até que os containers estejam completamente iniciados.

5. Acesse a aplicação em `http://localhost:5000` (ou na porta definida no seu `docker-compose.yml`).

6. Para executar os testes, execute o comando: docker-compose exec api dotnet test

7. Para parar e remover os containers, use: docker-compose down

- A API estará disponível em [http://localhost:5000](http://localhost:5000)
- O SQL Server estará acessível em `localhost:1435` (usuário: sa, senha: Desafio@2025)
- O MongoDB estará acessível em `localhost:27018` (usuário: admin, senha: desafio2025)

> **Observação:** Internamente, a API se comunica com o SQL Server na porta 1433 e com o MongoDB na porta 27017, conforme configurado no `docker-compose.yml` e no Dockerfile.

## Executando os Testes

Para rodar todos os testes automatizados: dotnet test

`docker-compose exec api dotnet test`

## Licença

Este projeto está licenciado sob a licença MIT.
