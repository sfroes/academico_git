using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using SMC.SGA.Administrativo.Areas.SRC.Views.GrupoDocumentoRequerido.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class GrupoDocumentoRequeridoController : SMCControllerBase
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

        private IGrupoDocumentoRequeridoService GrupoDocumentoRequeridoService
        {
            get { return this.Create<IGrupoDocumentoRequeridoService>(); }
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

        [SMCAuthorize(UC_SRC_002_04_12.CONFIGURAR_ETAPA_PESQUISAR_GRUPO_DOCUMENTOS)]
        public ActionResult Index(GrupoDocumentoRequeridoFiltroViewModel filtro)
        {
            var processoEtapa = this.ProcessoEtapaService.BuscarProcessoEtapa(filtro.SeqProcessoEtapa);

            filtro.SituacaoEtapa = processoEtapa.SituacaoEtapa;
            filtro.PossuiPaginaAnexarDocumentos = this.ConfiguracaoEtapaPaginaService.VerificarConfiguracaoPossuiPaginaAnexarDocumento(filtro.SeqConfiguracaoEtapa);
            filtro.PossuiPaginaRegistrarDocumentos = this.ConfiguracaoEtapaPaginaService.VerificarConfiguracaoPossuiPaginaRegistrarDocumento(filtro.SeqConfiguracaoEtapa);
            filtro.SeqProcesso = processoEtapa.SeqProcesso;

            return View(filtro);
        }

        [SMCAuthorize(UC_SRC_002_04_12.CONFIGURAR_ETAPA_PESQUISAR_GRUPO_DOCUMENTOS)]
        public ActionResult Cabecalho(long seqConfiguracaoEtapa)
        {
            var modeloCabecalho = ConfiguracaoEtapaService.BuscarCabecalhoConfiguracaoEtapa(seqConfiguracaoEtapa).Transform<CabecalhoGrupoDocumentoRequeridoViewModel>();
            return PartialView("_Cabecalho", modeloCabecalho);
        }

        [SMCAuthorize(UC_SRC_002_04_12.CONFIGURAR_ETAPA_PESQUISAR_GRUPO_DOCUMENTOS)]
        public ActionResult ListarGruposDocumentosRequeridos(GrupoDocumentoRequeridoFiltroViewModel filtro)
        {
            SMCPagerModel<GrupoDocumentoRequeridoListarViewModel> model = ExecuteService<GrupoDocumentoRequeridoFiltroData, GrupoDocumentoRequeridoListarData,
                                                                                         GrupoDocumentoRequeridoFiltroViewModel, GrupoDocumentoRequeridoListarViewModel>
                                                                                        (GrupoDocumentoRequeridoService.BuscarGruposDocumentosRequeridos, filtro);

            return PartialView("_DetailList", model);
        }
              
        [SMCAuthorize(UC_SRC_002_04_13.CONFIGURAR_ETAPA_MANTER_GRUPO_DOCUMENTOS)]
        public ActionResult Incluir(long seqConfiguracaoEtapa)
        {
            var configuracaoEtapa = ConfiguracaoEtapaService.BuscarConfiguracaoEtapa(seqConfiguracaoEtapa, IncludesConfiguracaoEtapa.ProcessoEtapa);

            var modelo = new GrupoDocumentoRequeridoViewModel()
            {
                SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                DescricaoConfiguracaoEtapa = configuracaoEtapa.Descricao,
                SeqProcessoEtapa = configuracaoEtapa.SeqProcessoEtapa,
                Mensagem = UIResource.Mensagem_Informativa
            };

            return PartialView("_DadosGrupoDocumentoRequerido", modelo);         
        }

        [SMCAuthorize(UC_SRC_002_04_13.CONFIGURAR_ETAPA_MANTER_GRUPO_DOCUMENTOS)]
        public ActionResult Editar(long seq)
        {
            var modelo = this.GrupoDocumentoRequeridoService.BuscarGrupoDocumentoRequerido(seq).Transform<GrupoDocumentoRequeridoViewModel>();
            modelo.Mensagem = UIResource.Mensagem_Informativa;
            
            var documentosRequeridos = GrupoDocumentoRequeridoService.BuscarDocumentosRequeridosSelect((bool)modelo.UploadObrigatorio, modelo.SeqConfiguracaoEtapa);
            modelo.DocumentosRequeridos = documentosRequeridos;

            return PartialView("_DadosGrupoDocumentoRequerido", modelo);
        }

        [SMCAuthorize(UC_SRC_002_04_13.CONFIGURAR_ETAPA_MANTER_GRUPO_DOCUMENTOS)]
        public ActionResult BuscarDocumentosRequeridosSelect(bool uploadObrigatorio, long seqConfiguracaoEtapa, long? seqDocumentoRequerido)
        {
            var documentosRequeridosList = GrupoDocumentoRequeridoService.BuscarDocumentosRequeridosSelect(uploadObrigatorio, seqConfiguracaoEtapa);

            if(seqDocumentoRequerido != null)
            {
                foreach (var doc in documentosRequeridosList)
                {
                    if (doc.Seq == seqDocumentoRequerido)
                        doc.Selected = true;
                }
            }

            return Json(documentosRequeridosList);
        }

        [SMCAuthorize(UC_SRC_002_04_13.CONFIGURAR_ETAPA_MANTER_GRUPO_DOCUMENTOS)]
        public ActionResult Salvar(GrupoDocumentoRequeridoViewModel modelo)
        {
            this.GrupoDocumentoRequeridoService.ValidarModeloSalvar(modelo.Transform<GrupoDocumentoRequeridoData>());

            ///********************** VALIDAÇÕES DE ASSERT **********************/

            var configuracaoEtapa = ConfiguracaoEtapaService.BuscarConfiguracaoEtapa(modelo.SeqConfiguracaoEtapa, IncludesConfiguracaoEtapa.Nenhum);
            var solicitacaoAssociadaConfiguracaoProcesso = this.SolicitacaoServicoService.VerificarConfiguracaoPossuiSolicitacaoServico(configuracaoEtapa.SeqConfiguracaoProcesso);
            var solicitacaoAssociadaConfiguracaoProcessoEmAberto = this.SolicitacaoServicoService.VerificarConfiguracaoPossuiSolicitacaoServicoEmAberto(configuracaoEtapa.SeqConfiguracaoProcesso);

            if (modelo.Seq != 0)
            {
                var grupoDocumentoRequeridoAntigo = this.GrupoDocumentoRequeridoService.BuscarGrupoDocumentoRequerido(modelo.Seq);
               
                if ((modelo.MinimoObrigatorio > grupoDocumentoRequeridoAntigo.MinimoObrigatorio) && solicitacaoAssociadaConfiguracaoProcesso)
                    Assert(modelo, UIResource.MSG_Assert_SalvarGrupoDocumentosAlteracaoMinimoDocumentos);
            }
            
            if (modelo.Seq == 0 && solicitacaoAssociadaConfiguracaoProcessoEmAberto)
                Assert(modelo, UIResource.MSG_Assert_SalvarGrupoDocumentosSolicitacaoAssociada);

            ///********************** FIM VALIDAÇÕES DE ASSERT **********************/

            long retorno = this.GrupoDocumentoRequeridoService.Salvar(modelo.Transform<GrupoDocumentoRequeridoData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Grupo_Documento_Requerido, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcessoEtapa = new SMCEncryptedLong(modelo.SeqProcessoEtapa), seqConfiguracaoEtapa = new SMCEncryptedLong(modelo.SeqConfiguracaoEtapa) });      
        }

        [SMCAuthorize(UC_SRC_002_04_13.CONFIGURAR_ETAPA_MANTER_GRUPO_DOCUMENTOS)]
        public ActionResult Excluir(long seq, long seqProcessoEtapa, long seqConfiguracaoEtapa)
        {
            try
            {
                this.GrupoDocumentoRequeridoService.Excluir(seq);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Grupo_Documento_Requerido, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcessoEtapa = new SMCEncryptedLong(seqProcessoEtapa), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
        }

    }
}