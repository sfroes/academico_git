# Script de teste de build
Set-Location $PSScriptRoot
Write-Host "Diretório atual: $(Get-Location)" -ForegroundColor Cyan
Write-Host "Iniciando build do projeto administrativo..." -ForegroundColor Green
node node_modules/@angular/cli/bin/ng build --project=administrativo
Write-Host "`nBuild finalizado!" -ForegroundColor Green
