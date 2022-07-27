FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Rossgram.WebApi/Rossgram.WebApi.csproj", "Rossgram.WebApi/"]
RUN dotnet restore "Rossgram.WebApi/Rossgram.WebApi.csproj"
COPY . .
WORKDIR "/src/Rossgram.WebApi"
RUN dotnet build "Rossgram.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rossgram.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rossgram.WebApi.dll"]
