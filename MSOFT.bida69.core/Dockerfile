FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["MSOFT.bida69.core/MSOFT.bida69.core.csproj", "MSOFT.bida69.core/"]
COPY ["MSOFT.BL/MSOFT.BL.csproj", "MSOFT.BL/"]
COPY ["MSOFT.DL/MSOFT.DL.csproj", "MSOFT.DL/"]
COPY ["MSOFT.Entities/MSOFT.Entities.csproj", "MSOFT.Entities/"]
COPY ["MSOFT.Common/MSOFT.Common.csproj", "MSOFT.Common/"]
RUN dotnet restore "MSOFT.bida69.core/MSOFT.bida69.core.csproj"
COPY . .
WORKDIR "/src/MSOFT.bida69.core"
RUN dotnet build "MSOFT.bida69.core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSOFT.bida69.core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSOFT.bida69.core.dll"]