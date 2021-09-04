#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["services/GeographiesApi/GeographiesApi.csproj", "services/GeographiesApi/"]
RUN dotnet restore "services/GeographiesApi/GeographiesApi.csproj"
COPY . .
WORKDIR "/src/services/GeographiesApi"
RUN dotnet build "GeographiesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeographiesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeographiesApi.dll"]