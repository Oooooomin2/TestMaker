#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TestMaker/TestMaker.csproj", "TestMaker/"]
COPY ["DDD.Domain/DDD.Domain.csproj", "DDD.Domain/"]
RUN dotnet restore "TestMaker/TestMaker.csproj"
COPY . .
WORKDIR "/src/TestMaker"
RUN dotnet build "TestMaker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestMaker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "TestMaker.dll"]