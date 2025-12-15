using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORG.Views.InstituicaoNivelModeloRelatorio.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoNivelModeloRelatorioDynamicModel : SMCDynamicViewModel
    {
        #region Data Source

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> InstituicaoNiveisEnsino { get; set; }

        #endregion

        [SMCHidden]
        [SMCKey]
        [SMCReadOnly]
        public override long Seq { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect(nameof(InstituicaoNiveisEnsino))]
        [SMCSize(SMCSize.Grid12_24)]
        public long SeqInstituicaoNivel { get; set; }      

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCRequired]
        [SMCSelect(AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid12_24)]
        //[SMCSortable(true, true)]
        public ModeloRelatorio ModeloRelatorio { get; set; }

        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCRequired]
        public SMCUploadFile ArquivoModelo { get; set; }

        [SMCHidden]
        public long SeqArquivoModelo { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .RegisterControls(RegisterHelperControls.Upload)
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao, (x as InstituicaoNivelModeloRelatorioDynamicModel).ModeloRelatorio.SMCGetDescription(), (x as InstituicaoNivelModeloRelatorioDynamicModel).DescricaoInstituicaoNivel))
                   .Tokens(tokenList: UC_ORG_002_06_01.MANTER_MODELO_WORD_RELATORIO_INSTITUICAO_NIVEL,
                           tokenInsert: UC_ORG_002_06_01.MANTER_MODELO_WORD_RELATORIO_INSTITUICAO_NIVEL,
                           tokenEdit: UC_ORG_002_06_01.MANTER_MODELO_WORD_RELATORIO_INSTITUICAO_NIVEL,
                           tokenRemove: UC_ORG_002_06_01.MANTER_MODELO_WORD_RELATORIO_INSTITUICAO_NIVEL)
                   .Service<IInstituicaoNivelModeloRelatorioService>(save: nameof(IInstituicaoNivelModeloRelatorioService.SalvarInstituicaoNivelModeloRelatorio),
                                                                     edit: nameof(IInstituicaoNivelModeloRelatorioService.BuscarInstituicaoNivelModeloRelatorio));
        }
    }
}