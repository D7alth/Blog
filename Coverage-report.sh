#!/bin/bash

# Directories
OUTPUT_DIR="./coverage"
TIMESTAMP=$(date +"%Y%m%d_%H%M%S")
REPORT_FILE="$OUTPUT_DIR/Data.opencover.$TIMESTAMP.xml"

mkdir -p "$OUTPUT_DIR"

# Tests with Coverage
dotnet test "src/Tests/Blog.Tests.csproj" \
    -p:CollectCoverage=true \
    -p:CoverletOutputFormat=opencover \
    -p:CoverletOutput="../../$REPORT_FILE" || exit 1

# Report 
reportgenerator \
    "-reports:$REPORT_FILE" \
    "-targetdir:$OUTPUT_DIR/coverage-report" \
    -reporttypes:HtmlInline_AzurePipelines || exit 1

echo "Report generated at: $TIMESTAMP"
