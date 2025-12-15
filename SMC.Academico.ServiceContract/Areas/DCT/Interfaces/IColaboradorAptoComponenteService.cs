using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Interfaces
{
    public interface IColaboradorAptoComponenteService : ISMCService
    {
        SMCPagerData<ColaboradorAptoComponenteData> BuscarColadoradorAptoComponentes(ColaboradorAptoComponenteFiltroData filtros);

        long SalvarColadoradorAptoComponente(ColaboradorAptoComponenteData modelo);

        bool ValidarFormacaoAcademica(long seqAtuacaoColaborador);

    }
}