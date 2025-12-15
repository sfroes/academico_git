using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Academico.UI.Mvc.Areas.SRC.Views.RegistroDocumento.App_LocalResources;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Controllers
{
    public class RegistroDocumentoController : SMCControllerBase
    {
        #region [ Services ]

        private IRegistroDocumentoService RegistroDocumentoService
        {
            get { return this.Create<IRegistroDocumentoService>(); }
        }

        private ISolicitacaoServicoService SolicitacaoServicoService
        {
            get { return this.Create<ISolicitacaoServicoService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_SRC_004_02_01.REGISTRAR_DOCUMENTOS)]
        public ActionResult CabecalhoRegistroDocumentos(SMCEncryptedLong seqSolicitacaoServico)
        {
            var modelHeader = this.RegistroDocumentoService.BuscarCabecalhoRegistroDocumentos(seqSolicitacaoServico);

            var view = GetExternalView(AcademicoExternalViews.REGISTRAR_DOCUMENTOS_CABECALHO);
            return PartialView(view, modelHeader.Transform<RegistroDocumentoCabecalhoViewModel>());
        }

        [SMCAuthorize(UC_SRC_004_02_01.REGISTRAR_DOCUMENTOS)]
        public ActionResult RegistrarDocumentos(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqPessoaAtuacao)
        {
            string tokenServico = SolicitacaoServicoService.BuscarSolicitacaoServico(seqSolicitacaoServico).TokenServico;
            var documentos = this.RegistroDocumentoService.BuscarDocumentosRegistro(seqSolicitacaoServico, exibirDocumentoNaoPermiteUpload: true);

            bool temTokenControlePrazoEntrega = SMCSecurityHelper.Authorize(UC_SRC_004_02_01.CONTROLE_PRAZO_ENTREGA);

            foreach (var doc in documentos)
            {
                foreach (var documento in doc.Documentos)
                {
                    DateTime? dataLimite = doc.DataLimiteEntrega;
                    documento.DataLimiteEntrega = dataLimite != null ? dataLimite : DateTime.Now.AddDays(1);
                }

                // Validando se existe pelo menos um documento para aquele tipo de documento
                // Caso será criado um documento incial para que a secretaria tenha um documento a validar
                // Adicionada nova validação para vefiricar se existe algum documento com o status diferente de "Nenhum", pois quando 
                // existe documento com esse status a viewmodel exibe erro
                if (!doc.Documentos.SMCAny() || (doc.Documentos.Count == 1 && doc.Documentos.SMCAny(c => c.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Nenhum)))
                {
                    doc.Documentos = new List<DocumentoItemData>();

                    DocumentoItemData docInicial = new DocumentoItemData();
                    docInicial.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega;
                    docInicial.SituacaoEntregaDocumentoInicial = SituacaoEntregaDocumento.AguardandoEntrega;
                    docInicial.SeqTipoDocumento = doc.SeqTipoDocumento;
                    docInicial.SeqSolicitacaoServico = seqSolicitacaoServico;
                    doc.Documentos.Add(docInicial);
                }
            }

            var registroDocumento = new RegistroDocumentoViewModel()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                SeqPessoaAtuacao = seqPessoaAtuacao,
                Documentos = documentos.TransformList<DocumentoViewModel>(),
                BackUrl = Request.UrlReferrer.ToString()
            };

            registroDocumento?.Documentos?.ForEach(d =>
            {
                var dataMinimaPrazoEntrega = DateTime.Now;
                var dataMaximaPrazoEntrega = DateTime.MaxValue;

                foreach (var documento in d.Documentos)
                {
                    if (tokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA || tokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU)
                    {
                        if (temTokenControlePrazoEntrega)
                        {
                            documento.DataMinimaEntrega = DateTime.Now;
                            documento.DataMaximaEntrega = DateTime.MaxValue;
                        }
                        else
                        {
                            documento.DataMinimaEntrega = DateTime.Now;
                            documento.DataMaximaEntrega = DateTime.Now;
                        }
                    }
                    else
                    {
                        documento.DataMinimaEntrega = DateTime.Now.AddDays(1);
                        documento.DataMaximaEntrega = DateTime.MaxValue;
                    }

                    dataMinimaPrazoEntrega = documento.DataMinimaEntrega;
                    dataMaximaPrazoEntrega = documento.DataMaximaEntrega;
                }

                d.Documentos.DefaultModel = new DocumentoItemViewModel
                {
                    SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega,
                    SituacaoEntregaDocumentoInicial = SituacaoEntregaDocumento.AguardandoEntrega,
                    SeqTipoDocumento = d.SeqTipoDocumento,
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    DataMinimaEntrega = dataMinimaPrazoEntrega,
                    DataMaximaEntrega = dataMaximaPrazoEntrega
                };
            });

            var view = GetExternalView(AcademicoExternalViews.REGISTRAR_DOCUMENTOS);
            return PartialView(view, registroDocumento);
        }

        [SMCAuthorize(UC_SRC_004_02_01.REGISTRAR_DOCUMENTOS)]
        public ActionResult PreencherDataEntrega(SituacaoEntregaDocumento situacaoEntregaDocumento, DateTime? dataEntrega, SMCUploadFile arquivoAnexado)
        {
            if (situacaoEntregaDocumento != SituacaoEntregaDocumento.AguardandoEntrega && situacaoEntregaDocumento != SituacaoEntregaDocumento.Pendente)
            {
                if (dataEntrega == null || !dataEntrega.HasValue)
                    dataEntrega = DateTime.Now.Date;
            }
            else if (situacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega || situacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)
            {
                if (arquivoAnexado == null)
                    dataEntrega = null;
            }

            return Content(dataEntrega.HasValue ? dataEntrega.Value.ToString("MM/dd/yyyy") : string.Empty);
        }

        [SMCAuthorize(UC_SRC_004_02_01.REGISTRAR_DOCUMENTOS)]
        public ActionResult PreencherVersaoDocumento(SituacaoEntregaDocumento situacaoEntregaDocumento, VersaoDocumento? versaoDocumento, VersaoDocumento versaoExigida, SMCUploadFile arquivoAnexado)
        {
            var listaVersaoDocumento = new List<SMCSelectListItem>();

            foreach (var item in SMCEnumHelper.GenerateKeyValuePair<VersaoDocumento>())
            {
                listaVersaoDocumento.Add(new SMCSelectListItem() { Text = item.Value, Value = ((int)item.Key).ToString() });
            }

            if (situacaoEntregaDocumento != SituacaoEntregaDocumento.AguardandoEntrega && situacaoEntregaDocumento != SituacaoEntregaDocumento.Pendente)
            {
                if ((versaoDocumento == null || versaoDocumento == VersaoDocumento.Nenhum) && versaoExigida != VersaoDocumento.Nenhum)
                {
                    var item = listaVersaoDocumento.FirstOrDefault(l => l.Value == ((int)versaoExigida).ToString());
                    if (item != null)
                        item.Selected = true;
                }
                else
                {
                    var item = listaVersaoDocumento.FirstOrDefault(l => l.Value == ((int)(versaoDocumento ?? VersaoDocumento.Nenhum)).ToString());
                    if (item != null)
                        item.Selected = true;
                }
            }
            else if (situacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega || situacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)
            {
                if (arquivoAnexado != null)
                {
                    if ((versaoDocumento == null || versaoDocumento == VersaoDocumento.Nenhum) && versaoExigida != VersaoDocumento.Nenhum)
                    {
                        var item = listaVersaoDocumento.FirstOrDefault(l => l.Value == ((int)versaoExigida).ToString());
                        if (item != null)
                            item.Selected = true;
                    }
                    else
                    {
                        var item = listaVersaoDocumento.FirstOrDefault(l => l.Value == ((int)(versaoDocumento ?? VersaoDocumento.Nenhum)).ToString());
                        if (item != null)
                            item.Selected = true;
                    }
                }
            }

            return Json(listaVersaoDocumento);
        }

        [SMCAuthorize(UC_SRC_004_02_01.REGISTRAR_DOCUMENTOS)]
        public ActionResult PreencherFormaEntregaDocumento(SituacaoEntregaDocumento situacaoEntregaDocumento, FormaEntregaDocumento? formaEntregaDocumento, SMCUploadFile arquivoAnexado)
        {
            var listaFormaEntregaDocumento = new List<SMCSelectListItem>();

            foreach (var item in SMCEnumHelper.GenerateKeyValuePair<FormaEntregaDocumento>())
            {
                listaFormaEntregaDocumento.Add(new SMCSelectListItem() { Text = item.Value, Value = ((int)item.Key).ToString() });
            }

            if (situacaoEntregaDocumento != SituacaoEntregaDocumento.AguardandoEntrega && situacaoEntregaDocumento != SituacaoEntregaDocumento.Pendente)
            {
                if (formaEntregaDocumento != null)
                    listaFormaEntregaDocumento.FirstOrDefault(l => l.Value == ((int)formaEntregaDocumento).ToString()).Selected = true;
            }
            else if (situacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega || situacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)
            {
                if (arquivoAnexado != null)
                {
                    if (formaEntregaDocumento != null)
                        listaFormaEntregaDocumento.FirstOrDefault(l => l.Value == ((int)formaEntregaDocumento).ToString()).Selected = true;
                }
            }

            return Json(listaFormaEntregaDocumento);
        }

        [SMCAuthorize(UC_SRC_004_02_01.REGISTRAR_DOCUMENTOS)]
        public ActionResult PreencherObservacao(SituacaoEntregaDocumento situacaoEntregaDocumento, string observacaoSecretaria, SMCUploadFile arquivoAnexado)
        {
            if (situacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega || situacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)
            {
                if (arquivoAnexado == null)
                    observacaoSecretaria = string.Empty;
            }

            return Content(observacaoSecretaria);
        }

        [SMCAuthorize(UC_SRC_004_02_01.REGISTRAR_DOCUMENTOS)]
        public ActionResult SalvarRegistroDocumentos(RegistroDocumentoViewModel model)
        {
            this.RegistroDocumentoService.SalvarRegistroDocumentos(model.Transform<RegistroDocumentoData>());

            SetSuccessMessage(string.Format(UIResource.MSG_Inserir_Sucesso, "Registro de documento(s)"), UIResource.Titulo_Inserir_Sucesso, SMCMessagePlaceholders.Centro);

            return SMCRedirectToUrl(model.BackUrl);
        }
    }
}