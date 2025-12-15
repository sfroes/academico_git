using System;
using System.Web.Mvc;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using SMC.Notificacoes.UI.Mvc.Models;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using SMC.SGA.Administrativo.Areas.ORG.Views.EntidadeConfiguracaoNotificacao.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.ORG.Controllers
{
    public class EntidadeConfiguracaoNotificacaoController : SMCControllerBase
    {
        #region Services

        internal SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces.INotificacaoService NotificacaoService => Create<INotificacaoService>();

        private Academico.ServiceContract.Areas.SRC.Interfaces.ITipoNotificacaoService TipoNotificacaoService => Create<Academico.ServiceContract.Areas.SRC.Interfaces.ITipoNotificacaoService>();

        private IEntidadeService EntidadeService => Create<IEntidadeService>();

        private IEntidadeConfiguracaoNotificacaoService EntidadeConfiguracaoNotificacaoService => Create<IEntidadeConfiguracaoNotificacaoService>();

        #endregion

        [SMCAuthorize(UC_ORG_002_09_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO_ENTIDADE)]
        public ActionResult Index()
        {
            var modelo = new EntidadeConfiguracaoNotificacaoFiltroViewModel()
            {
                Entidades = this.EntidadeService.BuscarUnidadesResponsaveisCorrespondenciaNotificacaoSelect(),
                TiposNotificacao = this.TipoNotificacaoService.BuscarTiposNotificacaoSelect()
            };

            return View(modelo);
        }

        [SMCAuthorize(UC_ORG_002_09_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO_ENTIDADE)]
        public ActionResult BuscarTipoNotificacao(long? seqEntidade)
        {
            if (seqEntidade.HasValue)
            {
                var entidade = this.EntidadeService.BuscarEntidade(seqEntidade.Value);
                return Json(this.TipoNotificacaoService.BuscarTiposNotificacaoPorUnidadeResponsavelSelect(entidade.SeqUnidadeResponsavelNotificacao.Value));
            }

            return Json(this.TipoNotificacaoService.BuscarTiposNotificacaoSelect());
        }

        [SMCAuthorize(UC_ORG_002_09_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO_ENTIDADE)]
        public ActionResult ListarEntidadeConfiguracaoNotificacao(EntidadeConfiguracaoNotificacaoFiltroViewModel filtro)
        {
            var result = this.EntidadeConfiguracaoNotificacaoService.BuscarEntidadeConfiguracaoNotificacao(filtro.Transform<EntidadeConfiguracaoNotificacaoFiltroData>());

            filtro.PageSettings.Total = result.Total;

            var model = new SMCPagerModel<EntidadeConfiguracaoNotificacaoListarViewModel>(result.TransformList<EntidadeConfiguracaoNotificacaoListarViewModel>(), filtro.PageSettings, filtro);

            return PartialView("_DetailList", model);
        }

        [SMCAuthorize(UC_ORG_002_09_02.MANTER_CONFIGURACAO_NOTIFICACAO_ENTIDADE)]
        public ActionResult Incluir()
        {
            var modelo = new EntidadeConfiguracaoNotificacaoViewModel()
            {
                Entidades = this.EntidadeService.BuscarUnidadesResponsaveisCorrespondenciaNotificacaoSelect(),
                TiposNotificacao = this.TipoNotificacaoService.BuscarTiposNotificacaoSelect(),
                DataInicioValidade = DateTime.Now
            };

            return View(modelo);
        }

        [SMCAuthorize(UC_ORG_002_09_02.MANTER_CONFIGURACAO_NOTIFICACAO_ENTIDADE)]
        public ActionResult WizardStepTipoNotificacao(EntidadeConfiguracaoNotificacaoViewModel modelo)
        {
            return PartialView("_WizardStepTipoNotificacao", modelo);
        }

        [SMCAuthorize(UC_ORG_002_09_02.MANTER_CONFIGURACAO_NOTIFICACAO_ENTIDADE)]
        public ActionResult BuscarObservacaoTipoNotificacao(long? seqTipoNotificacao, long? valDep)
        {
            seqTipoNotificacao = seqTipoNotificacao ?? valDep;

            var modelo = this.TipoNotificacaoService.BuscarTipoNotificacao(seqTipoNotificacao.GetValueOrDefault()).Transform<TipoNotificacaoViewModel>();

            return PartialView("_ObservacaoTipoNotificacao", modelo);
        }

        [SMCAuthorize(UC_ORG_002_09_02.MANTER_CONFIGURACAO_NOTIFICACAO_ENTIDADE)]
        public ActionResult WizardStepConfiguracaoEmail(EntidadeConfiguracaoNotificacaoViewModel modelo)
        {
            modelo.DescricaoTipoNotificacao = this.TipoNotificacaoService.BuscarTipoNotificacao(modelo.SeqTipoNotificacao).Descricao;
            var entidade = this.EntidadeService.BuscarEntidade(modelo.SeqEntidade);
            modelo.LayoutEmail = NotificacaoService.BuscarLayoutNotificacaoEmailPorSiglaGrupoAplicacao(SIGLA_APLICACAO.GRUPO_APLICACAO);
            if (modelo.ConfiguracaoNotificacao == null)
            {
                modelo.ConfiguracaoNotificacao = new ConfiguracaoNotificacaoEmailViewModel()
                {
                    SeqTipoNotificacao = modelo.SeqTipoNotificacao,
                    DataInicioValidade = DateTime.Now,
                    Descricao = $"{modelo.DescricaoTipoNotificacao} - {entidade.Nome}"
                };
            }
            else
            {
                
                if (modelo.SeqTipoNotificacaoComparacao > 0 && modelo.SeqTipoNotificacao != modelo.SeqTipoNotificacaoComparacao)
                {
                    modelo.ConfiguracaoNotificacao.SeqTipoNotificacao = modelo.SeqTipoNotificacao;
                    modelo.ConfiguracaoNotificacao.Descricao = $"{modelo.DescricaoTipoNotificacao} - {entidade.Nome}";
                }
            }

            modelo.SeqTipoNotificacaoComparacao = modelo.SeqTipoNotificacao;

            return PartialView("_WizardStepConfiguracaoEmail", modelo);
        }

        [SMCAuthorize(UC_ORG_002_09_02.MANTER_CONFIGURACAO_NOTIFICACAO_ENTIDADE)]
        public ActionResult WizardStepConfirmacao(EntidadeConfiguracaoNotificacaoViewModel modelo)
        {
            return PartialView("_WizardStepConfirmacao", modelo);
        }

        [SMCAuthorize(UC_ORG_002_09_02.MANTER_CONFIGURACAO_NOTIFICACAO_ENTIDADE)]
        public ActionResult Salvar(EntidadeConfiguracaoNotificacaoViewModel modelo)
        {
            long retorno = this.EntidadeConfiguracaoNotificacaoService.Salvar(modelo.Transform<EntidadeConfiguracaoNotificacaoData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Configuracao_Notificacao, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Editar), routeValues: new { seq = new SMCEncryptedLong(retorno) });
        }

        [SMCAuthorize(UC_ORG_002_09_02.MANTER_CONFIGURACAO_NOTIFICACAO_ENTIDADE)]
        public ActionResult Editar(long seq)
        {
            var modelo = this.EntidadeConfiguracaoNotificacaoService.BuscarEntidadeConfiguracaoNotificacao(seq).Transform<EntidadeConfiguracaoNotificacaoViewModel>();
            var entidade = this.EntidadeService.BuscarEntidade(modelo.SeqEntidade);

            modelo.Entidades = this.EntidadeService.BuscarUnidadesResponsaveisCorrespondenciaNotificacaoSelect();           
            modelo.ExisteRegistroEnvioNotificacaoConfiguracao = this.TipoNotificacaoService.VerificarConfiguracaoPossuiNotificacoes(modelo.SeqConfiguracaoTipoNotificacao);

            if (entidade.SeqUnidadeResponsavelNotificacao.HasValue)
                modelo.TiposNotificacao = this.TipoNotificacaoService.BuscarTiposNotificacaoPorUnidadeResponsavelSelect(entidade.SeqUnidadeResponsavelNotificacao.Value);
            else
                modelo.TiposNotificacao = this.TipoNotificacaoService.BuscarTiposNotificacaoSelect();

            return View("Editar", modelo);
        }

        [SMCAuthorize(UC_ORG_002_09_02.MANTER_CONFIGURACAO_NOTIFICACAO_ENTIDADE)]
        public ActionResult Excluir(long seq)
        {
            try
            {
                this.EntidadeConfiguracaoNotificacaoService.Excluir(seq);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Notificacao, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index));
        }
    }
}