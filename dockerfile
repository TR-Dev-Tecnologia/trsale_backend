FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

RUN dotnet dev-certs https --clean && \
    dotnet dev-certs https -t

EXPOSE 5000
EXPOSE 5001