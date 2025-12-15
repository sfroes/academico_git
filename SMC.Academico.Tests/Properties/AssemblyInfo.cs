using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xunit;

[assembly: AssemblyTitle("SMC.Academico.Tests")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("SMC.Academico.Tests")]
[assembly: AssemblyCopyright("Copyright ©  2019")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("554972c6-c1a4-4f47-bf0a-0c7116edc368")]

// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("4.2.10.11")]
[assembly: AssemblyFileVersion("4.2.10.11")]
//Limita o chrome a abrir apenas 1 navegador ao executar os testes
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, MaxParallelThreads = 2, DisableTestParallelization = true)]

