using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.SRC.Views.DocumentoRequerido.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class DocumentoRequeridoController : SMCControllerBase
    {
        #region [ Services ]   

        private IConfiguracaoEtapaService ConfiguracaoEtapaService
        {
            get { return this.Create<IConfiguracaoEtapaService>(); }
        }

        private IProcessoEtapaService ProcessoEtapaService
        {
            get { return this.Create<IProcessoEtapaService>(); }
        }

        private IDocumentoRequeridoService DocumentoRequeridoService
        {
            get { return this.Create<IDocumentoRequeridoService>(); }
        }

        private ISolicitacaoServicoService SolicitacaoServicoService
        {
            get { return this.Create<ISolicitacaoServicoService>(); }
        }

        private IConfiguracaoEtapaPaginaService ConfiguracaoEtapaPaginaService
        {
            get { return this.Create<IConfiguracaoEtapaPaginaService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_SRC_002_04_10.CONFIGURAR_ETAPA_PESQUISAR_DOCUMENTOS)]
        public ActionResult Index(DocumentoRequeridoFiltroViewModel filtro)
        {
            var processoEtapa = this.ProcessoEtapaService.BuscarProcessoEtapa(filtro.SeqProcessoEtapa);

            filtro.SituacaoEtapa = processoEtapa.SituacaoEtapa;
            //filtro.PossuiPaginaAnexarDocumentos = this.ConfiguracaoEtapaPaginaService.VerificarConfiguracaoPossuiPaginaAnexarDocumento(filtro.SeqConfiguracaoEtapa);
            //filtro.PossuiPaginaRegistrarDocumentos = this.ConfiguracaoEtapaPaginaService.VerificarConfiguracaoPossuiPaginaRegistrarDocumento(filtro.SeqConfiguracaoEtapa);
            filtro.PossuiPaginaAnexarDocumentos = this.ConfiguracaoEtapaPaginaService.VerificarConfiguracaoPossuiPaginaConfiguracaoDocumentoUpload(filtro.SeqConfiguracaoEtapa);
            filtro.PossuiPaginaRegistrarDocumentos = this.ConfiguracaoEtapaPaginaService.VerificarConfiguracaoPossuiPaginaConfiguracaoDocumentoRegistro(filtro.SeqConfiguracaoEtapa);
            filtro.SeqProcesso = processoEtapa.SeqProcesso;

            return View(filtro);
        }

        [SMCAuthorize(UC_SRC_002_04_10.CONFIGURAR_ETAPA_PESQUISAR_DOCUMENTOS)]
        public ActionResult Cabecalho(long seqConfiguracaoEtapa)
        {
            var modeloCabecalho = ConfiguracaoEtapaService.BuscarCabecalhoConfiguracaoEtapa(seqConfiguracaoEtapa).Transform<CabecalhoDocumentoRequeridoViewModel>();
            return PartialView("_Cabecalho", modeloCabecalho);
        }

        [SMCAuthorize(UC_SRC_002_04_10.CONFIGURAR_ETAPA_PESQUISAR_DOCUMENTOS)]
        public ActionResult ListarDocumentosRequeridos(DocumentoRequeridoFiltroViewModel filtro)
        {
            SMCPagerModel<DocumentoRequeridoListarViewModel> model = ExecuteService<DocumentoRequeridoFiltroData, DocumentoRequeridoListarData,
                                                                                    DocumentoRequeridoFiltroViewModel, DocumentoRequeridoListarViewModel>
                                                                                   (DocumentoRequeridoService.BuscarDocumentosRequeridos, filtro);

            return PartialView("_DetailList", model);
        }

        [SMCAuthorize(UC_SRC_002_04_11.CONFIGURAR_ETAPA_MANTER_DOCUMENTOS)]
        public ActionResult Incluir(long seqConfiguracaoEtapa)
        {
            var configuracaoEtapa = ConfiguracaoEtapaService.BuscarConfiguracaoEtapa(seqConfiguracaoEtapa, IncludesConfiguracaoEtapa.ProcessoEtapa |
                                                                                                           IncludesConfiguracaoEtapa.ConfiguracaoProcesso_Processo_Servico);

            string tokenServico = ConfiguracaoEtapaService.BuscarTokenServicoConfiguracaoEtapa(seqConfiguracaoEtapa);

            bool bloqueiaCampoPrazoEntrega = tokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU
                                          || tokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA;

            var modelo = new DocumentoRequeridoViewModel()
            {
                SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                DescricaoConfiguracaoEtapa = configuracaoEtapa.Descricao,
                SeqProcessoEtapa = configuracaoEtapa.SeqProcessoEtapa,
                BloqueiaCampoPrazoEntrega = bloqueiaCampoPrazoEntrega,
                TiposDocumento = this.DocumentoRequeridoService.BuscarTiposDocumentoPorServicoSelect(configuracaoEtapa.Servico.Seq)
            };

            return PartialView("_DadosDocumentoRequerido", modelo);
        }

        [SMCAuthorize(UC_SRC_002_04_11.CONFIGURAR_ETAPA_MANTER_DOCUMENTOS)]
        public ActionResult Editar(long seq)
        {
            var modelo = this.DocumentoRequeridoService.BuscarDocumentoRequerido(seq).Transform<DocumentoRequeridoViewModel>();

            string tokenServico = ConfiguracaoEtapaService.BuscarTokenServicoConfiguracaoEtapa(modelo.SeqConfiguracaoEtapa);

            bool bloqueiaCampoPrazoEntrega = tokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU
                                          || tokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA;
            
            modelo.BloqueiaCampoPrazoEntrega = bloqueiaCampoPrazoEntrega;

            modelo.TiposDocumento = this.DocumentoRequeridoService.BuscarTiposDocumentoSelect();

            if (modelo.PermiteUploadArquivo.HasValue && !modelo.PermiteUploadArquivo.Value)
                modelo.ObrigatorioUpload = null;

            if (!modelo.DataLimiteEntrega.HasValue)
                modelo.DataLimiteEntrega = null;

            return PartialView("_DadosDocumentoRequerido", modelo);
        }

        [SMCAuthorize(UC_SRC_002_04_11.CONFIGURAR_ETAPA_MANTER_DOCUMENTOS)]
        public ActionResult Salvar(DocumentoRequeridoViewModel modelo)
        {
            this.DocumentoRequeridoService.ValidarModeloSalvar(modelo.Transform<DocumentoRequeridoData>());

            ///********************** VALIDAÇÕES DE ASSERT **********************/

            var configuracaoEtapa = ConfiguracaoEtapaService.BuscarConfiguracaoEtapa(modelo.SeqConfiguracaoEtapa, IncludesConfiguracaoEtapa.Nenhum);
            var solicitacaoAssociadaConfiguracaoProcessoEmAberto = this.SolicitacaoServicoService.VerificarConfiguracaoPossuiSolicitacaoServicoEmAberto(configuracaoEtapa.SeqConfiguracaoProcesso);

            if (modelo.Seq == 0 && solicitacaoAssociadaConfiguracaoProcessoEmAberto)
                Assert(modelo, UIResource.MSG_Assert_SalvarDocumentoRequerido);

            ///********************** FIM VALIDAÇÕES DE ASSERT **********************/

            long retorno = this.DocumentoRequeridoService.Salvar(modelo.Transform<DocumentoRequeridoData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Documento_Requerido, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcessoEtapa = new SMCEncryptedLong(modelo.SeqProcessoEtapa), seqConfiguracaoEtapa = new SMCEncryptedLong(modelo.SeqConfiguracaoEtapa) });
        }

        [SMCAuthorize(UC_SRC_002_04_11.CONFIGURAR_ETAPA_MANTER_DOCUMENTOS)]
        public ActionResult Excluir(long seq, long seqProcessoEtapa, long seqConfiguracaoEtapa)
        {
            try
            {
                this.DocumentoRequeridoService.Excluir(seq);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Documento_Requerido, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcessoEtapa = new SMCEncryptedLong(seqProcessoEtapa), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
        }

        [SMCAuthorize(UC_SRC_002_04_11.CONFIGURAR_ETAPA_MANTER_DOCUMENTOS)]
        public JsonResult PreencherCampoSexo(bool? obrigatorio)
        {
            List<SMCSelectListItem> lista = new List<SMCSelectListItem>();
            lista = Enum.GetValues(typeof(Sexo)).Cast<Sexo>().Where(a => (short)a != 0).Select(a => new SMCSelectListItem() { Value = ((short)a).ToString(), Text = SMCEnumHelper.GetDescription(a) }).ToList();

            return Json(lista);
        }
        
        [SMCAuthorize(UC_SRC_002_04_11.CONFIGURAR_ETAPA_MANTER_DOCUMENTOS)]
        public JsonResult PreencherCampoUploadObrigatorio(bool? obrigatorio, bool? permiteUploadArquivo)
        {
            List<SMCSelectListItem> lista = new List<SMCSelectListItem>();

            if ((obrigatorio.HasValue && obrigatorio.Value == true) && (permiteUploadArquivo.HasValue && permiteUploadArquivo.Value == true))
            {
                lista.Add(new SMCSelectListItem() { Value = "", Text = "Selecionar", Selected = true });
                lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim" });
                lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não" });
            }
            else
            {
                lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não", Selected = true });
            }

            return Json(lista);
        }

        [SMCAuthorize(UC_SRC_002_04_11.CONFIGURAR_ETAPA_MANTER_DOCUMENTOS)]
        public JsonResult LimparCampoDataLimiteEntrega(bool? permiteEntregaPosterior, DateTime? dataLimiteEntrega)
        {
            if (permiteEntregaPosterior.HasValue && permiteEntregaPosterior == true)
            {
                return Json(dataLimiteEntrega);
            }
            else
            {
                return null;
            }
        }

        [SMCAuthorize(UC_SRC_002_04_11.CONFIGURAR_ETAPA_MANTER_DOCUMENTOS)]
        public JsonResult ExibeTermoResponsabilidadeEntrega(bool? obrigatorioUpload, bool? permiteEntregaPosterior, bool? exibeTermoResponsabilidadeEntrega)
        {
            List<SMCSelectListItem> lista = new List<SMCSelectListItem>();

            if ((obrigatorioUpload.HasValue && obrigatorioUpload == true) && (permiteEntregaPosterior.HasValue && permiteEntregaPosterior == true))
            {
                bool exibeTermo = (bool)exibeTermoResponsabilidadeEntrega;

                lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim", Selected = exibeTermo });
                lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não", Selected = !exibeTermo });
            }
            else
            {
                lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim" });
                lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não", Selected = true });
            }

            return Json(lista);
        }
    }
}