# Create a new container image
# Use offical dotnet core sdk
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-stage
WORKDIR /source

# COPY source destination
# Copy everything in current workdir to defined previous step
COPY . ./
WORKDIR /source/hanimyhoney.api
RUN dotnet publish -c Release -o /source/output

# Yeni bir container Image olusturalim
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime-stage
LABEL maintainer="remziyildirimytu@gmail.com" \
      description="Hani My Honey Project" \
      version="1.0.0"
WORKDIR /app
COPY --from=build-stage /source/output .

# Simdi portu disari acalim
EXPOSE 80
ENTRYPOINT ["dotnet","hanimyhoney.api.dll"]
# benim containerim calistiginda trigger eti ENTRYPOINT calistirir(CMD). 
# CMD [.....]
# VS Code console dan run etmek icin:
# Exp for build: docker build -t tagName .
# Exp for run  : docker run -d -p mapHostPortNumber:toContainerPortNumber --name dockerContainerName tagName

# docker build -t hanimyhoney .
# docker run -d -p 8080:80 --name my-wep-api-container hanimyhoney