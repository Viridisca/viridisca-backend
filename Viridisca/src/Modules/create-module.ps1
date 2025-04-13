# Обработка прерывания
trap {
    Write-Host "`nСкрипт прерван пользователем." -ForegroundColor Yellow
    exit
}

$scriptDirectory = $PSScriptRoot
$solutionPath = Join-Path -Path $scriptDirectory -ChildPath "..\..\Viridisca.sln"

# Основной цикл
while($true) {
    try {
        # Получение списка доступных модулей
        $availableModules = Get-ChildItem -Path $scriptDirectory -Directory | 
                          Select-Object -ExpandProperty Name
        
        # Запрос имени модуля с отслеживанием прерывания
        Write-Host "`nДоступные модули: $($availableModules -join ', ')" -ForegroundColor Cyan
        Write-Host "Нажмите ESC или Ctrl+C для выхода`n" -ForegroundColor DarkGray
        
        # Чтение ввода с обработкой клавиш
        $inputBuffer = ""
        :inputLoop while($true) {
            $key = [Console]::ReadKey($true)
            
            if($key.Key -eq [ConsoleKey]::Escape) {
                Write-Host "`nЗавершение работы..." -ForegroundColor Yellow
                exit
            }
            elseif($key.Key -eq [ConsoleKey]::Enter) {
                break inputLoop
            }
            elseif($key.Key -eq [ConsoleKey]::Backspace) {
                if($inputBuffer.Length -gt 0) {
                    $inputBuffer = $inputBuffer.Substring(0, $inputBuffer.Length - 1)
                    Write-Host "`b `b" -NoNewline
                }
            }
            else {
                $inputBuffer += $key.KeyChar
                Write-Host $key.KeyChar -NoNewline
            }
        }
        
        # Проверка на пустой ввод
        if([string]::IsNullOrWhiteSpace($inputBuffer)) {
            Write-Host "`nЗавершение работы..." -ForegroundColor Yellow
            exit
        }
        
        $moduleName = $inputBuffer.Trim()
        
        # Проверка существования папки модуля
        $modulePath = Join-Path -Path $scriptDirectory -ChildPath $moduleName
        if (-Not (Test-Path $modulePath)) {
            Write-Host "Модуль '$moduleName' не найден!" -ForegroundColor Red
            continue
        }

        # Создание проектов
        $libraries = @("Application", "Domain", "Infrastructure", "Presentation")
        foreach ($lib in $libraries) {
            $projectName = "Viridisca.Modules.$moduleName.$lib"
            $projectDir = Join-Path -Path $modulePath -ChildPath $projectName
            
            New-Item -Path $projectDir -ItemType Directory -Force | Out-Null
            
            $classContent = "namespace $projectName { public class ${lib}Class { } }"
            Set-Content -Path "$projectDir\$lib.cs" -Value $classContent -Encoding UTF8

            $csprojContent = @"
<Project Sdk="Microsoft.NET.Sdk">
</Project>
"@
            Set-Content -Path "$projectDir\$projectName.csproj" -Value $csprojContent -Encoding UTF8
            dotnet sln $solutionPath add "$projectDir\$projectName.csproj"
        }
        
        Write-Host "`nПроекты модуля '$moduleName' успешно созданы!`n" -ForegroundColor Green
    }
    catch {
        Write-Host "`nОшибка: $_" -ForegroundColor Red
    }
}