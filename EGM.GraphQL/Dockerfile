#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EGM.GraphQL/EGM.GraphQL.csproj", "EGM.GraphQL/"]
RUN dotnet restore "EGM.GraphQL/EGM.GraphQL.csproj"
COPY . .
WORKDIR "/src/EGM.GraphQL"
RUN dotnet build "EGM.GraphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EGM.GraphQL.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EGM.GraphQL.dll"]