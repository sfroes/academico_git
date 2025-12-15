using SMC.Academico.ServiceContract.Areas.CUR.Data.DivisaoComponente;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IDivisaoComponenteService : ISMCService
    {
        /// <summary>
        /// Busca as divisões de uma configuração de componente curricular
        /// </summary>
        /// <param name="seqConfiguracaoCompoente">Sequencial da configuração de componente curricular</param>
        /// <returns>Dados das divisões de componentes curriculares</returns>
        List<SMCDatasourceItem> BuscarDivisoesCompoentePorConfiguracao(long seqConfiguracaoCompoente);

        List<SMCDatasourceItem> BuscarDivisaoComponenteAluno(long seqAluno);

		ConfiguracaoDivisaoComponenteData BuscarDadosConfiguracaoDivisaoComponente(long seqDivisaoComponente, long seqMatrizCurricular);
	}
}
