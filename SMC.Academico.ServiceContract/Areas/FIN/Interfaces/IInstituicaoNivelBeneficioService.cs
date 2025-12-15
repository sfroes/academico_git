using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.FIN.Interfaces
{
    public interface IInstituicaoNivelBeneficioService : ISMCService
    {
        long SalvarInstituicaoNivelBeneficio(InstituicaoNivelBeneficioData instituicaoNivelBeneficio);

        SMCPagerData<InstituicaoNivelBeneficioListaData> BuscarInstituicoesNiveisBeneficios(InstituicaoNivelBeneficioFiltroData filtros);

        void ExcluirInstituicoesNiveisBeneficios(long Seq);
    }
}
