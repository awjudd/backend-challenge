FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Challenge.UI.Console/Challenge.UI.Console.csproj", "Challenge.UI.Console/"]
RUN dotnet restore "Challenge.UI.Console/Challenge.UI.Console.csproj"
COPY . .
WORKDIR "/src/Challenge.UI.Console"
RUN dotnet build "Challenge.UI.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Challenge.UI.Console.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Challenge.UI.Console.dll"]
