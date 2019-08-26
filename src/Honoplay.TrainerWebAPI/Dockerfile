FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Honoplay.TrainerWebAPI/Honoplay.TrainerWebAPI.csproj", "src/Honoplay.TrainerWebAPI/"]
RUN dotnet restore "src/Honoplay.TrainerWebAPI/Honoplay.TrainerWebAPI.csproj"
COPY . .
WORKDIR "/src/src/Honoplay.TrainerWebAPI"
RUN dotnet build "Honoplay.TrainerWebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Honoplay.TrainerWebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Honoplay.TrainerWebAPI.dll"]