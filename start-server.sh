#!/bin/bash

echo "Starting Smart Career Platform Backend Server..."

# Navigate to server directory
cd server

# Restore NuGet packages
echo "Restoring NuGet packages..."
dotnet restore

# Build the project
echo "Building the project..."
dotnet build

# Start the server
echo "Starting backend server on port 5000..."
dotnet run
