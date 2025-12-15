using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IHierarquiaEntidadeService : ISMCService
    {
        /// <summary>
        /// Busca as hierarquia de entidade de acordo com o filtro
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de hierarquias de entidades</returns>
        SMCPagerData<HierarquiaEntidadeListaData> BuscarHierarquiasEntidade(HierarquiaEntidadeFiltroData filtros);

        /// <summary>
        /// Salva a hierarquia de entidade
        /// </summary>
        /// <param name="hierarquiaEntidade">Hierarquia de entidade a ser salva</param>
        /// <returns>Sequencial da hierarquia de entidade salva</returns>
        long SalvarHierarquiaEntidade(HierarquiaEntidadeData hierarquiaEntidade);

        /// <summary>
        /// Busca uma HierarquiaEntidade através de seu Seq
        /// </summary>
        /// <param name="seqHierarquiaEntidade">Sequencial da hierarquia entidade</param>
        /// <returns>Dados da hierarquia de entidade</returns>
        HierarquiaEntidadeData BuscarHierarquiaEntidade(long seqHierarquiaEntidade);

        /// <summary>
        /// Busca as possíveis entidades superiories na visão organizacional de um tipo de entidade
        /// para montagem de select
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo de entidade</param>
        /// <returns>Lista de possíveis entidades superiores</returns>
        List<SMCDatasourceItem> BuscarHierarquiaOrganizacionalSuperiorSelect(long seqTipoEntidade, bool apenasAtivas);

        List<SMCDatasourceItem> BuscarHierarquiaSuperiorSelect(long seqTipoEntidade, TipoVisao tipoiVisao, bool apenasAtivas);

        List<SMCDatasourceItem> BuscarHierarquiaOrganizacionalSuperiorProcessoSelect(bool apenasAtivas, bool usarNomeReduzido);

        List<SMCDatasourceItem> BuscarHierarquiaOrganizacionalSuperiorAlunoSelect(bool apenasAtivas, bool usarNomeReduzido, bool usarSeqEntidade);

        List<SMCDatasourceItem> BuscarEntidadesFolhaVisaoSelect();

        List<SMCDatasourceItem> BuscarEntidadesHierarquiaSelect();
    }
}