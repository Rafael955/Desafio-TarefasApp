# TarefasApp

TarefasApp � uma aplica��o desenvolvida em .NET 9 para gerenciamento de tarefas, projetos e usu�rios. O sistema oferece uma API RESTful para manipula��o de tarefas e projetos, integra��o com banco de dados SQL Server e MongoDB, al�m de suporte a testes automatizados.

## Funcionalidades

- Cadastro, edi��o e remo��o de projetos
- Gerenciamento de tarefas vinculadas a projetos
- Cadastro e gerenciamento de usu�rios
- API RESTful para opera��es CRUD
- Testes automatizados com xUnit
- Mapeamento de entidades com Entity Framework Core
- Suporte a containers Docker

## Estrutura do Projeto

- **TarefasApp.Api**: Camada de apresenta��o (API)
- **TarefasApp.Domain**: Entidades, DTOs e servi�os de dom�nio
- **TarefasApp.Infra.Data**: Reposit�rios e mapeamentos de dados
- **TarefasApp.Tests**: Testes automatizados

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (opcional, para execu��o em containers)

## Como Executar

Execute todos os comandos abaixo em sequ�ncia no terminal:

1. Clone o reposit�rio: `git clone <url-do-repositorio> && cd TarefasApp`
2. Restaure os pacotes: `dotnet restore`
3. Execute as migra��es e inicie a aplica��o: `dotnet build && dotnet run --project TarefasApp.Api`

## Executando os Testes

`dotnet test`

## Endpoints Principais

- `/api/projetos` - Gerenciamento de projetos
- `/api/tarefas` - Gerenciamento de tarefas
- `/api/usuarios` - Gerenciamento de usu�rios

## Executando com Docker

Para rodar toda a stack (API, SQL Server e MongoDB) em containers Docker:

1. Certifique-se de que o Docker est� instalado e em execu��o.

2. No terminal, navegue at� a pasta do projeto.

3. Execute o comando abaixo para construir e iniciar os containers:    docker-compose up --build
 
4. Aguarde at� que os containers estejam completamente iniciados.

5. Acesse a aplica��o em `http://localhost:5000` (ou na porta definida no seu `docker-compose.yml`).

6. Para executar os testes, execute o comando: docker-compose exec api dotnet test

7. Para parar e remover os containers, use: docker-compose down

- A API estar� dispon�vel em [http://localhost:5000](http://localhost:5000)
- O SQL Server estar� acess�vel em `localhost:1435` (usu�rio: sa, senha: Desafio@2025)
- O MongoDB estar� acess�vel em `localhost:27018` (usu�rio: admin, senha: desafio2025)

> **Observa��o:** Internamente, a API se comunica com o SQL Server na porta 1433 e com o MongoDB na porta 27017, conforme configurado no `docker-compose.yml` e no Dockerfile.

## Executando os Testes

Para rodar todos os testes automatizados: dotnet test

`docker-compose exec api dotnet test`

## Licen�a

Este projeto est� licenciado sob a licen�a MIT.
