# Script para corrigir imports do moment
$files = Get-ChildItem -Path "projects" -Recurse -Filter "*.ts" | Where-Object {
    (Get-Content $_.FullName -Raw) -match "import \* as moment from 'moment'"
}

foreach ($file in $files) {
    Write-Host "Corrigindo: $($file.FullName)"
    $content = Get-Content $file.FullName -Raw
    $newContent = $content -replace "import \* as moment from 'moment'", "import moment from 'moment'"
    Set-Content -Path $file.FullName -Value $newContent -NoNewline
    Write-Host "OK: $($file.Name)"
}

Write-Host "Concluído! $($files.Count) arquivos corrigidos."
