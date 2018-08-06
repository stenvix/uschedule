# Development
## Required
Install .NET core sdk v2.1

Install one of RDBMS:
* PostgreSQL
* MS SQL Server
* MySQL

## Optional
Install docker and docker-compose for local development enviroment

## Common comands
Build and run project:
```
dotnet restore <ProjectName>.csproj
dotnet build <ProjectName>.csproj
```
Build and run whole project in docker:
```
docker-compose build
docker-compose up
```
Run only database in docker:
```
docker-compose run --service-ports db 
```