FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Basket.Web/Basket.Web.csproj ./Basket.Web/
RUN dotnet restore Basket.Web/Basket.Web.csproj
COPY . .
WORKDIR /src/Basket.Web
RUN dotnet build -c Release -o /app/build


FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/build .
CMD [ "dotnet", "Basket.Web.dll" ]

