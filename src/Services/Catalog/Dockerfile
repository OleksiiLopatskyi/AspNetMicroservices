FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY Catalog.API/Catalog.API.csproj ./Catalog.API/
RUN dotnet restore Catalog.API/Catalog.API.csproj
COPY . .
WORKDIR /src/Catalog.API
RUN dotnet build -c Release -o /app/build


FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/build .
CMD [ "dotnet", "Catalog.API.dll" ]

