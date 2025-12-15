using SMC.Academico.Domain.Models;
using SMC.Framework.Mapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IncluirAlunoHistoricoSituacaoVO : ISMCMappable
    {
        public long SeqAluno { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public string TokenSituacao { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public DateTime DataInicioSituacao { get; set; }

        public long? SeqAlunoHistoricoCicloLetivo { get; set; }

        public string Observacao { get; set; }

        public long? SeqPeriodoIntercambio { get; set; }

        /// <summary>
        /// Caso não exista aluno histórico ciclo letivo para o histórico atual, cria o mesmo
        /// </summary>
        public bool CriarAlunoHistoricoCicloLetivo { get; set; }

        public long? SeqArquivoAnexado { get; set; }

        public ArquivoAnexado ArquivoAnexado { get; set; }

        public DateTime DataExclusao { get; set; }

        public string UsuarioExclusao { get; set; }

        public string ObservacaoExclusao { get; set; }

    }
}