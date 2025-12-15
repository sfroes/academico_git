if (-not(Test-Path ..\..\..\Recursos)) {
  New-Item -ItemType Directory ..\..\..\Recursos
}
New-Item -ItemType SymbolicLink -Path ..\..\..\Recursos\4.0 -Target ..\..\Framework4.2\SMC.Recursos -Force
New-Item -ItemType SymbolicLink -Path ..\..\Recursos\4.0 -Target ..\..\Framework4.2\SMC.Recursos -Force
New-Item -ItemType SymbolicLink -Path projects\Content -Target ..\SMC.Academico.Recursos\Content -Force
