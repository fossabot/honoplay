FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 82
EXPOSE 443
ENV REDIS_CACHE_SERVICE=honoplay_redis_cache_service

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Honoplay.TraineeUserWebAPI/Honoplay.TraineeUserWebAPI.csproj", "Honoplay.TraineeUserWebAPI/"]
COPY ["Honoplay.Application/Honoplay.Application.csproj", "Honoplay.Application/"]
COPY ["Honoplay.Common/Honoplay.Common.csproj", "Honoplay.Common/"]
COPY ["Honoplay.Domain/Honoplay.Domain.csproj", "Honoplay.Domain/"]
COPY ["Honoplay.Persistence/Honoplay.Persistence.csproj", "Honoplay.Persistence/"]
COPY ["Honoplay.FluentValidatorJavascript/Honoplay.FluentValidatorJavascript.csproj", "Honoplay.FluentValidatorJavascript/"]


RUN dotnet restore "Honoplay.TraineeUserWebAPI/Honoplay.TraineeUserWebAPI.csproj"
COPY . .
WORKDIR /src/Honoplay.TraineeUserWebAPI
RUN dotnet build "Honoplay.TraineeUserWebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Honoplay.TraineeUserWebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Honoplay.TraineeUserWebAPI.dll"]
