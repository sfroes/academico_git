using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IGrupoCurricularService : ISMCService
    {
        /// <summary>
        /// Busca os dados do currículo e curso de um grupo curricular
        /// </summary>
        /// <param name="seqCurriculo">Sequencial do currículo</param>
        /// <returns>Dados do currículo e curso asociado</returns>
        GrupoCurricularCabecalhoData BuscarGrupoCurricularCabecalho(long seqCurriculo);

        /// <summary>
        /// Busca os Grupos Curriculares de um Currículo com seus componentes e sequenciais convertidos em index para permitir dois tipos de objeto na mesma árvore
        /// </summary>
        /// <param name="seqCurriculo">Sequencial do Currículo</param>
        /// <returns>Dados dos Grupos Curriculares e Componentes do Currículo</returns>
        GrupoCurricularListaData[] BuscarGruposCurricularesTree(long seqCurriculo);

        /// <summary>
        /// Busca os Grupos Curriculares de um Currículo com seus componentes e sequenciais convertidos em index para permitir dois tipos de objeto na mesma árvore.
        /// A descrição dos grupos curriculares será simples (somente o campo descrição sem aplicar a regra RN_CUR_045)
        /// </summary>
        /// <param name="seqCurriculo">Sequencial do Currículo</param>
        /// <returns>Dados dos Grupos Curriculares e Componentes do Currículo</returns>
        GrupoCurricularListaData[] BuscarGruposCurricularesDescricaoSimplesTree(long seqCurriculo);

        /// <summary>
        /// Busca os Grupos Curriculares de um Currículo com seus componentes para o lookup
        /// </summary>
        /// <param name="grupoCurricularFiltro">Sequencial de currículo ou currículo curso oferta</param>
        /// <returns>Array com dados dos grupos curriculares e componentes do currículo</returns>
        GrupoCurricularListaData[] BuscarGruposCurricularesLookup(GrupoCurricularFiltroData grupoCurricularFiltro);

        /// <summary>
        /// Busca o grupo curricular selecionado no lookup
        /// </summary>
        /// <param name="seqGrupoCurricular">Array de sequencial do grupo curricular</param>
        /// <returns>Array com todos os grupos curricularres selecionados</returns>
        GrupoCurricularData[] BuscarGruposCurricularesLookupSelecionado(long[] seqGruposCurriculares);

        /// <summary>
        /// Recupera o sequencial do curso e nível de ensino associados ao curriculo informado
        /// </summary>
        /// <param name="seqCurriculo">Sequencial do curriclo</param>
        /// <param name="seqGrupoSuperior">Sequencial do grupo curricular superior, 0 quando for raiz</param>
        /// <returns>Dto de GrupoCurricular com o Sequencial do Curso e Nível Ensino associados ao currículo</returns>
        GrupoCurricularData BuscarConfiguracao(long seqCurriculo, long seqGrupoSuperior);

        /// <summary>
        /// Grava o grupo curricular
        /// </summary>
        /// <param name="grupoCurricular">Dados do grupo curricular</param>
        /// <returns>Sequencial do grupo curricular gravado</returns>
        /// <exception cref="GrupoCurricularAssociadoDivisaoMatrizException">Caso seja alterada a configuração de um grupo associado a uma divisão de matriz</exception>
        long SalvarGrupoCurricular(GrupoCurricularData grupoCurricular);

        /// <summary>
        /// Busca o grupo curricular e gera a descrição formatada para ele
        /// </summary>
        /// <param name="seqGrupoCurricular">Sequencial do grupo curricular</param>
        /// <returns>Descricao formatada</returns>
        string BuscaGrupoCurricularDescricaoFormatada(long seqGrupoCurricular);

        /// <summary>
        /// Burcar o total dispensado conforme o tipo do grupo curricular informado
        /// </summary>
        /// <param name="seqGrupoCurricular">Sequencial do grupo curricular</param>
        /// <returns>Total dispensado a ser retornado e se o campo na tela deve ser bloqueado ou não</returns>

        int BuscarTotalDispensadoSolicitacaoDispensa(long seqGrupoCurricular);

        /// <summary>
        /// Valida se o grupo curricular possui formato de configuração
        /// </summary>
        /// <param name="seqGrupoCurricular">Sequencial do grupo curricular</param>
        /// <returns>retorna verdadeiro caso o grupo curricular possua formato de configuração</returns>
        bool ValidarFormatoConfiguracaoGrupoCurricular(long seqGrupoCurricular);

        /// <summary>
        /// Recupera o sequencial do curso e nível de ensino associados ao curriculo informado
        /// </summary>
        /// <param name="seq">Sequencial do grupo curricular</param>
        /// <returns>Dto de GrupoCurricular com sua formação específica, benefícios e bloqueios</returns>
        GrupoCurricularDescricaoData BuscarGrupoCurricularDescricao(long seq);
    }
}