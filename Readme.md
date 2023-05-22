# .NET Challenge

## Description
A simple chat for .NET challenge

## Installation
```ps
dotnet restore
```
### Get docker images for RabbitMQ and MSSQL Server
```ps
docker pull rabbitmq
```
```ps
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

### Run the docker image for RabbitMQ and MSSQL Server
```ps
docker run -d --hostname rabbitmq --name local-rabbit -p 8080:15672 -p 5672:5672 -p 25676:25676 rabbitmq:3-management
```
```ps
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=VeRyStR0nG@P4sSw0rd" -p 7500:1433 --name sql1 --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest
```

### Compile
In the solution root
```ps
dotnet build
```

## Running
Open a terminal, bash or powershell conole
### Running the bot
Move to bot folder
```ps
cd .\SimpleChat.Bot\
```
```ps
dotnet run
```

### Running the Simple chat
open another terminal, bash or powershell in the solution root
```ps
cd .\SimpleChat\
```
If you don't have EF tools installed, it can be installed using the following command:
```ps
dotnet tool install --global dotnet-ef 
```
Now, update the database
```ps
dotnet ef database update 
```
and finally run the app
```ps
dotnet run
```

Good chatting.