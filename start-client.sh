#!/bin/bash

echo "Starting Smart Career Platform Frontend..."

# Navigate to client directory
cd client

# Install dependencies
echo "Installing Node.js dependencies..."
npm install

# Start the development server
echo "Starting frontend development server on port 3000..."
npm run dev
