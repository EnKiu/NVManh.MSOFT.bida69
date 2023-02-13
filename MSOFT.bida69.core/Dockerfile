#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MSOFT.bida69.core/MSOFT.bida69.core.csproj", "MSOFT.bida69.core/"]
COPY ["MSOFT.Common/MSOFT.Common.csproj", "MSOFT.Common/"]
COPY ["MSOFT.Core/MSOFT.Core.csproj", "MSOFT.Core/"]
COPY ["MSOFT.Entities/MSOFT.Entities.csproj", "MSOFT.Entities/"]
COPY ["MSOFT.Infrastructure/MSOFT.Infrastructure.csproj", "MSOFT.Infrastructure/"]
RUN dotnet restore "MSOFT.bida69.core/MSOFT.bida69.core.csproj"
COPY . .
WORKDIR "/src/MSOFT.bida69.core"
RUN dotnet build "MSOFT.bida69.core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSOFT.bida69.core.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSOFT.bida69.core.dll"]