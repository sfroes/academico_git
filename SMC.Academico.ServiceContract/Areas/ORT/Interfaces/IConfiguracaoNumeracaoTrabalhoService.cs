using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Interfaces
{
    public interface IConfiguracaoNumeracaoTrabalhoService : ISMCService
    {
        long SalvarConfiguracaoNumeracaoTrabalho(ConfiguracaoNumeracaoTrabalhoData configuracao);

        SMCPagerData<ConfiguracaoNumeracaoTrabalhoListaData> BuscarConfiguracaoNumeracaoTrabalho(ConfiguracaoNumeracaoTrabalhoFiltroData filtro);

    }
}
