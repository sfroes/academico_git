using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using SMC.SGA.Administrativo.Areas.PES.Views.Mensagem.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class MensagemDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoMensagemService), nameof(IInstituicaoNivelTipoMensagemService.BuscarTiposMensagemSelect), values: new[] { nameof(SeqPessoaAtuacao), nameof(ApenasCadastroManual) })]
        public List<SMCDatasourceItem> TiposMensagem { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        public bool ApenasCadastroManual { get; } = true;

        [SMCHidden]
        [SMCParameter]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqMensagem { get; set; }

        [SMCConditionalReadonly(nameof(TipoMensagemBloqueado), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid16_24)]
        [SMCSelect(nameof(TiposMensagem), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        public long SeqTipoMensagem { get; set; }

        [SMCHidden]
        public bool PermitirOcorrenciaCicloLetivoAnterior { get; set; }

        [SMCHidden]
        public DateTime DataLimiteInicioVigencia { get; set; }

        [SMCConditionalRequired(nameof(PermitirOcorrenciaCicloLetivoAnterior), SMCConditionalOperation.Equals, false)]
        [SMCConditionalDisplay(nameof(PermitirOcorrenciaCicloLetivoAnterior), false)]
        [SMCOrder(2)]
        [SMCMinDate(nameof(DataLimiteInicioVigencia))]
        [SMCMaxDate(nameof(DataFimVigencia))]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCConditionalRequired(nameof(PermitirOcorrenciaCicloLetivoAnterior), SMCConditionalOperation.Equals, true)]
        [SMCConditionalDisplay(nameof(PermitirOcorrenciaCicloLetivoAnterior), true)]
        [SMCOrder(2)]
        [SMCMinDate(nameof(DataLimiteInicioVigencia))]
        [SMCMaxDate(nameof(DataFimVigencia))]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public DateTime DataInicioVigenciaCicloAnterior { get; set; }

        [SMCDependency(nameof(SeqTipoMensagem), nameof(MensagemController.DataFimObigratorio), "Mensagem", false)]
        [SMCHidden]
        public bool DataFimObrigatoria { get; set; }

        [SMCConditionalRequired(nameof(DataFimObrigatoria), SMCConditionalOperation.Equals, true)]
        [SMCMinDate(nameof(DataInicioVigencia))]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public DateTime? DataFimVigencia { get; set; }

        [SMCHtml]
        [SMCRequired]
        [SMCMultiline(Rows = 3)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDependency(nameof(SeqTipoMensagem), nameof(MensagemController.BuscarMensagemPadrao), "Mensagem", false, includedProperties: new string[] { nameof(SeqPessoaAtuacao), nameof(Seq) })]
        public string Descricao { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoMensagem), nameof(MensagemController.ArquivoObrigatorio), "Mensagem", true, includedProperties: new string[] { nameof(SeqPessoaAtuacao) })]
        public bool? ArquivoObrigatorio { get; set; }

        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCConditionalRequired(nameof(ArquivoObrigatorio), SMCConditionalOperation.Equals, true)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCHidden]
        public bool AlunoFormado { get; set; }

        #region [PropsControle]

        [SMCIgnoreProp]
        public string MensagemAssert { get; set; }

        [SMCHidden]
        public bool TipoMensagemBloqueado { get; set; }

        #endregion [PropsControle]

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Grid(allowSort: false)
                .ConfigureButton((botao, item, action) =>
                {
                    var itemParsed = item as MensagemListarDynamicModel;
                    if ((action == SMCDynamicButtonAction.Edit || action == SMCDynamicButtonAction.Remove) && !itemParsed.CadastroManual)
                        botao.Url("").Enabled(false).ButtonInstructions("Esta mensagem não pode ser alterada/excluída pois não é uma mensagem de cadastro manual.");
                    else //Tem que remover o texto do instructions senão aparece o texto do botão anterior
                        botao.Enabled(true).ButtonInstructions(string.Empty);
                })
                .Service<IMensagemPessoaAtuacaoService>(index: nameof(IMensagemPessoaAtuacaoService.ListarMensagens),
                                                        insert: nameof(IMensagemPessoaAtuacaoService.BuscarConfiguracaoMensagem),
                                                        save: nameof(IMensagemPessoaAtuacaoService.SalvarMensagem),
                                                        edit: nameof(IMensagemPessoaAtuacaoService.BuscarMensagem),
                                                        delete: nameof(IMensagemPessoaAtuacaoService.ExcluirMensagem))
                //.Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao, ((MensagemListarDynamicModel)x).DescricaoTipoMensagem))
                .Messages(x => ((MensagemListarDynamicModel)x).MensagemExcluir)
                .Tokens(tokenList: UC_PES_005_02_01.PESQUISAR_MENSAGEM,
                        tokenEdit: UC_PES_005_02_02.MANTER_MENSAGEM,
                        tokenRemove: UC_PES_005_02_02.MANTER_MENSAGEM,
                        tokenInsert: UC_PES_005_02_02.MANTER_MENSAGEM)
                .ButtonBackIndex("Index", "Aluno", x => new { area = "ALN" })
                .HeaderIndex("BuscarCabecalho")
                .Header("BuscarCabecalho")
                .ViewPartialEdit("_Edit")
                .ViewPartialInsert("_Edit")
                .Assert(nameof(UIResource.Mensagem_AlunoFormado), m => ((MensagemDynamicModel)m).AlunoFormado)
                .Assert("_AssertMensagemToken", (service, model) => {
                    return service.Create<IMensagemPessoaAtuacaoService>()
                           .validarMensagemAssert(((MensagemDynamicModel)model).Transform<MensagemData>());
                },
                (service, model) => {
                    var modelRetorno = (MensagemDynamicModel)model;
                    modelRetorno.MensagemAssert =  service.Create<IMensagemPessoaAtuacaoService>()
                                                    .BuscarMensagemAssert(((MensagemDynamicModel)model).Transform<MensagemData>());
                    return modelRetorno;
                });
        }

        #endregion Configurações
    }
}