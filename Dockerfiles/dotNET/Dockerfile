# 1. .NET Core Application Image: Contains your .NET Core application.
   
   # Use an official .NET SDK image as a parent image
    FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

    # Set the working directory in the container
    WORKDIR /source

    # Copy the project file and restore dependencies
    COPY *.csproj ./
    RUN dotnet restore

    # Copy the remaining source code & publish
    COPY . .
    RUN dotnet publish -c Release -o /app

    # Build the runtime image
    FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
    WORKDIR /app
    COPY --from=build /app .

    # Start the application
    ENTRYPOINT ["dotnet", "eTickets.dll"]
