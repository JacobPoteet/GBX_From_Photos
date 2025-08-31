# Installation Guide

This guide will help you install the necessary components to run the GBX_From_Photos application.

## Prerequisites

### Windows Requirements
- **Operating System**: Windows 10 (version 1809) or later, or Windows Server 2019 or later
- **Architecture**: x64, x86, ARM64
- **Memory**: Minimum 512MB RAM (2GB+ recommended)

## Step 1: Install .NET 6.0 Runtime

The GBX_From_Photos application requires .NET 6.0 or later to run.

### Option A: Download from Microsoft (Recommended)

1. **Visit the Official Download Page**
   - Go to: https://dotnet.microsoft.com/download/dotnet/6.0
   - Click on "Download .NET 6.0 Runtime"

2. **Choose the Right Version**
   - **For most users**: Download "x64" version
   - **For 32-bit systems**: Download "x86" version
   - **For ARM devices**: Download "ARM64" version

3. **Install the Runtime**
   - Run the downloaded installer
   - Follow the installation wizard
   - Restart your computer if prompted

### Option B: Use Windows Package Manager (winget)

If you have Windows Package Manager installed:

```powershell
winget install Microsoft.DotNet.Runtime.6
```

### Option C: Use Chocolatey

If you have Chocolatey installed:

```cmd
choco install dotnet-6.0-runtime
```

## Step 2: Verify Installation

After installation, verify that .NET is properly installed:

1. **Open Command Prompt or PowerShell**
2. **Run the following command:**
   ```cmd
   dotnet --version
   ```
3. **Expected output:** You should see a version number like `6.0.xxx`

If you see an error message, try restarting your computer and running the command again.

## Step 3: Build and Run the Application

### Using the Build Scripts

1. **Double-click `build.bat`** (Windows Command Prompt)
   - OR -
2. **Right-click `build.ps1` and select "Run with PowerShell"**

### Manual Build Commands

If you prefer to build manually:

1. **Open Command Prompt or PowerShell in the project folder**
2. **Restore packages:**
   ```cmd
   dotnet restore
   ```
3. **Build the application:**
   ```cmd
   dotnet build --configuration Release
   ```
4. **Run the application:**
   ```cmd
   dotnet run --configuration Release
   ```

## Troubleshooting

### Common Installation Issues

#### Issue: "dotnet is not recognized"
**Solution:**
- Restart your computer after installing .NET
- Check if .NET was added to your PATH environment variable
- Try running the command from a new Command Prompt/PowerShell window

#### Issue: "This application requires .NET Runtime"
**Solution:**
- Ensure you installed the correct .NET version (6.0 or later)
- Try installing the .NET Desktop Runtime specifically
- Check Windows Update for any pending .NET updates

#### Issue: Build fails with package restore errors
**Solution:**
- Check your internet connection
- Try clearing NuGet cache: `dotnet nuget locals all --clear`
- Ensure you have sufficient disk space

#### Issue: Application crashes on startup
**Solution:**
- Verify .NET 6.0+ is installed
- Check Windows Event Viewer for error details
- Try running as Administrator
- Ensure all Windows updates are installed

### System Compatibility

#### Windows 7/8/8.1
- **Not supported** - These operating systems are not compatible with .NET 6.0
- Consider upgrading to Windows 10 or later

#### Windows 10
- **Supported** - Version 1809 (October 2018 Update) or later required
- Earlier versions need to be updated first

#### Windows 11
- **Fully supported** - All versions compatible

#### Windows Server
- **Supported** - Windows Server 2019 or later required

## Alternative: Download Pre-built Binary

If you continue to have issues building from source:

1. Check the GitHub releases page for pre-built executables
2. Download the latest release ZIP file
3. Extract to a folder
4. Run `GBX_From_Photos.exe` directly

**Note:** Pre-built binaries still require .NET 6.0 Runtime to be installed.

## Getting Help

If you're still experiencing issues:

1. **Check the main README.md** for troubleshooting steps
2. **Review error logs** in the `output` folder
3. **Open a GitHub issue** with:
   - Your Windows version
   - .NET version (`dotnet --version`)
   - Error messages or screenshots
   - Steps to reproduce the issue

## Additional Resources

- **.NET Documentation**: https://docs.microsoft.com/dotnet/
- **.NET Download Center**: https://dotnet.microsoft.com/download
- **Windows System Requirements**: https://www.microsoft.com/windows/windows-11-specifications
