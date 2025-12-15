param(
    [Parameter(Mandatory = $false)][string]$Path = 'D:\SMC\Academico\SMC.Academico.Client\'
)

$tf = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer\tf.exe";

$currentPath = Get-Location
Set-Location $Path

$localFiles = Get-ChildItem -Exclude node_modules,dist,obj,*.ps1,*.*proj* | Get-ChildItem -Recurse -File | Select-Object -ExpandProperty FullName
$projectPath = Get-ChildItem -Filter *.njsproj
$project = [xml](Get-Content ($projectPath.FullName))
$projectItemGroup = $project.Project.ItemGroup
$projectItem = $projectItemGroup.Content
$projectItemPath = $projectItem | ForEach-Object { $path + $_.Include }
$newItem = $localFiles | Where-Object { $projectItemPath -notcontains $_ -and $_ -notlike '*\projects\Content\*' }

if ($newItem.Length -gt 0) {
  &$tf vc checkout $projectPath.FullName
  foreach ($item in $newItem) {
    $Content = $project.CreateElement('Content')
    $Content.SetAttribute('Include', $item.Replace($Path, ''))
    $projectItemGroup.AppendChild($Content)
    &$tf vc add $item
  }
}

$project.Save($projectPath.FullName)

Set-Location $currentPath
