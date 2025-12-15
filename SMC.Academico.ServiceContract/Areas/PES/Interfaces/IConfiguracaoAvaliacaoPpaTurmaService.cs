using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Dynamic;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IConfiguracaoAvaliacaoPpaTurmaService : ISMCService
    {
        ConfiguracaoAvaliacaoPpaTurmaCabecalhoData BuscarCabecalhoConfiguracaoAvaliacaoPpaTurma(long seqConfiguracaoAvaliacao);

        SMCPagerData<ConfiguracaoAvaliacaoPpaTurmaListarData> ListarTurmas(ConfiguracaoAvaliacaoPpaTurmaFiltroData filtro);
    }
}
