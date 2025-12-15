using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IInstituicaoModeloRelatorioService : ISMCService
    {
        InstituicaoModeloRelatorioData BuscarInstituicaoModeloRelatorio(long seq);

        InstituicaoModeloRelatorioData BuscarTemplateModeloRelatorio(long seqInstituicaoEnsino, ModeloRelatorio modeloRelatorio);

        SMCPagerData<InstituicaoModeloRelatorioListarData> BuscarInstituicaoModeloRelatorios(InstituicaoModeloRelatorioFiltroData filtro);

        long SalvarModeloRelatorio(InstituicaoModeloRelatorioData modelo);
    }
}