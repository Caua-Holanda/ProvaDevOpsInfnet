FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ProvaMedGroup.sln .
COPY ProvaMedGroup/*.csproj ./ProvaMedGroup/
COPY ProvaMedGroup.DomainModel/*.csproj ./ProvaMedGroup.DomainModel/
COPY ProvaMedGroup.DomainService/*.csproj ./ProvaMedGroup.DomainService/
COPY ProvaMedGroup.Infra/*.csproj ./ProvaMedGroup.Infra/
COPY Tests.MedGroup/*.csproj ./Tests.MedGroup/

RUN dotnet restore

COPY . .

RUN dotnet test Tests.MedGroup/Tests.MedGroup.csproj --logger "trx" --results-directory /testresults

WORKDIR /src/ProvaMedGroup
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ProvaMedGroup.dll"]