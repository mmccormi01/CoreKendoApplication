$siteName = $args[0]
$appName = $args[1]

# Constants
$anonAuthFilter = "/system.WebServer/security/authentication/AnonymousAuthentication"
$windowsAuthFilter = "/system.WebServer/security/authentication/windowsAuthentication"

#
# Turn auth on/off for a particular application
# Note that EnableAuthenticationOverridesForApplications must be executed prior to this function to allow overriding.
#
# Based on: https://github.com/jefflomax/configure-iis-webapps-powershell/blob/master/configureIISWebApplications.ps1
#
# Params:
#   website    - The website (i.e. 'Default Website')
#   appname    - The application living under the website
#   authFilter - The auth filter to toggle
#   isEnabled  - Whether
#
# Usage: ToggleAuthentication "Default Web Site" "My App" "/system.WebServer/security/authentication/AnonymousAuthentication" $true
#
function ToggleAuthentication
{
  param
  (
	[string] $sitetarget,
    [string] $apptarget,
    [string] $authFilter,
    [bool]   $isEnabled
  )

  $pspath = "IIS:\Sites\$sitetarget\$apptarget"
  $currentAuthLevel = Get-WebConfigurationProperty -filter $authFilter -name Enabled -PSPath $pspath
  Set-WebConfigurationProperty -filter $authFilter  -name Enabled -value $isEnabled -PSPath IIS:\ -location $sitetarget/$apptarget
}

# Set auth on the website
ToggleAuthentication $siteName $appName $anonAuthFilter  $false
ToggleAuthentication $siteName $appName $windowsAuthFilter $true