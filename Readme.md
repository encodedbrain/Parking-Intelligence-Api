# Parking Intelligence

`The WEX Bootcamp in the DIO.io platform proposes a project to build a system to monitor and manage a parking lot.`

 - O desafio requer que o desenvolvedor(a), desenvolva um gerenciador de estacionamento , com funcionalidades como por exemplo adicionar um veículo, remover um veículo (e exibir o valor cobrado durante o período) e listar os veículos e correlacionados 
 -  Esta API conterá altentificação de rotas, como também de login de usuário , cadastramento de novos veículos , pesquisa sobre vagas disponíveis em estacionamentos próximos, além de parte financeira que ira lidar com compras , valores das vagas 
 -  Também irá conter um mapa que irá ter a responsabilidade de orientar o cliente em quais regiões ele poderá se utilizar da vaga em estacionamentos próximos , optei pelo Angular para a parte de frontend, pois ele é um framework robusto e é muito utilizado quando falamos de desenvolvimento web se utilizando da linguagem c# , também é comum vermos o Angular em projetos onde se é utilizado o Java , o Angular presa muito pelo paradigma da orientação a objetos , é a melhor opção do mercado entre os frameworks mais populares quando se falamos de escalabilidade,o projeto é pequeno mas ao decorrer do tempo se tornará maior, pois a minha intenção é ir adicionando novas funcionalidades, então o angular é a melhor opção para essa solução em especifico
 - A parte de altentificação terá a responsabilidade de monitorar o usuário logado e verificar se o mesmo tem a permissão de acessar determinada rota ou efetuar determinada ação como por exemplo a deleção de algum veiculo cadastrado, para gerar mais segurança irei implementar um método que irar ser responsável de efetuar o refresh token , a cada determinado periodo será gerado um novo token para o usuário continuar navegando com segurança em sua conta .
## como testar de forma local:

**requisitos:**
- .net 7.0
- sqlServer

- clone o repositorio:
```git clone git@github.com:encodedbrain/Parking-Intelligence-Api.git```
## acessando a pasta e instalando as depêndencias: 

- acesse a pasta do projeto: 
```cd pasta_do_projeto```
- instale as dependências:
``` dotnet restore```
- inicie o servidor:
```dotnet run ou dotnet watch run```

### Contribuições: 

Estas são as instruções sobre como contribuir para o projeto:

1. faça um fork do projeto.
2. Crie um branch para suas alterações.
3. Faça suas alterações.
4. Confirme suas alterações.
5. Envie suas alterações para seu branch.
6. Abra uma solicitação pull.

## Licença

Este projeto está licenciado sob a licença MIT.

## Contato

Se você tiver alguma dúvida, entre em contato comigo em [marco](marcodmc0101@gmail.com).

