FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 61660
EXPOSE 44358

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FriendProximityAPI/FriendProximityAPI.csproj", "FriendProximityAPI/"]
RUN dotnet restore "FriendProximityAPI/FriendProximityAPI.csproj"
COPY . .
WORKDIR "/src/FriendProximityAPI"
RUN dotnet build "FriendProximityAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FriendProximityAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FriendProximityAPI.dll"]