using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class InstituicaoEnsinoService : SMCServiceBase, IInstituicaoEnsinoService
    {
        #region Servicos de Dominio

        /// <summary>
        /// Instância domain service da entidade InstituicaoEnsino.
        /// </summary>
        protected InstituicaoEnsinoDomainService InstituicaoEnsinoDomainService
        {
            get { return this.Create<InstituicaoEnsinoDomainService>(); }
        }

        /// <summary>
        /// Instância domain service da entidade EntidadeService.
        /// </summary>
        protected IEntidadeService EntidadeService
        {
            get { return this.Create<IEntidadeService>(); }
        }

        #endregion Servicos de Dominio

        /// <summary>
        /// Busca as configurações de entidade para cadastro de uma instituição de ensino
        /// </summary>
        /// <returns>Dados da configuração de entidade</returns>
        public EntidadeData BuscaConfiguracoesEntidadeInstutuicaoEnsino()
        {
            return this.EntidadeService.BuscarConfiguracoesEntidadeComClassificacao(TOKEN_TIPO_ENTIDADE_EXTERNADA.INSTITUICAO_ENSINO);
        }

        /// <summary>
        /// Buscar uma lista de instituições de ensino
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de instituições</returns>
        public SMCPagerData<InstituicaoEnsinoListaData> BuscarInstituicoesEnsino(InstituicaoEnsinoFiltroData filtros)
        {
            try
            {
                InstituicaoEnsinoDomainService.DisableFilter(FILTER.INSTITUICAO_ENSINO);

                return InstituicaoEnsinoDomainService.SearchBySpecification<InstituicaoEnsinoFilterSpecification,
                    InstituicaoEnsinoFiltroData,
                    InstituicaoEnsinoListaData,
                    InstituicaoEnsino>(filtros, IncludesInstituicaoEnsino.Mantenedora);
            }
            finally
            {
                InstituicaoEnsinoDomainService.EnableFilter(FILTER.INSTITUICAO_ENSINO);
            }
        }

        /// <summary>
        /// Buscar a instituições de ensino do usuário logado, utilizado em relatórios aproveita o filtro global
        /// </summary>
        /// <returns>Instituição de ensino do usuário logado</returns>
        public InstituicaoEnsinoData BuscarInstituicoesEnsinoLogado()
        {
            return InstituicaoEnsinoDomainService.SearchBySpecification(new InstituicaoEnsinoFilterSpecification(), IncludesInstituicaoEnsino.ArquivoLogotipo | IncludesInstituicaoEnsino.Enderecos).TransformList<InstituicaoEnsinoData>().FirstOrDefault();
        }

        /// <summary>
        /// Busca uma instituição de ensino
        /// </summary>
        /// <param name="seq">Sequencial da instituição de ensino</param>
        /// <returns>Dados da insitituição com sequencial informado</returns>
        public InstituicaoEnsinoData BuscarInstituicaoEnsino(long seq)
        {
            return InstituicaoEnsinoDomainService.BuscarInstituicaoEnsino(seq).Transform<InstituicaoEnsinoData>();
        }

        /// <summary>
        /// Busca uma instituição de ensino com as configurações do tipo de entidade instituição de ensino
        /// </summary>
        /// <param name="seq">Sequencial da instituição de ensino</param>
        /// <returns>Dados da insitituição com sequencial informado</returns>
        public InstituicaoEnsinoData BuscarInstituicaoEnsinoComConfiguracao(long seq)
        {
            var instituicaoEnsino = InstituicaoEnsinoDomainService.BuscarInstituicaoEnsino(seq);
            var configuracao = this.BuscaConfiguracoesEntidadeInstutuicaoEnsino();
            var instituicaoEnsinoData = instituicaoEnsino.Transform<InstituicaoEnsinoData>(configuracao);

            return instituicaoEnsinoData;
        }

        /// <summary>
        /// Busca uma instituição de ensino pela sigla.
        /// </summary>
        /// <param name="sigla">Sigla da instituição.</param>
        /// <returns>Dados da insitituição encontrada.</returns>
        public InstituicaoEnsinoData BuscarInstituicaoEnsinoPorSigla(string sigla)
        {
            return InstituicaoEnsinoDomainService.BuscarInstituicaoEnsinoPorSigla(sigla).Transform<InstituicaoEnsinoData>();
        }

        /// <summary>
        /// Salva uma instituicao de ensino
        /// </summary>
        /// <param name="instituicao">Dados da instituição a ser salva</param>
        /// <returns>Sequencial da instituição salva</returns>
        public long SalvarInstituicaoEnsino(InstituicaoEnsinoData instituicao)
        {
            try
            {
                InstituicaoEnsinoDomainService.DisableFilter(FILTER.INSTITUICAO_ENSINO);
                InstituicaoEnsino instituicaoEnsino = SMCMapperHelper.Create<InstituicaoEnsino>(instituicao);
                return this.InstituicaoEnsinoDomainService.SalvarInstituicaoEnsino(instituicaoEnsino);
            }
            finally
            {
                InstituicaoEnsinoDomainService.EnableFilter(FILTER.INSTITUICAO_ENSINO);
            }
        }

        /// <summary>
        /// Exclui uma instituição de ensino
        /// </summary>
        /// <param name="seqInstituicao">Sequencial da instituição de ensino para exclusão</param>
        public void ExcluirInstituicaoEnsino(long seqInstituicao)
        {
            try
            {
                InstituicaoEnsinoDomainService.DisableFilter(FILTER.INSTITUICAO_ENSINO);
                this.InstituicaoEnsinoDomainService.DeleteEntity(seqInstituicao);
            }
            finally
            {
                InstituicaoEnsinoDomainService.EnableFilter(FILTER.INSTITUICAO_ENSINO);
            }
        }

        /// <summary>
        /// Busca as insituições de ensino para seleção em um select.
        /// Busca apenas as que o usuário tem permissão de acordo com o filtro de dados.
        /// </summary>
        /// <returns>Lista de instituições de ensino que o usuário logado tem permissão de filtro de dados</returns>
        public List<SMCDatasourceItem> BuscarInstituicoesEnsinoSelect(bool ignorarFiltroDados = false)
        {
            try
            {
                if (ignorarFiltroDados)
                    InstituicaoEnsinoDomainService.DisableFilter(FILTER.INSTITUICAO_ENSINO);

                InstituicaoEnsinoFiltroData filtros = new InstituicaoEnsinoFiltroData();
                filtros.Ativo = true;

                InstituicaoEnsinoFilterSpecification spec = SMCMapperHelper.Create<InstituicaoEnsinoFilterSpecification>(filtros);
                var instituicoes = InstituicaoEnsinoDomainService.SearchBySpecification<InstituicaoEnsinoFilterSpecification,
                    InstituicaoEnsinoFiltroData,
                    InstituicaoEnsinoListaData,
                    InstituicaoEnsino>(filtros);

                return instituicoes.TransformList<SMCDatasourceItem>();
            }
            finally
            {
                if (ignorarFiltroDados)
                    InstituicaoEnsinoDomainService.EnableFilter(FILTER.INSTITUICAO_ENSINO);
            }
        }

        /// <summary>
        /// Busca a instituição de ensino da pessoa atuação logada
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Dados da instituição de ensino</returns>
        public InstituicaoEnsinoData BuscarInstituicaoEnsinoPorPessoaAtuacao(long seqPessoaAtuacao)
        {
            return InstituicaoEnsinoDomainService.BuscarInstituicaoEnsinoPorPessoaAtuacao(seqPessoaAtuacao).Transform<InstituicaoEnsinoData>();
        }
    }
}