using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.Domain.Models;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ORT.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.ORT.Views.TrabalhoAcademico.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.ORT.Controllers
{
    public class TrabalhoAcademicoController : SMCDynamicControllerBase
    {
        #region Serviços

        private IComponenteCurricularService ComponenteCurricularService
        {
            get { return this.Create<IComponenteCurricularService>(); }
        }

        private IInstituicaoNivelTipoVinculoAlunoService InstituicaoNivelTipoVinculoAlunoService
        {
            get { return this.Create<IInstituicaoNivelTipoVinculoAlunoService>(); }
        }

        private ITrabalhoAcademicoService TrabalhoAcademicoService
        {
            get { return this.Create<ITrabalhoAcademicoService>(); }
        }

        private ITipoTrabalhoService TipoTrabalhoService
        {
            get { return this.Create<ITipoTrabalhoService>(); }
        }

        private ITipoSituacaoMatriculaService TipoSituacaoMatriculaService
        {
            get { return this.Create<ITipoSituacaoMatriculaService>(); }
        }

        #endregion Serviços

        #region Constantes

        private const string INCLUSAO = "Inclusão";
        private const string ALTERAR = "Alteração";

        #endregion Constantes

        [SMCAuthorize(UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO)]
        public ActionResult HeaderEdit(TrabalhoAcademicoDynamicModel model)
        {
            return PartialView("_Header", model);
        }

        /// <summary>
        /// Step Informações do Trabalho
        /// </summary>
        /// <param name="model">Modelo</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO)]
        public ActionResult Passo1(TrabalhoAcademicoDynamicModel model)
        {
            SetViewModelPasso(model.Seq);

            this.ConfigureDynamic(model);

            model.SeqNivelEnsinoComparacao = model.SeqNivelEnsino;

            ValidaAlertaNivelEnsinoAlterado(model);

            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Step Aluno(s)
        /// </summary>
        /// <param name="model">Modelo</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO)]
        public ActionResult Passo2(TrabalhoAcademicoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            // Verifica se a data de depósito é igual a data atual ou maior/igual que a data referente ao sexto dia útil do mês corrente.
            if (model.GeraFinanceiroEntregaTrabalho)
            {
                if (!TrabalhoAcademicoService.ValidarDataMinimaDepositoSecretaria(model.DataDepositoSecretaria.Value))
                    throw new TrabalhoAcademicoDataInvalidaException();
            }
            else
            {
                model.DataDepositoSecretaria = null;
            }

            ///Caso haja alguma alteração no passo1 ele removerá limpará as listas
            if (model.SeqNivelEnsinoComparacao.HasValue && model.SeqNivelEnsinoComparacao != model.SeqNivelEnsino)
            {
                model.Autores = null;
                model.DivisoesComponente = null;
            }

            model.SeqNivelEnsinoComparacao = model.SeqNivelEnsino;
            model.SeqTipoTrabalhoComparacao = model.SeqTipoTrabalho;
            //model.SeqAlunoComparacao = model.SeqAluno;

            SetViewModelPasso(model.Seq);

            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Step Componente(s) Curricular(es)
        /// </summary>
        /// <param name="model">Modelo</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO)]
        public ActionResult Passo3(TrabalhoAcademicoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            ///Caso haja alguma alteração no passo1 ele removerá limpará as listas
            if (model.SeqAlunoComparacao.HasValue && model.SeqAlunoComparacao != model.SeqAluno)
            {
                model.DivisoesComponente = null;
            }

            //model.SeqNivelEnsinoComparacao = model.SeqNivelEnsino;
            //model.SeqTipoTrabalhoComparacao = model.SeqTipoTrabalho;
            model.SeqAlunoComparacao = model.SeqAluno;
            SetViewModelPasso(model.Seq);

            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Step Confirmação dos Dados de Trabalho Acadêmico
        /// </summary>
        /// <param name="model">Modelo</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO)]
        public ActionResult Passo4(TrabalhoAcademicoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            model.DescricaoNivelEnsino = BuscarDescricaoNivelEnsino(model);

            model.DescricaoTipoTrabalho = BuscarDescricaoTipoTrabalho(model);

            ///Dados do Componente Curricular para página de confirmação
            foreach (var item in model.DivisoesComponente)
            {
                item.DescricaoDivisaoComponente = BuscarDescricaoDivisaoComponente(model, item.SeqDivisaoComponente);
            }

            return SMCViewWizard(model, null);
        }

        [SMCAuthorize(UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO)]
        public JsonResult FormatarNome(long? seqAluno)
        {
            if (!seqAluno.HasValue) { return Json(""); }

            return Json(TrabalhoAcademicoService.FormatarNome(seqAluno.Value));
        }

        [SMCAuthorize(UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO)]
        public ActionResult BuscarTiposTrabalho(long seqInstituicaoEnsino, long? seqNivelEnsino)
        {
            var filtro = new InstituicaoNivelTipoTrabalhoFiltroData()
            {
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                SeqNivelEnsino = seqNivelEnsino,
                PermiteInclusaoManual = true
            };
            return Json(this.TipoTrabalhoService.BuscarTiposTrabalhoInstituicaoNivelEnsinoSelect(filtro));
        }

        [SMCAuthorize(UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO)]
        public JsonResult BuscarTiposSituacoesMatriculasTokenMatriculadoSelect(long seqCicloLetivo)
        {
            return Json(this.TipoSituacaoMatriculaService.BuscarTiposSituacoesMatriculasTokenMatriculadoSelect());
        }

        [SMCAuthorize(UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO)]
        public JsonResult PreencherDataDepositoSecretaria(long seqTipoTrabalho, bool GeraFinanceiroEntregaTrabalho, DateTime? DataDepositoSecretaria)
        {
            if (!GeraFinanceiroEntregaTrabalho)
                return Json(string.Empty);
            return Json(DataDepositoSecretaria);
        }


        [SMCAuthorize(UC_ORT_002_02_09.MANTER_AUTORIZACAO_NOVO_DEPOSITO)]
        public ActionResult IncluirSegundoDeposito(SMCEncryptedLong seqTrabalhoAcademico)
        {

            TrabalhoAcademicoSegundoDepositoViewModel model = new TrabalhoAcademicoSegundoDepositoViewModel();
            model.SeqTrabalhoAcademico = seqTrabalhoAcademico;

            return PartialView("_InserirSegundoDeposito", model);
        }

        [SMCAuthorize(UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO)]
        public ActionResult SalvarSegundoDeposito(TrabalhoAcademicoSegundoDepositoViewModel model)
        {
            TrabalhoAcademicoService.IncluirSegundoDeposito(model.SeqTrabalhoAcademico,
                model.JustificativaSegundoDeposito,
                model.DataAutorizacaoSegundoDeposito,
                model.ArquivoAnexadoSegundoDeposito.Transform<ArquivoAnexado>());

            SetSuccessMessage(UIResource.MSG_Sucesso_Inclusao_Segundo_Deposito, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Index", "TrabalhoAcademico", new { @area = "ORT", @seqTrabalhoAcademico = new SMCEncryptedLong(model.SeqTrabalhoAcademico)});
        }

        #region [ Métodos Privados ]

        private void ValidaAlertaNivelEnsinoAlterado(TrabalhoAcademicoDynamicModel model)
        {
            model.ExibirAlertaNivelEnsinoAlterado = (model.Seq == 0 && model.Autores?.Count > 0);
        }

        private void SetViewModelPasso(long seq)
        {
            ///Faz a verificação de inserção/edição pelo fato do wizard perder em qual viewmode ele se encontra de forma automatica
            if (seq == 0)
            {
                SMCHtmlHelperExtension.SetViewMode(this, SMCViewMode.Insert);
            }
            else
            {
                SMCHtmlHelperExtension.SetViewMode(this, SMCViewMode.Edit);
            }
        }

        private string BuscarDescricaoDivisaoComponente(TrabalhoAcademicoDynamicModel model, long seqDivisaoComponente)
        {
            var divisaoComponente = model.DivisoesComponentes.Where(w => w.Seq == seqDivisaoComponente).FirstOrDefault();

            return divisaoComponente?.Descricao;
        }

        private string BuscarDescricaoTipoTrabalho(TrabalhoAcademicoDynamicModel model)
        {
            var tipoTrabalho = model.TiposTrabalho.Where(w => w.Seq == model.SeqTipoTrabalho).FirstOrDefault();

            return tipoTrabalho?.Descricao;
        }

        private string BuscarDescricaoNivelEnsino(TrabalhoAcademicoDynamicModel model)
        {
            var nvlEnsino = model.NiveisEnsino.Where(w => w.Seq == model.SeqNivelEnsino).FirstOrDefault();

            return nvlEnsino?.Descricao;
        }

        [SMCAuthorize(UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO)]
        public ActionResult BuscarGeraFinanceiroEntregaTrabalho(long seqInstituicaoEnsino, long seqTipoTrabalho, long seqNivelEnsino)
        {
            if (seqInstituicaoEnsino == 0)
                return ThrowRedirect(new SMCApplicationException("Instituição de ensino logada não foi carregada corretamente."), "Index");

            var result = TipoTrabalhoService.BuscarGeraFinanceiroEntregaTrabalho(seqInstituicaoEnsino, seqTipoTrabalho, seqNivelEnsino).ToString();

            return Content(result);
        }

        #endregion [ Métodos Privados ]
    }
}