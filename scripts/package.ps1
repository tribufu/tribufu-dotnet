#!/usr/bin/env pwsh

Get-ChildItem ./artifacts -Filter *.nupkg -Recurse -Force | Remove-Item -Force

dotnet clean
dotnet build -c Release
dotnet pack -c Release -o ./artifacts
