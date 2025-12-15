using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CAM.Models;
using SMC.SGA.Administrativo.Areas.CAM.Views.CampanhaCopia.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class CampanhaCopiaController : SMCControllerBase
    {
        #region Services

        private ICampanhaService CampanhaService => Create<ICampanhaService>();
        private IProcessoSeletivoService ProcessoSeletivoService => Create<IProcessoSeletivoService>();

        #endregion Services

        [SMCAuthorize(UC_CAM_001_01_06.COPIAR_CAMPANHA)]
        public ActionResult CopiarCampanha(SMCEncryptedLong seqCampanha)
        {
            var model = new CampanhaCopiaViewModel() { SeqCampanhaOrigem = seqCampanha };

            return View(model);
        }

        [SMCAuthorize(UC_CAM_001_01_06.COPIAR_CAMPANHA)]
        public ActionResult WizardStepCampanha(CampanhaCopiaViewModel model)
        {
            this.SetViewMode(SMCViewMode.Insert);

            var campanhaOrigem = this.CampanhaService.BuscarCampanhaOrigem(model.SeqCampanhaOrigem);

            model.SeqCampanhaOrigem = campanhaOrigem.SeqCampanhaOrigem;
            model.DescricaoCampanhaOrigem = campanhaOrigem.DescricaoCampanhaOrigem;
            model.CiclosLetivosCampanhaOrigem = campanhaOrigem.CiclosLetivosCampanhaOrigem;

            return PartialView("_WizardStepCampanha", model);
        }

        [SMCAuthorize(UC_CAM_001_01_06.COPIAR_CAMPANHA)]
        public ActionResult WizardStepOferta(CampanhaCopiaViewModel model)
        {
            this.SetViewMode(SMCViewMode.Insert);

            ValidarCampanhaCopiaCampanha(model);

            var filtro = new CampanhaCopiaOfertaFiltroViewModel() { SeqCampanhaOrigem = model.SeqCampanhaOrigem, DesconsiderarTokensTipoOferta = new List<string>() { TOKEN_TIPO_OFERTA.TURMA } };
            var data = CampanhaService.BuscarCampanhaOfertas(filtro.Transform<CampanhaCopiaOfertaFiltroData>());
            model.CampanhaOfertas = new SMCPagerModel<CampanhaCopiaOfertaListaViewModel>(data.Transform<SMCPagerData<CampanhaCopiaOfertaListaViewModel>>(), filtro.PageSettings, filtro);

            model.CampanhaOfertas.SelectedValues = model.GridCampanhaOferta;

            return PartialView("_WizardStepOferta", model);
        }

        [SMCAuthorize(UC_CAM_001_01_06.COPIAR_CAMPANHA)]
        public ActionResult WizardStepProcessoSeletivo(CampanhaCopiaViewModel model)
        {
            this.SetViewMode(SMCViewMode.Insert);

            //Se alguma campanha oferta foi selecionada, realiza as validações
            if (model.GridCampanhaOferta != null && model.GridCampanhaOferta.Any())
            {
                ValidarOfertaCopiaCampanha(model);
            }

            var filtro = new CampanhaCopiaProcessoSeletivoFiltroViewModel() { SeqCampanhaOrigem = model.SeqCampanhaOrigem };

            var data = CampanhaService.BuscarProcessosSeletivos(filtro.Transform<CampanhaCopiaProcessoSeletivoFiltroData>());

            model.ProcessosSeletivos = data.TransformList<CampanhaCopiaProcessoSeletivoListaViewModel>();

            return PartialView("_WizardStepProcessoSeletivo", model);
        }

        [SMCAuthorize(UC_CAM_001_01_06.COPIAR_CAMPANHA)]
        public ActionResult WizardStepProcessoGPI(CampanhaCopiaViewModel model)
        {
            this.SetViewMode(SMCViewMode.Insert);

            //Se não foram selecionados processos seletivos
            if (model.GridProcessoSeletivo == null || !model.GridProcessoSeletivo.Any())
            {
                model.Step = 5;
                return WizardStepConfirmacao(model);
            }

            //Se algum processo seletivo foi selecionado, realiza as validações
            if (model.GridProcessoSeletivo != null && model.GridProcessoSeletivo.Any())
            {
                ValidarProcessoSeletivoCopiaCampanha(model);
            }

            model.CiclosLetivos = model.CiclosLetivosCampanhaDestino.Select(c => new SMCDatasourceItem()
            {
                Seq = c.SeqCicloLetivo.Seq.Value,
                Descricao = c.SeqCicloLetivo.Descricao
            }).ToList();

            var seqsProcessosSeletivosSelecionados = model.GridProcessoSeletivo.Select(p => Convert.ToInt64(p)).ToArray();

            model.ProcessosSeletivos = model.ProcessosSeletivos.Where(p => seqsProcessosSeletivosSelecionados.Contains(p.Seq)).ToList();

            var etapasProcessoGPI = ProcessoSeletivoService.BuscarEtapasProcessosGPI(seqsProcessosSeletivosSelecionados);

            foreach (var seqProcessoSeletivoSelecionado in seqsProcessosSeletivosSelecionados)
            {
                var processoSeletivoSelecionado = model.ProcessosSeletivos.FirstOrDefault(p => p.Seq == seqProcessoSeletivoSelecionado);

                if (string.IsNullOrEmpty(processoSeletivoSelecionado.DescricaoProcessoGPI))
                    processoSeletivoSelecionado.DescricaoProcessoGPI = processoSeletivoSelecionado.Descricao;

                if (processoSeletivoSelecionado.CopiarProcessoGPI == null)
                    processoSeletivoSelecionado.CopiarProcessoGPI = true;

                if (processoSeletivoSelecionado.CopiarNotificacoesGPI == null)
                    processoSeletivoSelecionado.CopiarNotificacoesGPI = true;

                if (processoSeletivoSelecionado.EtapasGPI == null || processoSeletivoSelecionado.EtapasGPI.Count == 0)
                    processoSeletivoSelecionado.EtapasGPI = etapasProcessoGPI.FirstOrDefault(p => p.SeqProcessoSeletivo == seqProcessoSeletivoSelecionado).EtapasGPI.TransformMasterDetailList<CampanhaCopiaEtapaProcessoGPIItemViewModel>();
            }

            return PartialView("_WizardStepProcessoGPI", model);
        }

        [SMCAuthorize(UC_CAM_001_01_06.COPIAR_CAMPANHA)]
        public ActionResult WizardStepConvocacao(CampanhaCopiaViewModel model)
        {
            this.SetViewMode(SMCViewMode.Insert);

            ValidarProcessoGPICopiaCampanha(model);

            model.CiclosLetivos = model.CiclosLetivosCampanhaDestino.Select(c => new SMCDatasourceItem()
            {
                Seq = c.SeqCicloLetivo.Seq.Value,
                Descricao = c.SeqCicloLetivo.Descricao
            }).ToList();

            var seqsProcessosSeletivosSelecionados = model.GridProcessoSeletivo.Select(p => Convert.ToInt64(p)).ToArray();

            model.ProcessosSeletivos = model.ProcessosSeletivos.Where(p => seqsProcessosSeletivosSelecionados.Contains(p.Seq)).ToList();

            var convocacoesProcessoSeletivo = ProcessoSeletivoService.BuscarConvocacoesProcessosSeletivos(seqsProcessosSeletivosSelecionados);

            foreach (var seqProcessoSeletivoSelecionado in seqsProcessosSeletivosSelecionados)
            {
                var processoSeletivoSelecionado = model.ProcessosSeletivos.FirstOrDefault(p => p.Seq == seqProcessoSeletivoSelecionado);

                if (processoSeletivoSelecionado.Convocacoes == null || processoSeletivoSelecionado.Convocacoes.Count == 0)
                    processoSeletivoSelecionado.Convocacoes = convocacoesProcessoSeletivo.FirstOrDefault(p => p.SeqProcessoSeletivo == seqProcessoSeletivoSelecionado).Convocacoes.TransformMasterDetailList<CampanhaCopiaConvocacaoProcessoSeletivoItemViewModel>();
            }

            return PartialView("_WizardStepConvocacao", model);
        }

        [SMCAuthorize(UC_CAM_001_01_06.COPIAR_CAMPANHA)]
        public ActionResult WizardStepConfirmacao(CampanhaCopiaViewModel model)
        {
            this.SetViewMode(SMCViewMode.ReadOnly);

            model.CiclosLetivos = model.CiclosLetivosCampanhaDestino.Select(c => new SMCDatasourceItem()
            {
                Seq = c.SeqCicloLetivo.Seq.Value,
                Descricao = c.SeqCicloLetivo.Descricao
            }).ToList();

            //Se selecionada alguma campanha oferta
            if (model.GridCampanhaOferta != null && model.GridCampanhaOferta.Any())
            {
                var campanhasSelecionadas = model.CampanhaOfertas.Where(c => model.GridCampanhaOferta.Select(g => Convert.ToInt64(g)).Contains(c.Seq)).ToList();

                model.CampanhaOfertas = new SMCPagerModel<CampanhaCopiaOfertaListaViewModel>(campanhasSelecionadas, model.CampanhaOfertas.PageSettings, model.CampanhaOfertas.Filters);
            }
            else
                model.CampanhaOfertas = new SMCPagerModel<CampanhaCopiaOfertaListaViewModel>();

            //Se selecionado algum processo seletivo
            if (model.GridProcessoSeletivo != null && model.GridProcessoSeletivo.Any())
            {
                var seqsProcessosSeletivosSelecionados = model.GridProcessoSeletivo.Select(p => Convert.ToInt64(p)).ToArray();

                model.ProcessosSeletivos = model.ProcessosSeletivos.Where(p => seqsProcessosSeletivosSelecionados.Contains(p.Seq)).ToList();

                foreach (var seqProcessoSeletivoSelecionado in seqsProcessosSeletivosSelecionados)
                {
                    var processoSeletivoSelecionado = model.ProcessosSeletivos.FirstOrDefault(p => p.Seq == seqProcessoSeletivoSelecionado);

                    var convocacoesSelecionadas = processoSeletivoSelecionado.Convocacoes.Where(c => c.Checked).TransformMasterDetailList<CampanhaCopiaConvocacaoProcessoSeletivoItemViewModel>();

                    foreach (var convocacao in convocacoesSelecionadas)
                    {
                        convocacao.DescricaoCicloLetivo = model.CiclosLetivos.FirstOrDefault(c => c.Seq == convocacao.SeqCicloLetivo).Descricao;
                    }

                    processoSeletivoSelecionado.Convocacoes = convocacoesSelecionadas;
                }
            }
            else
            {
                model.ProcessosSeletivos = new List<CampanhaCopiaProcessoSeletivoListaViewModel>();
            }

            return PartialView("_WizardStepConfirmacao", model);
        }

        [SMCAuthorize(UC_CAM_001_01_06.COPIAR_CAMPANHA)]
        public ActionResult SalvarCopiaCampanha(CampanhaCopiaViewModel model)
        {
            try
            {
                CampanhaService.SalvarCopiaCampanha(model.Transform<CampanhaCopiaData>());

                SetSuccessMessage(UIResource.MSG_CopiaCampanha_Sucesso, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Index", "Campanha");
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, UIResource.MSG_Titulo_Erro, SMCMessagePlaceholders.Centro);
                return PartialView("_WizardStepConfirmacao", model);
            }
        }

        [SMCAllowAnonymous]
        private void ValidarCampanhaCopiaCampanha(CampanhaCopiaViewModel model)
        {
            this.CampanhaService.ValidarCampanhaCopiaCampanha(model.SeqCampanhaOrigem, model.DescricaoCampanhaDestino, model.CiclosLetivosCampanhaDestino.Select(c => c.SeqCicloLetivo.Seq.GetValueOrDefault()).ToList());
        }

        [SMCAllowAnonymous]
        private void ValidarOfertaCopiaCampanha(CampanhaCopiaViewModel model)
        {
            this.CampanhaService.ValidarOfertaCopiaCampanha(model.SeqCampanhaOrigem, model.CiclosLetivosCampanhaDestino.Select(c => c.SeqCicloLetivo.Seq.GetValueOrDefault()).ToList(), model.GridCampanhaOferta.Select(o => Convert.ToInt64(o)).ToList());
        }

        [SMCAllowAnonymous]
        private void ValidarProcessoSeletivoCopiaCampanha(CampanhaCopiaViewModel model)
        {
            var seqsProcessosSelecionados = model.GridProcessoSeletivo == null ? new List<long>() : model.GridProcessoSeletivo.Select(p => Convert.ToInt64(p)).ToList();

            this.CampanhaService.ValidarProcessoSeletivoCopiaCampanha(seqsProcessosSelecionados);
        }

        [SMCAllowAnonymous]
        private void ValidarProcessoGPICopiaCampanha(CampanhaCopiaViewModel model)
        {
            var seqsProcessosSelecionados = model.GridProcessoSeletivo == null ? new List<long>() : model.GridProcessoSeletivo.Select(g => Convert.ToInt64(g)).ToList();
            var seqsOfertasSelecionadas = model.GridCampanhaOferta == null ? new List<long>() : model.GridCampanhaOferta.Select(c => Convert.ToInt64(c)).ToList();

            this.CampanhaService.ValidarProcessoGPICopiaCampanha(seqsOfertasSelecionadas, model.ProcessosSeletivos.Where(p => seqsProcessosSelecionados.Contains(p.Seq) && p.SeqProcessoGpi.HasValue).TransformList<CampanhaCopiaProcessoSeletivoListaData>());
        }
    }
}