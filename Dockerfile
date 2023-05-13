# build process
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /build
COPY src/Hotel.API/*.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish src/Hotel.API -c release -o /source
# runtime process
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /source .
ENTRYPOINT [ "dotnet", "Hotel.API.dll" ]
