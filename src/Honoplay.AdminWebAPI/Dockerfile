FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["Honoplay.AdminWebAPI/Honoplay.AdminWebAPI.csproj", "Honoplay.AdminWebAPI/"]
COPY ["Honoplay.Application/Honoplay.Application.csproj", "Honoplay.Application/"]
COPY ["Honoplay.Common/Honoplay.Common.csproj", "Honoplay.Common/"]
COPY ["Honoplay.Domain/Honoplay.Domain.csproj", "Honoplay.Domain/"]
COPY ["Honoplay.Persistence/Honoplay.Persistence.csproj", "Honoplay.Persistence/"]

RUN dotnet restore "Honoplay.AdminWebAPI/Honoplay.AdminWebAPI.csproj"
COPY . .
WORKDIR "/src/Honoplay.AdminWebAPI"
RUN dotnet build "Honoplay.AdminWebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Honoplay.AdminWebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Honoplay.AdminWebAPI.dll"]