using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class OrigemAvaliacaoService : SMCServiceBase, IOrigemAvaliacaoService
    {
        #region [ Serviços ]

        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => this.Create<OrigemAvaliacaoDomainService>();

        #endregion [ Serviços ]

        /// <summary>
        /// Buscar a descrição da origem de avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequncial da origem de avaliação</param>
        /// <returns>Descrição da origem de avaliação</returns>
        public string BuscarDescricaoOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            return OrigemAvaliacaoDomainService.BuscarDescricaoOrigemAvaliacao(seqOrigemAvaliacao);
        }

        /// <summary>
        /// Buscar origem avaliacao
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Origem avaliação</returns>
        public OrigemAvaliacaoData BuscarOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            return OrigemAvaliacaoDomainService.BuscarOrigemAvaliacao(seqOrigemAvaliacao).Transform<OrigemAvaliacaoData>();
        }

        /// <summary>
        /// Diario aberto ou fechado baseado na origem de avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Boleano</returns>
        public bool DiarioAbertoPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            return OrigemAvaliacaoDomainService.DiarioAbertoPorOrigemAvaliacao(seqOrigemAvaliacao);
        }

        /// <summary>
        /// Buscar alunos baseado na origem avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de alunos com sequencial histiorico aluno e nome</returns>
        public List<SMCDatasourceItem> BuscarAlunosPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            return OrigemAvaliacaoDomainService.BuscarAlunosPorOrigemAvaliacao(seqOrigemAvaliacao);
        }

        /// <summary>
        /// Buscar professores de uma origem avaliacao
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de professores responsaveis da turma</returns>
        public List<TurmaCabecalhoResponsavelData> BuscarProfessoresPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            return OrigemAvaliacaoDomainService.BuscarProfessoresPorOrigemAvaliacao(seqOrigemAvaliacao).TransformList<TurmaCabecalhoResponsavelData>();
        }

        /// <summary>
        /// Buscar origem avaliação por divisão
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequecial divisão de turma</param>
        /// <returns></returns>
        public long BuscarOrigemAvaliacaoPorDivisaoTurma(long seqDivisaoTurma)
        {
            return OrigemAvaliacaoDomainService.BuscarOrigemAvaliacaoPorDivisaoTurma(seqDivisaoTurma);
        }

        /// <summary>
        /// Buscar descrição do ciclo letivo pela origem avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns></returns>
        public string BusacarDescricaoCicloLetivoPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            return OrigemAvaliacaoDomainService.BusacarDescricaoCicloLetivoPorOrigemAvaliacao(seqOrigemAvaliacao);
        }
    }
}