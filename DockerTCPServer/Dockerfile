#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DockerTCPServer/DockerTCPServer.csproj", "DockerTCPServer/"]
RUN dotnet restore "DockerTCPServer/DockerTCPServer.csproj"
COPY . .
WORKDIR "/src/DockerTCPServer"
RUN dotnet build "DockerTCPServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerTCPServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
EXPOSE 12000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerTCPServer.dll"]