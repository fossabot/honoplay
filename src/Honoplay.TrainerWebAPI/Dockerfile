FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 81
EXPOSE 443
ENV REDIS_CACHE_SERVICE=honoplay_redis_cache_service

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Honoplay.TrainerWebAPI/Honoplay.TrainerWebAPI.csproj", "Honoplay.TrainerWebAPI/"]
COPY ["Honoplay.Application/Honoplay.Application.csproj", "Honoplay.Application/"]
COPY ["Honoplay.Common/Honoplay.Common.csproj", "Honoplay.Common/"]
COPY ["Honoplay.Domain/Honoplay.Domain.csproj", "Honoplay.Domain/"]
COPY ["Honoplay.Persistence/Honoplay.Persistence.csproj", "Honoplay.Persistence/"]
COPY ["Honoplay.FluentValidatorJavascript/Honoplay.FluentValidatorJavascript.csproj", "Honoplay.FluentValidatorJavascript/"]


RUN dotnet restore "Honoplay.TrainerWebAPI/Honoplay.TrainerWebAPI.csproj"
COPY . .
WORKDIR /src/Honoplay.TrainerWebAPI
RUN dotnet build "Honoplay.TrainerWebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Honoplay.TrainerWebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Honoplay.TrainerWebAPI.dll"]
