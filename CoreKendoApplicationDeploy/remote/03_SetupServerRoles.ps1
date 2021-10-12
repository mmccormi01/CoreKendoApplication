#
# Internal Server Roles
#

function AddFeatureIfMissing
{
  param
  (
    [string] $featureName
  )

  if ((Get-WindowsFeature $featureName).Installed -eq $false)
  {
    Add-WindowsFeature $featureName
  }
}

AddFeatureIfMissing "Web-Filtering"
AddFeatureIfMissing "Web-Windows-Auth"
AddFeatureIfMissing "Web-ASP"
AddFeatureIfMissing "Web-Net-Ext45"
AddFeatureIfMissing "Web-Asp-Net45"
AddFeatureIfMissing "Web-ISAPI-Filter"
AddFeatureIfMissing "NET-Framework-45-Features"
AddFeatureIfMissing "NET-Framework-45-Core"
AddFeatureIfMissing "NET-Framework-45-ASPNET"
AddFeatureIfMissing "NET-WCF-Services45"
AddFeatureIfMissing "NET-WCF-HTTP-Activation45"
AddFeatureIfMissing "NET-WCF-TCP-PortSharing45"
AddFeatureIfMissing "HTTP Activation"