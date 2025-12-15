using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.TUR.Exceptions;
using SMC.Academico.Common.Areas.TUR.Exceptions.Turma;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.TUR.Models;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Controllers
{
    public class SelecaoTurmaController : SMCControllerBase
    {
        #region [ Services ]

        private IEventoAulaService EventoAulaService => Create<IEventoAulaService>();

        private IInstituicaoNivelTipoVinculoAlunoService InstituicaoNivelTipoVinculoAlunoService => Create<IInstituicaoNivelTipoVinculoAlunoService>();

        private IHistoricoEscolarService HistoricoEscolarService => Create<IHistoricoEscolarService>();

        private IProcessoEtapaService ProcessoEtapaService => Create<IProcessoEtapaService>();

        private IProcessoService ProcessoService => Create<IProcessoService>();

        private IRequisitoService RequisitoService => Create<IRequisitoService>();

        private ISolicitacaoMatriculaItemService SolicitacaoMatriculaItemService => Create<ISolicitacaoMatriculaItemService>();

        private ISolicitacaoServicoService SolicitacaoServicoService => Create<ISolicitacaoServicoService>();

        private ITurmaService TurmaService => Create<ITurmaService>();

        private ISolicitacaoMatriculaService SolicitacaoMatriculaService => Create<ISolicitacaoMatriculaService>();

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        private ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();

        private IDivisaoMatrizCurricularComponenteService DivisaoMatrizCurricularComponenteService => Create<IDivisaoMatrizCurricularComponenteService>();

        #endregion [ Services ]

        [SMCAuthorize(UC_MAT_003_09_01.SELECIONAR_TURMAS)]
        [HttpGet]
        public ActionResult PesquisarSelecaoTurma(long seqSolicitacaoMatricula, long? seqIngressante, long? seqProcesso = null, long? seqProcessoEtapa = null, string backUrl = null, string turmaDescricao = null, bool pesquisar = false, bool somenteObrigatorio = true, long? seqPrograma = null)
        {
            var model = new SelecaoTurmaViewModel();
            model.SeqSolicitacaoMatricula = seqSolicitacaoMatricula;
            model.SeqIngressante = seqIngressante.GetValueOrDefault();
            model.SeqProcesso = seqProcesso;
            model.SeqProcessoEtapa = seqProcessoEtapa;
            model.backUrl = backUrl;
            model.SomenteObrigatorio = somenteObrigatorio;
            model.SeqPrograma = seqPrograma;
            model.ExibirFiltroCurriculo = true;

            if (seqProcesso.HasValue && seqProcesso > 0)
                model.CicloLetivoDescricao = ProcessoService.BuscarDescricaoCicloLetivoProcesso(model.SeqProcesso.Value);

            string tokenProcesso = ProcessoEtapaService.BuscarTokenProcessoEtapa(model.SeqProcessoEtapa.Value);

            if (seqProcessoEtapa.HasValue && seqProcessoEtapa > 0)
            {
                if (model.SeqIngressante > 0)
                {
                    var dadosVinculo = InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(model.SeqIngressante);
                    model.ExibirFiltroCurriculo = !tokenProcesso.Contains("DISCIPLINA_ELETIVA") && !tokenProcesso.Contains("DISCIPLINA_ISOLADA") && dadosVinculo.ExigeCurso.GetValueOrDefault();
                }
                else
                {
                    model.ExibirFiltroCurriculo = !tokenProcesso.Contains("DISCIPLINA_ELETIVA") && !tokenProcesso.Contains("DISCIPLINA_ISOLADA");
                }
            }

            model.TurmasMatrizOferta = TurmaService.BuscarTurmasPessoaAtuacao(seqSolicitacaoMatricula, model.SeqIngressante, seqProcesso, seqPrograma, tokenProcesso).TransformList<SelecaoTurmaOfertaViewModel>();

            TempData[MatriculaConstants.KEY_SESSION_TURMAS_OFERTADAS] = model.TurmasMatrizOferta;

            //Exibir mensagem de turma com pré requisito
            model.ExibirPreRequisito = model.TurmasMatrizOferta.Count(c => c.PreRequisito) > 0;

            if (!string.IsNullOrEmpty(turmaDescricao))
            {
                var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                model.TurmasMatrizOferta = model.TurmasMatrizOferta.Where(w => compareInfo.IndexOf(w.TurmaFormatado, turmaDescricao, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) > -1).ToList();
            }

            //Exibir somente as turmas do curriculo do aluno
            if (model.SomenteObrigatorio)
                model.TurmasMatrizOferta = model.TurmasMatrizOferta.Where(w => w.Pertence == TurmaOfertaMatricula.ComponentePertence).ToList();

            //Não exibe turma já selecionadas
            var registrosSelecionados = SolicitacaoMatriculaItemService.BuscarSequenciaisSelecaoTurmaSolicitacoesMatriculaItem(seqSolicitacaoMatricula);
            if (registrosSelecionados != null && registrosSelecionados.Count > 0)
                model.TurmasMatrizOferta = model.TurmasMatrizOferta.Where(w => !w.TurmaMatriculaDivisoes.Any(d => registrosSelecionados.Contains(d.Seq.GetValueOrDefault()))).ToList();

            //Controle de turmas com mesmo componente 
            model.TurmasMatrizOferta.ForEach(f =>
            {
                f.TurmaMatriculaDivisoes.ForEach(d =>
                {
                    d.SeqTurmaControle = f.Seq;
                    d.PreRequisito = f.PreRequisito == true ? true : false;
                });
            });

            //Turma com divisão que possui apenas um grupo
            model.TurmasMatrizOferta
                .SelectMany(s => s.TurmaMatriculaDivisoes)
                .Where(w => w.PermitirGrupo && w.SeqDivisaoTurma.HasValue)
                .ToList()
                .ForEach(f =>
                {
                    TurmaDivisaoGrupoSelecionado(f.SeqDivisaoTurma, f.PermitirGrupo, f.SeqDivisaoComponente, f.SeqTurmaControle);
                });

            if (pesquisar)
            {
                var view1 = GetExternalView(AcademicoExternalViews.SELECAO_TURMA_PESQUISAR);
                return PartialView(view1, model);
            }

            var view2 = GetExternalView(AcademicoExternalViews.SELECAO_TURMA);
            return PartialView(view2, model);
        }

        [SMCAuthorize(UC_MAT_003_09_01.SELECIONAR_TURMAS)]
        [HttpPost]
        public ActionResult PesquisarSelecaoTurmaDetalhes(SelecaoTurmaViewModel model)
        {
            return PesquisarSelecaoTurma(model.SeqSolicitacaoMatricula, model.SeqIngressante, model.SeqProcesso, model.SeqProcessoEtapa, model.backUrl, model.TurmaDescricao, true, model.SomenteObrigatorio, model.SeqPrograma);
        }

        private void TurmaDivisaoGrupoSelecionado(long? seqDivisaoTurma, bool? permitirGrupo, long? seqDivisaoComponente, long? seqTurmaControle)
        {
            if (seqDivisaoTurma.HasValue && permitirGrupo == true)
            {
                string valor = seqTurmaControle + "_" + seqDivisaoComponente + "_" + seqDivisaoTurma;
                if (TempData[MatriculaConstants.KEY_SESSION_TURMAS_GRUPOS_DIVISOES] == null)
                {
                    TempData[MatriculaConstants.KEY_SESSION_TURMAS_GRUPOS_DIVISOES] = valor;
                }
                else
                {
                    var dados = TempData[MatriculaConstants.KEY_SESSION_TURMAS_GRUPOS_DIVISOES].ToString().Split(';');
                    string final = valor + ";";
                    foreach (var item in dados)
                    {
                        if (!item.Contains(seqTurmaControle + "_" + seqDivisaoComponente + "_"))
                        {
                            final += item + ";";
                        }
                    }
                    final = final.Substring(0, final.Length - 1);
                    TempData[MatriculaConstants.KEY_SESSION_TURMAS_GRUPOS_DIVISOES] = final;
                }
            }
            else if (permitirGrupo == true)
            {
                if (TempData[MatriculaConstants.KEY_SESSION_TURMAS_GRUPOS_DIVISOES] != null)
                {
                    var dados = TempData[MatriculaConstants.KEY_SESSION_TURMAS_GRUPOS_DIVISOES].ToString().Split(';');
                    string final = string.Empty;
                    foreach (var item in dados)
                    {
                        if (!item.Contains(seqTurmaControle + "_" + seqDivisaoComponente + "_"))
                        {
                            final += item + ";";
                        }
                    }
                    final = final.Substring(0, final.Length - 1);
                    TempData[MatriculaConstants.KEY_SESSION_TURMAS_GRUPOS_DIVISOES] = final;
                }
            }

        }

        [SMCAuthorize(UC_MAT_003_09_01.SELECIONAR_TURMAS)]
        public ActionResult TurmaDivisaoComGrupoSelecionado(long? seqDivisaoTurma, bool? permitirGrupo, long? seqDivisaoComponente, long? seqTurmaControle)
        {
            TurmaDivisaoGrupoSelecionado(seqDivisaoTurma, permitirGrupo, seqDivisaoComponente, seqTurmaControle);
            return Json(seqDivisaoTurma);
        }

        [SMCAuthorize(UC_MAT_003_09_01.SELECIONAR_TURMAS)]
        [HttpPost]
        public ActionResult SalvarSelecaoTurma(SelecaoTurmaViewModel model)
        {
            var registro = (List<SelecaoTurmaOfertaViewModel>)TempData[MatriculaConstants.KEY_SESSION_TURMAS_OFERTADAS];
            var registroGrupo = TempData[MatriculaConstants.KEY_SESSION_TURMAS_GRUPOS_DIVISOES];

            if (registro != null && model.SelectedValues != null && model.SelectedValues[0] != 0)
            {
                try
                {
                    // Completa o modelo (obs.: SeqIngressante = SeqPessoaAtuacao da solicitação de matrícula)
                    if (model.SeqIngressante == 0)
                    {
                        model.SeqIngressante = SolicitacaoServicoService.BuscarSolicitacaoServico(model.SeqSolicitacaoMatricula).SeqPessoaAtuacao;
                    }

                    // Busca os registros selecionados
                    var registrosSelecionados = registro.Where(w => model.SelectedValues.Contains(w.Seq)).ToList();

                    /* Task 35600 - Não permitir seleção de turmas canceladas*/
                    var seqsTurmasCanceladas = TurmaService.ValidarTurmasCanceladas(registrosSelecionados?.Select(r => r.Seq));
                    if (seqsTurmasCanceladas.Any())
                    {
                        var turmasCanceladas = registrosSelecionados?.Where(r => seqsTurmasCanceladas.Contains(r.Seq)).Select(r => r.TurmaFormatado);
                        throw new SelecaoTurmaCanceladaException(turmasCanceladas);
                    }

                    // Faz a validação dos componentes cursados apenas dos itens selecionados
                    var solicitacaoValidacaoCursados = registrosSelecionados.SelectMany(m => m.TurmaMatriculaDivisoes)
                                                        .Select(s => new SolicitacaoMatriculaItemData()
                                                        {
                                                            Seq = s.Seq.GetValueOrDefault(),
                                                            SeqSolicitacaoMatricula = model.SeqSolicitacaoMatricula,
                                                            SeqDivisaoTurma = s.SeqDivisaoTurma,
                                                            SeqConfiguracaoComponente = s.SeqConfiguracaoComponente
                                                        }).ToList();
                    long? seqCicloLetivoProcesso = SolicitacaoMatriculaService.BuscarCicloLetivoProcessoSolicitacaoMatricula(model.SeqSolicitacaoMatricula);
                    var validarComponentesCursados = HistoricoEscolarService.VerificarHistoricoComponentesAprovadosDispensados(
                        model.SeqIngressante,
                        solicitacaoValidacaoCursados?.Select(s => (s.SeqConfiguracaoComponente, s.SeqDivisaoTurma)).ToList(),
                        seqCicloLetivoProcesso);
                    if (!string.IsNullOrEmpty(validarComponentesCursados))
                    {
                        throw new TurmaJaAprovadaDispensadaException(validarComponentesCursados);
                    }

                    // Busca as turmas já selecionadas para as demais validações
                    var registrosValidacao = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(model.SeqSolicitacaoMatricula, model.SeqIngressante, null, model.SeqProcessoEtapa, false)
                        .TransformList<SelecaoTurmaOfertaViewModel>();

                    // Junta os registros de turmas já selecionadas anteriormente com os que estão sendo selecionados agora
                    registrosValidacao.AddRange(registrosSelecionados);

                    // Cria os dados da solicitação para salvar
                    var solicitacaoValidacao = registrosValidacao.SelectMany(m => m.TurmaMatriculaDivisoes)
                                        .Select(s => new SolicitacaoMatriculaItemData()
                                        {
                                            Seq = s.Seq.GetValueOrDefault(),
                                            SeqSolicitacaoMatricula = model.SeqSolicitacaoMatricula,
                                            SeqDivisaoTurma = s.SeqDivisaoTurma,
                                            SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                                            SeqDivisaoComponente = s.SeqDivisaoComponente,
                                            Situacao = s.Situacao
                                        }).ToList();

                    // Valida os Pre-requisitos
                    var validarRequisito = RequisitoService.ValidarPreRequisitos(model.SeqIngressante, solicitacaoValidacao.Select(s => s.SeqDivisaoComponente).ToList(), null, TipoGestaoDivisaoComponente.Turma, model.SeqSolicitacaoMatricula);
                    if (!validarRequisito.Valido)
                    {
                        throw new TurmaPreCoRequisitoInvalidoException($"</br> {string.Join(string.Empty, validarRequisito.MensagensErro)}");
                    }

                    // Verifica a seleção de grupos
                    var registroDescricao = registrosSelecionados.ToList();
                    var permiteGrupo = registrosSelecionados.SelectMany(m => m.TurmaMatriculaDivisoes).Any(a => a.PermitirGrupo);
                    var solicitacaoMatricula = registrosSelecionados.SelectMany(m => m.TurmaMatriculaDivisoes)
                                                        .Select(s => new SolicitacaoMatriculaItemData()
                                                        {
                                                            Seq = s.Seq.GetValueOrDefault(),
                                                            SeqSolicitacaoMatricula = model.SeqSolicitacaoMatricula,
                                                            SeqDivisaoTurma = s.SeqDivisaoTurma,
                                                            SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                                                            SeqDivisaoComponente = s.SeqDivisaoComponente,
                                                            SeqTurmaControle = s.SeqTurmaControle,
                                                            DescricaoFormatada = registroDescricao.First(f => f.TurmaMatriculaDivisoes.Any(a => a.SeqDivisaoComponente == s.SeqDivisaoComponente)).TurmaFormatado,
                                                        }).ToList();
                    if (registroGrupo != null && permiteGrupo)
                    {
                        var registroDivisao = registroGrupo.ToString().Split(';');

                        solicitacaoMatricula.ForEach(f =>
                        {
                            if (!f.SeqDivisaoTurma.HasValue && registroDivisao.FirstOrDefault(d => d.Contains(f.SeqTurmaControle + "_" + f.SeqDivisaoComponente + "_")) != null)
                            {
                                var registroTexto = registroDivisao.FirstOrDefault(d => d.Contains(f.SeqTurmaControle + "_" + f.SeqDivisaoComponente + "_"));
                                var registroDivisaoTexto = registroTexto.Split('_');
                                if (registroDivisaoTexto.Length == 3)
                                {
                                    f.SeqDivisaoTurma = Convert.ToInt64(registroDivisaoTexto[2]);
                                }
                            }
                        });

                        if (solicitacaoMatricula.Any(a => !a.SeqDivisaoTurma.HasValue))
                        {
                            throw new SMCApplicationException("É obrigatório selecionar um grupo para a divisão.");
                        }
                    }
                    else if (registroGrupo == null && permiteGrupo)
                    {
                        throw new SMCApplicationException("É obrigatório selecionar um grupo para a divisão.");
                    }

                    // Verifica componente duplicado
                    var listaDivisoes = solicitacaoMatricula.Select(s => s.SeqDivisaoTurma.GetValueOrDefault()).ToList();
                    var validarTurmasDuplicadas = SolicitacaoMatriculaItemService.ValidarTurmasDuplicadas(model.SeqSolicitacaoMatricula, model.SeqIngressante, model.SeqProcessoEtapa.GetValueOrDefault(), listaDivisoes);
                    if (!string.IsNullOrEmpty(validarTurmasDuplicadas))
                    {
                        throw new TurmaIgualSelecionadaInvalidoException(validarTurmasDuplicadas);
                    }

                    // Verificar a coincidência de horário
                    // TASK 41785
                    // Caso seja uma etapa que possua um dos tokens:
                    // CHANCELA_ALTERACAO_PLANO_ESTUDO, CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA, CHANCELA_DISCIPLINA_ELETIVA_ORIGEM,
                    // CHANCELA_DISCIPLINA_ELETIVA_DESTINO, CHANCELA_PLANO_ESTUDO, CHANCELA_RENOVACAO_MATRICULA,
                    // não consistir essa regra
                    string tokenProcesso = ProcessoEtapaService.BuscarTokenProcessoEtapa(model.SeqProcessoEtapa.Value);
                    if (!tokenProcesso.Contains("CHANCELA_ALTERACAO_PLANO_ESTUDO")
                        && !tokenProcesso.Contains("CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA")
                        && !tokenProcesso.Contains("CHANCELA_DISCIPLINA_ELETIVA_ORIGEM")
                        && !tokenProcesso.Contains("CHANCELA_DISCIPLINA_ELETIVA_DESTINO")
                        && !tokenProcesso.Contains("CHANCELA_PLANO_ESTUDO")
                        && !tokenProcesso.Contains("CHANCELA_RENOVACAO_MATRICULA"))
                    {
                        List<long> seqsDivisao = new List<long>();
                        seqsDivisao.AddRange(solicitacaoValidacao.Where(w => w.SeqDivisaoTurma.HasValue && (w.Situacao == null || !w.Situacao.Contains("exclusão"))).Select(s => s.SeqDivisaoTurma.Value).ToList());
                        seqsDivisao.AddRange(solicitacaoMatricula.Where(w => w.SeqDivisaoTurma.HasValue).Select(s => s.SeqDivisaoTurma.Value).ToList());
                        List<SolicitacaoMatriculaItemData> planoTurmas = new List<SolicitacaoMatriculaItemData>();
                        if (tokenProcesso == "SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA")
                        {
                            planoTurmas = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaItensPlano(model.SeqSolicitacaoMatricula)
                                .Where(w => w.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.Cancelado && w.SeqDivisaoTurma.HasValue
                                && !seqsDivisao.Contains(w.SeqDivisaoTurma.Value)).ToList();
                            seqsDivisao.AddRange(planoTurmas.Where(w => w.Situacao == null || !w.Situacao.Contains("exclusão")).Select(s => s.SeqDivisaoTurma.Value).ToList());
                        }

                        var turmaHorario = this.EventoAulaService.ValidarColisaoHorariosDivisoes(seqsDivisao.Distinct().ToList());
                        if (turmaHorario != null && turmaHorario.Count > 0)
                        {
                            var descricaoTurmaHorario = registrosValidacao.Where(w => w.TurmaMatriculaDivisoes.Any(a => a.SeqDivisaoTurma.HasValue && turmaHorario.Contains(a.SeqDivisaoTurma.Value))).Select(l => l.TurmaFormatado).Distinct().ToList();
                            descricaoTurmaHorario.AddRange(solicitacaoMatricula.Where(w => w.SeqDivisaoTurma.HasValue && turmaHorario.Contains(w.SeqDivisaoTurma.Value)).Select(s => s.DescricaoFormatada).Distinct().ToList());

                            if (tokenProcesso == "SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA")
                            {
                                var seqsTurmaDescricao = planoTurmas.Where(w => w.SeqDivisaoTurma.HasValue && turmaHorario.Contains(w.SeqDivisaoTurma.Value)).Select(s => s.SeqTurma.Value).ToList();

                                seqsTurmaDescricao.ForEach(d =>
                                {
                                    descricaoTurmaHorario.Add(this.TurmaService.BuscarDescricaoTurmaConcatenado(d));
                                });

                            }

                            throw new TurmaCoincidenciaHorarioInvalidoException($"</br> {string.Join("</br>", descricaoTurmaHorario.Distinct().ToList())}");
                        }
                    }

                    var erro = SolicitacaoMatriculaItemService.ValidarVagaTurmaAtividadeIngressante(solicitacaoMatricula, model.SeqIngressante);
                    if (!string.IsNullOrEmpty(erro))
                        throw new TurmaVagasExcedidasException(erro);

                    string erroGravar = SolicitacaoMatriculaItemService.AdicionarSolicitacaoMatriculaTurmasItens(solicitacaoMatricula, model.SeqProcessoEtapa.Value);
                    if (!string.IsNullOrEmpty(erroGravar))
                        throw new SMCApplicationException(erroGravar);

                    if (model.SeqSolicitacaoMatricula > 0 && model.SeqPrograma.HasValue)
                        SolicitacaoServicoService.AtualizarSolicitacaoServicoEntidadeCompartilhada(model.SeqSolicitacaoMatricula, model.SeqPrograma.Value);
                }
                catch (Exception e)
                {
                    TempData[MatriculaConstants.KEY_SESSION_TURMAS_GRUPOS_DIVISOES] = registroGrupo;
                    TempData[MatriculaConstants.KEY_SESSION_TURMAS_OFERTADAS] = registro;
                    throw e;
                }
            }

            return SMCRedirectToUrl(model.backUrl);
        }

        #region [ Seleção Plano de Estudo ]

        [SMCAuthorize(UC_MAT_003_09_01.SELECIONAR_TURMAS)]
        [HttpGet]
        public ActionResult PlanoEstudoPesquisarTurma(long seqSolicitacaoMatricula, string backUrl, bool pesquisa = false, string descricaoTurma = null)
        {
            var model = new PlanoEstudoSelecaoTurmaViewModel();
            model.TurmasOferta = new List<PlanoEstudoTurmaViewModel>();
            model.SeqSolicitacaoMatricula = seqSolicitacaoMatricula;
            model.backUrl = backUrl;

            var seqCicloLetivo = SolicitacaoMatriculaService.BuscarCicloLetivoProcessoSolicitacaoMatricula(seqSolicitacaoMatricula);

            if (seqCicloLetivo.HasValue)
            {
                var cicloLetivo = CicloLetivoService.BuscarCicloLetivo(seqCicloLetivo.Value);
                model.CicloLetivoDescricao = cicloLetivo.Descricao;
            }

            var turmasOfertadas = SolicitacaoMatriculaService.RetornaListaTurmasOfertadas(seqSolicitacaoMatricula, descricaoTurma);

            var periodos = turmasOfertadas.Select(c => c.NumeroPeriodo).Distinct().ToList();

            foreach (var periodo in periodos)
            {
                var turmasNoPeriodo = turmasOfertadas.Where(c => c.NumeroPeriodo == periodo).OrderBy(c => c.NomeDisciplina).ToList();
                var divisaoTurmaOfertada = new PlanoEstudoTurmaViewModel()
                {
                    backUrl = model.backUrl,
                    NomePeriodoFormatado = $"{periodo}º período",
                    TurmaOfertadaItens = new List<PlanoEstudoOfertaItemViewModel>()
                };

                foreach (var turmaNoPeriodo in turmasNoPeriodo)
                {
                    var novoItem = new PlanoEstudoOfertaItemViewModel()
                    {
                        ListaCargasHorarias = new List<string>(),
                        SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                        NomeDisciplina = turmaNoPeriodo.NomeDisciplina,
                        SeqTurma = turmaNoPeriodo.SeqTurma,
                        Pertence = turmaNoPeriodo.PertenceAoCurriculo ? TurmaOfertaMatricula.ComponentePertence : TurmaOfertaMatricula.ComponenteNaoPertence
                    };

                    if (turmaNoPeriodo.CargaHoraria?.Count > 0)
                    {
                        foreach (var cargaHoraria in turmaNoPeriodo.CargaHoraria)
                        {
                            novoItem.ListaCargasHorarias.Add(cargaHoraria.CargaHorariaFormatada);
                        }
                    }

                    divisaoTurmaOfertada.TurmaOfertadaItens.Add(novoItem);
                }
                model.TurmasOferta.Add(divisaoTurmaOfertada);
            }

            if (pesquisa)
            {
                var viewPesquisa = GetExternalView(AcademicoExternalViews.PLANO_ESTUDO_SELECAO_TURMA_OFERTA_PESQUISAR);
                return PartialView(viewPesquisa, model);
            }

            TempData[MatriculaConstants.KEY_SESSION_PLANO_ESTUDO_TURMAS_SELECIONADAS] = model.TurmasOferta;

            var viewIndex = GetExternalView(AcademicoExternalViews.PLANO_ESTUDO_SELECAO_TURMA);
            return PartialView(viewIndex, model);
        }

        [SMCAuthorize(UC_MAT_003_09_01.SELECIONAR_TURMAS)]
        [HttpPost]
        public ActionResult PesquisarSelecaoPlanoEstudoTurmaDetalhes(PlanoEstudoSelecaoTurmaViewModel model)
        {
            return PlanoEstudoPesquisarTurma(model.SeqSolicitacaoMatricula, model.backUrl, true, model.DescricaoTurma);
        }

        [SMCAuthorize(UC_MAT_003_09_01.SELECIONAR_TURMAS)]
        [HttpPost]
        public ActionResult LimparSelecaoPlanoEstudoTurmaDetalhes(PlanoEstudoSelecaoTurmaViewModel model)
        {
            return PlanoEstudoPesquisarTurma(model.SeqSolicitacaoMatricula, model.backUrl, true, null);
        }

        public ActionResult PlanoEstudoSalvarSelecaoTurma(PlanoEstudoSelecaoTurmaViewModel model)
        {
            //var registroDisciplinas = (List<PlanoEstudoTurmaViewModel>)TempData[MatriculaConstants.KEY_SESSION_PLANO_ESTUDO_TURMAS_SELECIONADAS];

            var turmasSelecionadas = model.SelectedValues;

            if (turmasSelecionadas.Count > 0)
                SolicitacaoMatriculaService.PlanoEstudoSalvarTurmasSelecionadas(model.SeqSolicitacaoMatricula, turmasSelecionadas);

            return SMCRedirectToUrl(model.backUrl);
        }

        #endregion

    }
}