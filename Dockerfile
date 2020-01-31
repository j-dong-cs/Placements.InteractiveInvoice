FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /Placements.InteractiveInvoice

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Placements.InteractiveInvoice/*.csproj ./Placements.InteractiveInvoice/
RUN dotnet restore

# copy everything else and build app
COPY Placements.InteractiveInvoice/. ./Placements.InteractiveInvoice/
WORKDIR /Placements.InteractiveInvoice/Placements.InteractiveInvoice
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /Placements.InteractiveInvoice/Placements.InteractiveInvoice/out ./
# ENTRYPOINT ["dotnet", "Placements.InteractiveInvoice.dll"]
# Heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Placements.InteractiveInvoice.dll