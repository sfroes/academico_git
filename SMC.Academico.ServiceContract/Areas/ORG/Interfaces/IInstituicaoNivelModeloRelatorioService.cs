using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IInstituicaoNivelModeloRelatorioService : ISMCService
    {
        InstituicaoNivelModeloRelatorioData BuscarInstituicaoNivelModeloRelatorio(long seq);

        InstituicaoNivelModeloRelatorioData BuscarTemplateModeloRelatorio(long seqInstituicaoNivel, ModeloRelatorio modeloRelatorio);

        long SalvarInstituicaoNivelModeloRelatorio(InstituicaoNivelModeloRelatorioData modelo);

        InstituicaoNivelModeloRelatorioData VerificarTemplateModeloRelatorio(short? numeroDias, long seqInstituicaoNivel);
    }
}
 