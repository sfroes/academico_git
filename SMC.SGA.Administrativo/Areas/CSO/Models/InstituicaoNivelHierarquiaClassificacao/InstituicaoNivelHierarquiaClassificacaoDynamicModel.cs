using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CSO.Views.InstituicaoNivelHierarquiaClassificacao.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class InstituicaoNivelHierarquiaClassificacaoDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect("InstituicaoNiveis")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCOrder(1)]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect("HierarquiasClassificacao", "Seq", "Descricao")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public long SeqHierarquiaClassificacao { get; set; }

        [SMCDataSource("HierarquiaClassificacao")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICSODynamicService))]
        public List<SMCDatasourceItem> HierarquiasClassificacao { get; set; }

        [SMCOrder(2)]
        [SMCSortable(true, true, "HierarquiaClassificacao.Descricao")]
        [SMCMapProperty("HierarquiaClassificacao.Descricao")]
        [SMCInclude("HierarquiaClassificacao")]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        public string DescricaoHierarquiaClassificacao { get; set; }

        [SMCDependency("SeqHierarquiaClassificacao", "BuscarTiposClassificacaoPorHierarquiaSelect", "TipoClassificacao", false)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCOrder(3)]
        [SMCSelect("TiposClassificacao")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public long? SeqTipoClassificacao { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoClassificacaoService), "BuscarTiposClassificacaoPorHierarquiaSelect",
            values: new string[] { nameof(SeqHierarquiaClassificacao), nameof(Exclusivo) })]
        public List<SMCDatasourceItem> TiposClassificacao { get; set; }

        [SMCIgnoreMetadata]
        [SMCHidden]
        private bool Exclusivo { get { return false; } }

        [SMCOrder(3)]
        [SMCMapProperty("TipoClassificacao.Descricao")]
        [SMCInclude("TipoClassificacao")]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        public string DescricaoTipoClassificacao { get; set; }

        [SMCOrder(4)]
        [SMCRequired]
        [SMCRegularExpression(@"^[0-9]*$")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCMask("999")]
        public int? QuantidadeMinima { get; set; }

        [SMCOrder(5)]
        [SMCRegularExpression(@"^[0-9]*$")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCMask("999")]
        public int? QuantidadeMaxima { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelHierarquiaClassificacaoDynamicModel)x).DescricaoHierarquiaClassificacao,
                                ((InstituicaoNivelHierarquiaClassificacaoDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenList: UC_CSO_003_01_01.MANTER_HIERARQUIA_CLASSIFICACAO_INSTITUICAO_NIVEL,
                            tokenInsert: UC_CSO_003_01_01.MANTER_HIERARQUIA_CLASSIFICACAO_INSTITUICAO_NIVEL,
                            tokenEdit: UC_CSO_003_01_01.MANTER_HIERARQUIA_CLASSIFICACAO_INSTITUICAO_NIVEL,
                            tokenRemove: UC_CSO_003_01_01.MANTER_HIERARQUIA_CLASSIFICACAO_INSTITUICAO_NIVEL);
        }
    }
}