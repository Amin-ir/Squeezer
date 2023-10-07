# Use the official ASP.NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:7.0

# Set the working directory
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy application code
COPY . .

# Build release version
RUN dotnet publish -c Release -o out

# Expose port 5234 for the app
EXPOSE 5234 

ENV ASPNETCORE_URLS=http://+:5234

# Set entry point to run the app when container starts
ENTRYPOINT ["dotnet", "out/Squeezer.dll"]