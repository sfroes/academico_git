using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CAM.Views.InstituicaoNivelRegimeLetivo.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class InstituicaoNivelRegimeLetivoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCHidden]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect("InstituicaoNiveisEnsino")]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        [SMCDataSource()]
        public List<SMCDatasourceItem> InstituicaoNiveisEnsino { get; set; }

        [SMCOrder(1)]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCOrder(2)]
        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect("RegimesLetivos", "Seq", "Descricao", autoSelectSingleItem: true, SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqRegimeLetivo { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICAMDynamicService))]
        [SMCDataSource(dataSource: "RegimeLetivo")]
        public List<SMCDatasourceItem> RegimesLetivos { get; set; }

        [SMCOrder(2)]
        [SMCSortable(true, true, "RegimeLetivo.Descricao")]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCMapProperty("RegimeLetivo.Descricao")]
        [SMCInclude("RegimeLetivo")]
        public string DescricaoRegimeLetivo { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelRegimeLetivoDynamicModel)x).DescricaoRegimeLetivo,
                                ((InstituicaoNivelRegimeLetivoDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenInsert: UC_CAM_003_01_01.MANTER_REGIME_LETIVO_INSTITUICAO_NIVEL,
                       tokenEdit: UC_CAM_003_01_01.MANTER_REGIME_LETIVO_INSTITUICAO_NIVEL,
                       tokenRemove: UC_CAM_003_01_01.MANTER_REGIME_LETIVO_INSTITUICAO_NIVEL,
                       tokenList: UC_CAM_003_01_01.MANTER_REGIME_LETIVO_INSTITUICAO_NIVEL);
        }
    }
}