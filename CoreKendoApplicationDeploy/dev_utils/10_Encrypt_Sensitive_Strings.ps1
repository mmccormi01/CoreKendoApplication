#
# Encrypt_Sensitive_Strings.ps1
#
   $webConfigFile=$args[0]

# Encrypt the connectionStrings
   $webDirectoryFile = Get-ChildItem $webConfigFile
   $webDirectory = $webDirectoryFile.DirectoryName

   $currentDirectory = (Get-Location)
   Set-Location "C:\windows\Microsoft.Net\Framework\v4.0.30319\"
   .\aspnet_regiis.exe -pef "connectionStrings" $webDirectory
   Set-Location $currentDirectory