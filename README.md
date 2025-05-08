# API Bancária

Uma API moderna e segura para operações bancárias, incluindo gerenciamento de contas, pagamentos PIX e autenticação.

## 📑 Sumário

- [Visão Geral](#visão-geral)
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

## ✨ Funcionalidades

- **Autenticação e Autorização**: Autenticação segura baseada em JWT
- **Gerenciamento de Contas**: Consulta de saldo e extrato
- **Segurança em Transações**: Autenticação de transações baseada em senha do cartão
- **Integração PIX**: Suporte ao sistema de pagamento instantâneo brasileiro
  - Gerenciamento de chaves PIX
  - Processamento instantâneo de pagamentos
- **Documentação Swagger**: Documentação interativa da API

## 🏗️ Arquitetura

O projeto segue os princípios da Arquitetura Limpa, com separação clara de responsabilidades:

- **Camada de Domínio**: Regras de negócio principais, entidades e casos de uso
- **Camada de Aplicação**: Implementações de casos de uso e orquestração
- **Camada de Adaptadores**: Implementações de infraestrutura (banco de dados, serviços externos)
- **Camada de Entrada**: Controladores da API e endpoints

### Componentes Principais:

- **Padrão Mediator**: Para baixo acoplamento entre componentes
- **API Minimalista**: Utilizando a abordagem de API minimalista do ASP.NET Core para endpoints leves
- **Entity Framework Core**: Para persistência de dados (se utilizado)

##   Começando

### Pré-requisitos

- SDK do .NET 7.0 ou mais recente
- SQL Server (ou seu banco de dados preferido)
- Visual Studio 2022 ou JetBrains Rider (recomendado)

### Instalação

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/bank-api.git
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

4. Atualize o banco de dados (se estiver usando migrações do Entity Framework):
   ```bash
   dotnet ef database update
   ```

5. Execute a aplicação:
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
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "JwtSettings": {
    "Key": "SuaChaveSuperSecretaAquiFiqueLongaEComplexaParaSerSegura",
    "Issuer": "BankAPI",
    "Audience": "BankAPIClients",
    "DurationInMinutes": 120
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=seu-servidor;Database=BankDB;User Id=seu-usuario;Password=sua-senha;"
  }
}
```

## 📘 Documentação da API

A API está totalmente documentada usando Swagger. Quando executada em modo de desenvolvimento, acesse a interface Swagger em:
```
https://localhost:5001/swagger
```

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