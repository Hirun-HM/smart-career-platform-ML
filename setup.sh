#!/bin/bash

echo "🚀 Smart Career Platform - Complete Setup & Run Guide"
echo "======================================================"

# Function to check if a command exists
command_exists() {
    command -v "$1" >/dev/null 2>&1
}

# Check prerequisites
echo "📋 Checking prerequisites..."

if ! command_exists python3; then
    echo "❌ Python 3 is not installed. Please install Python 3.8 or higher."
    exit 1
fi

if ! command_exists node; then
    echo "❌ Node.js is not installed. Please install Node.js 18 or higher."
    exit 1
fi

if ! command_exists dotnet; then
    echo "❌ .NET SDK is not installed. Please install .NET 8 SDK."
    exit 1
fi

echo "✅ All prerequisites are installed!"

# Setup and run ML service
echo ""
echo "🤖 Setting up ML Service..."
cd ml-service

# Create virtual environment if it doesn't exist
if [ ! -d "venv" ]; then
    echo "📦 Creating Python virtual environment..."
    python3 -m venv venv
fi

# Activate virtual environment
echo "🔄 Activating virtual environment..."
source venv/bin/activate

# Install dependencies
echo "📥 Installing Python dependencies..."
pip install -r requirements.txt

# Create necessary directories
mkdir -p models
mkdir -p data

echo "✅ ML Service setup complete!"
echo ""

# Setup backend
echo "🔧 Setting up Backend Server..."
cd ../server

# Restore packages
echo "📦 Restoring NuGet packages..."
dotnet restore

# Build project
echo "🔨 Building project..."
dotnet build

if [ $? -ne 0 ]; then
    echo "❌ Backend build failed!"
    exit 1
fi

echo "✅ Backend setup complete!"
echo ""

# Setup frontend
echo "🎨 Setting up Frontend..."
cd ../client

# Install dependencies
echo "📦 Installing Node.js dependencies..."
npm install

echo "✅ Frontend setup complete!"
echo ""

echo "🎯 Setup completed successfully!"
echo ""
echo "Next steps:"
echo "1. Configure your Coursera API credentials in server/appsettings.json"
echo "2. Run the services using the startup scripts:"
echo "   - ./start-ml-service.sh (Terminal 1)"
echo "   - ./start-server.sh (Terminal 2)" 
echo "   - ./start-client.sh (Terminal 3)"
echo ""
echo "🌐 Once running, access the application at: http://localhost:3000"
