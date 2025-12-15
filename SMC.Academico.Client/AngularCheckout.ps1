param(
    [Parameter(Mandatory = $false)][string]$Path = 'D:\SMC\Academico\SMC.Academico.Client'
)

$tf = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer\tf.exe";

$currentPath = Get-Location

Set-Location $Path
&$tf vc checkout *.* /recursive

Set-Location $currentPath
