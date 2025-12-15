using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class IngressanteOfertaDetailViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [CampanhaOfertaLookup(useCustomFilter: false)]
        [SMCConditionalReadonly(nameof(IngressanteDynamicModel.SeqCampanha), requiredValue: "", RuleName = "RReadOnlyOferta_SeqCampanha")]
        [SMCConditionalReadonly(nameof(IngressanteDynamicModel.SeqCicloLetivo), requiredValue: "", RuleName = "RReadOnlyOferta_SeqCicloLetivo")]
        [SMCConditionalReadonly(nameof(IngressanteDynamicModel.SeqEntidadeResponsavel), requiredValue: "", RuleName = "RReadOnlyOferta_SeqEntidadeResponsavel")]
        [SMCConditionalReadonly(nameof(IngressanteDynamicModel.SeqNivelEnsino), requiredValue: "", RuleName = "RReadOnlyOferta_SeqNivelEnsino")]
        [SMCConditionalReadonly(nameof(IngressanteDynamicModel.SeqProcessoSeletivo), requiredValue: "", RuleName = "RReadOnlyOferta_SeqProcessoSeletivo")]
        [SMCConditionalReadonly(nameof(IngressanteDynamicModel.SeqTipoVinculoAluno), requiredValue: "", RuleName = "RReadOnlyOferta_SeqTipoVinculoAluno")]
        [SMCConditionalReadonly(nameof(IngressanteDynamicModel.DadosAcademicosSomenteLeitura), requiredValue: true, RuleName = "RReadOnlyOferta_DadosAcademicosSomenteLeitura", PersistentValue = true)]
        [SMCConditionalRule("RReadOnlyOferta_SeqCampanha || RReadOnlyOferta_SeqCicloLetivo || RReadOnlyOferta_SeqEntidadeResponsavel || RReadOnlyOferta_SeqNivelEnsino || RReadOnlyOferta_SeqProcessoSeletivo || RReadOnlyOferta_SeqTipoVinculoAluno || RReadOnlyOferta_DadosAcademicosSomenteLeitura")]
        [SMCDependency(nameof(IngressanteDynamicModel.Ativas))]
        [SMCDependency(nameof(IngressanteDynamicModel.SeqCampanha))]
        [SMCDependency(nameof(IngressanteDynamicModel.SeqCicloLetivo))]
        [SMCDependency(nameof(IngressanteDynamicModel.SeqEntidadeResponsavel))]
        [SMCDependency(nameof(IngressanteDynamicModel.SeqNivelEnsino))]
        [SMCDependency(nameof(IngressanteDynamicModel.SeqProcessoSeletivo))]
        [SMCDependency(nameof(IngressanteDynamicModel.SeqTipoVinculoAluno))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid22_24)]
        [SMCUnique]
        public CampanhaOfertaLookupViewModel SeqCampanhaOferta { get; set; }

        [SMCHidden]
        public long? SeqCampanhaOfertaOrigem { get; set; }

        [SMCHidden]
        public long? SeqInscricaoOfertaGpi { get; set; }

        [SMCHidden]
        public long? SeqCampanhaOfertaItem { get; set; }

        [SMCHidden]
        public bool? InteressePermanecerOfertaOrigem { get; set; }
    }
}