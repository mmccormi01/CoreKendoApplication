if ($args.Count -ne 7)
{
  Write-Host "Usage: Setup_ScheduledTasks.ps1 <Task Config File 1> <Task Name 1> <Task Config File 2> <Task Name 2> <Task Config File 3> <Task Name 3> <Task Folder>"
  throw "Improper invocation: $args"
}

$taskFile1=$args[0]
$taskName1=$args[1]
$taskFile2=$args[2]
$taskName2=$args[3]
$taskFile3=$args[4]
$taskName3=$args[5]
$taskPath=$args[6]

Register-ScheduledTask -Xml (get-content $taskFile1 | out-string) -TaskName $taskName1 -TaskPath $taskPath –Force
Register-ScheduledTask -Xml (get-content $taskFile2 | out-string) -TaskName $taskName2 -TaskPath $taskPath –Force
Register-ScheduledTask -Xml (get-content $taskFile3 | out-string) -TaskName $taskName3 -TaskPath $taskPath –Force

Enable-ScheduledTask -TaskName "$taskPath\$taskName1"
Enable-ScheduledTask -TaskName "$taskPath\$taskName2"
Enable-ScheduledTask -TaskName "$taskPath\$taskName3"