using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.MAT.Exceptions;
using SMC.Academico.Common.Areas.MAT.Includes;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Common.Areas.TUR.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Jobs.Service;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using static iTextSharp.text.pdf.AcroFields;

namespace SMC.Academico.Domain.Areas.MAT.DomainServices
{
    public class SolicitacaoMatriculaItemDomainService : AcademicoContextDomain<SolicitacaoMatriculaItem>
    {
        #region [ DomainService ]

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService => Create<ConfiguracaoEtapaDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => Create<MatrizCurricularOfertaDomainService>();

        private OrientacaoPessoaAtuacaoDomainService OrientacaoPessoaAtuacaoDomainService => Create<OrientacaoPessoaAtuacaoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private PlanoEstudoDomainService PlanoEstudoDomainService => Create<PlanoEstudoDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        private ProcessoEtapaDomainService ProcessoEtapaDomainService => Create<ProcessoEtapaDomainService>();

        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService => Create<ProcessoSeletivoOfertaDomainService>();

        private RequisitoDomainService RequisitoDomainService => Create<RequisitoDomainService>();

        private RestricaoTurmaMatrizDomainService RestricaoTurmaMatrizDomainService => Create<RestricaoTurmaMatrizDomainService>();

        private SituacaoItemMatriculaDomainService SituacaoItemMatriculaDomainService => Create<SituacaoItemMatriculaDomainService>();

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();

        private SolicitacaoMatriculaItemHistoricoSituacaoDomainService SolicitacaoMatriculaItemHistoricoSituacaoDomainService => Create<SolicitacaoMatriculaItemHistoricoSituacaoDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Adiciona os itens selecionado na tela de matricula ao registro de solicitacao de matricula
        /// </summary>
        /// <param name="turmasSelecionadas">Turmas selecionadas no processo de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <returns>Mensagem de erro da validação de vagas e requisitos</returns>
        public string AdicionarSolicitacaoMatriculaTurmasItens(List<SolicitacaoMatriculaItemVO> turmasSelecionadas, long seqProcessoEtapa)
        {
            try
            {
                using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin(SMCIsolationLevel.Serializable))
                {
                    // No fluxo de disciplina eletiva e mudança de plano de estudo existe duas situações iniciais
                    // para controle de chancela e liberação da turma
                    var situacaoItemCadastro = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(seqProcessoEtapa, null, null, ClassificacaoSituacaoFinal.FinalizadoComSucesso);
                    var situacaoItemNaoAlterado = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(seqProcessoEtapa, null, null, ClassificacaoSituacaoFinal.NaoAlterado);
                    var processoEtapa = ProcessoEtapaDomainService.BuscarProcessoEtapa(seqProcessoEtapa);
                    string listaErro = string.Empty;

                    if (turmasSelecionadas.Count > 0)
                    {
                        var seqSolicitacaoMatricula = turmasSelecionadas.First().SeqSolicitacaoMatricula;
                        var seqProcessoEtapaAtual = SolicitacaoMatriculaDomainService.SearchProjectionByKey(new SolicitacaoMatriculaFilterSpecification() { Seq = seqSolicitacaoMatricula },
                        x => x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa);

                        if (seqProcessoEtapa != seqProcessoEtapaAtual)
                        {
                            transacao.Rollback();
                            return $"A etapa atual não permite este processo.";
                        }
                    }

                    var itensSelecionadosPreviamente = this.SearchProjectionBySpecification(new SolicitacaoMatriculaItemFilterSpecification { SeqSolicitacaoMatricula = turmasSelecionadas.First().SeqSolicitacaoMatricula }, x => new
                    {
                        Seq = x.Seq,
                        SeqDivisaoTurma = x.SeqDivisaoTurma,
                        x.SeqConfiguracaoComponente,
                        PertencePlanoEstudo = x.PertencePlanoEstudo,
                        UltimoSeqSituacaoitem = x.HistoricosSituacao.OrderByDescending(a => a.Seq).FirstOrDefault().SeqSituacaoItemMatricula
                    });

                    foreach (var item in turmasSelecionadas)
                    {
                        // Verifica se o item já existe no plano de estudo
                        var itemSelecionadoPreviamente = itensSelecionadosPreviamente?.FirstOrDefault(i => i.Seq == item.Seq && i.SeqDivisaoTurma == item.SeqDivisaoTurma);
                        if (itemSelecionadoPreviamente != null && situacaoItemNaoAlterado != null)
                        {
                            //Quando o item já existir recebe a situação de Não Alterado
                            if (itemSelecionadoPreviamente.UltimoSeqSituacaoitem != situacaoItemCadastro.Seq && itemSelecionadoPreviamente.UltimoSeqSituacaoitem != situacaoItemNaoAlterado.Seq)
                            {
                                SolicitacaoMatriculaItemHistoricoSituacao historico = new SolicitacaoMatriculaItemHistoricoSituacao();
                                historico.SeqSolicitacaoMatriculaItem = itemSelecionadoPreviamente.Seq;
                                historico.SeqSituacaoItemMatricula = itemSelecionadoPreviamente.PertencePlanoEstudo ? situacaoItemNaoAlterado.Seq : situacaoItemCadastro.Seq;
                                this.SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);

                                if (processoEtapa != null && processoEtapa.ControleVaga)
                                {
                                    //Processar a vaga da turma selecionada
                                    var erro = ProcessarVagaTurmaAtividadeIngressante(itemSelecionadoPreviamente.Seq, item.DescricaoFormatada);
                                    if (!string.IsNullOrEmpty(erro))
                                        listaErro += erro;
                                }
                            }
                        }
                        else
                        {
                            //Verifica se o item de divisão ja foi adicionado antes para atualizar apenas o histórico
                            var itemDivisaoJaAdicionado = itensSelecionadosPreviamente?.FirstOrDefault(i => i.SeqConfiguracaoComponente == item.SeqConfiguracaoComponente && i.SeqDivisaoTurma == item.SeqDivisaoTurma);
                            if (itemDivisaoJaAdicionado != null)
                            {
                                item.Seq = itemDivisaoJaAdicionado.Seq;
                                item.PertencePlanoEstudo = itemDivisaoJaAdicionado.PertencePlanoEstudo;
                            }
                            else
                            {
                                item.Seq = 0;
                                item.PertencePlanoEstudo = false;
                            }

                            //Incluir um item novo com situação de finalizado com sucesso
                            var solicitacao = item.Transform<SolicitacaoMatriculaItem>();
                            this.SaveEntity(solicitacao);

                            SolicitacaoMatriculaItemHistoricoSituacao historico = new SolicitacaoMatriculaItemHistoricoSituacao
                            {
                                SeqSolicitacaoMatriculaItem = solicitacao.Seq
                            };
                            if (itemDivisaoJaAdicionado != null && item.PertencePlanoEstudo == true)
                            {
                                historico.SeqSituacaoItemMatricula = situacaoItemNaoAlterado.Seq;
                            }
                            else
                            {
                                historico.SeqSituacaoItemMatricula = situacaoItemCadastro.Seq;
                            }

                            this.SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);

                            if (processoEtapa != null && processoEtapa.ControleVaga)
                            {
                                //Processar a vaga da turma selecionada
                                var erro = ProcessarVagaTurmaAtividadeIngressante(solicitacao.Seq, item.DescricaoFormatada);
                                if (!string.IsNullOrEmpty(erro))
                                    listaErro += erro;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(listaErro))
                    {
                        transacao.Commit();
                        return string.Empty;
                    }
                    else
                    {
                        transacao.Rollback();
                        return $"Não foi possível realizar as solicitações:</br> {listaErro}";
                    }
                }
            }
            catch (Exception e)
            {
                if (e.InnerException != null && (e.InnerException is SqlException) && (e.InnerException as SqlException).Number == 1205)
                    throw new SMCApplicationException("Ocorreu um problema de concorrência ao reservar a vaga para a turma. Por favor, tente novamente.");

                throw e;
            }
        }

        /// <summary>
        /// Alterar os itens de solicitação de matricula de acordo com a turma selecionada para edição
        /// </summary>
        /// <param name="itens">Itens de solicitação de matricula</param>
        /// <param name="seqProcessoEtapa">Controle de vagas de acordo com Processo Etapa</param>
        public void AlterarSolicitacaoMatriculaTurmasItens(List<SolicitacaoMatriculaItemVO> itens, long seqProcessoEtapa)
        {
            try
            {
                using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin(SMCIsolationLevel.Serializable))
                {
                    string listaErro = string.Empty;
                    var situacaoItemCancelado = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(seqProcessoEtapa, null, null, ClassificacaoSituacaoFinal.Cancelado);

                    var processoEtapa = ProcessoEtapaDomainService.BuscarProcessoEtapa(seqProcessoEtapa);

                    var seqSolicitacaoMatricula = this.SearchByKey(new SMCSeqSpecification<SolicitacaoMatriculaItem>(itens.First().Seq)).SeqSolicitacaoMatricula;

                    var itensAlterado = new List<SolicitacaoMatriculaItemVO>();
                    foreach (var item in itens)
                    {
                        item.SeqSolicitacaoMatricula = seqSolicitacaoMatricula;
                        if (item.SeqDivisaoTurma.HasValue)
                        {
                            var registro = this.SearchByKey(new SMCSeqSpecification<SolicitacaoMatriculaItem>(item.Seq));

                            if (registro.SeqDivisaoTurma == item.SeqDivisaoTurma)
                                continue;

                            if (processoEtapa != null && processoEtapa.ControleVaga)
                            {
                                var seqPessoaAtuacao = this.SearchProjectionByKey(item.Seq, x => x.SolicitacaoMatricula.SeqPessoaAtuacao);

                                //A vaga ja é processada quando adicionamos o item no final deste método
                                //Processar a vaga da turma selecionada
                                //var erro = DivisaoTurmaDomainService.ProcessarVagaTurma(seqPessoaAtuacao, item.SeqDivisaoTurma.Value, item.DescricaoFormatada, true);

                                //if (!string.IsNullOrEmpty(erro))
                                //    throw new SMCApplicationException($"Não foi possível realizar as solicitações:</br> {erro}");

                                //Liberar a vaga da turma desmarcada
                                DivisaoTurmaDomainService.LiberarVagaTurma(seqPessoaAtuacao, registro.SeqDivisaoTurma.Value, true);

                            }

                            SolicitacaoMatriculaItemHistoricoSituacao historico = new SolicitacaoMatriculaItemHistoricoSituacao();
                            historico.SeqSolicitacaoMatriculaItem = registro.Seq;
                            historico.SeqSituacaoItemMatricula = situacaoItemCancelado.Seq;
                            historico.MotivoSituacaoMatricula = MotivoSituacaoMatricula.PorTrocaDeGrupo;
                            this.SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);

                            itensAlterado.Add(item);
                        }
                    }

                    if (itensAlterado.Count > 0)
                        AdicionarSolicitacaoMatriculaTurmasItens(itensAlterado, seqProcessoEtapa);

                    transacao.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Lista os itens de turmas selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa</param>
        /// <param name="habilitar">Habilitar os botões de editar e excluir de cada registro</param>
        /// <returns>Lista de turmas gravadas pelo ingressante</returns>
        public SMCPagerData<TurmaMatriculaListarVO> BuscarSolicitacaoMatriculaTurmasItens(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long? seqTurma, long? seqProcessoEtapa, bool habilitar, bool selecaoTurma = false, bool listagemTurmas = false)
        {
            //Recupera a matriz curricular oferta e o tipo de atuação
            InstituicaoNivelTipoVinculoAlunoVO dadosVinculo = new InstituicaoNivelTipoVinculoAlunoVO();
            PessoaAtuacaoDadosOrigemVO dadosOrigem = new PessoaAtuacaoDadosOrigemVO();
            List<long> seqsTurmasPertence = new List<long>();
            List<TurmaMatriculaListarVO> configuracoesCoRequisitos = new List<TurmaMatriculaListarVO>();

            if (seqPessoaAtuacao == 0 && seqSolicitacaoMatricula != 0)
                seqPessoaAtuacao = SolicitacaoMatriculaDomainService.SearchProjectionByKey(seqSolicitacaoMatricula, x => x.SeqPessoaAtuacao);

            //Disciplina Eletiva não necessita de informar o seqPessoaAtuacao
            if (seqPessoaAtuacao > 0)
            {
                dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);

                dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

                configuracoesCoRequisitos = TurmaDomainService.BuscarConfiguracaoTurmasCoRequisitoPessoaAtuacao(seqSolicitacaoMatricula, seqPessoaAtuacao);
            }

            if (dadosOrigem.SeqMatrizCurricularOferta > 0)
                seqsTurmasPertence = TurmaDomainService.BuscarSequenciaisTurmaMatrizOfertaLegenda(dadosOrigem.SeqMatrizCurricularOferta, seqSolicitacaoMatricula);

            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                SeqTurma = seqTurma,
                SituacaoTurmaAtual = SituacaoTurma.Ofertada,
                ExibirTurma = true,
                ItemDaMatriculaCanceladoPelaInstituicao = false
            };

            string tokenProcessoEtapa = string.Empty;
            List<long> seqsSituacaoEtapa = new List<long>();
            if (seqProcessoEtapa.HasValue && seqProcessoEtapa.Value > 0)
            {
                tokenProcessoEtapa = ProcessoEtapaDomainService.BuscarTokenProcessoEtapa(seqProcessoEtapa.Value);

                var etapa = SGFHelper.BuscarEtapas(seqSolicitacaoMatricula);
                var etapaConfiguracao = SGFHelper.BuscarEtapa(seqSolicitacaoMatricula, etapa.Where(w => w.SeqProcessoEtapa == seqProcessoEtapa.Value).FirstOrDefault().SeqConfiguracaoEtapa);
                seqsSituacaoEtapa = etapaConfiguracao.Situacoes.Select(s => s.SeqSituacao).ToList();
            }

            if (!tokenProcessoEtapa.Contains(MatriculaTokens.CHANCELA))
                specSolicitacaoMatriculaItem.ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.FinalizadoComSucesso;

            //Quando for plano de estudo tem que exibir cancelado pelo solicitante e não editável
            if (tokenProcessoEtapa == MatriculaTokens.SOLICITACAO_ALTERACAO_PLANO_ESTUDOS || tokenProcessoEtapa == MatriculaTokens.SOLICITACAO_ALTERACAO_PLANO_ESTUDOS_DI)
            {

                if (selecaoTurma)
                {
                    specSolicitacaoMatriculaItem.RegraSelecaoTurma = true;
                    specSolicitacaoMatriculaItem.ClassificacaoSituacaoFinal = null;
                }
                else if (listagemTurmas)
                {
                    specSolicitacaoMatriculaItem.RegraListagemTurma = true;
                    specSolicitacaoMatriculaItem.ClassificacaoSituacaoFinal = null;
                }
                else
                {
                    specSolicitacaoMatriculaItem.SolicitacaoPlanoEstudoAlterar = true;
                    specSolicitacaoMatriculaItem.ClassificacaoSituacaoFinal = null;
                    specSolicitacaoMatriculaItem.ItemDoPlanoCanceladoPelaInstituicao = false;
                }

            }
            else if (tokenProcessoEtapa == MatriculaTokens.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA)
            {
                specSolicitacaoMatriculaItem.DiferentePertencePlanoEstudo = true;
                specSolicitacaoMatriculaItem.ClassificacaoSituacaoFinal = null;
                specSolicitacaoMatriculaItem.ClassificacaoSituacoesFinaisDiferentes = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.Cancelado | ClassificacaoSituacaoFinal.FinalizadoSemSucesso };
            }
            else
            {
                specSolicitacaoMatriculaItem.MotivosSituacaoMatriculaDiferente = new[] { MotivoSituacaoMatricula.PeloSolicitante, MotivoSituacaoMatricula.PorTrocaDeGrupo };
            }

            if (tokenProcessoEtapa.Contains(MatriculaTokens.EFETIVACAO_MATRICULA))
                specSolicitacaoMatriculaItem.SeqProcessoEtapa = seqProcessoEtapa.Value;

            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;
            specSolicitacaoMatriculaItem.SetOrderBy(p => p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault().Descricao);

            var dadosItens = this.SearchProjectionBySpecification(specSolicitacaoMatriculaItem,
                p => new
                {
                    Seq = p.DivisaoTurma.SeqTurma,
                    SeqDivisaoTurma = p.SeqDivisaoTurma,
                    SeqNivelEnsino = p.ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino,
                    Codigo = p.DivisaoTurma.Turma.Codigo,
                    Numero = p.DivisaoTurma.Turma.Numero,
                    SeqTipoComponenteCurricular = p.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                    QuantidadeVagas = (short?)p.DivisaoTurma.Turma.QuantidadeVagas,
                    DescricaoTipoTurma = p.DivisaoTurma.Turma.TipoTurma.Descricao,
                    AssociacaoOfertaMatrizTipoTurma = p.DivisaoTurma.Turma.TipoTurma.AssociacaoOfertaMatriz,
                    DescricaoCicloLetivoInicio = p.DivisaoTurma.Turma.CicloLetivoInicio.Descricao,
                    DescricaoCicloLetivoFim = p.DivisaoTurma.Turma.CicloLetivoFim.Descricao,
                    SeqConfiguracaoComponentePrincipal = p.SeqConfiguracaoComponente.Value,
                    DescricaoConfiguracaoComponente = p.ConfiguracaoComponente.Descricao,
                    DescricaoConfiguracaoComponenteTurma = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).Descricao,
                    DescricaoConfiguracaoComponenteTurmaPrincipal = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).Descricao,
                    ComponenteCurricular = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).RestricoesTurmaMatriz.FirstOrDefault().ComponenteCurricularAssunto, // Nem toda REstricaoTurma possui ComponenteCurricularAssunto                    
                    Habilitar = habilitar,
                    SeqIngressante = (long?)seqPessoaAtuacao,

                    Credito = (short?)p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).ConfiguracaoComponente.ComponenteCurricular.Credito,
                    CreditoPrincipal = (short?)p.DivisaoTurma.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.Credito,

                    Situacao = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.Descricao ?? string.Empty,
                    SituacaoEtapa = p.HistoricosSituacao.Where(h => seqProcessoEtapa.HasValue && h.SituacaoItemMatricula.SeqProcessoEtapa == seqProcessoEtapa && seqsSituacaoEtapa.Contains(h.SeqSituacaoItemMatricula)).OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.Descricao ?? string.Empty,
                    ClassificacaoSituacaoFinal = (ClassificacaoSituacaoFinal?)p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal,
                    SituacaoInicial = (bool?)p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.SituacaoInicial,
                     SituacaoFinal = (bool?)p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.SituacaoFinal,
                    Motivo = (MotivoSituacaoMatricula?)p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula,
                    MotivoEtapa = (MotivoSituacaoMatricula?)p.HistoricosSituacao.Where(h => seqProcessoEtapa.HasValue && h.SituacaoItemMatricula.SeqProcessoEtapa == seqProcessoEtapa && seqsSituacaoEtapa.Contains(h.SeqSituacaoItemMatricula)).OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula,
                    PertencePlanoEstudo = (bool?)p.PertencePlanoEstudo,
                    SeqTurma = p.DivisaoTurma.SeqTurma,
                    DivisoesTurma = p.DivisaoTurma.Turma.DivisoesTurma.Select(s => new DivisaoTurmaVO()
                    {
                        Seq = s.Seq,
                        SeqDivisaoComponente = s.SeqDivisaoComponente,
                        NumeroDivisaoComponente = s.DivisaoComponente.Numero,
                        SeqLocalidade = (long?)s.SeqLocalidade,
                        DescricaoLocalidade = s.Localidade.Nome,
                        SeqOrigemAvaliacao = s.SeqOrigemAvaliacao,
                        SeqTurma = s.SeqTurma,
                        NumeroGrupo = s.NumeroGrupo,
                        QuantidadeVagas = s.QuantidadeVagas,
                        QuantidadeVagasOcupadas = (short?)s.QuantidadeVagasOcupadas,
                        QuantidadeVagasReservadas = (short?)s.QuantidadeVagasReservadas,
                        OrigemAvaliacao = new OrigemAvaliacaoVO()
                        {
                            Seq = s.OrigemAvaliacao.Seq,
                            SeqCriterioAprovacao = (long?)s.OrigemAvaliacao.SeqCriterioAprovacao,
                            QuantidadeGrupos = (short?)s.OrigemAvaliacao.QuantidadeGrupos,
                            QuantidadeProfessores = (short?)s.OrigemAvaliacao.QuantidadeProfessores,
                            ApurarFrequencia = (bool?)s.OrigemAvaliacao.ApurarFrequencia,
                            NotaMaxima = (short?)s.OrigemAvaliacao.NotaMaxima,
                            SeqEscalaApuracao = (long?)s.OrigemAvaliacao.SeqEscalaApuracao,
                        }
                    }).ToList(),
                    TurmaDivisoesItem = new TurmaDivisoesVO()
                    {
                        Seq = p.Seq,
                        SeqTurma = p.DivisaoTurma.SeqTurma,
                        SeqConfiguracaoComponente = p.SeqConfiguracaoComponente ?? 0,
                        SeqDivisaoComponente = (long?)p.DivisaoTurma.SeqDivisaoComponente ?? 0,
                        TipoDivisaoDescricao = p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.Descricao,
                        GerarOrientacao = (bool?)p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.GeraOrientacao ?? false,
                        SeqTipoOrientacao = p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.SeqTipoOrientacao,
                        Numero = (short?)p.DivisaoTurma.DivisaoComponente.Numero ?? 0,
                        CargaHoraria = p.DivisaoTurma.DivisaoComponente.CargaHoraria,
                        DescricaoComponenteCurricularOrganizacao = p.DivisaoTurma.DivisaoComponente.ComponenteCurricularOrganizacao.Descricao,
                        PermitirGrupo = p.DivisaoTurma.DivisaoComponente.PermiteGrupo
                    },
                    SeqsConfiguracoesComponentes = p.DivisaoTurma.Turma.ConfiguracoesComponente.Select(s => s.SeqConfiguracaoComponente).ToList()
                }).ToList();

            List<TurmaMatriculaListarVO> registros = new List<TurmaMatriculaListarVO>();
            dadosItens.ForEach(r =>
            {
                var map = SMCMapperHelper.Create<TurmaMatriculaListarVO>(r);
                map.Credito = r.Credito ?? r.CreditoPrincipal;
                map.DescricaoConfiguracaoComponenteTurma = map.DescricaoConfiguracaoComponenteTurma ?? r.DescricaoConfiguracaoComponenteTurmaPrincipal;

                registros.Add(map);
            });

            //Exibir campo apenas quando o tipo de pessoa-atuação for aluno
            //ou for ingressante e o o processo seletivo não está configurado para fazer reserva de vaga.
            var exibirVagas = false;
            if (seqPessoaAtuacao == 0 || dadosOrigem.TipoAtuacao == TipoAtuacao.Aluno)
            {
                exibirVagas = true;
            }
            else
            {
                var ingressanteProcessoSeletivo = IngressanteDomainService.BuscarIngressanteProcessoSeletivo(seqPessoaAtuacao);
                exibirVagas = !ingressanteProcessoSeletivo.ProcessoSeletivo.ReservaVaga;
            }

            foreach (var item in registros)
            {
                //Verificar se a configuração de componente da turma pertence a matriz curricular oferta do ingressante
                if (dadosOrigem.SeqMatrizCurricularOferta > 0)
                {
                    item.Pertence = seqsTurmasPertence.Contains(item.Seq) ? TurmaOfertaMatricula.ComponentePertence : TurmaOfertaMatricula.ComponenteNaoPertence;
                    var seqDivisaoComponente = new long[] { item.TurmaDivisoesItem.SeqDivisaoComponente };
                    var validacaoPre = RequisitoDomainService.ValidarPreRequisitos(seqPessoaAtuacao, seqDivisaoComponente, null, null, null);

                    item.PreRequisito = !validacaoPre.Valido;
                }
                else
                    item.Pertence = TurmaOfertaMatricula.ComponenteNaoPertence;

                var tiposComponenteNivel = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(item.SeqNivelEnsino, item.SeqTipoComponenteCurricular);

                item.TurmaMatriculaDivisoes = new List<TurmaMatriculaListarDetailVO>();

                var matriculaDivisao = new TurmaMatriculaListarDetailVO();

                if (tiposComponenteNivel != null)
                    item.TurmaDivisoesItem.FormatoCargaHoraria = tiposComponenteNivel.FormatoCargaHoraria;

                DivisaoTurmaVO divisaoItemDetalhe = null;
                if (item.SeqDivisaoTurma.HasValue)
                {
                    divisaoItemDetalhe = item.DivisoesTurma.Where(w => w.Seq == item.SeqDivisaoTurma && w.SeqDivisaoComponente == item.TurmaDivisoesItem.SeqDivisaoComponente).FirstOrDefault();
                }
                else
                {
                    divisaoItemDetalhe = item.DivisoesTurma.Where(w => w.SeqDivisaoComponente == item.TurmaDivisoesItem.SeqDivisaoComponente).FirstOrDefault();
                }

                if (divisaoItemDetalhe != null)
                {
                    item.TurmaDivisoesItem.TurmaCodigoFormatado = item.CodigoFormatado;
                    item.TurmaDivisoesItem.NumeroDivisaoComponente = divisaoItemDetalhe.NumeroDivisaoComponente;
                    item.TurmaDivisoesItem.NumeroGrupo = divisaoItemDetalhe.NumeroGrupo;
                }

                matriculaDivisao.DivisaoTurmaDescricao = item.TurmaDivisoesItem.DescricaoFormatada;
                matriculaDivisao.DivisaoTurmaDescricaoSemNumero = item.TurmaDivisoesItem.DescricaoFormatadaSemNumero;
                matriculaDivisao.DivisaoTurmaRelatorioDescricao = item.TurmaDivisoesItem.DivisaoTurmaRelatorioDescricao;
                matriculaDivisao.PermitirGrupo = item.TurmaDivisoesItem.PermitirGrupo;
                matriculaDivisao.SeqConfiguracaoComponente = item.TurmaDivisoesItem.SeqConfiguracaoComponente;
                matriculaDivisao.Seq = item.TurmaDivisoesItem.Seq;
                matriculaDivisao.SeqTurma = item.TurmaDivisoesItem.SeqTurma;
                matriculaDivisao.SeqPessoaAtuacao = seqPessoaAtuacao;
                matriculaDivisao.SeqDivisaoComponente = item.TurmaDivisoesItem.SeqDivisaoComponente;
                matriculaDivisao.DivisoesTurmas = new List<SMCDatasourceItem>();

                var divisoesSelect = item.DivisoesTurma.Where(w => w.SeqDivisaoComponente == item.TurmaDivisoesItem.SeqDivisaoComponente)
                                                            .Select(s => new SMCDatasourceItem()
                                                            {
                                                                Seq = s.Seq,
                                                                Descricao = exibirVagas ?
                                                                $"{item.CodigoFormatado}.{s.NumeroDivisaoComponente}.{s.NumeroGrupo.ToString().PadLeft(3, '0')} - {s.QuantidadeVagasDisponiveis} vagas disponíveis" :
                                                                $"{item.CodigoFormatado}.{s.NumeroDivisaoComponente}.{s.NumeroGrupo.ToString().PadLeft(3, '0')}"
                                                            });

                matriculaDivisao.DivisoesTurmas.AddRange(divisoesSelect);

                if (matriculaDivisao.DivisoesTurmas.Count == 1 && !item.SeqDivisaoTurma.HasValue)
                    matriculaDivisao.SeqDivisaoTurma = matriculaDivisao.DivisoesTurmas[0].Seq;
                else if (item.SeqDivisaoTurma.HasValue)
                    matriculaDivisao.SeqDivisaoTurma = item.SeqDivisaoTurma.Value;

                item.TurmaMatriculaDivisoes.Add(matriculaDivisao);
            }

            List<TurmaMatriculaListarVO> retorno = new List<TurmaMatriculaListarVO>();
            registros.SMCForEach(f =>
            {
                if ((!f.PertencePlanoEstudo.HasValue || f.PertencePlanoEstudo == false) && f.SituacaoFinal == true && f.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                    f.SituacaoPlanoEstudo = MatriculaPertencePlanoEstudo.Inclusao;
                else if (f.PertencePlanoEstudo == true && f.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado && (f.Motivo == MotivoSituacaoMatricula.PeloSolicitante || f.Motivo == MotivoSituacaoMatricula.PorTrocaDeGrupo || f.Motivo == MotivoSituacaoMatricula.PorDispensaAprovacao))
                    f.SituacaoPlanoEstudo = MatriculaPertencePlanoEstudo.Exclusao;
                else if (f.PertencePlanoEstudo == true && f.SituacaoInicial == true && f.SituacaoFinal == true && f.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado)
                    f.SituacaoPlanoEstudo = MatriculaPertencePlanoEstudo.NaoAlterado;
                else
                    f.SituacaoPlanoEstudo = MatriculaPertencePlanoEstudo.Nenhum;

                f.TurmaMatriculaDivisoes.SMCForEach(fd =>
                {
                    if (f.SeqDivisaoTurma == fd.SeqDivisaoTurma)
                    {
                        fd.Motivo = f.Motivo;
                        fd.Situacao = seqProcessoEtapa.HasValue ? f.SituacaoEtapa : f.Situacao;
                        fd.SituacaoPlanoEstudo = f.SituacaoPlanoEstudo;
                        fd.PertencePlanoEstudo = f.PertencePlanoEstudo.GetValueOrDefault();
                    }
                });
            });

            registros.GroupBy(g => g.Seq).SMCForEach(f =>
            {
                var primeiro = f.First();
                primeiro.TurmaMatriculaDivisoes = f.SelectMany(s => s.TurmaMatriculaDivisoes).OrderBy(o => o.DivisaoTurmaDescricao).ToList();
                retorno.Add(primeiro);
            });

            return new SMCPagerData<TurmaMatriculaListarVO>(retorno);
        }

        public string VerificarTurmasAprovadasDispensadasSelecaoTurma(long seqSolicitacaoServico)
        {
            // Recupera os dados das turmas que devem ser verificadas

            /*Somente se a pessoa-atuação for do tipo de atuação “Aluno” E possuir um vínculo ou tipo de termo de intercâmbio parametrizado por 
             instituição - nível de ensino-vínculo para conceder formação de acordo com a instituição de ensino logada, o nível de ensino e o vínculo do aluno.  

              1.Verificar se a pessoa - atuação em questão contém registro em seu histórico escolar, para o curso oferta localidade, com situação "Aprovado", 
                de algum item da solicitação em um ciclo letivo diferente do ciclo letivo do processo:

                            1.1 Somente para o serviço com o token “SOLICITACAO_ALTERACAO_PLANO_ESTUDO” verificar se a situação configurada para ser final com a classificação “Não alterado”.

                            1.2 Com a situação atual configurada para ser final com a classificação “Finalizado com sucesso”
                            Caso tenha, abortar a operação e exibir a seguinte mensagem de erro:

                            “Seleção não permitida.As turmas abaixo já foram concluídas ou dispensadas.”
                            - < Turma A >
                             - < Turma B >

              2.Caso contrário, verificar se a pessoa-atuação em questão contém registro em seu histórico escolar, para o curso oferta localidade, com situação "Dispensado", de algum item da solicitação:

                        2.1 Somente para o serviço com o token “SOLICITACAO_ALTERACAO_PLANO_ESTUDO” verificar se a situação configurada para ser final com a classificação “Não alterado”.

                        2.2 Com a situação atual configurada para ser final com a classificação “Finalizado com sucesso”
                        Caso tenha, abortar a operação e exibir a seguinte mensagem de erro:

                        “Seleção não permitida.As turmas abaixo já foram concluídas ou dispensadas.”
                        - < Turma A >
                         - < Turma B >

              3.Caso não exista, não fazer nada.*/

            var dadosSolicitacao = SolicitacaoMatriculaDomainService.SearchProjectionByKey(seqSolicitacaoServico, x => new
            {
                TipoAtuacao = x.PessoaAtuacao.TipoAtuacao,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqCicloLetivo = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                Itens = x.Itens.Where(i => i.SeqDivisaoTurma.HasValue).Select(t => new
                {
                    SeqSolicitacaoMatriculaItem = t.Seq,
                    Classificacao = t.HistoricosSituacao.OrderByDescending(h => h.Seq).Select(c => new
                    {
                        c.SituacaoItemMatricula.ClassificacaoSituacaoFinal,
                        c.SeqSituacaoItemMatricula,
                        c.SituacaoItemMatricula.SituacaoFinal,
                        c.SituacaoItemMatricula.SituacaoInicial
                    }).FirstOrDefault(),
                    t.SeqConfiguracaoComponente,
                    t.SeqDivisaoTurma
                })
            });

            if (dadosSolicitacao.TipoAtuacao == TipoAtuacao.Aluno)
            {
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosSolicitacao.SeqPessoaAtuacao);
                var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(dadosOrigem.SeqInstituicaoEnsino,
                                                                                                                                                        dadosOrigem.SeqNivelEnsino,
                                                                                                                                                        dadosOrigem.SeqTipoVinculoAluno,
                                                                                                                                                        IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio);

                if ((instituicaoNivelTipoVinculoAluno.ConcedeFormacao) ||
                       (instituicaoNivelTipoVinculoAluno?.TiposTermoIntercambio.Any(t => t.ConcedeFormacao) ?? false))
                {
                    var validar = new List<(long? SeqConfiguracaoComponente, long? SeqDivisaoTurma)>();
                    foreach (var turma in dadosSolicitacao.Itens)
                    {
                        if (dadosSolicitacao.TokenServico == TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO)
                        {
                            if (turma.Classificacao.SituacaoFinal && turma.Classificacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado)
                                validar.Add((turma.SeqConfiguracaoComponente, turma.SeqDivisaoTurma));
                        }

                        if (turma.Classificacao.SituacaoFinal && turma.Classificacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                            validar.Add((turma.SeqConfiguracaoComponente, turma.SeqDivisaoTurma));
                    }

                    return HistoricoEscolarDomainService.VerificarHistoricoComponentesAprovadosDispensados(dadosSolicitacao.SeqPessoaAtuacao, validar, dadosSolicitacao.SeqCicloLetivo);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Busca os sequencial de acordo com os filtros informados
        /// </summary>
        /// <param name="filtro">Filtro </param>
        /// <returns>Sequenciais da solicitação matrícula item</returns>
        public List<long?> BuscarSequenciaisSelecaoTurmaSolicitacoesMatriculaItem(long seqSolicitacaoMatricula)
        {
            // Busca os dados da solicitação para fazer a atualização do histórico dos itens e mudança da etapa.
            var tokenEtapaAtual = this.SolicitacaoMatriculaDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x =>
                x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.Token);

            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                SituacaoTurmaAtual = SituacaoTurma.Ofertada,
                ExibirTurma = true,
                MotivoSituacaoMatriculaDiferente = MotivoSituacaoMatricula.SolicitacaoCancelada,
            };

            var registros = this.SearchProjectionBySpecification(specSolicitacaoMatriculaItem,
                                                                p => new TurmaMatriculaListarVO()
                                                                {
                                                                    SituacaoInicial = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault() != null ?
                                                                               p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.SituacaoInicial :
                                                                               false,
                                                                    ClassificacaoSituacaoFinal = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault() != null ?
                                                                               p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal :
                                                                               null,
                                                                    TokenEtapa = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault() != null ?
                                                                               p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ProcessoEtapa.Token :
                                                                               null,
                                                                    Motivo = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula,
                                                                    PertencePlanoEstudo = p.PertencePlanoEstudo,
                                                                    //SituacaoTurmaAtual = p.DivisaoTurma.Turma.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma,
                                                                    TurmaDivisoesItem = new TurmaDivisoesVO()
                                                                    {
                                                                        Seq = p.Seq,
                                                                    },
                                                                }).ToList();

            var registroValidos = new List<TurmaMatriculaListarVO>();

            if (tokenEtapaAtual.Contains(MatriculaTokens.CHANCELA))
            {
                //Ser o token da etapa for Chancela, porque o registro pode estar com situação "Aguardando Chancela" ou "Chancela Indeferida"
                registroValidos.AddRange(registros.Where(w => w.TokenEtapa.Contains(MatriculaTokens.CHANCELA) && w.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.Cancelado));
            }

            //Situação final com classificação "Finalizada com sucesso",
            registroValidos.AddRange(registros.Where(w => w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso));

            //Situação final com classificação "Não alterado",
            registroValidos.AddRange(registros.Where(w => w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado));

            //Validação comentada porque de acordo com a Task 41253
            // Estiverem com a situação atual igual a situação parametrizada para ser a situação final,
            // com classificação "Cancelada" e motivo "Pela instituição"
            // E a situação da turma é igual a "Cancelada". 
            // Como só busca as turmas com situação igual a Ofertada, NUNCA vai ocorrer essa validação.
            //registroValidos.AddRange(registros.Where(w => w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado && w.Motivo == MotivoSituacaoMatricula.PelaInstituicao));

            //Situação final com classificação "Cancelada", motivo "Pelo solicitante" e campo "Pertence ao plano de estudo" igual a SIM.
            registroValidos.AddRange(registros.Where(w => w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado && w.Motivo == MotivoSituacaoMatricula.PeloSolicitante && w.PertencePlanoEstudo == true));

            return registroValidos.Select(s => s.TurmaDivisoesItem.Seq).ToList();
        }

        /// <summary>
        /// Verifica se existe alguma turma ou atividade já cadastrada
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir turma ou atividade selecionada no processo de matricula</returns>
        public bool VerificarTurmasAtividadesCadastradas(long seqSolicitacaoMatricula)
        {
            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.FinalizadoComSucesso,
                DiferentePertencePlanoEstudo = true
            };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            var registros = this.Count(specSolicitacaoMatriculaItem);

            return registros > 0;
        }

        /// <summary>
        /// Verifica se existe alguma turma ou atividade não alterada e pelo menos uma cancelada, ou pelo menos uma adiciona
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir turma ou atividade selecionada no processo de matricula</returns>
        public bool VerificarTurmasAtividadesCanceladasPlano(long seqSolicitacaoMatricula)
        {
            var specCancelado = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.Cancelado,
                MotivoSituacaoMatricula = MotivoSituacaoMatricula.PeloSolicitante,
                PertencePlanoEstudo = true
            };
            specCancelado.MaxResults = int.MaxValue;

            var registrosCancelado = this.Count(specCancelado) > 0;

            var specNaoAlterado = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.NaoAlterado,
                PertencePlanoEstudo = true
            };
            specNaoAlterado.MaxResults = int.MaxValue;

            var registrosNaoAlterado = this.Count(specNaoAlterado) > 0;

            var specAdicionado = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.FinalizadoComSucesso,
                DiferentePertencePlanoEstudo = true
            };
            specAdicionado.MaxResults = int.MaxValue;

            var registrosAdicionado = this.Count(specAdicionado) > 0;

            return (registrosCancelado && registrosNaoAlterado) || registrosAdicionado;
        }

        /// <summary>
        /// Verifica se existe alguma turma cancelada para a solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir turma cancelada na solicitação de matricula</returns>
        public bool VerificarSolicitacaoMatriculaTurmasCancelada(long? seqSolicitacaoMatricula)
        {
            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                SituacaoTurmaAtual = SituacaoTurma.Cancelada,
                ExibirTurma = true,
            };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            var registros = this.SearchProjectionBySpecification(specSolicitacaoMatriculaItem, p => p.Seq).Count();

            return registros > 0;
        }

        /// <summary>
        /// Verifica se existe alguma atividade academica cancelada para a solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir item cancelado na solicitação de matricula</returns>
        public bool VerificarSolicitacaoMatriculaItemCancelado(long? seqSolicitacaoMatricula)
        {
            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                ClassificacaoSituacoesFinais = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.Cancelado, ClassificacaoSituacaoFinal.FinalizadoSemSucesso },
                MotivosSituacaoMatriculaDiferente = new MotivoSituacaoMatricula[] { MotivoSituacaoMatricula.PeloSolicitante, MotivoSituacaoMatricula.PorTrocaDeGrupo }
            };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            var registros = this.SearchProjectionBySpecification(specSolicitacaoMatriculaItem, p => p.Seq).Count();

            return registros > 0;
        }

        /// <summary>
        /// Verifica se existe alguma turma ou atividade academica cancelada pelo solicitante para a solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir item cancelado na solicitação de matricula</returns>
        public bool VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(long? seqSolicitacaoMatricula)
        {
            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                ClassificacaoSituacoesFinais = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.Cancelado, ClassificacaoSituacaoFinal.FinalizadoSemSucesso },
                MotivoSituacaoMatricula = MotivoSituacaoMatricula.PeloSolicitante
            };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            var registros = this.SearchProjectionBySpecification(specSolicitacaoMatriculaItem, p => p.Seq).Count();

            return registros > 0;
        }

        /// <summary>
        /// Remove os itens de turmas selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        public void RemoverSolicitacaoMatriculaItemPorTurma(long seqSolicitacaoMatricula, long seqTurma, long seqProcessoEtapa)
        {
            try
            {
                // Verifica se existe lançamento de nota para turma que está sendo excluída
                var dadosSolicitacao = SolicitacaoMatriculaDomainService.SearchProjectionByKey(seqSolicitacaoMatricula, x => new
                {
                    SeqAluno = x.SeqPessoaAtuacao,
                    SeqCicloLetivo = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo
                });
                var existeNotaTurma = TurmaDomainService.VerificarLancamentoNotaTurma(dadosSolicitacao.SeqAluno, seqTurma, dadosSolicitacao.SeqCicloLetivo.GetValueOrDefault());
                if (existeNotaTurma)
                    throw new ExclusaoTurmaComHistoricoEscolarException();

                using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin(SMCIsolationLevel.Serializable))
                {
                    var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification() { SeqSolicitacaoMatricula = seqSolicitacaoMatricula, SeqTurma = seqTurma, ExibirTurma = true };
                    specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

                    var registros = this.SearchBySpecification(specSolicitacaoMatriculaItem, IncludesSolicitacaoMatriculaItem.DivisaoTurma).ToList();
                    var situacaoItemCadastro = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(seqProcessoEtapa, null, true, ClassificacaoSituacaoFinal.Cancelado);
                    var processoEtapa = ProcessoEtapaDomainService.BuscarProcessoEtapa(seqProcessoEtapa);

                    foreach (var item in registros)
                    {
                        SolicitacaoMatriculaItemHistoricoSituacao historico = new SolicitacaoMatriculaItemHistoricoSituacao();
                        historico.SeqSolicitacaoMatriculaItem = item.Seq;
                        historico.SeqSituacaoItemMatricula = situacaoItemCadastro.Seq;
                        historico.MotivoSituacaoMatricula = MotivoSituacaoMatricula.PeloSolicitante;
                        this.SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);

                        if (processoEtapa != null && processoEtapa.ControleVaga)
                        {
                            LiberarVagaTurmaAtividadeIngressante(item.Seq);
                        }
                    }

                    var specSolicitacaoMatriculaItemTurma = new SolicitacaoMatriculaItemFilterSpecification()
                    {
                        SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                        ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.FinalizadoComSucesso,
                        DiferentePertencePlanoEstudo = true,
                        ExibirTurma = true
                    };

                    var tokenProcessoEtapa = ProcessoEtapaDomainService.BuscarTokenProcessoEtapa(seqProcessoEtapa);

                    if (tokenProcessoEtapa == MatriculaTokens.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA && this.Count(specSolicitacaoMatriculaItemTurma) == 0)
                        SolicitacaoServicoDomainService.AtualizarSolicitacaoServicoEntidadeCompartilhada(seqSolicitacaoMatricula, null);

                    transacao.Commit();
                }
            }
            catch (Exception e)
            {
                if (e.InnerException != null && (e.InnerException is SqlException) && (e.InnerException as SqlException).Number == 1205)
                    throw new SMCApplicationException("Ocorreu um problema de concorrência ao liberar a vaga para a turma. Por favor, tente novamente.");

                throw e;
            }
        }

        /// <summary>
        /// Insere novamente os itens de turmas selecionado na tela de matricula como não editado
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <param name="descricao">Descrição da turma para caso de erro na ocupação de vaga</param>
        public string DesfazerRemoverSolicitacaoMatriculaItemPorTurma(long seqSolicitacaoMatricula, long seqTurma, long seqProcessoEtapa, string descricao)
        {
            try
            {
                using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin(SMCIsolationLevel.Serializable))
                {
                    var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification() { SeqSolicitacaoMatricula = seqSolicitacaoMatricula, SeqTurma = seqTurma, ExibirTurma = true };
                    specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

                    var registros = this.SearchBySpecification(specSolicitacaoMatriculaItem, IncludesSolicitacaoMatriculaItem.DivisaoTurma).Where(w => w.PertencePlanoEstudo).ToList();
                    var situacaoItemCadastro = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(seqProcessoEtapa, true, true, ClassificacaoSituacaoFinal.NaoAlterado);
                    var processoEtapa = ProcessoEtapaDomainService.BuscarProcessoEtapa(seqProcessoEtapa);
                    string listaErro = string.Empty;

                    foreach (var item in registros)
                    {
                        SolicitacaoMatriculaItemHistoricoSituacao historico = new SolicitacaoMatriculaItemHistoricoSituacao();
                        historico.SeqSolicitacaoMatriculaItem = item.Seq;
                        historico.SeqSituacaoItemMatricula = situacaoItemCadastro.Seq;
                        historico.MotivoSituacaoMatricula = null;
                        this.SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);

                        if (processoEtapa != null && processoEtapa.ControleVaga)
                        {
                            //Processar a vaga da turma selecionada
                            var erro = ProcessarVagaTurmaAtividadeIngressante(item.Seq, descricao);
                            if (!string.IsNullOrEmpty(erro))
                                listaErro += erro;
                        }
                    }

                    if (string.IsNullOrEmpty(listaErro))
                    {
                        transacao.Commit();
                        return string.Empty;
                    }
                    else
                    {
                        transacao.Rollback();
                        return $"Não foi possível realizar as solicitações:</br> {listaErro}";
                    }
                }
            }
            catch (Exception e)
            {
                if (e.InnerException != null && (e.InnerException is SqlException) && (e.InnerException as SqlException).Number == 1205)
                    throw new SMCApplicationException("Ocorreu um problema de concorrência ao reservar a vaga para a turma. Por favor, tente novamente.");

                throw e;
            }
        }

        /// <summary>
        /// Remove o item de atividade selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatriculaItem">Sequencial da solicitacao de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <param name="motivo">Motivo do cancelamento do item de matrícula</param>
        public void AlterarSolicitacaoMatriculaItemParaCancelado(long seqSolicitacaoMatriculaItem, long seqProcessoEtapa, MotivoSituacaoMatricula? motivo)
        {
            var situacaoItemCadastro = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(seqProcessoEtapa, null, true, ClassificacaoSituacaoFinal.Cancelado);

            SolicitacaoMatriculaItemHistoricoSituacao historico = new SolicitacaoMatriculaItemHistoricoSituacao();
            historico.SeqSolicitacaoMatriculaItem = seqSolicitacaoMatriculaItem;
            historico.SeqSituacaoItemMatricula = situacaoItemCadastro.Seq;
            historico.MotivoSituacaoMatricula = motivo;
            this.SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);
        }

        /// <summary>
        /// Insere novamente o item de atividade selecionado na tela de matricula como não editado
        /// </summary>
        /// <param name="seqSolicitacaoMatriculaItem">Sequencial da solicitacao de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        public void DesfazerRemoverSolicitacaoMatriculaItemPorAtividade(long seqSolicitacaoMatriculaItem, long seqProcessoEtapa)
        {
            var situacaoItemCadastro = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(seqProcessoEtapa, true, true, ClassificacaoSituacaoFinal.NaoAlterado);

            SolicitacaoMatriculaItemHistoricoSituacao historico = new SolicitacaoMatriculaItemHistoricoSituacao();
            historico.SeqSolicitacaoMatriculaItem = seqSolicitacaoMatriculaItem;
            historico.SeqSituacaoItemMatricula = situacaoItemCadastro.Seq;
            historico.MotivoSituacaoMatricula = null;
            this.SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);
        }

        /// <summary>
        /// Busca turma e atividades de acordo com solicitação de matricula, ingressante e etapa
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matrícula</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <param name="erro">Exibir apenas com situação cancelado ou finalizado sem sucesso</param>
        /// <param name="desconsiderarEtapa">Desconsidera a etapa para recuperar a situação atual do item</param>
        /// <param name="desativarFiltroDados">Desconsidera o filtro de dados</param>
        /// <returns>Objeto com as turmas e atividade</returns>
        public SolicitacaoMatriculaTurmaAtividadeVO BuscarSolicitacaoMatriculaTurmasAtividades(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, bool erro, bool desconsiderarEtapa, bool desativarFiltroDados = false, bool omitirVagas = false)
        {
            // Necessário remover o filtro global porque existe chancela de secretaria diferente ao do aluno no caso de disciplina eletiva
            if (desativarFiltroDados)
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var etapas = SGFHelper.BuscarEtapas(seqSolicitacaoMatricula);
            var etapa = etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == seqConfiguracaoEtapa);
            var seqsSituacaoEtapa = etapa.Situacoes.Select(s => s.SeqSituacao);
            var dadosConfiguracaoEtapa = ConfiguracaoEtapaDomainService.SearchProjectionByKey(seqConfiguracaoEtapa, x => new
            {
                Token = x.ProcessoEtapa.Token,
                SituacoesItens = x.ProcessoEtapa.SituacoesItemMatricula.Select(s => s.Seq).ToList()
            });
            var tokenEtapa = dadosConfiguracaoEtapa.Token;
            var seqsSituacoesItemMatriculaEtapa = dadosConfiguracaoEtapa.SituacoesItens;

            var retorno = new SolicitacaoMatriculaTurmaAtividadeVO() { Seq = etapa.SeqEtapaSGF, Etapa = etapa.DescricaoEtapa, TokenEtapa = tokenEtapa };

            var filtro = new SolicitacaoMatriculaItemFilterSpecification() { SeqSolicitacaoMatricula = seqSolicitacaoMatricula };

            if (erro)
            {
                filtro.ClassificacaoSituacoesFinais = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.Cancelado, ClassificacaoSituacaoFinal.FinalizadoSemSucesso };
                filtro.MotivosSituacaoMatriculaDiferente = new MotivoSituacaoMatricula[] { MotivoSituacaoMatricula.PeloSolicitante, MotivoSituacaoMatricula.PorTrocaDeGrupo };
            }
            else
            {
                filtro.ItemDaMatriculaCanceladoPelaInstituicao = false;
            }


            filtro.SeqsSituacoesItemMatricula = seqsSituacoesItemMatriculaEtapa;
            filtro.DesconsiderarEtapa = desconsiderarEtapa;

            bool exibirEntidadeResponsavelTurma = false;
            if (tokenEtapa == MatriculaTokens.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA
             || tokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_ORIGEM
             || tokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO)
            {
                exibirEntidadeResponsavelTurma = true;
            }

            if ((tokenEtapa == MatriculaTokens.SOLICITACAO_ALTERACAO_PLANO_ESTUDOS
             || tokenEtapa == MatriculaTokens.SOLICITACAO_ALTERACAO_PLANO_ESTUDOS_DI)
             && erro == false)
            {
                filtro.SolicitacaoPlanoEstudoAlterar = true;
            }
            else
            {
                filtro.ItemDoPlanoPertenceAoPlanoDeEstudoOuNaoPertenceEMotivoCancelamentoNaoEPeloSolicitante = true;
            }


            var seqPessoaAtuacao = SolicitacaoServicoDomainService.SearchProjectionByKey(seqSolicitacaoMatricula, p => p.SeqPessoaAtuacao);
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao, desativarFiltroDados);

            var dadosItens = this.SearchProjectionBySpecification(filtro, p => new
            {
                Seq = p.Seq,
                SeqPessoaAtuacao = p.SolicitacaoMatricula.SeqPessoaAtuacao,
                SeqDivisaoTurma = (long?)p.SeqDivisaoTurma,
                SeqTurma = (long?)p.DivisaoTurma.SeqTurma,
                SeqConfiguracaoComponente = (long?)p.SeqConfiguracaoComponente,
                Codigo = (int?)p.DivisaoTurma.Turma.Codigo,
                Numero = (short?)p.DivisaoTurma.Turma.Numero,
                DescricaoTipoTurma = p.DivisaoTurma.Turma.TipoTurma.Descricao,
                QuantidadeVagas = (short?)p.DivisaoTurma.Turma.QuantidadeVagas,
                PertencePlanoEstudo = (bool?)p.PertencePlanoEstudo,
                AssociacaoOfertaMatrizTipoTurma = (AssociacaoOfertaMatriz?)p.DivisaoTurma.Turma.TipoTurma.AssociacaoOfertaMatriz,
                SeqEntidadeResponsavel = (long?)p.SolicitacaoMatricula.SeqEntidadeResponsavel,

                SeqProgramaTurma = (long?)p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.Entidade.Seq,
                SeqProgramaTurmaPrincipal = (long?)p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.Entidade.Seq,

                ProgramaTurma = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.Entidade.Nome,
                ProgramaTurmaPrincipal = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.Entidade.Nome,
                ProgramaCompartilhado = p.SolicitacaoMatricula.EntidadeCompartilhada.Nome,

                DescricaoConfiguracaoComponenteTurma = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).Descricao,
                DescricaoConfiguracaoComponenteTurmaPrincipal = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).Descricao,

                SeqTipoComponenteCurricular = p.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                DescricaoConfiguracaoComponente = p.ConfiguracaoComponente.Descricao,

                SeqConfiguracaoComponentePrincipal = p.SeqConfiguracaoComponente.Value,
                Situacao = p.HistoricosSituacao.Where(h => desconsiderarEtapa || seqsSituacoesItemMatriculaEtapa.Contains(h.SeqSituacaoItemMatricula)).OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.Descricao ?? (desconsiderarEtapa ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.Descricao : string.Empty),
                ClassificacaoSituacaoFinal = (ClassificacaoSituacaoFinal?)p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal,
                SituacaoInicial = (bool?)p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.SituacaoInicial,
                SituacaoFinal = (bool?)p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.SituacaoFinal,
                SeqSituacao = (long?)p.HistoricosSituacao.Where(h => desconsiderarEtapa || seqsSituacoesItemMatriculaEtapa.Contains(h.SeqSituacaoItemMatricula)).OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.Seq ?? (desconsiderarEtapa ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.Seq : (long?)null),
                HistoricoSeqSituacao = p.HistoricosSituacao.OrderByDescending(h => h.Seq).Select(h => h.SeqSituacaoItemMatricula).ToList(),
                Motivo = (MotivoSituacaoMatricula?)p.HistoricosSituacao.Where(h => desconsiderarEtapa || seqsSituacoesItemMatriculaEtapa.Contains(h.SeqSituacaoItemMatricula)).OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula,
                // ComponenteCurricularAssunto = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).ComponenteCurricularAssunto,
                TurmaDivisoesItem = new TurmaDivisoesVO
                {
                    Seq = p.Seq,
                    SeqConfiguracaoComponente = (long?)p.DivisaoTurma.DivisaoComponente.SeqConfiguracaoComponente ?? 0,
                    SeqDivisaoComponente = (long?)p.DivisaoTurma.SeqDivisaoComponente ?? 0,
                    TipoDivisaoDescricao = p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.Descricao,
                    Numero = (short?)p.DivisaoTurma.DivisaoComponente.Numero ?? 0,
                    CargaHoraria = (short?)p.DivisaoTurma.DivisaoComponente.CargaHoraria,
                    DescricaoComponenteCurricularOrganizacao = p.DivisaoTurma.DivisaoComponente.ComponenteCurricularOrganizacao.Descricao,
                    PermitirGrupo = (bool?)p.DivisaoTurma.DivisaoComponente.PermiteGrupo ?? false,
                    Assunto = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).ComponenteCurricularAssunto.Descricao
                }
            }).ToList();

            long? seqMatrizCurricularOferta = null;
            var exibirVagas = false;

            if (dadosItens.SMCCount() > 0)
            {
                InstituicaoNivelTipoVinculoAlunoVO dadosVinculo = new InstituicaoNivelTipoVinculoAlunoVO();
                dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao, desativarFiltroDados);

                seqMatrizCurricularOferta = dadosOrigem.SeqMatrizCurricularOferta;

                //Verificar se o vínculo da pessoa-atuação foi parametrizado para exigir oferta de matriz:
                if (dadosOrigem.TipoAtuacao == TipoAtuacao.Aluno)
                    exibirVagas = true;
                else if (dadosOrigem.TipoAtuacao == TipoAtuacao.Ingressante)
                {
                    var ingressanteProcessoSeletivo = IngressanteDomainService.BuscarIngressanteProcessoSeletivo(seqPessoaAtuacao);
                    exibirVagas = !ingressanteProcessoSeletivo.ProcessoSeletivo.ReservaVaga;
                }
            }

            // Caso seja forçado para não exibir vagas....
            if (omitirVagas)
                exibirVagas = false;

            var registroItens = new List<SolicitacaoMatriculaTurmaAtividadeSituacaoVO>();

            // Caso tenha matriz, busca todos os seqTurmas que são da oferta do aluno para montar a legenda de pertence ou não ao plano de estudo
            List<long> seqsTurmasPertencemPlanoEstudo = new List<long>();
            if (seqMatrizCurricularOferta.HasValue && seqMatrizCurricularOferta != 0)
                seqsTurmasPertencemPlanoEstudo = TurmaDomainService.BuscarSequenciaisTurmaMatrizOfertaLegenda(seqMatrizCurricularOferta.GetValueOrDefault(), seqSolicitacaoMatricula);

            //Montar as legendas para turma e atividade
            dadosItens.ForEach(f =>
            {
                // Mapeia para o tipo certo
                var item = SMCMapperHelper.Create<SolicitacaoMatriculaTurmaAtividadeSituacaoVO>(f);

                // Busca as Divisoes da turma Turma
                item.DivisoesTurma = this.SearchProjectionByKey(f.Seq, p => p.DivisaoTurma.Turma.DivisoesTurma.Select(s => new DivisaoTurmaVO()
                {
                    Seq = s.Seq,
                    SeqDivisaoComponente = s.SeqDivisaoComponente,
                    NumeroDivisaoComponente = s.DivisaoComponente.Numero,
                    SeqLocalidade = (long?)s.SeqLocalidade,
                    DescricaoLocalidade = s.Localidade.Nome,
                    SeqOrigemAvaliacao = s.SeqOrigemAvaliacao,
                    SeqTurma = s.SeqTurma,
                    NumeroGrupo = s.NumeroGrupo,
                    QuantidadeVagas = s.QuantidadeVagas,
                    QuantidadeVagasOcupadas = (short?)s.QuantidadeVagasOcupadas,
                    QuantidadeVagasReservadas = (short?)s.QuantidadeVagasReservadas,
                    Situacao = f.Situacao,
                    Motivo = f.Motivo,

                    OrigemAvaliacao = new OrigemAvaliacaoVO()
                    {
                        Seq = s.OrigemAvaliacao.Seq,
                        SeqCriterioAprovacao = (long?)s.OrigemAvaliacao.SeqCriterioAprovacao,
                        QuantidadeGrupos = (short?)s.OrigemAvaliacao.QuantidadeGrupos,
                        QuantidadeProfessores = (short?)s.OrigemAvaliacao.QuantidadeProfessores,
                        ApurarFrequencia = (bool?)s.OrigemAvaliacao.ApurarFrequencia,
                        NotaMaxima = (short?)s.OrigemAvaliacao.NotaMaxima,
                        SeqEscalaApuracao = (long?)s.OrigemAvaliacao.SeqEscalaApuracao,
                    }
                }).ToList());

                item.SeqProgramaTurma = item.SeqProgramaTurma ?? f.SeqProgramaTurmaPrincipal;
                item.ProgramaTurma = item.ProgramaTurma ?? f.ProgramaTurmaPrincipal;
                item.DescricaoConfiguracaoComponenteTurma = item.DescricaoConfiguracaoComponenteTurma ?? f.DescricaoConfiguracaoComponenteTurmaPrincipal;

                //Verificar se a configuração de componente da turma pertence a matriz curricular oferta do ingressante
                if (seqMatrizCurricularOferta.HasValue && seqMatrizCurricularOferta != 0)
                {
                    //var verificaConfiguracao = MatrizCurricularOfertaDomainService.ValidarMatrizCurricularOfertaConfiguracaoComponente(seqMatrizCurricularOferta.Value, f.SeqConfiguracaoComponentePrincipal);
                    // Verifica se o seqTurma pertence aos seqs do plano de estudo do aluno
                    item.Pertence = seqsTurmasPertencemPlanoEstudo.Contains(f.SeqTurma.GetValueOrDefault()) ? TurmaOfertaMatricula.ComponentePertence : TurmaOfertaMatricula.ComponenteNaoPertence;
                }
                else
                    item.Pertence = TurmaOfertaMatricula.ComponenteNaoPertence;

                if (item.PertencePlanoEstudo.GetValueOrDefault())
                {
                    if (item.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado)
                        item.SituacaoPlanoEstudo = MatriculaPertencePlanoEstudo.NaoAlterado;
                    else
                        item.SituacaoPlanoEstudo = MatriculaPertencePlanoEstudo.Exclusao;
                }
                else
                    item.SituacaoPlanoEstudo = MatriculaPertencePlanoEstudo.Inclusao;

                item.SeqTurma = f.SeqTurma;

                registroItens.Add(item);
            });

            retorno.Turmas = registroItens.Where(w => w.SeqDivisaoTurma.HasValue).OrderBy(o => o.DescricaoConfiguracaoComponenteTurma).ToList();

            foreach (var item in retorno.Turmas)
            {
                var tiposComponenteNivel = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(item.SeqNivelEnsino, item.SeqTipoComponenteCurricular);

                item.TurmaMatriculaDivisoes = new List<TurmaMatriculaListarDetailVO>();

                var matriculaDivisao = new TurmaMatriculaListarDetailVO();

                if (tiposComponenteNivel != null)
                    item.TurmaDivisoesItem.FormatoCargaHoraria = tiposComponenteNivel.FormatoCargaHoraria;

                var divisaoItemDetalhe = item.DivisoesTurma.Where(w => w.SeqDivisaoComponente == item.TurmaDivisoesItem.SeqDivisaoComponente).FirstOrDefault();
                if (divisaoItemDetalhe != null)
                {
                    item.TurmaDivisoesItem.TurmaCodigoFormatado = item.CodigoFormatado;
                    item.TurmaDivisoesItem.NumeroDivisaoComponente = divisaoItemDetalhe.NumeroDivisaoComponente;
                    item.TurmaDivisoesItem.NumeroGrupo = divisaoItemDetalhe.NumeroGrupo;
                }

                matriculaDivisao.DivisaoTurmaDescricao = item.TurmaDivisoesItem.DescricaoFormatada;
                matriculaDivisao.DivisaoTurmaDescricaoSemNumero = item.TurmaDivisoesItem.DescricaoFormatadaSemNumero;
                matriculaDivisao.PermitirGrupo = item.TurmaDivisoesItem.PermitirGrupo;
                matriculaDivisao.SeqConfiguracaoComponente = item.TurmaDivisoesItem.SeqConfiguracaoComponente;
                matriculaDivisao.Seq = item.TurmaDivisoesItem.Seq;
                matriculaDivisao.SeqPessoaAtuacao = registroItens.FirstOrDefault().SeqPessoaAtuacao;
                matriculaDivisao.SituacaoPlanoEstudo = item.SituacaoPlanoEstudo;
                matriculaDivisao.Situacao = item.Situacao;
                matriculaDivisao.Motivo = item.Motivo;
                matriculaDivisao.SeqSituacaoItemMatricula = item.SeqSituacao;
                matriculaDivisao.PertencePlanoEstudo = item.PertencePlanoEstudo.GetValueOrDefault();

                matriculaDivisao.DivisoesTurmas = new List<SMCDatasourceItem>();

                var divisoesSelect = item.DivisoesTurma.Where(w => w.SeqDivisaoComponente == item.TurmaDivisoesItem.SeqDivisaoComponente)
                                                            .Select(s => new SMCDatasourceItem()
                                                            {
                                                                Seq = s.Seq,
                                                                Descricao = exibirVagas ?
                                                                $"{item.CodigoFormatado}.{s.NumeroDivisaoComponente}.{s.NumeroGrupo.ToString().PadLeft(3, '0')} - {s.QuantidadeVagasDisponiveis} vagas disponíveis" :
                                                                $"{item.CodigoFormatado}.{s.NumeroDivisaoComponente}.{s.NumeroGrupo.ToString().PadLeft(3, '0')}"
                                                            });

                matriculaDivisao.DivisoesTurmas.AddRange(divisoesSelect);
                matriculaDivisao.SeqDivisaoComponente = item.DivisoesTurma.FirstOrDefault().SeqDivisaoComponente;
                if (matriculaDivisao.DivisoesTurmas.Count == 1 && !item.SeqDivisaoTurma.HasValue)
                    matriculaDivisao.SeqDivisaoTurma = matriculaDivisao.DivisoesTurmas[0].Seq;
                else if (item.SeqDivisaoTurma.HasValue)
                    matriculaDivisao.SeqDivisaoTurma = item.SeqDivisaoTurma.Value;

                item.TurmaMatriculaDivisoes.Add(matriculaDivisao);
            }



            List<SolicitacaoMatriculaTurmaAtividadeSituacaoVO> retornoTurmas = new List<SolicitacaoMatriculaTurmaAtividadeSituacaoVO>();

            retorno.Turmas.GroupBy(g => g.SeqTurma).SMCForEach(f =>
            {
                var primeiro = f.First();
                primeiro.TurmaMatriculaDivisoes = f.SelectMany(s => s.TurmaMatriculaDivisoes).OrderBy(o => o.DivisaoTurmaDescricao).ToList();
                primeiro.ExibirEntidadeResponsavelTurma = exibirEntidadeResponsavelTurma && primeiro.SeqProgramaTurma != primeiro.SeqEntidadeResponsavel;
                retornoTurmas.Add(primeiro);
            });

            retorno.Turmas = retornoTurmas;

            if (desconsiderarEtapa)
                retorno.Atividades = registroItens.Where(w => !w.SeqDivisaoTurma.HasValue).OrderBy(o => o.DescricaoConfiguracaoComponente).ToList();
            else
                retorno.Atividades = registroItens.Where(w => !w.SeqDivisaoTurma.HasValue && w.SeqSituacao != null).OrderBy(o => o.DescricaoConfiguracaoComponente).ToList();

            if (retorno.Atividades.Count > 0)
            {
                var filtroComponentes = new ConfiguracaoComponenteFilterSpecification() { SeqConfiguracoesComponentes = retorno.Atividades.Select(s => s.SeqConfiguracaoComponente).ToArray() };
                var componentesDescricaoCompleta = ConfiguracaoComponenteDomainService.BuscarConfiguracoesComponentes(filtroComponentes);

                retorno.Atividades.ForEach(f => f.DescricaoConfiguracaoComponente = componentesDescricaoCompleta.FirstOrDefault(w => w.Seq == f.SeqConfiguracaoComponente)?.DescricaoFormatada);
            }

            // Necessário remover o filtro global porque existe chancela de secretaria diferente ao do aluno no caso de disciplina eletiva
            if (desativarFiltroDados)
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return retorno;
        }

        /// <summary>
        /// Busca o sequencial de acordo com a divisão da turma e a configuração do componente
        /// </summary>
        /// <param name="filtro">Filtro com sequencial da divisão da turma e da configuração do componente</param>
        /// <returns>Sequencial da solicitação matrícula item</returns>
        public long BuscarSequencialSolicitacaoMatriculaItem(SolicitacaoMatriculaItemFiltroVO filtro)
        {
            var spec = filtro.Transform<SolicitacaoMatriculaItemFilterSpecification>();

            var registro = this.SearchProjectionBySpecification(spec, p => p.Seq);

            return registro.FirstOrDefault();
        }

        /// <summary>
        /// Busca os sequencial de acordo com os filtros informados
        /// </summary>
        /// <param name="filtro">Filtro </param>
        /// <returns>Sequenciais da solicitação matrícula item</returns>
        public List<long> BuscarSequenciaisSolicitacoesMatriculaItem(SolicitacaoMatriculaItemFiltroVO filtro)
        {
            var spec = filtro.Transform<SolicitacaoMatriculaItemFilterSpecification>();

            var registro = this.SearchProjectionBySpecification(spec, p => p.Seq);

            return registro.ToList();
        }

        /// <summary>
        /// Lista os itens de atividades selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa</param>
        /// <param name="classificacaoFinal">Classificação do registro na tabela de histórico</param>
        /// <returns>Lista de sequenciais de atividades gravados pelo ingressante</returns>
        public List<SolicitacaoMatriculaItemVO> BuscarSolicitacaoMatriculaAtividadesItens(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long? seqProcessoEtapa, ClassificacaoSituacaoFinal? classificacaoFinal)
        {
            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                RegistroAtividade = true
            };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            string tokenProcessoEtapa = string.Empty;
            List<long> seqsSituacaoEtapa = new List<long>();
            if (seqProcessoEtapa.HasValue && seqProcessoEtapa.Value > 0)
            {
                tokenProcessoEtapa = ProcessoEtapaDomainService.BuscarTokenProcessoEtapa(seqProcessoEtapa.Value);

                var etapa = SGFHelper.BuscarEtapas(seqSolicitacaoMatricula);
                var etapaConfiguracao = SGFHelper.BuscarEtapa(seqSolicitacaoMatricula, etapa.Where(w => w.SeqProcessoEtapa == seqProcessoEtapa.Value).FirstOrDefault().SeqConfiguracaoEtapa);
                seqsSituacaoEtapa = etapaConfiguracao.Situacoes.Select(s => s.SeqSituacao).ToList();
            }

            if (!tokenProcessoEtapa.Contains(MatriculaTokens.CHANCELA))
                specSolicitacaoMatriculaItem.ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.FinalizadoComSucesso;

            //Quando for plano de estudo tem que exibir cancelado pelo solicitante e não editável
            if (tokenProcessoEtapa == MatriculaTokens.SOLICITACAO_ALTERACAO_PLANO_ESTUDOS || tokenProcessoEtapa == MatriculaTokens.SOLICITACAO_ALTERACAO_PLANO_ESTUDOS_DI)
            {
                specSolicitacaoMatriculaItem.SolicitacaoPlanoEstudoAlterar = true;
                specSolicitacaoMatriculaItem.ClassificacaoSituacaoFinal = null;
                specSolicitacaoMatriculaItem.ItemDoPlanoCanceladoPelaInstituicao = false;
            }
            else
                specSolicitacaoMatriculaItem.MotivoSituacaoMatriculaDiferente = MotivoSituacaoMatricula.PeloSolicitante;

            var registros = this.SearchProjectionBySpecification(specSolicitacaoMatriculaItem,
                p => new SolicitacaoMatriculaItemVO()
                {
                    Seq = p.Seq,
                    SeqSolicitacaoMatricula = p.SeqSolicitacaoMatricula,
                    SeqDivisaoTurma = p.SeqDivisaoTurma,
                    SeqConfiguracaoComponente = p.SeqConfiguracaoComponente,
                    Situacao = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.Descricao ?? string.Empty,
                    SituacaoEtapa = p.HistoricosSituacao.Where(h => seqProcessoEtapa.HasValue && h.SituacaoItemMatricula.SeqProcessoEtapa == seqProcessoEtapa && seqsSituacaoEtapa.Contains(h.SeqSituacaoItemMatricula)).OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.Descricao ?? string.Empty,
                    ClassificacaoSituacaoFinal = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal,
                    SituacaoInicial = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.SituacaoInicial,
                    SituacaoFinal = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.SituacaoFinal,
                    Motivo = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula,
                    MotivoEtapa = p.HistoricosSituacao.Where(h => seqProcessoEtapa.HasValue && h.SituacaoItemMatricula.SeqProcessoEtapa == seqProcessoEtapa && seqsSituacaoEtapa.Contains(h.SeqSituacaoItemMatricula)).OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula,
                    PertencePlanoEstudo = p.PertencePlanoEstudo,
                }).ToList();

            if (registros.Count > 0)
            {
                var filtro = new ConfiguracaoComponenteFilterSpecification() { SeqConfiguracoesComponentes = registros.Select(s => s.SeqConfiguracaoComponente).ToArray() };
                filtro.MaxResults = int.MaxValue;
                filtro.SetOrderBy(s => s.Descricao);

                InstituicaoNivelTipoVinculoAlunoVO dadosVinculo = new InstituicaoNivelTipoVinculoAlunoVO();
                PessoaAtuacaoDadosOrigemVO dadosOrigem = new PessoaAtuacaoDadosOrigemVO();

                dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);
                dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

                var listaRegistro = ConfiguracaoComponenteDomainService.BuscarConfiguracoesComponentes(filtro);

                foreach (var item in registros)
                {
                    var registroValida = listaRegistro.First(w => w.Seq == item.SeqConfiguracaoComponente);

                    ////TODO Task_24734 - Código comentado para o momento e assim que ocorrer acerto de dados será utlizado novamente
                    ////Verificar tipos de divisões com a definição de exigir orientação
                    //item.ObrigatorioOrientador = false;
                    //if (registroValida.DivisoesComponente.Any(a => a.GerarOrientacao))
                    //    item.ObrigatorioOrientador = OrientacaoPessoaAtuacaoDomainService.ValidarOrientacoesPessoaAtuacao(seqPessoaAtuacao, dadosVinculo.Seq);


                    var validacaoPre = RequisitoDomainService.ValidarPreRequisitos(seqPessoaAtuacao, registroValida.DivisoesComponente.Select(s => s.Seq), null, null, null);

                    item.PreRequisito = !validacaoPre.Valido;

                    item.DescricaoFormatada = registroValida.DescricaoFormatada;

                    item.DescricaoOrdenacao = registroValida.Descricao;

                    item.Credito = registroValida.ComponenteCurricularCredito;
                }
            }

            return registros.OrderBy(o => o.DescricaoOrdenacao).ToList();
        }

        /// <summary>
        /// Realizar o processamento de plano de estudo das solicitações de serviços listada
        /// </summary>
        /// <param name="processamento">Objeto de processamento</param>
        public void ProcessamentoPlanoEstudoServicoMatricula(ProcessamentoPlanoEstudoSATVO processamento)
        {
            var arraySeqs = Array.ConvertAll(processamento.SeqsSolicitacoesServicos.Split(';'), s => Int64.Parse(s));

            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification() { SeqsSolicitacoesServicos = arraySeqs };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            var registros = this.SearchBySpecification(specSolicitacaoMatriculaItem,
                  IncludesSolicitacaoMatriculaItem.DivisaoTurma
                | IncludesSolicitacaoMatriculaItem.HistoricosSituacao
                | IncludesSolicitacaoMatriculaItem.SolicitacaoMatricula
                | IncludesSolicitacaoMatriculaItem.SolicitacaoMatricula_PessoaAtuacao_Pessoa_DadosPessoais).ToList();

            var seqSituacaoFinalizadoComSucesso = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(processamento.SeqProcessoEtapa, null, true, ClassificacaoSituacaoFinal.FinalizadoComSucesso).Seq;
            var seqSituacaoFinalizadoSemSucesso = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(processamento.SeqProcessoEtapa, null, true, ClassificacaoSituacaoFinal.FinalizadoSemSucesso).Seq;
            decimal count = 0;
            decimal total = registros.Count;
            foreach (var item in registros)
            {
                //Iniciando a transacao
                using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
                {
                    try
                    {
                        /*
                        * Se o item de matrícula for do tipo de gestão "Turma",
                        * Verificar se existe requisito não cumprido(ver com Janice)
                        * Verificar se existe requisito cadastrado para o respectivo item e se a pessoa-atuação já cumpriu o requisito.
                        * Se existir e não tiver cumprido, atribuir ao histórico de situações do item, a situação configurada de acordo com a etapa,
                        * para ser a final e com o valor da "Classificação Situação" igual a "Finalizado sem sucesso".
                        * E ao motivo de situação do item, a situação “Exigência de requisitos não satisfeitos”.
                        */
                        var dadosIngressante = IngressanteDomainService.BuscarIngressanteMatrizOferta(item.SolicitacaoMatricula.SeqPessoaAtuacao);

                        if (dadosIngressante != null && dadosIngressante.SeqMatrizCurricularOferta.HasValue && item.SeqConfiguracaoComponente.HasValue)
                        {
                            var seqsDivisaoComponente = registros.Where(w => w.SeqSolicitacaoMatricula == item.SeqSolicitacaoMatricula)
                                                                        .Select(s => s.DivisaoTurma.SeqDivisaoComponente);

                            /*
                             * Atribuir ao histórico de situações do item, a situação configurada de acordo com a etapa
                             * em questão para ser a final e com o valor da "Classificação Situação" igual a “Finalizado sem sucesso”.
                             * E ao motivo de situação do item, a situação “Exigência de requisitos não satisfeitos”.
                             */
                            //var validacaoComponentes = seqsConfiguracoesSolicitacao.Select(s => new SolicitacaoMatriculaItemVO() { SeqConfiguracaoComponente = s }).ToList();

                            var validacao = RequisitoDomainService.ValidarPreRequisitos(item.SolicitacaoMatricula.SeqPessoaAtuacao, seqsDivisaoComponente, null, null, null);

                            if (!validacao.Valido)
                                SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoFinalizadoSemSucesso, MotivoSituacaoMatricula.ExigenciaRequisitosNaoSatisfeitos);


                            //var validacaoPre = RequisitoDomainService.ValidarPreRequisitoMatrizOferta(item.SolicitacaoMatricula.SeqPessoaAtuacao, validacaoComponentes, null, item.SeqConfiguracaoComponente.Value);

                            //if (!validacaoPre.valido)
                            //    SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoFinalizadoSemSucesso, MotivoSituacaoMatricula.ExigenciaRequisitosNaoSatisfeitos);

                            //var validacaoCo = RequisitoDomainService.ValidarCoRequisitoMatrizOferta(item.SolicitacaoMatricula.SeqPessoaAtuacao, validacaoComponentes, null, item.SeqConfiguracaoComponente.Value, null, item.SeqSolicitacaoMatricula);

                            //if (!validacaoCo.valido)
                            //    SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoFinalizadoSemSucesso, MotivoSituacaoMatricula.ExigenciaRequisitosNaoSatisfeitos);
                        }

                        /*
                        * Verificar se o item é “Atividade Acadêmica”, de acordo com a regra:
                        * Atribuir ao histórico de situações do item, a situação configurada de acordo com a etapa,
                        * para ser a final e com o valor da "Classificação Situação" igual a "Finalizado com sucesso".
                        */
                        if (!item.SeqDivisaoTurma.HasValue)
                        {
                            SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoFinalizadoComSucesso, null);

                            // Commit
                            transacao.Commit();
                            continue;
                        }

                        /*
                        * Verificar se existe vaga na Turma, de acordo com a regra:
                        * Subtrair da quantidade de vagas da divisão, o somatório da quantidade de vagas reservadas com a quantidade de vagas ocupadas
                        * [qtd_vagas - (qtd_vagas_reservadas + qtd_vagas_ocupadas)].
                        */
                        if (item.SeqDivisaoTurma.HasValue)
                        {
                            var divisaoAtual = DivisaoTurmaDomainService.BuscarDivisaoTurmaQuantidades(item.SeqDivisaoTurma.Value);
                            var quantidadeVagas = divisaoAtual.QuantidadeVagas;
                            var quantidadeVagasOcupadas = divisaoAtual.QuantidadeVagasOcupadas;
                            var quantidadeVagasReservadas = divisaoAtual.QuantidadeVagasReservadas;

                            //Se o resultado dessa subtração for um valor maior ou igual a 0:
                            if ((quantidadeVagas - (quantidadeVagasReservadas + quantidadeVagasOcupadas)) >= 0)
                            {
                                //Verificar se o vínculo da pessoa-atuação foi parametrizado para exigir oferta de matriz:
                                var dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(item.SolicitacaoMatricula.SeqPessoaAtuacao);

                                if (dadosVinculo.ExigeOfertaMatrizCurricular.GetValueOrDefault() == true && dadosIngressante != null)
                                {
                                    var seqTurmaConfiguracaoPrincipal = item.DivisaoTurma.Turma.ConfiguracoesComponente.Where(w => w.Principal).Select(s => s.Seq).FirstOrDefault();

                                    /*
                                     * Verificar se a quantidade de vagas ocupadas na matriz da oferta encontrada é menor que a quantidade de vagas reservadas na matriz,
                                     * de acordo com a oferta de matriz associada à pessoa - atuação em questão
                                     */

                                    var restricaoMatriz = RestricaoTurmaMatrizDomainService.BuscarRestricaoTurmaMatrizPorTurmaConfiguracaoMatriz(seqTurmaConfiguracaoPrincipal, dadosIngressante.SeqMatrizCurricularOferta);

                                    if (restricaoMatriz.QuantidadeVagasOcupadas < restricaoMatriz.QuantidadeVagasReservadas)
                                    {
                                        /*
                                         *  Atribuir ao histórico de situações do item, a situação configurada de acordo com a etapa
                                         *  em questão para ser a final e com o valor da "Classificação Situação" igual a “Finalizado com sucesso”.
                                         *  Salvar no campo quantidade de vaga ocupada na matriz o valor atual do campo +1.
                                         *  Salvar no campo quantidade de vaga ocupada da divisão em questão o valor atual do campo +1.
                                         */

                                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoFinalizadoComSucesso, null);

                                        var vagasAtualizadasOferta = restricaoMatriz.QuantidadeVagasOcupadas + 1;
                                        RestricaoTurmaMatrizDomainService.AtualizarQuantidadeVagasOculpadas(restricaoMatriz.Seq, (short)vagasAtualizadasOferta);

                                        var vagasAtualizadasDivisao = quantidadeVagasOcupadas + 1;
                                        DivisaoTurmaDomainService.AtualizarQuantidadeVagasOcupadas(item.SeqDivisaoTurma.Value, (short)vagasAtualizadasDivisao);

                                        // Commit
                                        transacao.Commit();
                                        continue;
                                    }
                                    else
                                    {
                                        /*
                                         * Atribuir ao histórico de situações do item, a situação configurada de acordo com a etapa
                                         * em questão para ser a final e com o valor da "Classificação Situação" igual a “Finalizado sem sucesso”.
                                         * E ao motivo de situação do item, a situação “Vagas excedidas”.
                                         */
                                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoFinalizadoSemSucesso, MotivoSituacaoMatricula.VagasExcedidas);

                                        // Commit
                                        transacao.Commit();
                                        continue;
                                    }
                                }
                                else
                                {
                                    /*
                                     * Atribuir ao histórico de situações do item, a situação configurada de acordo com a etapa em questão para ser a final
                                     * e com o valor da "Classificação Situação" igual a “Finalizado com sucesso”.
                                     * Salvar no campo quantidade de vaga ocupada da divisão o valor atual do campo + 1.
                                     */
                                    SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoFinalizadoComSucesso, null);

                                    var vagasAtualizadasDivisao = quantidadeVagasOcupadas + 1;
                                    DivisaoTurmaDomainService.AtualizarQuantidadeVagasOcupadas(item.SeqDivisaoTurma.Value, (short)vagasAtualizadasDivisao);

                                    // Commit
                                    transacao.Commit();
                                    continue;
                                }
                            }
                            else
                            {
                                /*
                                 * Atribuir ao histórico de situações do item, a situação configurada de acordo com a etapa
                                 * em questão para ser a final e com o valor da "Classificação Situação" igual a “Finalizado sem sucesso”.
                                 * E ao motivo de situação do item, a situação “Vagas excedidas”.
                                 */

                                SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoFinalizadoSemSucesso, MotivoSituacaoMatricula.VagasExcedidas);

                                // Commit
                                transacao.Commit();
                                continue;
                            }
                        }

                        // Commit
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Exibe o erro no log do job
                        var schedulerInfo = new SMCSchedulerHistoryModel()
                        {
                            SeqSchedulerHistory = processamento.SeqHistoricoAgendamento,
                            Log = ex.Message,
                            OriginID = item.Seq,
                            OriginName = item.SolicitacaoMatricula.PessoaAtuacao.DadosPessoais.Nome,
                        };
                        Scheduler.LogError(schedulerInfo);
                    }
                    finally
                    {
                        count += 1;
                        var progress = Convert.ToInt16(((decimal)(count / total)) * 100);
                        Scheduler.Progress(new SMCSchedulerHistoryModel()
                        {
                            SeqSchedulerHistory = processamento.SeqHistoricoAgendamento,
                            DateTime = DateTime.Now,
                            Progress = progress
                        });
                        var schedulerInfo = new SMCSchedulerHistoryModel()
                        {
                            SeqSchedulerHistory = processamento.SeqHistoricoAgendamento,
                            Log = string.Format("Processando matricula do {0}", item.Seq),
                            OriginID = item.Seq,
                            OriginName = item.SolicitacaoMatricula.PessoaAtuacao.DadosPessoais?.Nome,
                        };
                        Scheduler.LogInfo(schedulerInfo);
                    }
                }
            }

            //Agrupar solicitação de matricula para finalizar etapa e iniciar nova etapa
            var solicitacoesMatricula = registros.Select(s => s.SeqSolicitacaoMatricula).Distinct();

            foreach (var seqSolicitacao in solicitacoesMatricula)
            {
                var configuracaoEtapa = SolicitacaoMatriculaDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacao), x => x.GrupoEscalonamento.Itens.Select(y =>
                new { SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.Escalonamento.SeqProcessoEtapa).Seq }).FirstOrDefault()).SeqConfiguracaoEtapa;

                SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(seqSolicitacao, configuracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);
            }
        }

        /// <summary>
        /// Realizar o processamento de vagas de acordo com as turmas selecionadas para o ingressante
        /// </summary>
        /// <param name="seqSolicitacaoMatriculaItem">Sequenciais das solicitações de matrícula item</param>
        /// <param name="itemDescricao">Descricao completa do item que vincula na mesangem de erro da selecao</param>
        /// <returns>Texto com o erro da validação</returns>
        public string ProcessarVagaTurmaAtividadeIngressante(long seqSolicitacaoMatriculaItem, string itemDescricao, bool desabilitarFiltro = false)
        {
            if (desabilitarFiltro)
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var dadosSolicitacaoMatriculaItem = this.SearchProjectionByKey(seqSolicitacaoMatriculaItem, x => new
            {
                x.SolicitacaoMatricula.SeqPessoaAtuacao,
                x.SeqDivisaoTurma,
            });
            string ret = string.Empty;

            if (dadosSolicitacaoMatriculaItem != null && dadosSolicitacaoMatriculaItem.SeqDivisaoTurma.HasValue)
                ret = DivisaoTurmaDomainService.ProcessarVagaTurma(dadosSolicitacaoMatriculaItem.SeqPessoaAtuacao, dadosSolicitacaoMatriculaItem.SeqDivisaoTurma.Value, itemDescricao, desabilitarFiltro);

            if (desabilitarFiltro)
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return ret;
        }

        /// <summary>
        /// Realizar a liberação de vagas de acordo com as turmas selecionadas para o ingressante
        /// </summary>
        /// <param name="seqSolicitacaoMatriculaItem">Sequenciais das solicitações de matrícula item</param>
        public void LiberarVagaTurmaAtividadeIngressante(long seqSolicitacaoMatriculaItem, bool desabilitarFiltro = false)
        {
            if (desabilitarFiltro)
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var dadosSolicitacaoMatriculaItem = this.SearchProjectionByKey(seqSolicitacaoMatriculaItem, x => new
            {
                SeqPessoaAtuacao = x.SolicitacaoMatricula.SeqPessoaAtuacao,
                SeqDivisaoTurma = x.SeqDivisaoTurma,
            });

            if (dadosSolicitacaoMatriculaItem != null && dadosSolicitacaoMatriculaItem.SeqDivisaoTurma.HasValue)
                DivisaoTurmaDomainService.LiberarVagaTurma(dadosSolicitacaoMatriculaItem.SeqPessoaAtuacao, dadosSolicitacaoMatriculaItem.SeqDivisaoTurma.Value, desabilitarFiltro);

            if (desabilitarFiltro)
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
        }

        public string ValidarVagaTurmaAtividadeIngressante(long seqPessoaAtuacao, string itemDescricao, long? seqDivisaoTurma, bool desabilitarFiltro = false)
        {
            if (desabilitarFiltro)
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            string ret = string.Empty;

            if (seqPessoaAtuacao != 0 && seqDivisaoTurma.HasValue)
            {
                IngressanteTurmaVO dadosIngressante = null;
                var tipoAtuacao = PessoaAtuacaoDomainService.BuscarTipoAtuacaoPessoaAtuacao(seqPessoaAtuacao, desabilitarFiltro);
                var dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao, desabilitarFiltro);

                if (tipoAtuacao == TipoAtuacao.Ingressante)
                    dadosIngressante = IngressanteDomainService.BuscarIngressanteMatrizOferta(seqPessoaAtuacao, desabilitarFiltro);

                if (desabilitarFiltro)
                    this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

                if (dadosVinculo.ExigeOfertaMatrizCurricular.GetValueOrDefault() == false && dadosIngressante != null)
                {
                    var campanhaOfertaAtual = ProcessoSeletivoOfertaDomainService.BuscarProcessoSeletivoOfertaQuantidades(dadosIngressante.SeqProcessoSeletivo, seqDivisaoTurma.Value);
                    if (campanhaOfertaAtual != null)
                    {
                        if (campanhaOfertaAtual.ProcessoReservaVaga == true)
                        {
                            short quantidadeVagasCampanha = campanhaOfertaAtual.QuantidadeVagas;
                            short quantidadeVagasCampanhaOcupadas = campanhaOfertaAtual.QuantidadeVagasOcupadas;

                            if ((quantidadeVagasCampanhaOcupadas >= quantidadeVagasCampanha))
                            {
                                return $" {itemDescricao} - Motivo: {MotivoSituacaoMatricula.VagasExcedidas.SMCGetDescription()}";
                            }
                        }
                    }
                    else
                    {
                        return $" {itemDescricao}  - Motivo: {MotivoSituacaoMatricula.ItemCancelado.SMCGetDescription()}";
                    }
                }

                var divisaoAtual = DivisaoTurmaDomainService.BuscarDivisaoTurmaQuantidades(seqDivisaoTurma.Value);
                short quantidadeVagas = divisaoAtual.QuantidadeVagas;
                short quantidadeVagasOcupadas = divisaoAtual.QuantidadeVagasOcupadas.GetValueOrDefault();
                short quantidadeVagasReservadas = divisaoAtual.QuantidadeVagasReservadas.GetValueOrDefault();

                if ((quantidadeVagasReservadas + quantidadeVagasOcupadas) >= quantidadeVagas)
                {
                    return $" {itemDescricao} - Motivo: {MotivoSituacaoMatricula.VagasExcedidas.SMCGetDescription()}";
                }
            }
            return ret;
        }

        public void DesistenciaServicoMatricula(long seqSolicitacaoServico, long seqProcessoEtapa, long seqPessoaAtuacao)
        {
            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification() { SeqSolicitacaoMatricula = seqSolicitacaoServico };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            var registros = SearchBySpecification(specSolicitacaoMatriculaItem, IncludesSolicitacaoMatriculaItem.DivisaoTurma).ToList();

            var seqSituacaoCancelado = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(seqProcessoEtapa, true, true, ClassificacaoSituacaoFinal.Cancelado).Seq;

            foreach (var item in registros)
            {
                //Iniciando a transacao
                using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
                {
                    /*
                    * Verificar se o item é “Atividade Acadêmica”, de acordo com a regra:
                    * Atribuir ao histórico de situações do item, a situação configurada de acordo com a etapa,
                    * para ser a final e com o valor da "Classificação Situação" igual a "Finalizado com sucesso".
                    */
                    if (!item.SeqDivisaoTurma.HasValue)
                    {
                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoCancelado, null);

                        // Commit
                        transacao.Commit();
                        continue;
                    }

                    if (item.SeqDivisaoTurma.HasValue)
                    {
                        var divisaoAtual = DivisaoTurmaDomainService.BuscarDivisaoTurmaQuantidades(item.SeqDivisaoTurma.Value);
                        var quantidadeVagas = divisaoAtual.QuantidadeVagas;
                        var quantidadeVagasOcupadas = divisaoAtual.QuantidadeVagasOcupadas;
                        var quantidadeVagasReservadas = divisaoAtual.QuantidadeVagasReservadas;

                        //Verificar se o vínculo da pessoa-atuação foi parametrizado para exigir oferta de matriz:
                        var dadosIngressante = IngressanteDomainService.BuscarIngressanteMatrizOferta(seqPessoaAtuacao);
                        var dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);

                        //Verificar regra com Maira
                        if (dadosVinculo.ExigeOfertaMatrizCurricular.GetValueOrDefault() == true && dadosIngressante != null)
                        {
                            var seqTurmaConfiguracaoPrincipal = item.DivisaoTurma.Turma.ConfiguracoesComponente.Where(w => w.Principal).Select(s => s.Seq).FirstOrDefault();

                            /*
                             * Verificar se a quantidade de vagas ocupadas na matriz da oferta encontrada é menor que a quantidade de vagas reservadas na matriz,
                             * de acordo com a oferta de matriz associada à pessoa - atuação em questão
                             */
                            var restricaoMatriz = RestricaoTurmaMatrizDomainService.BuscarRestricaoTurmaMatrizPorTurmaConfiguracaoMatriz(seqTurmaConfiguracaoPrincipal, dadosIngressante.SeqMatrizCurricularOferta);

                            if (restricaoMatriz.QuantidadeVagasOcupadas > 0)
                            {
                                /*
                                 *  Atribuir ao histórico de situações do item, a situação configurada de acordo com a etapa
                                 *  em questão para ser a final e com o valor da "Classificação Situação" igual a “Finalizado com sucesso”.
                                 *  Salvar no campo quantidade de vaga ocupada na matriz o valor atual do campo +1.
                                 *  Salvar no campo quantidade de vaga ocupada da divisão em questão o valor atual do campo +1.
                                 */

                                SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoCancelado, null);

                                var vagasAtualizadasOferta = restricaoMatriz.QuantidadeVagasOcupadas - 1;
                                RestricaoTurmaMatrizDomainService.AtualizarQuantidadeVagasOculpadas(restricaoMatriz.Seq, (short)vagasAtualizadasOferta);

                                var vagasAtualizadasDivisao = quantidadeVagasOcupadas - 1;
                                DivisaoTurmaDomainService.AtualizarQuantidadeVagasOcupadas(item.SeqDivisaoTurma.Value, (short)vagasAtualizadasDivisao);

                                // Commit
                                transacao.Commit();
                                continue;
                            }
                            else
                            {
                                /*
                                 * Atribuir ao histórico de situações do item, a situação configurada de acordo com a etapa
                                 * em questão para ser a final e com o valor da "Classificação Situação" igual a “Finalizado sem sucesso”.
                                 * E ao motivo de situação do item, a situação “Vagas excedidas”.
                                 */
                                SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoCancelado, MotivoSituacaoMatricula.ItemCancelado);

                                // Commit
                                transacao.Commit();
                                continue;
                            }
                        }
                        else
                        {
                            /*
                             * Atribuir ao histórico de situações do item, a situação configurada de acordo com a etapa em questão para ser a final
                             * e com o valor da "Classificação Situação" igual a “Finalizado com sucesso”.
                             * Salvar no campo quantidade de vaga ocupada da divisão o valor atual do campo + 1.
                             */
                            SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, seqSituacaoCancelado, null);

                            var vagasAtualizadasDivisao = quantidadeVagasOcupadas - 1;
                            DivisaoTurmaDomainService.AtualizarQuantidadeVagasOcupadas(item.SeqDivisaoTurma.Value, (short)vagasAtualizadasDivisao);

                            // Commit
                            transacao.Commit();
                            continue;
                        }
                    }

                    // Commit
                    transacao.Commit();
                }
            }
        }

        /// <summary>
        /// Valida os pré e co-requisitos independente se é turma ou atividade
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="validaTipoGestao">Valida se os componentes tem o mesmo tipo de gestão ex turma com pré/có-requisito atividade</param>
        /// <returns>Valido = true quando não tem nada impeditivo para cadastro do componente</returns>
        public (bool valido, List<string> mensagemErro) ValidaSolicitacaoMatriculaItemPreCoRequisito(long seqSolicitacaoMatricula, long seqPessoaAtuacao, TipoGestaoDivisaoComponente? validaTipoGestao = null)
        {
            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                ClassificacaoSituacoesFinais = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.FinalizadoComSucesso, ClassificacaoSituacaoFinal.NaoAlterado },
                //PertencePlanoEstudo = false -- Validar todos os itens independente se ja estava no plano BUG 29468 e 29492
            };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            var registros = this.SearchProjectionBySpecification(specSolicitacaoMatriculaItem,
                                                                 p => new SolicitacaoMatriculaItemVO()
                                                                 {
                                                                     Seq = p.Seq,
                                                                     SeqDivisaoTurma = p.SeqDivisaoTurma,
                                                                     SeqTurma = p.DivisaoTurma.SeqTurma,
                                                                     SeqConfiguracaoComponente = p.SeqConfiguracaoComponente,
                                                                     SeqsConfiguracoesComponentes = p.DivisaoTurma.Turma.ConfiguracoesComponente.Select(s => s.SeqConfiguracaoComponente).ToList(),
                                                                     SeqDivisaoComponente = p.DivisaoTurma.SeqDivisaoComponente,
                                                                 }).ToList();

            var validacao = RequisitoDomainService.ValidarPreRequisitos(seqPessoaAtuacao, registros.Select(s => s.SeqDivisaoComponente.GetValueOrDefault()), null, validaTipoGestao, seqSolicitacaoMatricula);

            return validacao;
        }

        /// <summary>
        /// Valida os co-requisitos independente se é turma ou atividade
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="validaTipoGestao">Valida se os componentes tem o mesmo tipo de gestão ex turma com pré/có-requisito atividade</param>
        /// <returns>Valido = true quando não tem nada impeditivo para cadastro do componente</returns>
        public (bool valido, TipoRequisito tipo, List<string> mensagemErro) ValidaSolicitacaoMatriculaItemCoRequisito(long seqSolicitacaoMatricula, long seqPessoaAtuacao, TipoGestaoDivisaoComponente? validaTipoGestao = null, long? SeqProcessoEtapa = null)
        {
            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                ClassificacaoSituacoesFinais = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.FinalizadoComSucesso, ClassificacaoSituacaoFinal.NaoAlterado },
                //PertencePlanoEstudo = false -- Validar todos os itens independente se ja estava no plano BUG 29468 e 29492
            };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            var registros = this.SearchProjectionBySpecification(specSolicitacaoMatriculaItem,
                                                              p => new SolicitacaoMatriculaItemVO()
                                                              {
                                                                  Seq = p.Seq,
                                                                  SeqDivisaoTurma = p.SeqDivisaoTurma,
                                                                  SeqTurma = p.DivisaoTurma.SeqTurma,
                                                                  SeqConfiguracaoComponente = p.SeqConfiguracaoComponente,
                                                                  SeqsConfiguracoesComponentes = p.DivisaoTurma.Turma.ConfiguracoesComponente.Select(s => s.SeqConfiguracaoComponente).ToList(),
                                                                  ClassificacaoSituacaoFinal = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal,
                                                                  PossuiEtapaExclusao = p.HistoricosSituacao.Any(f => f.SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado
                                                                  && f.SituacaoItemMatricula.Descricao.Contains("exclusão")
                                                                  && f.SituacaoItemMatricula.ProcessoEtapa.Token.Contains("CHANCELA"))
                                                              }).ToList();
            //Remove quando chancelamos um exclusão
            registros = registros.Where(w => w.PossuiEtapaExclusao != true || w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado).ToList();
            var validacaoCo = RequisitoDomainService.ValidarPreRequisitos(seqPessoaAtuacao, null, registros.Select(s => s.SeqConfiguracaoComponente.GetValueOrDefault()), validaTipoGestao, seqSolicitacaoMatricula, SeqProcessoEtapa);

            return (validacaoCo.Valido, TipoRequisito.CoRequisito, validacaoCo.MensagensErro);
        }

        /// <summary>
        /// Valida se existe duas turmas de mesmo componente curricular selecionada
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="chancela">Utilizado na chancela para validar o indeferimento de um item excluído</param>
        /// <param name="desabilitarFiltro">Desabilita o filtro de HIERARQUIA_ENTIDADE_ORGANIZACIONAL</param>
        /// <returns>Valido = true quando não tem nada impeditivo para cadastro do componente</returns>
        public (bool valido, List<string> mensagemErro) ValidaSolicitacaoMatriculaItemTurmaDuplicada(long seqSolicitacaoMatricula, bool chancela = false, bool desabilitarFiltro = false)
        {
            if (desabilitarFiltro)
            {
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            var registros = new List<TurmaDuplicadaVO>();

            //Buscar turmas não alteradas do plano
            var specTurmaNaoAlteradaPlano = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                ExibirTurma = true,
                PertencePlanoEstudo = true,
                ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.NaoAlterado,
            };
            specTurmaNaoAlteradaPlano.MaxResults = int.MaxValue;

            var registrosNaoAlterado = this.SearchProjectionBySpecification(specTurmaNaoAlteradaPlano, p => new TurmaDuplicadaVO()
            {
                Codigo = p.DivisaoTurma.Turma.Codigo,
                Numero = p.DivisaoTurma.Turma.Numero,
                Descricao = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).Descricao,
                SeqTurma = p.DivisaoTurma.SeqTurma
            }).ToList();

            if (registrosNaoAlterado != null)
                registros.AddRange(registrosNaoAlterado);

            //Buscar turmas incluidas fora do plano
            var specTurmaIncluidas = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                ExibirTurma = true,
                PertencePlanoEstudo = false,
                ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.FinalizadoComSucesso,
            };
            specTurmaIncluidas.MaxResults = int.MaxValue;

            var registrosIncluido = this.SearchProjectionBySpecification(specTurmaIncluidas, p => new TurmaDuplicadaVO()
            {
                Codigo = p.DivisaoTurma.Turma.Codigo,
                Numero = p.DivisaoTurma.Turma.Numero,
                Descricao = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).Descricao,
                SeqTurma = p.DivisaoTurma.SeqTurma
            }).ToList();

            if (registrosIncluido != null)
                registros.AddRange(registrosIncluido);

            //Validação feita na chancela pois é quando criado o plano de estudo
            if (chancela)
            {
                //Buscar turmas indeferidas que pertencem ao plano de estudos
                var specTurmaExcluidaIndeferida = new SolicitacaoMatriculaItemFilterSpecification()
                {
                    SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                    ExibirTurma = true,
                    PertencePlanoEstudo = true,
                    ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.FinalizadoSemSucesso,
                };
                specTurmaExcluidaIndeferida.MaxResults = int.MaxValue;

                var registrosExcluidoIndeferido = this.SearchProjectionBySpecification(specTurmaExcluidaIndeferida, p => new TurmaDuplicadaVO()
                {
                    Codigo = p.DivisaoTurma.Turma.Codigo,
                    Numero = p.DivisaoTurma.Turma.Numero,
                    Descricao = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).Descricao,
                    SeqTurma = p.DivisaoTurma.SeqTurma
                }).ToList();

                if (registrosExcluidoIndeferido != null)
                    registros.AddRange(registrosExcluidoIndeferido);
            }

            if (desabilitarFiltro)
            {
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            var turmas = registros.GroupBy(g => g.Codigo).Where(w => w.Count() > 1 && w.GroupBy(t => t.SeqTurma).Count() > 1).SelectMany(s => s.Select(t => $"- {t.Codigo}.{t.Numero} - {t.Descricao}"));

            if (turmas != null && turmas.Count() > 0)
            {
                var mensagem = turmas.Select(s => s).ToList();
                return (false, mensagem);
            }

            return (true, new List<string>());
        }

        /// <summary>
        /// Recupera os planos de estudo item do aluno e grava na solicitação matricula item, com histórico de "Não Alterado"
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        public void ConverterPlanoEstudoItemEmSolicitacaoMatriculaItem(long seqSolicitacaoMatricula, long seqProcessoEtapa, long seqPessoaAtuacao)
        {
            // Recupera o ciclo letivo da solicitação
            var seqCicloLetivo = SolicitacaoServicoDomainService.SearchProjectionByKey(seqSolicitacaoMatricula, x => x.ConfiguracaoProcesso.Processo.SeqCicloLetivo);

            var specItens = new SolicitacaoMatriculaItemFilterSpecification() { SeqSolicitacaoMatricula = seqSolicitacaoMatricula };

            //Verifica se já existe itens na solicitação de matricula, senão copia o plano de estudo
            if (this.Count(specItens) == 0)
            {
                // Recupera o último planos de estudo ATUAL do aluno ordenado pelo ciclo letivo desc
                var filtroPlanoAtual = new PlanoEstudoFilterSpecification() { SeqAluno = seqPessoaAtuacao, Atual = true, SeqCicloLetivo = seqCicloLetivo };
                filtroPlanoAtual.SetOrderByDescending(x => x.DataInclusao);
                var planos = PlanoEstudoDomainService.SearchBySpecification(filtroPlanoAtual, IncludesPlanoEstudo.Itens).ToList();
                var planoEstudoAtual = planos.FirstOrDefault();

                // Caso tenha mais de um plano de estudo no mesmo ciclo como atual desativa os mais antigos deixando apenas o ultimo cadastrado
                foreach (var item in planos.Where(w => w.Seq != planoEstudoAtual.Seq))
                {
                    item.Atual = false;
                    PlanoEstudoDomainService.SaveEntity(item);
                }

                var specPlanoEstudo = new PlanoEstudoItemFilterSpecification() { SeqAluno = seqPessoaAtuacao, PlanoEstudoAtual = true, SeqCicloLetivo = seqCicloLetivo };

                var planoEstudoItens = PlanoEstudoItemDomainService.SearchBySpecification(specPlanoEstudo);

                if (planoEstudoItens != null)
                {
                    //Recupera a situação de não alterado para os itens do plano de estudo
                    var situacaoItemNaoAlterado = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(seqProcessoEtapa, true, null, ClassificacaoSituacaoFinal.NaoAlterado);

                    //Converte o plano de estudo item em solicitação de matricula item
                    var matriculaItens = planoEstudoItens.Select(s => new SolicitacaoMatriculaItem()
                    {
                        SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                        SeqDivisaoTurma = s.SeqDivisaoTurma,
                        SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                        PertencePlanoEstudo = true,
                    }).ToList();

                    //Configura a situação de não alterado em todos os itens do plano de estudo
                    matriculaItens.ForEach(f =>
                    {
                        f.HistoricosSituacao = new List<SolicitacaoMatriculaItemHistoricoSituacao>();
                        f.HistoricosSituacao.Add(new SolicitacaoMatriculaItemHistoricoSituacao() { SeqSituacaoItemMatricula = situacaoItemNaoAlterado.Seq });
                    });

                    //Grava a lista de itens
                    this.BulkSaveEntity(matriculaItens);
                }
            }
        }

        /// <summary>
        /// Verifica se existe algum registro que pertence ao plano de estudo e defini um alteração
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir item com plano de estudo para aluno e retorna false para ingressante</returns>
        public bool VerificarSolicitacaoMatriculaItemExisteRegistrosPlanoEstudo(long? seqSolicitacaoMatricula)
        {
            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                PertencePlanoEstudo = true,
            };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            return this.Count(specSolicitacaoMatriculaItem) > 0;
        }

        /// <summary>
        /// Lista os itens da solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <returns>Lista de itens da solicitacao de matricula</returns>
        public List<SolicitacaoMatriculaItemVO> BuscarSolicitacaoMatriculaItensPlano(long seqSolicitacaoMatricula)
        {
            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula
            };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            var registros = this.SearchProjectionBySpecification(specSolicitacaoMatriculaItem,
                                                                  p => new SolicitacaoMatriculaItemVO()
                                                                  {
                                                                      Seq = p.Seq,
                                                                      SeqSolicitacaoMatricula = p.SeqSolicitacaoMatricula,
                                                                      SeqDivisaoTurma = p.SeqDivisaoTurma,
                                                                      SeqTurma = p.DivisaoTurma.SeqTurma,
                                                                      SeqConfiguracaoComponente = p.SeqConfiguracaoComponente,
                                                                      SeqComponenteCurricular = p.ConfiguracaoComponente.SeqComponenteCurricular,
                                                                      ClassificacaoSituacaoFinal = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault() != null ?
                                                                                 p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal :
                                                                                 null,
                                                                      Motivo = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula,
                                                                      PertencePlanoEstudo = p.PertencePlanoEstudo,
                                                                      GeraOrientacao = (bool?)p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.GeraOrientacao,
                                                                      SeqTipoOrientacao = (long?)p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.SeqTipoOrientacao,
                                                                      TipoParticipacaoOrientacao = p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.TipoParticipacaoOrientacao,
                                                                      SeqDivisaoComponente = p.DivisaoTurma.SeqDivisaoComponente
                                                                  }).ToList();
            return registros;
        }

        /// <summary>
        /// Recupera todas as solicitações de matrícula item que tem divisões da turma e atualiza o histórico para cancelado
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        public void AlterarSolicitacaoMatriculaItemTurmaCancelada(long seqTurma)
        {
            var specTurma = new SolicitacaoMatriculaItemFilterSpecification() { SeqTurma = seqTurma, ExibirTurma = true, ClassificacaoSituacoesFinaisDiferentes = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.Cancelado } };

            var solicitacaoTurma = this.SearchBySpecification(specTurma, IncludesSolicitacaoMatriculaItem.HistoricosSituacao_SituacaoItemMatricula).ToList();

            foreach (var item in solicitacaoTurma)
            {
                var processoEtapa = item.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault() != null ?
                                    item.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.SeqProcessoEtapa :
                                    1;

                var situacaoCancelado = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(processoEtapa, false, true, ClassificacaoSituacaoFinal.Cancelado);

                SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, situacaoCancelado.Seq, MotivoSituacaoMatricula.PelaInstituicao);
            }
        }

        /// <summary>
        /// Verifica se o item da solicitação pertence ao plano de estudo do aluno
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de matricula item</param>
        /// <returns>Retorna a flag se pertence ao plano de estudo</returns>
        public bool VerificarItemPertencePlanoDeEstudo(long seq)
        {
            var pertence = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatriculaItem>(seq), p => p.PertencePlanoEstudo);

            return pertence;
        }

        /// <summary>
        /// Verifica se existe algum item da solicitação com situação mais atual da primeira etapa com classificação diferente da situação mais atual da última etapa.
        /// ou se existe algum item da solicitação que não possui situação da última etapa e a situação mais atual da primeira etapa deste item for diferente de “Cancelada”.
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existe alguma das situações descritas</returns>
        public bool VerificarSolicitacaoMatriculaItemRegistroDomumentoVia(long seqSolicitacaoMatricula, long seqPessoaAtuacao)
        {
            //Ingressante cujo vinculo foi parametrizado por instituição nível para não exigir curso. Não exibir o comando
            PessoaAtuacaoDadosOrigemVO dadosOrigem = new PessoaAtuacaoDadosOrigemVO();

            dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            if (dadosOrigem.TipoAtuacao != TipoAtuacao.Ingressante)
                return true;

            //Recuperar o vinculo do ingressante para verificar se exige curso
            InstituicaoNivelTipoVinculoAlunoVO dadosVinculo = new InstituicaoNivelTipoVinculoAlunoVO();
            dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);

            if (dadosVinculo.ExigeCurso.GetValueOrDefault())
                return true;

            var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                //MotivoSituacaoMatricula = MotivoSituacaoMatricula.PeloSolicitante
            };
            specSolicitacaoMatriculaItem.MaxResults = int.MaxValue;

            var registros = this.SearchProjectionBySpecification(specSolicitacaoMatriculaItem, p => new
            {
                ItemSeq = p.Seq,
                Etapas = p.SolicitacaoMatricula.Etapas.Select(e => new
                {
                    e.ConfiguracaoEtapa.SeqProcessoEtapa,
                    e.ConfiguracaoEtapa.ProcessoEtapa.Ordem,
                }),
                ItemHistorico = p.HistoricosSituacao.Select(s => new
                {
                    s.Seq,
                    s.SituacaoItemMatricula.ClassificacaoSituacaoFinal,
                    s.SituacaoItemMatricula.SeqProcessoEtapa,
                    s.SituacaoItemMatricula.SituacaoFinal,
                })
            }).ToList();

            var SeqProcessoPrimeiraEtapa = registros.Select(w => w.Etapas.OrderBy(o => o.Ordem).FirstOrDefault().SeqProcessoEtapa).FirstOrDefault();
            var SeqProcessoUltimaEtapa = registros.Select(w => w.Etapas.OrderByDescending(o => o.Ordem).FirstOrDefault().SeqProcessoEtapa).FirstOrDefault();

            foreach (var item in registros)
            {
                //Verifica se existe algum item da solicitação com situação mais atual da primeira etapa
                //com classificação diferente da situação mais atual da última etapa.
                var registroPrimeiraEtapa = item.ItemHistorico.Where(w => w.SeqProcessoEtapa == SeqProcessoPrimeiraEtapa).OrderByDescending(o => o.Seq).FirstOrDefault();

                if (registroPrimeiraEtapa == null)
                    continue;

                var registroUltimaEtapa = item.ItemHistorico.Where(w => w.SeqProcessoEtapa == SeqProcessoUltimaEtapa).OrderByDescending(o => o.Seq).FirstOrDefault();

                if (registroUltimaEtapa != null && registroPrimeiraEtapa.ClassificacaoSituacaoFinal != registroUltimaEtapa.ClassificacaoSituacaoFinal)
                    return false;

                //Verifica se existe algum item da solicitação que não possui situação da última etapa
                //e a situação mais atual da primeira etapa deste item for diferente de “Cancelada”.
                if (registroPrimeiraEtapa.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.Cancelado && registroUltimaEtapa == null)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Gera pelo domínio a descrição dos itens de uma solicitação, para salvar na descrição atualizada e descrição original de uma solicitação de serviços.
        /// </summary>
        public string GerarDescricaoItensSolicitacao(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, bool desconsideraEtapa, bool desativarFiltroDados)
        {
            // Templates para criação dos elementos abaixo
            Func<SMCSize, SMCSize, SMCSize, SMCSize, string, string, string> GerarFieldset = (sizeMD, sizeXS, sizeSM, sizeLG, label, conteudo) => { return $"<fieldset class=\"{SMCSizeHelper.GetSizeClasses(sizeMD, sizeXS, sizeSM, sizeLG)}\"><legend>{label ?? string.Empty}</legend>{conteudo ?? string.Empty}</fieldset>"; };
            Func<SMCSize, SMCSize, SMCSize, SMCSize, bool, MatriculaPertencePlanoEstudo, string, DivisaoSituacaoListaVO[], string, string> GerarItem = (sizeMD, sizeXS, sizeSM, sizeLG, gerarLegendaPertence, itemPlanoEstudo, turma, divisoes, situacao) =>
            {
                // Monta o retorno
                var retTmp = $"<div class=\"{SMCSizeHelper.GetSizeClasses(sizeMD, sizeXS, sizeSM, sizeLG)} smc-listdetailed-background\">";

                //Em caso de atividade academica
                if (divisoes == null)
                {
                    // Armazena o css para a descrição caso não tenha legenda
                    var strCssSemLegenda = "smc-size-md-15 smc-size-xs-24 smc-size-sm-13 smc-size-lg-15";

                    // Caso tenha legenda....
                    if (gerarLegendaPertence && itemPlanoEstudo != MatriculaPertencePlanoEstudo.Nenhum)
                    {
                        // Recupera o attr smclegend
                        SMCLegendItemAttribute attr = typeof(MatriculaPertencePlanoEstudo).GetField(itemPlanoEstudo.ToString()).SMCGetSingleCustomAttribute<SMCLegendItemAttribute>();

                        // Gera a legenda
                        if (attr.Shape == SMCGeometricShapes.Custom)
                            retTmp += $"<div class=\"smc-size-md-1 smc-size-xs-2 smc-size-sm-2 smc-size-lg-1\" style=\"margin-top:-6px;\"><div class=\"{attr.CssClass ?? string.Empty}\" title=\"{attr.Description}\"><span title=\"{attr.Description}\"></span></div></div>";
                        else
                            retTmp += $"<div class=\"smc-size-md-1 smc-size-xs-2 smc-size-sm-2 smc-size-lg-1\" style=\"margin-top:-6px;\"><div class=\"smc-legend-{attr.Shape.ToString().ToLower() + (attr.IsFilled ? "-fill" : "")} smc-legend-{attr.Color.ToString().ToLower()} {attr.CssClass ?? string.Empty}\" title=\"{attr.Description}\"><span title=\"{attr.Description}\"></span></div></div>";

                        // Altera o tamanho do item pois tem legenda
                        strCssSemLegenda = "smc-size-md-14 smc-size-xs-22 smc-size-sm-11 smc-size-lg-14";
                    }
                    retTmp += $"<div class=\"smc-listdetailed-conteudo-titulo {strCssSemLegenda}\">";
                    retTmp += $"<p>{turma}</p>";
                    retTmp += "</div>" +
                              "<div class=\"smc-size-md-9 smc-size-xs-24 smc-size-sm-11 smc-size-lg-9 smc-sga-totalizador-destaque smc-sga-descricao-situacao-legenda\">" +
                             $"<p>{situacao}</p>" +
                              "</div>" +
                              "</div>";
                }
                else
                {
                    //Para turmas a legenda e a situação ficam na divisão da turma                     
                    retTmp += $"<div class=\"smc-listdetailed-conteudo-titulo smc-size-md-24 smc-size-xs-24 smc-size-sm-24 smc-size-lg-24\">";
                    retTmp += $"<p>{turma}</p>";

                    if (divisoes != null)
                        foreach (var divisao in divisoes)
                        {
                            retTmp += $"<div class=\"smc-size-md-24 smc-size-xs-24 smc-size-sm-24 smc-size-lg-24\">";
                            // Armazena o css para a descrição caso não tenha legenda
                            string strCssSemLegenda = "smc-size-md-12 smc-size-xs-24 smc-size-sm-13 smc-size-lg-14";

                            // Caso tenha legenda....
                            if (gerarLegendaPertence && itemPlanoEstudo != MatriculaPertencePlanoEstudo.Nenhum)
                            {
                                // Recupera o attr smclegend
                                SMCLegendItemAttribute attr = typeof(MatriculaPertencePlanoEstudo).GetField(divisao.SituacaoPlanoEstudo.ToString()).SMCGetSingleCustomAttribute<SMCLegendItemAttribute>();

                                // Gera a legenda
                                if (attr.Shape == SMCGeometricShapes.Custom)
                                    retTmp += $"<div class=\"smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-1\" style=\"margin-top:-6px;\"><div class=\"{attr.CssClass ?? string.Empty}\" title=\"{attr.Description}\"><span title=\"{attr.Description}\"></span></div></div>";
                                else
                                    retTmp += $"<div class=\"smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-1\" style=\"margin-top:-6px;\"><div class=\"smc-legend-{attr.Shape.ToString().ToLower() + (attr.IsFilled ? "-fill" : "")} smc-legend-{attr.Color.ToString().ToLower()} {attr.CssClass ?? string.Empty}\" title=\"{attr.Description}\"><span title=\"{attr.Description}\"></span></div></div>";

                                strCssSemLegenda = "smc-size-md-10 smc-size-xs-22 smc-size-sm-11 smc-size-lg-13";
                            }

                            retTmp += $"<div class=\"smc-listdetailed-conteudo-titulo {strCssSemLegenda}\">";
                            retTmp += $"<label class=\"smc-listdetailed-conteudo-subtitulo smc-listdetailed-conteudo-valor-secundario\" style=\"padding-top:8px;\">{divisao.DescricaoDivisao}</label>";
                            retTmp += "</div>";
                            retTmp += $"<div class=\"smc-size-md-12 smc-size-xs-24 smc-size-sm-11 smc-size-lg-10 smc-sga-totalizador-destaque smc-sga-descricao-situacao-legenda\">";
                            retTmp += $"<p>{divisao.SituacaoDivisao}</p>";
                            retTmp += "</div>";
                            retTmp += "</div>";
                        }
                    retTmp += "</div>";
                    retTmp += "</div>";
                }

                return retTmp;
            };

            string ret = string.Empty;
            bool erro = false;
            var registro = BuscarSolicitacaoMatriculaTurmasAtividades(seqSolicitacaoMatricula, seqConfiguracaoEtapa, erro, desconsideraEtapa, desativarFiltroDados, true);
            registro.Turmas = registro.Turmas.Where(w => !string.IsNullOrEmpty(w.Situacao)).ToList();
            registro.Atividades = registro.Atividades.Where(w => !string.IsNullOrEmpty(w.Situacao)).ToList();

            var gerarLegenda = registro.Turmas.Any(t => t.PertencePlanoEstudo.GetValueOrDefault()) || registro.Atividades.Any(t => t.PertencePlanoEstudo.GetValueOrDefault());

            if (gerarLegenda && ((registro.Turmas != null && registro.Turmas.Any()) || (registro.Atividades != null && registro.Atividades.Any())))
            {
                ret += "<div class=\"smc-size-md-16 smc-size-xs-24 smc-size-sm-24 smc-size-lg-16\" data-type=\"smc-div\" id=\"SituacaoItemMatricula\" data-configurated=\"true\">" +
                           "<p class=\"smc-sga-titulo-legenda\">Tipo de solicitação</p>" +
                           "<div class=\"smc-legend-box smc-sga-legenda smc-legend-orientation-horizontal\" data-type=\"smc-legend\">" +
                              "<ul class=\"smc-legend-content\">" +
                                "<li class=\"smc-legend-content-title\" data-type=\"smc-legend-title\"><span>SituacaoItemMatricula_Legenda_Titulo</span></li>";
                foreach (var item in SMCEnumHelper.GetDescriptions<MatriculaPertencePlanoEstudo>())
                {
                    // Recupera o attr smclegend
                    SMCLegendItemAttribute attr = typeof(MatriculaPertencePlanoEstudo).GetField(item).SMCGetSingleCustomAttribute<SMCLegendItemAttribute>();

                    // Gera a legenda
                    if (attr != null)
                    {
                        if (attr.Shape == SMCGeometricShapes.Custom)
                            ret += $"<li class=\"{attr.CssClass ?? string.Empty}\" data-type=\"smc-legend-item\"><span title=\"{attr.Description}\">{attr.Description}</span></li>";
                        else
                            ret += $"<li class=\"smc-legend-{attr.Shape.ToString().ToLower() + (attr.IsFilled ? "-fill" : "")} smc-legend-{attr.Color.ToString().ToLower()} {attr.CssClass ?? string.Empty}\" data-type=\"smc-legend-item\"><span title=\"{attr.Description}\">{attr.Description}</span></li>";
                    }
                }
                ret += "</ul>" +
                   "</div>" +
                "</div>";
            }

            if (registro.Turmas != null && registro.Turmas.Any())
            {
                string conteudoTurmas = string.Empty;
                foreach (var turma in registro.Turmas)
                {
                    string nomeTurmaFormatado = string.Empty;
                    List<DivisaoSituacaoListaVO> divisoes = null;

                    if (turma.ExibirEntidadeResponsavelTurma &&
                        (
                            registro.TokenEtapa == MatriculaTokens.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA ||
                            registro.TokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_ORIGEM ||
                            registro.TokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO
                        )
                       )
                        nomeTurmaFormatado = $"{turma.ProgramaCompartilhado} - ";
                    else if (turma.ExibirEntidadeResponsavelTurma)
                        nomeTurmaFormatado = $"{turma.ProgramaTurma} - ";

                    nomeTurmaFormatado += $"{turma.TurmaFormatado}";

                    if (turma.TurmaMatriculaDivisoes != null)
                    {
                        divisoes = new List<DivisaoSituacaoListaVO>();
                        foreach (var divisao in turma.TurmaMatriculaDivisoes)
                        {
                            DivisaoSituacaoListaVO divisaoSituacao = new DivisaoSituacaoListaVO();
                            divisaoSituacao.DescricaoDivisao = $"{divisao.DivisoesTurmas.FirstOrDefault(d => d.Seq == divisao.SeqDivisaoTurma).Descricao} - {divisao.DivisaoTurmaDescricaoSemNumero}";
                            divisaoSituacao.SituacaoPlanoEstudo = divisao.SituacaoPlanoEstudo;

                            //SMCLegendItemAttribute attr = typeof(MatriculaPertencePlanoEstudo).GetField(divisao.SituacaoPlanoEstudo.ToString()).SMCGetSingleCustomAttribute<SMCLegendItemAttribute>();
                            //divisaoSituacao.SituacaoDivisao = $"{attr.Description}";
                            divisaoSituacao.SituacaoDivisao = $"{divisao.Situacao}";
                            if (divisao.Motivo != null && !string.IsNullOrEmpty(divisao.Motivo.SMCGetDescription()))
                            {
                                divisaoSituacao.SituacaoDivisao += $" - {divisao.Motivo.SMCGetDescription()}";
                            }

                            divisoes.Add(divisaoSituacao);
                        }
                    }

                    conteudoTurmas += GerarItem(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, gerarLegenda, turma.SituacaoPlanoEstudo, nomeTurmaFormatado, divisoes?.ToArray(), null);
                }

                ret += GerarFieldset(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, "Turmas", conteudoTurmas);
            }

            if (registro.Atividades != null && registro.Atividades.Any())
            {
                string conteudoAtividades = string.Empty;

                foreach (var atividade in registro.Atividades)
                {
                    var situacaoMotivo = string.Empty;
                    if (atividade.Motivo == null || string.IsNullOrEmpty(atividade.Motivo.SMCGetDescription()))
                        situacaoMotivo = atividade.Situacao;
                    else
                        situacaoMotivo = $"{atividade.Situacao} - {atividade.Motivo.SMCGetDescription()}";

                    conteudoAtividades += GerarItem(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, gerarLegenda, atividade.SituacaoPlanoEstudo, atividade.DescricaoConfiguracaoComponente, null, situacaoMotivo);
                }
                ret += GerarFieldset(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, "Atividades acadêmicas", conteudoAtividades);
            }

            return ret;
        }

        /// <summary>
        /// Implementação da RN_MAT_151
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Solicitação de matrícula a ser verificada</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação da solicitação de serviço</param>
        /// <param name="seqProcessoEtapa">Processo etapa para considerar na hora de verificar a situação dos itens</param>
        /// <param name="seqsNovasDivisoesTurma">Novas divisões de turmas que estão sendo incluidas na solicitação de matrícula</param>
        /// <param name="seqsChancela">Itens sendo chancelados</param>
        /// <returns>Mensagem de erro no caso de falha da regra ou "" em caso de sucesso</returns>
        public string ValidarTurmasDuplicadas(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long seqProcessoEtapa, List<long> seqsNovasDivisoesTurma, List<long> seqsChancela)
        {
            /// 1.Verificar se existe mais de um item na solicitação da pessoa-atuação:
            ///  -> Que não pertence ao plano de estudos, com situação atual configurada para ser a final com classificação "Finalizado com sucesso" OU
            ///  -> Que pertence ao plano, com situação atual configurada para ser a final com classificação "Não alterado" OU
            ///  -> Que pertence ao plano, com situação atual configurada para ser a final com classificação "Finalizado sem sucesso"
            ///  -> Cujas configurações de componente das divisões da turma são iguais. Caso isto ocorra, verificar se as divisões são da mesma turma(considerar o sequencial da turma).
            /// 1.1.Se forem da mesma (turma), prosseguir.
            /// 1.2.Se não forem da mesma (turma), verificar se elas possuem assunto e se os assuntos são diferentes.
            ///     1.2.1.Se os assuntos forem diferentes, prosseguir.
            ///         1.2.2.Se não possuírem assuntos ou se os assuntos não forem diferentes, abortar a operação e exibir a seguinte mensagem de erro:
            ///         "Seleção não permitida. Não é possível cursar as turmas abaixo, pois elas são do mesmo componente:
            ///             - < Turma A >
            ///             - < Turma B >
            ///         Favor verificar detalhes das turmas, clicando em "Mais informações"."

            // Busca a origem da pessoa atuação da solicitação
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            // Busca os itens da solicitação de matricula
            var specItens = new SolicitacaoMatriculaItemFilterSpecification() { SeqSolicitacaoMatricula = seqSolicitacaoMatricula, ExibirTurma = true };
            var itens = this.SearchProjectionBySpecification(specItens, i => new ValidarTurmasDuplicadasVO()
            {
                SeqItem = i.Seq,
                SeqDivisaoTurma = i.SeqDivisaoTurma,
                PertencePlanoEstudo = i.PertencePlanoEstudo,
                SeqTurma = i.DivisaoTurma.SeqTurma,
                CodigoTurma = i.DivisaoTurma.Turma.Codigo,
                NumeroTurma = i.DivisaoTurma.Turma.Numero,
                DescricaoConfiguracaoComponenteAluno = i.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).Descricao,
                SeqComponenteCurricularAssunto = i.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).RestricoesTurmaMatriz.FirstOrDefault().SeqComponenteCurricularAssunto,
                SeqCompomentecurricularAssuntoPrincipal = i.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(r => r.OfertaMatrizPrincipal).SeqComponenteCurricularAssunto,
                ConfiguracoesComponente = i.DivisaoTurma.Turma.ConfiguracoesComponente.Select(c => new ValidarTurmasDuplicadasConfigComponenteVO()
                {
                    SeqConfiguracaoComponente = c.SeqConfiguracaoComponente,
                    Principal = c.Principal,
                    Descricao = c.Descricao
                }).ToList(),
                HistoricoSituacaoAtual = i.HistoricosSituacao.OrderByDescending(h => h.DataInclusao)
                                                             .Select(h => new ValidarTurmasDuplicadasHistoricoSituacaoVO()
                                                             {
                                                                 SeqProcessoEtapa = h.SituacaoItemMatricula.SeqProcessoEtapa,
                                                                 SeqSituacaoItemMatricula = h.SeqSituacaoItemMatricula,
                                                                 SituacaoInicial = h.SituacaoItemMatricula.SituacaoInicial,
                                                                 SituacaoFinal = h.SituacaoItemMatricula.SituacaoFinal,
                                                                 ClassificacaoSituacaoFinal = h.SituacaoItemMatricula.ClassificacaoSituacaoFinal
                                                             })
                                                             .FirstOrDefault(h => h.SeqProcessoEtapa == seqProcessoEtapa)
            }).ToList();

            // Retira os itens que não tem situação atual na etapa
            itens = itens.Where(i => i.HistoricoSituacaoAtual != null).ToList();

            // Se existe componente de assunto principal e não encontrou o assunto da restrição do aluno, atualiza com a principal
            foreach (var item in itens.Where(i => i.SeqCompomentecurricularAssuntoPrincipal.HasValue && !i.SeqComponenteCurricularAssunto.HasValue))
            {
                item.SeqComponenteCurricularAssunto = item.SeqCompomentecurricularAssuntoPrincipal;
            }

            // Caso tenha informado algum sequencial de turma para inclusao, busca as informações da mesma
            if (seqsNovasDivisoesTurma != null && seqsNovasDivisoesTurma.Count > 0)
            {
                var specDivisaoTurma = new DivisaoTurmaFilterSpecification() { Seqs = seqsNovasDivisoesTurma };
                var itensNovos = DivisaoTurmaDomainService.SearchProjectionBySpecification(specDivisaoTurma, i => new ValidarTurmasDuplicadasVO()
                {
                    SeqDivisaoTurma = i.Seq,
                    PertencePlanoEstudo = false,
                    SeqTurma = i.SeqTurma,
                    CodigoTurma = i.Turma.Codigo,
                    NumeroTurma = i.Turma.Numero,
                    DescricaoConfiguracaoComponenteAluno = i.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).Descricao,
                    SeqComponenteCurricularAssunto = i.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).RestricoesTurmaMatriz.FirstOrDefault().SeqComponenteCurricularAssunto,
                    ConfiguracoesComponente = i.Turma.ConfiguracoesComponente.Select(c => new ValidarTurmasDuplicadasConfigComponenteVO()
                    {
                        SeqConfiguracaoComponente = c.SeqConfiguracaoComponente,
                        Principal = c.Principal,
                        Descricao = c.Descricao
                    }).ToList()
                }).ToList();

                itens.AddRange(itensNovos);
            }

            // Caso tenha informado algum sequencial para chancela, altera a situação do item para "Finalizado com sucesso"
            if (seqsChancela != null && seqsChancela.Count > 0)
            {
                foreach (var seq in seqsChancela)
                {
                    var itemChancela = itens.FirstOrDefault(i => i.SeqItem == seq);
                    itemChancela.HistoricoSituacaoAtual.ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.FinalizadoComSucesso;
                    itemChancela.HistoricoSituacaoAtual.SituacaoFinal = true;
                }
            }

            // Filtra os itens de acordo com a situação
            //  -> Que não pertence ao plano de estudos, com situação atual configurada para ser a final com classificação "Finalizado com sucesso" OU
            //  -> Que pertence ao plano, com situação atual configurada para ser a final com classificação "Não alterado" OU
            //  -> Que pertence ao plano, com situação atual configurada para ser a final com classificação "Finalizado sem sucesso"
            var itensValidar = itens.Where(i => i.HistoricoSituacaoAtual == null || // Com situação NULL estão os novos itens!
                                                (!i.PertencePlanoEstudo && i.HistoricoSituacaoAtual.SituacaoFinal && i.HistoricoSituacaoAtual.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso) ||
                                                (i.PertencePlanoEstudo && i.HistoricoSituacaoAtual.SituacaoFinal && (i.HistoricoSituacaoAtual.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado || i.HistoricoSituacaoAtual.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso))
                                            ).ToList();

            // Mensagem de retorno
            var mensagem = "";

            // Se existem itens para validação
            if (itensValidar.Count() > 1)
            {
                // Busca todas as configurações de componente de todas as turmas, caso seja turma compartilhada busca todas as configurações
                var listaConfig = itensValidar.SelectMany(c => c.ConfiguracoesComponente).Select(c => c.SeqConfiguracaoComponente).Distinct().ToList();

                // Para cada configuração encontrada
                foreach (var config in listaConfig)
                {
                    // Verifica se existe mais de uma turma para validar
                    var itensConfigIgual = itensValidar.Where(i => i.ConfiguracoesComponente.Any(c => c.SeqConfiguracaoComponente == config)).ToList();
                    if (itensConfigIgual.Count() > 1)
                    {
                        // Verifica se os itens encontrados são de turmas diferentes
                        var qtdTurmas = itensConfigIgual.Select(i => i.SeqTurma).Distinct().ToList();
                        if (qtdTurmas.Count() > 1)
                        {
                            // São de turmas diferentes e não tem assunto
                            if (itensConfigIgual.All(c => c.SeqComponenteCurricularAssunto == null))
                            {
                                foreach (var item in itensConfigIgual)
                                {
                                    var descAux = item.DescricaoConfiguracaoComponenteAluno ?? item.ConfiguracoesComponente.FirstOrDefault(c => c.Principal).Descricao;
                                    mensagem += $"</br>- {item.CodigoTurma}.{item.NumeroTurma} - {descAux}";
                                }
                            }
                            else // Verifica se são do mesmo assunto
                            {
                                // Cria um dicionário agrupando os assuntos que existem
                                // Necessário pois podem ter 3 itens, sendo 2 deles do mesmo assunto
                                var dic = itensConfigIgual.Where(a => a.SeqComponenteCurricularAssunto.HasValue).GroupBy(a => a.SeqComponenteCurricularAssunto.Value).ToDictionary(x => x.Key, x => x.ToList());
                                var duplicado = dic.Where(x => x.Value.Count > 1).Select(x => x.Value).ToList();
                                if (duplicado.Count > 0)
                                {
                                    foreach (var item in duplicado.FirstOrDefault())
                                    {
                                        var descAux = item.DescricaoConfiguracaoComponenteAluno ?? item.ConfiguracoesComponente.FirstOrDefault(c => c.Principal).Descricao;
                                        mensagem += $"</br>- {item.CodigoTurma}.{item.NumeroTurma} - {descAux}";
                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(mensagem))
                        break;
                }
            }

            return mensagem;
        }

        public bool VerificaPertenceAoPlano(long seqSolicitacaoMatricula, long seqDivisaoTurma)
        {
            var spec = new SolicitacaoMatriculaItemFilterSpecification
            {
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                SeqDivisaoTurma = seqDivisaoTurma
            };

            var pertenceAoPlano = this.SearchByKey(spec);
            if (pertenceAoPlano == null)
                return false;
            else
                return pertenceAoPlano.PertencePlanoEstudo;
        }
    }
}