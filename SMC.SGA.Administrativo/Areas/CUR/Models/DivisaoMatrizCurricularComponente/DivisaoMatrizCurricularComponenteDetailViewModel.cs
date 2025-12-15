using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DivisaoMatrizCurricularComponenteDetailViewModel : SMCViewModelBase
    {
        #region [DataSource]

        //FIX: Utilizar o datasource do mestre nas views
        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> EscalasApuracao { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> ListaTipoPagamentoAula { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> ComprovacaoArtigoSelect { get; set; }

        #endregion

        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDivisaoMatrizCurricularComponente { get; set; }

        [SMCHidden]
        public bool TipoDivisaoComponenteArtigo { get; set; }

        [SMCHidden]
        public long SeqDivisaoComponente { get; set; }

        [SMCOrder(1)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public string DivisaoComponenteDescricao { get; set; }

        [SMCConditionalDisplay(nameof(TipoDivisaoComponenteArtigo), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(TipoDivisaoComponenteArtigo), SMCConditionalOperation.Equals, true)]
        [SMCSelect(nameof(ComprovacaoArtigoSelect))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCOrder(2)]
        public ComprovacaoArtigo? ComprovacaoArtigoMinima { get; set; }

        [SMCConditionalReadonly(nameof(TipoComponenteCurricularTurma), SMCConditionalOperation.Equals, false)]
        [SMCConditionalRequired(nameof(TipoComponenteCurricularTurma), SMCConditionalOperation.Equals, true)]
        [SMCMask("999")]
        [SMCMinValue(0)]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public short? QuantidadeGrupos { get; set; }

        [SMCConditionalReadonly(nameof(TipoComponenteCurricularTurma), SMCConditionalOperation.Equals, false)]
        [SMCMask("999")]
        [SMCMinValue(0)]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public short? QuantidadeProfessores { get; set; }

        [SMCConditionalReadonly(nameof(SeqEscalaApuracao), SMCConditionalOperation.NotEqual, "")]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public short? NotaMaxima { get; set; }

        [SMCOrder(6)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public bool ApurarFrequencia { get; set; }

        [SMCConditionalDisplay(nameof(TipoComponenteCurricularTurma), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(TipoComponenteCurricularTurma), SMCConditionalOperation.Equals, true)]
        [SMCOrder(7)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public bool? MateriaLecionadaObrigatoria { get; set; }

        [SMCConditionalReadonly(nameof(NotaMaxima), SMCConditionalOperation.NotEqual, "")]
        [SMCOrder(8)]
        [SMCSelect(nameof(EscalasApuracao))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public long? SeqEscalaApuracao { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCSelect]
        [SMCConditionalRequired(nameof(DivisaoComponenteCargaHorariaGrade), SMCConditionalOperation.GreaterThen, 0)]
        [SMCConditionalDisplay(nameof(DivisaoComponenteCargaHorariaGrade), SMCConditionalOperation.GreaterThen, 0)]
        public TipoDistribuicaoAula? TipoDistribuicaoAula { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCSelect]
        [SMCConditionalRequired(nameof(DivisaoComponenteCargaHorariaGrade), SMCConditionalOperation.GreaterThen, 0)]
        [SMCConditionalDisplay(nameof(DivisaoComponenteCargaHorariaGrade), SMCConditionalOperation.GreaterThen, 0)]
        public TipoPulaFeriado? TipoPulaFeriado { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        [SMCSelect(nameof(ListaTipoPagamentoAula))]
        [SMCConditionalDisplay(nameof(DivisaoComponenteCargaHorariaGrade), SMCConditionalOperation.GreaterThen, 0)]
        [SMCConditionalRequired(nameof(TipoDistribuicaoAula), SMCConditionalOperation.NotEqual, "", RuleName = "R3")]
        [SMCConditionalRequired(nameof(DivisaoComponenteCargaHorariaGrade), SMCConditionalOperation.GreaterThen, 0, RuleName = "R4")]
        [SMCConditionalRule("R3 && R4")]
        [SMCConditionalReadonly(nameof(TipoDistribuicaoAula), SMCConditionalOperation.Equals, "", RuleName = "R1")]
        [SMCConditionalReadonly(nameof(DivisaoComponenteCargaHorariaGradeAuxiliar), SMCConditionalOperation.Equals, 0, RuleName = "R2")]
        [SMCConditionalRule("R1 || R2")]
        [SMCDependency(nameof(TipoDistribuicaoAula), "BuscarDadosTipoPagamentoAula", "DivisaoMatrizCurricularComponente", true)]
        public TipoPagamentoAula? TipoPagamentoAula { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCCssClass("componente-aula-sabado")]
        [SMCRadioButtonList]
        [SMCConditionalRequired(nameof(DivisaoComponenteCargaHorariaGrade), SMCConditionalOperation.GreaterThen, 0)]
        [SMCConditionalDisplay(nameof(DivisaoComponenteCargaHorariaGrade), SMCConditionalOperation.GreaterThen, 0)]
        public bool? AulaSabado { get; set; }

        [SMCHidden]
        public int? DivisaoComponenteCargaHorariaGrade { get; set; }

        //Propriedade auxiliar para ser usada no ConditionalReadOnly do TipoPagamento, para não ficar com null se não tiver valor
        [SMCHidden]
        public int DivisaoComponenteCargaHorariaGradeAuxiliar
        {
            get
            {
                return DivisaoComponenteCargaHorariaGrade ?? 0;
            }
        }

        [SMCHidden]
        public bool TipoComponenteCurricularTurma { get; set; }
    }
}