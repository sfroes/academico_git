using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.SRC.Views.ConfiguracaoProcesso.App_LocalResources;
using System;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.UI.Mvc.Html;
using System.Linq;
using SMC.Academico.Common.Areas.SRC.Enums;
using System.Collections.Generic;
using SMC.Framework.Util;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class ConfiguracaoProcessoController : SMCControllerBase
    {
        #region [ Services ]

        private IConfiguracaoProcessoService ConfiguracaoProcessoService
        {
            get { return this.Create<IConfiguracaoProcessoService>(); }
        }

        private IProcessoService ProcessoService
        {
            get { return this.Create<IProcessoService>(); }
        }

        private ITurnoService TurnoService
        {
            get { return this.Create<ITurnoService>(); }
        }

        private INivelEnsinoService NivelEnsinoService
        {
            get { return this.Create<INivelEnsinoService>(); }
        }

        private ITipoVinculoAlunoService TipoVinculoAlunoService
        {
            get { return this.Create<ITipoVinculoAlunoService>(); }
        }

        private IServicoService ServicoService
        {
            get { return this.Create<IServicoService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_SRC_002_02_01.PESQUISAR_CONFIGURACAO_PROCESSO)]
        public ActionResult Index(ConfiguracaoProcessoFiltroViewModel filtro)
        {
            var processo = this.ProcessoService.BuscarProcessoEditar(filtro.SeqProcesso);
            filtro.DescricaoProcesso = processo.Descricao;
            filtro.ProcessoEncerrado = processo.ProcessoEncerrado;

            return View(filtro);
        }

        [SMCAuthorize(UC_SRC_002_02_01.PESQUISAR_CONFIGURACAO_PROCESSO)]
        public ActionResult CabecalhoProcesso(long seqProcesso)
        {
            var modeloProcesso = ProcessoService.BuscarCabecalhoProcesso(seqProcesso, false).Transform<CabecalhoConfiguracaoProcessoViewModel>();
            return PartialView("_CabecalhoProcesso", modeloProcesso);
        }

        [SMCAuthorize(UC_SRC_002_02_01.PESQUISAR_CONFIGURACAO_PROCESSO)]
        public ActionResult ListarConfiguracoesProcesso(ConfiguracaoProcessoFiltroViewModel filtro)
        {
            SMCPagerModel<ConfiguracaoProcessoListarViewModel> model = ExecuteService<ConfiguracaoProcessoFiltroData, ConfiguracaoProcessoListarData,
                                                                         ConfiguracaoProcessoFiltroViewModel, ConfiguracaoProcessoListarViewModel>
                                                                         (ConfiguracaoProcessoService.BuscarConfiguracoesProcesso, filtro);

            return PartialView("_DetailList", model);
        }

        [SMCAuthorize(UC_SRC_002_02_02.MANTER_CONFIGURACAO_PROCESSO)]
        public ActionResult Incluir(long seqProcesso)
        {
            var processo = this.ProcessoService.BuscarProcessoEditar(seqProcesso);
            var servico = this.ServicoService.BuscarServico(processo.SeqServico);

            var modelo = new ConfiguracaoProcessoViewModel()
            {
                SeqProcesso = seqProcesso,
                DescricaoProcesso = processo.Descricao,
                Mensagem = string.Format(UIResource.Mensagem_Informativa, "<p>"),
                NiveisEnsinoDataSource = this.NivelEnsinoService.BuscarNiveisEnsinoPorServicoSelect(processo.SeqServico),
                TiposVinculoDataSource = this.TipoVinculoAlunoService.BuscarTiposVinculoAlunoPorServicoSelect(processo.SeqServico),
                SeqsEntidadesResponsaveis = processo.UnidadesResponsaveis.Where(a => a.TipoUnidadeResponsavel == TipoUnidadeResponsavel.EntidadeResponsavel).Select(a => a.SeqEntidadeResponsavel).ToList(),
                SeqsNiveisEnsino = servico.InstituicaoNivelServicos.Select(a => a.SeqNivelEnsino).Distinct().ToList(),
                TiposConfiguracaoDataSource = Enum.GetValues(typeof(TipoConfiguracaoProcesso)).Cast<TipoConfiguracaoProcesso>().Select(a => new SMCDatasourceItem() { Seq = (short)a, Descricao = SMCEnumHelper.GetDescription(a) }).ToList()
            };

            return View(modelo);
        }

        [SMCAuthorize(UC_SRC_002_02_02.MANTER_CONFIGURACAO_PROCESSO)]
        public ActionResult WizardStepTipoConfiguracao(ConfiguracaoProcessoViewModel modelo)
        {
            return PartialView("_WizardStepTipoConfiguracao", modelo);
        }

        [SMCAuthorize(UC_SRC_002_02_02.MANTER_CONFIGURACAO_PROCESSO)]
        public ActionResult WizardStepConfiguracao(ConfiguracaoProcessoViewModel modelo)
        {
            if (modelo.SeqTipoConfiguracao == (short)TipoConfiguracaoProcesso.NivelEnsino)
            {
                modelo.ExibirSecaoNivelEnsino = true;
                modelo.ExibirSecaoCursos = false;
            }
            else if (modelo.SeqTipoConfiguracao == (short)TipoConfiguracaoProcesso.OfertaCursoLocalidade)
            {
                modelo.ExibirSecaoCursos = true;
                modelo.ExibirSecaoNivelEnsino = false;
            }

            return PartialView("_WizardStepConfiguracao", modelo);
        }

        [SMCAuthorize(UC_SRC_002_02_02.MANTER_CONFIGURACAO_PROCESSO)]
        public ActionResult WizardStepConfirmacao(ConfiguracaoProcessoViewModel modelo)
        {
            //FIX: LIMPANDO OS VALORES DO MESTRE DETALHES SE ELE NÃO É EXIBIDO NA TELA (CORREÇÃO WIZZARD)
            if (!modelo.ExibirSecaoNivelEnsino)
                modelo.NiveisEnsino = new SMCMasterDetailList<ConfiguracaoProcessoNivelEnsinoViewModel>();
            if (!modelo.ExibirSecaoCursos)
                modelo.Cursos = new SMCMasterDetailList<ConfiguracaoProcessoCursoViewModel>();

            return PartialView("_WizardStepConfirmacao", modelo);
        }

        [SMCAuthorize(UC_SRC_002_02_02.MANTER_CONFIGURACAO_PROCESSO)]
        public ActionResult Editar(long seq)
        {
            var modelo = this.ConfiguracaoProcessoService.BuscarConfiguracaoProcesso(seq).Transform<ConfiguracaoProcessoViewModel>();

            var processo = this.ProcessoService.BuscarProcessoEditar(modelo.SeqProcesso);
            var servico = this.ServicoService.BuscarServico(processo.SeqServico);

            modelo.DescricaoProcesso = processo.Descricao;
            modelo.ProcessoEncerrado = processo.ProcessoEncerrado;
            modelo.Mensagem = string.Format(UIResource.Mensagem_Informativa, "<p>");
            modelo.NiveisEnsinoDataSource = this.NivelEnsinoService.BuscarNiveisEnsinoPorServicoSelect(processo.SeqServico);
            modelo.TiposVinculoDataSource = this.TipoVinculoAlunoService.BuscarTiposVinculoAlunoPorServicoSelect(processo.SeqServico);
            modelo.SeqsEntidadesResponsaveis = processo.UnidadesResponsaveis.Where(a => a.TipoUnidadeResponsavel == TipoUnidadeResponsavel.EntidadeResponsavel).Select(a => a.SeqEntidadeResponsavel).ToList();
            modelo.SeqsNiveisEnsino = servico.InstituicaoNivelServicos.Select(a => a.SeqNivelEnsino).Distinct().ToList();
            modelo.TiposConfiguracaoDataSource = Enum.GetValues(typeof(TipoConfiguracaoProcesso)).Cast<TipoConfiguracaoProcesso>().Select(a => new SMCDatasourceItem() { Seq = (short)a, Descricao = SMCEnumHelper.GetDescription(a) }).ToList();

            if (modelo.NiveisEnsino != null && modelo.NiveisEnsino.Any())
            {
                modelo.ExibirSecaoNivelEnsino = true;
                modelo.SeqTipoConfiguracao = (short)TipoConfiguracaoProcesso.NivelEnsino;
            }
            else if (modelo.Cursos != null && modelo.Cursos.Any())
            {
                modelo.ExibirSecaoCursos = true;
                modelo.SeqTipoConfiguracao = (short)TipoConfiguracaoProcesso.OfertaCursoLocalidade;
            }

            return View(modelo);
        }

        [SMCAuthorize(UC_SRC_002_02_02.MANTER_CONFIGURACAO_PROCESSO)]
        public ActionResult BuscarTurnosPorOfertaCursoLocalidadeSelect(long? seqCursoOfertaLocalidade)
        {
            if (seqCursoOfertaLocalidade.HasValue)
                return Json(this.TurnoService.BuscarTurnosPorCursoOfertaLocalidadeSelect(seqCursoOfertaLocalidade.GetValueOrDefault()));
            else
                return Json(this.TurnoService.BuscarTunos());
        }

        [SMCAuthorize(UC_SRC_002_02_02.MANTER_CONFIGURACAO_PROCESSO)]
        public ActionResult Salvar(ConfiguracaoProcessoViewModel modelo)
        {
            //FIX: LIMPANDO OS VALORES DO MESTRE DETALHES SE ELE NÃO É EXIBIDO NA TELA (CORREÇÃO WIZZARD)
            if (!modelo.ExibirSecaoNivelEnsino)
                modelo.NiveisEnsino = new SMCMasterDetailList<ConfiguracaoProcessoNivelEnsinoViewModel>();
            if (!modelo.ExibirSecaoCursos)
                modelo.Cursos = new SMCMasterDetailList<ConfiguracaoProcessoCursoViewModel>();

            long retorno = this.ConfiguracaoProcessoService.Salvar(modelo.Transform<ConfiguracaoProcessoData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Configuracao_Processo, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Editar), routeValues: new { seq = new SMCEncryptedLong(retorno) });
        }

        [SMCAuthorize(UC_SRC_002_02_02.MANTER_CONFIGURACAO_PROCESSO)]
        public ActionResult Excluir(long seq, long seqProcesso)
        {
            try
            {
                this.ConfiguracaoProcessoService.Excluir(seq);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Configuracao_Processo, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcesso = new SMCEncryptedLong(seqProcesso) });
        }
    }
}