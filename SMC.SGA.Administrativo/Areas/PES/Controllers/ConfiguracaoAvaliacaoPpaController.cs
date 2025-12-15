using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.AvaliacaoPermanente.ServiceContract.Areas.QST.Interfaces;
using SMC.SGA.Administrativo.Areas.PES.Views.ConfiguracaoAvaliacaoPpa.App_LocalResources;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class ConfiguracaoAvaliacaoPpaController : SMCDynamicControllerBase
    {
        #region [ Services ]
        private IConfiguracaoAvaliacaoPpaService ConfiguracaoAvaliacaoPpaService => this.Create<IConfiguracaoAvaliacaoPpaService>();

        private IConfiguracaoAvaliacaoPpaTurmaService ConfiguracaoAvaliacaoPpaTurmaService => this.Create<IConfiguracaoAvaliacaoPpaTurmaService>();

        private IPessoaAtuacaoAmostraPpaService PessoaAtuacaoAmostraPpaService => this.Create<IPessoaAtuacaoAmostraPpaService>();

        private IAmostraService AmostraService => Create<IAmostraService>();

        #endregion

        [SMCAllowAnonymous]
        public ActionResult BuscarDataAvaliacaoIntitucionalSelecionada(int? codigoAvaliacaoPpa)
        {
            if (!codigoAvaliacaoPpa.HasValue)
            {
                return Json(new
                {
                    DataInicioVigencia = string.Empty,
                    DataFimVigencia = string.Empty,
                });
            }

            var resultado = AmostraService.BuscarDataAvaliacaoSelecionada(codigoAvaliacaoPpa.Value);

            return Json(new
            {
                DataInicioVigencia = resultado.DataInicio,
                DataFimVigencia = resultado.DataFim,
            });
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarEspecieAvaliadorSelecionado(int? codigoAvaliacaoPpa)
        {
            if (!codigoAvaliacaoPpa.HasValue)
                return Json(null);

            var resultado = AmostraService.BuscarEspecieAvaliador(codigoAvaliacaoPpa.Value);

            return Json(resultado);
        }

        [SMCAuthorize(UC_PES_007_01_03.CONSULTAR_TURMAS_CONFIGURACAO)]
        public ActionResult CabecalhoTurma(long seq)
        {
            var modelHeader = ConfiguracaoAvaliacaoPpaTurmaService.BuscarCabecalhoConfiguracaoAvaliacaoPpaTurma(seq);
            var model = modelHeader.Transform<ConsultaTurmaAmostraViewModel>();
            
            model.NomeCabecalho = "ConsultaTurmas";

            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_PES_007_01_04.CONSULTAR_AMOSTRAS)]
        public ActionResult CabecalhoAmostra(long seqConfiguracaoAvaliacaoPpa, long? seqConfiguracaoAvaliacaoPpaTurma)
        {
            var modelHeader = PessoaAtuacaoAmostraPpaService.BuscarCabecalhoPessoaAtuacaoAmostraPpa(seqConfiguracaoAvaliacaoPpa, seqConfiguracaoAvaliacaoPpaTurma);

            var model = modelHeader.Transform<ConsultaTurmaAmostraViewModel>();
            model.NomeCabecalho = "ConsultaAmostras";

            if (seqConfiguracaoAvaliacaoPpaTurma != null)
            {
                model.ExibirTurma = true;
            }

            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_PES_007_01_05.ALTERAR_DATA_LIMITE_RESPOSTA)]
        public ActionResult CabecalhoAlterarData(ConsultaTurmaAmostraViewModel model)
        {
            model.NomeCabecalho = "AlterarDataLimiteResposta";
            model.ExibirDatas = true;

            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_PES_007_01_03.CONSULTAR_TURMAS_CONFIGURACAO)]
        public ActionResult ConsultarTurma(ConsultaTurmaAmostraFiltroViewModel filtro)
        {
            var model = filtro.Transform<ConsultaTurmaAmostraViewModel>();

            return View("ConsultaConfiguracaoAvaliacaoPpaTurma", model);
        }

        [SMCAuthorize(UC_PES_007_01_03.CONSULTAR_TURMAS_CONFIGURACAO)]
        public ActionResult ListarTurmas(ConsultaTurmaAmostraFiltroViewModel filtro)
        {
            var filtroData = filtro.Transform<ConfiguracaoAvaliacaoPpaTurmaFiltroData>();
            var listaData = ConfiguracaoAvaliacaoPpaTurmaService.ListarTurmas(filtroData);
            var listaTurmas = new SMCPagerModel<ConsultaConfiguracaoAvaliacaoPpaTurmaListarViewModel>
                (listaData.TransformList<ConsultaConfiguracaoAvaliacaoPpaTurmaListarViewModel>(), filtro.PageSettings, filtro);

            filtro.PageSettings.Total = listaData.Total;

            return PartialView("_ListarTurmas", listaTurmas);
        }

        [SMCAuthorize(UC_PES_007_01_04.CONSULTAR_AMOSTRAS)]
        public ActionResult ConsultarAmostra(ConsultaTurmaAmostraFiltroViewModel filtro)
        {
            var model = filtro.Transform<ConsultaTurmaAmostraViewModel>();

            return View("ConsultaPessoaAtuacaoAmostra", model);
        }

        [SMCAuthorize(UC_PES_007_01_04.CONSULTAR_AMOSTRAS)]
        public ActionResult ListarAmostras(ConsultaTurmaAmostraFiltroViewModel filtro)
        {

            var filtroData = filtro.Transform<PessoaAtuacaoAmostraPpaFiltroData>();
            var listaData = PessoaAtuacaoAmostraPpaService.ListarAmostras(filtroData);

            var listaAmostras = new SMCPagerModel<ConsultaPessoaAtuacaoAmostraPpaListarViewModel>
                (listaData.TransformList<ConsultaPessoaAtuacaoAmostraPpaListarViewModel>(), filtro.PageSettings, filtro);

            filtro.PageSettings.Total = listaData.Total;

            return PartialView("_ListarAmostras", listaAmostras);
        }

        [SMCAuthorize(UC_PES_007_01_05.ALTERAR_DATA_LIMITE_RESPOSTA)]
        public ActionResult AlterarDataLimite(SMCEncryptedLong seqConfiguracaoAvaliacaoPpa)
        {
            var dados = ConfiguracaoAvaliacaoPpaService.BuscarConfiguracaoAvaliacao(seqConfiguracaoAvaliacaoPpa);

            ConsultaTurmaAmostraViewModel model = new ConsultaTurmaAmostraViewModel();

            model.DescricaoConfiguracaoAvaliacaoPpa = dados.Descricao;
            model.EntidadeResponsavel = dados.NomeEntidadeResponsavel;
            model.TipoAvaliacao = dados.TipoAvaliacaoPpa;
            model.DataInicio = dados.DataInicioVigencia;
            model.DataFim = dados.DataFimVigencia;

            model.DataLimiteResposta = dados.DataLimiteRespostas;
            model.SeqConfiguracaoAvaliacaoPpa = seqConfiguracaoAvaliacaoPpa;

            return PartialView("_EditarDataLimiteResposta", model);
        }

        [SMCAuthorize(UC_PES_007_01_05.ALTERAR_DATA_LIMITE_RESPOSTA)]
        public ActionResult SalvarDataLimiteResposta(ConsultaTurmaAmostraViewModel model)
        {
            ConfiguracaoAvaliacaoPpaService.SalvarAlteracaoDataLimiteResposta(model.SeqConfiguracaoAvaliacaoPpa.Value, model.DataLimiteResposta.Value);
            SetSuccessMessage(UIResource.DataLimiteAlteradaSucesso, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Index", "ConfiguracaoAvaliacaoPpa", new { @area = "PES", @seqConfiguracaoAvaliacaoPpa = model.SeqConfiguracaoAvaliacaoPpa });
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarInstrumentos(int? codigoAvaliacaoPpa)
        {
            if (!codigoAvaliacaoPpa.HasValue)
                return Json(null);

            var lista = AmostraService.BuscarInstrumentos(codigoAvaliacaoPpa);
            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem() { Seq = item.Seq, Descricao = $"{item.Seq} - {item.Descricao}" });
            }

            return Json(retorno);
        }

    }
}