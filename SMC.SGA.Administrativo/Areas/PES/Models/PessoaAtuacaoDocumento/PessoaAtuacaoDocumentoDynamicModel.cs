using System;
using System.Collections.Generic;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Service.Areas.PES.Services;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using SMC.SGA.Administrativo.Areas.PES.Views.PessoaAtuacaoDocumento.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoDocumentoDynamicModel : SMCDynamicViewModel
    {

        #region [Data Sources]

        [SMCDataSource]
        [SMCServiceReference(typeof(IPessoaAtuacaoDocumentoService), nameof(PessoaAtuacaoDocumentoService.BuscarDocumentosSelect))]
        public List<SMCSelectListItem> TiposDocumentos { get; set; }

        #endregion [Data Sources]

        [SMCParameter]
        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCParameter]
        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public bool HabilitaCampo { get; set; }

        [SMCHidden]
        public bool HabilitaData { get; set; }

        [SMCKey]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCRequired]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid16_24)]
        [SMCRequired]
        [SMCSelect(nameof(TiposDocumentos))]
        [SMCConditionalReadonly(nameof(HabilitaCampo), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        public long? SeqTipoDocumento { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCConditionalReadonly(nameof(HabilitaData), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        public DateTime DataEntrega { get; set; } = DateTime.Today;

        [SMCOrder(3)]
        [SMCHidden]
        [SMCSize(SMCSize.Grid6_24)]
        public long? SeqSolicitacaoDocumentoRequerido { get; set; }

        [SMCDescription]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid12_24)]
        public string Observacao { get; set; }

        [SMCOrder(5)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCFile(AllowedFileExtensions = new string[] { "doc", "docx", "xls", "xlsx", "jpg", "jpeg", "png", "pdf", "rar", "zip", "ps", "xml" },
                 AreaDownload = "",
                 ControllerDownload = "Home",
                 ActionDownload = "DownloadFileGuid",
                 MaxFileSize = 25000000,
                 MaxFiles = 1,
                 HideDescription = true,
                 DisplayFilesInContextWindow = true)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.HeaderIndex("CabecalhoPessoaAtuacaoDocumento")
                   .EditInModal(refreshIndexPageOnSubmit: true)
                   .HeaderIndexList(nameof(PessoaAtuacaoDocumentoController.ApresentarMensagemInformativa))
                   .Button(idResource: "Visualizar_Documento",
                           action: "DownloadFileGuid",
                           controller: "../Home",
                           securityToken: UC_PES_001_11_01.PESQUISAR_DOCUMENTOS_PESSOA_ATUACAO,
                           htmlAttributes: new { target = "_blank" },
                           routes: d => new { guidFile = ((PessoaAtuacaoDocumentoListarDynamicModel)d).UidArquivo })
                   .IgnoreFilterGeneration(true)
                   .Button(idResource: "Consultar_Solicitacao",
                           action: "ConsultarSolicitacaoServicoModal",
                           controller: "SolicitacaoServicoRoute",
                           securityToken: UC_PES_001_11_01.PESQUISAR_DOCUMENTOS_PESSOA_ATUACAO,
                           displayButton: x => ((PessoaAtuacaoDocumentoListarDynamicModel)x).ExibeSolServico,
                           htmlAttributes: new
                           {
                               data_modal_title = "Consulta Solicitação de Serviço",
                               data_behavior = "data-modal-open",
                               data_idmodal = "modalSolicitacao",
                               data_ajax = "true",
                               data_ajax_method = "Get",
                               data_ajax_mode = "replace"
                           },
                           routes: r => new
                           {
                               area = "",
                               seqSolicitacaoServico = ((PessoaAtuacaoDocumentoListarDynamicModel)r).SeqSolicitacaoServico,
                               seqPessoaAtuacao = ((PessoaAtuacaoDocumentoListarDynamicModel)r).SeqPessoaAtuacao,
                               numeroProtocolo = SMCDESCrypto.EncryptForURL(((PessoaAtuacaoDocumentoListarDynamicModel)r).NumeroProtocoloSolicitado),
                               backUrl = System.Web.HttpContext.Current.Request.Url.Host == "localhost" ?
                                                   $"http://{System.Web.HttpContext.Current.Request.Url.Host}/Dev/{SMCContext.ApplicationId}/PES/PessoaAtuacaoDocumento?seqPessoaAtuacao={((PessoaAtuacaoDocumentoListarDynamicModel)r).SeqPessoaAtuacao}" :
                                                   $"http://{System.Web.HttpContext.Current.Request.Url.Host}/{SMCContext.ApplicationId}/PES/PessoaAtuacaoDocumento?seqPessoaAtuacao={((PessoaAtuacaoDocumentoListarDynamicModel)r).SeqPessoaAtuacao}"
                           })
                   .ButtonBackIndex("Index", "Aluno", x => new { area = "ALN" })
                   .ConfigureButton((button, model, action) =>
                    {
                        if (action == SMCDynamicButtonAction.Insert)
                        {
                            var servico = new PessoaAtuacaoDocumentoService();
                            var obj = (PessoaAtuacaoDocumentoFiltroDynamicModel)model;

                            if (servico.VerificarServico(obj.SeqPessoaAtuacao.Value))
                            {
                                button.Enabled(false);
                                button.ButtonInstructions("Não é possível incluir ou alterar um documento, pois há uma solicitação de serviço em aberto!");
                            }

                            if (servico.NaoExisteProcesso(obj.SeqPessoaAtuacao.Value))
                            {
                                button.Enabled(false);
                                button.ButtonInstructions("O processo de entrega de documento não possui uma configuração compatível com o aluno.");
                            }
                        }
                        if (action == SMCDynamicButtonAction.Edit)
                        {
                            var servico = new PessoaAtuacaoDocumentoService();
                            var dados = (PessoaAtuacaoDocumentoListarDynamicModel)model;

                            if (dados.NumeroProtocoloSolicitado != null)
                            {
                                button.Enabled(false);
                                button.ButtonInstructions("Este documento foi entregue por meio de solicitação e não pode ser alterado. Exclua o registro e inclua um novo do mesmo tipo de documento");
                            }

                            else if (servico.VerificarServico(dados.SeqPessoaAtuacao.Value))
                            {
                                button.Enabled(false);
                                button.ButtonInstructions("Não é possível incluir ou alterar um documento, pois há uma solicitação de serviço em aberto!");
                            }
                            else if (servico.NaoExisteProcesso(dados.SeqPessoaAtuacao.Value))
                            {
                                button.Enabled(false);
                                button.ButtonInstructions("O processo de entrega de documento não possui uma configuração compatível com o aluno.");
                            }
                            else
                            {
                                button.Enabled(true);
                                button.ButtonInstructions(string.Empty);
                            }
                        }
                        if (action == SMCDynamicButtonAction.Remove)
                        {
                            var servico = new PessoaAtuacaoDocumentoService();
                            var dados = (PessoaAtuacaoDocumentoListarDynamicModel)model;

                            if (servico.VerificarServico(dados.SeqPessoaAtuacao.Value))
                            {
                                button.Enabled(false);
                                button.ButtonInstructions("Não é possível excluir este documento, pois há uma solicitação de serviço em aberto!");
                            }

                            if (servico.NaoExisteProcesso(dados.SeqPessoaAtuacao.Value))
                            {
                                button.Enabled(false);
                                button.ButtonInstructions("O processo de entrega de documento não possui uma configuração compatível com o aluno.");
                            }
                        }
                    })
                   .Service<IPessoaAtuacaoDocumentoService>(index: nameof(IPessoaAtuacaoDocumentoService.BuscarTiposDocumentoLista),
                                                            insert: nameof(IPessoaAtuacaoDocumentoService.VerificarServico),
                                                            save: nameof(IPessoaAtuacaoDocumentoService.SalvarDocumento),
                                                            delete: nameof(IPessoaAtuacaoDocumentoService.ExcluirDocumento))
                   .Messages(x =>
                   {
                       var documento = (x as PessoaAtuacaoDocumentoListarDynamicModel);
                       var controller = new PessoaAtuacaoDocumentoController();

                       if (documento.SeqTipoDocumento != 0)
                       {
                           if (controller.VerificaDocumentoObrigatorio((x as PessoaAtuacaoDocumentoListarDynamicModel).SeqTipoDocumento.Value, (x as PessoaAtuacaoDocumentoListarDynamicModel).SeqPessoaAtuacao.Value))
                           {
                               return string.Format(UIResource.Listar_Excluir_Confirmacao_Personalizada,
                                             ((PessoaAtuacaoDocumentoListarDynamicModel)x).DescricaoTipoDocumento);
                           }
                       }
                       return string.Format(UIResource.Listar_Excluir_Confirmacao,
                                               ((PessoaAtuacaoDocumentoListarDynamicModel)x).DescricaoTipoDocumento);
                   })
                   .Tokens(tokenList: UC_PES_001_11_01.PESQUISAR_DOCUMENTOS_PESSOA_ATUACAO,
                           tokenInsert: UC_PES_001_11_02.MANTER_DOCUMENTOS_PESSOA_ATUACAO,
                           tokenEdit: UC_PES_001_11_02.MANTER_DOCUMENTOS_PESSOA_ATUACAO,
                           tokenRemove: UC_PES_001_11_02.MANTER_DOCUMENTOS_PESSOA_ATUACAO);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            if (viewMode == SMCViewMode.Insert)
            {
                this.HabilitaCampo = true;
                this.HabilitaData = true;
            }
            if (viewMode == SMCViewMode.Edit)
            {
                this.HabilitaData = true;
            }
            base.InitializeModel(viewMode);
        }

    }
}