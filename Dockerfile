#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Kanbersky.Customer.Api/Kanbersky.Customer.Api.csproj", "Kanbersky.Customer.Api/"]
COPY ["Kanbersky.Customer.Core/Kanbersky.Customer.Core.csproj", "Kanbersky.Customer.Core/"]
COPY ["Kanbersky.Customer.Business/Kanbersky.Customer.Business.csproj", "Kanbersky.Customer.Business/"]
COPY ["Kanbersky.Customer.DAL/Kanbersky.Customer.DAL.csproj", "Kanbersky.Customer.DAL/"]
COPY ["Kanbersky.Customer.Entity/Kanbersky.Customer.Entity.csproj", "Kanbersky.Customer.Entity/"]
RUN dotnet restore "Kanbersky.Customer.Api/Kanbersky.Customer.Api.csproj"
COPY . .
WORKDIR "/src/Kanbersky.Customer.Api"
RUN dotnet build "Kanbersky.Customer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kanbersky.Customer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kanbersky.Customer.Api.dll"]