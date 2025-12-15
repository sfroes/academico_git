using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORG.Views.InstituicaoNivelModeloRelatorio.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoModeloRelatorioDynamicModel : SMCDynamicViewModel
    {
        #region Data Source

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoEnsinoService), nameof(IInstituicaoEnsinoService.BuscarInstituicoesEnsinoSelect))]
        public List<SMCDatasourceItem> InstituicoesEnsino { get; set; }

        #endregion

        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(InstituicoesEnsino))]
        [SMCSize(SMCSize.Grid12_24)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCRequired]
        [SMCSelect(AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid12_24)]
        public ModeloRelatorio ModeloRelatorio { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCSelect(AutoSelectSingleItem = true)]
        [SMCConditionalRequired(nameof(ModeloRelatorio), SMCConditionalOperation.Equals, new object[] { SMC.Academico.Common.Areas.ORG.Enums.ModeloRelatorio.AtaDefesaTrabalhoAcademico })]
        [SMCConditionalReadonly(nameof(ModeloRelatorio), SMCConditionalOperation.NotEqual, new object[] { SMC.Academico.Common.Areas.ORG.Enums.ModeloRelatorio.AtaDefesaTrabalhoAcademico })]
        public Idioma? Idioma { get; set; }

        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
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
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao, (x as InstituicaoModeloRelatorioListarDynamicModel).ModeloRelatorio.SMCGetDescription(), (x as InstituicaoModeloRelatorioListarDynamicModel).DescricaoInstituicaoEnsino))
                   .Tokens(tokenList: UC_ORG_002_10_01.MANTER_PARAMETRO_MODELO_WORD_POR_INSTITUICAO,
                           tokenInsert: UC_ORG_002_10_01.MANTER_PARAMETRO_MODELO_WORD_POR_INSTITUICAO,
                           tokenEdit: UC_ORG_002_10_01.MANTER_PARAMETRO_MODELO_WORD_POR_INSTITUICAO,
                           tokenRemove: UC_ORG_002_10_01.MANTER_PARAMETRO_MODELO_WORD_POR_INSTITUICAO)
                   .Service<IInstituicaoModeloRelatorioService>(index: nameof(IInstituicaoModeloRelatorioService.BuscarInstituicaoModeloRelatorios),
                                                                save: nameof(IInstituicaoModeloRelatorioService.SalvarModeloRelatorio),
                                                                edit: nameof(IInstituicaoModeloRelatorioService.BuscarInstituicaoModeloRelatorio));
        }
    }
}