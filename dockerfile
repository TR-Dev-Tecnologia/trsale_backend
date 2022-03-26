FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore

WORKDIR /app/TRSale.WebApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/TRSale.WebApi/out ./
ENV LANG pt_PT  
ENV LANGUAGE pt_PT  
ENV LC_ALL pt_PT

ENTRYPOINT ["dotnet", "TRSale.WebApi.dll"]