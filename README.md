# API Bancária

Uma API moderna e segura para operações bancárias, incluindo gerenciamento de contas, pagamentos PIX e autenticação.

## 📑 Sumário

- [Visão Geral](#visão-geral)
- [Estrutura do Projeto](#estrutura-geral)
- [Funcionalidades](#funcionalidades)
- [Arquitetura](#arquitetura)
- [Começando](#começando)
  - [Pré-requisitos](#pré-requisitos)
  - [Instalação](#instalação)
  - [Configuração](#configuração)
- [Documentação da API](#documentação-da-api)
  - [Autenticação](#autenticação)
  - [Operações de Conta](#operações-de-conta)
  - [Operações PIX](#operações-pix)
- [Segurança](#segurança)
- [Testes](#testes)
- [Desenvolvimento e Implantação](#desenvolvimento-e-implantação)
- [Licença](#licença)

## 🔍 Visão Geral

A API Bancária é uma API RESTful baseada em .NET Core projetada para operações bancárias. Ela utiliza uma abordagem de arquitetura limpa com foco em princípios de design orientado a domínio para garantir manutenibilidade, testabilidade e escalabilidade.

## Estrutura do Projeto

A estrutura do projeto é organizada da seguinte forma:

```
/POC-MyBank
│
├── /src/MyBank
│   ├── /Adapters
│   │   ├── /Inbound
│   │   │   └── /WebApi
│   │   │       └── /Bank
│   │   │           └── /Endpoints
│   │   └── /Outbound
│   │       ├── /Database
│   │       └── /Logging
│   ├── /Configurations
│   ├── /Domain
│   │   ├── /Core
│   │   └── /Services
│   └── /UseCases
│       └── /Accounts
│           └── /GetBalance
│
│
├── .dockerignore
├── .gitignore
├── LICENSE
└── README.md
```
### Descrição das Pastas

- **/src/MyBank**: Contém a lógica principal da aplicação, incluindo serviços, entidades e casos de uso.
  - **/Adapters**: Implementações de adaptadores que conectam a aplicação a diferentes interfaces externas, como APIs e bancos de dados.
    - **/Inbound**: Adaptadores que recebem solicitações externas, como chamadas de API.
    - **/Outbound**: Adaptadores que se comunicam com serviços externos, como bancos de dados e sistemas de logging.
  - **/Configurations**: Contém as configurações da aplicação, como definições de ambiente, parâmetros de inicialização e injeções de dependência.
  - **/Domain**: Contém a lógica de domínio da aplicação, incluindo entidades, serviços de domínio e exceções.
  - **/UseCases**: Implementações de casos de uso que representam as operações que a aplicação pode realizar, organizadas por domínio.

- **.dockerignore** e **.gitignore**: Arquivos de configuração para ignorar arquivos e pastas desnecessárias durante a construção de imagens Docker e commits no Git.

- **LICENSE**: Arquivo que contém a licença do projeto.

- **README.md**: Este arquivo, que contém informações sobre o projeto.

## ✨ Funcionalidades

- **Autenticação e Autorização**: Autenticação segura baseada em JWT
- **Gerenciamento de Contas**: Consulta de saldo e extrato
- **Segurança em Transações**: Autenticação de transações baseada em senha do cartão
- **Integração PIX**: Suporte ao sistema de pagamento instantâneo brasileiro
  - Gerenciamento de chaves PIX
  - Processamento instantâneo de pagamentos
- **Documentação Swagger**: Documentação interativa da API

## 🏗️ Fundamentos Utilizados

### SOLID

Os princípios SOLID são um conjunto de diretrizes que ajudam a criar sistemas de software mais compreensíveis, flexíveis e manuteníveis. Eles incluem:

- **S**: Single Responsibility Principle (Princípio da Responsabilidade Única)
- **O**: Open/Closed Principle (Princípio do Aberto/Fechado)
- **L**: Liskov Substitution Principle (Princípio da Substituição de Liskov)
- **I**: Interface Segregation Principle (Princípio da Segregação de Interfaces)
- **D**: Dependency Inversion Principle (Princípio da Inversão de Dependência)

### DDD (Domain-Driven Design)

O DDD é uma abordagem que foca na modelagem do domínio da aplicação. Ele promove a colaboração entre especialistas do domínio e desenvolvedores para criar um modelo que reflita a lógica de negócios.

### Clean Architecture

A Clean Architecture é um padrão que separa a lógica de negócios da infraestrutura, permitindo que a aplicação seja independente de frameworks e tecnologias. Isso facilita a manutenção e a evolução do sistema.

### Ports and Adapters

O padrão Ports and Adapters (também conhecido como Arquitetura Hexagonal) permite que a aplicação se comunique com o mundo exterior (como bancos de dados, APIs, etc.) através de portas e adaptadores. Isso promove a testabilidade e a flexibilidade.

### Arquitetura Hexagonal

A Arquitetura Hexagonal é uma forma de estruturar a aplicação de modo que a lógica de negócios esteja no centro, cercada por interfaces que permitem a comunicação com o exterior. Isso facilita a troca de implementações sem afetar a lógica central.

## Links Úteis

- [SOLID Principles - Medium](- [SOLID Principles](https://medium.com/desenvolvendo-com-paixao/o-que-%C3%A9-solid-o-guia-completo-para-voc%C3%AA-entender-os-5-princ%C3%ADpios-da-poo-2b937b3fc530)
- [SOLID Principles - Alura](https://www.alura.com.br/artigos/solid)
- [SOLID Principles - Video: Código Fonte](https://www.youtube.com/watch?v=mkx0CdWiPRA&t=108s)

- [Domain-Driven Design - Martin Fowler](https://martinfowler.com/tags/domain%20driven%20design.html)
- [Domain-Driven Design - FullCycle](https://fullcycle.com.br/domain-driven-design/)
- [Domain-Driven Design - Video: Eximia.co playlist](https://www.youtube.com/watch?v=2X9Q97u4tUg&list=PLkpjQs-GfEMN8CHp7tIQqg6JFowrIX9ve)

- [Clean Architecture - Medium](https://medium.com/@gabrielfernandeslemos/clean-architecture-uma-abordagem-baseada-em-princ%C3%ADpios-bf9866da1f9c)
- [Clean Architecture - FullCycle](https://fullcycle.com.br/o-que-e-clean-architecture/)
- [Clean Architecture - Video: Código Fonte](https://www.youtube.com/watch?v=ow8UUjS5vzU&t=4s)

- [Ports and Adapters](https://martinfowler.com/bliki/PortsAndAdapters.html)
- [Ports and Adapters](https://martinfowler.com/bliki/PortsAndAdapters.html)
- [Ports and Adapters](https://martinfowler.com/bliki/PortsAndAdapters.html)

- [Hexagonal Architecture](https://en.wikipedia.org/wiki/Hexagonal_architecture)
- [Hexagonal Architecture](https://en.wikipedia.org/wiki/Hexagonal_architecture)
- [Hexagonal Architecture](https://en.wikipedia.org/wiki/Hexagonal_architecture)

## 🏗️ Arquitetura

O projeto segue os princípios da Arquitetura Limpa, com separação clara de responsabilidades:

- **Camada de Domínio**: Regras de negócio principais, entidades e casos de uso
- **Camada de Aplicação**: Implementações de casos de uso e orquestração
- **Camada de Adaptadores**: Implementações de infraestrutura (banco de dados, serviços externos)
- **Camada de Entrada**: Controladores da API e endpoints

### Componentes Principais:

- **Padrão Mediator**: Para baixo acoplamento entre componentes
- **API Minimalista**: Utilizando a abordagem de API minimalista do ASP.NET Core para endpoints leves

##   Começando

### Pré-requisitos

- SDK do .NET 7.0 ou mais recente
- SQL Server (ou seu banco de dados preferido)
- Visual Studio 2022 ou JetBrains Rider (recomendado)

### Instalação

1. Clone o repositório:
   ```bash
   git clone https://github.com/luisfabiosm/POC-MyBank.git
   cd bank-api
   ```

2. Restaure as dependências:
   ```bash
   dotnet restore
   ```

3. Compile a solução:
   ```bash
   dotnet build
   ```
4. Execute a aplicação:
   ```bash
   dotnet run --project src/Adapters.Inbound.WebApi.Bank
   ```

### Configuração

A aplicação utiliza o sistema de configuração padrão do ASP.NET Core. As configurações principais estão no arquivo `appsettings.json`:

```json
{
    "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Jwt": {
    "Key": "suachaveaqui",
    "Issuer": "MyBank",
    "Audience": "MyBankClient"
  },
  "AllowedHosts": "*",
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    }
  }
}
```

## 📘 Documentação da API

A API está totalmente documentada usando Swagger. Quando executada em modo de desenvolvimento, acesse a interface Swagger em:
```
https://localhost:7294/swagger
```

### Dados Mocados

Usuário Cliente:

    Cpf = "97786149031",
    Name = "LUIZ NONO SILVA",
    AccessPassword = "07122526",
    CardPassword = "0321",
    PhoneNumber = "91985758797"


Conta 1:

    BankNumber = 1,
    AgencyNumber = 1,
    AccountNumber = "25202",
    Cpf = "97786149031",
    Balance = 10000.53m,
    PixKeys =
    {
     PixKey { Key = "97786149031", Type = Cpf },
     PixKey { Key = "91985758797", Type = Phone }
    }


Conta 2 (externa)

    BankNumber = 2,
    AgencyNumber = 21,
    AccountNumber = "2547719",
    Cpf = "62566610282",
    PixKeys = 
    {
     PixKey { Key = "62566610282", Type = Cpf },
     PixKey { Key = "91981155731", Type = Phone }
    }

### Autenticação

#### Login
```
POST /api/auth/login
```
Autentica um usuário e retorna um token JWT.

Requisição:
```json
{
  "cpf": "12345678900",
  "password": "senha123"
}
```

Resposta:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiration": "2023-12-31T23:59:59",
  "name": "João Silva",
  "cpf": "12345678900",
  "bankNumber": 1,
  "agencyNumber": 1234,
  "accountNumber": "123456-7",
  "correlationId": "550e8400-e29b-41d4-a716-446655440000"
}
```

### Operações de Conta

#### Consultar Saldo
```
GET /api/account/balance
```
Retorna o saldo atual da conta.

Requisição:
```json
{
  "bankNumber": 1,
  "agencyNumber": 1234,
  "accountNumber": "123456-7"
}
```

Resposta:
```json
{
  "agencyNumber": 1234,
  "accountNumber": "123456-7",
  "cpf": "12345678900",
  "name": "João Silva",
  "balance": 1540.75,
  "formattedBalance": "R$ 1.540,75",
  "lastUpdate": "2023-06-15T14:35:21",
  "correlationId": "550e8400-e29b-41d4-a716-446655440000"
}
```

#### Obter Extrato
```
GET /api/account/statement
```
Retorna o extrato da conta com histórico de transações.

#### Autenticar Transação
```
POST /api/account/authenticate-transaction
```
Autentica uma transação usando a senha do cartão.

### Operações PIX

#### Consultar Chaves PIX
```
GET /api/pix/keys
```
Retorna informações sobre uma chave PIX.

#### Iniciar Pagamento PIX
```
POST /api/pix/pay
```
Inicia uma transferência PIX.

## 🔒 Segurança

A API implementa várias medidas de segurança:

- **Autenticação JWT**: Autenticação segura baseada em tokens
- **HTTPS**: Todas as comunicações devem usar HTTPS
- **Políticas de Senha**: Requisitos de senhas fortes
- **Autenticação de Transações**: Verificação adicional para operações sensíveis
- **Validação de Entrada**: Validação abrangente de todas as entradas
- **Políticas CORS**: Controle de compartilhamento de recursos entre origens

## 🧪 Testes

O projeto inclui vários tipos de testes:

1. **Testes Unitários**: Testando componentes individuais isoladamente
   ```bash
   dotnet test tests/Domain.UnitTests
   ```

2. **Testes de Integração**: Testando interações entre componentes
   ```bash
   dotnet test tests/Domain.IntegrationTests
   ```

3. **Testes de API**: Testes de ponta a ponta da API
   ```bash
   dotnet test tests/Api.Tests
   ```

## 🛠️ Desenvolvimento e Implantação

### Ambiente de Desenvolvimento

Para desenvolvimento, use a configuração `appsettings.Development.json` que inclui:
- Log detalhado
- Interface Swagger habilitada
- Banco de dados em memória (opcional)

### Implantação em Produção

Para implantação em produção:
1. Atualize strings de conexão e configurações JWT
2. Garanta a configuração adequada de log
3. Configure verificações de saúde
4. Configure HTTPS com certificados válidos
5. Configure pipelines de CI/CD

```bash
# Comando de publicação de exemplo
dotnet publish -c Release -o ./publish
```

## 📄 Licença

Este projeto está licenciado sob a [Licença MIT](LICENSE).