#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0
EXPOSE 5000
EXPOSE 5001
COPY [".", "/app"]
COPY ["./Https", "/root/.aspnet/https/"]
COPY ["./DataProtection-Keys", "/root/.aspnet/DataProtection-Keys"]
WORKDIR /app
ENTRYPOINT ["dotnet", "watch", "run"]