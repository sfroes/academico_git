using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CSO.Models;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.CSO.Views.CursoFormacaoEspecifica.App_LocalResources;
using System;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.Extensions;
using SMC.Academico.ServiceContract.Areas.CNC.Data;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class CursoFormacaoEspecificaController : SMCDynamicControllerBase
    {
        #region Serviços

        private ICursoService CursoService
        {
            get { return this.Create<ICursoService>(); }
        }

        private ICursoFormacaoEspecificaService CursoFormacaoEspecificaService
        {
            get { return this.Create<ICursoFormacaoEspecificaService>(); }
        }

        private ITipoFormacaoEspecificaService TipoFormacaoEspecificaService
        {
            get { return this.Create<ITipoFormacaoEspecificaService>(); }
        }

        private IFormacaoEspecificaService FormacaoEspecificaService
        {
            get { return this.Create<IFormacaoEspecificaService>(); }
        }

        private ICursoOfertaLocalidadeService CursoOfertaLocalidadeService
        {
            get { return this.Create<ICursoOfertaLocalidadeService>(); }
        }

        private ITitulacaoService TitulacaoService => Create<ITitulacaoService>();

        #endregion Serviços

        public ActionResult CabecalhoCursoFormacaoEspecifica(CursoFormacaoEspecificaDynamicModel model)
        {
            // Busca as informações do curso para o cabeçalho
            CursoFormacaoEspecificaCabecalhoViewModel modelHeader = ExecuteService<CursoData, CursoFormacaoEspecificaCabecalhoViewModel>(CursoService.BuscarCurso, model.SeqCurso);

            return PartialView("_Cabecalho", modelHeader);
        }

        public ActionResult MensagemCursoFormacaoEspecifica()
        {
            return PartialView("_MensagemTreeView");
        }

        [SMCAuthorize(UC_CSO_001_01_04.PESQUISAR_FORMACAO_ESPECIFICA_CURSO)]
        public ActionResult TipoFormacaoEspecificaRequired(long? SeqTipoFormacaoEspecifica, long? seqFormacaoEspecifica)
        {
            bool required = false;

            if (seqFormacaoEspecifica.HasValue && seqFormacaoEspecifica != 0)
            {
                required = this.FormacaoEspecificaService.FormacaoEspefificaExigeGrau(seqFormacaoEspecifica.Value);
            }
            // Caso tenha definido o sequencial do tipo de formação, busca a informação de exigência de grau acadêmico ou não
            else if (SeqTipoFormacaoEspecifica.HasValue)
            {
                var tipoFormacao = TipoFormacaoEspecificaService.BuscarTipoFormacaoEspecifica(SeqTipoFormacaoEspecifica.Value);
                required = tipoFormacao?.ExigeGrau ?? false;
            }

            return new ContentResult { Content = required.ToString() };
        }

        [SMCAuthorize(UC_CSO_001_01_05.MANTER_FORMACAO_ESPECIFICA_CURSO)]
        public ActionResult TipoFormacaoEspecificaExigeTitulacao(long? SeqTipoFormacaoEspecifica, long? seqFormacaoEspecifica)
        {
            bool required = false;

            if (seqFormacaoEspecifica.HasValue && seqFormacaoEspecifica != 0)
                required = this.FormacaoEspecificaService.FormacaoEspefificaExibeTitulacao(seqFormacaoEspecifica.Value);
            else if (SeqTipoFormacaoEspecifica.HasValue)
            {
                var tipoFormacao = TipoFormacaoEspecificaService.BuscarTipoFormacaoEspecifica(SeqTipoFormacaoEspecifica.Value);
                required = tipoFormacao?.PermiteTitulacao ?? false;
            }

            return new ContentResult { Content = required.ToString() };
        }

        /// <summary>
        /// Recupera a descrição da formação específica conforme a regra RN_CSO_024 - Mascara - Formação Específica para cursos com cadastro simples
        /// </summary>
        /// <param name="seqTipoFormacaoEspecifica">Sequencial do tipo da formação específica</param>
        /// <param name="seqGrauAcademico">Sequencial do grau acadêmico</param>
        /// <returns>Descrição da formação específica segundo a regra RN_CSO_024</returns>
        [SMCAuthorize(UC_CSO_001_01_05.MANTER_FORMACAO_ESPECIFICA_CURSO)]
        public ActionResult BuscarDescricaoFormacaoEspecificaCadastroSimples(long? seqTipoFormacaoEspecifica, long? seqGrauAcademico)
        {
            return Json(this.CursoFormacaoEspecificaService.BuscarDescricaoFormacaoEspecifica(seqTipoFormacaoEspecifica, seqGrauAcademico));
        }

        [SMCAuthorize(UC_CSO_001_01_06.REPLICAR_FORMACAO_ESPECIFICA_CURSO)]
        public ActionResult ReplicarCursoFormacaoEspecifica(SMCEncryptedLong seqCurso, SMCEncryptedLong seqFormacaoEspecifica)
        {
            var formacaoEspecifica = this.FormacaoEspecificaService.BuscarFormacaoEspecifica(seqFormacaoEspecifica);

            var model = new ReplicarCursoFormacaoEspecificaViewModel()
            {
                SeqCurso = seqCurso,
                SeqFormacaoEspecifica = seqFormacaoEspecifica,
                CursosOfertasLocalidades = this.CursoOfertaLocalidadeService.BuscarCursoOfertasLocalidadeReplicarCursoFormacaoEspecificaSelect(seqCurso, seqFormacaoEspecifica),
                Mensagem = string.Format(UIResource.Mensagem_Informativa, formacaoEspecifica.DescricaoTipoFormacaoEspecifica, formacaoEspecifica.Descricao)
            };

            return PartialView("_ReplicarCursoFormacaoEspecifica", model);
        }

        [SMCAuthorize(UC_CSO_001_01_06.REPLICAR_FORMACAO_ESPECIFICA_CURSO)]
        public ActionResult SalvarReplicarCursoFormacaoEspecifica(ReplicarCursoFormacaoEspecificaViewModel model)
        {
            try
            {
                CursoFormacaoEspecificaService.SalvarReplicarCursoFormacaoEspecifica(model.Transform<ReplicarCursoFormacaoEspecificaData>());

                SetSuccessMessage(UIResource.MensagemSucessoReplicarFormacaoEspecificaCurso, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message);
            }

            return SMCRedirectToAction("Index", "CursoFormacaoEspecifica", routeValues: new { SeqCurso = new SMCEncryptedLong(model.SeqCurso) });
        }

        [SMCAuthorize(UC_CSO_001_01_05.MANTER_FORMACAO_ESPECIFICA_CURSO)]
        public ActionResult BuscarTitulacoesSelect(long? seqGrauAcademico, long? seqCurso, CursoTipoFormacao? cursoTipoFormacao, long? seqCursoOferta, long? seqCursoFormacaoEspecifica)
        {
            var filtro = new TitulacaoFiltroData()
            {
                Ativo = true,
                SeqGrauAcademico = seqGrauAcademico,
                SeqCurso = seqCurso,
                CursoTipoFormacao = cursoTipoFormacao,
                SeqCursoOuGrauAcademicoCurso = true,
                SeqCursoOferta = seqCursoOferta,
                SeqCursoFormacaoEspecifica = seqCursoFormacaoEspecifica
            };
            return Json(this.TitulacaoService.BuscarTitulacoesSelect(filtro));
        }
    }
}