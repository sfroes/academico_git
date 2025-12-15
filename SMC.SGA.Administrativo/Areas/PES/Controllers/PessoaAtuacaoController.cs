using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.PES.Models;
using SMC.SGA.Administrativo.Areas.PES.Views.PessoaAtuacao.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class PessoaAtuacaoController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IPessoaAtuacaoService PessoaAtuacaoService
        {
            get { return Create<IPessoaAtuacaoService>(); }
        }

        private ISolicitacaoServicoService SolicitacaoServicoService
        {
            get { return Create<ISolicitacaoServicoService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_PES_001_04_01.VISUALIZAR_DOCUMENTOS_PESSOA_ATUACAO)]
        public ActionResult VisualizarDocumento(SMCEncryptedLong seqPessoaAtuacao)
        {
            var modelo = this.PessoaAtuacaoService.BuscarDocumentosPessoaAtuacao(seqPessoaAtuacao).Transform<PessoaAtuacaoVisualizacaoDocumentoViewModel>();

            return View(modelo);
        }

        [SMCAuthorize(UC_PES_001_04_01.VISUALIZAR_DOCUMENTOS_PESSOA_ATUACAO)]
        public ActionResult CabecalhoPessoaAtuacao(SMCEncryptedLong seqPessoaAtuacao)
        {
            var model = ExecuteService<PessoaAtuacaoCabecalhoData, PessoaAtuacaoCabecalhoViewModel>(PessoaAtuacaoService.BuscarPessoaAtuacaoCabecalho, (long)seqPessoaAtuacao);

            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_PES_001_04_02.REGISTRAR_DOCUMENTO)]
        public ActionResult RegistrarDocumento(SMCEncryptedLong seqPessoaAtuacao, SMCEncryptedLong seqTipoDocumento)
        {
            var model = new PessoaAtuacaoRegistroDocumentoViewModel()
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                SeqTipoDocumento = seqTipoDocumento,
                DescricaoTipoDocumento = this.PessoaAtuacaoService.BuscarDescricaoTipoDocumento(seqTipoDocumento)
            };
            return View(model);
        }

        [SMCAuthorize(UC_PES_001_04_02.REGISTRAR_DOCUMENTO)]
        public ActionResult Passo1(PessoaAtuacaoRegistroDocumentoViewModel model)
        {
            model.SolicitacoesServico = this.SolicitacaoServicoService.BuscarSolicitacoesPessoaAtuacaoTipoDocumentoSelect(model.SeqPessoaAtuacao, model.SeqTipoDocumento);

            return PartialView("_SelecionarSolicitacaoServico", model);
        }

        [SMCAuthorize(UC_PES_001_04_02.REGISTRAR_DOCUMENTO)]
        public ActionResult Passo2(PessoaAtuacaoRegistroDocumentoViewModel model)
        {
            var result = this.PessoaAtuacaoService.PrepararModeloRegistroDocumento(model.SeqPessoaAtuacao, model.SeqTipoDocumento, model.SeqsSolicitacoesServico).Transform<PessoaAtuacaoRegistroDocumentoViewModel>();

            model.DescricaoTipoDocumento = result.DescricaoTipoDocumento;
            model.PermiteVarios = result.PermiteVarios;
            model.SituacoesEntregaDocumento = result.SituacoesEntregaDocumento;
            model.Documentos = result.Documentos;

            return PartialView("_RegistrarDocumento", model);
        }

        [SMCAuthorize(UC_PES_001_04_02.REGISTRAR_DOCUMENTO)]
        public ActionResult PreencherDataEntrega(SituacaoEntregaDocumento situacaoEntregaDocumento, DateTime? dataEntrega)
        {
            if (situacaoEntregaDocumento != SituacaoEntregaDocumento.AguardandoEntrega && situacaoEntregaDocumento != SituacaoEntregaDocumento.Pendente)
            {
                if (dataEntrega == null || !dataEntrega.HasValue)
                    dataEntrega = DateTime.Now.Date;
            }
            else if (situacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega || situacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)
                dataEntrega = null;

            return Content(dataEntrega.HasValue ? dataEntrega.Value.ToString("MM/dd/yyyy") : string.Empty);
        }

        [SMCAuthorize(UC_PES_001_04_02.REGISTRAR_DOCUMENTO)]
        public ActionResult PreencherVersaoDocumento(SituacaoEntregaDocumento situacaoEntregaDocumento, VersaoDocumento? versaoDocumento, VersaoDocumento versaoExigida)
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
                    listaVersaoDocumento.FirstOrDefault(l => l.Value == ((int)versaoExigida).ToString()).Selected = true;
                }
                else
                {
                    if (versaoDocumento.HasValue)
                        listaVersaoDocumento.FirstOrDefault(l => l.Value == ((int)versaoDocumento).ToString()).Selected = true;
                }
            }

            return Json(listaVersaoDocumento);
        }

        [SMCAuthorize(UC_PES_001_04_02.REGISTRAR_DOCUMENTO)]
        public ActionResult PreencherFormaEntregaDocumento(SituacaoEntregaDocumento situacaoEntregaDocumento, FormaEntregaDocumento? formaEntregaDocumento)
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

            return Json(listaFormaEntregaDocumento);
        }

        [SMCAuthorize(UC_PES_001_04_02.REGISTRAR_DOCUMENTO)]
        public ActionResult PreencherObservacao(SituacaoEntregaDocumento situacaoEntregaDocumento, string observacao)
        {
            if (situacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega || situacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente || string.IsNullOrEmpty(observacao))
            {
                observacao = string.Empty;
            }

            return Content(observacao);
        }

        [SMCAuthorize(UC_PES_001_04_02.REGISTRAR_DOCUMENTO)]
        public ActionResult SalvarRegistroDocumento(PessoaAtuacaoRegistroDocumentoViewModel model)
        {
            Assert(model, string.Format(UIResource.Mensagem_Confirmacao, model.DescricaoTipoDocumento), () =>
            {
                return true;
            });

            this.PessoaAtuacaoService.SalvarRegistroDocumento(model.Transform<PessoaAtuacaoRegistroDocumentoData>());

            SetSuccessMessage(UIResource.MSG_Registro_Documento_Sucesso, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(PessoaAtuacaoController.VisualizarDocumento), "PessoaAtuacao", new { seqPessoaAtuacao = new SMCEncryptedLong(model.SeqPessoaAtuacao) });
        }
    }
}