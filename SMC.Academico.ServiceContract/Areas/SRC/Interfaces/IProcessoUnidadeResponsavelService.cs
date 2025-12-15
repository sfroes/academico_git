using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IProcessoUnidadeResponsavelService : ISMCService
    {
        List<SMCDatasourceItem> BuscarUnidadesResponsaveisVinculadasProcessoSelect(TipoUnidadeResponsavel tipoUnidadeResponsavel = TipoUnidadeResponsavel.Nenhum, bool usarNomeReduzido = false);

        /// <summary>
        /// Buscar unidades responsaveis do processo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do Processo</param>
        /// <returns>Lista das unidades responsaveis de um processo</returns>
        List<SMCDatasourceItem> BuscarUnidadesResponsaveisPorProcessoSelect(long seqProcesso);
    }
}