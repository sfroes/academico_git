using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CUR.Models;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Controllers
{
    public class SelecaoAtividadeController : SMCControllerBase
    {
        #region [ Services ]

        private IConfiguracaoComponenteService ConfiguracaoComponenteService => Create<IConfiguracaoComponenteService>();

        private ISolicitacaoMatriculaItemService SolicitacaoMatriculaItemService => Create<ISolicitacaoMatriculaItemService>();

        private IRequisitoService RequisitoService => Create<IRequisitoService>();

        #endregion [ Services ]

        [SMCAuthorize(UC_MAT_003_10_01.SELECIONAR_ATIVIDADES_ACADEMICAS)]
        [HttpGet]
        public ActionResult PesquisarSelecaoAtividade(long seqSolicitacaoMatricula, long seqIngressante, long seqProcessoEtapa, string backUrl)
        {
            var model = new SelecaoAtividadeViewModel();
            model.SeqSolicitacaoMatricula = seqSolicitacaoMatricula;
            model.SeqIngressante = seqIngressante;
            //model.SeqProcesso = seqProcesso;
            model.SeqProcessoEtapa = seqProcessoEtapa;
            model.backUrl = backUrl;

            model.AtividadesAcademicaMatriculaItem = ConfiguracaoComponenteService.BuscarConfiguracaoComponentePessoaAtuacaoEntidade(model.SeqIngressante, seqSolicitacaoMatricula).TransformList<SelecaoAtividadeOfertaViewModel>();

            TempData[MatriculaConstants.KEY_SESSION_ATIVIDADES_OFERTADAS] = model.AtividadesAcademicaMatriculaItem;

            //Exibir mensagem de turma com pré requisito
            model.ExibirPreRequisito = model.AtividadesAcademicaMatriculaItem.Count(c => c.PreRequisito) > 0;

            //Não exibe turma já selecionadas
            var registroGravadosBanco = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaAtividadesItens(seqSolicitacaoMatricula, model.SeqIngressante, model.SeqProcessoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso);

            //var registrosSelecionados = (List<long>)Session[MatriculaConstants.KEY_SESSION_ATIVIDADES_SELECIONADAS];
            var registrosSelecionados = registroGravadosBanco.Select(x => x.Seq).ToList();

            if (registrosSelecionados != null && registrosSelecionados.Count > 0)
                model.AtividadesAcademicaMatriculaItem = model.AtividadesAcademicaMatriculaItem.Where(w => !registrosSelecionados.Contains(w.SeqSolicitacaoMatriculaItem.GetValueOrDefault())).ToList();

            var view = GetExternalView(AcademicoExternalViews.SELECAO_ATIVIDADE);
            return PartialView(view, model);
        }

        [SMCAuthorize(UC_MAT_003_10_01.SELECIONAR_ATIVIDADES_ACADEMICAS)]
        [HttpPost]
        public ActionResult SalvarSelecaoAtividade(SelecaoAtividadeViewModel model)
        {
            //TODO: Melhorar código repetido em MatriculaController.SalvarSelecaoAtividadeAcademicaMatricula
            var registro = (List<SelecaoAtividadeOfertaViewModel>)TempData[MatriculaConstants.KEY_SESSION_ATIVIDADES_OFERTADAS];
            if (registro != null && model.SelectedValues != null)
            {
                var registrosSelecionados = registro.Where(w => model.SelectedValues.Contains(w.Seq));
				var configuracoesSelecionadas = registrosSelecionados?.Select(t => t.Seq)?.ToList();

				// Busca as turmas já selecinadas para a validação
				var registrosValidacao = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaAtividadesItens(model.SeqSolicitacaoMatricula, model.SeqIngressante, model.SeqProcessoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                    .TransformList<SelecaoAtividadeOfertaViewModel>();
                registrosValidacao.ForEach(f => f.Seq = f.SeqConfiguracaoComponente);
                registrosValidacao.AddRange(registrosSelecionados);

                var solicitacaoValidacao = registrosValidacao.Select(s => new SolicitacaoMatriculaItemData()
                {
                    Seq = s.SeqSolicitacaoMatriculaItem.GetValueOrDefault(),
                    SeqSolicitacaoMatricula = model.SeqSolicitacaoMatricula,
                    SeqDivisaoTurma = null,
                    SeqConfiguracaoComponente = s.Seq,                    
                }).ToList();

                var validar = RequisitoService.ValidarPreRequisitos(model.SeqIngressante,null, solicitacaoValidacao.Select(s => s.SeqConfiguracaoComponente.GetValueOrDefault()).ToList(), null, model.SeqSolicitacaoMatricula);
                if (!validar.Valido)
                {
                    TempData[MatriculaConstants.KEY_SESSION_ATIVIDADES_OFERTADAS] = registro;
                    throw new ConfiguracaoComponentePreRequisitoInvalidoException($"</br> {string.Join("</br>", validar.MensagensErro)}");
                }
                
                var registroDescricao = registrosSelecionados.ToList();
                var solicitacaoMatricula = registrosSelecionados.Select(s => new SolicitacaoMatriculaItemData()
                {
                    Seq = s.SeqSolicitacaoMatriculaItem.GetValueOrDefault(),
                    SeqSolicitacaoMatricula = model.SeqSolicitacaoMatricula,
                    SeqDivisaoTurma = null,
                    SeqConfiguracaoComponente = s.Seq,
                    DescricaoFormatada = registroDescricao.First(f => f.Seq == s.Seq).DescricaoFormatada,
                }).ToList();

                try
                {
                    string erroGravar = SolicitacaoMatriculaItemService.AdicionarSolicitacaoMatriculaTurmasItens(solicitacaoMatricula, model.SeqProcessoEtapa);
                    if (!string.IsNullOrEmpty(erroGravar))
                        throw new SMCApplicationException(erroGravar);
                }
                catch (Exception e)
                {
                    TempData[MatriculaConstants.KEY_SESSION_ATIVIDADES_OFERTADAS] = registro;
                    throw e;
                }
            }
            return SMCRedirectToUrl(model.backUrl);
        }
    }
}