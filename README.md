## Sobre o projeto

Esta API, desenvolvida utilizando **.NET 8.0**, adota os princípios do **Domain-Drive Design (DDD)** para oferecer uma solução estruturada e eficaz no gerenciamento de despesas pessoais. O principal objetivo é permitir que os usuários registrem suas despesas, detalhando informações como título, data e hota, descrição, valor e tipo de pagamento, com os dados sendo armazenados de forma segura em um banco de dados **MySQL**.

A arquitetura da **API** baseia-se em **REST**, utilizando métodos **HTTP** padrão para uma comunicação eficiente e simplificada. Além disso, é complementada por uma documentação **Swagger**, que proporciona uma interfacegráfica interativa para os desenvolvedores possam explorar e testar os endpoints de maneira fácil.

Dentre os pacotes NuGet utilizados, o **AutoMapper** é o responsável pelo mapeamento entre os objetos de domínio e requisição/resposta, reduzindo a necessidade de código repetitivo e manual. O **FluentAssertions** é utilizado no testes de unidade para tomar as verificações mais legíveis, ajudando a escrever testes claros e compreensiveis. Para as validações, o **FluentValidation** é usado para implementar regras de validação  de forma simples e intuitiva nas classes de requisições, mantendo o código limpo e fácil de manter. Por fim, o **EntityFramework** atua como um ORM (Object-Relacional Mapper) que simplifica as interações com o banco de dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessidade de lidar com consultas SQL.

![hero-image]

### Features

- **Domain-Driven Design (DDD)**: Estrutura modular que facilita o entendimento e a manutenção de domínio da aplicação.
- **Testes de Unidade**: Testes Abrangentes com FluentAssertions para garantir a funcionalidade e a qualidade.
- **Geração de Relatórios**: Capacidade de exportar relatórios detalhados para **PDF e Excel**, oferecendo uma anánlise visual e eficaz das despesas.
- **RESTful API com Documentação Swagger**: Interface documentada que facilita a integração e o teste por parte dos desenvolvedores.

### Construído com

![badge-dot-net]
![badge-windows]
![badge-visual-studio]
![badge-mysql]
![badge-swagger]

## Getting Started

Para obter uma cópia local funcionando, siga estes passos simples.

### Requisitos

* Visual studio versão 2022+ ou Visual Studio Code
* Windowns 10+ ou Linux/MacOs com [.NET SDK][dot-net-sdk] instalado
* MySql Server

### Instalação

1. Clone o repositorio:
    ```sh
    gitclone https://github.com/MateusFernanndo/CashFlow.git
    ```
    
2. Preencha as informações no arquivo `appsettings.Development.json`.
3. Execute a API e aproveite o seu teste :)



<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

<!-- Image -->
[hero-image]: image/heroimage.png

<!-- Badges -->
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=flat-square
[badge-windows]: https://img.shields.io/badge/Windowns-blue?style=flat-square&logo=Windows
[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-purple?style=flat-square
[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=flat-square
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=flat-square

