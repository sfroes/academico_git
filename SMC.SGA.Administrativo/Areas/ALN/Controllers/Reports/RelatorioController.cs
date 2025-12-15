using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ALN.Controllers
{
    public class RelatorioController : SMCControllerBase
    {
        #region [ Services ]

        internal IAlunoService AlunoService => Create<IAlunoService>();
        internal ICursoOfertaLocalidadeService CursoOfertaLocalidadeService => Create<ICursoOfertaLocalidadeService>();
        internal IEntidadeService EntidadeService => Create<IEntidadeService>();
        internal IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();
        internal IInstituicaoNivelService InstituicaoNivelService => Create<IInstituicaoNivelService>();
        internal ITipoRelatorioService TipoRelatorioService => Create<ITipoRelatorioService>();
        internal ITipoVinculoAlunoService TipoVinculoAlunoService => Create<ITipoVinculoAlunoService>();
        internal ISituacaoMatriculaService SituacaoMatriculaService => Create<ISituacaoMatriculaService>();
        internal IInstituicaoNivelTipoDocumentoAcademicoService InstituicaoNivelTipoDocumentoAcademicoService => Create<IInstituicaoNivelTipoDocumentoAcademicoService>();
        internal IInstituicaoNivelTipoDocumentoModeloRelatorioService InstituicaoNivelTipoDocumentoModeloRelatorioService => Create<IInstituicaoNivelTipoDocumentoModeloRelatorioService>();
        internal IHierarquiaEntidadeItemService HierarquiaEntidadeItemService => Create<IHierarquiaEntidadeItemService>();
        #endregion [ Services ]

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("AlunoReport");

        #endregion APIS

        [SMCAllowAnonymous]
        public ActionResult Index(RelatorioFiltroViewModel filtro)
        {
            filtro.TiposRelatorio = this.TipoRelatorioService.BuscarTiposRelatorioSelect().TransformList<SMCSelectListItem>();
            filtro.EntidadesResponsaveis = this.EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect();
            filtro.Localidades = this.CursoOfertaLocalidadeService.BuscarLocalidadesAtivasSelect();
            filtro.NiveisEnsino = this.InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();
            filtro.TiposVinculos = this.TipoVinculoAlunoService.BuscarTiposVinculoAlunoSelect();
            filtro.NiveisEnsinoPorGrupoDocumentoAcademico = this.InstituicaoNivelTipoDocumentoAcademicoService.BuscarNiveisEnsinoPorGrupoDocumentoAcademicoSelect(GrupoDocumentoAcademico.DeclaracoesGenericasAluno);
            filtro.ImprimirComponenteCurricularSemCreditos = false;
            filtro.ExibirMediaNotas = false;
            filtro.ExibeProfessor = true;
            filtro.ExibirEmentasComponentesCurriculares = false;

            return View(filtro);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarAlunosRelatorio(RelatorioFiltroViewModel filtro)
        {
            var lista = filtro.Transform<RelatorioListarViewModel>();
            var filtroAluno = filtro.Transform<RelatorioFiltroData>();
            filtroAluno.SeqsEntidadesResponsaveis = filtro.SeqEntidadesResponsaveis;

            var result = AlunoService.BuscarAlunosRelatorio(filtroAluno);

            lista.Alunos = new SMCPagerModel<RelatorioListarItemViewModel>(result.TransformList<RelatorioListarItemViewModel>());

            return PartialView("_DetailList", lista);
        }

        [SMCAllowAnonymous]
        public JsonResult BuscarSituacoesMatricula(long? seqNivelEnsino, long? seqTipoVinculoAluno, TipoRelatorio? tipoRelatorio)
        {
            /*3. Adicionar o filtro Situação de matrícula, entre os campos Ciclo letivo de ingresso e Turno, com a seguinte regra de navegação:
             * No select da Situação de matrícula: listar para seleção todas as situações de matrícula, cujos tipos são vínculos ativos,
             * parametrizadas por instituição-nível-vínculo, conforme os valores dos campos "Nível de Ensino" e "Vínculo",
             * ou caso estes campos não possuam valores, conforme as regras RN_USG_001 - Filtro por Instituição de Ensino
             * e RN_USG_004 - Filtro por Nível de Ensino.
             * Listar em ordem alfabética.*/
            List<SMCDatasourceItem> ret = SituacaoMatriculaService.BuscarSituacoesMatriculasDaInstiuicaoSelect(new SituacaoMatriculaFiltroData { SeqNivelEnsino = seqNivelEnsino, SeqTipoVinculoAluno = seqTipoVinculoAluno, VinculoAtivo = true });

            if (tipoRelatorio == TipoRelatorio.ListagemAssinatura)
            {
                var dataSourceItemFormado = SituacaoMatriculaService.BuscarSituacaoMatriculaItemSelectPorToken(TOKENS_SITUACAO_MATRICULA.FORMADO);

                ret.Add(dataSourceItemFormado);
                ret = ret.OrderBy(c => c.Descricao).ToList();
            }

            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [SMCAllowAnonymous]
        public FileContentResult ApresentarRelatorio()
        {
            return new FileContentResult(TempData["Relatorio"] as byte[], "application/pdf");
        }

        [SMCAllowAnonymous]
        public JsonResult GerarRelatorio(RelatorioListarViewModel model, List<long> gridAlunos)
        {
            model.NomeUsuarioLogado = SMCContext.User.SMCGetNome();
            model.SelectedValues = gridAlunos;

            if (model.SelectedValues == null || !model.SelectedValues.Any())
                throw new RelatorioSemSelecionarAlunoException();

            var param = new
            {
                model.Alunos,
                model.ExibeProfessor,
                model.ExibirCampoAssinatura,
                model.ExibirEmentasComponentesCurriculares,
                model.ExibirNaDeclaracao,
                model.ImprimirComponenteCurricularSemCreditos,
                model.ExibirMediaNotas,
                model.NomeUsuarioLogado,
                model.SelectedValues,
                model.SeqCicloLetivo,
                model.TipoRelatorio,
                model.TituloListagem,
                model.SeqNivelEnsinoPorGrupoDocumentoAcademico,
                model.SeqTipoDocumentoAcademico,
                model.IdiomaDocumentoAcademico,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq
            };

            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            TempData["Relatorio"] = dadosReport;

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [SMCAllowAnonymous]
        public ActionResult ConfigurarRelatorioDeclaracaoGenerica(RelatorioListarViewModel model, List<long> gridAlunos)
        {
            var modelo = new ConfigurarRelatorioDeclaracaoGenericaViewModel();
            try
            {
                if (model.TipoRelatorio == TipoRelatorio.DeclaracaoGenerica && gridAlunos != null && gridAlunos.Count > 0)
                {
                    if (gridAlunos.Count > 1)
                        throw new MaisDeUmAlunoSelecionadoException();
                }
                else
                {
                    throw new Exception(Views.Relatorio.App_LocalResources.UIResource.MSG_SelecioneUmAluno);
                }

                InstituicaoNivelTipoDocumentoAcademicoService.ValidarPermissaoConfigurarRelatorio(model.SeqNivelEnsinoPorGrupoDocumentoAcademico.Value, model.SeqTipoDocumentoAcademico.Value);

                var dados = AlunoService.ConfigurarRelatorioDeclaracaoGenerica(model.SeqNivelEnsinoPorGrupoDocumentoAcademico.Value, model.SeqTipoDocumentoAcademico.Value, model.IdiomaDocumentoAcademico.Value, gridAlunos.FirstOrDefault());

                var dadosTags = dados.Transform<ConfigurarRelatorioDeclaracaoGenericaViewModel>();

                dadosTags.Tags.SMCForEach(f => f.Descricao = f.DescricaoTag);

                dadosTags.SeqNivelEnsinoPorGrupoDocumentoAcademico = model.SeqNivelEnsinoPorGrupoDocumentoAcademico.Value;
                dadosTags.SeqTipoDocumentoAcademico = model.SeqTipoDocumentoAcademico.Value;
                dadosTags.IdiomaDocumentoAcademico = model.IdiomaDocumentoAcademico.Value;
                dadosTags.SeqAluno = gridAlunos.FirstOrDefault();
                dadosTags.TipoRelatorio = model.TipoRelatorio;
                modelo = dadosTags;

                return PartialView("_ConfigurarRelatorioDeclaracaoGenerica", modelo);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);

                return SMCRedirectToAction(nameof(Index), routeValues: new { tipoRelatorio = model.TipoRelatorio, seqNivelEnsinoPorGrupoDocumentoAcademico = model.SeqNivelEnsinoPorGrupoDocumentoAcademico, seqTipoDocumentoAcademico = model.SeqTipoDocumentoAcademico, idiomaDocumentoAcademico = model.IdiomaDocumentoAcademico });
            }
        }

        [SMCAllowAnonymous]
        public JsonResult EmitirRelatorioDeclaracaoGenerica(ConfigurarRelatorioDeclaracaoGenericaViewModel model)
        {
            model.Tags.SMCForEach(f => f.DescricaoTag = f.Descricao);
            var arquivo = AlunoService.BuscarRelatorioGenerico(model.Transform<ConfigurarRelatorioDeclaracaoGenericaData>());
            TempData["Relatorio"] = arquivo;

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [SMCAllowAnonymous]
        public ActionResult EnviarParaAssinatura(ConfigurarRelatorioDeclaracaoGenericaViewModel model)
        {
            var keySession = string.Format(KEY_DESABILITA_ENVIO_ASSINATURA._KEY_DESABILITA_ENVIO_ASSINATURA, model.SeqAluno, model.SeqTipoDocumentoAcademico);

            try
            {
                if (!Convert.ToBoolean(HttpContext.Session[keySession]))
                {
                    Session[keySession] = true;

                    model.Tags.SMCForEach(f => f.DescricaoTag = f.Descricao);

                    var tagExpiracaoDoc = model.Tags.Where(w => w.DescricaoTag == TagsDeclaracaoGenerica.EXPIRACAO_DOC).ToList();
                    if (tagExpiracaoDoc != null && tagExpiracaoDoc.Any())
                    {
                        if (!DateTime.TryParse(tagExpiracaoDoc.FirstOrDefault().Valor, out DateTime data))
                            throw new Exception(Views.Relatorio.App_LocalResources.UIResource.MSG_Formato_Data);
                    }

                    AlunoService.EnviarParaAssinatura(model.Transform<ConfigurarRelatorioDeclaracaoGenericaData>());
                    SetSuccessMessage(Views.Relatorio.App_LocalResources.UIResource.MSG_Envio_Documento_Com_Sucesso, target: SMCMessagePlaceholders.Centro);
                }
                else
                {
                    throw new Exception("Este documento já foi encaminhado para assinatura.");
                }
            }
            catch (Exception ex)
            {
                Session[keySession] = false;

                ex = ex.SMCLastInnerException();
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index), routeValues: new { tipoRelatorio = model.TipoRelatorio, seqNivelEnsinoPorGrupoDocumentoAcademico = model.SeqNivelEnsinoPorGrupoDocumentoAcademico, seqTipoDocumentoAcademico = model.SeqTipoDocumentoAcademico, idiomaDocumentoAcademico = model.IdiomaDocumentoAcademico });
        }

        [SMCAllowAnonymous]
        public FileContentResult GerarEApresentarRelatorio(RelatorioListarViewModel model, SMCEncryptedLong seqAluno)
        {
            GerarRelatorio(model, new List<long> { seqAluno });
            return new FileContentResult(TempData["Relatorio"] as byte[], "application/pdf");
        }

        [SMCAllowAnonymous]
        public JsonResult PreencherTiposDocumentoAcademico(long seqNivelEnsinoPorGrupoDocumentoAcademico)
        {
            var retorno = InstituicaoNivelTipoDocumentoAcademicoService.BuscarTiposDocumentoAcademicoPorNivelEnsinoSelect(seqNivelEnsinoPorGrupoDocumentoAcademico, GrupoDocumentoAcademico.DeclaracoesGenericasAluno);
            return Json(retorno);
        }

        [SMCAllowAnonymous]
        public JsonResult PreencherIdiomasDocumentoAcademico(long seqNivelEnsinoPorGrupoDocumentoAcademico, long seqTipoDocumentoAcademico)
        {
            var retorno = InstituicaoNivelTipoDocumentoModeloRelatorioService.BuscarIdiomasDocumentoAcademicoSelect(seqNivelEnsinoPorGrupoDocumentoAcademico, seqTipoDocumentoAcademico);
            return Json(retorno);
        }

        [SMCAllowAnonymous]
        public JsonResult PreencherNivelEnsinoSelecionado(long? seqNivelEnsinoPorGrupoDocumentoAcademico, TipoRelatorio tipoRelatorio)
        {
            var retorno = this.InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();

            if (TipoRelatorio.DeclaracaoGenerica == tipoRelatorio && seqNivelEnsinoPorGrupoDocumentoAcademico.HasValue)
                retorno.SMCForEach(f =>
                {
                    if (f.Seq == seqNivelEnsinoPorGrupoDocumentoAcademico)
                        f.Selected = true;
                });
            return Json(retorno);
        }

        [SMCAllowAnonymous]
        public JsonResult PreencherSeqsEntidadesResponsaveis(long? seqEntidadesResponsaveis)
        {
            var retorno = this.EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect();

            if (seqEntidadesResponsaveis.HasValue)
            {
                var seqsHierarquiaEntidadeItem = this.HierarquiaEntidadeItemService.BuscarHierarquiaEntidadesItemFilhasEntidadeVinculo(seqEntidadesResponsaveis.Value);
                var lista = new List<SMCDatasourceItem>();
                foreach (var seq in seqsHierarquiaEntidadeItem)
                {
                    var item = retorno.Where(w => w.Seq == seq).FirstOrDefault();
                    item.Selected = true;
                    lista.Add(item);
                }
                retorno = lista;
            }

            return Json(retorno);
        }
    }
}