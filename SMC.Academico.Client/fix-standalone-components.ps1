# Script para remover standalone: true de componentes que estão em NgModules
# Isso corrige o erro TS-996008

Write-Host "Procurando componentes com standalone: true..." -ForegroundColor Cyan

$files = Get-ChildItem -Path "projects" -Recurse -Filter "*.ts" | Where-Object {
    $content = Get-Content $_.FullName -Raw
    $content -match "standalone:\s*true" -and ($content -match "@Component" -or $content -match "@Pipe")
}

Write-Host "Encontrados $($files.Count) arquivos com standalone: true`n" -ForegroundColor Yellow

$count = 0
foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw

    # Remover standalone: true e a vírgula seguinte (se existir)
    $newContent = $content -replace ",?\s*standalone:\s*true\s*,?", ""

    # Limpar vírgulas duplas que podem ter sobrado
    $newContent = $newContent -replace ",,", ","

    # Limpar vírgulas antes de fechar chaves
    $newContent = $newContent -replace ",\s*\}", "}"

    Set-Content -Path $file.FullName -Value $newContent -NoNewline
    $count++
    Write-Host "[$count/$($files.Count)] Corrigido: $($file.Name)" -ForegroundColor Green
}

Write-Host "`n✅ Concluído! $count arquivos corrigidos." -ForegroundColor Green
Write-Host "`nAgora execute 'npm run adm' novamente para testar." -ForegroundColor Cyan
