using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Controllers;
using SMC.SGA.Administrativo.Areas.ORG.Views.AtoNormativo.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "DadosGerais", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DadosPublicacao", Size = SMCSize.Grid24_24)]
    public class AtoNormativoDynamicModel : SMCDynamicViewModel
    {
        #region [DataSource]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IAssuntoNormativoService), nameof(IAssuntoNormativoService.BuscarAssuntosNormativoSelect), values: new string[] { "Ativo = true" })]
        public List<SMCDatasourceItem> AssuntosNormativos { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoAtoNormativoService), nameof(ITipoAtoNormativoService.BuscarTiposAtoNormativoSelect), values: new string[] { "Ativo = true" })]
        public List<SMCDatasourceItem> TiposAtoNormativos { get; set; }

        #endregion [DataSource]

        [SMCHidden]
        [SMCIgnoreProp]
        public string DescricaoAssuntosNormativos { get; set; }

        [SMCHidden]
        [SMCIgnoreProp]
        public string DescricaoTiposAtoNormativos { get; set; }

        [SMCKey]
        [SMCGroupedProperty("DadosGerais")]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCGroupedProperty("DadosGerais")]
        [SMCRequired]
        [SMCSelect(nameof(AssuntosNormativos), NameDescriptionField = nameof(DescricaoAssuntosNormativos))]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid7_24)]
        public long SeqAssuntoNormativo { get; set; }

        [SMCGroupedProperty("DadosGerais")]
        [SMCRequired]
        [SMCSelect(nameof(TiposAtoNormativos), NameDescriptionField = nameof(DescricaoTiposAtoNormativos))]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid7_24)]
        public long SeqTipoAtoNormativo { get; set; }

        [SMCGroupedProperty("DadosGerais")]
        [SMCMaxLength(50)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public string NumeroDocumento { get; set; }

        [SMCHidden]
        [SMCGroupedProperty("DadosGerais")]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCGroupedProperty("DadosGerais")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]

        public DateTime DataDocumento { get; set; }

        [SMCSelect]
        [SMCGroupedProperty("DadosPublicacao")]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid8_24)]

        public VeiculoPublicacao? VeiculoPublicacao { get; set; }

        [SMCHidden]
        [SMCGroupedProperty("DadosPublicacao")]
        [SMCDependency(nameof(VeiculoPublicacao), nameof(AtoNormativoController.HabilitaCampos), "AtoNormativo", false)]
        public bool HabilitaCampo { get; set; }

        [SMCGroupedProperty("DadosPublicacao")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCConditional(SMCConditionalBehavior.ReadOnly, nameof(HabilitaCampo), SMCConditionalOperation.Equals, false)]
        [SMCConditional(SMCConditionalBehavior.Required, nameof(HabilitaCampo), SMCConditionalOperation.Equals, true)]
        public int? NumeroPublicacao { get; set; }

        [SMCGroupedProperty("DadosPublicacao")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCConditional(SMCConditionalBehavior.ReadOnly, nameof(HabilitaCampo), SMCConditionalOperation.Equals, false)]
        [SMCConditional(SMCConditionalBehavior.Required, nameof(HabilitaCampo), SMCConditionalOperation.Equals, true)]
        public int? NumeroSecaoPublicacao { get; set; }

        [SMCGroupedProperty("DadosPublicacao")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCConditional(SMCConditionalBehavior.ReadOnly, nameof(HabilitaCampo), SMCConditionalOperation.Equals, false)]
        [SMCConditional(SMCConditionalBehavior.Required, nameof(HabilitaCampo), SMCConditionalOperation.Equals, true)]
        public int? NumeroPaginaPublicacao { get; set; }


        [SMCGroupedProperty("DadosPublicacao")]
        [SMCMinDate(nameof(DataDocumento))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid8_24)]
        [SMCConditional(SMCConditionalBehavior.ReadOnly, nameof(HabilitaCampo), SMCConditionalOperation.Equals, false)]
        [SMCConditional(SMCConditionalBehavior.Required, nameof(HabilitaCampo), SMCConditionalOperation.Equals, true)]
        public DateTime? DataPublicacao { get; set; }

        [SMCMinDate(nameof(DataDocumento))]
        [SMCGroupedProperty("DadosGerais")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]

        public DateTime? DataPrazoValidade { get; set; }

        [SMCDescription]
        [SMCGroupedProperty("DadosGerais")]
        [SMCDependency(nameof(Seq), nameof(AtoNormativoController.ManipularDescricao), "AtoNormativo", false, new string[] { nameof(SeqAssuntoNormativo), nameof(SeqTipoAtoNormativo), nameof(NumeroDocumento), nameof(DataDocumento), nameof(VeiculoPublicacao), nameof(NumeroPublicacao), nameof(NumeroSecaoPublicacao), nameof(NumeroPaginaPublicacao), nameof(DataPublicacao), nameof(Descricao) })]
        [SMCDependency(nameof(SeqAssuntoNormativo), nameof(AtoNormativoController.ManipularDescricao), "AtoNormativo", false, new string[] { nameof(Seq), nameof(SeqTipoAtoNormativo), nameof(NumeroDocumento), nameof(DataDocumento), nameof(VeiculoPublicacao), nameof(NumeroPublicacao), nameof(NumeroSecaoPublicacao), nameof(NumeroPaginaPublicacao), nameof(DataPublicacao), nameof(Descricao) })]
        [SMCDependency(nameof(SeqTipoAtoNormativo), nameof(AtoNormativoController.ManipularDescricao), "AtoNormativo", false, new string[] { nameof(Seq), nameof(SeqAssuntoNormativo), nameof(NumeroDocumento), nameof(DataDocumento), nameof(VeiculoPublicacao), nameof(NumeroPublicacao), nameof(NumeroSecaoPublicacao), nameof(NumeroPaginaPublicacao), nameof(DataPublicacao), nameof(Descricao) })]
        [SMCDependency(nameof(NumeroDocumento), nameof(AtoNormativoController.ManipularDescricao), "AtoNormativo", false, new string[] { nameof(Seq), nameof(SeqAssuntoNormativo), nameof(DataDocumento), nameof(SeqTipoAtoNormativo), nameof(VeiculoPublicacao), nameof(NumeroPublicacao), nameof(NumeroSecaoPublicacao), nameof(NumeroPaginaPublicacao), nameof(DataPublicacao), nameof(Descricao) })]
        [SMCDependency(nameof(DataDocumento), nameof(AtoNormativoController.ManipularDescricao), "AtoNormativo", false, new string[] { nameof(Seq), nameof(SeqAssuntoNormativo), nameof(NumeroDocumento), nameof(SeqTipoAtoNormativo), nameof(VeiculoPublicacao), nameof(NumeroPublicacao), nameof(NumeroSecaoPublicacao), nameof(NumeroPaginaPublicacao), nameof(DataPublicacao), nameof(Descricao) })]
        [SMCDependency(nameof(VeiculoPublicacao), nameof(AtoNormativoController.ManipularDescricao), "AtoNormativo", false, new string[] { nameof(Seq), nameof(SeqAssuntoNormativo), nameof(SeqTipoAtoNormativo), nameof(NumeroDocumento), nameof(DataDocumento), nameof(NumeroPublicacao), nameof(NumeroSecaoPublicacao), nameof(NumeroPaginaPublicacao), nameof(DataPublicacao), nameof(Descricao) })]
        [SMCDependency(nameof(NumeroPublicacao), nameof(AtoNormativoController.ManipularDescricao), "AtoNormativo", false, new string[] { nameof(Seq), nameof(SeqAssuntoNormativo), nameof(SeqTipoAtoNormativo), nameof(NumeroDocumento), nameof(DataDocumento), nameof(VeiculoPublicacao), nameof(NumeroSecaoPublicacao), nameof(NumeroPaginaPublicacao), nameof(DataPublicacao), nameof(Descricao) })]
        [SMCDependency(nameof(NumeroSecaoPublicacao), nameof(AtoNormativoController.ManipularDescricao), "AtoNormativo", false, new string[] { nameof(Seq), nameof(SeqAssuntoNormativo), nameof(SeqTipoAtoNormativo), nameof(NumeroDocumento), nameof(DataDocumento), nameof(VeiculoPublicacao), nameof(NumeroPublicacao), nameof(NumeroPaginaPublicacao), nameof(DataPublicacao), nameof(Descricao) })]
        [SMCDependency(nameof(NumeroPaginaPublicacao), nameof(AtoNormativoController.ManipularDescricao), "AtoNormativo", false, new string[] { nameof(Seq), nameof(SeqAssuntoNormativo), nameof(SeqTipoAtoNormativo), nameof(NumeroDocumento), nameof(DataDocumento), nameof(VeiculoPublicacao), nameof(NumeroPublicacao), nameof(NumeroSecaoPublicacao), nameof(DataPublicacao), nameof(Descricao) })]
        [SMCDependency(nameof(DataPublicacao), nameof(AtoNormativoController.ManipularDescricao), "AtoNormativo", false, new string[] { nameof(Seq), nameof(SeqAssuntoNormativo), nameof(SeqTipoAtoNormativo), nameof(NumeroDocumento), nameof(DataDocumento), nameof(VeiculoPublicacao), nameof(NumeroPublicacao), nameof(NumeroSecaoPublicacao), nameof(NumeroPaginaPublicacao), nameof(Descricao) })]
        [SMCMaxLength(255)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid20_24)]
        public string Descricao { get; set; }

        [SMCGroupedProperty("DadosPublicacao")]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCUrl]
        public string EnderecoEletronico { get; set; }

        [SMCHidden]
        [SMCGroupedProperty("DadosPublicacao")]
        public long? SeqArquivoAnexado { get; set; }

        [SMCGroupedProperty("DadosPublicacao")]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true, MaxFileSize = 26214400, AllowedFileExtensions = new string[] { "doc", "docx", "xls", "xlsx", "jpg", "jpeg", "png", "pdf", "rar", "zip", "ps", "txt", "bmp" })]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid20_24)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        /// <summary>
        /// Propriedade pra controle
        /// </summary>
        [SMCHidden]
        public bool Ativo => true;

        #region [Propriedade: BI_ORG_002 - Atos Normativos da Entidade] 

        [SMCParameter]
        [SMCIgnoreProp]
        public long? SeqInstituicaoOrigem { get; set; }

        [SMCParameter]
        [SMCIgnoreProp]
        public long? SeqEntidadeOrigem { get; set; }

        [SMCParameter]
        [SMCIgnoreProp]
        public long? SeqProgramaOrigem { get; set; }

        [SMCParameter]
        [SMCIgnoreProp]
        public long? SeqCursoOfertaLocalidadeOrigem { get; set; }

        [SMCParameter]
        [SMCIgnoreProp]
        public long? SeqEntidadeCursoOfertaLocalidadeOrigem { get; set; }

        #endregion [Propriedade: BI_ORG_002 - Atos Normativos da Entidade]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal()
                .ModalSize(SMCModalWindowSize.Largest)
                .ButtonBackEdit((controller, model) =>
                {
                    var modelDynamic = (model as AtoNormativoDynamicModel);
                    if (modelDynamic.SeqInstituicaoOrigem.HasValue)
                    {
                        return controller.Url.Action("Editar", "InstituicaoEnsino", new { Seq = SMCEncryptedLong.GetStringValue(modelDynamic.SeqInstituicaoOrigem), Area = "ORG" });
                    }
                    if (modelDynamic.SeqEntidadeOrigem.HasValue)
                    {
                        return controller.Url.Action("Editar", "Entidade", new { Seq = SMCEncryptedLong.GetStringValue(modelDynamic.SeqEntidadeOrigem), Area = "ORG" });
                    }
                    if (modelDynamic.SeqProgramaOrigem.HasValue)
                    {
                        return controller.Url.Action("Editar", "Programa", new { Seq = SMCEncryptedLong.GetStringValue(modelDynamic.SeqProgramaOrigem), Area = "CSO" });
                    }
                    if (modelDynamic.SeqCursoOfertaLocalidadeOrigem.HasValue && modelDynamic.SeqEntidadeCursoOfertaLocalidadeOrigem.HasValue)
                    {
                        return controller.Url.Action("Editar", "CursoOfertaLocalidade", new
                        {
                            Seq = SMCEncryptedLong.GetStringValue(modelDynamic.SeqCursoOfertaLocalidadeOrigem),
                            SeqEntidade = SMCEncryptedLong.GetStringValue(modelDynamic.SeqEntidadeCursoOfertaLocalidadeOrigem),
                            Area = "CSO"
                        });
                    }
                    return controller.Url.Action("Index", "AtoNormativo");
                })
                .Button("AssociacaoEntidades", "Index", "AssociacaoEntidades",
                            (model) => new { seqAtoNormativo = SMCDESCrypto.EncryptNumberForURL((model as AtoNormativoListarDynamicModel).Seq) })
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao, (x as AtoNormativoListarDynamicModel).Descricao))
                .Tokens(tokenInsert: UC_ORG_003_03_02.MANTER_ATO_NORMATIVO,
                           tokenEdit: UC_ORG_003_03_02.MANTER_ATO_NORMATIVO,
                           tokenRemove: UC_ORG_003_03_02.MANTER_ATO_NORMATIVO,
                           tokenList: UC_ORG_003_03_01.PESQUISAR_ATO_NORMATIVO)
                .Service<IAtoNormativoService>(index: nameof(IAtoNormativoService.BuscarAtosNormativos),
                                               edit: nameof(IAtoNormativoService.BuscarAtoNormativo),
                                               save: nameof(IAtoNormativoService.SalvarAtoNormativo));
        }
    }
}