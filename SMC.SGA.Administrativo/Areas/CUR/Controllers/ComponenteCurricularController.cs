using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using SMC.SGA.Administrativo.Areas.CUR.Views.ComponenteCurricular.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class ComponenteCurricularController : SMCDynamicControllerBase
    {
        #region [ Service ]

        private IComponenteCurricularService ComponenteCurricularService
        {
            get { return this.Create<IComponenteCurricularService>(); }
        }

        private IInstituicaoNivelTipoComponenteCurricularService InstituicaoNivelTipoComponenteCurricularService
        {
            get { return this.Create<IInstituicaoNivelTipoComponenteCurricularService>(); }
        }

        private IInstituicaoNivelService InstituicaoNivelService
        {
            get { return this.Create<IInstituicaoNivelService>(); }
        }

        #endregion [ Service ]

        [SMCAuthorize(UC_CUR_002_01_01.PESQUISAR_COMPONENTE_CURRICULAR)]
        public ActionResult CabecalhoLista(ComponenteCurricularDynamicModel model)
        {
            return PartialView("_HeaderList", model);
        }

        [SMCAuthorize(UC_CUR_002_01_01.PESQUISAR_COMPONENTE_CURRICULAR)]
        public JsonResult BuscarTipoComponenteCurricularSelect(long seqInstituicaoNivelResponsavel, bool filtro = false)
        {
            List<SMCDatasourceItem> listItens = InstituicaoNivelTipoComponenteCurricularService.BuscarTipoComponenteCurricularSelect(seqInstituicaoNivelResponsavel);

            if (listItens.Count == 0)
                if (filtro)
                    SetErrorMessage(new Exception(UIResource.MSG_NaoExisteTipoComponenteNivelEnsino), target: SMCMessagePlaceholders.Centro);
                else
                    this.ThrowRedirect(new Exception(UIResource.MSG_NaoExisteTipoComponenteNivelEnsinoCadastro), "Incluir", null);

            return Json(listItens);
        }

        [SMCAuthorize(UC_CUR_002_01_01.PESQUISAR_COMPONENTE_CURRICULAR)]
        public JsonResult BuscarEntidadesPorTipoComponenteSelect(ComponenteCurricularFiltroDynamicModel model)
        {
            List<SMCDatasourceItem> listItens = new List<SMCDatasourceItem>();

            if (model.SeqInstituicaoNivelResponsavel > 0 && model.SeqTipoComponenteCurricular > 0)
            {
                listItens = InstituicaoNivelTipoComponenteCurricularService.BuscarEntidadesPorTipoComponenteSelect(model.SeqInstituicaoNivelResponsavel.Value, model.SeqTipoComponenteCurricular.Value);
            }

            return Json(listItens);
        }

        [SMCAuthorize(UC_CUR_002_01_02.MANTER_COMPONENTE_CURRICULAR)]
        public ActionResult StepTipoComponenteCurricular(ComponenteCurricularDynamicModel model)
        {
            //Chamada do configure após popular os valores do datasource
            this.ConfigureDynamic(model);

            var configuracao = InstituicaoNivelTipoComponenteCurricularService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(model.SeqInstituicaoNivelResponsavel, model.SeqTipoComponenteCurricular);
            model.SeqInstituicaoEnsino = configuracao.SeqInstituicaoEnsino;
            model.Codigo = "0";
            model.DescricaoReduzidaObrigatorio = configuracao.NomeReduzidoObrigatorio;
            model.SiglaObrigatorio = configuracao.SiglaObrigatoria;
            model.CargaHorariaDisplay = configuracao.ExibeCargaHoraria;
            model.CargaHorariaObrigatorio = configuracao.ExigeCargaHoraria;
            model.CreditoDisplay = configuracao.ExibeCredito;
            model.CreditoObrigatorio = configuracao.ExigeCredito;
            model.QuantidadeHorasCredito = configuracao.QuantidadeHorasPorCredito;
            model.EmentaDisplay = configuracao.PermiteEmenta;
            model.OrgaoReguladorDisplay = configuracao.TipoOrgaoRegulador != TipoOrgaoRegulador.Nenhum;
            model.RegistroTipoOrgaoRegulador = configuracao.TipoOrgaoRegulador;
            model.PermiteAssuntoComponente = configuracao.PermiteAssuntoComponente;

            if (model.Seq == 0)
                model.QuantidadeSemanas = configuracao.QuantidadeHorasPorCredito;

            if (configuracao.PermiteEmenta && (model.Ementas == null || model.Ementas.Count == 0))
            {
                model.Ementas = new Framework.UI.Mvc.Html.SMCMasterDetailList<ComponenteCurricularEmentaViewModel>();
                model.Ementas.Add(new ComponenteCurricularEmentaViewModel());
            }

            // Cria as opções para view padrão
            SMCDefaultViewWizardOptions options = new SMCDefaultViewWizardOptions();
            options.Title = "ComponenteCurricularDynamicModel";
            options.ActionSave = "Salvar";

            this.SetViewMode(Framework.SMCViewMode.Insert);

            // Retorna o passo do wizard
            return SMCViewWizard(model, options);
        }

        [SMCAuthorize(UC_CUR_002_01_02.MANTER_COMPONENTE_CURRICULAR)]
        public string CargaHorariaCredito(ComponenteCurricularDynamicModel model)
        {
            if (model.CargaHorariaDisplay
             && model.CreditoDisplay
             && model.CargaHoraria > 0
             && model.QuantidadeHorasCredito > 0)
            {
                model.Credito = (short)(model.CargaHoraria / model.QuantidadeHorasCredito);
            }
            else
            {
                model.Credito = null;
            }

            // Retorna o passo do wizard
            return model.Credito.ToString();
        }

        [SMCAuthorize(UC_CUR_002_01_02.MANTER_COMPONENTE_CURRICULAR)]
        public ActionResult StepOrganizacao(ComponenteCurricularDynamicModel model)
        {
            //Chamada do configure após popular os valores do datasource
            this.ConfigureDynamic(model);

            var configuracao = InstituicaoNivelTipoComponenteCurricularService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(model.SeqInstituicaoNivelResponsavel, model.SeqTipoComponenteCurricular);

            model.TipoOrganizacaoDisplay = configuracao.PermiteSubdivisaoOrganizacao;
            model.OrganizacoesDisplay = configuracao.PermiteSubdivisaoOrganizacao;
            model.EntidadesResponsaveisObrigatorio = configuracao.EntidadeResponsavelObrigatoria;

            // Caso alguma entidade responsável selecionada não seja válida, limpa as entidades responsáveis
            if (model.EntidadesResponsaveis.SMCCount() > 0)
            {
                var entidadesValidas = model.EntidadesResponsavel.Select(s => s.Seq);
                if (model.EntidadesResponsaveis.Any(a => !entidadesValidas.Contains(a.SeqEntidade)))
                    model.EntidadesResponsaveis = null;
            }

            // Caso seja obrigatório ter entidades responsáveis e o mestre detalhe não esteja inicializado, inicializa
            if (model.EntidadesResponsaveisObrigatorio && model.EntidadesResponsaveis.SMCCount() == 0)
            {
                model.EntidadesResponsaveis = new SMCMasterDetailList<ComponenteCurricularEntidadeResponsavelViewModel>();
                model.EntidadesResponsaveis.Add(new ComponenteCurricularEntidadeResponsavelViewModel());
            }

            // Cria as opções para view padrão
            SMCDefaultViewWizardOptions options = new SMCDefaultViewWizardOptions();
            options.Title = "ComponenteCurricularDynamicModel";
            options.ActionSave = "Salvar";

            this.SetViewMode(Framework.SMCViewMode.Insert);

            // Retorna o passo do wizard
            return SMCViewWizard(model, options);
        }

        [SMCAuthorize(UC_CUR_002_03_01.VISUALIZAR_DETALHES_COMPONENTE_CURRICULAR)]
        public ActionResult VerDetalhes(SMCEncryptedLong seq)
        {
            // Busca as informações do Componente curricular para o cabeçalho
            ComponenteCurricularDetalheViewModel model = ExecuteService<ComponenteCurricularDetalheData, ComponenteCurricularDetalheViewModel>(ComponenteCurricularService.BuscarComponenteCurricularDetalhe, seq);
            return View("_VerDetalhes", model);
        }

        [SMCAuthorize(UC_CUR_002_03_01.VISUALIZAR_DETALHES_COMPONENTE_CURRICULAR)]
        public ActionResult VerDetalhesPartial(SMCEncryptedLong seq)
        {
            // Busca as informações do Componente curricular para o cabeçalho
            ComponenteCurricularDetalheViewModel model = ExecuteService<ComponenteCurricularDetalheData, ComponenteCurricularDetalheViewModel>(ComponenteCurricularService.BuscarComponenteCurricularDetalhe, seq);
            return PartialView("_VerDetalhes", model);
        }

        [SMCAuthorize(UC_CUR_002_01_02.MANTER_COMPONENTE_CURRICULAR)]
        public ActionResult TipoOrgaoReguladorInstituicaoNivel(TipoOrgaoRegulador registroTipoOrgaoRegulador)
        {
            return Json(SMCEnumHelper.GetDescription(registroTipoOrgaoRegulador));
        }
    }
}