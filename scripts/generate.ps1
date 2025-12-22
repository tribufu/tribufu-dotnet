#!/usr/bin/env pwsh

$env:CSHARP_POST_PROCESS_FILE = ""

java -jar ./vendor/openapi-generator/openapi-generator-cli.jar generate `
    -i https://api.tribufu.com/openapi.json `
    -g csharp `
    -o . `
    --global-property apis,models,supportingFiles,apiDocs=false,modelDocs=false,apiTests=false,modelTests=false `
    --additional-properties=packageName=Tribufu,library=restsharp,targetFramework=net47,zeroBasedEnums=true `
    --openapi-normalizer SET_TAGS_FOR_ALL_OPERATIONS=TribufuGenerated `
    --skip-validate-spec
