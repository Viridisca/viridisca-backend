# ��������� ����������
trap {
    Write-Host "`n������ ������� �������������." -ForegroundColor Yellow
    exit
}

$scriptDirectory = $PSScriptRoot
$solutionPath = Join-Path -Path $scriptDirectory -ChildPath "..\..\Viridisca.sln"

# �������� ����
while($true) {
    try {
        # ��������� ������ ��������� �������
        $availableModules = Get-ChildItem -Path $scriptDirectory -Directory | 
                          Select-Object -ExpandProperty Name
        
        # ������ ����� ������ � ������������� ����������
        Write-Host "`n��������� ������: $($availableModules -join ', ')" -ForegroundColor Cyan
        Write-Host "������� ESC ��� Ctrl+C ��� ������`n" -ForegroundColor DarkGray
        
        # ������ ����� � ���������� ������
        $inputBuffer = ""
        :inputLoop while($true) {
            $key = [Console]::ReadKey($true)
            
            if($key.Key -eq [ConsoleKey]::Escape) {
                Write-Host "`n���������� ������..." -ForegroundColor Yellow
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
        
        # �������� �� ������ ����
        if([string]::IsNullOrWhiteSpace($inputBuffer)) {
            Write-Host "`n���������� ������..." -ForegroundColor Yellow
            exit
        }
        
        $moduleName = $inputBuffer.Trim()
        
        # �������� ������������� ����� ������
        $modulePath = Join-Path -Path $scriptDirectory -ChildPath $moduleName
        if (-Not (Test-Path $modulePath)) {
            Write-Host "������ '$moduleName' �� ������!" -ForegroundColor Red
            continue
        }

        # �������� ��������
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
        
        Write-Host "`n������� ������ '$moduleName' ������� �������!`n" -ForegroundColor Green
    }
    catch {
        Write-Host "`n������: $_" -ForegroundColor Red
    }
}