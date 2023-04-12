# .NET Challenge

## Description
A simple chat for .NET challenge

## Installation
```sh
$ dotnet restore
```
### Get docker images for RabbitMQ and MSSQL Server
```sh
$ docker pull rabbitmq
```
```sh
$ sudo docker pull mcr.microsoft.com/mssql/server:2022-latest
```


### Run the docker image for RabbitMQ and MSSQL Server
```sh
$ docker run -d —-hostname rabbitmq -—name local-rabbit -p 8080:15672 -p 5672:5672 -p 25676:25676 rabbitmq:3-management
```
```sh
$ sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=VeRyStR0nG@P4sSw0rd" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest
```

### Compile
In the solution root
```sh
$ dotnet build
```

## Running
Open a terminal, bash or powershell conole
### Running the bot
Move to bot folder
```sh
$ cd .\SimpleChat.Bot\
```
```sh
$ dotnet run
```

### Running the Simple chat
open another terminal, bash or powershell in the solution root
```sh
$ cd .\SimpleChat\
```
If you don't have EF tools installed, it can be installed using the following command:
```sh
$ dotnet tool install --global dotnet-ef 
```
Now, update the database
```sh
$ dotnet ef database update 
```
and finally run the app
```sh
$ dotnet run
```

Good chatting.