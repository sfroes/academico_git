using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.SGA.Administrativo.Areas.ORG.Views.EntidadeImagem.App_LocalResources;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;


namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class EntidadeImagemDynamicModel : SMCDynamicViewModel
    {
        #region DataSource

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoEnsinoService), nameof(IInstituicaoEnsinoService.BuscarInstituicoesEnsinoSelect), values: new string[] { nameof(IgnorarFiltroDadosInstituicao) })]
        public List<SMCDatasourceItem> InstituicoesEnsino { get; set; }

        [SMCIgnoreProp]
        public bool IgnorarFiltroDadosInstituicao { get; set; } = true;

        #endregion


        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCOrder(0)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCSelect(nameof(InstituicoesEnsino), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCReadOnly]
        public long SeqEntidade { get; set; }


        [SMCRequired]
        [SMCOrder(1)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCSelect(AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24)]
        public TipoImagem TipoImagem { get; set; }

        [SMCOrder(2)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", 
                 ControllerDownload = "Home", HideDescription = true, MaxFileSize = 1572864, // 1.5 × 1.048.576 (1,5 MB)
                 AllowedFileExtensions = new string[] { "jpg", "jpeg", "png" })]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCSize(SMCSize.Grid16_24)]
        [SMCRequired]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCHidden]
        public long SeqArquivoAnexado { get; set; }


        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .RegisterControls(RegisterHelperControls.Upload)
                   .Messages(x => UIResource.Listar_Excluir_Confirmacao)
                   .ButtonBackIndex("Index", "InstituicaoEnsino", x => new { area = "ORG" })
                   .IgnoreFilterGeneration()
                   .HeaderIndex("BuscarCabecalho")
                   .Service<IEntidadeImagemService>(edit: nameof(IEntidadeImagemService.BuscarEntidadeImagem),
                                                    index: nameof(IEntidadeImagemService.BuscarEntidadeImagens),
                                                    save: nameof(IEntidadeImagemService.SalvarEntidadeImagem),
                                                    delete: nameof(IEntidadeImagemService.ExcluirEntidadeImagem))
                   .Tokens(tokenList: UC_ORG_001_12_01.PESQUISAR_ASSOCIACAO_IMAGEM,
                           tokenInsert: UC_ORG_001_12_02.MANTER_ASSOCIACAO_IMAGEM,
                           tokenEdit: UC_ORG_001_12_02.MANTER_ASSOCIACAO_IMAGEM,
                           tokenRemove: UC_ORG_001_12_02.MANTER_ASSOCIACAO_IMAGEM);

        }

    }
}