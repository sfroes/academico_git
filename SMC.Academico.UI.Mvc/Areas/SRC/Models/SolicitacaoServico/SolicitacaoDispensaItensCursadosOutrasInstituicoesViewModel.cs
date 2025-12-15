using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "ItemCursado", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DadosDoProfessor", Size = SMCSize.Grid24_24)]
    public class SolicitacaoDispensaItensCursadosOutrasInstituicoesViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoDispensa { get; set; }

        [SMCMaxLength(255)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid18_24)]
        public string Instituicao { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCSelect("CiclosLetivos", UseCustomSelect = true)]
        public long SeqCicloLetivo { get; set; }

        [SMCMaxLength(255)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCGroupedProperty("ItemCursado")]
        [SMCSize(SMCSize.Grid24_24)]
        public string Dispensa { get; set; }

        [SMCConditionalRequired(nameof(Credito), SMCConditionalOperation.Equals, "")]
        [SMCMask("999")]
        [SMCOrder(4)]
        [SMCGroupedProperty("ItemCursado")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public short? CargaHoraria { get; set; }

        [SMCConditionalReadonly(nameof(CargaHoraria), SMCConditionalOperation.Equals, "")]
        [SMCConditionalRequired(nameof(CargaHoraria), SMCConditionalOperation.NotEqual, "")]
        [SMCOrder(5)]
        [SMCGroupedProperty("ItemCursado")]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        [SMCConditionalRequired(nameof(CargaHoraria), SMCConditionalOperation.Equals, "")]
        [SMCMask("999")]
        [SMCOrder(6)]
        [SMCGroupedProperty("ItemCursado")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public short? Credito { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCMaxLength(255)]
        [SMCOrder(7)]
        [SMCGroupedProperty("DadosDoProfessor")]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid18_24)]
        public string NomeProfessor { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(8)]
        [SMCGroupedProperty("DadosDoProfessor")]
        [SMCSelect("Titulacoes")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqTitulacaoProfessor { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCDecimalDigits(1)]
        [SMCMaxValue(100)]
        [SMCOrder(9)]
        [SMCGroupedProperty("ItemCursado")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public decimal? Nota { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCMaxLength(50)]
        [SMCOrder(10)]
        [SMCGroupedProperty("ItemCursado")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string Conceito { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCMultiline]
        [SMCOrder(11)]
        [SMCHidden(SMCViewMode.List)]
        public string Observacao { get; set; }
    }
}