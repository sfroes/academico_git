using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class GrupoCurricularService : SMCServiceBase, IGrupoCurricularService
    {
        #region [ DomainService ]

        private GrupoCurricularDomainService GrupoCurricularDomainService
        {
            get { return this.Create<GrupoCurricularDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca os dados do currículo e curso de um grupo curricular
        /// </summary>
        /// <param name="seqCurriculo">Sequencial do currículo</param>
        /// <returns>Dados do currículo e curso asociado</returns>
        public GrupoCurricularCabecalhoData BuscarGrupoCurricularCabecalho(long seqCurriculo)
        {
            return this.GrupoCurricularDomainService
                .BuscarGrupoCurricularCabecalho(seqCurriculo)
                .Transform<GrupoCurricularCabecalhoData>();
        }

        /// <summary>
        /// Busca os Grupos Curriculares de um Currículo com seus componentes e sequenciais convertidos em index para permitir dois tipos de objeto na mesma árvore
        /// </summary>
        /// <param name="seqCurriculo">Sequencial do Currículo</param>
        /// <returns>Dados dos Grupos Curriculares e Componentes do Currículo</returns>
        public GrupoCurricularListaData[] BuscarGruposCurricularesTree(long seqCurriculo)
        {
            return this.GrupoCurricularDomainService
                .BuscarGruposCurricularesTree(seqCurriculo)
                .TransformToArray<GrupoCurricularListaData>();
        }

        /// <summary>
        /// Busca os Grupos Curriculares de um Currículo com seus componentes e sequenciais convertidos em index para permitir dois tipos de objeto na mesma árvore.
        /// A descrição dos grupos curriculares será simples (somente o campo descrição sem aplicar a regra RN_CUR_045)
        /// </summary>
        /// <param name="seqCurriculo">Sequencial do Currículo</param>
        /// <returns>Dados dos Grupos Curriculares e Componentes do Currículo</returns>
        public GrupoCurricularListaData[] BuscarGruposCurricularesDescricaoSimplesTree(long seqCurriculo)
        {
            return this.GrupoCurricularDomainService
                .BuscarGruposCurricularesTree(seqCurriculo, false)
                .TransformToArray<GrupoCurricularListaData>();
        }

        /// <summary>
        /// Busca os Grupos Curriculares de um Currículo com seus componentes para o lookup
        /// </summary>
        /// <param name="grupoCurricularFiltro">Sequencial de currículo ou currículo curso oferta</param>
        /// <returns>Array com dados dos grupos curriculares e componentes do currículo</returns>
        public GrupoCurricularListaData[] BuscarGruposCurricularesLookup(GrupoCurricularFiltroData grupoCurricularFiltro)
        {
            var grupoCurricularVO = GrupoCurricularDomainService.BuscarGruposCurricularesLookup(grupoCurricularFiltro.Transform<GrupoCurricularFiltroVO>());
            return grupoCurricularVO.TransformToArray<GrupoCurricularListaData>();
        }

        /// <summary>
        /// Busca o grupo curricular selecionado no lookup
        /// </summary>
        /// <param name="seqGruposCurriculares">Array de sequencial do grupo curricular</param>
        /// <returns>Array com todos os grupos curricularres selecionados</returns>
        public GrupoCurricularData[] BuscarGruposCurricularesLookupSelecionado(long[] seqGruposCurriculares)
        {
            return GrupoCurricularDomainService.BuscarGruposCurricularesLookupSelecionado(seqGruposCurriculares).TransformToArray<GrupoCurricularData>();
        }

        /// <summary>
        /// Recupera o sequencial do curso e nível de ensino associados ao curriculo informado
        /// </summary>
        /// <param name="seqCurriculo">Sequencial do curriclo</param>
        /// <param name="seqGrupoSuperior">Sequencial do grupo curricular superior, 0 quando for raiz</param>
        /// <returns>Dto de GrupoCurricular com o Sequencial do Curso e Nível Ensino associados ao currículo</returns>
        public GrupoCurricularData BuscarConfiguracao(long seqCurriculo, long seqGrupoSuperior)
        {
            return this.GrupoCurricularDomainService
                .BuscarConfiguracoes(seqCurriculo, seqGrupoSuperior)
                .Transform<GrupoCurricularData>();
        }

        /// <summary>
        /// Grava o grupo curricular
        /// </summary>
        /// <param name="grupoCurricular">Dados do grupo curricular</param>
        /// <returns>Sequencial do grupo curricular gravado</returns>
        /// <exception cref="GrupoCurricularAssociadoDivisaoMatrizException">Caso seja alterada a configuração de um grupo associado a uma divisão de matriz</exception>
        public long SalvarGrupoCurricular(GrupoCurricularData grupoCurricular)
        {
            return this.GrupoCurricularDomainService
                .SalvarGrupoCurricular(grupoCurricular.Transform<GrupoCurricularVO>());
        }

        /// <summary>
        /// Busca o grupo curricular e gera a descrição formatada para ele
        /// </summary>
        /// <param name="seqGrupoCurricular">Sequencial do grupo curricular</param>
        /// <returns>Descricao formatada</returns>
        public string BuscaGrupoCurricularDescricaoFormatada(long seqGrupoCurricular)
        {
            return this.GrupoCurricularDomainService.BuscaGrupoCurricularDescricaoFormatada(seqGrupoCurricular);
        }

        /// <summary>
        /// Burcar o total dispensado conforme o tipo do grupo curricular informado
        /// </summary>
        /// <param name="seqGrupoCurricular">Sequencial do grupo curricular</param>
        /// <returns>Total dispensado a ser retornado</returns>
        public int BuscarTotalDispensadoSolicitacaoDispensa(long seqGrupoCurricular)
        {
            return this.GrupoCurricularDomainService.BuscarTotalDispensadoSolicitacaoDispensa(seqGrupoCurricular);
        }

        /// <summary>
        /// Valida se o grupo curricular possui formato de configuração
        /// </summary>
        /// <param name="seqGrupoCurricular">Sequencial do grupo curricular</param>
        /// <returns>retorna verdadeiro caso o grupo curricular possua formato de configuração</returns>
        public bool ValidarFormatoConfiguracaoGrupoCurricular(long seqGrupoCurricular)
        {
            return this.GrupoCurricularDomainService.ValidarFormatoConfiguracaoGrupoCurricular(seqGrupoCurricular);
        }

        /// <summary>
        /// Recupera o sequencial do curso e nível de ensino associados ao curriculo informado
        /// </summary>
        /// <param name="seq">Sequencial do grupo curricular</param>
        /// <returns>Dto de GrupoCurricular com sua formação específica, benefícios e bloqueios</returns>
        public GrupoCurricularDescricaoData BuscarGrupoCurricularDescricao(long seq)
        {
            return GrupoCurricularDomainService.BuscarGrupoCurricularDescricao(seq).Transform<GrupoCurricularDescricaoData>();
        }
    }
}