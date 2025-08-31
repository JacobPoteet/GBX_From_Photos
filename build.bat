@echo off
echo Building GBX_From_Photos application...
echo.

REM Check if .NET is installed
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET 6.0 or later is not installed.
    echo.
    echo Please install .NET 6.0 or later from: https://dotnet.microsoft.com/download
    echo.
    pause
    exit /b 1
)

echo .NET version found:
dotnet --version
echo.

echo Restoring NuGet packages...
dotnet restore
if %errorlevel% neq 0 (
    echo ERROR: Failed to restore packages.
    pause
    exit /b 1
)

echo Building application...
dotnet build --configuration Release
if %errorlevel% neq 0 (
    echo ERROR: Build failed.
    pause
    exit /b 1
)

echo.
echo Build completed successfully!
echo.
echo To run the application:
echo   dotnet run --configuration Release
echo.
echo Or navigate to bin\Release\net6.0-windows\ and run GBX_From_Photos.exe
echo.
pause
