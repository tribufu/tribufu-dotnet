#!/usr/bin/env pwsh

Get-ChildItem ./releases -Filter *.nupkg -Recurse -Force | Remove-Item -Force

dotnet clean
dotnet build -c Release
dotnet pack -c Release -o ./releases
