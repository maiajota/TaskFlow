# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar o arquivo .csproj para restaurar as dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante dos arquivos e publicar o aplicativo
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copiar os arquivos publicados do container de build
COPY --from=build /app/out ./

# Definir o comando de entrada para rodar a aplicação
ENTRYPOINT ["dotnet", "TaskFlow.dll"]
