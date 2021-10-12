if ($args.Count -ne 3)
{
  Write-Host "Usage: 40_GenerateGoogleAnalyticsFile.ps1 <file name> <tracking id> <is production>"
  throw "Improper invocation: $args"
}

$outputFile = $args[0]
$trackingId = $args[1]
$isProduction = $args[2]

if($isProduction -eq $true){
	$outputString = "ga('create', '" + $trackingId + "', 'auto');"

	Add-Content $outputFile " "
	Add-Content $outputFile $outputString
	Add-Content $outputFile "ga('send', 'pageview');"
}