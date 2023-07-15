#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
#EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["BEComentarios.csproj", "."]
RUN dotnet restore "./BEComentarios.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BEComentarios.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BEComentarios.csproj" -c Release -o /app/publish /p:UseAppHost=false

#RUN dotnet publish "BEComentarios.csproj" -c Release -o /app/publish  \
    #--runtime alpine-x64 \
    #--self-contained true \
    #/p:PublishTrimmed=true \
    #/p:TrimMode=Link \
    #/p:PublishSingleFile=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BEComentarios.dll"]