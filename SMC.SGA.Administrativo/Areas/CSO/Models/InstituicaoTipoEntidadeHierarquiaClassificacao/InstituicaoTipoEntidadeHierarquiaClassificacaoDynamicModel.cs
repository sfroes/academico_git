using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CSO.Views.InstituicaoTipoEntidadeHierarquiaClassificacao.App_LocalResources;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class InstituicaoTipoEntidadeHierarquiaClassificacaoDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCKey]
        [SMCOrder(0)]
        public override long Seq { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect("InstituicoesTipoEntidade")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public long SeqInstituicaoTipoEntidade { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarInstituicaoTiposEntidadeSelect))]
        public List<SMCDatasourceItem> InstituicoesTipoEntidade { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("InstituicaoTipoEntidade.TipoEntidade")]
        [SMCMapProperty("InstituicaoTipoEntidade.TipoEntidade.Descricao")]
        [SMCOrder(1)]
        [SMCSortable(true, true, "InstituicaoTipoEntidade.TipoEntidade.Descricao")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public string DescricaoInstituicaoTipoEntidade { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect("HierarquiasClassificacao", "Seq", "Descricao", autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public long SeqHierarquiaClassificacao { get; set; }

        [SMCDataSource("HierarquiaClassificacao")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICSODynamicService))]
        public List<SMCDatasourceItem> HierarquiasClassificacao { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("HierarquiaClassificacao")]
        [SMCMapProperty("HierarquiaClassificacao.Descricao")]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCSortable(true, true, "HierarquiaClassificacao.Descricao")]
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

        [SMCHidden]
        [SMCIgnoreMetadata]
        private bool Exclusivo { get { return false; } }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("TipoClassificacao")]
        [SMCMapProperty("TipoClassificacao.Descricao")]
        [SMCOrder(3)]
        public string DescricaoTipoClassificacao { get; set; }

        [SMCOrder(4)]
        [SMCRegularExpression(@"^[0-9]*$")]
        [SMCMinValue(1)]
        [SMCMask("999")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public short? QuantidadeMinima { get; set; }

        [SMCOrder(5)]
        [SMCRegularExpression(@"^[0-9]*$")]
        [SMCMask("999")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public short? QuantidadeMaxima { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoTipoEntidadeHierarquiaClassificacaoDynamicModel)x).DescricaoHierarquiaClassificacao,
                                ((InstituicaoTipoEntidadeHierarquiaClassificacaoDynamicModel)x).DescricaoInstituicaoTipoEntidade))
                   .Tokens(tokenInsert: UC_CSO_003_07_01.MANTER_HIERARQUIA_CLASSIFICACAO_INSTITUICAO_TIPO_ENTIDADE,
                           tokenEdit: UC_CSO_003_07_01.MANTER_HIERARQUIA_CLASSIFICACAO_INSTITUICAO_TIPO_ENTIDADE,
                           tokenRemove: UC_CSO_003_07_01.MANTER_HIERARQUIA_CLASSIFICACAO_INSTITUICAO_TIPO_ENTIDADE,
                           tokenList: UC_CSO_003_07_01.MANTER_HIERARQUIA_CLASSIFICACAO_INSTITUICAO_TIPO_ENTIDADE);

            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("seqInstituicaoTipoEntidade"))
                options.ButtonBackIndex("Index", "InstituicaoTipoEntidade", x => new { area = "ORG" });
        }
        
    }
}