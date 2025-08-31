# GBX_From_Photos Build Script
Write-Host "Building GBX_From_Photos application..." -ForegroundColor Green
Write-Host ""

# Check if .NET is installed
try {
    $dotnetVersion = dotnet --version
    Write-Host ".NET version found: $dotnetVersion" -ForegroundColor Yellow
} catch {
    Write-Host "ERROR: .NET 6.0 or later is not installed." -ForegroundColor Red
    Write-Host ""
    Write-Host "Please install .NET 6.0 or later from: https://dotnet.microsoft.com/download" -ForegroundColor Yellow
    Write-Host ""
    Read-Host "Press Enter to continue"
    exit 1
}

Write-Host ""

# Restore NuGet packages
Write-Host "Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Failed to restore packages." -ForegroundColor Red
    Read-Host "Press Enter to continue"
    exit 1
}

# Build application
Write-Host "Building application..." -ForegroundColor Yellow
dotnet build --configuration Release
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Build failed." -ForegroundColor Red
    Read-Host "Press Enter to continue"
    exit 1
}

Write-Host ""
Write-Host "Build completed successfully!" -ForegroundColor Green
Write-Host ""
Write-Host "To run the application:" -ForegroundColor Yellow
Write-Host "  dotnet run --configuration Release"
Write-Host ""
Write-Host "Or navigate to bin\Release\net6.0-windows\ and run GBX_From_Photos.exe" -ForegroundColor Yellow
Write-Host ""

Read-Host "Press Enter to continue"
