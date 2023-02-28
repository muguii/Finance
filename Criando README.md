# Finance
API REST de um sistema para realizar a gestão financeira pessoal, possibilitando a criação de usuário e posteriormente suas contas, receitas, despesas e categorias de renda e consumo.

# Tecnologias, arquiteturas e padrões utilizados
* Arquitetura limpa
* ASP.NET Core com .NET 6
* Entity Framework Core
* Padrão CQRS (Command and Query Responsibility Segregation)
* Padrão Repository
* Validação com FluentValidation
* Autenticação e Autorização com JWT (Json Web Token)
* Testes Unitários com xUnit, utilizando os padrões Given-When-Then e AAA (Arrange, Act and Assert)
* Paginação de dados
* Unit of Work

# Funcionalidades do Finance
* Cadastro, atualização e login de usuários
  * Usuário administrador com acesso à obtenção de todos os usuários e suas contas, receitas, despesas e categorias
* Arquivamento e desarquivamento de contas e categorias
* Cadastro, atualização, remoção e obtenção de contas, receitas, despesas e categorias
