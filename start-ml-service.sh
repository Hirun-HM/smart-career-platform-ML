#!/bin/bash

echo "Starting Smart Career Platform ML Service..."

# Navigate to ML service directory
cd ml-service

# Check if virtual environment exists
if [ ! -d "venv" ]; then
    echo "Creating Python virtual environment..."
    python3 -m venv venv
fi

# Activate virtual environment
echo "Activating virtual environment..."
source venv/bin/activate

# Install dependencies
echo "Installing Python dependencies..."
pip install -r requirements.txt

# Create models directory if it doesn't exist
mkdir -p models

# Start the ML service
echo "Starting ML service on port 5001..."
python app.py
