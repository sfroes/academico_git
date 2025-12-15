using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IInstituicaoNivelCalendarioService : ISMCService
    {
        InstituicaoNivelCalendarioData BuscarInstituicaoNivelCalendario(long seq);

        SMCPagerData<InstituicaoNivelCalendarioListaData> BuscarListaInstituicaoNivelCalendario(InstituicaoNivelCalendarioFiltroData filtros);

        List<SMCDatasourceItem> BuscarTiposAvaliacao(UsoCalendario usoCalendario);

        List<SMCDatasourceItem> BuscarTiposEventosCalendario(List<long> seqsNivelEnsino);

        /// <summary>
        /// Buscar tipos de envento por intituicao nivel
        /// </summary>
        /// <param name="seqsInstituicaoNivel">Sequencial instituição nivel</param>
        /// <returns>Lista baseado na instituicao nivel para select</returns>
        List<SMCDatasourceItem> BuscarTiposEventosCalendarioInstituicaoNivel(long seqInstituicaoNivel);

        List<SMCDatasourceItem> BuscarTiposEventosTrabalhoAcademico(long? seqTrabalhoAcademico, long? seqOrigemAvaliacao);

    }
}
