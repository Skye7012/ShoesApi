#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ShoesApi.Api/ShoesApi.Api.csproj", "src/ShoesApi.Api/"]
COPY ["src/ShoesApi.Contracts/ShoesApi.Contracts.csproj", "src/ShoesApi.Contracts/"]
COPY ["src/ShoesApi.Infrastructure/ShoesApi.Infrastructure.csproj", "src/ShoesApi.Infrastructure/"]
COPY ["src/ShoesApi.Application/ShoesApi.Application.csproj", "src/ShoesApi.Application/"]
COPY ["src/ShoesApi.Domain/ShoesApi.Domain.csproj", "src/ShoesApi.Domain/"]
RUN dotnet restore "src/ShoesApi.Api/ShoesApi.Api.csproj"
COPY . .
WORKDIR "/src/src/ShoesApi.Api"
RUN dotnet build "ShoesApi.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShoesApi.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoesApi.Api.dll"]