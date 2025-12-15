using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ORG.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Controllers;
using SMC.SGA.Administrativo.Areas.ORG.Views.AssociacaoEntidades.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class AssociacaoEntidadesDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqAtoNormativo { get; set; }

        [EntidadeAtoNormativoLookup]
        [SMCDependency(nameof(AtoNormativoDynamicModel.SeqInstituicaoEnsino))]
        [SMCDescription]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24)]
        public EntidadeAtoNormativoLookupViewModel LookupEntidade { get; set; }

        [SMCHidden]
        [SMCIgnoreMetadata]
        public long? SeqEntidade { get { return LookupEntidade?.Seq; } set { LookupEntidade = new EntidadeAtoNormativoLookupViewModel() { Seq = value }; } }

        [SMCHidden]
        [SMCDependency(nameof(LookupEntidade), nameof(AssociacaoEntidadesController.BuscarTokenTipoEntidadeSelect), "AssociacaoEntidades", true)]
        public string TokenTipoEntidade { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(LookupEntidade), nameof(AssociacaoEntidadesController.EntidadeCursoOfertaLocalidadeExigeGrau), "AssociacaoEntidades", true)]
        public bool HabilitaCampo { get; set; }

        [SMCDependency(nameof(TokenTipoEntidade), nameof(AssociacaoEntidadesController.BuscarGrauAcademicoSelect), "AssociacaoEntidades", true, new string[] { nameof(LookupEntidade), nameof(SeqAtoNormativo), nameof(Seq) })]
        [SMCDependency(nameof(LookupEntidade), nameof(AssociacaoEntidadesController.BuscarGrauAcademicoSelect), "AssociacaoEntidades", true, new string[] { nameof(TokenTipoEntidade), nameof(SeqAtoNormativo), nameof(Seq) })]
        [SMCSelect(autoSelectSingleItem: true)]
        [SMCConditional(SMCConditionalBehavior.Visibility | SMCConditionalBehavior.Required, nameof(HabilitaCampo), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public long? SeqGrauAcademico { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Grid(allowSort: true, hideRowNumber: true)
                .ButtonBackIndex("Index", "AtoNormativo")
                .HeaderIndex("CabecalhoAssociacaoEntidades")
                .EditInModal()
                .ModalSize(SMCModalWindowSize.Large)
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao, (x as AssociacaoEntidadesListarDynamicModel).NomeEntidade))
                .Tokens(tokenInsert: UC_ORG_003_03_04.MANTER_ASSOCIACAO_ENTIDADE,
                           tokenEdit: UC_ORG_003_03_04.MANTER_ASSOCIACAO_ENTIDADE,
                           tokenRemove: UC_ORG_003_03_04.MANTER_ASSOCIACAO_ENTIDADE,
                           tokenList: UC_ORG_003_03_03.PESQUISAR_ASSOCIACAO_ENTIDADE)
                .Service<IAtoNormativoService>(index: nameof(IAtoNormativoService.BuscarAssociacoesEntidades),
                                               edit: nameof(IAtoNormativoService.BuscarAssociacaoEntidades),
                                               save: nameof(IAtoNormativoService.SalvarAssociacaoEntidades),
                                               delete: nameof(IAtoNormativoService.ExcluirAssociacaoEntidade));
        }
    }
}