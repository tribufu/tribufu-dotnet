#!/usr/bin/env pwsh

$ErrorActionPreference = "Stop"

$env:CSHARP_POST_PROCESS_FILE = ""

java -jar ./vendor/openapi-generator/openapi-generator-cli.jar generate `
    -i https://api.tribufu.com/openapi.json `
    -g csharp `
    -o . `
    --global-property apis,models,supportingFiles,apiDocs=false,modelDocs=false,apiTests=false,modelTests=false `
    --additional-properties=packageName=Tribufu,library=restsharp,targetFramework=net47,zeroBasedEnums=true `
    --openapi-normalizer SET_TAGS_FOR_ALL_OPERATIONS=TribufuGenerated `
    --skip-validate-spec

$srcDir = "./src"
$sourceDir = "./Source"

if (Test-Path $srcDir) {
    New-Item -ItemType Directory -Force -Path $sourceDir | Out-Null
    Copy-Item -Path (Join-Path $srcDir "*") -Destination $sourceDir -Recurse -Force
    Remove-Item $srcDir -Recurse -Force
}
