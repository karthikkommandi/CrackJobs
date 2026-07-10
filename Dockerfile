# ===========================
# Build Stage
# ===========================

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore CrackJobs.csproj

RUN dotnet publish CrackJobs.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false


# ===========================
# Runtime Stage
# ===========================

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet","CrackJobs.dll"]