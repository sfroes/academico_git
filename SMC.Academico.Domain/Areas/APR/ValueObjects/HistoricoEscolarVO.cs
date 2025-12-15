using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class HistoricoEscolarVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqAluno { get; set; }

        public long SeqAlunoHistorico { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public bool? ComponenteOptativoMatriz { get; set; }

        public bool Optativa { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public bool? ComponenteCurricularExigeAssunto { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public long? SeqCriterioAprovacao { get; set; }

        public string DescricaoCriterioAprovacao { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public decimal? Nota { get; set; }

        public long? SeqEscalaApuracaoItem { get; set; }

        public short? Faltas { get; set; }

        public string SituacaoFinal { get; set; }

        #region [ Campos para configuração da view e cálculo de situação final ]

        public long? SeqEscalaApuracao { get; set; }

        public List<SMCDatasourceItem> EscalaApuracaoItens { get; set; }

        public bool? IndicadorApuracaoEscala { get; set; }

        public bool? IndicadorApuracaoFrequencia { get; set; }

        public bool? IndicadorApuracaoNota { get; set; }

        public short? PercentualMinimoFrequencia { get; set; }

        public short? PercentualMinimoNota { get; set; }

        public short? NotaMaxima { get; set; }

        [SMCMapProperty("CargaHorariaRealizada")]
        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        #endregion [ Campos para configuração da view e cálculo de situação final ]

        public List<HistoricoEscolarColaboradorVO> Colaboradores { get; set; }

        public bool VinculoAlunoExigeOfertaMatrizCurricular { get; set; }

        public bool ConsiderarMatriz { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        public bool ComponenteCurricularGestaoExame { get; set; }

        public DateTime? DataExame { get; set; }

		public TipoArredondamento? TipoArredondamento { get; set; }

        public long? SeqSolicitacaoDispensa { get; set; }

        public string Observacao { get; set; }

        public bool SomenteLeitura { get; set; }

        public decimal? PercentualFrequencia { get; set; }

        public SituacaoHistoricoEscolar SituacaoHistoricoEscolar { get; set; }
    }
}