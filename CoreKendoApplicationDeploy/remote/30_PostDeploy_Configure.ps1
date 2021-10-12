    $webConfigFile=$args[0]
	$log4net_file=$args[1]
    $log4net_level=$args[2]
	$appsettingsConfigFile=$args[3]
	$inspectorRoutingBaseAddress=$args[4]

	# Encrypt the connectionStrings
	$webDirectoryFile = Get-ChildItem $webConfigFile
	$webDirectory = $webDirectoryFile.DirectoryName

	$currentDirectory = (Get-Location)
	Set-Location "C:\windows\Microsoft.Net\Framework\v4.0.30319\"
	.\aspnet_regiis.exe -pef "connectionStrings" $webDirectory
	Set-Location $currentDirectory

	# Update the log4net config
    $log4netdoc = (Get-Content $log4net_file) -as [Xml]
    $log4netRoot = $log4netdoc.get_DocumentElement()
    $log4netRoot.root.level.value = $log4net_level

    $log4netdoc.Save($log4net_file)

    # Update the appsettings file
	$AppSettingsDoc = (Get-Content $appsettingsConfigFile) -as [Xml]

	$AppSettingsDictionary = @{
		InspectorRoutingBaseAddress = $inspectorRoutingBaseAddress
	}

	foreach($key in $AppSettingsDictionary.Keys)
    {
        if(($addKey = $AppSettingsDoc.SelectSingleNode("//appSettings/add[@key = '$key']")))
        {
            $addKey.SetAttribute('value',$AppSettingsDictionary[$key])
        }
    }

	$AppSettingsDoc.Save($appSettingsConfigFile)