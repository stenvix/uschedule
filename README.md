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
####Build and run
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
####Migrations
Add new migration
```
 dotnet ef migrations add "Name" --startup-project USchedule.API --project USchedule.Persistence
```
Remove latest migration
```
 dotnet ef migrations remove --startup-project USchedule.API --project USchedule.Persistence
```
Update existing database
```
dotnet ef database update  --startup-project USchedule.API --project USchedule.Persistence
```