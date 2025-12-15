using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class InstituicaoNivelTipoComponenteCurricularController : SMCDynamicControllerBase
    {
        #region Serviços

        private ITipoDivisaoComponenteService TipoDivisaoComponenteService => this.Create<ITipoDivisaoComponenteService>(); 

        private IInstituicaoNivelService InstituicaoNivelService => this.Create<IInstituicaoNivelService>(); 

        #endregion Serviços

        [SMCAuthorize(UC_CUR_004_01_02.MANTER_TIPO_COMPONENTE_INSTITUICAO_NIVEL_ENSINO)]
        public ActionResult StepTipoDivisaoComponente(InstituicaoNivelTipoComponenteCurricularDynamicModel model)
        {
            model.TiposDivisao = TipoDivisaoComponenteService.BuscarTipoDivisaoComponenteSelect(model.SeqTipoComponenteCurricular);

            if (model.TiposDivisaoComponente == null)
            {
                model.TiposDivisaoComponente = new Framework.UI.Mvc.Html.SMCMasterDetailList<InstituicaoNivelTipoDivisaoComponenteViewModel>();
                model.TiposDivisaoComponente.Add(new InstituicaoNivelTipoDivisaoComponenteViewModel());
            }

            model.InstituicaoNivelExigeCredito = InstituicaoNivelService.BuscarInstituicaoNivel(model.SeqInstituicaoNivel).PermiteCreditoComponenteCurricular;

            //Chamada do configure após popular os valores do datasource
            this.ConfigureDynamic(model);

            // Cria as opções para view padrão
            SMCDefaultViewWizardOptions options = new SMCDefaultViewWizardOptions();
            options.Title = "InstituicaoNivelTipoComponenteCurricular";
            options.ActionSave = "Salvar";
            //options.Steps = this._dynamicOptions.Steps;

            this.SetViewMode(Framework.SMCViewMode.Insert);

            // Retorna o passo do wizard
            return SMCViewWizard(model, options);
        }

        [SMCAllowAnonymous]
        public short BuscarTipoGestaoDivisaoComponente(long seqTipoDivisaoComponente)
        {
            var tipoDivisao = TipoDivisaoComponenteService.BuscarTipoDivisaoComponente(seqTipoDivisaoComponente);
            return (short)tipoDivisao.TipoGestaoDivisaoComponente;
        }
    }
}