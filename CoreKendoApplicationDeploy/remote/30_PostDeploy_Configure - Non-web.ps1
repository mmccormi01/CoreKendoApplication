if ($args.Count -ne 3)
{
  Write-Host "Usage: PostDeploy_Configure.ps1 <App Config File> <log4net file> <log4net level>"
  throw "Improper invocation: $args"
}

$appConfigFile=$args[0]
$log4net_file=$args[1]
$log4net_level=$args[2]

# Encrypt the connectionStrings
$appDirectoryFile = Get-ChildItem $appConfigFile
$appDirectory = $appDirectoryFile.DirectoryName

# Rename app config file to web.config so encryption works
Rename-Item -Path $appConfigFile  -NewName web.config

$currentDirectory = (Get-Location)
Set-Location "C:\windows\Microsoft.Net\Framework\v4.0.30319\"
.\aspnet_regiis.exe -pef "connectionStrings" $appDirectory -prov "DataProtectionConfigurationProvider"
.\aspnet_regiis.exe -pef "appSettings" $appDirectory -prov "DataProtectionConfigurationProvider"
Set-Location $currentDirectory

# Change the name back
$webConfigPath = "$($appDirectory)\web.config"
Rename-Item -Path $webConfigPath -NewName $appConfigFile

# Update the log4net config
$log4netdoc = (Get-Content $log4net_file) -as [Xml]
$log4netRoot = $log4netdoc.get_DocumentElement()
$log4netRoot.root.level.value = $log4net_level

$log4netdoc.Save($log4net_file)