FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["test/event/NetCore.Event/NetCore.Event.csproj", "NetCore.Event/"]
RUN dotnet restore "test/event/NetCore.Event/NetCore.Event.csproj"
COPY . .
WORKDIR "/src/NetCore.Event"
RUN dotnet build "NetCore.Event.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCore.Event.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCore.Event.dll"]
