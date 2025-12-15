using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaParametrosDetalheViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoComponente { get; set; }

        [SMCHidden]
        public long SeqDivisaoComponente { get; set; }

        [SMCHidden]
        public long SeqOrigemAvaliacao { get; set; }

        [SMCOrder(1)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid7_24)]
        public string DivisaoComponenteDescricao { get; set; }

        [SMCHidden]
        public bool PermiteGrupo { get; set; }

        [SMCConditionalReadonly(nameof(PermiteGrupo), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCConditionalRequired(nameof(PermiteGrupo), SMCConditionalOperation.NotEqual, false)]
        [SMCMask("999")]
        [SMCMinValue(0)]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid3_24)]
        public short? QuantidadeGrupos { get; set; }

        [SMCMask("999")]
        [SMCMinValue(0)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public short? QuantidadeProfessores { get; set; }

        [SMCConditionalReadonly(nameof(TurmaParametrosOfertaViewModel.SeqCriterioAprovacao), SMCConditionalOperation.Equals, "False", DataAttribute = "apura-frequencia", PersistentValue = true)]
        [SMCOrder(4)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public bool ApurarFrequencia { get; set; }

        [SMCConditionalReadonly(nameof(TurmaParametrosOfertaViewModel.SeqCriterioAprovacao), SMCConditionalOperation.NotEqual, "True", DataAttribute = "apura-nota" )]
        [SMCConditionalRequired(nameof(TurmaParametrosOfertaViewModel.SeqCriterioAprovacao), SMCConditionalOperation.NotEqual, "True", DataAttribute = "apura-nota", RuleName = "RuleNR")]
        [SMCConditionalRequired(nameof(TurmaParametrosOfertaViewModel.CriterioDescricaoEscalaApuracao), SMCConditionalOperation.NotEqual, "", RuleName = "RuleCR")]
        [SMCConditionalRule("RuleNR && RuleCR")]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid3_24)]
        public short? NotaMaxima { get; set; }

        [SMCConditionalReadonly(nameof(TurmaParametrosOfertaViewModel.SeqCriterioAprovacao), SMCConditionalOperation.Equals, "True", DataAttribute = "apura-nota", RuleName = "RuleNota")]
        [SMCConditionalReadonly(nameof(TurmaParametrosOfertaViewModel.CriterioDescricaoEscalaApuracao), SMCConditionalOperation.Equals, "", RuleName = "RuleCriterio")]
        [SMCConditionalRule("RuleNota && RuleCriterio")]
        [SMCOrder(6)]
        [SMCSelect(nameof(TurmaDynamicModel.EscalasApuracao))]
        [SMCSize(SMCSize.Grid3_24)]
        public long? SeqEscalaApuracao { get; set; }

        [SMCHidden]
        public bool? MateriaLecionadaObrigatoria { get; set; }
    }
}