using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class TipoDivisaoComponenteViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid2_24)]
        [SMCSortable(true)]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoComponenteCurricular { get; set; }

        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid13_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid13_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid5_24)]
        public TipoGestaoDivisaoComponente TipoGestaoDivisaoComponente { get; set; }

        [SMCConditionalReadonly(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.NotEqual, TipoGestaoDivisaoComponente.Turma)]
        [SMCConditionalRequired(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.Equals, TipoGestaoDivisaoComponente.Turma)]
        [SMCOrder(4)]       
        [SMCSelect("Modalidades", "Seq", "Descricao")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public long? SeqModalidade { get; set; }

        [SMCConditionalReadonly(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.NotEqual, TipoGestaoDivisaoComponente.Turma)]
        [SMCConditionalRequired(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.Equals, TipoGestaoDivisaoComponente.Turma)]
        [SMCOrder(5)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public bool GeraOrientacao { get; set; }               

        [SMCConditionalReadonly(nameof(GeraOrientacao), SMCConditionalOperation.NotEqual, "True")]
        [SMCConditionalRequired(nameof(GeraOrientacao), SMCConditionalOperation.Equals, "True")]
        [SMCOrder(6)]
        [SMCSelect("TiposOrientacao", "Seq", "Descricao")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid8_24)]
        public long? SeqTipoOrientacao { get; set; }

        [SMCConditionalReadonly(nameof(GeraOrientacao), SMCConditionalOperation.NotEqual, "True")]
        [SMCConditionalRequired(nameof(GeraOrientacao), SMCConditionalOperation.Equals, "True")]
        [SMCOrder(7)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid8_24)]
        public TipoParticipacaoOrientacao? TipoParticipacaoOrientacao { get; set; }

        [SMCConditionalReadonly(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.NotEqual, TipoGestaoDivisaoComponente.EntregaComprovante)]
        [SMCConditionalRequired(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.Equals, TipoGestaoDivisaoComponente.EntregaComprovante)]
        [SMCMapForceFromTo]
        [SMCOrder(8)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public bool? Artigo { get; set; }
    }
}