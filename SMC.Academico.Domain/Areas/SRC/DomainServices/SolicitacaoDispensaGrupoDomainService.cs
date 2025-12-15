using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class SolicitacaoDispensaGrupoDomainService : AcademicoContextDomain<SolicitacaoDispensaGrupo>
    {
        #region DomainServices

        private DispensaDomainService DispensaDomainService => Create<DispensaDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();

        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();

        private SolicitacaoDispensaDomainService SolicitacaoDispensaDomainService => Create<SolicitacaoDispensaDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        #endregion DomainServices

        public void SalvarAtendimentoDispensaAgrupamentoGrupo(SolicitacaoDispensaGrupo dados)
        {
            using (var transact = SMCUnitOfWork.Begin())
            {
                /*  1. Verificar se os itens selecionados em “Itens cursados” já foram usados em agrupamentos já salvos
                    em solicitacao_dispensa_grupo, para a mesma solicitação de dispensa. Em caso afirmativo, abortar
                    operação e exibir a mensagem de erro:
                    MENSAGEM
                        “Existem um ou mais itens cursados que já foram utilizados em outros agrupamentos de dispensa”*/

                var seqsOrigensInternas = dados.OrigensInternas.Select(d => d.SeqSolicitacaoDispensaOrigemInterna).ToList();
                var seqsOrigensExternas = dados.OrigensExternas.Select(d => d.SeqSolicitacaoDispensaOrigemExterna).ToList();
                var seqsOrigens = new List<long>();
                var seqsDestinos = dados.Destinos.Select(d => d.SeqSolicitacaoDispensaDestino).ToList();
                seqsOrigens.AddRange(seqsOrigensInternas);
                seqsOrigens.AddRange(seqsOrigensExternas);

                /* Ao salvar um grupo, consistir:
                 * Se o grupo tiver componente cursado externo só poderá ter modo de exibição aproveitamento de créditos.
                 * Caso contrário, abortar a operação e emitir mensagem de erro:
                 * Este agrupamento possui item cursado em instituição externa, portanto o modo de exibição só poderá ser “Aproveitamento de créditos”.
                 */
                if (seqsOrigensExternas.Any() && dados.ModoExibicaoHistoricoEscolar == ModoExibicaoHistoricoEscolar.ComponentesConcluidos)
                    throw new SMCApplicationException("Este agrupamento possui item cursado em instituição externa, portanto o modo de exibição só poderá ser \"Aproveitamento de créditos\".");

                if (seqsOrigens != null && seqsOrigens.Any())
                {
                    var itensJaUtilizados = this.Count(new SolicitacaoDispensaGrupoFilterSpecification
                    {
                        SeqsOrigens = seqsOrigens,
                        SeqDispensaDiferente = dados.SeqDispensa,
                        SeqDiferente = dados.Seq,
                        SeqSolicitacaoDispensa = dados.SeqSolicitacaoDispensa
                    });

                    if (itensJaUtilizados > 0)
                        throw new SMCApplicationException("Existem um ou mais itens cursados que já foram utilizados em outros agrupamentos de dispensa.");
                }
                else
                    throw new SMCApplicationException("É necessário selecionar pelo menos um item cursado.");

                /*  2. Verificar se os itens selecionados em “Itens a serem dispensados” já foram usados em agrupamentos
                    já salvos em solicitacao_dispensa_grupo, para a mesma solicitação de dispensa.
                    Em caso afirmativo, abortar operação e exibir a mensagem de erro:
                    MENSAGEM
                    “Existem um ou mais itens a serem dispensados que já foram utilizados em outros agrupamentos de dispensa”*/
                if (seqsDestinos != null && seqsDestinos.Any())
                {
                    var itensJaUtilizados = this.Count(new SolicitacaoDispensaGrupoFilterSpecification
                    {
                        SeqsDestinos = seqsDestinos,
                        SeqDispensaDiferente = dados.SeqDispensa,
                        SeqDiferente = dados.Seq,
                        SeqSolicitacaoDispensa = dados.SeqSolicitacaoDispensa
                    });

                    if (itensJaUtilizados > 0)
                        throw new SMCApplicationException("Existem um ou mais itens a serem dispensados que já foram utilizados em outros agrupamentos de dispensa.");
                }
                else
                    throw new SMCApplicationException("É necessário selecionar pelo menos um item dispensado.");

                /*  3. Verificar se o somatório de carga horária dos itens cursados está dentro do percentual de equivalência do somatório de
                    carga horária dos itens a serem dispensados: (somatório cursado/somatório a dispensar) x 100 >= Percentual de equivalência
                    Onde percentual de equivalência é o valor parametrizado em "Parâmetro por Instituição e Nível".
                    Em caso de violação, abortar a operação e exibir a mensagem de erro:
                    MENSAGEM
                    "É necessário que o somatório de carga horária dos itens cursados seja, no mínimo, [percentual de equivalência]%
                    equivalente ao somatório de carga horária dos itens solicitados para dispensa". */

                var seqPessoaAtuacao = SolicitacaoDispensaDomainService.SearchProjectionByKey(dados.SeqSolicitacaoDispensa, x => x.SeqPessoaAtuacao);
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao, true);
                var dadosCargaHoraria = SolicitacaoDispensaDomainService.SearchProjectionByKey(dados.SeqSolicitacaoDispensa, x => new DadosCalculoTotalDispensaVO
                {
                    SeqsComponentesCurricularesDispensa = x.Destinos.Where(d => seqsDestinos.Contains(d.Seq) && d.SeqComponenteCurricular.HasValue).Select(d => d.SeqComponenteCurricular.Value).ToList(),
                    DadosGruposCurricularesDispensa = x.Destinos.Where(d => seqsDestinos.Contains(d.Seq) && d.SeqCurriculoCursoOfertaGrupo.HasValue).Select(d => new DadosCalculoCurriculoCursoOfertaGrupo
                    {
                        SeqCurriculoCursoOfertaGrupo = d.SeqCurriculoCursoOfertaGrupo.Value,
                        QuantidadeDispensaGrupo = d.QuantidadeDispensaGrupo
                    }).ToList(),
                    SeqsComponentesCurricularesOrigemInterna = x.OrigensInternas.Where(o => seqsOrigensInternas.Contains(o.Seq) && o.HistoricoEscolar.SeqComponenteCurricular.HasValue).Select(o => o.HistoricoEscolar.SeqComponenteCurricular.Value).ToList(),
                    OrigensExternas = x.OrigensExternas.Where(o => seqsOrigensExternas.Contains(o.Seq)).Select(e => new DadosCalculoTotalDispensaOrigemExternaVO
                    {
                        FormatoCargaHoraria = e.FormatoCargaHoraria,
                        Credito = e.Credito,
                        CargaHoraria = e.CargaHoraria
                    }).ToList(),
                    SeqInstituicaoEnsino = x.AlunoHistorico.Aluno.Pessoa.SeqInstituicaoEnsino
                });
                var horas = SolicitacaoDispensaDomainService.CalcularTotaisDispensa(dadosCargaHoraria);

                if (horas.Cursado.TotalHoras > 0 && horas.Dispensa.TotalHoras > 0)
                {
                    var seqNivelEnsinoHistoricoAtual = AlunoHistoricoDomainService.BuscarNivelEnsinoHistoricoAtualAluno(seqPessoaAtuacao);
                    var instituicaoNivel = InstituicaoNivelDomainService.BuscarInstituicaoNivelPorNivelEnsino(seqNivelEnsinoHistoricoAtual);
                    decimal percent = horas.Cursado.TotalHoras / (decimal)horas.Dispensa.TotalHoras * 100;

                    if (percent < instituicaoNivel.PercentualEquivalencia)
                        throw new SolicitacaoDispensaCursadosCargaHorariaException(instituicaoNivel.PercentualEquivalencia.ToString());
                }

                /*  Se as validações 1, 2 e 3 forem satisfeitas:
                    1. Se no agrupamento os itens cursados e os itens a serem dispensados formam uma dispensa exatamente igual ***
                    cadastrada na tabela dispensa e se o valor do campo "Exibir no histórico escolar como" selecionado na tela é diferente do valor
                    salvo na tabela dispensa, exibir a mensagem de orientação:
                    MENSAGEM
                    "O grupo criado é uma equivalência já cadastrada previamente, portanto o valor do campo "Exibir no histórico escolar como"
                    deverá ser igual a <Valor salvo na tabela Dispensa para o grupo em questão>".*/

                (long? SeqDispensa, ModoExibicaoHistoricoEscolar? ModoExibicaoHistoricoEscolar) equivalencia = (null, null);
                if (!seqsOrigensExternas.Any())
                    equivalencia = DispensaDomainService.BuscarDispensaEquivalente(dadosCargaHoraria.SeqsComponentesCurricularesOrigemInterna, dadosCargaHoraria.SeqsComponentesCurricularesDispensa, dadosOrigem.SeqMatrizCurricularOferta);

                if (equivalencia.ModoExibicaoHistoricoEscolar.HasValue && equivalencia.ModoExibicaoHistoricoEscolar != dados.ModoExibicaoHistoricoEscolar)
                    throw new SMCApplicationException($"O grupo criado é uma equivalência já cadastrada previamente, portanto o valor do campo \"Exibir no histórico escolar como\" deverá ser igual a \"{SMCEnumHelper.GetDescription(equivalencia.ModoExibicaoHistoricoEscolar.Value)}\".");

                /*  2. Salvar o agrupamento e o valor de “modo_exibicao_historico_escolar” em solicitacao_dispensa_grupo.
                    Se os itens cursados e os itens a serem forem dispensados formarem uma dispensa exatamente igual *** cadastrada na tabela
                    dispensa, salvar o valor do campo modo_exibicao_historico_escolar igual ao valor salvo na tabela dispensa e o sequencial da dispensa.*/
                if (equivalencia.SeqDispensa.HasValue)
                    dados.SeqDispensa = equivalencia.SeqDispensa;

                /*  3. Salvar em solicitação_dispensa_grupo_origem_interna os itens selecionados em “itens cursados” e que estão
                    em solicitacao_dispensa_origem_interna.
                    4. Salvar em solicitação_dispensa_grupo_origem_externa os itens selecionados em “itens cursados” e que
                    estão em solicitacao_dispensa_origem_externa.
                    5. Salvar em solicitação_dispensa_grupo_destino os itens selecionados em “itens a serem dispensados” e que estão
                    em solicitacao_dispensa_destino.

                    ***[se todos os componentes do grupo seq_grupo_origem e do grupo grupo seq_grupo_dispensado, da
                    tabela dispensa, forem iguais aos componentes selecionados em "Itens cursados" e em "Itens a serem dispensados", respectivamente.
                    Não pode sobrar ou faltar nenhum componente. Em "Itens a serem dispensados" não pode haver grupo curricular selecionado e em
                    "Itens cursados" não pode haver Itens cursados em outras instituições, ou seja, de origem externa.*/

                SaveEntity(dados);
                transact.Commit();
            }
        }

        /// <summary>
        /// RN_SRC_089 - Solicitação - Criação automática de grupos de itens de dispensa
        /// Verifica se existe apenas 1 item cursado (interno ou externo) e também só 1 item a ser dispensado.
        /// Nesse caso, já cria o agrupamento, verificando também se esses itens formam uma equivalência.
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        public void VerificarAgrupamentoAutomatico(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação
            var spec = new SMCSeqSpecification<SolicitacaoDispensa>(seqSolicitacaoServico);
            var solicitacao = SolicitacaoDispensaDomainService.SearchProjectionByKey(spec, x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                ExisteGrupo = x.Grupos.Any(),
                OrigensExternas = x.OrigensExternas.Select(e => new
                {
                    Seq = e.Seq,
                }).ToList(),
                OrigensInternas = x.OrigensInternas.Select(i => new
                {
                    Seq = i.Seq,
                    SeqComponenteCurricular = i.HistoricoEscolar.SeqComponenteCurricular
                }).ToList(),
                Destinos = x.Destinos.Select(d => new
                {
                    Seq = d.Seq,
                    SeqComponenteCurricular = d.SeqComponenteCurricular
                }).ToList()
            });

            // Se não tem nenhum grupo na solicitação
            if (!solicitacao.ExisteGrupo)
            {
                // Conta quantas origens (internas ou externas) tem na solicitação
                int countOrigens = 0;
                bool temOrigemInterna = false;
                if (solicitacao.OrigensExternas.Any())
                    countOrigens += solicitacao.OrigensExternas.Count();
                if (solicitacao.OrigensInternas.Any())
                {
                    countOrigens += solicitacao.OrigensInternas.Count();
                    temOrigemInterna = true;
                }

                // Conta quantos destinos tem na solicitação
                int countDestinos = 0;
                if (solicitacao.Destinos.Any())
                    countDestinos = solicitacao.Destinos.Count();

                // Se a quantidade de itens cursados (nesta instituição ou em outra instituição) OU
                // a quantidade de itens a serem dispensados for igual a um, criar o grupo automaticamente
                if (countOrigens == 1 || countDestinos == 1)
                {
                    var grupo = new SolicitacaoDispensaGrupo()
                    {
                        SeqSolicitacaoDispensa = seqSolicitacaoServico,
                        ModoExibicaoHistoricoEscolar = ModoExibicaoHistoricoEscolar.AproveitamentoCreditos,
                        OrigensInternas = new List<SolicitacaoDispensaGrupoOrigemInterna>(),
                        OrigensExternas = new List<SolicitacaoDispensaGrupoOrigemExterna>(),
                        Destinos = new List<SolicitacaoDispensaGrupoDestino>()
                    };
                    foreach (var origem in solicitacao.OrigensInternas)
                    {
                        var origemGrupo = new SolicitacaoDispensaGrupoOrigemInterna()
                        {
                            SeqSolicitacaoDispensaOrigemInterna = origem.Seq
                        };
                        grupo.OrigensInternas.Add(origemGrupo);
                    }
                    foreach (var origem in solicitacao.OrigensExternas)
                    {
                        var origemGrupo = new SolicitacaoDispensaGrupoOrigemExterna()
                        {
                            SeqSolicitacaoDispensaOrigemExterna = origem.Seq
                        };
                        grupo.OrigensExternas.Add(origemGrupo);
                    }
                    foreach (var destino in solicitacao.Destinos)
                    {
                        var destinoGrupo = new SolicitacaoDispensaGrupoDestino()
                        {
                            SeqSolicitacaoDispensaDestino = destino.Seq
                        };
                        grupo.Destinos.Add(destinoGrupo);
                    }

                    // Antes de salvar o grupo, se a origem é interna, verifica se existe alguma equivalencia
                    if (temOrigemInterna)
                    {
                        // Busca a matriz do aluno
                        var seqMatrizCurricular = AlunoDomainService.BuscarDadosMatrizCurricularAlunoUltimoPlano(solicitacao.SeqPessoaAtuacao).SeqMatrizCurricularOferta.GetValueOrDefault();

                        // Verifica se existe algum registro de dispensa (equivalencia) com os mesmo itens da solicitação de dispensa,
                        // que não tenha a matriz na exceção e esteja ativa.
                        var dispensa = DispensaDomainService.BuscarDispensaEquivalente(solicitacao.OrigensInternas.Select(i => i.SeqComponenteCurricular.HasValue ? i.SeqComponenteCurricular.Value : 0).ToList(),
                                                                                       solicitacao.Destinos.Select(d => d.SeqComponenteCurricular.HasValue ? d.SeqComponenteCurricular.Value : 0).ToList(),
                                                                                       seqMatrizCurricular);
                        // Se encontrou a dispensa... Ajusta o grupo criado.
                        if (dispensa.SeqDispensa.HasValue)
                        {
                            grupo.SeqDispensa = dispensa.SeqDispensa.Value;
                            grupo.ModoExibicaoHistoricoEscolar = dispensa.ModoExibicao.Value;
                        }
                    }

                    // Salva o grupo
                    this.SaveEntity(grupo);
                }
            }
        }


        public bool VerificarAgrupamento(long seqSolicitacaoServico, long seqSolicitacaoDispensaDestino)
        {
            var spec = new SolicitacaoDispensaGrupoFilterSpecification
            {
                SeqSolicitacaoDispensa = seqSolicitacaoServico,
                SeqSolicitacaoDispensaDestino = seqSolicitacaoDispensaDestino
            };

            var retorno = this.SearchBySpecification(spec).Any();
            return retorno;
        }
    }
}