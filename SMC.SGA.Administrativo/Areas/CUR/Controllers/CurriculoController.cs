using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CSO.Models;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using SMC.SGA.Administrativo.Areas.CUR.Views.Curriculo.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class CurriculoController : SMCDynamicControllerBase
    {
        #region [ Serviços ]

        private ICurriculoService CurriculoService
        {
            get { return this.Create<ICurriculoService>(); }
        }

        private IEntidadeService EntidadeService
        {
            get { return this.Create<IEntidadeService>(); }
        }

        #endregion [ Serviços ]

        public ActionResult CabecalhoCurriculo(CurriculoDynamicModel model)
        {
            var headerModel = new CursoCabecalhoViewModel();
            if (!model.IsNew())
            {
                headerModel = ExecuteService<EntidadeCabecalhoData, CursoCabecalhoViewModel>(EntidadeService.BuscarEntidadeCabecalho, model.SeqCursoParametro);
                headerModel.Exibir = true;
            }
            return PartialView("_Cabecalho", headerModel);
        }

        [SMCAuthorize(UC_CUR_001_01_02.MANTER_CURRICULO)]
        public ActionResult Step1(CurriculoDynamicModel model)
        {
            try
            {
                this.ConfigureDynamic(model);

                // Recupera configuração do Nível de Ensino no Curso na Instituição atual
                var configuracao = this.CurriculoService.BuscarConfiguracoesCurriculo(model.SeqCurso.Seq.Value);
                new SMCMapper().Inject(model, configuracao);

                //Se o curso não tiver ofertas vinculadas exibir mensagem de erro
                if (model.CursosOfertaDataSource.Count == 0)
                    throw new Exception(UIResource.MSG_CurriculoCursoSemOferta);

                // Monta o cabeçalho do curriculo com o curso
                var header = this.EntidadeService.BuscarEntidadeCabecalho(model.SeqCurso.Seq.Value);
                model.NomeCurso = header.Nome;
                model.SiglaCurso = header.Sigla;

                if (string.IsNullOrEmpty(model.SiglaCurso))
                {
                    throw new CurriculoSiglaCursoObrigatoriaException();
                }

                // Caso apenas a configuraçõe tenha ofertas atualiza o modelo
                if (model.CursosOferta.SMCCount() == 0 && configuracao.CursosOferta.SMCCount() > 0)
                    model.CursosOferta = configuracao.CursosOferta.TransformMasterDetailList<CurriculoCursoOfertaViewModel>();

                // Atualiza o código e descrição do currículo
                model.Codigo = $"{model.SiglaCurso.Trim()}00";
                model.Descricao = $"{UIResource.Label_Curriculo} {model.NomeCurso}";

                // Cria as opções para view padrão
                var options = new SMCDefaultViewWizardOptions();
                options.Title = "Curriculo";
                options.ActionSave = "Salvar";

                // Retorna o passo do wizard
                return SMCViewWizard(model, options);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                throw ex;
            }
        }

        [SMCAuthorize(UC_CUR_001_01_02.MANTER_CURRICULO)]
        public ActionResult CalcularQuantidadeHoraRelogioObrigatoria(short quantidadeHoraAulaObrigatoria)
        {
            return CalcularHorasRelogio(quantidadeHoraAulaObrigatoria);
        }

        [SMCAuthorize(UC_CUR_001_01_02.MANTER_CURRICULO)]
        public ActionResult CalcularQuantidadeHoraRelogioOptativa(short quantidadeHoraAulaOptativa)
        {
            return CalcularHorasRelogio(quantidadeHoraAulaOptativa);
        }

        [SMCAuthorize(UC_CUR_001_01_01.PESQUISAR_CURRICULO)]
        public JsonResult BuscarCurriculoPorCursoSelectLookup(CursoLookupViewModel seqCurso)
        {
            List<SMCDatasourceItem> listItens = CurriculoService.BuscarCurriculoPorCursoSelect(seqCurso.Seq.Value);

            return Json(listItens);
        }

        [SMCAuthorize(UC_CUR_001_01_01.PESQUISAR_CURRICULO)]
        public ActionResult ValidarQuantidadeHoraAulaObrigatoria(short? quantidadeCreditoObrigatorio = null)
        {
            return Content((!quantidadeCreditoObrigatorio.HasValue).ToString().ToLower());
        }

        [SMCAuthorize(UC_CUR_001_01_01.PESQUISAR_CURRICULO)]
        public ActionResult ValidarQuantidadeCreditoObrigatorio(short? quantidadeHoraAulaObrigatoria = null)
        {
            return Content((!quantidadeHoraAulaObrigatoria.HasValue).ToString().ToLower());
        }

        [NonAction]
        public ActionResult CalcularHorasAula(short quantidadeHoraRelogio = 0)
        {
            short horasAula = (short)(quantidadeHoraRelogio * 60F / 50F);
            return Json(horasAula);
        }

        [NonAction]
        public ActionResult CalcularHorasRelogio(short quantidadeHoraAula = 0)
        {
            short horasRelogio = (short)(quantidadeHoraAula * 50F / 60F);
            return Json(horasRelogio);
        }
    }
}