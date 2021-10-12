Import-Module WebAdministration

$iisSiteName = $args[0]
$iisAppPoolName = $args[1]
$iisAppName = $args[2]
$directoryPath = $args[3]
$appPoolUsername = $args[4]
$appPoolPassword = $args[5]

$iisAppPoolDotNetVersion = "v4.0"

# Create the target directory if it doesn't exist
if (!(Test-Path $directoryPath -pathType container))
{
	New-Item -ItemType directory -Path $directoryPath
}

#store the starting directory
$currentDirectory = (Get-Location)

#navigate to the app pools root
cd IIS:\AppPools\

#check if the app pool exists
if (!(Test-Path $iisAppPoolName -pathType container))
{
    #create the app pool
    $appPool = New-Item $iisAppPoolName
    $appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $iisAppPoolDotNetVersion

	$appPool | Set-ItemProperty -Name "processModel.identityType" -Value SpecificUser
	$appPool | Set-ItemProperty -Name "processModel.userName" -Value $appPoolUsername
	$appPool | Set-ItemProperty -Name "processModel.password" -Value $appPoolPassword

	Restart-WebAppPool -Name $iisAppPoolName
}

#navigate to the sites root
cd "IIS:\Sites\"

# Create the web application
New-WebApplication -Site $iisSiteName -name $iisAppName -physicalPath $directoryPath -ApplicationPool $iisAppPoolName -Force

# New-Webapplication puts a bunch of default structure in place. Remove it.
Get-ChildItem -Path $directoryPath -Include *.* -File -Recurse | foreach { $_.Delete()}

#return to where we started
Set-Location $currentDirectory