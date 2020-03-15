FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PZCheeseria.APi/PZCheeseria.Api.csproj", "PZCheeseria.APi/"]
COPY ["PZCheeseria.Persistence/PZCheeseria.Persistence.csproj", "PZCheeseria.Persistence/"]
COPY ["PZCheeseria.Domain/PZCheeseria.Domain.csproj", "PZCheeseria.Domain/"]
COPY ["PZCheeseria.Common/PZCheeseria.Common.csproj", "PZCheeseria.Common/"]
COPY ["PZCheeseria.Infrastructure/PZCheeseria.Infrastructure.csproj", "PZCheeseria.Infrastructure/"]
COPY ["PZCheeseria.BusinessLogic/PZCheeseria.BusinessLogic.csproj", "PZCheeseria.BusinessLogic/"]
RUN dotnet restore "PZCheeseria.APi/PZCheeseria.Api.csproj"
COPY . .
WORKDIR "/src/PZCheeseria.APi"
RUN dotnet build "PZCheeseria.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PZCheeseria.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PZCheeseria.Api.dll"]