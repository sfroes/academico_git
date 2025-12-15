using SMC.Academico.Common.Constants;
using SMC.Academico.UI.Mvc.Areas.ORG.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.ORG.Controllers;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class AtoNormativoEntidadeViewModel : SMCViewModelBase, ISMCMappable
    {
        //    [SMCHidden]
        //    [SMCKey]
        //    public long Seq { get; set; }

        //    [SMCHidden]
        //    public long? SeqAtoNormativo { get; set; }

        //    [EntidadeAtoNormativoLookup]
        //    [SMCDependency(nameof(AtoNormativoDynamicModel.SeqInstituicaoEnsino))]
        //    [SMCDescription]
        //    [SMCOrder(2)]
        //    [SMCRequired]
        //    [SMCSize(SMCSize.Grid14_24)]
        //    public EntidadeAtoNormativoLookupViewModel LookupEntidade { get; set; }

        //    [SMCHidden]
        //    [SMCIgnoreMetadata]
        //    public long? SeqEntidade { get { return LookupEntidade?.Seq; } set { LookupEntidade = new EntidadeAtoNormativoLookupViewModel() { Seq = value }; } }

        //    [SMCHidden]
        //    [SMCDependency(nameof(LookupEntidade), nameof(AtoNormativoController.BuscarTokenTipoEntidadeSelect), "AtoNormativo", true)]
        //    public string TokenTipoEntidade { get; set; }

        //    [SMCHidden]
        //    [SMCDependency(nameof(LookupEntidade), nameof(AtoNormativoController.EntidadeCursoOfertaLocalidadeExigeGrau), "AtoNormativo", true)]
        //    public bool HabilitaCampo { get; set; }

        //    [SMCDependency(nameof(TokenTipoEntidade), nameof(AtoNormativoController.BuscarGrauAcademicoSelect), "AtoNormativo", true, new string[] { nameof(LookupEntidade), nameof(SeqAtoNormativo), nameof(Seq) })]
        //    [SMCDependency(nameof(LookupEntidade), nameof(AtoNormativoController.BuscarGrauAcademicoSelect), "AtoNormativo", true, new string[] { nameof(TokenTipoEntidade), nameof(SeqAtoNormativo), nameof(Seq) })]
        //    [SMCSelect(autoSelectSingleItem: true)]
        //    [SMCConditional(SMCConditionalBehavior.Visibility | SMCConditionalBehavior.Required, nameof(HabilitaCampo), SMCConditionalOperation.Equals, true)]
        //    [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        //    public long? SeqGrauAcademico { get; set; }
    }
}