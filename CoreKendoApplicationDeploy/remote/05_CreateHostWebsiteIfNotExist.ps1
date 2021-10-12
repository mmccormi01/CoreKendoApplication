Import-Module WebAdministration

$iisSiteName = $args[0]
$iisAppPoolName = $args[1]
$directoryPath = $args[2]
$dnsbinding = $args[3]

$iisAppPoolDotNetVersion = "v4.0"

# Only do this if the container website does not already exist
if (!(Test-Path $directoryPath -pathType container))
{
	New-Item -ItemType directory -Path $directoryPath

    #store the starting directory
    $currentDirectory = (Get-Location)

    #navigate to the app pools root
    cd IIS:\AppPools\

    # make sure app pool exists
    if (!(Test-Path $iisAppPoolName -pathType container))
    {
        #create the app pool
        $appPool = New-Item $iisAppPoolName
        $appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $iisAppPoolDotNetVersion
    }

    #navigate to the sites root
    cd "IIS:\Sites\"

    # Create the web site
	New-Website -Name "$iisSiteName" -Port 80 -HostHeader "$dnsbinding" -PhysicalPath "$directoryPath" -ApplicationPool "$iisAppPoolName"

    # New-Webapplication puts a bunch of default structure in place. Remove it.
    Get-ChildItem -Path $directoryPath -Include *.* -File -Recurse | foreach { $_.Delete()}

    #return to where we started
    Set-Location $currentDirectory
}

#
# Set the Portal permissions to default to Windows Auth & Anon disabled
#

$anonAuthFilter = "/system.WebServer/security/authentication/AnonymousAuthentication"
$windowsAuthFilter = "/system.WebServer/security/authentication/windowsAuthentication"

function ToggleAuthentication
{
  param
  (
	[string] $sitetarget,
    [string] $authFilter,
    [bool]   $isEnabled
  )

  $pspath = "IIS:\Sites\$sitetarget\$apptarget"
  $currentAuthLevel = Get-WebConfigurationProperty -filter $authFilter -name Enabled -PSPath $pspath
  Set-WebConfigurationProperty -filter $authFilter  -name Enabled -value $isEnabled -PSPath IIS:\ -location $sitetarget
}

ToggleAuthentication $iisSiteName $anonAuthFilter  $false
ToggleAuthentication $iisSiteName $windowsAuthFilter $true