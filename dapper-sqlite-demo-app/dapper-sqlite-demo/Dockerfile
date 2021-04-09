#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["dapper-sqlite-demo/dapper-sqlite-demo.csproj", "dapper-sqlite-demo/"]
RUN dotnet restore "dapper-sqlite-demo/dapper-sqlite-demo.csproj"
COPY . .
WORKDIR "/src/dapper-sqlite-demo"
RUN dotnet build "dapper-sqlite-demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dapper-sqlite-demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dapper-sqlite-demo.dll"]