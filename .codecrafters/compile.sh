#!/bin/sh
#
# This script is used to compile your program on CodeCrafters
#
# This runs before .codecrafters/run.sh
#
# Learn more: https://codecrafters.io/program-interface

set -e # Exit on failure

# Ensure the correct path is used for the .csproj file inside the src/Presentation folder
cd /app/src/Presentation  # Change to the src/Presentation directory
dotnet build --configuration Release --output /tmp/codecrafters-build-redis-csharp Presentation.csproj
