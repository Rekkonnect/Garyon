param(
    [switch]$Serve
)

$ErrorActionPreference = 'Stop'

function Set-ApiTocRoot {
    $apiTocPath = Join-Path $PSScriptRoot 'api\toc.yml'

    if (-not (Test-Path $apiTocPath)) {
        throw "Generated API TOC was not found at '$apiTocPath'."
    }

    $content = Get-Content $apiTocPath -Raw
    $header = "### YamlMime:TableOfContent`r`nitems:`r`n"
    $apiRoot = "- name: API Reference`r`n  href: index.md`r`n  items:`r`n"

    if ($content.StartsWith($header)) {
        $content = $content.Substring($header.Length)
    }

    if ($content.StartsWith($apiRoot)) {
        return
    }

    $indentedContent = ($content -split "`r?`n") |
        ForEach-Object {
            if ($_ -eq '') { '' }
            else { "  $_" }
        }

    $normalizedContent = $header + $apiRoot + ($indentedContent -join "`r`n").TrimEnd() + "`r`n"
    Set-Content -Path $apiTocPath -Value $normalizedContent -NoNewline
}

function Clear-ApiOutput {
    $apiDir = Join-Path $PSScriptRoot 'api'

    if (-not (Test-Path $apiDir)) {
        New-Item -ItemType Directory -Path $apiDir | Out-Null
        return
    }

    $indexPath = Join-Path $apiDir 'index.md'

    Get-ChildItem -Path $apiDir -Force -Recurse -File |
        Where-Object { $_.FullName -ne $indexPath } |
        Remove-Item -Force

    Get-ChildItem -Path $apiDir -Force -Recurse -Directory |
        Sort-Object -Property FullName -Descending |
        ForEach-Object {
            if ((Get-ChildItem -Path $_.FullName -Force -Recurse | Measure-Object).Count -eq 0) {
                Remove-Item -LiteralPath $_.FullName -Force
            }
        }
}

function Set-ApiLandingPage {
    $apiTocPath = Join-Path $PSScriptRoot 'api\toc.yml'
    $apiIndexPath = Join-Path $PSScriptRoot 'api\index.md'

    if (-not (Test-Path $apiTocPath)) {
        throw "Generated API TOC was not found at '$apiTocPath'."
    }

    $lines = Get-Content $apiTocPath
    $namespaces = [System.Collections.Generic.List[object]]::new()

    for ($i = 0; $i -lt $lines.Count; $i++) {
        if ($lines[$i] -match '^  - uid:\s+(.+)$') {
            $uid = $Matches[1]
            $name = $uid

            if ($i + 1 -lt $lines.Count -and $lines[$i + 1] -match '^    name:\s+(.+)$') {
                $name = $Matches[1]
            }

            $namespaces.Add([pscustomobject]@{
                Uid = $uid
                Name = $name
            })
        }
    }

    $content = @(
        '# API Reference'
        ''
        '<!-- This page is generated from `docs/api/toc.yml` by the docs build scripts. -->'
        ''
        'Browse the generated Garyon API reference by namespace.'
        ''
        'Use the search box or the namespace list below to jump directly into the API surface.'
        ''
        '## Namespaces'
        ''
    )

    foreach ($namespace in $namespaces) {
        $content += "- [$($namespace.Name)]($($namespace.Uid).yml)"
    }

    $content += ''
    $content += 'The namespace pages include the local API table of contents for drilling into individual types.'

    Set-Content -Path $apiIndexPath -Value ($content -join "`r`n") -NoNewline
}

function Set-ConceptualLandingPages {
    $scriptPath = Join-Path $PSScriptRoot 'generate-nav.py'
    python $scriptPath

    if ($LASTEXITCODE -ne 0) {
        throw "Conceptual landing page generation failed."
    }
}

Push-Location $PSScriptRoot

try {
    Write-Host "Building Garyon documentation..." -ForegroundColor Cyan

    if (-not (Get-Command docfx -ErrorAction SilentlyContinue)) {
        Write-Host "DocFX is not installed. Installing..." -ForegroundColor Yellow
        dotnet tool install -g docfx

        if ($LASTEXITCODE -ne 0) {
            Write-Host "Failed to install DocFX. Install it manually with 'dotnet tool install -g docfx'." -ForegroundColor Red
            exit 1
        }
    }

    Write-Host "Generating API metadata..." -ForegroundColor Cyan
    Write-Host "Cleaning previous API output..." -ForegroundColor Cyan
    Clear-ApiOutput
    docfx metadata docfx.json

    if ($LASTEXITCODE -ne 0) {
        Write-Host "Metadata generation failed." -ForegroundColor Red
        exit 1
    }

    Write-Host "Normalizing API table of contents..." -ForegroundColor Cyan
    Set-ApiTocRoot

    Write-Host "Generating API landing page..." -ForegroundColor Cyan
    Set-ApiLandingPage

    Write-Host "Generating conceptual landing pages..." -ForegroundColor Cyan
    Set-ConceptualLandingPages

    Write-Host "Building documentation site..." -ForegroundColor Cyan
    docfx build docfx.json

    if ($LASTEXITCODE -ne 0) {
        Write-Host "Documentation build failed." -ForegroundColor Red
        exit 1
    }

    Write-Host "Documentation built successfully." -ForegroundColor Green
    Write-Host "Output location: docs/_site/" -ForegroundColor Cyan

    if ($Serve) {
        Write-Host "Starting documentation server on http://localhost:8080 ..." -ForegroundColor Cyan
        docfx docfx.json --serve
    }
}
finally {
    Pop-Location
}
