FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Backend.Api/Backend.Api.csproj", "Backend.Api/"]
COPY ["src/Backend.Application/Backend.Application.csproj", "Backend.Application/"]
COPY ["src/Backend.Domain/Backend.Domain.csproj", "Backend.Domain/"]
COPY ["src/Backend.Common/Backend.Common.csproj", "Backend.Common/"]
COPY ["src/Backend.External/Backend.External.csproj", "Backend.External/"]
COPY ["src/Backend.Persistence/Backend.Persistence.csproj", "Backend.Persistence/"]

RUN dotnet restore "Backend.Api/Backend.Api.csproj"
COPY . ../
WORKDIR /src/Backend.Api
RUN dotnet build "Backend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.Api.dll"]