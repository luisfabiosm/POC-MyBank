# Microservice.API: Template .NET de Microserviço (Arquitetura Limpa/Hexagonal)

## Propósito do Projeto

Este projeto é um template .NET para iniciar novos microserviços seguindo os princípios de Clean Architecture (Arquitetura Limpa) e Hexagonal. Ele oferece uma estrutura básica com práticas recomendadas, facilitando a criação de APIs escaláveis, testáveis e fáceis de manter. O objetivo é permitir que desenvolvedores comecem rapidamente novos projetos de microserviço, já organizados conforme padrões de arquitetura limpa e adaptadores.

## Estrutura do Projeto

O template organiza o código em pastas principais de acordo com a arquitetura limpa e hexagonal. Entre os diretórios principais estão:

* **Domain/**: Contém as entidades de domínio, agregados, objetos de valor e interfaces que definem as regras de negócio centrais. Toda lógica de negócio pura fica concentrada aqui, sem dependências externas.
* **Adapters/**: Inclui código de infraestrutura e comunicação externa, separando adaptadores de entrada (inbound) e saída (outbound):

  * **Adapters/Inbound/**: Contém adaptadores de entrada, como controladores da API ou definições de portas de entrada que recebem requisições e acoplam à lógica de domínio.
  * **Adapters/Outbound/Database/SQL/** e **Adapters/Outbound/Database/NoSQL/**: Contêm implementações de repositórios para acesso a banco de dados. O diretório **SQL** inclui exemplos para SQL Server/PostgreSQL, enquanto **NoSQL** traz a implementação para MongoDB. Apenas a opção escolhida via `--database-type` será incluída no projeto final.
  * **Adapters/Outbound/Messaging/Kafka/** e **Adapters/Outbound/Messaging/RabbitMQ/**: Contêm adaptadores de mensageria para Kafka e RabbitMQ, habilitados pelos parâmetros `--use-kafka` e `--use-rabbitmq`, respectivamente.
  * **Adapters/Outbound/Metrics/**: Contém adaptadores para coleta de métricas (por exemplo, integração com Prometheus), habilitados pelo parâmetro `--use-metrics`.
* **Microservice.API/** (projeto principal): Diretório raiz (por padrão `src/mybank`) que contém o ponto de entrada da aplicação (por exemplo, `Program.cs`), configurações iniciais e o arquivo de projeto (`.csproj`). Aqui ficam as configurações de inicialização, injeção de dependências e roteamento da API.

Essa estrutura modular facilita a manutenção, pois separa claramente a lógica de negócio (no domínio) das implementações externas (nos adaptadores).

## Parâmetros do Template (CLI)

O template permite configuração via CLI do `dotnet new` usando os seguintes parâmetros:

* `--database-type {none|mongodb|sqlserver|postgresql}` (ou `-db`): Define o tipo de banco de dados a ser usado. Opções disponíveis:

  * `none`: sem persistência (apenas domínio em memória).
  * `mongodb`: inclui adaptadores de MongoDB.
  * `sqlserver`: inclui adaptadores para SQL Server.
  * `postgresql`: inclui adaptadores para PostgreSQL.
    O padrão é `sqlserver`.
* `--use-kafka` (ou `-kafka`): Inclui o adaptador de mensageria Kafka no projeto. Se não for especificado, o projeto não terá integração com Kafka.
* `--use-rabbitmq` (ou `-rabbitmq`): Inclui o adaptador de mensageria RabbitMQ no projeto. Se não for especificado, o projeto não terá integração com RabbitMQ.
* `--use-metrics` (ou `-metrics`): Inclui adaptadores de métricas (por exemplo, Prometheus) no projeto. O padrão é habilitado (`true`); para desabilitar métricas, é possível passar `--use-metrics false`.
* `--version <versão>` (ou `-v <versão>`): Define a versão do microserviço que será aplicada no arquivo do projeto (`.csproj`). O padrão é `1.0.0`.
* `--no-restore`: Impede a restauração automática dos pacotes NuGet após a criação do projeto. Se usado, será necessário executar manualmente `dotnet restore` após gerar o projeto.

Esses parâmetros permitem personalizar rapidamente o projeto inicial de acordo com as necessidades do microserviço.

## Exemplos de Uso

A seguir estão alguns exemplos de como usar o comando `dotnet new` com este template:

* Criar um microserviço básico com valores padrão:

  ```bash
  dotnet new mybank -n MeuServico
  ```
* Criar um microserviço sem banco de dados:

  ```bash
  dotnet new mybank -n MeuServico --database-type none
  ```
* Criar um microserviço usando PostgreSQL como banco de dados:

  ```bash
  dotnet new mybank -n MeuServico --database-type postgresql
  ```
* Criar um microserviço com adaptadores Kafka e RabbitMQ:

  ```bash
  dotnet new mybank -n MeuServico --use-kafka --use-rabbitmq
  ```
* Criar um microserviço desabilitando métricas:

  ```bash
  dotnet new mybank -n MeuServico --use-metrics false
  ```
* Criar um microserviço sem restauração automática de pacotes:

  ```bash
  dotnet new mybank -n MeuServico --no-restore
  ```

## Restauração de Pacotes

Por padrão, ao criar o projeto com `dotnet new`, o template executa automaticamente `dotnet restore` para baixar as dependências. Caso o parâmetro `--no-restore` seja utilizado, essa restauração será pulada. Neste caso, após a geração do projeto você deve executar manualmente:

```bash
dotnet restore
```

Esse comando restaurará todos os pacotes NuGet necessários antes de compilar ou executar o projeto.

## Autor

Este template foi desenvolvido por **Fabio Magalhaes**.
