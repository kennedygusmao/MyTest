# MyTest

# Logs da aplicação

https://kisslog.net
usuário:emailparateste.dotnet@gmail.com 
senha:teste@123

# Necessário
Baixar o SDK .Net Core versão 2.2.0 


https://dotnet.microsoft.com/download/dotnet-core

Baixar o git para executar o clone da branche
https://git-scm.com/downloads

# Executar o projeto
baixar o projeto no seguinte repositório
https://github.com/kennedygusmao/MyTest.git

## Exemplo
Este processo é para executar o projeto MT.Api
Abra o Prompt de Comando

```sh
$ cd C:\Users\Usuario\source
$ git clone https://github.com/kennedygusmao/MyTest.git
$ cd MyTest
$ cd src\MT.Api
$ dotnet build
$ dotnet run
```
exemplo para visualizar o projeto MT.Api
https://localhost:5001/swagger/index.html

Executar o seguinte processo para o projeto MT.Web
Exemplo
```sh
$ cd MyTest
$ cd src\MT.Web
$ dotnet build
$ dotnet run
```
exemplo para visualizar o projeto MT.Web
https://localhost:5004/

# Executar os testes
Exemplo
```sh
$ cd MyTest
$ cd src\MT.Test
$ dotnet build
$ dotnet test /p:CollectCoverage=true
```

# Resultado dos testes

Execução de Teste Bem-sucedida.
Total de testes: 8
     Aprovados: 8
Tempo total: 3,6958 Segundos

Calculating coverage result...
 
+------------+--------+--------+--------+
| Module     | Line   | Branch | Method |
+------------+--------+--------+--------+
| MT.Data    | 31,64% | 41,66% | 80,76% |
+------------+--------+--------+--------+
| MT.Domain  | 88,57% | 100%   | 82,6%  |
+------------+--------+--------+--------+
| MT.Service | 83,23% | 61,11% | 95,23% |
+------------+--------+--------+--------+

+---------+--------+--------+--------+
|         | Line   | Branch | Method |
+---------+--------+--------+--------+
| Total   | 52,48% | 58%    | 85,71% |
+---------+--------+--------+--------+
| Average | 67,81% | 67,58% | 86,19% |
+---------+--------+--------+--------+
