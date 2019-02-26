FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
ENV APP_CONFIGURATION=Release
ENV ASPNETCORE_ENVIRONMENT=Production

WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet build -c ${APP_CONFIGURATION} -o /app

FROM build AS publish
RUN dotnet publish -c ${APP_CONFIGURATION} -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gerenciador_Financeiro.dll"]
