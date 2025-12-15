using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IGrupoCurricularComponenteService : ISMCService
    {
        /// <summary>
        /// Busca os Grupos Curriculares Componentes de um Currículo com seus componentes para o lookup
        /// </summary>
        /// <param name="grupoComponenteCurricularFiltro">Sequencial de currículo ou currículo curso oferta</param>
        /// <returns>Array com dados dos grupos curriculares componentes e componentes do currículo</returns>
        GrupoCurricularComponenteListaData[] BuscarGruposCurricularesComponentesLookup(GrupoCurricularComponenteFiltroData grupoCurricularComponenteFiltro);

        /// <summary>
        /// Busca o grupo curricular componente selecionado no lookup
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Array de sequencial do grupo curricular componente</param>
        /// <returns>Array com todos os grupos curriculares componentes selecionados</returns>
        GrupoCurricularComponenteData[] BuscarGruposCurricularesComponentesLookupSelecionado(long[] seqsGruposCurricularesComponentes);


        /// <summary>
        /// Buscar lista de componentes curriculares ativos e com tipo ATA(Atividade Acadêmico)
        /// </summary>
        /// <returns>Lista de componentes curriculares, atendendo aos parâmetros</returns>
        List<SMCDatasourceItem> BuscarComponentesCurricularesPadrao(long seqCurriculoCursoOferta);

        void Excluir(long seq);
    }
}