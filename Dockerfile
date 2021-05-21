#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0
EXPOSE 5000
COPY [".", "/app"]
#COPY ["./Https", "/root/.aspnet/https/"]
#COPY ["./DataProtection-Keys", "/root/.aspnet/DataProtection-Keys"]
WORKDIR /app
RUN dotnet add package Microsoft.EntityFrameworkCore.Design
RUN dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install --global dotnet-ef
#RUN dotnet ef migrations add InitialCreateToPostgreSQL
#RUN dotnet ef database update
ENTRYPOINT ["dotnet", "watch", "run"]