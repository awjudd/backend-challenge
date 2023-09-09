FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Challenge.UI.WebApi/Challenge.UI.WebApi.csproj", "Challenge.UI.WebApi/"]
RUN dotnet restore "Challenge.UI.WebApi/Challenge.UI.WebApi.csproj"
COPY . .
WORKDIR "/src/Challenge.UI.WebApi"
RUN dotnet build "Challenge.UI.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Challenge.UI.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Challenge.UI.WebApi.dll"]
