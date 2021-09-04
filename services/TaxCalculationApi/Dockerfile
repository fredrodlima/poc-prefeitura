#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["services/TaxCalculationApi/TaxCalculationApi.csproj", "services/TaxCalculationApi/"]
RUN dotnet restore "services/TaxCalculationApi/TaxCalculationApi.csproj"
COPY . .
WORKDIR "/src/services/TaxCalculationApi"
RUN dotnet build "TaxCalculationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaxCalculationApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaxCalculationApi.dll"]