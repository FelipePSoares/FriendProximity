FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY FriendProximityAPI/FriendProximityAPI.csproj FriendProximityAPI/
COPY FriendProximityAPI.Domain/FriendProximityAPI.Domain.csproj FriendProximityAPI.Domain/
COPY FriendProximityAPI.Shared/FriendProximityAPI.Shared.csproj FriendProximityAPI.Shared/
COPY FriendProximityAPI.Infrastructure/FriendProximityAPI.Infrastructure.csproj FriendProximityAPI.Infrastructure/
RUN dotnet restore FriendProximityAPI/FriendProximityAPI.csproj
COPY . .
WORKDIR /src/FriendProximityAPI
RUN dotnet build FriendProximityAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish FriendProximityAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FriendProximityAPI.dll"]
