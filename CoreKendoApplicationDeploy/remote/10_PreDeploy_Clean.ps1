Import-Module WebAdministration

$iisAppName = $args[0]
$iisAppPoolName = $args[1]
$directoryPath = $args[2]
$iiswebsitename = $args[3]

#stop the application pool if it exists
If (test-path "IIS:\AppPools\$iisAppPoolName")
{
    if((Get-WebAppPoolState $iisAppPoolName).Value -ne 'Stopped')
    {
        Stop-WebAppPool -Name $iisAppPoolName
    }
}

#remove iis application
If (test-path "IIS:\Sites\$iiswebsitename\$iisAppName")
{
    remove-item "IIS:\Sites\$iiswebsitename\$iisAppName" -recurse
}

#remove iis app pool
If (test-path "IIS:\AppPools\$iisAppPoolName")
{
    remove-item "IIS:\AppPools\$iisAppPoolName" -recurse
}

#delete application directory
if (test-path $directoryPath -pathType container)
{
    remove-item $directoryPath -recurse
}