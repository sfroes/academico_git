using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.UI.Mvc.Util;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using SMC.SGA.Administrativo.Areas.SRC.Views.ConfiguracaoEtapaPagina.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class ConfiguracaoEtapaPaginaController : SMCControllerBase
    {
        #region [ Services ]   

        private IProcessoService ProcessoService
        {
            get { return this.Create<IProcessoService>(); }
        }

        private IProcessoEtapaService ProcessoEtapaService
        {
            get { return this.Create<IProcessoEtapaService>(); }
        }

        private IConfiguracaoEtapaService ConfiguracaoEtapaService
        {
            get { return this.Create<IConfiguracaoEtapaService>(); }
        }

        private IConfiguracaoEtapaPaginaService ConfiguracaoEtapaPaginaService
        {
            get { return this.Create<IConfiguracaoEtapaPaginaService>(); }
        }

        private ITextoSecaoPaginaService TextoSecaoPaginaService
        {
            get { return this.Create<ITextoSecaoPaginaService>(); }
        }

        private IArquivoSecaoPaginaService ArquivoSecaoPaginaService
        {
            get { return this.Create<IArquivoSecaoPaginaService>(); }
        }

        private IArquivoAnexadoService ArquivoAnexadoService
        {
            get { return Create<IArquivoAnexadoService>(); }
        }

        #endregion [ Services ]

        #region Arvore

        [SMCAuthorize(UC_SRC_002_04_03.CONFIGURAR_ETAPA_FLUXO_PAGINAS)]
        public ActionResult Index(ConfiguracaoEtapaPaginaFiltroViewModel filtro)
        {
            var configuracaoEtapa = this.ConfiguracaoEtapaService.BuscarConfiguracaoEtapa(filtro.SeqConfiguracaoEtapa, IncludesConfiguracaoEtapa.Nenhum).Transform<ConfiguracaoEtapaViewModel>();
            var processoEtapa = this.ProcessoEtapaService.BuscarProcessoEtapa(configuracaoEtapa.SeqProcessoEtapa).Transform<ProcessoEtapaViewModel>();

            filtro.PossuiPaginas = this.ConfiguracaoEtapaPaginaService.VerificarConfiguracaoPossuiPaginas(filtro.SeqConfiguracaoEtapa);
            filtro.SeqProcessoEtapa = configuracaoEtapa.SeqProcessoEtapa;
            filtro.SituacaoEtapa = processoEtapa.SituacaoEtapa;
            filtro.SeqProcesso = processoEtapa.SeqProcesso;

            return View(filtro);
        }

        [SMCAuthorize(UC_SRC_002_04_03.CONFIGURAR_ETAPA_FLUXO_PAGINAS)]
        public ActionResult Cabecalho(long seqConfiguracaoEtapa)
        {
            var modeloCabecalho = ConfiguracaoEtapaService.BuscarCabecalhoConfiguracaoEtapa(seqConfiguracaoEtapa).Transform<CabecalhoConfiguracaoEtapaPaginaViewModel>();
            return PartialView("_Cabecalho", modeloCabecalho);
        }

        [SMCAuthorize(UC_SRC_002_04_03.CONFIGURAR_ETAPA_FLUXO_PAGINAS)]
        public ActionResult ArvoreConfiguracaoPaginaEtapa(long seqConfiguracaoEtapa)
        {
            ConfiguracaoEtapaPaginaFiltroViewModel filtro = new ConfiguracaoEtapaPaginaFiltroViewModel() { SeqConfiguracaoEtapa = seqConfiguracaoEtapa };

            List<ArvoreItemConfiguracaoPaginaEtapaViewModel> itens = this.ConfiguracaoEtapaPaginaService.BuscarArvoreConfiguracaoEtapa(filtro.Transform<ConfiguracaoEtapaPaginaFiltroData>()).TransformList<ArvoreItemConfiguracaoPaginaEtapaViewModel>();
            List<SMCTreeViewNode<ArvoreItemConfiguracaoPaginaEtapaViewModel>> itensArvore = SMCTreeView.For<ArvoreItemConfiguracaoPaginaEtapaViewModel>(itens);

            return PartialView("_ArvoreItemConfiguracaoPaginaEtapa", itensArvore);
        }

        #endregion

        #region Recuperar Página Excluida

        [SMCAuthorize(UC_SRC_002_04_04.CONFIGURAR_ETAPA_ASSOCIAR_PAGINA_TEMPLATE)]
        public ActionResult RecuperarPaginasConfiguracaoPaginaEtapa(long seqConfiguracaoEtapa)
        {
            var paginasNaoCriadas = this.ConfiguracaoEtapaService.BuscarPaginasNaoCriadas(seqConfiguracaoEtapa);

            var modelo = new RecuperarPaginaEtapaViewModel
            {
                SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                PaginasDisponiveis = new SMCMasterDetailList<RecuperarPaginaEtapaItemViewModel>()
            };

            foreach (var pagina in paginasNaoCriadas)
            {
                modelo.PaginasDisponiveis.Add(new RecuperarPaginaEtapaItemViewModel()
                {
                    Descricao = pagina.Descricao,
                    Seq = pagina.Seq
                });
            }

            return PartialView("_RecuperarPaginasEtapa", modelo);
        }

        [SMCAuthorize(UC_SRC_002_04_04.CONFIGURAR_ETAPA_ASSOCIAR_PAGINA_TEMPLATE)]
        public ActionResult SalvarRecuperacaoPaginasConfiguracaoPaginaEtapa(RecuperarPaginaEtapaViewModel modelo)
        {
            var selecionados = modelo.PaginasDisponiveis?.Where(c => c.Selected).ToList();
            List<(long, ConfiguracaoDocumento?)> seqsPaginasEConfiguracoesDocumento = new List<(long, ConfiguracaoDocumento?)>();

            if (selecionados != null)
            {
                foreach (var pagina in selecionados)
                {
                    seqsPaginasEConfiguracoesDocumento.Add((pagina.Seq, pagina.ConfiguracaoDocumento));
                }

                if (seqsPaginasEConfiguracoesDocumento.Count > 0)
                {
                    this.ConfiguracaoEtapaPaginaService.AdicionarPaginas(modelo.SeqConfiguracaoEtapa, seqsPaginasEConfiguracoesDocumento);

                    SetSuccessMessage(UIResource.Mensagem_Sucesso_Configuracao_Etapa_Pagina, target: SMCMessagePlaceholders.Centro);
                }
            }

            return RenderAction("ArvoreConfiguracaoPaginaEtapa", modelo.SeqConfiguracaoEtapa);
        }

        #endregion Recuperar Página Excluida

        #region Configurar Página

        [SMCAuthorize(UC_SRC_002_04_07.CONFIGURAR_ETAPA_PAGINA)]
        public ActionResult ConfigurarPaginaConfiguracaoEtapaPagina(long seqConfiguracaoEtapaPagina)
        {
            var modelo = this.ConfiguracaoEtapaPaginaService.BuscarConfiguracaoEtapaPagina(seqConfiguracaoEtapaPagina).Transform<ConfiguracaoEtapaPaginaViewModel>();

            modelo.PaginasSGF = this.ConfiguracaoEtapaPaginaService.BuscarPaginasPorEtapa(modelo.SeqEtapaSgf);
            modelo.Formularios = this.ConfiguracaoEtapaPaginaService.BuscarFormularios();

            if (modelo.SeqFormulario.HasValue)
                modelo.VisoesFormulario = this.ConfiguracaoEtapaPaginaService.BuscarVisoesPorFormularioSelect(modelo.SeqFormulario.Value);

            return PartialView("_ConfigurarEtapaPagina", modelo);
        }

        [SMCAuthorize(UC_SRC_002_04_07.CONFIGURAR_ETAPA_PAGINA)]
        public ActionResult BuscarVisoesPorFormularioSelect(long seqFormulario)
        {
            return Json(this.ConfiguracaoEtapaPaginaService.BuscarVisoesPorFormularioSelect(seqFormulario));
        }

        [SMCAuthorize(UC_SRC_002_04_07.CONFIGURAR_ETAPA_PAGINA)]
        public ActionResult SalvarConfiguracaoPaginaEtapa(ConfiguracaoEtapaPaginaViewModel modelo)
        {
            this.ConfiguracaoEtapaPaginaService.Salvar(modelo.Transform<ConfiguracaoEtapaPaginaData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Configuracao_Etapa_Pagina, target: SMCMessagePlaceholders.Centro);

            return RenderAction("ArvoreConfiguracaoPaginaEtapa", modelo.SeqConfiguracaoEtapa);
        }

        #endregion Configurar Página

        #region Duplicar/Excluir página

        [SMCAuthorize(UC_SRC_002_04_04.CONFIGURAR_ETAPA_ASSOCIAR_PAGINA_TEMPLATE)]
        public ActionResult ExcluirConfiguracaoEtapaPagina(long seqConfiguracaoEtapa, long seqConfiguracaoEtapaPagina)
        {
            try
            {
                this.ConfiguracaoEtapaPaginaService.ValidarModeloExcluir(seqConfiguracaoEtapaPagina);

                /********************** VALIDAÇÕES DE ASSERT **********************/

                var modelo = this.ConfiguracaoEtapaPaginaService.BuscarConfiguracaoEtapaPagina(seqConfiguracaoEtapaPagina).Transform<ConfiguracaoEtapaPaginaViewModel>();

                Assert(modelo, string.Format(UIResource.Exclusao_Configuracao_Etapa_Pagina_Confirmacao, modelo.TituloPagina));

                /********************** FIM VALIDAÇÕES DE ASSERT **********************/

                this.ConfiguracaoEtapaPaginaService.Excluir(seqConfiguracaoEtapaPagina);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Configuracao_Etapa_Pagina, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return RenderAction("ArvoreConfiguracaoPaginaEtapa", seqConfiguracaoEtapa);
        }

        [SMCAuthorize(UC_SRC_002_04_03.CONFIGURAR_ETAPA_FLUXO_PAGINAS)]
        public ActionResult DuplicarPaginaConfiguracaoEtapaPagina(long seqConfiguracaoEtapa, long seqConfiguracaoEtapaPagina)
        {
            this.ConfiguracaoEtapaPaginaService.DuplicarConfiguracaoEtapaPagina(seqConfiguracaoEtapaPagina);

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Duplicar_Pagina, target: SMCMessagePlaceholders.Centro);

            return RenderAction("ArvoreConfiguracaoPaginaEtapa", seqConfiguracaoEtapa);
        }

        #endregion Duplicar/Excluir página

        #region Seção Texto

        [SMCAuthorize(UC_SRC_002_04_05.CONFIGURAR_ETAPA_PAGINA_TEXTO)]
        public ActionResult ConfigurarTextoSecaoConfiguracaoPaginaEtapa(long seq, string pagina, string secao)
        {
            var modelo = this.TextoSecaoPaginaService.BuscarTextoSecaoPagina(seq).Transform<TextoSecaoPaginaViewModel>();

            modelo.DescricaoPagina = pagina;
            modelo.DescricaoSecao = secao;

            return PartialView("_ConfigurarTextoSecao", modelo);
        }

        [SMCAuthorize(UC_SRC_002_04_05.CONFIGURAR_ETAPA_PAGINA_TEXTO)]
        public void SalvarConfiguracaoTextoSecao(TextoSecaoPaginaViewModel modelo)
        {
            long retorno = this.TextoSecaoPaginaService.Salvar(modelo.Transform<TextoSecaoPaginaData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Configuracao_Texto, target: SMCMessagePlaceholders.Centro);
        }

        #endregion Seção Texto

        #region Seção Arquivo
        [SMCAuthorize(UC_SRC_002_04_06.CONFIGURAR_ETAPA_PAGINA_ARQUIVO)]
        public ActionResult ConfigurarArquivoSecaoConfiguracaoPaginaEtapa(long seqSecaoPaginaSGF, long seqConfiguracaoEtapaPagina, string pagina, string secao)
        {
            var filtro = new ArquivoSecaoPaginaFiltroViewModel()
            {
                SeqConfiguracaoEtapaPagina = seqConfiguracaoEtapaPagina,
                SeqSecaoPaginaSgf = seqSecaoPaginaSGF
            };

            var modelo = new ConfigurarArquivoSecaoViewModel();
            modelo.SeqConfiguracaoEtapaPagina = seqConfiguracaoEtapaPagina;
            modelo.SeqSecaoPaginaSgf = seqSecaoPaginaSGF;
            modelo.DescricaoPagina = pagina;
            modelo.DescricaoSecao = secao;
            modelo.Arquivos.AddRange(this.ArquivoSecaoPaginaService.BuscarArquivosSecaoPagina(filtro.Transform<ArquivoSecaoPaginaFiltroData>()).TransformList<ArquivoSecaoPaginaViewModel>());
            //modelo.CamposReadOnly = this.ArquivoSecaoPaginaService.ValidaConfigurarArquivoSecaoReadOnly(seqConfiguracaoEtapaPagina);

            return PartialView("_ConfigurarArquivoSecao", modelo);
        }

        [SMCAuthorize(UC_SRC_002_04_06.CONFIGURAR_ETAPA_PAGINA_ARQUIVO)]
        public void SalvarConfiguracaoArquivoSecao(ConfigurarArquivoSecaoViewModel modelo)
        {
            foreach (var item in modelo.Arquivos)
            {
                item.ArquivoAnexado.FileData = SMCUploadHelper.GetFileData(item.ArquivoAnexado);
                item.SeqSecaoPaginaSgf = modelo.SeqSecaoPaginaSgf;
                item.SeqConfiguracaoEtapaPagina = modelo.SeqConfiguracaoEtapaPagina;
            }

            this.ArquivoSecaoPaginaService.Salvar(modelo.SeqConfiguracaoEtapaPagina, modelo.SeqSecaoPaginaSgf, modelo.Arquivos.TransformList<ArquivoSecaoPaginaData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Configuracao_Arquivo, target: SMCMessagePlaceholders.Centro);
        }

        #endregion Seção Arquivo       
    }
}