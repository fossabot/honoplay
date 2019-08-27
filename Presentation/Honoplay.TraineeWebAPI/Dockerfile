FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Presentation/Honoplay.TraineeUserWebAPI/Honoplay.TraineeUserWebAPI.csproj", "Presentation/Honoplay.TraineeUserWebAPI/"]
RUN dotnet restore "Presentation/Honoplay.TraineeUserWebAPI/Honoplay.TraineeUserWebAPI.csproj"
COPY . .
WORKDIR "/src/Presentation/Honoplay.TraineeUserWebAPI"
RUN dotnet build "Honoplay.TraineeUserWebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Honoplay.TraineeUserWebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Honoplay.TraineeUserWebAPI.dll"]
