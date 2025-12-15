using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Linq;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class AvaliacaoService : SMCServiceBase, IAvaliacaoService
    {
        #region [Domain Service]

        private AvaliacaoDomainService AvaliacaoDomainService => Create<AvaliacaoDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();

        #endregion [Domain Service]

        /// <summary>
        /// Salvar avaliacao
        /// </summary>
        /// <param name="avaliacao">Dados da avaliacao</param>
        /// <returns>Sequencial da avaliação</returns>
        public long SalvarAvaliacao(AvaliacaoData avaliacao)
        {
            return AvaliacaoDomainService.SalvarAvaliacao(avaliacao.Transform<AvaliacaoVO>());
        }

        /// <summary>
        /// Buscar avaliação
        /// </summary>
        /// <param name="seq">Sequencial da avaliação</param>
        /// <returns>Modelo da avaliação</returns>
        public AvaliacaoData BuscarAvaliacao(long seq)
        {
            var ret = AvaliacaoDomainService.BuscarAvaliacao(seq).Transform<AvaliacaoData>();

            // Preenche o codigo de unidade
            ret.CodigoUnidade = EntidadeDomainService.RecuperarCodigoUnidadeSeoPorSeqOrigemAvaliacao(ret.AplicacoesAvaliacao?.FirstOrDefault()?.SeqOrigemAvaliacao ?? 0);

            return ret;
        }

        /// <summary>
        /// Inicia o preenchimento dos dados para criação de uma nova avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliação</param>
        /// <returns>Objeto com os dados iniciais preenchidos</returns>
        public AvaliacaoEditarData PreencherDadosNovaAvaliacao(long seqOrigemAvaliacao)
        {
            return AvaliacaoDomainService.PreencherDadosNovaAvaliacao(seqOrigemAvaliacao).Transform<AvaliacaoEditarData>();
        }

        /// <summary>
        /// Buscar avaliação para a edição
        /// </summary>
        /// <param name="seq">Sequencial da avaliação</param>
        /// <returns>Modelo da avaliação</returns>
        public AvaliacaoEditarData BuscarAvaliacaoEdicao(long seq)
        {
            var ret = AvaliacaoDomainService.BuscarAvaliacaoEdicao(seq).Transform<AvaliacaoEditarData>();
            return ret;
        }

        /// <summary>
        /// Lista de avaliações
        /// </summary>
        /// <param name="filtro">Filtros especificados</param>
        /// <returns>Lista avaliações</returns>
        public SMCPagerData<AvaliacaoData> BuscarAvaliacoes(AvaliacaoFiltroData filtro)
        {
            return AvaliacaoDomainService.BuscarAvaliacoes(filtro.Transform<AvaliacaoFilterSpecification>()).Transform<SMCPagerData<AvaliacaoData>>();
        }

        /// <summary>
        /// Deletar Avaliação
        /// </summary>
        /// <param name="seq">Sequencial da avalaiacão</param>
        public void DeleteAvalicao(long seq)
        {
            AvaliacaoDomainService.DeleteAvalicao(seq);
        }

        /// <summary>
        /// Consultar avaliações da turma
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        ///<returns>Dados da consulta das avaliações</returns>
        public ConsultaAvaliacoesTurmaData ConsultaAvaliacoes(long seqTurma, long seqPessoaAtuacao)
        {
            return AvaliacaoDomainService.ConsultaAvaliacoes(seqTurma, seqPessoaAtuacao).Transform<ConsultaAvaliacoesTurmaData>();
        }
    }
}