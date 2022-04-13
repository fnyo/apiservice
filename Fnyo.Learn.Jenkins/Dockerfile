#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Fnyo.Learn.Jenkins/Fnyo.Learn.Jenkins.csproj", "Fnyo.Learn.Jenkins/"]
COPY ["Fnyo.Learn.Service/Fnyo.Learn.Service.csproj", "Fnyo.Learn.Service/"]
RUN dotnet restore "Fnyo.Learn.Jenkins/Fnyo.Learn.Jenkins.csproj"
COPY . .
WORKDIR "/src/Fnyo.Learn.Jenkins"
RUN dotnet build "Fnyo.Learn.Jenkins.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fnyo.Learn.Jenkins.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fnyo.Learn.Jenkins.dll"]