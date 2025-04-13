$logFile = Join-Path $PSScriptRoot "clean.log"
$slnFiles = Get-ChildItem -Path $PSScriptRoot -Filter *.sln -File

if (-not $slnFiles) {
    "No .sln file found." | Tee-Object -FilePath $logFile -Append
    exit
}

$projectPath = $slnFiles[0].Directory.FullName
"Script started at $(Get-Date)" | Tee-Object -FilePath $logFile -Append
"Project path: $projectPath" | Tee-Object -FilePath $logFile -Append

$foldersToDelete = @('.vs', 'bin', 'obj')

Get-ChildItem -Path $projectPath -Recurse -Directory | 
    Where-Object { $foldersToDelete -contains $_.Name } | 
    ForEach-Object {
        try {
            $folderPath = $_.FullName
            "Trying to delete: $folderPath" | Tee-Object -FilePath $logFile -Append
            
            # Проверка блокировки файлов
            $lockedFiles = Get-ChildItem -Path $folderPath -Recurse -File | 
                Where-Object { -not $_.CanWrite }
            
            if ($lockedFiles) {
                "Locked files detected in: $folderPath" | Tee-Object -FilePath $logFile -Append
                $lockedFiles | ForEach-Object { " - Locked: $($_.FullName)" }
            }
            
            Remove-Item $folderPath -Recurse -Force -ErrorAction Stop
            "Successfully deleted: $folderPath" | Tee-Object -FilePath $logFile -Append
        } 
        catch {
            "ERROR deleting $folderPath`: $($_.Exception.Message)" | Tee-Object -FilePath $logFile -Append
        }
    }

"Script ended at $(Get-Date)" | Tee-Object -FilePath $logFile -Append
Read-Host "Press Enter to exit"