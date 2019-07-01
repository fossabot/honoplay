FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Honoplay.AdminWebAPI/Honoplay.AdminWebAPI.csproj", "Honoplay.AdminWebAPI/"]
COPY ["Honoplay.Application/Honoplay.Application.csproj", "Honoplay.Application/"]
COPY ["Honoplay.Common/Honoplay.Common.csproj", "Honoplay.Common/"]
COPY ["Honoplay.Domain/Honoplay.Domain.csproj", "Honoplay.Domain/"]
COPY ["Honoplay.Persistence/Honoplay.Persistence.csproj", "Honoplay.Persistence/"]
COPY ["External/FluentValidatorJavascript/Src/FluentValidatorJavascript/FluentValidatorJavascript.csproj", "External/FluentValidatorJavascript/Src/FluentValidatorJavascript/"]

RUN dotnet restore "Honoplay.AdminWebAPI/Honoplay.AdminWebAPI.csproj"
COPY . .
WORKDIR /src/Honoplay.AdminWebAPI
RUN dotnet build "Honoplay.AdminWebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Honoplay.AdminWebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Honoplay.AdminWebAPI.dll"]