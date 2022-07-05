# Para rodar a aplicação

# TesteBancoMaster

Este projeto foi construido para a realização de um teste para a empresa [Banco Master](https://www.bancomaster.com.br/).

## O Desafio

O desafio pode ser visualizado clicando [aqui](https://github.com/MagnoBelloni/TesteBancoMaster/blob/main/readme-teste.md).

## Inicio

### Pre-requisitos:

.NET 6
SqlServer

### Como rodar a aplicação

```
Add-Migration MigrationInicial -StartupProject TesteBancoMaster.API

Update-Database -StartupProject TesteBancoMaster.API
```

Já existe um seed inicial para popular o banco

## Rotas

As rotas podem ser visualizadas no swagger acessando: https://localhost:5001/swagger/index.html

## Autor:

- **Magno Belloni** - [LinkedIn](https://www.linkedin.com/in/magnobelloni/)
