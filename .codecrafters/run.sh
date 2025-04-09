#!/bin/sh
#
# This script is used to run your program on CodeCrafters
#
# This runs after .codecrafters/compile.sh
#
# Learn more: https://codecrafters.io/program-interface

set -e # Exit on failure

# Execute the built application (adjust if your executable name is different)
exec /tmp/codecrafters-build-redis-csharp/Presentation "$@"
