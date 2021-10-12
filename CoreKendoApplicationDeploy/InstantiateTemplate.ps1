 <#
    .Synopsis
        Instantiate a new project from a template project, create a local Git repository, and perform an initial commit.
    .Description
        This script does the following:
            1. Copy the contents of this project to a new directory. This directory will be created if it does not exist.
            2. Removes the existing .vs, .git, and any bin or obj directories (and their children) which may exist in the target project after the copy.
            3. Renames all folders in the target project to match the desired naming scheme. e.g., If you choose SomeProject as your project name, you will end up with a directory structure which looks like this:
                SomeProject
                SomeProject/SomeProjectDeploy
                SomeProject/SomeProjectService
                SomeProject/SomeProjectService.Tests
                SomeProject/SomeProjectWeb
                SomeProject/SomeProjectWeb.Tests
            4. Corrects all relative paths in the target project to account for the name changes in #3.
            5. Creates a local Git repository, adds the new source, and performs an initial commit.
            6. Provides the user an example of Git command line steps to push their project to a remote repository.

        Note that while it is possible to execute the new project prior to committing it to Git, this will generate numerous binaries & dependency files which should not be committed to source control.
 #>

 param (
     [string]$targetDirectory = $( Read-Host "Please enter full path (256 char max recommended) to a destination directory (directory will be created if it doesn't exist) "),
     [string]$newProjectName = $( Read-Host "Please enter a name for your new project ")
 )

$originalProjectName = "PimaCountyTemplateDevCore"

# Create targetDirectory if it doesn't exist
If(!(test-path $targetDirectory))
{
      New-Item -ItemType Directory -Force -Path $targetDirectory
}


#Copy the project source to the target location
Write-Host "Copying template to $targetDirectory."
robocopy ..\..\..\. $targetDirectory /e /NFL /NDL /NJH /NJS /nc /ns /np

# Remove the .vs directory if it exists, to eliminate any potential caching issues.
Write-Host "Removing the .vs directory to avoid potential caching issues."
Remove-Item "$targetDirectory\.vs" -Recurse -Force

#Remove the .git directory so this has no connection to the original repository
Write-Host "Removing the .git directory to avoid potential conflicts with source Git repository."
Remove-Item "$targetDirectory\.git" -Recurse -Force

#Remove any leftover bin or obj directories
Write-Host "Removing bin and obj directories throughout the solution to eliminate any binary commits."
Get-ChildItem $targetDirectory\* -Include bin,obj -Recurse -force | Remove-Item -Force -Recurse

# Rename all the folders to the chosen project name
Write-Host "Updating naming convention for all project folders from $originalProjectName[projectComponent] to $newProjectName[projectComponent]"
Get-ChildItem $targetDirectory\* -Recurse -Filter *$originalProjectName* | Rename-Item -NewName { $_.name -replace "$originalProjectName", "$newProjectName"}

# Look through every solution and project files and fix paths
Write-Host "Fixing paths in configuration files."
$projectFiles = Get-ChildItem -path $targetDirectory\* -Include *.sln,*.pssproj,*.csproj -Recurse
foreach ($file in $projectFiles)
{
    (Get-Content $file.PSPath) |
    Foreach-Object { $_ -replace "$originalProjectName", "$newProjectName" } |
    Set-Content $file.PSPath
}

# Setup local git repo
Write-Host "Setting up local Git for instantiated project"
cd $targetDirectory
git init .
git add --all
git commit -m 'Initial Checkin'

Write-Host "`r`n`r`n`r`n`r`nLAST STEP: To add this code to Remote Git, do the following:"
Write-Host "git remote add origin https://tfs.pima.gov/tfs/Pima_County/OPTRA/_git/<YOUR_REPOSITORY_NAME>"
Write-Host "git push -u origin --all"




