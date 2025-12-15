using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Calendarios.UI.Mvc.Areas.ESF.Lookups;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.UI.Mvc.Util;
using SMC.Framework.Util;
using SMC.SGA.Professor.Areas.APR.Models;
using SMC.SGA.Professor.Areas.APR.Views.Avaliacao.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Professor.Areas.APR.Controllers
{
    public class AvaliacaoController : SMCControllerBase
    {
        #region [Servicos]

        private IAgendaService AgendaService => Create<IAgendaService>();

        private IEntidadeService EntidadeService => Create<IEntidadeService>();

        private IAplicacaoAvaliacaoService AplicacaoAvaliacaoService => Create<IAplicacaoAvaliacaoService>();

        private IAvaliacaoService AvaliacaoService => Create<IAvaliacaoService>();

        private IEventoAulaService EventoAulaService => Create<IEventoAulaService>();

        private IOrigemAvaliacaoService OrigemAvaliacaoService => Create<IOrigemAvaliacaoService>();

        private ITurmaService TurmaService => Create<ITurmaService>();

        #endregion [Servicos]

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ActionResult Index(AvaliacaoFiltroViewModel filtro)
        {
            PreencherViewModel(filtro);
            return View(filtro);
        }

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ActionResult CabecalhoAvalicaoTurma(long seqOrigemAvaliacao)
        {
            AvaliacaoCabecalhoViewModel model = new AvaliacaoCabecalhoViewModel { SeqOrigemAvaliacao = seqOrigemAvaliacao };
            model.OrigemAvaliacao = OrigemAvaliacaoService.BuscarDescricaoOrigemAvaliacao(seqOrigemAvaliacao);
            return PartialView("_CabecalhoAvalicaoTurma", model);
        }

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ActionResult ListarAvaliacoes(AvaliacaoFiltroViewModel filtro)
        {
            // Cria o filtro data
            AvaliacaoFiltroData avaliacaoFiltroData = filtro.Transform<AvaliacaoFiltroData>();

            // Chama o service
            SMCPagerData<AvaliacaoListarViewModel> lista = AvaliacaoService.BuscarAvaliacoes(avaliacaoFiltroData).Transform<SMCPagerData<AvaliacaoListarViewModel>>();

            // Atualiza nos itens se está com diário aberto ou fechado
            lista.SMCForEach(f => f.DiarioFechado = filtro.DiarioFechado);

            // Monta o retorno

            // Retorna
            SMCPagerModel<AvaliacaoListarViewModel> model = new SMCPagerModel<AvaliacaoListarViewModel>(lista, filtro.PageSettings, filtro);

            return PartialView("_ListagemAvaliacoes", model);
        }

        private void PreencherViewModel(AvaliacaoFiltroViewModel model)
        {
            OrigemAvaliacaoData origemAvaliacao = OrigemAvaliacaoService.BuscarOrigemAvaliacao(model.SeqOrigemAvaliacao);
            model.TiposAvaliacoes = ListarTiposAvaliacao(origemAvaliacao.TipoOrigemAvaliacao);
        }

        [SMCAuthorize(UC_APR_001_01_02.MANTER_AVALIACAO_TURMA)]
        public JsonResult AlterouEntregaWeb(bool entregaWeb, TipoOrigemAvaliacao tipoOrigemAvaliacao, bool temConfiguracaoGrade, bool horarioGrade, long seqOrigemAvaliacao, long? seqAgendaTurma)
        {
            var disponibilizarGrade = temConfiguracaoGrade && tipoOrigemAvaliacao != TipoOrigemAvaliacao.Turma && !entregaWeb;

            if (disponibilizarGrade)
            {
                var divisaoEvento = this.EventoAulaService.BuscarEventosOrigemAvaliacao(null, seqOrigemAvaliacao);
                if (divisaoEvento == null || divisaoEvento.EventoAulas == null || divisaoEvento.EventoAulas.Count() == 0)
                {
                    disponibilizarGrade = false;
                    horarioGrade = false;
                }
                else
                {
                    disponibilizarGrade = true;
                }
            }

            // Verifica se a turma tem integração com o AGD/SEF. Se não tem agenda não integra com AGD/SEF.
            bool turmaIntegracaoSEF = false;
            if (seqAgendaTurma.HasValue)
                turmaIntegracaoSEF = AgendaService.VerificarIntegracaoSEF(seqAgendaTurma.Value, TOKEN_TIPO_EVENTO_SEF.AULA);

            return Json(new
            {
                DisponibilizarGrade = disponibilizarGrade,
                HorarioGrade = disponibilizarGrade ? horarioGrade.ToString() : false.ToString(),
                TurmaIntegracaoSEF = turmaIntegracaoSEF
            }, JsonRequestBehavior.AllowGet);
        }

        private void PreencherViewModel(AvaliacaoViewModel model)
        {
            model.TiposAvaliacoes = ListarTiposAvaliacao(model.TipoOrigemAvaliacao);

            // Caso seja turma, não tem como selecionar grade horaria.
            if (model.TipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
            {
                model.DisponibilizarGrade = false;
                model.HorarioGrade = false;
            }
            else
            {
                model.HorarioGrade = model.SeqInicioGradeAvaliacao != null;
                model.DisponibilizarGrade = model.TemConfiguracaoGrade && !model.EntregaWeb.GetValueOrDefault();
            }

            // Verifica se a turma tem integração com o AGD/SEF. Se não tem agenda não integra com AGD/SEF.
            model.TurmaIntegracaoSEF = false;
            if (model.SeqAgendaTurma.HasValue)
            {
                try
                {
                    model.TurmaIntegracaoSEF = AgendaService.VerificarIntegracaoSEF(model.SeqAgendaTurma.Value, TOKEN_TIPO_EVENTO_SEF.AULA);
                }
                catch (Exception e)
                {
                    SetErrorMessage(e.Message, UIResource.Titulo_Erro, SMCMessagePlaceholders.Centro);
                }
            }

            /*NV10 - Desabilitar o campo deixando a opção "Outros horários" marcada caso:
            1) Se o tipo da origem de avaliação recebido como parâmetro for "Turma" OU
            2) Se o tipo da origem de avaliação recebido como parâmetro for "Divisão de turma" e a divisão não tem configuração
            de grade OU
            3) Se o campo "Entrega online" for igual a "Sim" Se o campo estiver habilitado, marcar a opção "Horarios de grade" como padrão*/

            BuscarDadosGrade(model);

            // Caso seja horario da grade, verifica se não tem informado o horario explicitamente
            if (model.HorarioGrade)
            {
                if (model.SeqInicioGradeAvaliacao.HasValue || model.SeqFimGradeAvaliacao.HasValue)
                    model.HorarioGrade = true;
                else if (model.DataInicioAplicacaoAvaliacao.HasValue || model.DataFimAplicacaoAvaliacao.HasValue)
                    model.HorarioGrade = false;
            }
        }

        /// <summary>
        /// Retorna data source conforme regra de navegação NV_02
        /// </summary>
        /// <returns>Lita Datasourceitem</returns>
        private List<SMCDatasourceItem<TipoAvaliacao>> ListarTiposAvaliacao(TipoOrigemAvaliacao tipoOrigem)
        {
            List<SMCDatasourceItem<TipoAvaliacao>> retorno = new List<SMCDatasourceItem<TipoAvaliacao>>();

            if (tipoOrigem == TipoOrigemAvaliacao.Turma)
            {
                retorno.Add(new SMCDatasourceItem<TipoAvaliacao>() { Seq = TipoAvaliacao.Reavaliacao, Descricao = SMCEnumHelper.GetDescription(TipoAvaliacao.Reavaliacao) });
            }
            else if (tipoOrigem == TipoOrigemAvaliacao.DivisaoTurma)
            {
                retorno.Add(new SMCDatasourceItem<TipoAvaliacao>() { Seq = TipoAvaliacao.Prova, Descricao = SMCEnumHelper.GetDescription(TipoAvaliacao.Prova) });
                retorno.Add(new SMCDatasourceItem<TipoAvaliacao>() { Seq = TipoAvaliacao.Trabalho, Descricao = SMCEnumHelper.GetDescription(TipoAvaliacao.Trabalho) });
            }

            return retorno;
        }

        [SMCAuthorize(UC_APR_001_01_02.MANTER_AVALIACAO_TURMA)]
        public ActionResult Insert(long seqOrigemAvaliacao)
        {
            if (OrigemAvaliacaoService.DiarioAbertoPorOrigemAvaliacao(seqOrigemAvaliacao))
            {
                SetErrorMessage(string.Format(UIResource.Mensagem_Diario_Fechado_Insert),
                                UIResource.Titulo_Erro,
                                SMCMessagePlaceholders.Centro);

                return SMCRedirectToAction("Index", null, new { SeqOrigemAvaliacao = new SMCEncryptedLong(seqOrigemAvaliacao) });
            }

            // Inicia o preenchimento dos dados para criar uma nova avaliação
            var avaliacaoTurmaViewModel = AvaliacaoService.PreencherDadosNovaAvaliacao(seqOrigemAvaliacao).Transform<AvaliacaoViewModel>();

            // Preenche os dados da viewmodel
            PreencherViewModel(avaliacaoTurmaViewModel);

            // Retorna
            return View(avaliacaoTurmaViewModel);
        }

        private AvaliacaoViewModel BuscarDadosGrade(AvaliacaoViewModel avaliacaoTurmaViewModel)
        {
            var divisaoEvento = this.EventoAulaService.BuscarEventosOrigemAvaliacao(null, avaliacaoTurmaViewModel.SeqOrigemAvaliacao);

            if (avaliacaoTurmaViewModel.TipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma || divisaoEvento == null || divisaoEvento.EventoAulas == null || divisaoEvento.EventoAulas.Count() == 0)
            {
                avaliacaoTurmaViewModel.DisponibilizarGrade = false;
                avaliacaoTurmaViewModel.HorarioGrade = false;
            }
            else
            {
                avaliacaoTurmaViewModel.DisponibilizarGrade = true;
                avaliacaoTurmaViewModel.HorarioGrade = true;
                //avaliacaoTurmaViewModel.TurmaSeq = divisaoEvento.TurmaSeq;

                avaliacaoTurmaViewModel.SeqAgendaTurma = divisaoEvento.SeqAgendaTurma;
                avaliacaoTurmaViewModel.GradesInicio = divisaoEvento.EventoAulas.Select(s => new SMCDatasourceItem()
                {
                    Seq = s.Seq,
                    Descricao = $"{s.Data.ToShortDateString()} {s.HoraInicio} - {s.HoraFim}",
                }).ToList();

                TempData["GRADE_HORARIO"] = divisaoEvento.EventoAulas.Select(s => new AvaliacaoGradeTurnoViewModel()
                {
                    Seq = s.Seq,
                    Descricao = $"{s.Data.ToShortDateString()} {s.HoraInicio} - {s.HoraFim}",
                    SeqTurno = s.Turno,
                    Data = s.Data,
                    HoraInicio = Convert.ToInt64(s.HoraInicio.Remove(2, 1)),
                    HoraFim = Convert.ToInt64(s.HoraFim.Remove(2, 1)),
                    Local = s.Local,
                    CodigoLocalSEF = s.CodigoLocalSEF,
                    CodigoUnidadeSEO = divisaoEvento.CodigoUnidadeSEO,
                }).ToList();

                avaliacaoTurmaViewModel.GradesFim = avaliacaoTurmaViewModel.GradesInicio;
            }

            return avaliacaoTurmaViewModel;
        }

        [SMCAuthorize(UC_APR_001_01_02.MANTER_AVALIACAO_TURMA)]
        public ActionResult Edit(long seq)
        {
            // Busca para edição
            AvaliacaoEditarData avaliacao = AvaliacaoService.BuscarAvaliacaoEdicao(seq);

            // Faz o mapeamento
            var model = avaliacao.Transform<AvaliacaoViewModel>();
            PreencherViewModel(model);


            return View(model);
        }

        [SMCAuthorize(UC_APR_001_01_02.MANTER_AVALIACAO_TURMA)]
        public ActionResult Delete(long seq, long seqOrigemAvaliacao)
        {
            SetSuccessMessage(string.Format(UIResource.Mensagem_Exclusao_Avaliacao),
                    UIResource.Titulo_Sucesso,
                    SMCMessagePlaceholders.Centro);

            AvaliacaoService.DeleteAvalicao(seq);

            return SMCRedirectToAction("Index", null, new { SeqOrigemAvaliacao = new SMCEncryptedLong(seqOrigemAvaliacao) });
        }

        [SMCAuthorize(UC_APR_001_01_02.MANTER_AVALIACAO_TURMA)]
        public ActionResult SalvarNovoAvaliacao(AvaliacaoViewModel model)
        {
            try
            {
                long seqAvaliacao = Salvar(model);
                SetSuccessMessage(string.Format(UIResource.Mensagem_Salvar_Avaliacao), UIResource.Titulo_Sucesso, SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Insert", routeValues: new { SeqOrigemAvaliacao = new SMCEncryptedLong(model.SeqOrigemAvaliacao) });
            }
            catch (Exception ex)
            {
                PreencherViewModel(model);

                string view = model.Seq == 0 ? "Insert" : "Edit";
                SetErrorMessage(ex, UIResource.Titulo_Erro, SMCMessagePlaceholders.Centro);
                return View(view, model);
            }
        }

        [SMCAuthorize(UC_APR_001_01_02.MANTER_AVALIACAO_TURMA)]
        public ActionResult SalvarAvaliacao(AvaliacaoViewModel model)
        {
            try
            {
                long seqAvaliacao = Salvar(model);
                SetSuccessMessage(string.Format(UIResource.Mensagem_Salvar_Avaliacao), UIResource.Titulo_Sucesso, SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Edit", routeValues: new { SeqOrigemAvaliacao = new SMCEncryptedLong(model.SeqOrigemAvaliacao), Seq = new SMCEncryptedLong(seqAvaliacao) });
            }
            catch (Exception ex)
            {
                PreencherViewModel(model);

                string view = model.Seq == 0 ? "Insert" : "Edit";
                SetErrorMessage(ex, UIResource.Titulo_Erro, SMCMessagePlaceholders.Centro);
                return View(view, model);
            }
        }

        private long Salvar(AvaliacaoViewModel model)
        {
            if ((!model.EntregaWeb.GetValueOrDefault() && model.Seq > 0) && (model.EntregaWeb != model.EntregaWebInBD))
            {
                Assert(model, UIResource.Mensagem_Assert_Salvar_Avaliacao);

                model.Instrucao = null;
                model.ArquivoAnexadoInstrucao = null;
                model.SeqArquivoAnexadoInstrucao = null;
            }

            if (model.LocalSEF?.Seq == 0)
                model.LocalSEF = null;

            if (model.HorarioGrade)
            {
                model.DataFimAplicacaoAvaliacao = null;
                model.DataInicioAplicacaoAvaliacao = null;
            }
            else
            {
                model.SeqInicioGradeAvaliacao = null;
                model.SeqFimGradeAvaliacao = null;

                var dadosIniciaisNovaAvaliacao = AvaliacaoService.PreencherDadosNovaAvaliacao(model.SeqOrigemAvaliacao).Transform<AvaliacaoViewModel>();

                if (!model.DataFimAplicacaoAvaliacao.HasValue &&
                   (model.DataInicioAplicacaoAvaliacao < dadosIniciaisNovaAvaliacao.DataInicioLimiteAvaliacao || model.DataInicioAplicacaoAvaliacao > dadosIniciaisNovaAvaliacao.DataFimLimiteAvaliacao))
                    throw new AvaliacaoDataForaPeriodoTurmaException();

                if (model.DataFimAplicacaoAvaliacao.HasValue &&
                   (model.DataInicioAplicacaoAvaliacao < dadosIniciaisNovaAvaliacao.DataInicioLimiteAvaliacao ||
                    model.DataInicioAplicacaoAvaliacao > dadosIniciaisNovaAvaliacao.DataFimLimiteAvaliacao ||
                    model.DataFimAplicacaoAvaliacao < dadosIniciaisNovaAvaliacao.DataInicioLimiteAvaliacao ||
                    model.DataFimAplicacaoAvaliacao > dadosIniciaisNovaAvaliacao.DataFimLimiteAvaliacao))
                    throw new AvaliacaoDataForaPeriodoTurmaException();
            }

            AvaliacaoData avalicaoData = new AvaliacaoData();

            avalicaoData.Seq = model.Seq;
            avalicaoData.SeqArquivoAnexadoInstrucao = model.SeqArquivoAnexadoInstrucao;

            avalicaoData.TipoAvaliacao = model.TipoAvaliacao.Value;
            avalicaoData.Descricao = model.Descricao;
            avalicaoData.Instrucao = model.Instrucao;
            avalicaoData.Valor = model.Valor.GetValueOrDefault();
            avalicaoData.AplicacoesAvaliacao = new List<AplicacaoAvaliacaoData>();
            avalicaoData.ArquivoAnexadoInstrucao = model.ArquivoAnexadoInstrucao;
            //Tratamento de arquivo
            //- Caso não tenha o data do arquivo ele busca atraves do helper para prencher o modelo que será salvo
            if (avalicaoData.ArquivoAnexadoInstrucao?.FileData == null && model.ArquivoAnexadoInstrucao != null)
            {
                avalicaoData.ArquivoAnexadoInstrucao.FileData = SMCUploadHelper.GetFileData(model.ArquivoAnexadoInstrucao);
            }
            //-Valida se por ventura o arquivo foi removido e limpa o seq do arquivo anterior
            if (model.SeqArquivoAnexadoInstrucao != null && model.ArquivoAnexadoInstrucao == null)
            {
                avalicaoData.SeqArquivoAnexadoInstrucao = null;
            }
            //renan avalicaoData.SeqInstituicaoEnsino = model.SeqInstituicaoEnsino;
            avalicaoData.AplicacoesAvaliacao.Add(new AplicacaoAvaliacaoData()
            {
                Seq = model.SeqAplicacaoAvaliacao,
                SeqOrigemAvaliacao = model.SeqOrigemAvaliacao,
                SeqAgendaTurma = model.SeqAgendaTurma,
                DataInicioAplicacaoAvaliacao = model.DataInicioAplicacaoAvaliacao,
                DataFimAplicacaoAvaliacao = model.DataFimAplicacaoAvaliacao,
                EntregaWeb = model.EntregaWeb.GetValueOrDefault(),
                QuantidadeMaximaPessoasGrupo = model.QuantidadeMaximaPessoasGrupo,
                Local = model.LocalSEF == null ? model.Local : model.LocalSEF.Descricao,
                Sigla = model.Sigla,
                CodigoLocalSef = (int?)model.LocalSEF?.Seq,
                SeqEventoAgd = model.SeqEventoAgd
            });

            //Verifica se selecionou um Horário da Grade
            if (model.HorarioGrade == true)
            {
                var gradesBanco = (List<AvaliacaoGradeTurnoViewModel>)TempData["GRADE_HORARIO"];
                if (gradesBanco.Count() > 0 &&
                    model.SeqInicioGradeAvaliacao.HasValue &&
                    model.SeqFimGradeAvaliacao.HasValue)
                {
                    avalicaoData.AplicacoesAvaliacao[0].SeqEventoAulaInicio = model.SeqInicioGradeAvaliacao;
                    avalicaoData.AplicacoesAvaliacao[0].SeqEventoAulaFim = model.SeqFimGradeAvaliacao;
                    var registroSelecionado = gradesBanco.Where(w => w.Seq == model.SeqInicioGradeAvaliacao).FirstOrDefault();
                    var registroSelecionadoFim = gradesBanco.Where(w => w.Seq == model.SeqFimGradeAvaliacao).FirstOrDefault().HoraFim;
                    var minutosTotalInicio = (((int)(registroSelecionado.HoraInicio / 100)) * 60) + (registroSelecionado.HoraInicio % 100);
                    var minutosTotalFim = (((int)(registroSelecionadoFim / 100)) * 60) + (registroSelecionadoFim % 100);
                    var dataInicio = registroSelecionado.Data.Value.AddMinutes(minutosTotalInicio);
                    var dataFim = registroSelecionado.Data.Value.AddMinutes(minutosTotalFim);
                    avalicaoData.AplicacoesAvaliacao[0].DataInicioAplicacaoAvaliacao = dataInicio;
                    avalicaoData.AplicacoesAvaliacao[0].DataFimAplicacaoAvaliacao = dataFim;

                    //Em caso de erro vem com a grade preenchida
                    model.GradesInicio = gradesBanco.Select(s => new SMCDatasourceItem()
                    {
                        Seq = s.Seq,
                        Descricao = s.Descricao,
                    }).ToList();
                    model.GradesFim = gradesBanco.Where(w => w.SeqTurno == registroSelecionado.SeqTurno && w.HoraInicio >= registroSelecionado.HoraFim).Select(s => new SMCDatasourceItem()
                    {
                        Seq = s.Seq,
                        Descricao = s.Descricao,
                    }).ToList();
                } 
                TempData["GRADE_HORARIO"] = gradesBanco;
            }
            else if (model.TurmaIntegracaoSEF == true)
            {
                avalicaoData.AplicacoesAvaliacao[0].CodigoLocalSef = (int?)model.LocalSEF?.Seq;
            }

            return AvaliacaoService.SalvarAvaliacao(avalicaoData);
        }

        [SMCAuthorize(UC_APR_001_01_02.MANTER_AVALIACAO_TURMA)]
        public JsonResult ProximaSigla(long seqOrigemAvaliacao, TipoAvaliacao tipoAvaliacao)
        {
            AplicacaoAvaliacaoFiltroData aplicacaoAvaliacaoFiltroData = new AplicacaoAvaliacaoFiltroData();
            aplicacaoAvaliacaoFiltroData.SeqOrigemAvaliacao = seqOrigemAvaliacao;
            aplicacaoAvaliacaoFiltroData.TipoAvaliacao = tipoAvaliacao;

            return Json(AplicacaoAvaliacaoService.BuscarProximaSiglaAvaliacao(aplicacaoAvaliacaoFiltroData));
        }

        [SMCAuthorize(UC_APR_001_01_02.MANTER_AVALIACAO_TURMA)]
        public JsonResult BuscarFimGradeAvaliacaoSelect(long? seqInicioGradeAvaliacao)
        {
            var retorno = new List<SMCDatasourceItem>();
            var gradesBanco = (List<AvaliacaoGradeTurnoViewModel>)TempData["GRADE_HORARIO"];
            if (seqInicioGradeAvaliacao.HasValue)
            {
                var registroSelecionado = gradesBanco.Where(w => w.Seq == seqInicioGradeAvaliacao).FirstOrDefault();
                retorno = gradesBanco.Where(w => w.Data == registroSelecionado.Data
                && w.SeqTurno == registroSelecionado.SeqTurno
                && (w.HoraInicio >= registroSelecionado.HoraFim || w.HoraFim == registroSelecionado.HoraFim)).Select(s => new SMCDatasourceItem()
                {
                    Seq = s.Seq,
                    Descricao = s.Descricao,
                }).ToList();
            }
            else
            {
                retorno = gradesBanco.Select(s => new SMCDatasourceItem()
                {
                    Seq = s.Seq,
                    Descricao = s.Descricao,
                }).ToList();
            }

            TempData["GRADE_HORARIO"] = gradesBanco;
            return Json(retorno);
        }

        [SMCAuthorize(UC_APR_001_01_02.MANTER_AVALIACAO_TURMA)]
        public JsonResult BuscarLocalGradeAvaliacaoSelect(long? seqInicioGradeAvaliacao, bool? horarioGrade, string local)
        {
            string retorno = local;
            if (horarioGrade == true)
            {
                var gradesBanco = (List<AvaliacaoGradeTurnoViewModel>)TempData["GRADE_HORARIO"];
                if (seqInicioGradeAvaliacao.HasValue)
                {
                    var registroSelecionado = gradesBanco.Where(w => w.Seq == seqInicioGradeAvaliacao).FirstOrDefault();
                    if (!String.IsNullOrEmpty(registroSelecionado.Local))
                        retorno = registroSelecionado.Local;
                }
                TempData["GRADE_HORARIO"] = gradesBanco;
            }

            return Json(retorno);
        }

        [SMCAuthorize(UC_APR_001_01_02.MANTER_AVALIACAO_TURMA)]
        public JsonResult BuscarLocalTurmaSEFHorarioLivre(bool? horarioGrade, bool? turmaIntegracaoSEF)
        {
            int? retorno = null;
           
            if (horarioGrade == false && turmaIntegracaoSEF == true)
            {
                var gradesBanco = (List<AvaliacaoGradeTurnoViewModel>)TempData["GRADE_HORARIO"];
                var registroSelecionado = gradesBanco.FirstOrDefault();
                retorno = registroSelecionado.CodigoUnidadeSEO;
                TempData["GRADE_HORARIO"] = gradesBanco;
            }

            return Json(retorno);
        }

        [SMCAuthorize(UC_APR_001_01_02.MANTER_AVALIACAO_TURMA)]
        public JsonResult VerificarTurmaIntegracaoSEF(bool? horarioGrade, bool? disponibilizarGrade, long? seqAgendaTurma)
        {
            try
            {
                bool retorno = false;
                if (seqAgendaTurma.HasValue)
                    retorno = AgendaService.VerificarIntegracaoSEF(seqAgendaTurma.Value, TOKEN_TIPO_EVENTO_SEF.AULA);
                return Json(retorno);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}