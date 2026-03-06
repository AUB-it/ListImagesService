FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

# Kopier projektfilen og restore
COPY ["ListImagesService.csproj", "./"]
RUN dotnet restore "ListImagesService.csproj"

# Kopier resten af koden og publish
COPY . . 
RUN dotnet publish "ListImagesService.csproj" -c Release -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app

# Kopier de færdige filer
COPY --from=build /app/published-app .

# Miljøvariabel skal matche koden 
ENV ImagePath=/srv/images

ENTRYPOINT ["dotnet", "ListImagesService.dll"]