FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CleanArquiteture.WebAPI/CleanArquiteture.WebAPI.csproj", "CleanArquiteture.WebAPI/"]
COPY ["CleanArchiteture.Application/CleanArchiteture.Application.csproj", "CleanArchiteture.Application/"]
COPY ["CleanArchiteture.Domain/CleanArchiteture.Domain.csproj", "CleanArchiteture.Domain/"]
COPY ["CleanArchiteture.Persistence/CleanArchiteture.Persistence.csproj", "CleanArchiteture.Persistence/"]
RUN dotnet restore "./CleanArquiteture.WebAPI/CleanArquiteture.WebAPI.csproj"
COPY . .
WORKDIR "/src/CleanArquiteture.WebAPI"
RUN dotnet build "./CleanArquiteture.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./CleanArquiteture.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArquiteture.WebAPI.dll"]
