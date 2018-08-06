FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

COPY USchedule.Shared/. ./USchedule.Shared/
COPY USchedule.Parser/. ./USchedule.Parser/

RUN dotnet restore USchedule.Parser/USchedule.Parser.csproj
RUN dotnet build USchedule.Parser/USchedule.Parser.csproj

FROM build as publish
WORKDIR /app/USchedule.Parser
RUN dotnet publish -o out USchedule.Parser.csproj

FROM microsoft/dotnet:2.1-aspnetcore-runtime as runtime
WORKDIR /app
COPY --from=publish /app/USchedule.Parser/out ./
CMD dotnet USchedule.Parser.dll