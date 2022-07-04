#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TesteBancoMaster.API/TesteBancoMaster.API.csproj", "TesteBancoMaster.API/"]
RUN dotnet restore "TesteBancoMaster.API/TesteBancoMaster.API.csproj"
COPY . .
WORKDIR "/src/TesteBancoMaster.API"
RUN dotnet build "TesteBancoMaster.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TesteBancoMaster.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TesteBancoMaster.API.dll"]