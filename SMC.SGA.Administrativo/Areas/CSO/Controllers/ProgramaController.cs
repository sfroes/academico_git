using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CSO.Models;
using SMC.SGA.Administrativo.Areas.CSO.Views.Programa.App_LocalResources;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class ProgramaController : SMCDynamicControllerBase
    {
        #region [ Service ]

        private ISituacaoEntidadeService SituacaoEntidadeService => Create<ISituacaoEntidadeService>();
        private IFormacaoEspecificaService FormacaoEspecificaService => Create<IFormacaoEspecificaService>();
        private ICursoService CursoService => Create<ICursoService>();
        private ICursoOfertaLocalidadeService CursoOfertaLocalidadeService => Create<ICursoOfertaLocalidadeService>();
        private IProgramaService ProgramaService => Create<IProgramaService>();
        private ITitulacaoService TitulacaoService => Create<ITitulacaoService>();
        private INivelEnsinoService NivelEnsinoService => Create<INivelEnsinoService>();
        private IGrauAcademicoService GrauAcademicoService => Create<IGrauAcademicoService>();

        #endregion [ Service ]

        [SMCAuthorize(UC_ORG_001_10_01.MANTER_SITUACAO_ENTIDADE)]
        public ActionResult PreencherCodigoCapesObrigatorio(long seqSituacaoAtual)
        {
            var situacaoEntidade = this.SituacaoEntidadeService.BuscarSituacaoEntidade(seqSituacaoAtual);

            return Json(!(situacaoEntidade.CategoriaAtividade == CategoriaAtividade.EmDesativacao
                       || situacaoEntidade.CategoriaAtividade == CategoriaAtividade.Inativa));
        }


        [SMCAuthorize(UC_CSO_002_01_07.REPLICAR_FORMACAO_ESPECIFICA_PROGRAMA)]
        public ActionResult ReplicarFormacaoEspecificaPrograma(SMCEncryptedLong seqFormacaoEspecifica, SMCEncryptedLong seqEntidadeResponsavel)
        {
            var model = new ReplicaFormacaoEspecificaProgramaViewModel() { SeqFormacaoEspecifica = seqFormacaoEspecifica, SeqEntidadeResponsavel = seqEntidadeResponsavel };
            return View(model);
        }

        [SMCAuthorize(UC_CSO_002_01_07.REPLICAR_FORMACAO_ESPECIFICA_PROGRAMA)]
        public ActionResult ReplicarFormacaoEspecificaProgramaStepCurso(ReplicaFormacaoEspecificaProgramaViewModel model)
        {
            this.SetViewMode(SMCViewMode.Insert);

            model.Cursos = this.CursoService.BuscarCursosReplicarFormacaoEspecificaProgramaSelect(model.Transform<CursoReplicaFormacaoEspecificaProgramaFiltroData>());

            PreencherMensagemInformativa(model);

            return PartialView("_WizardStepCurso", model);
        }

        [SMCAuthorize(UC_CSO_002_01_07.REPLICAR_FORMACAO_ESPECIFICA_PROGRAMA)]
        public ActionResult ReplicarFormacaoEspecificaProgramaStepTitulacao(ReplicaFormacaoEspecificaProgramaViewModel model)
        {
            if (!this.FormacaoEspecificaService.FormacaoEspefificaExibeTitulacao(model.SeqFormacaoEspecifica))
            {
                model.Step++;
                return ReplicarFormacaoEspecificaProgramaStepCursoOfertaLocalidade(model);
            }

            this.SetViewMode(SMCViewMode.Insert);

            var seqsCursosTitulacoes = model.CursosTitulacoes?.Select(s => s.SeqCurso).ToArray() ?? new long[0];
            if (!model.SeqsCursos.SequenceEqual(seqsCursosTitulacoes))
            {
                model.CursosTitulacoes = new SMCMasterDetailList<ReplicaFormacaoEspecificaProgramaTitulacaoCursoViewModel>();
                foreach (var seqCurso in model.SeqsCursos)
                {
                    var curso = model.Cursos.Single(s => s.Seq == seqCurso);
                    var listaGrauAcademico = GrauAcademicoService.BuscarGrauAcademicoPorNivelEnsinoCurso(seqCurso);
                    model.CursosTitulacoes.Add(new ReplicaFormacaoEspecificaProgramaTitulacaoCursoViewModel()
                    {
                        SeqCurso = curso.Seq,
                        DescricaoCurso = curso.Descricao,
                        Titulacoes = new SMCMasterDetailList<ReplicaFormacaoEspecificaProgramaTitulacaoViewModel>()
                        {
                            DefaultModel = new ReplicaFormacaoEspecificaProgramaTitulacaoViewModel()
                            {
                                SeqCurso = seqCurso,
                                ExigeGrau = FormacaoEspecificaService.FormacaoEspefificaExigeGrau(model.SeqFormacaoEspecifica),
                                DataSourceGrauAcademico = listaGrauAcademico.TransformList<SMCSelectListItem>(),
                                DataSourceTitulacoes = TitulacaoService
                                    .BuscarTitulacoesSelect(new TitulacaoFiltroData() { SeqCurso = seqCurso, SeqCursoOuGrauAcademicoCurso = true, ListaGrauAcademico = listaGrauAcademico.Select(s => s.Seq).ToList() })
                                    .TransformList<SMCSelectListItem>(),
                                Ativo = true
                            }
                        }
                    });
                }
            }

            PreencherMensagemInformativa(model);

            return PartialView("_WizardStepTitulacao", model);
        }

        [SMCAuthorize(UC_CSO_002_01_07.REPLICAR_FORMACAO_ESPECIFICA_PROGRAMA)]
        public ActionResult ReplicarFormacaoEspecificaProgramaStepCursoOfertaLocalidade(ReplicaFormacaoEspecificaProgramaViewModel model)
        {
            this.SetViewMode(SMCViewMode.Insert);

            model.CursosOfertasLocalidades = this.CursoOfertaLocalidadeService.BuscarCursosOfertasLocalidadesReplicarFormacaoEspecificaProgramaSelect(model.Transform<CursoOfertaLocalidadeReplicaFormacaoEspecificaProgramaFiltroData>());

            PreencherMensagemInformativa(model);

            return PartialView("_WizardStepCursoOfertaLocalidade", model);
        }

        [SMCAuthorize(UC_CSO_002_01_07.REPLICAR_FORMACAO_ESPECIFICA_PROGRAMA)]
        public ActionResult ReplicarFormacaoEspecificaProgramaStepConfirmacao(ReplicaFormacaoEspecificaProgramaViewModel model)
        {
            this.SetViewMode(SMCViewMode.Insert);

            var itensSelecionados = this.ProgramaService.BuscarItensSelecionadosReplicarFormacaoEspecificaPrograma(model.Transform<ReplicaFormacaoEspecificaProgramaData>()).TransformList<ReplicaFormacaoEspecificaProgramaConfirmacaoViewModel>();

            model.ItensSelecionados = SMCTreeView.For(itensSelecionados);

            PreencherMensagemInformativa(model);

            return PartialView("_WizardStepConfirmacao", model);
        }

        [SMCAuthorize(UC_CSO_002_01_07.REPLICAR_FORMACAO_ESPECIFICA_PROGRAMA)]
        public ActionResult SalvarReplicarFormacaoEspecificaPrograma(ReplicaFormacaoEspecificaProgramaViewModel model)
        {
            try
            {
                ProgramaService.SalvarReplicarFormacaoEspecificaPrograma(model.Transform<ReplicaFormacaoEspecificaProgramaData>());

                SetSuccessMessage(UIResource.MensagemSucessoReplicarFormacaoEspecificaPrograma, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message);
            }

            return SMCRedirectToAction("Index", "FormacaoEspecifica", new { SeqEntidadeResponsavel = new SMCEncryptedLong(model.SeqEntidadeResponsavel) });
        }

        [SMCAuthorize(UC_CSO_002_01_07.REPLICAR_FORMACAO_ESPECIFICA_PROGRAMA)]
        public ActionResult BuscarTitulacoesCurso(SMCEncryptedLong seqCurso)
        {
            var result = TitulacaoService
                .BuscarTitulacoesSelect(new TitulacaoFiltroData() { SeqCurso = seqCurso, SeqCursoOuGrauAcademicoCurso = true })
                .TransformList<SMCSelectListItem>();
            return Json(result);
        }

        private void PreencherMensagemInformativa(ReplicaFormacaoEspecificaProgramaViewModel model)
        {
            var formacaoEspecifica = this.FormacaoEspecificaService.BuscarFormacaoEspecifica(model.SeqFormacaoEspecifica);
            var descricaoFormacao = $"[{formacaoEspecifica.DescricaoTipoFormacaoEspecifica}] {formacaoEspecifica.Descricao}";

            switch (model.Step)
            {
                case 0: //Curso
                    model.MensagemInformativa = string.Format(UIResource.MSG_MensagemInformativaCurso, descricaoFormacao);
                    break;
                case 1: //Titulação
                    model.MensagemInformativa = string.Format(UIResource.MSG_MensagemInformativaTitulacao, descricaoFormacao);
                    break;
                case 2: //Curso oferta localidade
                    model.MensagemInformativa = string.Format(UIResource.MSG_MensagemInformativaCursoOfertaLocalidade, descricaoFormacao);
                    break;
                case 3: //Confirmação
                    model.MensagemInformativa = string.Format(UIResource.MSG_MensagemInformativaConfirmacao, descricaoFormacao);
                    break;
                default:
                    break;
            }

        }
    }
}