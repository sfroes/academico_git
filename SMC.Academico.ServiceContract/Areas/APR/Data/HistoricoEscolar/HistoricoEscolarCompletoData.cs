using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class HistoricoEscolarCompletoData : ISMCMappable
    {
        #region Dados da entidade

        public long Seq { get; set; }

        public long SeqAlunoHistorico { get; set; }

        public long SeqCicloLetivo { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public SituacaoHistoricoEscolar SituacaoHistoricoEscolar { get; set; }

        public long? SeqEscalaApuracaoItem { get; set; }

        public short? Nota { get; set; }

        public short? Faltas { get; set; }

        public decimal? PercentualFrequencia { get; set; }

        public short? CargaHorariaRealizada { get; set; }

        public short? Credito { get; set; }

        public bool Optativa { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public List<HistoricoEscolarColaboradorData> Colaboradores { get; set; }

        #endregion Dados da entidade

        #region Dados adicionais

        public long SeqAluno { get; set; }

        public long? SeqCriterioAprovacao { get; set; }

        public string SituacaoFinal { get; set; }

        public long SeqTurma { get; set; }

        public string DescricaoOferta { get; set; }

        public string DescricaoAluno { get; set; }

        public long RegistroAcademico { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public string DescricaoTurma { get; set; }

        public string Apuracao { get; set; }

        public bool ExibirApuracao { get; set; }

        public bool ExibirNota { get; set; }

        public bool ExibirFaltas { get; set; }

        #endregion Dados adicionais
    }
}