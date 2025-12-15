using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions;
using SMC.Academico.Common.Areas.APR.Exceptions.LancamentoHistoricoEscolar;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Helpers;
using SMC.Academico.Domain.Repositories;
using SMC.Formularios.ServiceContract.Areas.TMP;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using static iTextSharp.text.pdf.AcroFields;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class HistoricoEscolarDomainService : AcademicoContextDomain<HistoricoEscolar>
    {
        #region [ DomainService ]

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private TurmaConfiguracaoComponenteDomainService TurmaConfiguracaoComponenteDomainService => Create<TurmaConfiguracaoComponenteDomainService>();

        private DivisaoComponenteDomainService DivisaoComponenteDomainService => Create<DivisaoComponenteDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private AlunoFormacaoDomainService AlunoFormacaoDomainService => Create<AlunoFormacaoDomainService>();

        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        private ComponenteCurricularDomainService ComponenteCurricularDomainService => Create<ComponenteCurricularDomainService>();

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();

        private CriterioAprovacaoDomainService CriterioAprovacaoDomainService => Create<CriterioAprovacaoDomainService>();

        private CurriculoCursoOfertaGrupoDomainService CurriculoCursoOfertaGrupoDomainService => Create<CurriculoCursoOfertaGrupoDomainService>();

        private DivisaoMatrizCurricularDomainService DivisaoMatrizCurricularDomainService => Create<DivisaoMatrizCurricularDomainService>();

        private EscalaApuracaoItemDomainService EscalaApuracaoItemDomainService => Create<EscalaApuracaoItemDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private GrupoCurricularDomainService GrupoCurricularDomainService => Create<GrupoCurricularDomainService>();

        private HistoricoEscolarColaboradorDomainService HistoricoEscolarColaboradorDomainService => Create<HistoricoEscolarColaboradorDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private IngressanteFormacaoEspecificaDomainService IngressanteFormacaoEspecificaDomainService => Create<IngressanteFormacaoEspecificaDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => Create<MatrizCurricularOfertaDomainService>();

        private MensagemDomainService MensagemDomainService => Create<MensagemDomainService>();

        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private PessoaAtuacaoBeneficioDomainService PessoaAtuacaoBeneficioDomainService => Create<PessoaAtuacaoBeneficioDomainService>();

        private PessoaAtuacaoCondicaoObrigatoriedadeDomainService PessoaAtuacaoCondicaoObrigatoriedadeDomainService => Create<PessoaAtuacaoCondicaoObrigatoriedadeDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        private RestricaoTurmaMatrizDomainService RestricaoTurmaMatrizDomainService => Create<RestricaoTurmaMatrizDomainService>();

        private SolicitacaoAtividadeComplementarDomainService SolicitacaoAtividadeComplementarDomainService => Create<SolicitacaoAtividadeComplementarDomainService>();

        private SolicitacaoDispensaDomainService SolicitacaoDispensaDomainService => Create<SolicitacaoDispensaDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private TermoIntercambioDomainService TermoIntercambioDomainService => Create<TermoIntercambioDomainService>();

        private TipoComponenteCurricularDomainService TipoComponenteCurricularDomainService => Create<TipoComponenteCurricularDomainService>();

        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();

        private DivisaoMatrizCurricularComponenteDomainService DivisaoMatrizCurricularComponenteDomainService => Create<DivisaoMatrizCurricularComponenteDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();

        private ApuracaoAvaliacaoDomainService ApuracaoAvaliacaoDomainService => Create<ApuracaoAvaliacaoDomainService>();

        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();

        private IAcademicoRepository AcademicoRepository => this.Create<IAcademicoRepository>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => this.Create<ConfiguracaoEventoLetivoDomainService>();

        #endregion [ DomainService ]

        #region [ Queries ]

        #region [ _buscarIntegralizacaoDispensaSGFTemplate ]

        private string _buscarIntegralizacaoDispensaSGFTemplate =
                        @"  select	distinct top 1
	                            sr.seq_template_processo_sgf
                            from	SRC.solicitacao_dispensa sd
                            join	SRC.solicitacao_servico ss
		                            on sd.seq_solicitacao_servico = ss.seq_solicitacao_servico
                            join	SRC.configuracao_processo cp
		                            on ss.seq_configuracao_processo = cp.seq_configuracao_processo
                            join	SRC.processo pr
		                            on cp.seq_processo =  pr.seq_processo
                            join	SRC.servico sr
		                            on pr.seq_servico = sr.seq_servico
                            where	ss.seq_aluno_historico = {0} ";

        #endregion [ _buscarIntegralizacaoDispensaSGFTemplate ]

        #region [ _procedureComponentesHistoricosIntegralizacao ]

        private string _procedureComponentesHistoricosIntegralizacao =
                        @"  EXEC ACADEMICO.APR.st_rel_componentes_historico_escolar
                                    @SEQ_ALUNO_HISTORICO = {0},
                                    @IND_EXIBE_QTD_CREDITO_ZERO = {1},
                                    @IND_EXIBE_COMPONENTES_REPROVADOS = {2},
                                    @IND_EXIBE_COMPONENTES_CURSADOS_SEM_HISTORICO = {3},
                                    @IND_EXIBE_COMPONENTES_EXAME = {4},
                                    @IND_IGNORA_DISPENSA_COMO_CURSADO = {5},
                                    @SEQ_SITUACAO_ETAPA_DEFERIDA = {6} ";

        #endregion [ _procedureComponentesHistoricosIntegralizacao ]

        #endregion [ Queries ]

        /// <summary>
        /// Busca os dados do cabeçalho do histórico escolar de um aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Dados do Cabeçalho</returns>
        public HistoricoEscolarCabecalhoVO BuscarHistoricoEscolarCabecalho(long seqAluno)
        {
            //Recuperar os dados da pessoa atuação identificando aluno e ingressante
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            //Recuperar os dados da instituição nível tipo vínculo aluno para organizar de acordo com o parâmetro de conceder formação
            var concedeFormacao = ValidarConcederFormacaoIntegralizacao(seqAluno, dadosOrigem.TipoAtuacao, dadosOrigem.SeqNivelEnsino, dadosOrigem.SeqTipoVinculoAluno);

            // Busca os dados do cabeçalho
            var dadosMatriz = BuscarIntegralizacaoCabecalho(seqAluno, TipoAtuacao.Aluno, dadosOrigem.SeqMatrizCurricularOferta, false, concedeFormacao, dadosOrigem.SeqAlunoHistoricoAtual);

            var specHistorico = new HistoricoEscolarFilterSpecification()
            {
                SeqAluno = seqAluno,
                SituacoesHistoricoEscolar = new[] { SituacaoHistoricoEscolar.Aprovado, SituacaoHistoricoEscolar.Dispensado }
            };
            var itensHistorico = SearchProjectionBySpecification(specHistorico, p => new HistoricoEscolarCabecalhoVO()
            {
                TotalCargaHorariaObrigatoria = !p.Optativa ? p.CargaHorariaRealizada : null,
                TotalCargaHorariaOptativa = p.Optativa ? p.CargaHorariaRealizada : null,
                TotalCreditosObrigatorios = !p.Optativa ? p.Credito : null,
                TotalCreditosOptativos = p.Optativa ? p.Credito : null
            }).ToList();

            var header = new HistoricoEscolarCabecalhoVO()
            {
                NumeroRegistroAcademico = dadosMatriz.RA,
                NomeAluno = string.IsNullOrEmpty(dadosMatriz.NomeSocial) ? dadosMatriz.NomeRegistro : $"{dadosMatriz.NomeSocial} ({dadosMatriz.NomeRegistro})",
                NomeCursoOferta = dadosMatriz.OfertaCurso,
                DescricaoTurno = dadosMatriz.Turno,
                NomeLocalidade = dadosMatriz.Localidade
            };
            header.TotalCargaHorariaObrigatoria = itensHistorico.Sum(s => s.TotalCargaHorariaObrigatoria);
            header.TotalCargaHorariaOptativa = itensHistorico.Sum(s => s.TotalCargaHorariaOptativa);
            header.TotalCreditosObrigatorios = itensHistorico.Sum(s => s.TotalCreditosObrigatorios);
            header.TotalCreditosOptativos = itensHistorico.Sum(s => s.TotalCreditosOptativos);

            // Dados da matriz do aluno
            header.ExibirOfertaMatriz = dadosMatriz.ExibirOfertaMatriz;
            header.OfertaMatriz = dadosMatriz.OfertaMatriz;
            header.SituacaoOfertaMatriz = dadosMatriz.SituacaoOfertaMatriz;
            header.ExibirDisciplinaIsolada = dadosMatriz.ExibirDisciplinaIsolada;
            header.VinculoDisciplinaIsolada = dadosMatriz.VinculoDisciplinaIsolada;

            return header;
        }

        /// <summary>
        /// Método que verifica se existe componentes ja aprovados ou dispensados na lista
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="dadosComponentesSelecionados">Dados dos componentes selecionados</param>
        /// <param name="seqCicloLetivoIgnorar">Sequencial do ciclo letivo que deve ser ignorado. Útil quando for validar se já cursou a disciplina em ciclo letivo diferente do atual, por exemplo</param>
        /// <returns>Retorna lista de componentes separado com , para os componentes já aprovados ou dispensados</returns>
        public string VerificarHistoricoComponentesAprovadosDispensados(long seqPessoaAtuacao, List<(long? SeqConfiguracaoComponente, long? SeqDivisaoTurma)> dadosComponentesSelecionados, long? seqCicloLetivoIgnorar)
        {
            // Valida apenas se for aluno
            var tipoAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(seqPessoaAtuacao, x => x.TipoAtuacao);
            if (tipoAtuacao != TipoAtuacao.Aluno)
                return string.Empty;

            // Lista para fazer a consulta no histórico
            List<HistoricoEscolarAprovadoFiltroVO> configuracoesValidar = new List<HistoricoEscolarAprovadoFiltroVO>();

            // Para cada divisão de turma selecionada
            foreach (var divisao in dadosComponentesSelecionados.Where(d => d.SeqDivisaoTurma.HasValue))
            {
                // Busca todas as configurações de componente da turma
                var specDivisao = new DivisaoTurmaFilterSpecification() { Seq = divisao.SeqDivisaoTurma.Value };
                var configs = DivisaoTurmaDomainService.SearchProjectionByKey(specDivisao, d => d.Turma.ConfiguracoesComponente).ToList();
                foreach (var config in configs)
                {
                    var specConfig = new TurmaConfiguracaoComponenteFilterSpecification() { Seq = config.Seq };
                    var dados = TurmaConfiguracaoComponenteDomainService.SearchProjectionByKey(specConfig, c => new
                    {
                        c.ConfiguracaoComponente.SeqComponenteCurricular,
                        c.ConfiguracaoComponente.ComponenteCurricular.ExigeAssuntoComponente,
                        c.RestricoesTurmaMatriz
                    });

                    if (!dados.ExigeAssuntoComponente.GetValueOrDefault())
                    {
                        if (!configuracoesValidar.Any(c => c.SeqComponenteCurricular == dados.SeqComponenteCurricular))
                            configuracoesValidar.Add(new HistoricoEscolarAprovadoFiltroVO() { SeqComponenteCurricular = dados.SeqComponenteCurricular });
                    }
                    else
                    {
                        foreach (var restricao in dados.RestricoesTurmaMatriz)
                        {
                            if (!configuracoesValidar.Any(c => c.SeqComponenteCurricular == dados.SeqComponenteCurricular && c.SeqComponenteCurricularAssunto == restricao.SeqComponenteCurricularAssunto))
                            {
                                configuracoesValidar.Add(new HistoricoEscolarAprovadoFiltroVO()
                                {
                                    SeqComponenteCurricular = dados.SeqComponenteCurricular,
                                    SeqComponenteCurricularAssunto = restricao.SeqComponenteCurricularAssunto
                                });
                            }
                        }
                    }
                }
            }

            // Para cada componente de atividade academica selecionada
            foreach (var item in dadosComponentesSelecionados.Where(c => c.SeqConfiguracaoComponente.HasValue && !c.SeqDivisaoTurma.HasValue))
            {
                // Busca os dados do componente curricular para validar
                var specComponente = new ConfiguracaoComponenteFilterSpecification() { Seq = item.SeqConfiguracaoComponente.Value };
                var dado = ConfiguracaoComponenteDomainService.SearchProjectionByKey(specComponente, c => new HistoricoEscolarAprovadoFiltroVO
                {
                    SeqComponenteCurricular = c.SeqComponenteCurricular
                });
                if (!configuracoesValidar.Any(c => c.SeqComponenteCurricular == dado.SeqComponenteCurricular))
                    configuracoesValidar.Add(dado);
            }

            return VerificarHistoricoComponentesAprovadosDispensados(seqPessoaAtuacao, configuracoesValidar, seqCicloLetivoIgnorar);
        }

        /// <summary>
        /// Método que verifica se existe componentes não aprovados ou dispensados na lista
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="dadosComponentesSelecionados">Dados dos componentes selecionados</param>
        /// <param name="seqCicloLetivoIgnorar">Sequencial do ciclo letivo que deve ser ignorado. Útil quando for validar se já cursou a disciplina em ciclo letivo diferente do atual, por exemplo</param>
        /// <returns>Retorna lista de componentes separado com , para os componentes não aprovados ou dispensados</returns>
        public string VerificarHistoricoComponentesPlanoNaoAprovadosDispensados(long seqPessoaAtuacao, List<(long? SeqConfiguracaoComponente, long? SeqDivisaoTurma)> dadosComponentesSelecionados, long? seqCicloLetivoIgnorar)
        {
            // Valida apenas se for aluno
            var tipoAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(seqPessoaAtuacao, x => x.TipoAtuacao);
            if (tipoAtuacao != TipoAtuacao.Aluno)
                return string.Empty;

            // Lista para componentes plano sem histórico
            List<string> retornoComponentes = new List<string>();
            var seqAlunoHistoricoAtual = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao), x => x.Historicos.FirstOrDefault(h => h.Atual).Seq);

            // Para cada configuração de componente curricular selecionada
            foreach (var item in dadosComponentesSelecionados.Where(d => d.SeqConfiguracaoComponente.HasValue))
            {
                // Busca os dados do componente curricular
                var dadosComponente = ConfiguracaoComponenteDomainService.SearchProjectionByKey(item.SeqConfiguracaoComponente.Value, x => new
                {
                    SeqComponenteCurricular = x.SeqComponenteCurricular,
                    ExigeAssuntoComponente = x.ComponenteCurricular.ExigeAssuntoComponente
                });

                // Cria o componente que será usado para consulta no histórico
                var componente = new HistoricoEscolarAprovadoFiltroVO { SeqComponenteCurricular = dadosComponente.SeqComponenteCurricular };

                // Caso exija assunto, busca o seq do assunto na configuração de restrição da matriz.
                // Segundo Luciana e Raphaela, todas as restrições de matriz terão o mesmo seq de assunto. Sendo assim, busca o primeiro que achar
                if (dadosComponente.ExigeAssuntoComponente.GetValueOrDefault() && item.SeqDivisaoTurma.HasValue)
                    componente.SeqComponenteCurricularAssunto = DivisaoTurmaDomainService.SearchProjectionByKey(item.SeqDivisaoTurma.Value, x => x.Turma.ConfiguracoesComponente.FirstOrDefault(c => c.SeqConfiguracaoComponente == item.SeqConfiguracaoComponente).RestricoesTurmaMatriz.FirstOrDefault(e => e.SeqComponenteCurricularAssunto.HasValue).SeqComponenteCurricularAssunto);

                var componenteAprovado = SearchProjectionByKey(new HistoricoEscolarFilterSpecification
                {
                    SeqAlunoHistorico = seqAlunoHistoricoAtual,
                    SeqComponenteCurricular = componente.SeqComponenteCurricular,
                    SeqComponenteCurricularAssunto = componente.SeqComponenteCurricularAssunto,
                    SituacoesHistoricoEscolar = new SituacaoHistoricoEscolar[] { SituacaoHistoricoEscolar.Aprovado, SituacaoHistoricoEscolar.Dispensado },
                    SeqCicloLetivoDiferente = seqCicloLetivoIgnorar
                }, x => new
                {
                    SeqComponenteCurricular = x.SeqComponenteCurricular,
                    SeqComponenteCurricularAssunto = x.SeqComponenteCurricularAssunto
                });

                if (componenteAprovado == null)
                {
                    retornoComponentes.Add(ComponenteCurricularDomainService.BuscarDescricaoSimplesComponenteCurricular(componente.SeqComponenteCurricular.Value, componente.SeqComponenteCurricularAssunto));
                }
            }

            return string.Join(", ", retornoComponentes);
        }

        public HistoricoEscolarCompletoVO BuscarHistoricoEscolarTurma(long seqTurma, long seqAluno)
        {
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            var dadosTurma = TurmaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Turma>(seqTurma), x => new
            {
                SeqOrigemAvaliacao = x.SeqOrigemAvaliacao,
                DescricaoTurma = x.ConfiguracoesComponente.FirstOrDefault(c => c.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).Descricao,
                DescricaoTurmaPrincipal = x.ConfiguracoesComponente.FirstOrDefault(c => c.Principal).Descricao,
                DescricaoCicloLetivo = x.CicloLetivoFim.Descricao
            });

            var dadosRet = BuscarHistoricoEscolar(new HistoricoEscolarFilterSpecification { SeqAluno = seqAluno, SeqOrigemAvaliacao = dadosTurma.SeqOrigemAvaliacao }) ?? new HistoricoEscolarCompletoVO();

            // Caso não tenha sequencial de histórico escolar, buscar os dados de faltas do somatório de faltas das aulas
            if (dadosRet.Seq == 0)
            {
                // Critérios de aprovação
                var dadosOrigemAvaliacao = OrigemAvaliacaoDomainService.SearchProjectionByKey(dadosTurma.SeqOrigemAvaliacao, x => new
                {
                    ExibirFaltas = x.CriterioAprovacao.ApuracaoFrequencia
                });

                if (dadosOrigemAvaliacao.ExibirFaltas)
                {
                    var dadosFaltas = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, null, seqAluno, null).FirstOrDefault();
                    if (dadosFaltas != null)
                    {
                        dadosRet.Faltas = dadosFaltas.SomaFaltasApuracao;
                        dadosRet.ExibirFaltas = dadosOrigemAvaliacao.ExibirFaltas;
                    }
                }
            }

            dadosRet.SeqTurma = seqTurma;
            dadosRet.DescricaoTurma = dadosTurma.DescricaoTurma ?? dadosTurma.DescricaoTurmaPrincipal;
            if (string.IsNullOrEmpty(dadosRet.DescricaoCicloLetivo))
                dadosRet.DescricaoCicloLetivo = dadosTurma.DescricaoCicloLetivo;

            return dadosRet;
        }

        public HistoricoEscolarCompletoVO BuscarHistoricoEscolar(HistoricoEscolarFilterSpecification spec)
        {
            var dadosAluno = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(spec.SeqAluno.GetValueOrDefault()), x => new
            {
                DescricaoAluno = x.DadosPessoais.Nome ?? x.DadosPessoais.NomeSocial,
                DescricaoOferta = x.Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                RegistroAcademico = x.NumeroRegistroAcademico,
            });

            var ret = this.SearchProjectionByKey(spec, x => new HistoricoEscolarCompletoVO
            {
                Apuracao = x.EscalaApuracaoItem.Descricao,
                CargaHorariaRealizada = x.CargaHorariaRealizada,
                Colaboradores = x.Colaboradores,
                Credito = x.Credito,
                DescricaoTurma = x.ComponenteCurricular.Descricao,
                Faltas = x.Faltas,
                Nota = x.Nota,
                Optativa = x.Optativa,
                PercentualFrequencia = x.PercentualFrequencia,
                Seq = x.Seq,
                SeqAluno = x.AlunoHistorico.SeqAluno,
                SeqAlunoHistorico = x.SeqAlunoHistorico,
                SeqCicloLetivo = x.SeqCicloLetivo,
                DescricaoCicloLetivo = x.CicloLetivo.Descricao,
                SeqComponenteCurricular = x.SeqComponenteCurricular,
                SeqComponenteCurricularAssunto = x.SeqComponenteCurricularAssunto,
                SeqCriterioAprovacao = x.OrigemAvaliacao.SeqCriterioAprovacao,
                SeqEscalaApuracaoItem = x.SeqEscalaApuracaoItem,
                SeqOrigemAvaliacao = x.SeqOrigemAvaliacao,
                SeqTurma = 0,
                SituacaoHistoricoEscolar = x.SituacaoHistoricoEscolar,
                ExibirApuracao = x.OrigemAvaliacao.CriterioAprovacao.SeqEscalaApuracao.HasValue,
                ExibirFaltas = x.OrigemAvaliacao.CriterioAprovacao.ApuracaoFrequencia,
                ExibirNota = x.OrigemAvaliacao.CriterioAprovacao.ApuracaoNota
            }) ?? new HistoricoEscolarCompletoVO();

            ret.DescricaoAluno = dadosAluno.DescricaoAluno;
            ret.DescricaoOferta = dadosAluno.DescricaoOferta;
            ret.SituacaoFinal = SMCEnumHelper.GetDescription(ret.SituacaoHistoricoEscolar);
            ret.RegistroAcademico = dadosAluno.RegistroAcademico;

            return ret;
        }

        /// <summary>
        /// Busca o histórico escolar de um aluno
        /// </summary>
        /// <param name="filtros">Sequencial do aluno e dados de paginação</param>
        /// <returns>Dados do histórico escolar do aluno</returns>
        public SMCPagerData<HistoricoEscolarListaVO> BuscarHistoricosEscolares(HistoricoEscolarFilterSpecification filtros)
        {
            filtros.SetOrderBy(o => o.AlunoHistorico.CicloLetivo.AnoNumeroCicloLetivo);
            filtros.SetOrderBy(o => o.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome);
            filtros.SetOrderBy(o => o.AlunoHistorico.CursoOfertaLocalidadeTurno.Turno.Descricao);
            filtros.SetOrderBy(o => o.CicloLetivo.AnoNumeroCicloLetivo);
            filtros.SetOrderBy(o => o.ComponenteCurricular.Descricao);

            int total;
            var historicoAluno = SearchProjectionBySpecification(filtros, p => new HistoricoEscolarListaVO()
            {
                Seq = p.Seq,
                SeqAluno = p.AlunoHistorico.SeqAluno,
                SeqAlunoHistorico = p.SeqAlunoHistorico,
                SeqOrigemAvaliacao = p.SeqOrigemAvaliacao,
                SeqSolicitacaoDispensa = ((SolicitacaoDispensa)p.SolicitacaoServico) != null ? p.SeqSolicitacaoServico : null,
                DescricaoAlunoHistoricoCicloLetivo = p.AlunoHistorico.CicloLetivo.Descricao,
                DescricaoAlunoHistoricoCursoOfertaLocalidade = p.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                DescricaoTurno = p.AlunoHistorico.CursoOfertaLocalidadeTurno.Turno.Descricao,
                DescricaoCicloLetivo = p.CicloLetivo.Descricao,
                DescricaoComponenteCurricular = p.ComponenteCurricular.Descricao,
                CodigoComponenteCurricular = p.ComponenteCurricular.Codigo,
                DescricaoAssunto = p.ComponenteCurricularAssunto.Descricao,
                DescricaoGrupoCurricular = p.GrupoCurricular.Descricao,
                CargaHoraria = p.CargaHorariaRealizada,
                Creditos = p.Credito,
                ObrigatorioOptativo = p.Optativa ? ObrigatorioOptativo.OP : ObrigatorioOptativo.OB,
                Nota = p.Nota,
                DescricaoConceito = p.EscalaApuracaoItem.Descricao,
                Faltas = p.Faltas,
                SituacaoHistoricoEscolar = p.SituacaoHistoricoEscolar,
                Colaboradores = p.Colaboradores.Select(s => s.SeqColaborador.HasValue ? s.Colaborador.DadosPessoais.Nome : s.NomeColaborador).OrderBy(o => o).ToList(),
                TiposGestaoDivisaoComponente = p.ComponenteCurricular.TipoComponente.TiposDivisao.Select(s => s.TipoGestaoDivisaoComponente).ToList(),
                Observacao = p.Observacao,
                ComponenteCurricularGestaoExame = p.ComponenteCurricular.TipoComponente.TiposDivisao.All(a => a.TipoGestaoDivisaoComponente == TipoGestaoDivisaoComponente.Exame),
                SeqCicloLetivo = p.SeqCicloLetivo
            }, out total).ToList();

            foreach (var historico in historicoAluno)
            {
                if (string.IsNullOrEmpty(historico.DescricaoComponenteCurricular))
                    historico.DescricaoComponenteCurricular = historico.DescricaoGrupoCurricular;
                else
                {
                    historico.DescricaoComponenteCurricular = $"{historico.CodigoComponenteCurricular} - {historico.DescricaoComponenteCurricular}";

                    if (!string.IsNullOrEmpty(historico.DescricaoAssunto))
                        historico.DescricaoComponenteCurricular += $": {historico.DescricaoAssunto}";
                }
                historico.DescricaoAlunoHistorico = $"{historico.DescricaoAlunoHistoricoCicloLetivo} - {historico.DescricaoAlunoHistoricoCursoOfertaLocalidade} - {historico.DescricaoTurno}";
                historico.DescricaoSituacaoFinal = SMCEnumHelper.GetDescription(historico.SituacaoHistoricoEscolar);

                //Validar se historico somente leitura
                if (historico.ComponenteCurricularGestaoExame)
                {
                    if (historico.SeqSolicitacaoDispensa.HasValue)
                    {
                        historico.SomenteLeitura = true;
                    }
                }
                else
                {
                    bool posteriorCiclo = false;

                    // Validação do período de matrícula
                    // Recupera os ciclos letivos com solicitação de matricula ou renovação
                    var ciclosMatricula = AlunoHistoricoDomainService.SearchProjectionByKey(new SMCSeqSpecification<AlunoHistorico>(historico.SeqAlunoHistorico), p => new
                    {
                        p.CicloLetivo.AnoNumeroCicloLetivo,
                        ContemSolicitacaoMatricula = (p.SolicitacaoServico as SolicitacaoMatricula) != null,
                        AnoNumeroCicloHistorico = p.HistoricosCicloLetivo
                            .Where(w => (w.SolicitacaoServico as SolicitacaoMatricula) != null ||
                                        w.AlunoHistoricoSituacao.Any(a => (a.SolicitacaoServico as SolicitacaoMatricula) != null))
                            .Select(s => s.CicloLetivo.AnoNumeroCicloLetivo).Min()
                    });

                    // Recupera o ciclo letivo informado
                    var anoNumeroCicloLetivoComponente = CicloLetivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CicloLetivo>(historico.SeqCicloLetivo), p => p.AnoNumeroCicloLetivo);
                    var anoCiclosMatriculaRenovacaoAluno = new List<string>();

                    if (ciclosMatricula.ContemSolicitacaoMatricula)
                    {
                        anoCiclosMatriculaRenovacaoAluno.Add(ciclosMatricula.AnoNumeroCicloLetivo);
                    }

                    if (!string.IsNullOrEmpty(ciclosMatricula.AnoNumeroCicloHistorico))
                    {
                        anoCiclosMatriculaRenovacaoAluno.Add(ciclosMatricula.AnoNumeroCicloHistorico);
                    }

                    // Caso tenha algum ciclo letivo com solicitação de matricula, compara o menor ciclo com o componente
                    if (anoCiclosMatriculaRenovacaoAluno.Any() && anoNumeroCicloLetivoComponente.CompareTo(anoCiclosMatriculaRenovacaoAluno.Min()) >= 0)
                    {
                        posteriorCiclo = true;
                    }

                    if (historico.SeqSolicitacaoDispensa.HasValue
                        || historico.SeqOrigemAvaliacao.HasValue
                        || !SMCSecurityHelper.Authorize(UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)
                        || posteriorCiclo)
                    {
                        historico.SomenteLeitura = true;
                    }

                    historico.Nota = ArredondarNota(historico.Nota);
                }
            }

            return new SMCPagerData<HistoricoEscolarListaVO>(historicoAluno, total);
        }

        /// <summary>
        /// Busca um registro de histórico escolar que representa a associação de um componente
        /// </summary>
        /// <param name="seq">Sequencial do item de histórico escolar</param>
        /// <exception cref="SMCInvalidOperationException">Caso o histórico informado tenha origem de avaliação (significa que não foi criado nesta tela)</exception>
        /// <returns>Dados do histórico escolar</returns>
        public HistoricoEscolarVO BuscarHistoricoEscolarComponente(long seq)
        {
            var historico = SearchProjectionByKey(new SMCSeqSpecification<HistoricoEscolar>(seq), p => new HistoricoEscolarVO()
            {
                Seq = p.Seq,
                SeqAluno = p.AlunoHistorico.SeqAluno,
                SeqAlunoHistorico = p.SeqAlunoHistorico,
                SeqCicloLetivo = p.SeqCicloLetivo,
                DescricaoCicloLetivo = p.CicloLetivo.Descricao,
                Optativa = p.Optativa,
                SeqComponenteCurricular = p.SeqComponenteCurricular,
                ComponenteCurricularExigeAssunto = p.ComponenteCurricular.ExigeAssuntoComponente,
                SeqComponenteCurricularAssunto = p.SeqComponenteCurricularAssunto,
                SeqCriterioAprovacao = p.SeqCriterioAprovacao,
                DescricaoCriterioAprovacao = p.CriterioAprovacao.Descricao,
                SeqOrigemAvaliacao = p.SeqOrigemAvaliacao,
                IndicadorApuracaoEscala = !p.CriterioAprovacao.ApuracaoNota && p.CriterioAprovacao.SeqEscalaApuracao.HasValue,
                IndicadorApuracaoFrequencia = p.CriterioAprovacao.ApuracaoFrequencia,
                IndicadorApuracaoNota = p.CriterioAprovacao.ApuracaoNota,
                PercentualMinimoFrequencia = p.CriterioAprovacao.PercentualFrequenciaAprovado,
                TipoArredondamento = p.CriterioAprovacao.TipoArredondamento,
                PercentualMinimoNota = p.CriterioAprovacao.PercentualNotaAprovado,
                NotaMaxima = p.CriterioAprovacao.NotaMaxima,
                Nota = p.Nota,
                SeqEscalaApuracaoItem = p.SeqEscalaApuracaoItem,
                Faltas = p.Faltas,
                SeqEscalaApuracao = p.EscalaApuracaoItem.SeqEscalaApuracao,
                EscalaApuracaoItens = p.EscalaApuracaoItem.EscalaApuracao.Itens.Select(s => new SMCDatasourceItem()
                {
                    Seq = s.Seq,
                    Descricao = s.Descricao
                }).ToList(),
                CargaHoraria = p.CargaHorariaRealizada,
                Credito = p.Credito,
                Colaboradores = p.Colaboradores.Select(s => new HistoricoEscolarColaboradorVO()
                {
                    Seq = s.Seq,
                    SeqColaborador = s.SeqColaborador,
                    NomeColaborador = s.NomeColaborador,
                    NomeOrdenacao = s.SeqColaborador.HasValue ? s.Colaborador.DadosPessoais.Nome : s.NomeColaborador
                }).OrderBy(o => o.NomeOrdenacao).ToList(),
                SeqMatrizCurricular = p.AlunoHistorico.HistoricosCicloLetivo.FirstOrDefault(f => f.SeqCicloLetivo == p.SeqCicloLetivo).PlanosEstudo.OrderByDescending(o => o.Seq).FirstOrDefault().MatrizCurricularOferta.SeqMatrizCurricular,
                DataExame = p.DataExame,
                ComponenteCurricularGestaoExame = p.ComponenteCurricular.TipoComponente.TiposDivisao.All(a => a.TipoGestaoDivisaoComponente == TipoGestaoDivisaoComponente.Exame),
                SeqSolicitacaoDispensa = ((SolicitacaoDispensa)p.SolicitacaoServico) != null ? p.SeqSolicitacaoServico : null,
                PercentualFrequencia = p.PercentualFrequencia,
                Observacao = p.Observacao,
                SituacaoHistoricoEscolar = p.SituacaoHistoricoEscolar
            });

            if (historico.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado)
                historico.SomenteLeitura = true;
            historico.SituacaoFinal = historico.SituacaoHistoricoEscolar.SMCGetDescription();

            historico.VinculoAlunoExigeOfertaMatrizCurricular =
                historico.ConsiderarMatriz =
                BuscarExigenciaOfertaMatrizCurricularAluno(historico.SeqAluno);

            historico.Nota = ArredondarNota(historico.Nota);

            historico.ComponenteOptativoMatriz = historico.Optativa;

            //Validar se historico somente leitura
            if (historico.ComponenteCurricularGestaoExame)
            {
                if (historico.SeqSolicitacaoDispensa.HasValue)
                {
                    historico.SomenteLeitura = true;
                }
            }
            else
            {
                bool posteriorCiclo = false;

                // Validação do período de matrícula
                // Recupera os ciclos letivos com solicitação de matricula ou renovação
                var ciclosMatricula = AlunoHistoricoDomainService.SearchProjectionByKey(new SMCSeqSpecification<AlunoHistorico>(historico.SeqAlunoHistorico), p => new
                {
                    p.CicloLetivo.AnoNumeroCicloLetivo,
                    ContemSolicitacaoMatricula = (p.SolicitacaoServico as SolicitacaoMatricula) != null,
                    AnoNumeroCicloHistorico = p.HistoricosCicloLetivo
                        .Where(w => (w.SolicitacaoServico as SolicitacaoMatricula) != null ||
                                    w.AlunoHistoricoSituacao.Any(a => (a.SolicitacaoServico as SolicitacaoMatricula) != null))
                        .Select(s => s.CicloLetivo.AnoNumeroCicloLetivo).Min()
                });

                // Recupera o ciclo letivo informado
                var anoNumeroCicloLetivoComponente = CicloLetivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CicloLetivo>(historico.SeqCicloLetivo.GetValueOrDefault()), p => p.AnoNumeroCicloLetivo);
                var anoCiclosMatriculaRenovacaoAluno = new List<string>();

                if (ciclosMatricula.ContemSolicitacaoMatricula)
                {
                    anoCiclosMatriculaRenovacaoAluno.Add(ciclosMatricula.AnoNumeroCicloLetivo);
                }

                if (!string.IsNullOrEmpty(ciclosMatricula.AnoNumeroCicloHistorico))
                {
                    anoCiclosMatriculaRenovacaoAluno.Add(ciclosMatricula.AnoNumeroCicloHistorico);
                }

                // Caso tenha algum ciclo letivo com solicitação de matricula, compara o menor ciclo com o componente
                if (anoCiclosMatriculaRenovacaoAluno.Any() && anoNumeroCicloLetivoComponente.CompareTo(anoCiclosMatriculaRenovacaoAluno.Min()) >= 0)
                {
                    posteriorCiclo = true;
                }

                if (historico.SeqSolicitacaoDispensa.HasValue
                    || historico.SeqOrigemAvaliacao.HasValue
                    || !SMCSecurityHelper.Authorize(UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)
                    || posteriorCiclo)
                {
                    historico.SomenteLeitura = true;
                }
            }

            return historico;
        }

        /// <summary>
        /// Grava uma associação de histórico escolar com componente
        /// </summary>
        /// <param name="historicoEscolarVO">Dados do histórico escolar</param>
        /// <returns>Sequencial do histórico escolar gravado</returns>
        public long SalvarHistoricoEscolarComponente(HistoricoEscolarVO historicoEscolarVO)
        {
            var historicoEscolar = historicoEscolarVO.Transform<HistoricoEscolar>();

            // Recalculo da situação final
            historicoEscolar.SituacaoHistoricoEscolar = CalcularSituacaoFinal(historicoEscolarVO.Transform<HistoricoEscolarSituacaoFinalVO>());

            // Valida se o historico escolar é somente leitura, caso ele seja salvará somente a observação não necessitando validar os campos.
            if (!historicoEscolarVO.SomenteLeitura)
            {
                // Caso o aluno histórico ainda não esteja definido,
                // recupera o registro mais recente para o ciclo letivo informado
                if (historicoEscolar.SeqAlunoHistorico == 0)
                {
                    var specHistorico = new AlunoHistoricoFilterSpecification()
                    {
                        SeqAluno = historicoEscolarVO.SeqAluno,
                        SeqCicloLetivo = historicoEscolarVO.SeqCicloLetivo
                    };
                    specHistorico.SetOrderByDescending(o => o.Seq);
                    historicoEscolar.SeqAlunoHistorico = AlunoHistoricoDomainService.SearchProjectionByKey(specHistorico, p => p.Seq);
                }

                // Carga horária
                decimal cargaHoraria = historicoEscolar.CargaHorariaRealizada.GetValueOrDefault();

                // Calcula o % de frequencia
                historicoEscolar.PercentualFrequencia = HistoricoEscolarDomainService.CalcularPercentualFrequencia(cargaHoraria, historicoEscolar.Faltas);

                // Validação de histórico duplicado segundo RN_APR_037 Associação componente curricular no histórico escolar
                /*RN_APR_037 - Associação componente curricular no histórico escolar
                "...
                1. Para componentes que não exigem assunto, não deve ser permitido associar o mesmo componente curricular mais de uma vez ao histórico escolar se já existir registro com situação dispensada ou aprovada. Em caso de violação, abortar a operação e exibir a mensagem
                ..."*/
                var specHistoricoDuplicado = new HistoricoEscolarFilterSpecification()
                {
                    SeqAluno = historicoEscolarVO.SeqAluno,
                    SeqComponenteCurricular = historicoEscolarVO.SeqComponenteCurricular,
                    SeqComponenteCurricularAssunto = historicoEscolarVO.ComponenteCurricularExigeAssunto.GetValueOrDefault() ? historicoEscolarVO.SeqComponenteCurricularAssunto : null
                };

                var dadosComponente = SearchProjectionBySpecification(specHistoricoDuplicado, p => new
                {
                    p.Seq,
                    Situacao = p.SituacaoHistoricoEscolar
                }).Where(c => c.Seq != historicoEscolarVO.Seq).ToList();

                if (dadosComponente.Any())
                {
                    if (historicoEscolarVO.ComponenteCurricularExigeAssunto.GetValueOrDefault())
                        throw new HistoricoEscolarComponenteCurricularAssuntoDuplicadoException();
                    else
                    {
                        if (dadosComponente.Any(c => c.Situacao == SituacaoHistoricoEscolar.Aprovado || c.Situacao == SituacaoHistoricoEscolar.Dispensado))
                            throw new HistoricoEscolarComponenteCurricularDuplicadoException();
                    }
                }

                // Verifica se o componente é do tipo de gestão exame
                var tiposGestaoDivisaoComponente = ComponenteCurricularDomainService.SearchProjectionByKey(new SMCSeqSpecification<ComponenteCurricular>(historicoEscolar.SeqComponenteCurricular.GetValueOrDefault()), p =>
                    p.TipoComponente.TiposDivisao.Select(s => s.TipoGestaoDivisaoComponente)).ToList();
                if (!tiposGestaoDivisaoComponente.All(a => a == TipoGestaoDivisaoComponente.Exame))
                {
                    // Validação do período de matrícula
                    // Recupera os ciclos letivos com solicitação de matricula ou renovação
                    var ciclosMatricula = AlunoHistoricoDomainService.SearchProjectionByKey(new SMCSeqSpecification<AlunoHistorico>(historicoEscolar.SeqAlunoHistorico), p => new
                    {
                        p.CicloLetivo.AnoNumeroCicloLetivo,
                        ContemSolicitacaoMatricula = (p.SolicitacaoServico as SolicitacaoMatricula) != null,
                        AnoNumeroCicloHistorico = p.HistoricosCicloLetivo
                            .Where(w => (w.SolicitacaoServico as SolicitacaoMatricula) != null ||
                                        w.AlunoHistoricoSituacao.Any(a => (a.SolicitacaoServico as SolicitacaoMatricula) != null))
                            .Select(s => s.CicloLetivo.AnoNumeroCicloLetivo).Min()
                    });

                    // Recupera o ciclo letivo informado
                    var anoNumeroCicloLetivoComponente = CicloLetivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CicloLetivo>(historicoEscolar.SeqCicloLetivo), p => p.AnoNumeroCicloLetivo);

                    var anoCiclosMatriculaRenovacaoAluno = new List<string>();
                    if (ciclosMatricula.ContemSolicitacaoMatricula)
                        anoCiclosMatriculaRenovacaoAluno.Add(ciclosMatricula.AnoNumeroCicloLetivo);
                    if (!string.IsNullOrEmpty(ciclosMatricula.AnoNumeroCicloHistorico))
                        anoCiclosMatriculaRenovacaoAluno.Add(ciclosMatricula.AnoNumeroCicloHistorico);

                    // Caso tenha algum ciclo letivo com solicitação de matricula, compara o menor ciclo com o componente
                    if (anoCiclosMatriculaRenovacaoAluno.Any() && anoNumeroCicloLetivoComponente.CompareTo(anoCiclosMatriculaRenovacaoAluno.Min()) >= 0)
                        throw new HistoricoEscolarComponenteCurricularGestaoNaoPermitidaException();
                }
                SaveEntity(historicoEscolar);
            }
            else
            {
                // Caso o histórico seja somente leitura
                UpdateFields(historicoEscolar, p => p.Observacao);
            }

            return historicoEscolar.Seq;
        }

        /// <summary>
        /// Grava o lançamento de notas e frequência final de todos os alunos de uma turma.
        /// </summary>
        /// <param name="dados">Informações de notas, faltas e conteúdo lecionado de uma turma.</param>
        /// <param name="seqProfessor">Sequncial do professo autenticado no SGA.Professor</param>
        /// <param name="gravarMateriaLecionada">Grava a matéria lecionada quando true e seqProfessor é null</param>
        /// <returns></returns>
        public void SalvarLancamentoNotasFrequenciaFinal(LancamentoHistoricoEscolarVO dados, long? seqProfessor = null, bool gravarMateriaLecionada = true)
        {
            // Listas que contém os sequenciais dos alunos que tiveram notas e/ou
            // faltas lançadas ou atualizadas para envio posterior de mensagens para os mesmos.
            Dictionary<long, short?> alunosNota = new Dictionary<long, short?>();
            List<long> alunosFalta = new List<long>();

            // Buscando novamente no banco os lançamentos para NÃO utilizar os campos hidden que vieram da tela,
            // pois poderiam ter sido alterados manualmente pelo usuário (falha de segurança).
            var professores = seqProfessor == null ? null : new List<long>() { seqProfessor.Value };
            var alunosDiarioTurmaAlunoVO = TurmaDomainService.BuscarDiarioTurmaAluno(dados.SeqTurma, null, null, professores);
            gravarMateriaLecionada &= seqProfessor == null;
            List<HistoricoEscolar> alunos = alunosDiarioTurmaAlunoVO.TransformList<HistoricoEscolar>();

            long? seqAluno = null;
            if (alunos.Count() == 1)
                seqAluno = alunos.FirstOrDefault().SeqPessoaAtuacao;

            ApuracaoAvaliacaoDomainService.ValidarTurmaPermiteApuracaoFrequencia(dados.SeqTurma, seqAluno);

            try
            {
                var t = TurmaDomainService.SearchProjectionByKey(dados.SeqTurma, p => new
                {
                    p.SeqCicloLetivoFim,
                    p.SeqOrigemAvaliacao,
                    p.OrigemAvaliacao.SeqCriterioAprovacao
                });

                for (int i = 0; i < alunos.Count; i++)
                {
                    // Só vai salvar o histórico escolar se houve lançamento de nota ou falta.
                    var aluno = dados.Lancamentos.Where(a => a.SeqAlunoHistorico == alunos[i].SeqAlunoHistorico).FirstOrDefault();
                    if (aluno != null)
                    {
                        // Verificando qual mensagem enviar.
                        if (aluno.Nota.HasValue && aluno.Nota != alunos[i].Nota)
                            alunosNota.Add(aluno.SeqPessoaAtuacao, aluno.Nota);

                        if (aluno.SeqEscalaApuracaoItem.HasValue && aluno.SeqEscalaApuracaoItem != alunos[i].SeqEscalaApuracaoItem && !alunosNota.ContainsKey(aluno.SeqPessoaAtuacao))
                            alunosNota.Add(aluno.SeqPessoaAtuacao, aluno.Nota);

                        if (aluno.SomaFaltasApuracao.HasValue && aluno.SomaFaltasApuracao != alunos[i].Faltas)
                            alunosFalta.Add(aluno.SeqPessoaAtuacao);

                        // Atualizando notas e faltas
                        var alunoCalculo = aluno.Transform<HistoricoEscolarSituacaoFinalVO>();
                        alunoCalculo.Faltas = aluno.SomaFaltasApuracao;

                        alunos[i].Nota = aluno.Nota;
                        alunos[i].Faltas = aluno.SomaFaltasApuracao;
                        alunos[i].SeqCicloLetivo = t.SeqCicloLetivoFim;
                        alunos[i].SeqOrigemAvaliacao = t.SeqOrigemAvaliacao;
                        alunos[i].SeqCriterioAprovacao = t.SeqCriterioAprovacao;
                        alunos[i].Observacao = aluno.Observacao;

                        // Recupera qual carga horária utilizar
                        decimal cargaHoraria = RecuperarCargaHoraria(alunosDiarioTurmaAlunoVO[i].CargaHorariaGrade,
                                                                     alunosDiarioTurmaAlunoVO[i].CargaHoraria.GetValueOrDefault(),
                                                                     alunosDiarioTurmaAlunoVO[i].CargaHorariaExecutada);

                        // Atribui no histórico
                        alunos[i].CargaHorariaRealizada = alunosDiarioTurmaAlunoVO[i].CargaHoraria.GetValueOrDefault();

                        // Calcula o % de frequencia
                        alunos[i].PercentualFrequencia = CalcularPercentualFrequencia(cargaHoraria, alunos[i].Faltas);

                        // Obtendo o item da escala de apuração.
                        if (alunosDiarioTurmaAlunoVO[i].SeqEscalaApuracao.GetValueOrDefault() > 0 && alunosDiarioTurmaAlunoVO[i].IndicadorApuracaoNota)
                        {
                            if (aluno.Nota.HasValue)
                            {
                                SMCDatasourceItem escalaApuracaoItem = EscalaApuracaoItemDomainService.BuscarEscalaApuracaoItensSelect(new EscalaApuracaoItemFilterSpecification() { Percentual = aluno.Nota, SeqEscalaApuracao = aluno.SeqEscalaApuracao }).FirstOrDefault();
                                if (escalaApuracaoItem != null)
                                    alunos[i].SeqEscalaApuracaoItem = escalaApuracaoItem.Seq;
                                else
                                    throw new EscalaApuracaoItemNaoEncontradoException(aluno.Nota.Value);
                            }
                            else
                            {
                                alunos[i].SeqEscalaApuracaoItem = null;
                            }
                        }
                        else
                        {
                            alunos[i].SeqEscalaApuracaoItem = aluno.SeqEscalaApuracaoItem;
                        }

                        // Calcula a situação final
                        if (aluno.SituacaoHistoricoEscolar.HasValue && aluno.SituacaoHistoricoEscolar != SituacaoHistoricoEscolar.Nenhum)
                        {
                            alunos[i].SituacaoHistoricoEscolar = aluno.SituacaoHistoricoEscolar.Value;
                        }
                        else
                        {
                            alunoCalculo.CargaHoraria = (short)cargaHoraria;
                            alunos[i].SituacaoHistoricoEscolar = CalcularSituacaoFinal(alunoCalculo);
                        }

                        //Salvando o histórico escolar.
                        if (alunos[i].Seq == default(long))
                        {
                            if (aluno.Nota.HasValue || aluno.SeqEscalaApuracaoItem.HasValue || aluno.SemNota)
                                SaveEntity(alunos[i]);
                        }
                        else
                        {
                            // Apaga o registro se todas as informações foram apagadas.
                            if (!aluno.Nota.HasValue && !aluno.SeqEscalaApuracaoItem.HasValue && !aluno.SemNota)
                                DeleteEntity(alunos[i]);
                            else
                                UpdateEntity(alunos[i]);
                        }

                        if (aluno.Nota.HasValue || aluno.SeqEscalaApuracaoItem.HasValue || aluno.SemNota)
                        {
                            GravarHistoricoEscolarColaborador(
                                alunos[i].Seq, dados.SeqTurma,
                                alunosDiarioTurmaAlunoVO[i].SeqPlanoEstudo,
                                alunosDiarioTurmaAlunoVO[i].TurmaOrientacao);
                        }
                    }
                }

                // Salvando o campo máteria lecionada.
                if (gravarMateriaLecionada)
                {
                    OrigemAvaliacao o = OrigemAvaliacaoDomainService.SearchByKey(new SMCSeqSpecification<OrigemAvaliacao>(dados.SeqOrigemAvaliacao));
                    o.MateriaLecionada = dados.MateriaLecionada?.Trim();
                    OrigemAvaliacaoDomainService.UpdateEntity(o);
                }

                EnviarMensagens(alunosFalta, alunosNota, dados.SeqTurma);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Grava a associação entre histórico escolar e os colaboradores da orientação ou da divisão do aluno.
        /// </summary>
        /// <param name="seqHistoricoEscolar">Sequencial do histórico escolar</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqPlanoEstudo">Sequencial do plano de estudo</param>
        /// <param name="orientacao">Quanto setado, associa apenas o orientador do aluno, caso contrário, associa todos colaboradores da divisão do aluno</param>
        public void GravarHistoricoEscolarColaborador(long seqHistoricoEscolar, long seqTurma, long seqPlanoEstudo, bool orientacao)
        {
            if (orientacao)
            {
                GravarHistoricoEscolarColaboradorOrientacao(seqHistoricoEscolar, seqTurma, seqPlanoEstudo);
            }
            else
            {
                GravarHistoricoEscolarColaboradorDivisao(seqHistoricoEscolar, seqTurma, seqPlanoEstudo);
            }
        }

        /// <summary>
        /// Grava a associação entre histórico escolar e os colaboradores da orientação.
        /// </summary>
        /// <param name="seqHistoricoEscolar">Sequencial do histórico escolar</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqPlanoEstudo">Sequencial do plano de estudo</param>
        private void GravarHistoricoEscolarColaboradorOrientacao(long seqHistoricoEscolar, long seqTurma, long seqPlanoEstudo)
        {
            var colaboradoresOrientacao = new List<KeyValuePair<long, string>>();
            PlanoEstudoItemFilterSpecification specPEI = new PlanoEstudoItemFilterSpecification()
            {
                SeqPlanoEstudo = seqPlanoEstudo,
                SeqTurma = seqTurma
            };
            var itens = PlanoEstudoItemDomainService.SearchProjectionBySpecification(specPEI, a =>
                a.Orientacao.OrientacoesColaborador.Select(c => new
                {
                    c.SeqColaborador,
                    c.Colaborador.DadosPessoais.Nome,
                    c.Colaborador.DadosPessoais.NomeSocial
                })).FirstOrDefault();
            if (itens.SMCAny())
            {
                colaboradoresOrientacao = itens
                    .Select(s => new KeyValuePair<long, string>(s.SeqColaborador,
                        PessoaDadosPessoaisDomainService.FormatarNomeSocial(s.Nome, s.NomeSocial)))
                    .ToList();
            }
            GravarHistoricoEscolarColaborador(colaboradoresOrientacao, seqHistoricoEscolar);
        }

        /// <summary>
        /// Grava a associação entre histórico escolar e os colaboradores da divisão do aluno.
        /// </summary>
        /// <param name="seqHistoricoEscolar">Sequencial do histórico escolar</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqPlanoEstudo">Sequencial do plano de estudo</param>
        private void GravarHistoricoEscolarColaboradorDivisao(long seqHistoricoEscolar, long seqTurma, long seqPlanoEstudo)
        {
            var specPEI = new PlanoEstudoItemFilterSpecification()
            {
                SeqPlanoEstudo = seqPlanoEstudo,
                SeqTurma = seqTurma
            };
            var colaboradoresDivisaoAluno = PlanoEstudoItemDomainService.SearchProjectionBySpecification(specPEI, p =>
                p.DivisaoTurma.Colaboradores.Select(s => new
                {
                    s.SeqColaborador,
                    s.Colaborador.DadosPessoais.Nome,
                    s.Colaborador.DadosPessoais.NomeSocial
                }))
                .FirstOrDefault()
                .Select(s => new KeyValuePair<long, string>(s.SeqColaborador,
                    PessoaDadosPessoaisDomainService.FormatarNomeSocial(s.Nome, s.NomeSocial)))
                .ToList();
            GravarHistoricoEscolarColaborador(colaboradoresDivisaoAluno, seqHistoricoEscolar);
        }

        /// <summary>
        /// Grava a associação entre histórico escolar e colaboradores.
        /// </summary>
        /// <param name="colaboradores">Colaboradores que devem ser associados ao histórico escolar</param>
        /// <param name="seqHistoricoEscolar">Sequencial do histórico escolar</param>
        private void GravarHistoricoEscolarColaborador(List<KeyValuePair<long, string>> colaboradores, long seqHistoricoEscolar)
        {
            var spec = new HistoricoEscolarColaboradorFilterSpecification()
            {
                SeqHistoricoEscolar = seqHistoricoEscolar,
                ComColaborador = true
            };
            var colaboradoresGravados = HistoricoEscolarColaboradorDomainService.SearchProjectionBySpecification(spec, p => new
            {
                p.Seq,
                SeqColaborador = p.SeqColaborador.Value
            }).ToList();

            foreach (var colaborador in colaboradores)
            {
                if (!colaboradoresGravados.Any(a => a.SeqColaborador == colaborador.Key))
                {
                    HistoricoEscolarColaborador hec = new HistoricoEscolarColaborador();
                    hec.SeqHistoricoEscolar = seqHistoricoEscolar;
                    hec.SeqColaborador = colaborador.Key;
                    hec.NomeColaborador = colaborador.Value;

                    HistoricoEscolarColaboradorDomainService.SaveEntity(hec);
                }
            }
            foreach (var colaborador in colaboradoresGravados.Where(w => !colaboradores.Any(a => a.Key == w.SeqColaborador)))
            {
                HistoricoEscolarColaboradorDomainService.DeleteEntity(colaborador.Seq);
            }
        }

        /// <summary>
        /// Se o componente estiver em algum grupo curricular do currículo do aluno gravar histórico escolar
        /// no campo ind_optativa o mesmo valor !obrigatório do grupo curricular.
        ///
        /// Se o componente não estiver em algum grupo curricular do currículo do aluno considerar que é
        /// optativa
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação (do aluno)</param>
        /// <returns>Retorna um booleano que represanta a informação do histórico escolar "Optativa"</returns>
        private bool VerificarOptativa(long seqComponenteCurricular, long seqPessoaAtuacao)
        {
            // Busca a ultima matriz curricular do aluno
            var dadosMatriz = AlunoDomainService.BuscarDadosMatrizCurricularAlunoUltimoPlano(seqPessoaAtuacao);

            // Busca o curriculo-curso-oferta-grupo deste componente na matriz do aluno
            var spec = new CurriculoCursoOfertaGrupoFilterSpecification()
            {
                SeqComponenteCurricular = seqComponenteCurricular,
                SeqCurriculoCursoOferta = dadosMatriz.SeqCurriculoCursoOferta
            };
            var curriculoCursoOfertaGrupo = CurriculoCursoOfertaGrupoDomainService.SearchByKey(spec);
            if (curriculoCursoOfertaGrupo != null && curriculoCursoOfertaGrupo.Seq > 0)
            {
                return !curriculoCursoOfertaGrupo.Obrigatorio;
            }

            return true;
        }

        /// <summary>
        /// Ao salvar (incluir ou alterar) um lançamento de nota, gravar uma mensagem para os alunos (pessoas com tipo atuação aluno)
        /// da turma em questão cujas notas foram incluídas ou alteradas.
        /// Filtrar:
        /// - Tipo de mensagem: igual a “Nota”.
        /// - Categoria igual a “Linha do Tempo”
        /// Gravar:
        /// - Descrição da mensagem: mensagem padrão parametrizada por instituição e nível para o tipo de mensagem em questão.
        /// - A data e o usuário de inclusão.
        /// </summary>
        /// <param name="alunosFalta">Lista de sequenciais dos alunos que tiveram as faltas lançadas ou atualizadas.</param>
        /// <param name="alunosNota">Lista de sequenciais dos alunos que tiveram as notas lançadas ou atualizadas.</param>
        /// <param name="seqTurma">Sequencial da turma destes alunos.</param>
        private void EnviarMensagens(List<long> alunosFalta, Dictionary<long, short?> alunosNota, long seqTurma)
        {
            Turma t = TurmaDomainService.SearchByKey(new SMCSeqSpecification<Turma>(seqTurma), a => a.ConfiguracoesComponente[0].ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino);
            if (alunosFalta.Count > 0)
            {
                foreach (var aluno in alunosFalta)
                {
                    Dictionary<string, string> dicTagsFaltas = new Dictionary<string, string>();
                    dicTagsFaltas.Add(TOKEN_TAG_MENSAGEM.TURMA, ObterDescricaoTurma(seqTurma, aluno));

                    MensagemDomainService.EnviarMensagemPessoaAtuacao(aluno, t.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.SeqInstituicaoEnsino, t.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino, TOKEN_TIPO_MENSAGEM.FREQUENCIA_FINAL, CategoriaMensagem.LinhaDoTempo, dicTagsFaltas);
                }
            }
            if (alunosNota.Count > 0)
            {
                foreach (var aluno in alunosNota)
                {
                    Dictionary<string, string> dicTagsNota = new Dictionary<string, string>
                    {
                        { TOKEN_TAG_MENSAGEM.TURMA, ObterDescricaoTurma(seqTurma, aluno.Key) },
                        { TOKEN_TAG_MENSAGEM.NOTA, aluno.Value.HasValue ? aluno.Value.ToString() : "''" }
                    };

                    MensagemDomainService.EnviarMensagemPessoaAtuacao(aluno.Key, t.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.SeqInstituicaoEnsino, t.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino, TOKEN_TIPO_MENSAGEM.NOTA_FINAL, CategoriaMensagem.LinhaDoTempo, dicTagsNota);
                }
            }
        }

        /// <summary>
        /// RN_TUR_025 - Exibição Descrição Turma
        /// Obtém a descrição de uma turma.
        /// Caso exista um currículo associado a pessoa-atuação informada, verificar se uma das configurações de componente
        /// da turma está associada à oferta de matriz da pessoa-atuação em questão.
        /// - Se sim, exibir os dados da configuração associada à oferta de matriz curricular da pessoa-atuação em questão.
        /// - Se não, exibir os dados da configuração principal da turma.
        /// Caso não exista um currículo associado, exibir os dados da configuração principal da turma.
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma.</param>
        /// <param name="seqAluno">Sequencial do aluno.</param>
        /// <returns>Descrição da tuma, neste formato: [Código da Turma] + "." + [Número da Turma] + "-" + [Descrição da turma].</returns>
        private string ObterDescricaoTurma(long seqTurma, long seqAluno)
        {
            long? seqMatrizCurricularOferta = AlunoDomainService.BuscarDadosMatrizCurricularAlunoUltimoPlano(seqAluno).SeqMatrizCurricularOferta;

            if (seqMatrizCurricularOferta.GetValueOrDefault() > 0)
            {
                RestricaoTurmaMatrizFilterSpecification spec = new RestricaoTurmaMatrizFilterSpecification()
                {
                    SeqMatrizCurricularOferta = seqMatrizCurricularOferta.Value,
                    SeqTurma = seqTurma
                };

                string descricao = RestricaoTurmaMatrizDomainService.SearchProjectionBySpecification(spec, a => new SMCDatasourceItem()
                {
                    Descricao = a.TurmaConfiguracaoComponente.Turma.Codigo + "." + a.TurmaConfiguracaoComponente.Turma.Numero + "-" + a.TurmaConfiguracaoComponente.Descricao
                }).FirstOrDefault()?.Descricao ?? string.Empty;

                if (!string.IsNullOrEmpty(descricao))
                {
                    return descricao;
                }
            }

            return TurmaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Turma>(seqTurma), a => new SMCDatasourceItem()
            {
                Descricao = a.Codigo + "." + a.Numero + "-" + a.ConfiguracoesComponente.Where(b => b.Principal).FirstOrDefault().Descricao
            })?.Descricao ?? string.Empty;
        }

        /// <summary>
        /// Inclui ou altera os lançamentos de notas e frequências finais de todos os alunos de uma turma.
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqsOrientadores">Sequenciais dos orientadores para filtro dos alunos</param>
        /// <param name="lancamentoNotaParcial">Informa se foi chamado da tela de lançamento parcial</param>
        /// <returns></returns>
        public LancamentoHistoricoEscolarVO LancarNotasFrequenciasFinais(long seqTurma, List<long> seqsOrientadores, bool lancamentoNotaParcial = false)
        {
            LancamentoHistoricoEscolarVO obj = new LancamentoHistoricoEscolarVO
            {
                SeqTurma = seqTurma
            };

            //Buscando os alunos da turma
            var alunos = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, null, null, seqsOrientadores);
            if (alunos != null && alunos.Count > 0)
            {
                obj.Lancamentos = alunos.TransformList<LancamentoHistoricoEscolarDetalhesVO>();

                if (obj.Lancamentos != null && obj.Lancamentos.Count > 0)
                    foreach (var item in obj.Lancamentos)
                        item.SemNota = item.Seq != 0 && !item.Nota.HasValue && !item.SeqEscalaApuracaoItem.HasValue;
            }

            //Buscando o campo matéria lecionada
            Turma turma = TurmaDomainService.SearchByKey(new SMCSeqSpecification<Turma>(seqTurma), a => a.OrigemAvaliacao);
            if (turma != null)
            {
                // Task 36596
                if (!lancamentoNotaParcial && turma.OrigemAvaliacao.PermiteAvaliacaoParcial.GetValueOrDefault())
                    throw new LancamentoHistoricoEscolarTurmaPermiteAvaliacaoParcialException();

                obj.SeqOrigemAvaliacao = turma.SeqOrigemAvaliacao;
                obj.MateriaLecionada = turma.OrigemAvaliacao.MateriaLecionada;
            }
            return obj;
        }

        /// <summary>
        /// Calcular situação final do aluno no lançamento de nota parcial
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
        /// <param name="nota">Nota final aluno</param>
        /// <param name="seqsProfessores">Lista sequencial dos professores</param>
        /// <returns>Situação final do aluno</returns>
        public SituacaoHistoricoEscolar CalcularSituacaofinalLancamentoNotaParcial(long seqOrigemAvaliacao, long seqAlunoHistorico, short? nota, List<long> seqsProfessores)
        {
            var dadosOrientacao = OrigemAvaliacaoDomainService.BuscarDadosOrientacao(seqOrigemAvaliacao);
            long seqAluno = AlunoHistoricoDomainService.BuscarSeqAlunoPorAlunoHistorico(seqAlunoHistorico);

            var filtroOrientador =
                !seqsProfessores.SMCAny(a => dadosOrientacao.seqsColaboradoresResposavelTurma.Contains(a))
                && dadosOrientacao.orientacao
                && dadosOrientacao.tipoOrigemAvaliacao != TipoOrigemAvaliacao.Turma
                ? seqsProfessores : null;

            HistoricoEscolarSituacaoFinalVO dadosCalculo = TurmaDomainService.BuscarDiarioTurmaAluno(dadosOrientacao.seqTurma, null, seqAluno, filtroOrientador)
                                                                             .FirstOrDefault().Transform<HistoricoEscolarSituacaoFinalVO>();

            // Recupera qual carga horária utilizar
            dadosCalculo.CargaHoraria = RecuperarCargaHoraria(dadosCalculo.CargaHorariaGrade,
                                                              dadosCalculo.CargaHoraria.GetValueOrDefault(),
                                                              dadosCalculo.CargaHorariaExecutada);

            //Caso não exista faltas lançadas no historico escolar assume as faltas apuradas
            dadosCalculo.Faltas = dadosCalculo.Faltas ?? dadosCalculo.SomaFaltasApuracao;

            dadosCalculo.Nota = nota;
            dadosCalculo.SemNota = !nota.HasValue;
            if (nota == 0)
            {
                var specApuracao = new ApuracaoAvaliacaoFilterSpecification()
                {
                    SeqOrigemAvaliacao = seqOrigemAvaliacao,
                    SeqAlunoHistorico = seqAlunoHistorico
                };
                dadosCalculo.SemNota = ApuracaoAvaliacaoDomainService.Count(specApuracao) == 0;
            }

            return CalcularSituacaoFinal(dadosCalculo);
        }

        /// <summary>
        /// Calcula a situação final do aluno segundo a regra RN_APR_007 - Cálculo da situação final de aluno
        /// </summary>
        /// <param name="historicoEscolarSituacaoVO">Dados para o cálculo da situação final do aluno</param>
        /// <returns>Situação final do aluno</returns>
        public SituacaoHistoricoEscolar CalcularSituacaoFinal(HistoricoEscolarSituacaoFinalVO dadosCalculo)
        {
            // Essa implementação da regra RN_APR_007 existe também no javascript, em /Areas/APR/LancamentoHistoricoEscolar/Index.js

            if (dadosCalculo.IndicadorPermiteAlunoSemNota && dadosCalculo.SemNota && !dadosCalculo.Nota.HasValue)
                return SituacaoHistoricoEscolar.AlunoSemNota;

            // Variáveis com os mesmos nomes da regra
            short cargaHoraria = dadosCalculo.CargaHoraria.GetValueOrDefault();

            // Calcula o % de frequencia do aluno
            decimal? frequencia = CalcularPercentualFrequencia(cargaHoraria, dadosCalculo.Faltas);
            frequencia = ArredondarPercentualFrequencia(frequencia, dadosCalculo.TipoArredondamento.GetValueOrDefault());

            bool aprovadoNota = dadosCalculo.Nota >= dadosCalculo.NotaMaxima.GetValueOrDefault() * dadosCalculo.PercentualMinimoNota.GetValueOrDefault() / 100F;
            bool aprovadoFrequencia = frequencia >= dadosCalculo.PercentualMinimoFrequencia.GetValueOrDefault();
            bool aprovadoEscala = false;
            bool aprovado = false;

            // Recuperação de variáveis que requerem outras consultas
            if (dadosCalculo.SeqEscalaApuracaoItem.HasValue)
            {
                var dados = EscalaApuracaoItemDomainService.SearchProjectionByKey(new SMCSeqSpecification<EscalaApuracaoItem>(dadosCalculo.SeqEscalaApuracaoItem.Value), p => new
                {
                    p.Aprovado,
                    p.SeqEscalaApuracao
                });
                aprovadoEscala = dados.Aprovado;

                if (!dadosCalculo.SeqEscalaApuracao.HasValue)
                    dadosCalculo.SeqEscalaApuracao = dados.SeqEscalaApuracao;
            }

            if (dadosCalculo.IndicadorApuracaoNota && dadosCalculo.IndicadorApuracaoFrequencia)
                // RN_APR_007.1
                aprovado = aprovadoFrequencia && aprovadoNota;
            else if (dadosCalculo.IndicadorApuracaoNota)
                // RN_APR_007.2
                aprovado = aprovadoNota;
            else if (dadosCalculo.IndicadorApuracaoFrequencia && dadosCalculo.SeqEscalaApuracao.HasValue)
                // RN_APR_007.3
                aprovado = aprovadoFrequencia && aprovadoEscala;
            else if (dadosCalculo.IndicadorApuracaoFrequencia)
                // RN_APR_007.4
                aprovado = aprovadoFrequencia;
            else
                // RN_APR_007.5
                aprovado = aprovadoEscala;

            return aprovado ? SituacaoHistoricoEscolar.Aprovado : SituacaoHistoricoEscolar.Reprovado;
        }

        /// <summary>
        /// Apaga o registro de histórico escolar
        /// </summary>
        /// <param name="seq">Sequencial do registro</param>
        /// <exception cref="SMCInvalidOperationException">Caso o registro tenha uma origem de avaliação (Significa que não foi criado na tela de histórico escolar)</exception>
        public void ExcluirHistoricoEscolarComponente(long seq)
        {
            var historico = SearchByKey(new SMCSeqSpecification<HistoricoEscolar>(seq));
            if (historico.SeqOrigemAvaliacao.HasValue)
                throw new SMCInvalidOperationException();

            DeleteEntity(historico);
        }

        /// <summary>
        /// Excluir historico esclar por aluno historico
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        public void ExcluirHistoricoEscolarPorAlunoHistorico(long seqAlunoHistorico, long seqOrigemAvaliacao)
        {
            // Busca o historico escolar do aluno historico
            var spec = new HistoricoEscolarFilterSpecification()
            {
                SeqAlunoHistorico = seqAlunoHistorico,
                SeqOrigemAvaliacao = seqOrigemAvaliacao
            };
            var historicoEscolar = SearchBySpecification(spec).FirstOrDefault();

            // Exclui históricos encontrado do aluno historico
            DeleteEntity(historicoEscolar);
        }

        /// <summary>
        /// Recupera a obrigatoriedade de matriz conforme o vínculo do aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Obrigatoriedade da matriz conforme o vínculo do aluno</returns>
        public HistoricoEscolarVO BuscarConfiguracaoHistoricoEscolarComponente(long seqAluno)
        {
            var configuracaoHistorico = new HistoricoEscolarVO()
            {
                SeqAluno = seqAluno
            };

            configuracaoHistorico.VinculoAlunoExigeOfertaMatrizCurricular =
                configuracaoHistorico.ConsiderarMatriz =
                BuscarExigenciaOfertaMatrizCurricularAluno(seqAluno);

            return configuracaoHistorico;
        }

        private bool BuscarExigenciaOfertaMatrizCurricularAluno(long seqAluno)
        {
            var configAluno = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqAluno), p => new
            {
                p.Historicos.FirstOrDefault(f => f.Atual).SeqNivelEnsino,
                p.SeqTipoVinculoAluno
            });

            var specNivel = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqNivelEnsino = configAluno.SeqNivelEnsino,
                SeqTipoVinculoAluno = configAluno.SeqTipoVinculoAluno
            };

            return InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionByKey(specNivel, p => p.ExigeOfertaMatrizCurricular);
        }

        /// <summary>
        /// Salva o histórico escolar de uma solicitação de atividade complementar deferida
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de atividade complementar deferida</param>
        public void SalvarHistoricoAtividadeComplementar(long seqSolicitacaoServico)
        {
            // Busca as informações da solicitação de atividade complementar deferida
            var spec = new SMCSeqSpecification<SolicitacaoAtividadeComplementar>(seqSolicitacaoServico);
            var dados = SolicitacaoAtividadeComplementarDomainService.SearchProjectionByKey(spec, x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqCicloLetivo = x.SeqCicloLetivo,
                SeqAlunoHistorico = x.SeqAlunoHistorico,
                SeqComponenteCurricular = x.DivisaoComponente.ConfiguracaoComponente.SeqComponenteCurricular,
                Credito = x.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.Credito,
                CargaHoraria = x.CargaHoraria,
                Situacao = x.SituacaoHistoricoEscolar,
                SeqEscalaApuracaoItem = x.SeqEscalaApuracaoItem,
                Nota = x.Nota,
                Faltas = x.Faltas
            });

            // Se a solicitação não possui o seq-aluno-historico informado, erro
            if (!dados.SeqAlunoHistorico.HasValue)
                throw new SolicitacaoServicoSemAlunoHistoricoException();

            // Busca a ultima matriz curricular do aluno
            var dadosMatriz = AlunoDomainService.BuscarDadosMatrizCurricularAlunoUltimoPlano(dados.SeqPessoaAtuacao);

            // Busca a divisao-matriz-curricular-componente
            var specDiv = new DivisaoMatrizCurricularComponenteFilterSpecification()
            {
                SeqComponenteCurricular = dados.SeqComponenteCurricular,
                SeqMatrizCurricular = dadosMatriz.SeqMatrizCurricular
            };
            var seqCriterioAprovacao = DivisaoMatrizCurricularComponenteDomainService.SearchProjectionByKey(specDiv, x => x.SeqCriterioAprovacao);

            /*	Salvar no histórico escolar do aluno-histórico em questão:
                -	Ciclo letivo: Ciclo letivo preenchido.
                -	Origem avaliação: nula.
                -	Situação histórico escolar: valor do campo “Situação final” calculado na tela.
                -	Escala apuração: sequencial da escala apuração item preenchido automaticamente na tela no campo apuração.
                -	Nota: valor informado na tela.
                -	Falta: valor informado na tela.
                -	Percentual de frequência: Qtd de faltas /CH.
                -	Carga horária realizada: de acordo com o que foi aprovado na solicitação atualizada.
                -	Créditos: quantidade de crédito do componente.
                -	Optativa: se o componente pertencer a um grupo de optativas, setar este campo em 1, senão 0.
                -	Componente curricular: componente curricular referente à configuração de componente selecionada no campo "Atividade complementar" da solicitação.
                -	Componente curricular assunto: nulo.
                -	Critério de aprovação: sequencial do critério associado à configuração do componente na matriz do aluno (matriz do último plano de estudo do aluno).
                -	Grupo curricular: nulo.
                -	Solicitação de serviço: solicitação de serviço em atendimento.
                -	Quantidade de itens: nulo.
                -	Data exame: nulo.*/

            var historico = new HistoricoEscolar
            {
                SeqAlunoHistorico = dados.SeqAlunoHistorico.Value,
                SeqCicloLetivo = dados.SeqCicloLetivo,
                SituacaoHistoricoEscolar = dados.Situacao.GetValueOrDefault(),
                SeqCriterioAprovacao = seqCriterioAprovacao,
                SeqEscalaApuracaoItem = dados.SeqEscalaApuracaoItem,
                SeqSolicitacaoServico = seqSolicitacaoServico,
                Nota = dados.Nota,
                Faltas = dados.Faltas,
                SeqComponenteCurricular = dados.SeqComponenteCurricular,
                CargaHorariaRealizada = dados.CargaHoraria,
                Credito = dados.Credito,
                Optativa = VerificarOptativa(dados.SeqComponenteCurricular, dados.SeqPessoaAtuacao),
            };

            // Calcula o % de frequencia
            historico.PercentualFrequencia = HistoricoEscolarDomainService.CalcularPercentualFrequencia((decimal?)dados.CargaHoraria, dados.Faltas);

            // Salva o histórico escolar
            SaveEntity(historico);
        }

        /// <summary>
        /// Salva o Historico Escolar de uma solicitação de Dispensa Individual
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de dispensa</param>
        internal void SalvarHistoricoDispensaIndividual(long seqSolicitacaoServico)
        {
            // Recupera os dados da solicitação
            var solicitacao = SolicitacaoDispensaDomainService.SearchProjectionByKey(seqSolicitacaoServico, x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqAlunoHistorico = x.SeqAlunoHistorico,
                Destinos = x.Destinos.Select(i => new
                {
                    SeqCicloLetivo = i.SeqCicloLetivo,

                    SeqComponenteCurricular = i.SeqComponenteCurricular,
                    SeqComponenteCurricularAssunto = i.SeqComponenteCurricularAssunto,
                    CargaHoraria = i.ComponenteCurricular.CargaHoraria,
                    Credito = i.ComponenteCurricular.Credito,
                    SeqGrupoCurricular = (long?)i.CurriculoCursoOfertaGrupo.SeqGrupoCurricular,

                    SeqCurriculoCursoOfertaGrupo = i.SeqCurriculoCursoOfertaGrupo,
                    QuantidadeDispensaGrupo = i.QuantidadeDispensaGrupo,
                    FormatoConfiguracaoGrupo = i.CurriculoCursoOfertaGrupo.GrupoCurricular.FormatoConfiguracaoGrupo,
                    GrupoObrigatorio = (bool?)i.CurriculoCursoOfertaGrupo.Obrigatorio,
                })
            });

            // Se a solicitação não possui o seq-aluno-historico informado, erro
            if (!solicitacao.SeqAlunoHistorico.HasValue)
                throw new SolicitacaoServicoSemAlunoHistoricoException();

            // Para cada destino da solicitação de dispensa...
            foreach (var destino in solicitacao.Destinos)
            {
                /*  Cria o HistoricoEscolar:
                 *  - Aluno histórico: Aluno histórico da solicitação de dispensa
                 *  - Ciclo letivo: Ciclo letivo para qual a dispensa está sendo realizada.
                 *  - Situação historico escolar: Dispensado.
                 *  - Solicitação de serviço: solicitação de serviço em atendimento.
                 */
                var historico = new HistoricoEscolar
                {
                    SeqAlunoHistorico = solicitacao.SeqAlunoHistorico.Value,
                    SeqCicloLetivo = destino.SeqCicloLetivo,
                    SituacaoHistoricoEscolar = SituacaoHistoricoEscolar.Dispensado,
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao
                };

                /*  Se o item da dispensa for um componente curricular:
                *  - Componente curricular: componente curricular da dispensa.
                *  - Componente curricular assunto: assunto de componente selecionado na dispensa quando o componente selecionado exige assunto.
                *  - Carga horária realizada: carga horária do componente da dispensa.
                *  - Crédito: quantidade de créditos do componente da dispensa.
                *  - Optativa: se o componente da dispensa pertencer ao um grupo de optativas, setar este campo em 1, senão 0.
                */
                if (destino.SeqComponenteCurricular.HasValue)
                {
                    historico.SeqComponenteCurricular = destino.SeqComponenteCurricular.Value;
                    historico.SeqComponenteCurricularAssunto = destino.SeqComponenteCurricularAssunto;
                    historico.CargaHorariaRealizada = destino.CargaHoraria;
                    historico.Credito = destino.Credito;
                    historico.Optativa = VerificarOptativa(destino.SeqComponenteCurricular.Value, solicitacao.SeqPessoaAtuacao);
                }

                /*  Se o item da dispensa for um grupo curricular:
                 *  - Grupo curricular: grupo curricular da dispensa.
                 *  - Identificar qual é o formato (crédito, ch ou itens) do grupo selecionado e salvar o valor de "Total a ser dispensado" em um dos 3 campos:
                 *      - Quantidade de itens
                 *      - Carga horária realizada
                 *      - Crédito
                 *  - Optativa: se o grupo curricular da dispensa for de optativas, setar este campo em 1, senão 0.
                */
                if (destino.SeqCurriculoCursoOfertaGrupo.HasValue)
                {
                    switch (destino.FormatoConfiguracaoGrupo)
                    {
                        case FormatoConfiguracaoGrupo.CargaHoraria:
                            historico.CargaHorariaRealizada = destino.QuantidadeDispensaGrupo;
                            break;

                        case FormatoConfiguracaoGrupo.Credito:
                            historico.Credito = destino.QuantidadeDispensaGrupo;
                            break;

                        case FormatoConfiguracaoGrupo.Itens:
                            historico.QuantidadeItens = destino.QuantidadeDispensaGrupo;
                            break;
                    }
                    historico.Optativa = !destino.GrupoObrigatorio.Value;
                    historico.SeqGrupoCurricular = destino.SeqGrupoCurricular.Value;
                }

                // Salva o histórico escolar
                SaveEntity(historico);
            }
        }

        /// <summary>
        /// Exclui os históricos escolares que foram criados a partir de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço que deu origem ao histórico escolar</param>
        internal void ExcluirHistoricoEscolarPorSolicitacao(long seqSolicitacaoServico)
        {
            // Busca os históricos escolares que foram criados pela solicitação de serviço
            var spec = new HistoricoEscolarFilterSpecification()
            {
                SeqSoliciacaoServico = seqSolicitacaoServico
            };
            var listaHistoricos = SearchBySpecification(spec);

            // Exclui todos os históricos encontrados
            foreach (var historico in listaHistoricos)
            {
                DeleteEntity(historico);
            }
        }

        /// <summary>
        /// Dados do cabeçalho da consulda de integralização, separado quando colocado o filtro para evitar duas buscas completas
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="visaoAluno">Verifica o sistema que chamou a modal</param>
        /// <returns>Dados do cabeçalho da integralização</returns>
        public IntegralizacaoMatrizCurricularOfertaCabecalhoVO CabecalhoTelaIntegralizacaoCurricularHistorico(long seqPessoaAtuacao, bool visaoAluno)
        {
            //Recuperar os dados da pessoa atuação identificando aluno e ingressante
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            //Recuperar os dados da instituição nível tipo vínculo aluno para organizar de acordo com o parâmetro de conceder formação
            var concedeFormacao = ValidarConcederFormacaoIntegralizacao(seqPessoaAtuacao, dadosOrigem.TipoAtuacao, dadosOrigem.SeqNivelEnsino, dadosOrigem.SeqTipoVinculoAluno);

            //Recuperar os dados informativos do cabeçalho da consulta
            return BuscarIntegralizacaoCabecalho(seqPessoaAtuacao, dadosOrigem.TipoAtuacao, dadosOrigem.SeqMatrizCurricularOferta, visaoAluno, concedeFormacao, dadosOrigem.SeqAlunoHistoricoAtual);
        }

        /// <summary>
        /// Consulta histórico escolar, matriz curricular e plano de estudo para exibir os dados da integralização curricular do aluno/ingressante
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="visaoAluno">Defini o projeto, se for SGA.Aluno exibe apenas nome social</param>
        /// <param name="filtro">Filtro de situação e descrição da configuração de componente</param>
        /// <returns>Objeto de retorno da consulta estruturado com matriz e componentes</returns>
        public IntegralizacaoConsultaHistoricoVO ConsultaIntegralizacaoCurricularHistorico(long seqPessoaAtuacao, bool visaoAluno, IntegralizacaoConsultaHistoricoFiltroVO filtro)
        {
            try
            {
                IntegralizacaoConsultaHistoricoVO retorno = new IntegralizacaoConsultaHistoricoVO();
                List<ComponentesCreditosVO> dadosHistoricoEscolar = new List<ComponentesCreditosVO>();
                List<ComponentesCreditosVO> dadosPlanoEstudo = new List<ComponentesCreditosVO>();
                List<ComponentesCreditosVO> dadosPlanoEstudoAtual = new List<ComponentesCreditosVO>();
                List<ComponentesCreditosVO> dadosPlanoEstudoAntigo = new List<ComponentesCreditosVO>();
                List<ComponentesCreditosVO> dadosHistoricoEscolarSemMatriz = new List<ComponentesCreditosVO>();
                List<IntegralizacaoMatrizCurricularOfertaVO> dadosMatrizOferta = new List<IntegralizacaoMatrizCurricularOfertaVO>();
                List<GrupoCurricularInformacaoVO> dadosBeneficiosCondicoes = new List<GrupoCurricularInformacaoVO>();
                List<GrupoCurricularInformacaoFormacaoVO> dadosFormacoesEspecificas = new List<GrupoCurricularInformacaoFormacaoVO>();
                List<long> dadosBeneficiosPessoaAtuacao = new List<long>();
                List<long> dadosCondicoesPessoaAtuacao = new List<long>();
                List<AlunoFormacaoVO> dadosFormacoesPessoaAtuacao = new List<AlunoFormacaoVO>();
                List<long> seqsComponentesFiltros = new List<long>();
                List<long> seqsComponentesDiferentesFiltros = new List<long>();
                bool validacaoHistoricoSemMatriz = false;

                retorno.HistoricoEscolarComMatriz = new List<IntegralizacaoMatrizDivisaoVO>();
                retorno.HistoricoEscolarSemMatriz = new List<IntegralizacaoHistoricoSemMatrizVO>();

                //Recuperar os dados da pessoa atuação identificando aluno e ingressante
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

                //Recuperar os dados da instituição nível tipo vínculo aluno para organizar de acordo com o parâmetro de conceder formação
                var concedeFormacao = ValidarConcederFormacaoIntegralizacao(seqPessoaAtuacao, dadosOrigem.TipoAtuacao, dadosOrigem.SeqNivelEnsino, dadosOrigem.SeqTipoVinculoAluno);

                //Recuperar os dados informativos do cabeçalho da consulta
                retorno.DadosCabecalho = BuscarIntegralizacaoCabecalho(seqPessoaAtuacao, dadosOrigem.TipoAtuacao, dadosOrigem.SeqMatrizCurricularOferta, visaoAluno, concedeFormacao, dadosOrigem.SeqAlunoHistoricoAtual);

                if (dadosOrigem.SeqAlunoHistoricoAtual > 0)
                {
                    // Recupera os dados do histórico escolar de acordo com a procedure ACADEMICO.APR.st_rel_componentes_historico_escolar
                    dadosHistoricoEscolar = ComponentesCriteriosHistoricoEscolar(dadosOrigem.SeqAlunoHistoricoAtual, true, true, true, true, true);

                    // Recupera os dados do plano de estudo do aluno
                    var cicloAtual = ConfiguracaoEventoLetivoDomainService.BuscarSeqCicloLetivo(DateTime.Today, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                    dadosPlanoEstudo = dadosHistoricoEscolar.Where(w => w.TipoConclusao == TipoConclusao.CursadoSemHistoricoEscolar)
                                                            .Select(w => new ComponentesCreditosVO()
                                                            {
                                                                SeqPlanoEstudo = w.SeqPlanoEstudo,
                                                                SeqComponente = w.SeqComponente,
                                                                CodigoComponente = w.CodigoComponente,
                                                                DescricaoComponente = w.DescricaoComponente,
                                                                SeqComponenteCurricularAssunto = w.SeqComponenteCurricularAssunto,
                                                                DescricaoComponenteAssunto = w.DescricaoComponenteAssunto,
                                                                CargaHoraria = w.CargaHoraria,
                                                                Credito = w.Credito,
                                                                CicloAtual = w.SeqCicloLetivo == cicloAtual,
                                                            }).ToList();

                    // Remover registros que não serão utilizados na integralização
                    dadosHistoricoEscolar = dadosHistoricoEscolar.Where(w => w.TipoConclusao != TipoConclusao.AproveitamentoCredito).ToList();
                    dadosHistoricoEscolar = dadosHistoricoEscolar.Where(w => w.TipoConclusao != TipoConclusao.CursadoSemHistoricoEscolar).ToList();

                    foreach (var historico in dadosHistoricoEscolar)
                    {
                        if (historico.Nota != "Dispensado")
                        {
                            var nota = ArredondarNota(Convert.ToDecimal(historico.Nota));
                            historico.Nota = $"{nota}";
                        }
                    }

                    // Separa os dados dos planos atual e antigos
                    dadosPlanoEstudoAtual = dadosPlanoEstudo.Where(w => w.CicloAtual == true && !dadosHistoricoEscolar.Any(a => a.SeqComponente == w.SeqComponente && a.SeqComponenteCurricularAssunto == w.SeqComponenteCurricularAssunto)).ToList();
                    dadosPlanoEstudoAntigo = dadosPlanoEstudo.Where(w => w.CicloAtual == false && !dadosHistoricoEscolar.Any(a => a.SeqComponente == w.SeqComponente && a.SeqComponenteCurricularAssunto == w.SeqComponenteCurricularAssunto)).ToList();

                    switch (filtro.FiltroSituacaoConfiguracao)
                    {
                        case SituacaoComponenteIntegralizacao.AConcluir:
                            seqsComponentesDiferentesFiltros = dadosHistoricoEscolar.Where(w => w.SeqComponente.HasValue
                                                                                    && (w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado
                                                                                     || w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado
                                                                                     || w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Reprovado)
                                                                                ).Select(s => s.SeqComponente.Value).ToList();

                            var seqsPlanoEstudo = dadosPlanoEstudo.Where(w => w.SeqComponente.HasValue).Select(s => s.SeqComponente.Value).ToList();
                            if (seqsPlanoEstudo != null && seqsPlanoEstudo.Count > 0)
                                seqsComponentesDiferentesFiltros.AddRange(seqsPlanoEstudo);

                            break;

                        case SituacaoComponenteIntegralizacao.EmCurso:
                            seqsComponentesFiltros = dadosPlanoEstudoAtual.Where(w => w.SeqComponente.HasValue).Select(s => s.SeqComponente.Value).ToList();
                            break;

                        case SituacaoComponenteIntegralizacao.Concluido:
                            var dadosGrupoConcluido = dadosHistoricoEscolar.Where(v => v.SeqComponente.HasValue).OrderByDescending(o => o.SeqHistoricoEscolar).GroupBy(g => g.SeqComponente);

                            seqsComponentesFiltros = dadosGrupoConcluido.Where(w => w.FirstOrDefault().SeqComponente.HasValue
                                                                                    && (w.Any(a => a.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado
                                                                                               || a.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado))
                                                                               ).Select(s => s.Key.Value).ToList();
                            break;

                        case SituacaoComponenteIntegralizacao.Reprovado:
                            var dadosGrupoReprovado = dadosHistoricoEscolar.Where(v => v.SeqComponente.HasValue).OrderByDescending(o => o.SeqHistoricoEscolar).GroupBy(g => g.SeqComponente);
                            seqsComponentesFiltros = dadosGrupoReprovado.Where(w => w.FirstOrDefault().SeqComponente.HasValue
                                                                                    && w.FirstOrDefault().SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Reprovado
                                                                                    && !dadosPlanoEstudoAtual.Any(a => a.SeqComponente == w.Key.Value)
                                                                                 ).Select(s => s.Key.Value).ToList();
                            break;

                        case SituacaoComponenteIntegralizacao.AguardandoNota:
                            seqsComponentesFiltros = dadosPlanoEstudoAntigo.Where(w => w.SeqComponente.HasValue).Select(s => s.SeqComponente.Value).ToList();
                            break;

                        case SituacaoComponenteIntegralizacao.Nenhum:
                            break;

                        default:
                            break;
                    }

                    if (filtro.FiltroSituacaoConfiguracao != null && seqsComponentesFiltros.Count == 0 && seqsComponentesDiferentesFiltros.Count == 0)
                        return retorno;
                }

                // Verificar se existe matriz curricular em caso positivo pesquisar a matriz com grupos e componentes
                if (dadosOrigem.SeqMatrizCurricularOferta > 0 && concedeFormacao == true)
                {
                    retorno.MensagemComponentesCursados = $"Os componentes listados a seguir são considerados como extracurriculares, pois não integralizam o currículo: {retorno.DadosCabecalho.OfertaMatriz}.";

                    // Busca os beneficios da pessoa atuação, na busca só pode retornar grupos com o mesmo beneficio ou sem nenhum beneficio
                    foreach (var item in PessoaAtuacaoBeneficioDomainService.BuscarPesssoasAtuacoesBeneficiosIntegralizacao(seqPessoaAtuacao, SituacaoChancelaBeneficio.Deferido))
                    {
                        dadosBeneficiosPessoaAtuacao.Add(item.SeqBeneficio);
                    }

                    // Busca as condições de obrigatóriedade da pessoa atuação, na busca só pode retornar grupos com a mesma obrigatoriedade ou sem nenhuma obrigatoriedade
                    dadosCondicoesPessoaAtuacao = PessoaAtuacaoCondicaoObrigatoriedadeDomainService.BuscarSequenciaisCondicaoObrigatoriedadePessoaAtuacao(seqPessoaAtuacao);

                    // Busca as formações específicas da pessoa atuação, verificação em tabelas diferentes para aluno e ingressante
                    if (dadosOrigem.TipoAtuacao == TipoAtuacao.Aluno)
                        dadosFormacoesPessoaAtuacao = AlunoFormacaoDomainService.BuscarSequenciaisFormacoesAlunoHistorico(dadosOrigem.SeqAlunoHistoricoAtual);
                    else if (dadosOrigem.TipoAtuacao == TipoAtuacao.Ingressante)
                        dadosFormacoesPessoaAtuacao = IngressanteFormacaoEspecificaDomainService.BuscarSequenciaisFormacoesIngressante(seqPessoaAtuacao);

                    List<long> seqsDadosFormacoesPessoaAtuacao = new List<long>();

                    if (dadosFormacoesPessoaAtuacao.Count > 0)
                        seqsDadosFormacoesPessoaAtuacao = FormacaoEspecificaDomainService.BuscarInformacaoTreeFormacaoEspecificaSequenciais(dadosFormacoesPessoaAtuacao, retorno.DadosCabecalho.SeqCurso);

                    //Registro da matriz oferta planificados
                    var filtroMatrizOferta = new MatrizCurricularOfertaIntegralizacaoFiltroVO();
                    filtroMatrizOferta.Seq = dadosOrigem.SeqMatrizCurricularOferta;
                    filtroMatrizOferta.SeqsBeneficios = dadosBeneficiosPessoaAtuacao;
                    filtroMatrizOferta.SeqsCondicoes = dadosCondicoesPessoaAtuacao;
                    filtroMatrizOferta.SeqsFormacoes = seqsDadosFormacoesPessoaAtuacao;
                    filtroMatrizOferta.SeqsComponentesFiltros = seqsComponentesFiltros;
                    filtroMatrizOferta.SeqsComponentesDiferentesFiltros = seqsComponentesDiferentesFiltros;
                    filtroMatrizOferta.DescricaoComponentesFiltro = filtro.FiltroDescricaoConfiguracao;

                    dadosMatrizOferta = MatrizCurricularOfertaDomainService.BuscarIntegralizacaoConfiguracoesMatrizCurricularOferta(filtroMatrizOferta);

                    // Agrupa os valores por divisão da matriz
                    var dadosMatrizDivisao = dadosMatrizOferta.GroupBy(g => g.NumeroDivisao);

                    foreach (var itemDivisao in dadosMatrizDivisao)
                    {
                        // Registro da divisão
                        var registroDivisao = itemDivisao.First();
                        IntegralizacaoMatrizDivisaoVO divisao = registroDivisao.Transform<IntegralizacaoMatrizDivisaoVO>();
                        if (string.IsNullOrEmpty(divisao.DescricaoDivisao))
                            divisao.DescricaoDivisao = "Componentes sem divisão";

                        divisao.Grupos = new List<IntegralizacaoMatrizGrupoVO>();

                        var listGrupos = itemDivisao.Select(s => s.SeqGrupoCurricular).Distinct().ToList();
                        if (listGrupos.Count > 0)
                        {
                            var listTreeGrupos = GrupoCurricularDomainService.BuscarTreeGruposPorGruposFilhos(listGrupos).OrderBy(o => o.OrdenacaoTipoConfiguracaoGrupo).ThenBy(t => t.DescricaoGrupo).ToList();
                            var listTreeGruposPais = listTreeGrupos.Where(w => !w.SeqGrupoCurricularSuperior.HasValue).ToList();
                            var dadosMatrizGrupo = itemDivisao.ToList();
                            var listBeneficios = GrupoCurricularDomainService.BuscarInformacaoGrupoBeneficioCondicoes(listGrupos);
                            var listFormacoes = GrupoCurricularDomainService.BuscarInformacaoGrupoFormacaoEspecifica(listGrupos);
                            var listGrupoDivisao = DivisaoMatrizCurricularDomainService.BuscarIntegralizacaoDivisaoMatrizCurricularGrupo(registroDivisao.SeqDivisaoMatrizCurricular, listGrupos);

                            foreach (var treeGrupoPai in listTreeGruposPais)
                            {
                                IntegralizacaoListasGrupoVO listaGrupos = new IntegralizacaoListasGrupoVO()
                                {
                                    ListaGrupos = listTreeGrupos,
                                    ListaDados = dadosMatrizGrupo,
                                    ListaHistoricoEscolar = dadosHistoricoEscolar,
                                    ListaPlanoEstudoAtual = dadosPlanoEstudoAtual,
                                    ListaPlanoEstudoAntigo = dadosPlanoEstudoAntigo,
                                    ListaBeneficioCondicao = listBeneficios,
                                    ListaFormacaoEspecifica = listFormacoes,
                                    ListaGrupoDivisao = listGrupoDivisao,
                                    ListaBeneficioPessoaAtuacao = dadosBeneficiosPessoaAtuacao,
                                    ListaCondicoesPessoaAtuacao = dadosCondicoesPessoaAtuacao
                                };

                                divisao.Grupos.Add(HierarquiaMatrizIntegralizacaoGrupo(treeGrupoPai.SeqGrupoCurricular, listaGrupos));
                            }
                        }

                        retorno.HistoricoEscolarComMatriz.Add(divisao);
                    }

                    var sequenciais = dadosMatrizOferta.Select(s => s.SeqComponenteCurricular).Distinct().ToList();
                    if (sequenciais.Count > 0)
                    {
                        var dadosHistoricoSemMatriz = dadosHistoricoEscolar.Where(w => !sequenciais.Contains(w.SeqComponente.GetValueOrDefault()) && w.SeqComponente.GetValueOrDefault() != 0).TransformList<IntegralizacaoHistoricoSemMatrizVO>();

                        var dadosPlanoSemMatriz = dadosPlanoEstudoAtual.Where(w => (!sequenciais.Contains(w.SeqComponente.GetValueOrDefault()) || w.SeqComponenteCurricularAssunto.HasValue) && w.SeqComponente.GetValueOrDefault() != 0).TransformList<IntegralizacaoHistoricoSemMatrizVO>();

                        var dadosPlanoSemMatrizAntigo = new List<IntegralizacaoHistoricoSemMatrizVO>();
                        //var dadosPlanoSemMatrizAntigo = dadosPlanoEstudoAntigo.Where(w => !sequenciais.Contains(w.SeqComponente.GetValueOrDefault()) && w.SeqComponente.GetValueOrDefault() != 0).TransformList<IntegralizacaoHistoricoSemMatrizVO>();

                        // Define as situações de integralização curricular
                        dadosHistoricoSemMatriz.ForEach(f => f.SituacaoComponente = SituacaoPorHistorico(f.Transform<ComponentesCreditosVO>()));

                        var historicoSemMatrizOrdenacao = AgruparHistoricosComponentesSemMatriz(dadosHistoricoSemMatriz, dadosPlanoSemMatriz, dadosPlanoSemMatrizAntigo).OrderBy(o => o.SituacaoComponente).ThenBy(t => t.DescricaoComponente).ToList();
                        retorno.HistoricoEscolarSemMatriz.AddRange(historicoSemMatrizOrdenacao);

                        validacaoHistoricoSemMatriz = true;
                    }
                }

                if (retorno.HistoricoEscolarSemMatriz.Count == 0 && !validacaoHistoricoSemMatriz)
                {
                    if (string.IsNullOrEmpty(retorno.MensagemComponentesCursados))
                    {
                        if (concedeFormacao == true)
                            retorno.MensagemComponentesCursados = $"Não foi identificada a Oferta de Matriz Curricular. Abaixo segue os componentes curriculares com registros no Histórico Escolar e/ou Plano de Estudos.";
                        else
                            retorno.MensagemComponentesCursados = $"Os componentes curriculares listados a seguir referem-se aos componentes que possuem registros no Histórico Escolar e/ou Plano de Estudos.";
                    }

                    var dadosHistoricoSemMatriz = dadosHistoricoEscolar.TransformList<IntegralizacaoHistoricoSemMatrizVO>();

                    var dadosPlanoSemMatriz = dadosPlanoEstudoAtual.TransformList<IntegralizacaoHistoricoSemMatrizVO>();

                    var dadosPlanoSemMatrizAntigo = new List<IntegralizacaoHistoricoSemMatrizVO>();// dadosPlanoEstudoAntigo.TransformList<IntegralizacaoHistoricoSemMatrizVO>();

                    // Define as situações de integralização curricular
                    dadosHistoricoSemMatriz.ForEach(f => f.SituacaoComponente = SituacaoPorHistorico(f.Transform<ComponentesCreditosVO>()));

                    var historicoSemMatrizOrdenacao = AgruparHistoricosComponentesSemMatriz(dadosHistoricoSemMatriz, dadosPlanoSemMatriz, dadosPlanoSemMatrizAntigo).OrderBy(o => o.SituacaoComponente).ThenBy(t => t.DescricaoComponente).ToList();
                    retorno.HistoricoEscolarSemMatriz.AddRange(historicoSemMatrizOrdenacao);
                }

                // Verificação dos filtros de pesquisa para componentes fora da matriz curricular do aluno
                if (seqsComponentesFiltros.Count > 0)
                    retorno.HistoricoEscolarSemMatriz = retorno.HistoricoEscolarSemMatriz.Where(w => seqsComponentesFiltros.Contains(w.SeqComponente)).ToList();

                if (seqsComponentesDiferentesFiltros.Count > 0)
                    retorno.HistoricoEscolarSemMatriz = retorno.HistoricoEscolarSemMatriz.Where(w => !seqsComponentesDiferentesFiltros.Contains(w.SeqComponente)).ToList();

                if (!string.IsNullOrEmpty(filtro.FiltroDescricaoConfiguracao))
                    retorno.HistoricoEscolarSemMatriz = retorno.HistoricoEscolarSemMatriz.Where(w => w.DescricaoComponente.ToUpper().SMCRemoveAccents().Contains(filtro.FiltroDescricaoConfiguracao.ToUpper().SMCRemoveAccents())).ToList();

                // Definir o tipo de componente curricular de todos os componentes sem matriz
                ObterTipoComponenteCurricularIntegralizacao(retorno.HistoricoEscolarSemMatriz);

                // Informa o percentual de cada grupo
                foreach (var divisao in retorno.HistoricoEscolarComMatriz)
                {
                    foreach (var grupo in divisao.Grupos)
                    {
                        AtualizarPercentualGrupo(grupo, retorno.DadosCabecalho.DadosConclusaoCursoAluno);
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método recursivo para atualizar o percentual de conclusão do grupo e seus sub-grupos
        /// </summary>
        /// <param name="grupo">Grupo a ser atualizado</param>
        /// <param name="percentuais">Percentuais de cada grupo já calculado</param>
        private void AtualizarPercentualGrupo(IntegralizacaoMatrizGrupoVO grupo, CalculoConclusaoCursoAlunoVO percentuais)
        {
            grupo.PercentualConclusaoGrupo = percentuais.PercentualGrupo.FirstOrDefault(x => x.SeqGrupoCurricular == grupo.SeqGrupoCurricular)?.PercentualConclusaoGrupo;
            foreach (var grupoFilho in grupo.GruposFilhos)
            {
                AtualizarPercentualGrupo(grupoFilho, percentuais);
            }
        }

        /// <summary>
        /// Buscar dados para o cabeçalho da tela de consulta de integralização
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="tipoAtuacao">tipo de atuação</param>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <returns>Dados para exibir no cabeçalho da consulta</returns>
        public IntegralizacaoMatrizCurricularOfertaCabecalhoVO BuscarIntegralizacaoCabecalho(long seqPessoaAtuacao, TipoAtuacao tipoAtuacao, long seqMatrizCurricularOferta, bool visaoAluno, bool concedeFormacao, long? seqAlunoHistorico)
        {
            IntegralizacaoMatrizCurricularOfertaCabecalhoVO retorno = new IntegralizacaoMatrizCurricularOfertaCabecalhoVO();

            if (tipoAtuacao == TipoAtuacao.Aluno)
            {
                retorno = AlunoHistoricoDomainService.SearchProjectionByKey(new AlunoHistoricoFilterSpecification() { SeqAluno = seqPessoaAtuacao, Atual = true },
                          p => new IntegralizacaoMatrizCurricularOfertaCabecalhoVO
                          {
                              SeqAluno = p.SeqAluno,
                              RA = p.Aluno.NumeroRegistroAcademico,
                              CodigoMigracao = p.Aluno.CodigoAlunoMigracao,
                              NomeRegistro = p.Aluno.DadosPessoais.Nome,
                              NomeSocial = p.Aluno.DadosPessoais.NomeSocial,
                              Situacao = p.HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault(c => !c.DataExclusao.HasValue).AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Today && !s.DataExclusao.HasValue).SituacaoMatricula.Descricao,
                              Vinculo = p.Aluno.TipoVinculoAluno.Descricao,
                              ExibirDisciplinaIsolada = p.Aluno.TipoVinculoAluno.TipoVinculoAlunoFinanceiro == Financeiro.Common.Areas.GRA.Enums.TipoVinculoAlunoFinanceiro.RegimeDisciplinaIsolada,
                              VinculoDisciplinaIsolada = p.EntidadeVinculo.Nome,
                              DescricaoFormaIngresso = p.FormaIngresso.Descricao,
                              OfertaCurso = p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                              Localidade = p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                              Turno = p.CursoOfertaLocalidadeTurno.Turno.Descricao,
                          });
            }
            else if (tipoAtuacao == TipoAtuacao.Ingressante)
            {
                var dataInclusao = DateTime.Today.AddDays(1);
                retorno = IngressanteDomainService.SearchProjectionByKey(seqPessoaAtuacao,
                         p => new IntegralizacaoMatrizCurricularOfertaCabecalhoVO
                         {
                             SeqIngressante = p.Seq,
                             RA = p.Seq,
                             NomeRegistro = p.DadosPessoais.Nome,
                             NomeSocial = p.DadosPessoais.NomeSocial,
                             SituacaoIngressante = p.HistoricosSituacao.OrderByDescending(o => o.DataInclusao).FirstOrDefault(s => s.DataInclusao <= dataInclusao).SituacaoIngressante,
                             Vinculo = p.TipoVinculoAluno.Descricao,
                             DescricaoFormaIngresso = p.FormaIngresso.Descricao,
                             OfertaCampanha = p.Ofertas.FirstOrDefault().CampanhaOferta.Descricao,
                         });
            }

            if (seqMatrizCurricularOferta > 0 && concedeFormacao == true)
            {
                var matrizCurricularOferta = MatrizCurricularOfertaDomainService.SearchProjectionByKey(seqMatrizCurricularOferta,
                                             p => new
                                             {
                                                 p.Seq,
                                                 p.MatrizCurricular.Descricao,
                                                 p.MatrizCurricular.DescricaoComplementar,
                                                 Localidade = p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                                 Turno = p.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                                 HistoricoSituacao = p.HistoricosSituacao.Select(h => new { h.DataInicio, h.DataFim, h.SituacaoMatrizCurricularOferta }).ToList(),
                                                 SeqCurso = p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                                             });

                var situacaoAtual = matrizCurricularOferta.HistoricoSituacao.FirstOrDefault(f => f.DataInicio <= DateTime.Today && (f.DataFim ?? DateTime.MaxValue) >= DateTime.Today)?.SituacaoMatrizCurricularOferta ?? SituacaoMatrizCurricularOferta.Nenhum;

                retorno.OfertaMatriz = MatrizCurricularOfertaDomainService.GerarDescricaoMatrizCurricuarOferta(matrizCurricularOferta.Descricao, matrizCurricularOferta.DescricaoComplementar, matrizCurricularOferta.Localidade, matrizCurricularOferta.Turno);
                retorno.SituacaoOfertaMatriz = SMCEnumHelper.GetDescription(situacaoAtual);
                retorno.ExibirOfertaMatriz = true;
                retorno.SeqCurso = matrizCurricularOferta.SeqCurso;
            }

            //Defini a visão de acordo com o projeto
            retorno.VisaoAluno = visaoAluno;

            //Recupera todos os tipos de componentes curriculares para legenda
            retorno.TiposComponentesCurriculares = TipoComponenteCurricularDomainService.BuscarTiposComponentesCurricularesIntegralizacao();

            // Chama a rotina que calcula o percentual de conclusão de cada grupo curricular.
            if (seqAlunoHistorico.HasValue)
            {
                retorno.DadosConclusaoCursoAluno = AcademicoRepository.CalcularPercentualConclusaoCursoAluno(seqAlunoHistorico.Value);
            }

            return retorno;
        }

        /// <summary>
        /// Retorna uma lista com os componentes curriculares que já estão aprovados ou dispensados da lista informada como parâmetro
        /// </summary>
        /// <param name="seqAlunoHistoricoAtual">Histórico atual do aluno</param>
        /// <param name="componentesComAssunto">Componentes curriculares</param>
        /// <param name="seqCicloLetivoIgnorar">Sequencial do ciclo letivo que deve ser ignorado. Útil quando for validar se já cursou a disciplina em ciclo letivo diferente do atual, por exemplo</param>
        /// <returns>Componentes aprovados ou dispensados</returns>
        public List<HistoricoEscolarAprovadoVO> ObterComponentesCurricularesAprovadosDispensados(long seqAlunoHistoricoAtual, List<HistoricoEscolarAprovadoFiltroVO> componentesComAssunto, long? seqCicloLetivoIgnorar)
        {
            List<HistoricoEscolarAprovadoVO> historicoComponentesAprovadosDispensados = new List<HistoricoEscolarAprovadoVO>();
            foreach (var itemComponenteAssunto in componentesComAssunto)
            {
                var spec = new HistoricoEscolarFilterSpecification()
                {
                    SeqAlunoHistorico = seqAlunoHistoricoAtual,
                    SeqComponenteCurricular = itemComponenteAssunto.SeqComponenteCurricular,
                    SeqComponenteCurricularAssunto = itemComponenteAssunto.SeqComponenteCurricularAssunto,
                    SituacaoHistoricoEscolar = SituacaoHistoricoEscolar.Aprovado,
                    SeqCicloLetivoDiferente = seqCicloLetivoIgnorar
                };

                // Recupera todos os históricos escolares dos componentes selecionados
                // Que estão aprovados diferente do ciclo letivo do processo
                var historicoComponenteAprovado = ObterHistoricoEscolarComponenteAprovadoDispensado(spec);
                if (historicoComponenteAprovado != null)
                {
                    historicoComponentesAprovadosDispensados.Add(MontarHistoricoEscolarComponenteAprovadoDispensado(historicoComponenteAprovado));
                }

                // Recupera todos os históricos escolares dos componentes selecionados
                // Que estão dispensados em qualquer ciclo letivo
                spec.SeqCicloLetivoDiferente = null;
                spec.SituacaoHistoricoEscolar = SituacaoHistoricoEscolar.Dispensado;
                var historicoComponeteDispensado = ObterHistoricoEscolarComponenteAprovadoDispensado(spec);
                if (historicoComponeteDispensado != null)
                {
                    historicoComponentesAprovadosDispensados.Add(MontarHistoricoEscolarComponenteAprovadoDispensado(historicoComponeteDispensado));
                }
            }
            return historicoComponentesAprovadosDispensados;
        }

        private HistoricoEscolarComponentesAprovadoDispensadoVO ObterHistoricoEscolarComponenteAprovadoDispensado(HistoricoEscolarFilterSpecification spec)
        {
            HistoricoEscolarComponentesAprovadoDispensadoVO retorno = SearchProjectionByKey(spec, p => new HistoricoEscolarComponentesAprovadoDispensadoVO
            {
                DescricaoComponente = p.ComponenteCurricular.Descricao,
                DescricaoAssunto = p.ComponenteCurricularAssunto.Descricao,
                SeqComponenteCurricular = p.SeqComponenteCurricular,
                SeqComponenteCurricularAssunto = p.SeqComponenteCurricularAssunto
            });

            return retorno;
        }

        private HistoricoEscolarAprovadoVO MontarHistoricoEscolarComponenteAprovadoDispensado(HistoricoEscolarComponentesAprovadoDispensadoVO historicoEscolarAprovadoDispensadoVO)
        {
            var retorno = new HistoricoEscolarAprovadoVO();
            retorno.SeqComponenteCurricular = historicoEscolarAprovadoDispensadoVO.SeqComponenteCurricular;
            retorno.SeqComponenteCurricularAssunto = historicoEscolarAprovadoDispensadoVO.SeqComponenteCurricularAssunto;
            if (string.IsNullOrEmpty(historicoEscolarAprovadoDispensadoVO.DescricaoAssunto))
            {
                retorno.DescricaoComponenteCurricularAprovado = $"{historicoEscolarAprovadoDispensadoVO.DescricaoComponente}";
            }
            else
            {
                retorno.DescricaoComponenteCurricularAprovado = $"{historicoEscolarAprovadoDispensadoVO.DescricaoComponente} ({historicoEscolarAprovadoDispensadoVO.DescricaoAssunto})";
            }
            return retorno;
        }

        public List<HistoricoEscolarDispensadoVO> ObterComponentesCurricularesDispensados(long seqAlunoHistorico)
        {
            var spec = new HistoricoEscolarFilterSpecification()
            {
                SeqAlunoHistorico = seqAlunoHistorico,
                SituacaoHistoricoEscolar = SituacaoHistoricoEscolar.Dispensado
            };

            //Recupera todos os históricos escolares dos componentes dispensados
            var componentesDispensados = SearchProjectionBySpecification(spec, x => new HistoricoEscolarDispensadoVO
            {
                SeqComponenteCurricular = x.SeqComponenteCurricular,
                DescricaoComponenteCurricularDispensado = x.ComponenteCurricular.Descricao
            }).ToList();

            return componentesDispensados;
        }

        /// <summary>
        /// Verificar o parâmetro conceder formação de acordo com o tipo de vínculo ou com o tipo de intercambio
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="tipoAtuacao">tipo de atuação</param>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <param name="seqTipoVinculoAluno">Sequencial do tipo de vínculo</param>
        /// <returns>Retorna o valor do parâmetro conceder formação</returns>
        public bool ValidarConcederFormacaoIntegralizacao(long seqPessoaAtuacao, TipoAtuacao tipoAtuacao, long seqNivelEnsino, long seqTipoVinculoAluno)
        {
            var configVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(seqNivelEnsino, seqTipoVinculoAluno);

            if (!configVinculo.ConcedeFormacao)
            {
                // Tipo de termo que concendem formação na configuração do vínculo do ingressante
                var tiposTermoFormacao = configVinculo.TiposTermoIntercambio.Where(w => w.ConcedeFormacao).Select(s => s.Seq).ToArray();
                var specTermos = new SMCContainsSpecification<TermoIntercambio, long>(p => p.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio, tiposTermoFormacao);
                var seqsTermos = TermoIntercambioDomainService.SearchProjectionBySpecification(specTermos, p => p.Seq).ToList();

                // Se possui termos de intercambio verificar o parâmetros dos termos
                List<PessoaAtuacaoTermoIntercambio> termos = new List<PessoaAtuacaoTermoIntercambio>();
                if (tipoAtuacao == TipoAtuacao.Aluno)
                    termos = AlunoDomainService.SearchProjectionByKey(seqPessoaAtuacao, p => new { p.TermosIntercambio }).TermosIntercambio.ToList();
                else if (tipoAtuacao == TipoAtuacao.Ingressante)
                    termos = IngressanteDomainService.SearchProjectionByKey(seqPessoaAtuacao, p => new { p.TermosIntercambio }).TermosIntercambio.ToList();

                if (termos.Count > 0)
                    return termos.Count(c => seqsTermos.Contains(c.SeqTermoIntercambio)) > 0;
            }

            return configVinculo.ConcedeFormacao;
        }

        /// <summary>
        /// Componentes currículares do histórico escolar com critérios de aprovação.
        /// </summary>
        /// <param name="seqAlunoHistoricoAtual">Sequencial do aluno historico atual</param>
        /// <param name="exibirReprovados">Exibe os componentes reprovados</param>
        /// <param name="exibeCreditoZero">Exibe os componentes com crédito igual a 0</param>
        /// <param name="exibeComponenteCursadoSemHistorico">Exibe os componentes que foram cursados e que não tem lançamento no histórico ainda</param>
        /// <param name="exibeExame">Exibe os componentes de gestão de exame</param>
        /// <param name="ignoraFlagDispensaCursado">Ignora o flag de dispensa para exibir como "Componente concluido"</param>
        /// <returns>Lista de objetos da procedure ACADEMICO.APR.st_rel_componentes_historico_escolar </returns>
        public List<ComponentesCreditosVO> ComponentesCriteriosHistoricoEscolar(long seqAlunoHistoricoAtual, bool exibirReprovados, bool exibeCreditoZero = true, bool exibeComponenteCursadoSemHistorico = false, bool exibeExame = false, bool ignoraFlagDispensaCursado = false)
        {
            // Verifica se informou o aluno histórico
            if (seqAlunoHistoricoAtual == 0)
                return new List<ComponentesCreditosVO>();

            // Busca a situação de deferimento da solicitação de dispensa
            string queryTemplate = string.Format(_buscarIntegralizacaoDispensaSGFTemplate, seqAlunoHistoricoAtual);
            var seqsTemplateSGF = RawQuery<long?>(queryTemplate);
            var seqTemplateSGF = seqsTemplateSGF.FirstOrDefault();
            long etapaFinalSucesso = 0;
            if (seqTemplateSGF != null)
            {
                EtapaSimplificadaData[] etapasSGF = SGFHelper.BuscarEtapasSGFCache(seqTemplateSGF.Value);
                etapaFinalSucesso = etapasSGF.SelectMany(e => e.Situacoes).FirstOrDefault(s => s.SituacaoFinalProcesso && s.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoComSucesso).Seq;
            }
            string textoEtapaFinalSucesso = etapaFinalSucesso > 0 ? etapaFinalSucesso.ToString() : "NULL";

            // Chama a procedure
            var chamadaProcedure = string.Format(_procedureComponentesHistoricosIntegralizacao, seqAlunoHistoricoAtual, exibeCreditoZero, exibirReprovados, exibeComponenteCursadoSemHistorico, exibeExame, ignoraFlagDispensaCursado, textoEtapaFinalSucesso);
            return RawQuery<ComponentesCreditosVO>(chamadaProcedure);
        }

        /// <summary>
        /// Monta hierarquia de grups para integralização curricular.
        /// </summary>
        /// <param name="seqGrupo">Sequencial do grupo analisado</param>
        /// <param name="grupoListas">Listas com dados para preencher informações</param>
        /// <returns>Objeto grupo preenchido com a tree de grupo e configurações</returns>
        private IntegralizacaoMatrizGrupoVO HierarquiaMatrizIntegralizacaoGrupo(long seqGrupo, IntegralizacaoListasGrupoVO grupoListas)
        {
            var grupo = new IntegralizacaoMatrizGrupoVO();

            var listaGruposFilhos = grupoListas.ListaGrupos.Where(a => a.SeqGrupoCurricularSuperior == seqGrupo).ToList();
            if (listaGruposFilhos.Count > 0)
            {
                grupo = grupoListas.ListaGrupos.First(f => f.SeqGrupoCurricular == seqGrupo).Transform<IntegralizacaoMatrizGrupoVO>();
                grupo.GruposFilhos = new List<IntegralizacaoMatrizGrupoVO>();
                grupo.Configuracoes = new List<IntegralizacaoMatrizConfiguracaoVO>();

                foreach (var itemGrupo in listaGruposFilhos)
                    grupo.GruposFilhos.Add(HierarquiaMatrizIntegralizacaoGrupo(itemGrupo.SeqGrupoCurricular, grupoListas));

                return grupo;
            }
            else
            {
                grupo = grupoListas.ListaDados.First(f => f.SeqGrupoCurricular == seqGrupo).Transform<IntegralizacaoMatrizGrupoVO>();
                grupo.GruposFilhos = new List<IntegralizacaoMatrizGrupoVO>();
                grupo.Configuracoes = new List<IntegralizacaoMatrizConfiguracaoVO>();

                var listaBeneficiosCondicoes = grupoListas.ListaBeneficioCondicao.Where(w => w.SeqGrupoCurricular == seqGrupo).ToList();
                if (listaBeneficiosCondicoes.Count > 0 && grupoListas.ListaBeneficioPessoaAtuacao.Count > 0)
                    grupo.Beneficios = listaBeneficiosCondicoes.Where(w => w.SeqBeneficio.HasValue && grupoListas.ListaBeneficioPessoaAtuacao.Contains(w.SeqBeneficio.GetValueOrDefault())).ToList();

                if (listaBeneficiosCondicoes.Count > 0 && grupoListas.ListaCondicoesPessoaAtuacao.Count > 0)
                    grupo.CondicoesObrigatorias = listaBeneficiosCondicoes.Where(w => w.SeqCondicaoObrigatoriedade.HasValue && grupoListas.ListaCondicoesPessoaAtuacao.Contains(w.SeqCondicaoObrigatoriedade.GetValueOrDefault())).ToList();

                var formacaoEspecifica = grupoListas.ListaFormacaoEspecifica.FirstOrDefault(w => w.SeqFormacaoEspecifica == grupo.SeqFormacaoEspecifica && w.Level == 0);
                if (formacaoEspecifica != null)
                {
                    grupo.FormacoesEspecificas = new List<GrupoCurricularInformacaoFormacaoVO>();
                    grupo.FormacoesEspecificas.Add(formacaoEspecifica);
                    long? seqFormacaoSuperior = formacaoEspecifica.SeqFormacaoEspecificaSuperior;

                    while (seqFormacaoSuperior.HasValue)
                    {
                        var formacaoEspecificaPai = grupoListas.ListaFormacaoEspecifica.FirstOrDefault(w => w.SeqFormacaoEspecifica == seqFormacaoSuperior.Value);
                        grupo.FormacoesEspecificas.Insert(0, formacaoEspecificaPai);
                        seqFormacaoSuperior = formacaoEspecificaPai.SeqFormacaoEspecificaSuperior;
                    }
                }

                var listaGruposDivisoes = grupoListas.ListaGrupoDivisao.Where(w => w.SeqGrupoCurricular == grupo.SeqGrupoCurricular).ToList();
                if (listaGruposDivisoes.Count > 0)
                    grupo.GruposDivisoes = listaGruposDivisoes.Select(s => $"{s.DescricaoDivisao} - {s.ConfiguracaoGrupo}").ToList();

                var grupoDispensa = grupoListas.ListaHistoricoEscolar.Where(w => w.SeqGrupoCurricular == seqGrupo && w.SeqSolicitacaoServico.HasValue).ToList();
                if (grupoDispensa != null && grupoDispensa.Count > 0)
                {
                    var grupoConfiguracao = grupoListas.ListaGrupos.FirstOrDefault(f => grupoDispensa.Any(c => c.SeqGrupoCurricular == f.SeqGrupoCurricular));
                    if (grupoConfiguracao != null)
                    {
                        if (grupoConfiguracao.TokenTipoConfiguracaoGrupo == TOKEN_TIPO_CONFIGURACAO_GRUPO_CURRICULAR.TKN_TODOS_OBRIGATORIOS)
                            grupo.MensagemDispensaGrupo = $"Dispensado {grupoDispensa.Sum(s => s.QuantidadeItens)} itens, através do deferimento da solicitação";
                        else if (grupoConfiguracao.FormatoConfiguracaoGrupo == FormatoConfiguracaoGrupo.CargaHoraria)
                            grupo.MensagemDispensaGrupo = $"Dispensado {grupoDispensa.Sum(s => s.CargaHoraria)} horas, através do deferimento da solicitação";
                        else if (grupoConfiguracao.FormatoConfiguracaoGrupo == FormatoConfiguracaoGrupo.Credito)
                            grupo.MensagemDispensaGrupo = $"Dispensado {grupoDispensa.Sum(s => s.Credito)} créditos, através do deferimento da solicitação";
                        else
                            grupo.MensagemDispensaGrupo = $"Dispensado {grupoDispensa.Sum(s => s.QuantidadeItens)} itens, através do deferimento da solicitação";
                    }

                    grupo.ProtocoloDispensaGrupo = new List<string>();
                    grupo.SeqSolicitacaoServico = new List<long?>();

                    foreach (var dispensa in grupoDispensa)
                    {
                        var protocolo = SolicitacaoServicoDomainService.BuscarNumeroProtocoloSolicitacaoServico(dispensa.SeqSolicitacaoServico.Value);
                        grupo.ProtocoloDispensaGrupo.Add(protocolo);
                        grupo.SeqSolicitacaoServico.Add(dispensa.SeqSolicitacaoServico);
                    }
                }

                var listaConfiguracoes = grupoListas.ListaDados.Where(w => w.SeqGrupoCurricular == seqGrupo).ToList();
                foreach (var item in listaConfiguracoes)
                {
                    var historicos = grupoListas.ListaHistoricoEscolar.Where(f => f.SeqComponente == item.SeqComponenteCurricular)
                        .OrderByDescending(o => o.SeqHistoricoEscolar)
                        .GroupBy(g => g.SeqComponenteCurricularAssunto).ToList();

                    foreach (var historico in historicos)
                    {
                        IntegralizacaoMatrizConfiguracaoVO configuracaoH = item.Transform<IntegralizacaoMatrizConfiguracaoVO>();
                        configuracaoH.SiglaComponente = item.SiglaTipoComponenteCurricular;
                        configuracaoH.CargaHoraria = item.CargaHoraria;
                        configuracaoH.Credito = item.Credito;
                        configuracaoH.SituacaoComponente = SituacaoComponenteIntegralizacao.AConcluir;

                        if (historico.FirstOrDefault().SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado || historico.FirstOrDefault().SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado)
                            configuracaoH.SituacaoComponente = SituacaoComponenteIntegralizacao.Concluido;
                        else
                            configuracaoH.SituacaoComponente = SituacaoPorHistorico(historico.FirstOrDefault());

                        configuracaoH.SeqHistoricoEscolarUltimo = historico.FirstOrDefault().SeqHistoricoEscolar;
                        configuracaoH.SeqsHistoricosEscolar = historico.Select(s => s.SeqHistoricoEscolar.Value).ToList();
                        //configuracaoH.SeqsHistoricosEscolar.Add(historico.SeqHistoricoEscolar.Value);
                        configuracaoH.ExibirInformacao = configuracaoH.SituacaoComponente == SituacaoComponenteIntegralizacao.Concluido || configuracaoH.SituacaoComponente == SituacaoComponenteIntegralizacao.Reprovado;

                        if (!string.IsNullOrEmpty(historico.FirstOrDefault().DescricaoComponenteAssunto))
                            configuracaoH.DescricaoConfiguracao = $"{configuracaoH.DescricaoConfiguracao} : {historico.FirstOrDefault().DescricaoComponenteAssunto}";

                        grupo.Configuracoes.Add(configuracaoH);
                    }

                    if (historicos.Count > 0)
                        continue;

                    IntegralizacaoMatrizConfiguracaoVO configuracao = item.Transform<IntegralizacaoMatrizConfiguracaoVO>();
                    configuracao.SiglaComponente = item.SiglaTipoComponenteCurricular;
                    configuracao.CargaHoraria = item.CargaHoraria;
                    configuracao.Credito = item.Credito;
                    configuracao.SituacaoComponente = SituacaoComponenteIntegralizacao.AConcluir;
                    //if (historico.Count > 0)
                    //{
                    //    if (historico.Any(a => a.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado || a.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado))
                    //        configuracao.SituacaoComponente = SituacaoComponenteIntegralizacao.Concluido;
                    //    else
                    //        configuracao.SituacaoComponente = SituacaoPorHistorico(historico.FirstOrDefault());

                    //    configuracao.SeqsHistoricosEscolar = historico.Where(s => s.SeqHistoricoEscolar.HasValue).Select(s => s.SeqHistoricoEscolar.GetValueOrDefault()).ToList();
                    //    configuracao.SeqHistoricoEscolarUltimo = historico.FirstOrDefault().SeqHistoricoEscolar;
                    //}

                    configuracao.ExibirInformacao = configuracao.SituacaoComponente == SituacaoComponenteIntegralizacao.Concluido || configuracao.SituacaoComponente == SituacaoComponenteIntegralizacao.Reprovado;

                    var planoEstudoAtual = grupoListas.ListaPlanoEstudoAtual.FirstOrDefault(f => f.SeqComponente == item.SeqComponenteCurricular);
                    if (planoEstudoAtual != null)
                    {
                        configuracao.SeqPlanoEstudo = planoEstudoAtual.SeqPlanoEstudo;
                        configuracao.ExibirInformacao = true;
                        if (configuracao.SituacaoComponente != SituacaoComponenteIntegralizacao.Concluido)
                        {
                            /* Removido porque sempre que for EmCurso Exibe Informação*/
                            ////Se foi reprovado no histórico mas esta cursando o componente exibe as informações
                            //if (configuracao.SituacaoComponente == SituacaoComponenteIntegralizacao.Reprovado)
                            //    configuracao.ExibirInformacao = true;
                            //else if (ComponenteCurricularDomainService.ValidarComponenteCurricularExigeAssunto(null, configuracao.SeqComponenteCurricular))
                            //    configuracao.ExibirInformacao = true;

                            configuracao.SituacaoComponente = SituacaoComponenteIntegralizacao.EmCurso;
                        }
                    }

                    //Planos estudos antigos sem histórico escolar com situação igual "À Concluir" recebe situação "Aguardando Nota"
                    var planoEstudoAntigo = grupoListas.ListaPlanoEstudoAntigo.FirstOrDefault(f => f.SeqComponente == item.SeqComponenteCurricular);
                    if (planoEstudoAntigo != null && configuracao.SituacaoComponente == SituacaoComponenteIntegralizacao.AConcluir)
                    {
                        configuracao.SeqPlanoEstudoAntigo = planoEstudoAntigo.SeqPlanoEstudo;
                        configuracao.SituacaoComponente = SituacaoComponenteIntegralizacao.AguardandoNota;
                        configuracao.ExibirInformacao = true;
                    }

                    grupo.Configuracoes.Add(configuracao);
                }

                grupo.Configuracoes = grupo.Configuracoes.OrderBy(o => ((SituacaoComponenteIntegralizacaoOrderBy)o.SituacaoComponente).SMCGetDescription()).ThenBy(t => t.DescricaoConfiguracao).ToList();

                return grupo;
            }
        }

        /// <summary>
        /// Define a situação da integralização curricular de acordo com historico escolar
        /// </summary>
        /// <param name="historico">Objeto de histórico escolar</param>
        /// <returns>Situação da integralização curricular</returns>
        private SituacaoComponenteIntegralizacao SituacaoPorHistorico(ComponentesCreditosVO historico)
        {
            switch (historico.SituacaoHistoricoEscolar)
            {
                case SituacaoHistoricoEscolar.Nenhum:
                    return SituacaoComponenteIntegralizacao.AConcluir;

                case SituacaoHistoricoEscolar.Aprovado:
                    return SituacaoComponenteIntegralizacao.Concluido;

                case SituacaoHistoricoEscolar.Dispensado:
                    return SituacaoComponenteIntegralizacao.Concluido;

                case SituacaoHistoricoEscolar.Reprovado:
                    return SituacaoComponenteIntegralizacao.Reprovado;

                case SituacaoHistoricoEscolar.AlunoSemNota:
                    return SituacaoComponenteIntegralizacao.AguardandoNota;

                default:
                    return SituacaoComponenteIntegralizacao.AConcluir;
            }
        }

        /// <summary>
        /// Busca os históricos escolares para modal de integralização curricular
        /// </summary>
        /// <param name="seqsHistoricoEscolar">Sequenciais concatenados por ,</param>
        /// <returns>Dados do histórico escolar do aluno</returns>
        public List<HistoricoEscolarListaVO> BuscarHistoricosEscolaresIntegralizacao(string seqsHistoricoEscolar)
        {
            HistoricoEscolarFilterSpecification spec = new HistoricoEscolarFilterSpecification();
            spec.Seqs = Array.ConvertAll(seqsHistoricoEscolar.Split(','), long.Parse);
            spec.SetOrderByDescending(o => o.Seq);

            var historicoEscolar = SearchProjectionBySpecification(spec, p => new HistoricoEscolarListaVO()
            {
                Seq = p.Seq,
                SeqComponenteCurricular = p.SeqComponenteCurricular,
                SeqComponenteCurricularAssunto = p.SeqComponenteCurricularAssunto,
                CodigoComponenteCurricular = p.ComponenteCurricular.Codigo,
                DescricaoComponenteCurricular = p.ComponenteCurricular.Descricao,
                SiglaComponente = p.ComponenteCurricular.TipoComponente.Sigla,
                DescricaoAssunto = p.ComponenteCurricularAssunto.Descricao,
                SeqSolicitacaoDispensa = ((SolicitacaoDispensa)p.SolicitacaoServico) != null ? p.SeqSolicitacaoServico : null,
                ProtocoloSolicitacaoDispensa = ((SolicitacaoDispensa)p.SolicitacaoServico) != null ? p.SolicitacaoServico.NumeroProtocolo : null,
                DataSolicitacaoDispensa = ((SolicitacaoDispensa)p.SolicitacaoServico) != null ? p.SolicitacaoServico.DataSolicitacao : (DateTime?)null,
                DescricaoCicloLetivo = p.CicloLetivo.Descricao,
                CargaHoraria = p.CargaHorariaRealizada,
                Creditos = p.Credito,
                ObrigatorioOptativo = p.Optativa ? ObrigatorioOptativo.OP : ObrigatorioOptativo.OB,
                Nota = p.Nota,
                DescricaoConceito = p.EscalaApuracaoItem.Descricao,
                Faltas = p.Faltas,
                SituacaoHistoricoEscolar = p.SituacaoHistoricoEscolar,
                SeqAlunoHistorico = p.SeqAlunoHistorico,
                SeqCicloLetivo = p.SeqCicloLetivo,
            }).ToList();

            foreach (var item in historicoEscolar)
            {
                var notaArredondada = ArredondarNota(item.Nota);
                item.Nota = notaArredondada;
            }


            return historicoEscolar;
        }

        /// <summary>
        /// Agrupa os itens por componente concatenando os seqs de históricos escolares para pode utilizar nos dados da modal
        /// </summary>
        /// <param name="componentesHistoricos">Componentes do histórico escolar</param>
        /// <param name="componentesPlanos">Componentes do plano de estudo atual</param>
        /// <returns>Lista de componentes sem matriz curricular com historicos escolares</returns>
        private List<IntegralizacaoHistoricoSemMatrizVO> AgruparHistoricosComponentesSemMatriz(List<IntegralizacaoHistoricoSemMatrizVO> componentesHistoricos, List<IntegralizacaoHistoricoSemMatrizVO> componentesPlanos, List<IntegralizacaoHistoricoSemMatrizVO> componentesPlanosAntigos)
        {
            var retornoHistorico = new List<IntegralizacaoHistoricoSemMatrizVO>();
            var listaCompleta = new List<IntegralizacaoHistoricoSemMatrizVO>();

            listaCompleta.AddRange(componentesHistoricos);
            listaCompleta.AddRange(componentesPlanos);
            listaCompleta.AddRange(componentesPlanosAntigos);

            var agrupamento = listaCompleta.GroupBy(g => new { g.SeqComponente, g.SeqComponenteCurricularAssunto });

            foreach (var item in agrupamento)
            {
                var componente = item.Last().Transform<IntegralizacaoHistoricoSemMatrizVO>();
                componente.ExibirInformacao = item.Any(a => a.SituacaoComponente != SituacaoComponenteIntegralizacao.AConcluir);
                componente.SeqsHistoricosEscolar = item.Where(w => w.SeqHistoricoEscolar != 0).Select(s => s.SeqHistoricoEscolar).ToList();
                componente.SeqHistoricoEscolarUltimo = item.LastOrDefault(w => w.SeqHistoricoEscolar != 0)?.SeqHistoricoEscolar;

                if (componentesPlanos.Any(a => a.SeqComponente == componente.SeqComponente))
                {
                    componente.SituacaoComponente = SituacaoComponenteIntegralizacao.EmCurso;
                    componente.SeqPlanoEstudo = item.Last().SeqPlanoEstudo;
                    componente.SeqPlanoEstudoAntigo = null;
                }
                else if (componente.SituacaoComponente == SituacaoComponenteIntegralizacao.Nenhum && componentesPlanosAntigos.Any(a => a.SeqComponente == componente.SeqComponente))
                {
                    componente.SituacaoComponente = SituacaoComponenteIntegralizacao.AguardandoNota;
                    componente.SeqPlanoEstudo = null;
                    componente.SeqPlanoEstudoAntigo = item.Last().SeqPlanoEstudo;
                }

                if (!string.IsNullOrEmpty(componente.DescricaoComponenteAssunto))
                    componente.DescricaoComponente = $"{componente.DescricaoComponente} : {componente.DescricaoComponenteAssunto}";

                retornoHistorico.Add(componente);
            }

            return retornoHistorico;
        }

        /// <summary>
        /// Flag para visualizar consulta de integralização, sempre para aluno e quando for ingressante somente se conceder formação.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Retorna o valor da flag de controle da visualização</returns>
        public bool VisualizarConsultaConcederFormacaoIntegralizacao(long seqPessoaAtuacao)
        {
            //Recuperar os dados da pessoa atuação identificando aluno e ingressante
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            if (dadosOrigem.TipoAtuacao == TipoAtuacao.Aluno)
                return true;

            if (dadosOrigem.TipoAtuacao == TipoAtuacao.Ingressante)
            {
                //Recuperar os dados da instituição nível tipo vínculo aluno para organizar de acordo com o parâmetro de conceder formação
                var concedeFormacao = ValidarConcederFormacaoIntegralizacao(seqPessoaAtuacao, dadosOrigem.TipoAtuacao, dadosOrigem.SeqNivelEnsino, dadosOrigem.SeqTipoVinculoAluno);

                return concedeFormacao;
            }

            return false;
        }

        /// <summary>
        /// Buscar o ciclo letivo e o curso oferta localidade turno do historico escolar
        /// </summary>
        /// <param name="seq">Sequencial do historico escolar</param>
        /// <returns>Retorno o sequencial do ciclo letivo e do curso oferta localidade turno</returns>
        public (long SeqCicloLetivo, long SeqCursoOfertaLocalidadeTurno) BuscarCicloLetivoLocalidadeTurnoHistoricoEscolar(long seq)
        {
            var registro = this.SearchProjectionByKey(new SMCSeqSpecification<HistoricoEscolar>(seq), p => new
            {
                SeqCicloLetivo = p.SeqCicloLetivo,
                SeqCursoOfertaLocalidadeTurno = p.AlunoHistorico.SeqCursoOfertaLocalidadeTurno
            });

            return (registro.SeqCicloLetivo, registro.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault());
        }

        /// <summary>
        /// Método que verifica se existe componentes ja aprovados ou dispensados na lista
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="componentesComAssunto">Lista de componentes com assunto</param>
        /// <param name="seqCicloLetivoIgnorar">Sequencial do ciclo letivo que deve ser ignorado. Útil quando for validar se já cursou a disciplina em ciclo letivo diferente do atual, por exemplo</param>
        /// <returns>Retorna lista de componentes separado com , para os componentes já aprovados ou dispensados</returns>
        public string VerificarHistoricoComponentesAprovadosDispensados(long seqPessoaAtuacao, List<HistoricoEscolarAprovadoFiltroVO> componentesComAssunto, long? seqCicloLetivoIgnorar)
        {
            // Recupera qual o aluno histórico atual
            var seqAlunoHistoricoAtual = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao), x => x.Historicos.FirstOrDefault(h => h.Atual).Seq);

            // Valida a aprovação dos componentes curriculares
            var componentesAprovados = this.ObterComponentesCurricularesAprovadosDispensados(seqAlunoHistoricoAtual, componentesComAssunto, seqCicloLetivoIgnorar);

            if (componentesAprovados.Any())
                return $"<br />- {string.Join("<br />- ", componentesAprovados.Select(c => c.DescricaoComponenteCurricularAprovado))}";
            else
                return string.Empty;
        }

        /// <summary>
        /// Retornar dados historico escolar baseado na procedure ACADEMICO.APR.st_rel_componentes_historico_escolar
        /// </summary>
        /// <param name="seqAluno">Sequencial Aluno</param>
        /// <param name="exibirReprovados">Indicador para exibir ou não componentes reprovados</param>
        /// <param name="exibeCreditoZero">Indicador para exibir ou não componentes com crédito zero</param>
        /// <param name="exibeComponenteCursadoSemHistorico">Indicador para exibir ou não componentes cursados que ainda não tem lançamento de nota no histórico</param>
        /// <param name="exibeExame">Indicador para exibir ou não componentes de exame cursados</param>
        /// <returns>Lista de componentes cursados</returns>
        public List<ComponentesCreditosVO> ConsultarNotasFrequencias(long seqAluno, bool exibirReprovados, bool exibeCreditoZero, bool exibeComponenteCursadoSemHistorico, bool exibeExame)
        {
            // Recuperar os dados da pessoa atuação identificando aluno e ingressante
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            //RN_CUR_077 - Parametrização crédito: "Exibir o campo "Créditos" somente se o valor do campo "Permite Crédito" for igual a "Sim" em Parâmetros por Instituição e Nível de Ensino."
            var instuicaoNivelPermiteCredito = this.InstituicaoNivelDomainService.BuscarInstituicaoNivelPorNivelEnsino(dadosOrigem.SeqNivelEnsino).PermiteCreditoComponenteCurricular;

            // Recupera os dados do histórico escolar de acordo com a procedure ACADEMICO.APR.st_rel_componentes_historico_escolar
            var dadosHistoricoEscolar = this.ComponentesCriteriosHistoricoEscolar(dadosOrigem.SeqAlunoHistoricoAtual, exibirReprovados, exibeCreditoZero, exibeComponenteCursadoSemHistorico, exibeExame);

            foreach (var item in dadosHistoricoEscolar)
            {
                item.PermitirCredito = instuicaoNivelPermiteCredito;

                if ((item.TipoConclusao == TipoConclusao.Exame ||
                    item.TipoConclusao == TipoConclusao.AproveitamentoCredito ||
                    item.TipoConclusao == TipoConclusao.ComponenteConcluido) &&
                   (!string.IsNullOrEmpty(item.Nota) && item.Nota != "Dispensado"))
                {
                    var nota = ArredondarNota(Convert.ToDecimal(item.Nota));
                    item.Nota = $"{nota}";
                }

                if (item.TipoConclusao == TipoConclusao.ComponenteConcluido &&
                    !string.IsNullOrEmpty(item.PercentualFrequencia))
                {
                    if (item.TipoArredondamento.HasValue)
                    {
                        var percentualFrequencia = ArredondarPercentualFrequencia(Convert.ToDecimal(item.PercentualFrequencia), item.TipoArredondamento.Value);
                        item.PercentualFrequencia = $"{percentualFrequencia}%";
                    }
                    else
                    {
                        item.PercentualFrequencia = $"{item.PercentualFrequencia}%";
                    }
                }

                if (item.TipoConclusao == TipoConclusao.CursadoSemHistoricoEscolar && item.Faltas.HasValue)
                {
                    var cargaHoraria = RecuperarCargaHoraria(item.CargaHorariaGrade, item.CargaHoraria.Value, item.CargaHorariaExecutada.Value);

                    var percentualFrequencia = CalcularPercentualFrequencia(cargaHoraria, item.Faltas.Value);
                    item.PercentualFrequencia = item.PercentualFrequencia = $"{Math.Round((decimal)percentualFrequencia, 2)}%";
                }
            }

            return dadosHistoricoEscolar;
        }

        /// <summary>
        /// Obtem e vincula os tipos de componentes para todos os componentes da integralização
        /// </summary>
        /// <param name="historicoEscolarSemMatriz">Lista componentes sem matriz</param>
        public void ObterTipoComponenteCurricularIntegralizacao(List<IntegralizacaoHistoricoSemMatrizVO> historicoEscolarSemMatriz)
        {
            List<long> seqsComponentes = new List<long>();

            if (historicoEscolarSemMatriz != null && historicoEscolarSemMatriz.Count > 0)
                seqsComponentes.AddRange(historicoEscolarSemMatriz.Select(t => t.SeqComponente).ToList());

            var listaComponenteTipo = ComponenteCurricularDomainService.BuscarComponentesCurricularesComTipoComponenteIntegralizacao(seqsComponentes.ToArray());

            historicoEscolarSemMatriz.ForEach(f => f.SiglaComponente = listaComponenteTipo.FirstOrDefault(d => d.SeqComponenteCurricular == f.SeqComponente)?.Sigla);
        }

        public bool TrabalhoAcademicoDoAlunoAprovado(long seqOrigemAvaliacao)
        {
            var spec = new HistoricoEscolarFilterSpecification { SeqOrigemAvaliacao = seqOrigemAvaliacao };
            var retorno = this.SearchByKey(spec);

            if (retorno == null)
                return false;

            return retorno.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado;
        }

        /// <summary>
        /// Regra de arredondamento de percentual de frequencia.
        /// - Se tem percentual:
        ///     - Se o tipo de arredondamento for "Arredondar para o teto":
        ///         - Se a casa decimal for igual a 00, exibir o valor da frequência que está salvo no histórico escolar, sem casas decimais.
        ///         - Se existir casas decimais de 10 a 99 (inclusive), exibir o número inteiro imediatamente maior, sem casas decimais.
        ///     - Se o tipo de arredondamento for "Não arredondar" ou não tiver informado
        ///         - Exibir o valor da frequência que está salvo no histórico escolar, sem casas decimais
        /// - Se não tem percentual, retorna NULL       
        /// </summary>
        /// <param name="percentual">Percentual a ser arredondado</param>
        /// <param name="tipoArredondamento">Tipo de arredondamento</param>
        /// <returns>Percentual arredondado conforme regra</returns>
        public static decimal? ArredondarPercentualFrequencia(decimal? percentual, TipoArredondamento tipoArredondamento)
        {
            // Verifica o arredondamento do % de frequencia
            if (percentual.HasValue)
            {
                if (tipoArredondamento == TipoArredondamento.ArredondarParaTeto)
                    return Decimal.Ceiling(percentual.Value);
                else
                    return Decimal.Floor(percentual.Value);
            }
            else
                return percentual;
        }

        public static decimal? ArredondarNota(decimal? nota)
        {
            if (nota.HasValue)
                return Math.Round(nota.Value, 0, MidpointRounding.AwayFromZero);
            else
                return nota;
        }

        /// <summary>
        /// Calcula o percentual de frequencia. 
        /// Regra:
        /// Caso não tenha sido definido carga horária
        ///     Se tem falta = 0, o percentual de freq é 100 %
        ///     Se tem falta > 0, o percentual de freq é 0 %
        ///     Se falta é null , o percentual de de freq fica null
        /// Caso contrário faz a conta:
        ///     100 - ((faltas / ch) * 100)
        /// </summary>
        /// <param name="cargaHoraria">CH a ser considerada no calculo</param>
        /// <param name="faltas">Faltas a ser considerada no calculo</param>
        /// <returns>Calcula o percentual de frequencia para histórico escolar</returns>
        public static decimal? CalcularPercentualFrequencia(decimal? cargaHoraria, short? faltas)
        {
            decimal? percentual;

            if (!cargaHoraria.HasValue || (cargaHoraria.HasValue && cargaHoraria.Value <= 0))
            {
                if (faltas.HasValue)
                {
                    if (faltas.Value > 0)
                        percentual = 0;
                    else
                        percentual = 100;
                }
                else
                    percentual = null;
            }
            else
                percentual = 100 - (((decimal)faltas.GetValueOrDefault() / cargaHoraria) * 100);

            return percentual;
        }

        public short RecuperarCargaHoraria(short? cargaHorariaGrade, short CargaHoraria, short cargaHorariaExecutada)
        {
            var cargaHorariaBase = (cargaHorariaGrade.HasValue && cargaHorariaGrade.Value > 0) ? cargaHorariaGrade.Value : CargaHoraria;
            var cargaHoraria = Math.Max(cargaHorariaExecutada, cargaHorariaBase);

            return cargaHoraria;
        }

      
    }
}