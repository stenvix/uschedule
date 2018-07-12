FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

COPY *.sln  .
COPY USchedule.API/. ./USchedule.API/
COPY USchedule.Core/. ./USchedule.Core/
COPY USchedule.Persistence/. ./USchedule.Persistence/
COPY USchedule.Models/. ./USchedule.Models/
COPY USchedule.Domain/. ./USchedule.Domain/
COPY USchedule.Services/. ./USchedule.Services/

RUN dotnet restore
RUN dotnet build

FROM build as publish
WORKDIR /app/USchedule.API
RUN dotnet publish -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime as runtime
WORKDIR /app
COPY --from=publish /app/USchedule.API/out ./
CMD dotnet USchedule.API.dll