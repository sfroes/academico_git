using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CSO.Views.InstituicaoNivelModalidade.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class InstituicaoNivelModalidadeDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        [SMCOrder(0)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect(nameof(InstituicaoNiveis))]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCCssClass("smc-size-md-8 smc-size-xs-8 smc-size-sm-8 smc-size-lg-8")]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCOrder(1)]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect(nameof(Modalidades), "Seq", "Descricao", SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqModalidade { get; set; }

        [SMCDataSource("Modalidade")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICSODynamicService))]
        public List<SMCDatasourceItem> Modalidades { get; set; }

        [SMCCssClass("smc-size-md-8 smc-size-xs-8 smc-size-sm-8 smc-size-lg-8")]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("Modalidade")]
        [SMCMapProperty("Modalidade.Descricao")]
        [SMCOrder(2)]
        [SMCSortable(true, true, "Modalidade.Descricao")]
        public string DescricaoModalidade { get; set; }

        [SMCDetail(min: 1)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCInclude("TiposEntidadeLocalidade.TipoEntidadeLocalidade")]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(2, 2)]
        [SMCUnique]
        public SMCMasterDetailList<InstituicaoNivelModalidadeLocalidadeMasterDetailViewModel> TiposEntidadeLocalidade { get; set; }

        [SMCCssClass("smc-size-md-8 smc-size-xs-8 smc-size-sm-8 smc-size-lg-8")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Filter | SMCViewMode.Insert)]
        [SMCInclude("TiposEntidadeLocalidade.TipoEntidadeLocalidade")]
        [SMCMapMethod("RecuperarTiposEntidadeLocalidadeDescricao")]
        [SMCOrder(3)]
        public List<string> TiposEntidadeLocalidadeDescricao { get; set; }

        //[SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        //[SMCOrder(3)]
        //[SMCSelect(nameof(TiposEntidadeLocalidade))]
        //[SMCSize(SMCSize.Grid8_24)]
        //public long? SeqTipoEntidadeLocalidade { get; set; }

        //[SMCDataSource()]
        //[SMCIgnoreProp]
        //[SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarTipoEntidadesSuperioresCursoUnidadeDaInstituicaoSelect))]
        //public List<SMCDatasourceItem> TiposEntidadeLocalidade { get; set; }

        //[SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        //[SMCInclude("TipoEntidadeLocalidade")]
        //[SMCMapProperty("TipoEntidadeLocalidade.Descricao")]
        //[SMCOrder(3)]
        //public string DescricaoTipoEntidadeLocalidade { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelModalidadeDynamicModel)x).DescricaoModalidade,
                                ((InstituicaoNivelModalidadeDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenList: UC_CSO_003_02_01.MANTER_MODALIDADE_INSTITUICAO_NIVEL,
                           tokenInsert: UC_CSO_003_02_01.MANTER_MODALIDADE_INSTITUICAO_NIVEL,
                           tokenEdit: UC_CSO_003_02_01.MANTER_MODALIDADE_INSTITUICAO_NIVEL,
                           tokenRemove: UC_CSO_003_02_01.MANTER_MODALIDADE_INSTITUICAO_NIVEL);
        }
    }
}