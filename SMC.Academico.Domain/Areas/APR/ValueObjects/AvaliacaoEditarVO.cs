using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Models;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class AvaliacaoEditarVO : ISMCMappable
    {
        public long? SeqAgendaTurma { get; set; }

        public int? CodigoUnidade { get; set; }

        public TipoOrigemAvaliacao TipoOrigemAvaliacao { get; set; }

        public bool? EntregaWebInBD { get; set; }

        public long Seq { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public TipoAvaliacao? TipoAvaliacao { get; set; }

        public string Sigla { get; set; }

        public string Descricao { get; set; }

        public short? Valor { get; set; }

        public bool? EntregaWeb { get; set; }

        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }

        public bool HorarioGrade { get; set; }

        public DateTime? DataInicioAplicacaoAvaliacao { get; set; }

        public DateTime? DataFimAplicacaoAvaliacao { get; set; }

        public long? SeqInicioGradeAvaliacao { get; set; }

        public long? SeqFimGradeAvaliacao { get; set; }

        public short? QuantidadeMaximaPessoasGrupo { get; set; }

        public string Instrucao { get; set; }

        public long? SeqArquivoAnexadoInstrucao { get; set; }

        public ArquivoAnexado ArquivoAnexadoInstrucao { get; set; }

        public long? LocalSEF { get; set; }

        public string Local { get; set; }

        public bool TemConfiguracaoGrade { get; set; }

        public long SeqAplicacaoAvaliacao { get; set; }

        public long? SeqEventoAgd { get; set; }

        public DateTime DataInicioLimiteAvaliacao { get; set; }
        public DateTime DataFimLimiteAvaliacao { get; set; }

    }
}