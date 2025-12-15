using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.GRD.Constants;
using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Academico.Domain.Areas.GRD.Resources;
using SMC.Academico.Domain.Areas.GRD.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.Calendarios.Common.Areas.CLD.Enums;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Security;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.IntegracaoAcademico.ServiceContract.Areas.ACA.Data;
using SMC.IntegracaoAcademico.ServiceContract.Areas.ACA.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SMC.Academico.Domain.Areas.GRD.DomainServices
{
    public class EventoAulaDomainService : AcademicoContextDomain<EventoAula>
    {
        #region [ Services ]

        public IEventoService EventoService => Create<IEventoService>();
        public ITipoEventoService TipoEventoService => Create<ITipoEventoService>();
        public ITabelaHorarioService TabelaHorarioService => Create<ITabelaHorarioService>();

        public IAcademicoService AcademicoService => Create<IAcademicoService>();

        #endregion [ Services ]

        #region [ DomainServices ]

        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();
        private ColaboradorVinculoDomainService ColaboradorVinculoDomainService => Create<ColaboradorVinculoDomainService>();
        private DivisaoTurmaColaboradorDomainService DivisaoTurmaColaboradorDomainService => Create<DivisaoTurmaColaboradorDomainService>();
        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();
        private EventoAulaColaboradorDomainService EventoAulaColaboradorDomainService => Create<EventoAulaColaboradorDomainService>();
        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();
        private InstituicaoNivelTipoEventoDomainService InstituicaoNivelTipoEventoDomainService => Create<InstituicaoNivelTipoEventoDomainService>();
        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();
        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();
        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();
        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();
        private ApuracaoFrequenciaGradeDomainService ApuracaoFrequenciaGradeDomainService => Create<ApuracaoFrequenciaGradeDomainService>();
        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();
        private GradeHorariaCompartilhadaDomainService GradeHorariaCompartilhadaDomainService => Create<GradeHorariaCompartilhadaDomainService>();

        #endregion [ DomainServices ]

        public EventoAulaTurmaVO BuscarEventosTurma(long seqTurma)
        {
            EventoAulaTurmaVO retorno = new EventoAulaTurmaVO();
            TabelaHorarioData tabelaHorario = null;

            retorno = TurmaDomainService.SearchProjectionByKey(seqTurma, p => new EventoAulaTurmaVO
            {
                EventoAulaTurmaCabecalho = new EventoAulaTurmaCabecalhoVO
                {
                    Numero = p.Numero,
                    Codigo = p.Codigo,
                    SeqCicloLetivoInicio = p.SeqCicloLetivoInicio,
                    CicloLetivoInicio = p.CicloLetivoInicio.Descricao,
                    DescricaoConfiguracaoComponente = p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).Descricao,
                    InicioPeriodoLetivo = p.DataInicioPeriodoLetivo,
                    FimPeriodoLetivo = p.DataFimPeriodoLetivo,
                    TurmaCancelada = p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma == SituacaoTurma.Cancelada,
                    SeqCursoOfertaLocalidade = p.ConfiguracoesComponente.SelectMany(s => s.RestricoesTurmaMatriz)
                                                .FirstOrDefault(f => f.OfertaMatrizPrincipal)
                                                .CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                    SeqAgendaTurma = p.SeqAgendaTurma,
                    DiarioFechado = p.HistoricosFechamentoDiario.Count > 0 ? p.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false
                },
                /* Serão listadas todas as divisões cadastradas para a turma cujo componente associado possui carga horária de grade
                maior que zero.Colocar esta regra como informativo para o usuário.
                Apresentar as divisões ordenadas pela sua codificação. */
                EventoAulaDivisoesTurma = p.DivisoesTurma.Select(s => new EventoAulaDivisaoTurmaVO
                {
                    Seq = s.Seq,
                    TurmaCodigo = s.Turma.Codigo,
                    TurmaNumero = s.Turma.Numero,
                    Numero = s.DivisaoComponente.Numero,
                    NumeroGrupo = s.NumeroGrupo,
                    CargaHorariaGrade = s.DivisaoComponente.CargaHorariaGrade,
                    DescricaoDivisaoTurma = s.DivisaoComponente.ConfiguracaoComponente.Descricao,
                    DescricaoLocalidade = s.Localidade.Nome,
                    TipoDivisaoDescricao = s.DivisaoComponente.TipoDivisaoComponente.Descricao,
                    TipoDistribuicaoAula = s.HistoricoConfiguracaoGradeAtual.TipoDistribuicaoAula,
                    TipoPulaFeriado = s.HistoricoConfiguracaoGradeAtual.TipoPulaFeriado,
                    AulaSabado = s.HistoricoConfiguracaoGradeAtual.AulaSabado,
                    SeqCursoOfertaLocalidadeTurno = s.OrigemAvaliacao.MatrizCurricularOferta.SeqCursoOfertaLocalidadeTurno,
                    InicioPeriodoLetivo = s.Turma.DataInicioPeriodoLetivo,
                    FimPeriodoLetivo = s.Turma.DataFimPeriodoLetivo,
                    QuantidadeSemanas = s.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.QuantidadeSemanas,
                    TemHistoricoEscolar = s.OrigemAvaliacao.HistoricosEscolares.Count() > 0,
                    TemConfiguracaoGrade = s.HistoricosConfiguracaoGrade.Count() > 0,
                    Compartilhamentos = s.GradesHorariaCompartilhadas.Select(sc => new GradeHorariaCompartilhadaVO()
                    {
                        Seq = sc.SeqGradeHorariaCompartilhada.Value,
                        Descricao = sc.GradeHorariaCompartilhada.Descricao
                    }).ToList(),
                    EventoAulas = s.EventosAula.Select(se => new EventoAulaVO
                    {
                        CodigoRecorrencia = se.CodigoRecorrencia,
                        Data = se.Data,
                        DiaSemana = (int)se.DiaSemana,
                        //TODO: Verificar binder e transform
                        HoraFimSpan = se.HoraFim,
                        HoraInicioSpan = se.HoraInicio,
                        Local = se.Local,
                        CodigoLocalSEF = se.CodigoLocalSEF,
                        Seq = se.Seq,
                        SeqDivisaoTurma = se.SeqDivisaoTurma,
                        SeqEventoAgd = se.SeqEventoAgd,
                        SeqHorarioAgd = se.SeqHorarioAgd,
                        SituacaoApuracaoFrequencia = se.SituacaoApuracaoFrequencia,
                        Feriado = se.Feriado,
                        Colaboradores = se.Colaboradores.Select(sc => new EventoAulaColaboradorVO()
                        {
                            Seq = sc.Seq,
                            SeqColaborador = sc.SeqColaborador,
                            SeqColaboradorSubstituto = sc.SeqColaboradorSubstituto,
                            NomeColaborador = sc.Colaborador.DadosPessoais.Nome,
                            NomeSocialColaborador = sc.Colaborador.DadosPessoais.NomeSocial,
                            NomeColaboradorSubstituto = sc.ColaboradorSubstituto.DadosPessoais.Nome,
                            NomeSocialColaboradorSubstituto = sc.ColaboradorSubstituto.DadosPessoais.NomeSocial,

                        }).ToList()
                    }).OrderBy(o => o.Data).ThenBy(o => o.HoraInicioSpan).ToList()
                }).Where(w => w.CargaHorariaGrade > 0).OrderBy(o => o.TurmaNumero).ThenBy(o => o.Numero).ThenBy(o => o.NumeroGrupo).ToList()
            });

            if (retorno.EventoAulaTurmaCabecalho.SeqAgendaTurma.HasValue && retorno.EventoAulaDivisoesTurma.SMCAny(a => a.EventoAulas.SMCAny()))
            {
                tabelaHorario = TabelaHorarioService.BuscarTabelaHorarioAgendaTurma(new TabelaHorarioAgendaTurmaFiltroData()
                {
                    SeqAgenda = retorno.EventoAulaTurmaCabecalho.SeqAgendaTurma.Value,
                    DataInicioPeriodoLetivo = retorno.EventoAulaTurmaCabecalho.InicioPeriodoLetivo,
                    DataFimPeriodoLetivo = retorno.EventoAulaTurmaCabecalho.FimPeriodoLetivo
                });
            }

            foreach (var item in retorno.EventoAulaDivisoesTurma)
            {
                /*RN_TUR_023 - Exibição Codificação de turma
                A codificação de uma turma deverá ser exibida sempre com a seguinte concatenação de dados:
                [Código da Turma] + "." + [Número da Turma] + "." + [Número da Divisão da Configuração do
                Componente] + "." + [Número do Grupo].*/
                item.GrupoFormatado = $"{item.TurmaCodigo}.{item.TurmaNumero}.{item.Numero}.{item.NumeroGrupo:d3}";

                /*RN_GRD_020 - Exibe descrição divisão da turma A descrição de uma divisão de turma deverá ser a seguinte concatenação de dados:
                [Codificação(RN_TUR_023 - Exibição Codificação de turma)] +"-" +
                [Descrição da configuração do componente] +":" + [Descrição da configuração do componente substituto] +"-" + [Carga horária e Crédito do componente] + [label parametrizado]
                O ": " ,após a descrição da configuração do componente, só deve ser exibido se houver assunto(componente substituto)
                O label parametrizado é o conteúdo do campo Formato em Parâmetros por Instituição e Nível de Ensino, para o tipo do componente em questão.
                Na ausência da carga horária, retirar o respectivo label. */
                item.DescricaoDivisaoFormatada = $"{item.GrupoFormatado} - {DivisaoTurmaDomainService.BuscarDivisaoTurmaCabecalho(item.Seq, quebraLinha: false).TurmaDescricaoFormatado}";

                // UC_GRD_001_01_01.NV04
                item.DescricaoLocalidade = string.IsNullOrEmpty(item.DescricaoLocalidade) ? "Localidade não informada" : item.DescricaoLocalidade;

                if (item.EventoAulas.SMCAny())
                {
                    foreach (var eventoAula in item.EventoAulas)
                    {
                        var colaboradores = new HashSet<string>();
                        eventoAula.Turno = (short)(tabelaHorario.Horarios.FirstOrDefault(f => f.Seq == eventoAula.SeqHorarioAgd)?.Turno ?? Turno.Nenhum);
                        eventoAula.HoraInicio = eventoAula.HoraInicioSpan.ToString(@"hh\:mm");
                        eventoAula.HoraFim = eventoAula.HoraFimSpan.ToString(@"hh\:mm");
                        eventoAula.DiaSemanaDescricao = ((TipoDiaSemana)eventoAula.DiaSemana).SMCGetDescription();
                        eventoAula.DiaSemanaFormatada = $"{eventoAula.Data.ToShortDateString()} - {eventoAula.DiaSemanaDescricao}";
                        if (eventoAula.Colaboradores.SMCAny())
                        {
                            foreach (var eventoAulaColaborador in eventoAula.Colaboradores)
                            {
                                // UC_GRD_001_01_01.NV12
                                // Formata os colaboradores como:
                                // "[CodigoPessoaAtuacao] - [NomeFormatado]"
                                var descricaoColaborador = PessoaDadosPessoaisDomainService
                                    .FormatarNomeSocial(eventoAulaColaborador.NomeColaborador, eventoAulaColaborador.NomeSocialColaborador);
                                descricaoColaborador = $"{eventoAulaColaborador.SeqColaborador} - {descricaoColaborador}";
                                // Caso tenha um substituto:
                                // "[CodigoPessoaAtuacao] - [NomeFormatado] (substituto(a) de [CodigoPessoaAtuacaoSubstituto] - [NomeFormatadoSubstituto])"
                                if (eventoAulaColaborador.SeqColaboradorSubstituto.HasValue)
                                {
                                    var descricaoColaboradorSubstituto = PessoaDadosPessoaisDomainService
                                        .FormatarNomeSocial(eventoAulaColaborador.NomeColaboradorSubstituto, eventoAulaColaborador.NomeSocialColaboradorSubstituto);
                                    descricaoColaborador = $"{eventoAulaColaborador.SeqColaboradorSubstituto} - {descricaoColaboradorSubstituto} (substituto(a) de {descricaoColaborador})";
                                }
                                eventoAulaColaborador.DescricaoFormatada = descricaoColaborador;
                                colaboradores.Add(descricaoColaborador);
                            }
                        }
                        else
                        {
                            // UC_GRD_001_01_01.NV04
                            eventoAula.Colaboradores = new List<EventoAulaColaboradorVO>()
                            {
                                new EventoAulaColaboradorVO()
                                {
                                    DescricaoFormatada = MessagesResource.EventoAulaSemColaborador
                                }
                            };
                            colaboradores.Add(MessagesResource.EventoAulaSemColaborador);
                        }
                        eventoAula.DescricaoColaboradores = string.Join(", ", colaboradores.OrderBy(o => o));
                    }
                }
            }

            /*RN_GRD_001 - Consultar grade
            Se a turma selecionada estiver cancelada, enviar mensagem de erro:
            A turma selecionada está cancelada não sendo permitida manutenções na grade horária. */
            if (retorno.EventoAulaTurmaCabecalho.TurmaCancelada)
            {
                // UC_GRD_001_01_01.NV15.1
                retorno.EventoAulaTurmaCabecalho.MensagemFalha = MessagesResource.EventoAulaTurmaCancelada;
                retorno.EventoAulaTurmaCabecalho.SomenteLeitura = true;
            }

            retorno.PermiteAlterarDataAgendamentoAula = SMCSecurityHelper.Authorize(UC_GRD_001_01_02.PERMITE_ALTERAR_DATA_AGENDAMENTO_AULA);
            if (!SMCSecurityHelper.Authorize(UC_GRD_001_01_06.PERMITE_ALTERAR_GRADE_FORA_VIGENCIA_TURMA) &&
                DateTime.Today > retorno.EventoAulaTurmaCabecalho.FimPeriodoLetivo)
            {
                // UC_GRD_001_01_01.NV15.2
                retorno.EventoAulaTurmaCabecalho.MensagemFalha = MessagesResource.EventoAulaTurmaExpirada;
                retorno.EventoAulaTurmaCabecalho.SomenteLeitura = true;
            }

            //Turma com diario fechado
            if (retorno.EventoAulaTurmaCabecalho.DiarioFechado)
            {
                retorno.EventoAulaTurmaCabecalho.MensagemFalha = MessagesResource.EventoAulaTurmaDiarioFechado;
                retorno.EventoAulaTurmaCabecalho.SomenteLeitura = true;
            }

            /* UC_GRD_001_01_01.NV15.5 Verificar se existe algum registro de configuração de grade
             criado para alguma divisão de turma.Caso não exista, desabilitar essa opção exibindo a mensagem em um icone informativo:
            "Operação indisponível! Nenhuma divisão desta turma possui configuração de grade de aula" */
            if (retorno.EventoAulaDivisoesTurma.All(a => !a.TemConfiguracaoGrade))
            {
                retorno.EventoAulaTurmaCabecalho.MensagemFalha = MessagesResource.EventosAulaTodosNaoPossuemConfiguracaoGrade;
                retorno.EventoAulaTurmaCabecalho.SomenteLeitura = true;
            }

            /*UC_GRD_001_01_01.NV15.6 Verificar se existe alguma divisão de turma cuja a divisão de componente possui carga hoária de grade informada e
              que seja maior que zero. Caso não tenha desabilitar essa opção exibindo a mensagem em um icone informativo:
            "Operação indisponível! Nenhuma divisão desta turma possui carga horária para lançamento de grade de aula."*/
            if (retorno.EventoAulaDivisoesTurma.All(a => !a.CargaHorariaGrade.HasValue || a.CargaHorariaGrade == 0))
            {
                retorno.EventoAulaTurmaCabecalho.MensagemFalha = MessagesResource.EventosAulaTodosNaoPossuemCargaHoraria;
                retorno.EventoAulaTurmaCabecalho.SomenteLeitura = true;
            }

            /*UC_GRD_001_01_01.NV15.7 Verificar se existe agenda configurada para a turma. Caso não exista, desabilitar essa opção exibindo a mensagem em um icone informativo:
              "Operação indisponível. Não existe agenda configurada para esta turma."*/
            if (!retorno.EventoAulaTurmaCabecalho.SeqAgendaTurma.HasValue)
            {
                retorno.EventoAulaTurmaCabecalho.MensagemFalha = MessagesResource.EventoAulaTurmaSemAgenda;
                retorno.EventoAulaTurmaCabecalho.SomenteLeitura = true;
            }

            if (!retorno.EventoAulaTurmaCabecalho.SomenteLeitura && retorno.EventoAulaDivisoesTurma.Any(a => !ValidarDivisao(a)))
            {
                retorno.EventoAulaTurmaCabecalho.MensagemFalha = MessagesResource.EventoAulaTurmaDivisaoNaoConfigurada;
                retorno.EventoAulaTurmaCabecalho.SomenteLeitura = true;
            }

            //RN_GRD_007  - Buscar Unidade SEO da Localidade da Turma para fins de Grade
            retorno.EventoAulaTurmaCabecalho.CodigoUnidadeSeo = EntidadeDomainService.RecuperarCodigoUnidadeSeo(retorno.EventoAulaTurmaCabecalho.SeqCursoOfertaLocalidade);

            return retorno;
        }

        /// <summary>
        /// Buscar os eventos pelo sequencial origem avaliação
        /// Considerando que relação divisão turma e origem avaliação seja 1 x 1
        /// </summary>
        /// <param name="seqDivisaoTurma"></param>
        /// <param name="seqOrigemAvaliacao"></param>
        /// <returns></returns>
        public EventoAulaDivisaoTurmaVO BuscarEventosOrigemAvaliacao(long? seqDivisaoTurma, long? seqOrigemAvaliacao)
        {
            EventoAulaDivisaoTurmaVO retorno = new EventoAulaDivisaoTurmaVO();
            TabelaHorarioData tabelaHorario = null;

            // Verifica se deve buscar como turma ou como divisão de turma
            bool verificarNaTurma = false;
            if (!seqOrigemAvaliacao.HasValue)
                return null;

            var tipoOrigemAvaliacao = OrigemAvaliacaoDomainService.SearchProjectionByKey(seqOrigemAvaliacao.Value, x => x.TipoOrigemAvaliacao);
            if (tipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
                verificarNaTurma = true;

            if (verificarNaTurma && seqOrigemAvaliacao.HasValue)
            {
                TurmaFilterSpecification specTurma = new TurmaFilterSpecification { SeqOrigemAvaliacao = seqOrigemAvaliacao };

                retorno = TurmaDomainService.SearchProjectionByKey(specTurma, s => new EventoAulaDivisaoTurmaVO
                {
                    Seq = s.Seq,
                    TurmaSeq = s.Seq,
                    TurmaCodigo = s.Codigo,
                    TurmaNumero = s.Numero,
                    SeqAgendaTurma = s.SeqAgendaTurma,
                    Numero = s.Numero,
                    TipoOrigemAvaliacao = s.OrigemAvaliacao.TipoOrigemAvaliacao,
                    SeqCursoOfertaLocalidadeTurno = s.OrigemAvaliacao.MatrizCurricularOferta.SeqCursoOfertaLocalidadeTurno,
                    InicioPeriodoLetivo = s.DataInicioPeriodoLetivo,
                    FimPeriodoLetivo = s.DataFimPeriodoLetivo,
                    TemHistoricoEscolar = s.OrigemAvaliacao.HistoricosEscolares.Count() > 0,
                });
            }
            else
            {
                DivisaoTurmaFilterSpecification specDivisao = new DivisaoTurmaFilterSpecification();

                if (seqDivisaoTurma.HasValue)
                    specDivisao.Seq = seqDivisaoTurma.Value;

                if (seqOrigemAvaliacao.HasValue)
                    specDivisao.SeqOrigemAvaliacao = seqOrigemAvaliacao.Value;

                retorno = DivisaoTurmaDomainService.SearchProjectionByKey(specDivisao, s => new EventoAulaDivisaoTurmaVO
                {
                    Seq = s.Seq,
                    TurmaSeq = s.SeqTurma,
                    TurmaCodigo = s.Turma.Codigo,
                    TurmaNumero = s.Turma.Numero,
                    SeqAgendaTurma = s.Turma.SeqAgendaTurma,
                    Numero = s.DivisaoComponente.Numero,
                    NumeroGrupo = s.NumeroGrupo,
                    TipoOrigemAvaliacao = s.OrigemAvaliacao.TipoOrigemAvaliacao,
                    CargaHorariaGrade = s.DivisaoComponente.CargaHorariaGrade,
                    DescricaoDivisaoTurma = s.DivisaoComponente.ConfiguracaoComponente.Descricao,
                    DescricaoLocalidade = s.Localidade.Nome,
                    TipoDivisaoDescricao = s.DivisaoComponente.TipoDivisaoComponente.Descricao,
                    TipoDistribuicaoAula = s.HistoricoConfiguracaoGradeAtual.TipoDistribuicaoAula,
                    TipoPulaFeriado = s.HistoricoConfiguracaoGradeAtual.TipoPulaFeriado,
                    AulaSabado = s.HistoricoConfiguracaoGradeAtual.AulaSabado,
                    SeqCursoOfertaLocalidadeTurno = s.OrigemAvaliacao.MatrizCurricularOferta.SeqCursoOfertaLocalidadeTurno,
                    InicioPeriodoLetivo = s.Turma.DataInicioPeriodoLetivo,
                    FimPeriodoLetivo = s.Turma.DataFimPeriodoLetivo,
                    QuantidadeSemanas = s.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.QuantidadeSemanas,
                    TemHistoricoEscolar = s.OrigemAvaliacao.HistoricosEscolares.Count() > 0,
                    EventoAulas = s.EventosAula.Select(se => new EventoAulaVO
                    {
                        CodigoRecorrencia = se.CodigoRecorrencia,
                        Data = se.Data,
                        DiaSemana = (int)se.DiaSemana,
                        HoraFimSpan = se.HoraFim,
                        HoraInicioSpan = se.HoraInicio,
                        Local = se.Local,
                        CodigoLocalSEF = se.CodigoLocalSEF,
                        Seq = se.Seq,
                        SeqDivisaoTurma = se.SeqDivisaoTurma,
                        SeqEventoAgd = se.SeqEventoAgd,
                        SeqHorarioAgd = se.SeqHorarioAgd,
                        Colaboradores = se.Colaboradores.Select(sc => new EventoAulaColaboradorVO()
                        {
                            Seq = sc.Seq,
                            SeqColaborador = sc.SeqColaborador,
                            SeqColaboradorSubstituto = sc.SeqColaboradorSubstituto,
                            NomeColaborador = sc.Colaborador.DadosPessoais.Nome,
                            NomeSocialColaborador = sc.Colaborador.DadosPessoais.NomeSocial,
                            NomeColaboradorSubstituto = sc.ColaboradorSubstituto.DadosPessoais.Nome,
                            NomeSocialColaboradorSubstituto = sc.ColaboradorSubstituto.DadosPessoais.NomeSocial
                        }).ToList()
                    }).OrderBy(o => o.Data).ThenBy(o => o.HoraInicioSpan).ToList(),
                });
            }

            if (retorno == null)
                return null;

            if (tipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
                return retorno;
            else if (tipoOrigemAvaliacao != TipoOrigemAvaliacao.DivisaoTurma)
                return null;

            EventoAulaTurmaCabecalhoVO agendaPeriodo = TurmaDomainService.SearchProjectionByKey(retorno.TurmaSeq, p => new EventoAulaTurmaCabecalhoVO
            {
                InicioPeriodoLetivo = p.DataInicioPeriodoLetivo,
                FimPeriodoLetivo = p.DataFimPeriodoLetivo,
                SeqAgendaTurma = p.SeqAgendaTurma,
                SeqCursoOfertaLocalidade = p.OrigemAvaliacao.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade
            });

            /*RN_TUR_023 - Exibição Codificação de turma
            A codificação de uma turma deverá ser exibida sempre com a seguinte concatenação de dados:
            [Código da Turma] + "." + [Número da Turma] + "." + [Número da Divisão da Configuração do
            Componente] + "." + [Número do Grupo].*/
            retorno.GrupoFormatado = $"{retorno.TurmaCodigo}.{retorno.TurmaNumero}.{retorno.Numero}.{retorno.NumeroGrupo:d3}";

            /*RN_GRD_020 - Exibe descrição divisão da turma A descrição de uma divisão de turma deverá ser a seguinte concatenação de dados:
            [Codificação(RN_TUR_023 - Exibição Codificação de turma)] +"-" +
            [Descrição da configuração do componente] +":" + [Descrição da configuração do componente substituto] +"-" + [Carga horária e Crédito do componente] + [label parametrizado]
            O ": " ,após a descrição da configuração do componente, só deve ser exibido se houver assunto(componente substituto)
            O label parametrizado é o conteúdo do campo Formato em Parâmetros por Instituição e Nível de Ensino, para o tipo do componente em questão.
            Na ausência da carga horária, retirar o respectivo label. */
            retorno.DescricaoDivisaoFormatada = $"{retorno.GrupoFormatado} - {DivisaoTurmaDomainService.BuscarDivisaoTurmaCabecalho(retorno.Seq, quebraLinha: false).TurmaDescricaoFormatado}";

            // UC_GRD_001_01_01.NV04
            retorno.DescricaoLocalidade = string.IsNullOrEmpty(retorno.DescricaoLocalidade) ? "Sem local" : retorno.DescricaoLocalidade;

            if (retorno.EventoAulas.SMCAny())
            {
                tabelaHorario = TabelaHorarioService.BuscarTabelaHorarioAgendaTurma(new TabelaHorarioAgendaTurmaFiltroData()
                {
                    SeqAgenda = agendaPeriodo.SeqAgendaTurma.Value,
                    DataInicioPeriodoLetivo = agendaPeriodo.InicioPeriodoLetivo,
                    DataFimPeriodoLetivo = agendaPeriodo.FimPeriodoLetivo
                });
                //short turno = 0;
                //int diaAnterior = retorno.EventoAulas.First().Data.Day;
                foreach (var eventoAula in retorno.EventoAulas)
                {
                    var colaboradores = new HashSet<string>();
                    eventoAula.Turno = (short)(tabelaHorario.Horarios.FirstOrDefault(f => f.Seq == eventoAula.SeqHorarioAgd)?.Turno ?? Turno.Nenhum);
                    eventoAula.HoraInicio = eventoAula.HoraInicioSpan.ToString(@"hh\:mm");
                    eventoAula.HoraFim = eventoAula.HoraFimSpan.ToString(@"hh\:mm");
                    eventoAula.DiaSemanaDescricao = ((TipoDiaSemana)eventoAula.DiaSemana).SMCGetDescription();
                    eventoAula.DiaSemanaFormatada = $"{eventoAula.Data.ToShortDateString()} - {eventoAula.DiaSemanaDescricao}";
                    //if (eventoAula.Data.Day != diaAnterior)
                    //{
                    //    turno++;
                    //    diaAnterior = eventoAula.Data.Day;
                    //}
                    //eventoAula.Turno = turno;
                }
            }

            //RN_GRD_007  - Buscar Unidade SEO da Localidade da Turma para fins de Grade
            retorno.CodigoUnidadeSEO = EntidadeDomainService.RecuperarCodigoUnidadeSeo(agendaPeriodo.SeqCursoOfertaLocalidade);

            return retorno;
        }

        public void SalvarEventos(List<EventoAulaVO> eventos)
        {
            long seqAgenda = 0;
            long seqTipoEventoAgd = 0;
            int diasUteis = 0;
            bool permiteAulaSabado = false;
            List<FeriadoData> feriados = new List<FeriadoData>();
            List<HorarioCompletoData> tabelaHorarios = new List<HorarioCompletoData>();
            List<long> listaDivisoesCompartilhamento = new List<long>();
            List<EventoAulaVO> eventosCompartilhados = new List<EventoAulaVO>();

            if (eventos.SMCAny())
            {
                var dadosTurma = DivisaoTurmaDomainService.SearchProjectionByKey(eventos.First().SeqDivisaoTurma, p => new
                {
                    SeqAgenda = p.Turma.SeqAgendaTurma ?? 0,
                    p.SeqTurma,
                    p.HistoricoConfiguracaoGradeAtual.AulaSabado,
                    p.Turma.DataInicioPeriodoLetivo,
                    p.Turma.DataFimPeriodoLetivo,
                    SeqCursoOfertaLocalidade = p.Turma.ConfiguracoesComponente.SelectMany(s => s.RestricoesTurmaMatriz)
                                                .FirstOrDefault(f => f.OfertaMatrizPrincipal)
                                                .CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                });
                permiteAulaSabado = dadosTurma.AulaSabado;
                seqAgenda = dadosTurma.SeqAgenda;
                seqTipoEventoAgd = InstituicaoNivelTipoEventoDomainService.BuscarSeqTipoEventoAgdPorTokenDivisao(TOKEN_TIPO_EVENTO_SEF.AULA, eventos[0].SeqDivisaoTurma);
                var codUnidadeSeo = EntidadeDomainService.RecuperarCodigoUnidadeSeo(dadosTurma.SeqCursoOfertaLocalidade);
                diasUteis = InstituicaoNivelDomainService.BuscarInstituicaoNivelPorTurma(dadosTurma.SeqTurma).QuantidadeDiasPrazoApuracaoFrequencia;
                feriados = AcademicoService.BuscarFeriados((long)codUnidadeSeo.GetValueOrDefault(), dadosTurma.DataInicioPeriodoLetivo, dadosTurma.DataFimPeriodoLetivo);
                tabelaHorarios = TabelaHorarioService.BuscarTabelaHorarioAgendaTurma(new TabelaHorarioAgendaTurmaFiltroData()
                {
                    SeqAgenda = seqAgenda,
                    DataInicioPeriodoLetivo = dadosTurma.DataInicioPeriodoLetivo,
                    DataFimPeriodoLetivo = dadosTurma.DataFimPeriodoLetivo
                }).Horarios.Where(w => w.Turno == (Turno)eventos.FirstOrDefault().Turno).OrderByDescending(o => o.HoraFim).ToList();
                listaDivisoesCompartilhamento.AddRange(GradeHorariaCompartilhadaDomainService.BuscarDivisoesGradeHorariaCompartilhada(eventos.FirstOrDefault().SeqDivisaoTurma));
                eventosCompartilhados = BuscarEventosDivisoes(listaDivisoesCompartilhamento);
            }

            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                foreach (var eventoVO in eventos)
                {
                    var eventoModel = eventoVO.Transform<EventoAula>();
                    eventoModel.HoraInicio = TimeSpan.ParseExact(eventoVO.HoraInicio, @"hh\:mm", CultureInfo.CurrentCulture);
                    eventoModel.HoraFim = TimeSpan.ParseExact(eventoVO.HoraFim, @"hh\:mm", CultureInfo.CurrentCulture);
                    eventoModel.SituacaoApuracaoFrequencia = SituacaoApuracaoFrequencia.NaoApurada;

                    // Ignora eventos duplicados na inclusão
                    var specEventoDuplicado = new EventoAulaFilterSpecification()
                    {
                        SeqDivisaoTurma = eventoVO.SeqDivisaoTurma,
                        SeqHorarioAgd = eventoVO.SeqHorarioAgd,
                        Data = eventoVO.Data.Date
                    };
                    if (Count(specEventoDuplicado) > 0)
                    {
                        continue;
                    }
                    TimeSpan horarioFimTurno = tabelaHorarios.FirstOrDefault(f => f.DiaSemana == (TipoDiaSemana)eventoVO.DiaSemana).HoraFim;
                    DateTime datahora = eventoModel.Data + horarioFimTurno;
                    eventoModel.DataLimiteApuracaoFrequencia = AcademicoService.CalcularDataPorDiaUtil(datahora, diasUteis, feriados, permiteAulaSabado);

                    RetornoOperacaoEventoData eventoAgd = new RetornoOperacaoEventoData();
                    var dataAgd = new EventoData()
                    {
                        CodigoLocalSEF = eventoModel.CodigoLocalSEF,
                        DataInicio = eventoModel.Data.Add(eventoModel.HoraInicio),
                        DataFim = eventoModel.Data.Add(eventoModel.HoraFim),
                        EventoDiaInteiro = false,
                        EventoParticular = false,
                        Local = eventoModel.Local,
                        Nome = eventoVO.Descricao,
                        SeqAgenda = seqAgenda,
                        SeqHorario = eventoModel.SeqHorarioAgd,
                        SeqTipoEvento = seqTipoEventoAgd
                    };
                    var existeEventoRaiz = ValidarExisteEventoRaiz(eventosCompartilhados, eventoVO.SeqHorarioAgd, eventoVO.Data, eventoVO.CodigoLocalSEF);
                    if (listaDivisoesCompartilhamento.Count > 0 && existeEventoRaiz)
                    {
                        dataAgd.CodigoLocalSEF = null;
                    }
                    eventoAgd = EventoService.SalvarEvento(dataAgd);
                    AGDHelper.TratarErroAGD(eventoAgd);

                    eventoModel.SeqEventoAgd = eventoAgd.SeqEvento.ToString();

                    ValidarColaboradorAtivo(eventoModel);
                    SaveEntity(eventoModel);
                }
                AtualizarProfessoresDivisao(eventos);
                transacao.Commit();
            }
        }

        private bool ValidarExisteEventoRaiz(List<EventoAulaVO> eventosCompartilhados, long seqHorarioAgd, DateTime data, int? codigoLocalSEF)
        {
            var eventos = eventosCompartilhados
                .Where(w => w.SeqHorarioAgd == seqHorarioAgd && w.Data == data && w.CodigoLocalSEF == codigoLocalSEF)
                .ToList();

            foreach (var evento in eventos)
            {
                var eventoAgd = EventoService.BuscarEvento(long.Parse(evento.SeqEventoAgd));

                if (eventoAgd.CodigoLocalSEF.HasValue)
                {
                    return true;
                }
            }

            return false;
        }

        public void EditarEventos(List<EventoAulaVO> eventos, List<long> seqsEventosExcluir)
        {
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                ValidarColisaoHorarioAluno(eventos);
                ValidarColisaoHorarioSolicitacaoServico(eventos);
                if (seqsEventosExcluir.SMCAny())
                {
                    ExcluirEventos(seqsEventosExcluir);
                }
                SalvarEventos(eventos);
                transacao.Commit();
            }
        }

        public void EditarLocalEventos(int? codigoLocalSEF, string local, List<long> seqEventos)
        {
            long seqAgenda = 0, seqTipoEventoAgd = 0, seqDivisaoTurma = 0;
            var dadosTurma = this.SearchProjectionByKey(seqEventos[0], p => new
            {
                p.SeqDivisaoTurma,
                p.DivisaoTurma.Turma.SeqAgendaTurma,
                CodigoTurma = p.DivisaoTurma.Turma.Codigo,
                NumeroTurma = p.DivisaoTurma.Turma.Numero,
                NumeroDivisaoComponente = p.DivisaoTurma.DivisaoComponente.Numero,
                p.DivisaoTurma.NumeroGrupo
            });
            seqDivisaoTurma = dadosTurma.SeqDivisaoTurma;
            seqAgenda = dadosTurma.SeqAgendaTurma.Value;
            seqTipoEventoAgd = InstituicaoNivelTipoEventoDomainService.BuscarSeqTipoEventoAgdPorTokenDivisao(TOKEN_TIPO_EVENTO_SEF.AULA, seqDivisaoTurma);
            var descricaoEvento = DivisaoTurmaDomainService.MontarDescricaoDivisaoTurmaRegraGRD020(dadosTurma.SeqDivisaoTurma);

            var spec = new EventoAulaFilterSpecification() { Seqs = seqEventos };
            var eventos = SearchBySpecification(spec, i => i.Colaboradores).ToList();
            var seqsDivisoesCompartilhadas = GradeHorariaCompartilhadaDomainService.BuscarDivisoesGradeHorariaCompartilhada(dadosTurma.SeqDivisaoTurma);
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                foreach (var evento in eventos)
                {
                    if (seqsDivisoesCompartilhadas.SMCAny())
                    {
                        AtualizarLocalEventoCompartilhamento(evento, seqsDivisoesCompartilhadas, codigoLocalSEF, seqAgenda, seqTipoEventoAgd, descricaoEvento, out int? codigoLocalSEFCompartilhado);
                        AtualizarLocalEvento(evento, codigoLocalSEF, local);
                        evento.CodigoLocalSEF = codigoLocalSEFCompartilhado;
                    }
                    else
                    {
                        AtualizarLocalEvento(evento, codigoLocalSEF, local);
                    }
                    SalvarEventoAGD(evento, seqAgenda, seqTipoEventoAgd, descricaoEvento);
                }
                transacao.Commit();
            }
        }

        public void EditarColaboradoresEventos(List<EventoAulaColaborador> colaboradores, List<long> seqEventos, long seqEventoTemplate, bool somenteColaborador)
        {
            if (somenteColaborador)
            {
                EditarColaboradoresPrincipaisEventos(colaboradores, seqEventos, seqEventoTemplate);
            }
            else
            {
                EditarColaboradoresSubstitutosEventos(colaboradores, seqEventos, seqEventoTemplate);
            }
        }

        public void ExcluirEventos(List<long> seqsEventoAula)
        {
            if (!seqsEventoAula.SMCAny())
            {
                return;
            }
            var specEventosValidacao = new EventoAulaFilterSpecification() { Seqs = seqsEventoAula };
            var eventosExclusao = SearchBySpecification(specEventosValidacao).ToList();
            //Todos os eventos são da mesma divisão
            var seqDivisaoTurma = eventosExclusao.First().SeqDivisaoTurma;
            List<long> listaDivisoesCompartilhamento = new List<long>();
            List<EventoAulaVO> eventosCompartilhados = new List<EventoAulaVO>();
            listaDivisoesCompartilhamento.AddRange(GradeHorariaCompartilhadaDomainService.BuscarDivisoesGradeHorariaCompartilhada(seqDivisaoTurma));
            eventosCompartilhados = BuscarEventosDivisoes(listaDivisoesCompartilhamento);
            var seqTipoEventoAgd = InstituicaoNivelTipoEventoDomainService.BuscarSeqTipoEventoAgdPorTokenDivisao(TOKEN_TIPO_EVENTO_SEF.AULA, eventosExclusao[0].SeqDivisaoTurma);
            var seqAgenda = DivisaoTurmaDomainService.SearchProjectionByKey(eventosExclusao[0].SeqDivisaoTurma, p => p.Turma.SeqAgendaTurma.Value);
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                foreach (var seqEvento in seqsEventoAula)
                {
                    var eventoExclusao = eventosExclusao.First(f => f.Seq == seqEvento);
                    var eventosCompartilhadosDia = eventosCompartilhados
                        .Where(w => w.SeqHorarioAgd == eventoExclusao.SeqHorarioAgd && w.Data == eventoExclusao.Data)
                        .ToList();
                    if (eventosCompartilhadosDia.SMCAny())
                    {
                        var existeEventoRaiz = ValidarExisteEventoRaiz(eventosCompartilhadosDia, eventoExclusao.SeqHorarioAgd, eventoExclusao.Data, eventoExclusao.CodigoLocalSEF);
                        if (!existeEventoRaiz)
                        {
                            var descricaoEvento = DivisaoTurmaDomainService.MontarDescricaoDivisaoTurmaRegraGRD020(eventoExclusao.SeqDivisaoTurma);
                            AtualizarLocalRaizEventoCompartilhamento(eventoExclusao, listaDivisoesCompartilhamento, eventoExclusao.CodigoLocalSEF, seqAgenda, seqTipoEventoAgd, descricaoEvento);
                        }
                    }
                    this.DeleteEntity(seqEvento);
                }

                var seqsEnventosAGD = eventosExclusao
                    .Select(s => new OperacaoExcluirEventoData() { SeqEvento = long.Parse(s.SeqEventoAgd) })
                    .ToList();
                EventoService.ExcluirEventos(seqsEnventosAGD);

                AtualizarRemocaoProfessoresDivisao(seqDivisaoTurma);
                transacao.Commit();
            }
        }

        public string ValidarColisao(List<EventoAulaValidacaoColisaoColaboradorVO> model)
        {
            var spec = new EventoAulaColaboradorFilterSpecification()
            {
                Seqs = model.Select(s => s.SeqColaborador).Distinct().ToList(),
                Datas = model.Select(s => s.DataAula.Date).Distinct().ToList()
            };
            spec.SetOrderBy(o => o.EventoAula.Data);
            var eventos = EventoAulaColaboradorDomainService.SearchProjectionBySpecification(spec, p => new
            {
                p.EventoAula.SeqDivisaoTurma,
                p.EventoAula.Data,
                p.EventoAula.HoraInicio,
                p.EventoAula.HoraFim,
                SeqColaborador = p.SeqColaboradorSubstituto ?? p.SeqColaborador,
                p.EventoAula.CodigoLocalSEF
            }).ToList();
            var compartilhamentos = GradeHorariaCompartilhadaDomainService
                .BuscarDivisoesGradeHorariaCompartilhada(model.First().SeqDivisaoTurma)
                .ToList();

            foreach (var evento in eventos)
            {
                var conflitoHorario = model
                    .Where(w => w.SeqColaborador == evento.SeqColaborador
                             && w.DataAula.Date == evento.Data.Date
                             && w.HoraInicio == evento.HoraInicio.ToString(@"hh\:mm")
                             && w.HoraFim == evento.HoraFim.ToString(@"hh\:mm")
                             && w.SeqDivisaoTurma != evento.SeqDivisaoTurma)
                    .ToList();
                if (conflitoHorario.SMCAny() && compartilhamentos.SMCAny())
                {
                    if (compartilhamentos.Contains(evento.SeqDivisaoTurma) && conflitoHorario.All(a => a.CodigoLocalSEF == evento.CodigoLocalSEF))
                    {
                        return null;
                    }
                }
                if (conflitoHorario.SMCAny())
                {
                    var dadosDivisao = DivisaoTurmaDomainService.SearchProjectionByKey(evento.SeqDivisaoTurma, p => new
                    {
                        p.Seq,
                        CodigoTurma = p.Turma.Codigo,
                        NumeroTurma = p.Turma.Numero,
                        NumeroDivisaoComponente = p.DivisaoComponente.Numero,
                        p.NumeroGrupo
                    });
                    var dadosColaborador = ColaboradorDomainService.SearchProjectionByKey(evento.SeqColaborador, p => new { p.DadosPessoais.Nome, p.DadosPessoais.NomeSocial });
                    var colaborador = PessoaDadosPessoaisDomainService.FormatarNomeSocial(dadosColaborador.Nome, dadosColaborador.NomeSocial);
                    var grupoFormatado = DivisaoTurmaDomainService.FormatarCodigoDivisaoTurma(dadosDivisao.CodigoTurma,
                                                                                              dadosDivisao.NumeroTurma,
                                                                                              dadosDivisao.NumeroDivisaoComponente,
                                                                                              dadosDivisao.NumeroGrupo);
                    // RN_GRD_020
                    var divisao = $"{grupoFormatado} - {DivisaoTurmaDomainService.BuscarDivisaoTurmaCabecalho(dadosDivisao.Seq, quebraLinha: false).TurmaDescricaoFormatado}";
                    return string.Format(MessagesResource.EventoAulaColisaoColaborador, colaborador, divisao, evento.Data.ToString("dd/MM/yyyy"), evento.HoraInicio, evento.HoraFim);
                }
            }
            return null;
        }

        public void AtualizarProfessoresDivisao(List<EventoAulaVO> eventos)
        {
            //RN_GRD_014 - Atualiza colaborador na Grade
            /*Na inclusão e alteração do professor nos eventos de aula verificar se o mesmo já existe na
            divisao_turma_colaborador considerando o ciclo letivo da turma. Se não existir inclui-lo e
            se já existir atualizar a sua carga horária.*/

            //Todos os eventos são da mesma divisão
            var seqDivisaoTurma = eventos.FirstOrDefault().SeqDivisaoTurma;

            var dadosDivisao = DivisaoTurmaDomainService.SearchProjectionByKey(seqDivisaoTurma, p => new
            {
                ColaboradoresEventos = p.EventosAula
                    .SelectMany(sm => sm.Colaboradores
                                        .Select(s => s.SeqColaboradorSubstituto.HasValue ? s.SeqColaboradorSubstituto.Value : s.SeqColaborador)).ToList(),
                DivisaoTurmaColaboradores = p.Colaboradores.Select(s => new DivisaoTurmaColaboradorVO()
                {
                    Seq = s.Seq,
                    SeqColaborador = s.SeqColaborador,
                    SeqDivisao = s.SeqDivisaoTurma,
                    SeqDivisaoTurma = s.SeqDivisaoTurma,
                    QuantidadeCargaHoraria = s.QuantidadeCargaHoraria
                })
            });

            if (dadosDivisao == null)
            {
                //TODO: Verificar como tratar projeção no teste unitário
                return;
            }

            foreach (var divisaoTurmaColaborador in dadosDivisao.DivisaoTurmaColaboradores)
            {
                var aulas = dadosDivisao.ColaboradoresEventos.Count(c => c == divisaoTurmaColaborador.SeqColaborador);
                if (aulas == 0)
                {
                    DivisaoTurmaColaboradorDomainService.ExcluirDivisaoTurmaColaborador(divisaoTurmaColaborador.Seq);
                }
                else if (aulas != divisaoTurmaColaborador.QuantidadeCargaHoraria)
                {
                    divisaoTurmaColaborador.QuantidadeCargaHoraria = (short)aulas;
                    DivisaoTurmaColaboradorDomainService.SalvarDivisaoTurmaColaborador(divisaoTurmaColaborador);
                }
            }
            var seqsNovosColaboradores = dadosDivisao.ColaboradoresEventos
                .Distinct()
                .Except(dadosDivisao.DivisaoTurmaColaboradores.Select(s => s.SeqColaborador));
            foreach (var seqNovoColaborador in seqsNovosColaboradores)
            {
                var novoColaborador = new DivisaoTurmaColaboradorVO()
                {
                    SeqColaborador = seqNovoColaborador,
                    SeqDivisao = seqDivisaoTurma,
                    SeqDivisaoTurma = seqDivisaoTurma,
                    QuantidadeCargaHoraria = (short)dadosDivisao.ColaboradoresEventos.Count(c => c == seqNovoColaborador)
                };
                DivisaoTurmaColaboradorDomainService.SalvarDivisaoTurmaColaborador(novoColaborador);
            }
        }

        public void AtualizarRemocaoProfessoresDivisao(long seqDivisaoTurma)
        {
            //RN_GRD_014 - Atualiza colaborador na Grade
            /*Na inclusão e alteração do professor nos eventos de aula verificar se o mesmo já existe na
            divisao_turma_colaborador considerando o ciclo letivo da turma. Se não existir inclui-lo e
            se já existir atualizar a sua carga horária.*/

            var dadosDivisao = DivisaoTurmaDomainService.SearchProjectionByKey(seqDivisaoTurma, p => new
            {
                ColaboradoresEventos = p.EventosAula
                    .SelectMany(sm => sm.Colaboradores
                                        .Select(s => s.SeqColaboradorSubstituto.HasValue ? s.SeqColaboradorSubstituto.Value : s.SeqColaborador)).ToList(),
                DivisaoTurmaColaboradores = p.Colaboradores.Select(s => new DivisaoTurmaColaboradorVO()
                {
                    Seq = s.Seq,
                    SeqColaborador = s.SeqColaborador,
                    SeqDivisao = s.SeqDivisaoTurma,
                    SeqDivisaoTurma = s.SeqDivisaoTurma,
                    QuantidadeCargaHoraria = s.QuantidadeCargaHoraria
                })
            });

            foreach (var divisaoTurmaColaborador in dadosDivisao.DivisaoTurmaColaboradores)
            {
                var aulas = dadosDivisao.ColaboradoresEventos.Count(c => c == divisaoTurmaColaborador.SeqColaborador);
                if (aulas == 0)
                {
                    DivisaoTurmaColaboradorDomainService.ExcluirDivisaoTurmaColaborador(divisaoTurmaColaborador.Seq);
                }
                else if (aulas != divisaoTurmaColaborador.QuantidadeCargaHoraria)
                {
                    divisaoTurmaColaborador.QuantidadeCargaHoraria = (short)aulas;
                    DivisaoTurmaColaboradorDomainService.SalvarDivisaoTurmaColaborador(divisaoTurmaColaborador);
                }
            }
        }

        public bool ValidarVinculoColaboradorPeriodo(long seqColaborador, DateTime dataInicio, DateTime dataFim)
        {
            var specColaboradorVinculo = new ColaboradorVinculoFilterSpecification()
            {
                SeqColaborador = seqColaborador,
                DataInicio = dataInicio,
                DataFim = dataFim,
            };

            return ColaboradorVinculoDomainService.Count(specColaboradorVinculo) > 0;
        }

        /// <summary>
        /// Buscar eventos do aluno por periodo
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="inicioPeriodo">Inicio do periodo</param>
        /// <param name="fimPeriodo">Fim do periodo</param>
        /// <returns></returns>
        public virtual List<EventoAulaVO> BuscarEventoAulaAlunoNoPeriodo(long seqAluno, DateTime? inicioPeriodo, DateTime? fimPeriodo)
        {
            List<long> seqsDvisoesAluno = PlanoEstudoItemDomainService.BuscarTurmasAlunoNoPeriodo(seqAluno, inicioPeriodo.Value, fimPeriodo)
                                                                      .Select(s => s.SeqDivisaoTurma).Cast<long>().ToList();

            //Caso não tenha divisão para o periodo do plano de estudo do aluno sai da validação
            if (seqsDvisoesAluno.Count == 0)
            {
                return new List<EventoAulaVO>();
            }

            var spec = new EventoAulaFilterSpecification() { SeqsDivisaoTurma = seqsDvisoesAluno, InicioPeriodo = inicioPeriodo, FimPeriodo = fimPeriodo };
            var eventos = SearchProjectionBySpecification(spec, p => new EventoAulaVO
            {
                CodigoLocalSEF = p.CodigoLocalSEF,
                CodigoRecorrencia = p.CodigoRecorrencia,
                Data = p.Data,
                DataCancelamento = p.DataCancelamento,
                DiaSemana = (int)p.DiaSemana,
                Feriado = p.Feriado,
                HoraFim = p.HoraFim.ToString(),
                HoraInicio = p.HoraInicio.ToString(),
                Local = p.Local,
                Seq = p.Seq,
                SeqDivisaoTurma = p.SeqDivisaoTurma,
                SeqEventoAgd = p.SeqEventoAgd,
                SeqHorarioAgd = p.SeqHorarioAgd,
                SituacaoApuracaoFrequencia = p.SituacaoApuracaoFrequencia,
                Turno = p.Turno
            }).ToList();

            return eventos;
        }

        public List<string> ValidarColisaoHorarioAluno(List<EventoAulaVO> eventos)
        {
            long seqDivisaoTurma = eventos.FirstOrDefault().SeqDivisaoTurma;

            List<DivisaoTurmaRelatorioAlunoVO> alunosDivisao = DivisaoTurmaDomainService.BuscarAlunosDivisaoTurma(seqDivisaoTurma).OrderBy(o => o.NomeAluno).ToList();

            var periodoTurma = DivisaoTurmaDomainService.SearchProjectionByKey(seqDivisaoTurma, p => new
            {
                p.Turma.DataInicioPeriodoLetivo,
                p.Turma.DataFimPeriodoLetivo
            });

            var mensagemColisao = new HashSet<string>();

            foreach (var eventoAula in eventos)
            {
                alunosDivisao.ForEach(aluno =>
                {
                    var eventosAluno = BuscarEventoAulaAlunoNoPeriodo(aluno.SeqPessoaAtuacao, periodoTurma.DataInicioPeriodoLetivo, periodoTurma.DataFimPeriodoLetivo);

                    eventosAluno
                        .Where(w => w.DiaSemana == eventoAula.DiaSemana && w.SeqHorarioAgd == eventoAula.SeqHorarioAgd && w.Data == eventoAula.Data)
                        .SMCForEach(evento =>
                        {
                            int codigoTurma = DivisaoTurmaDomainService.SearchProjectionByKey(evento.SeqDivisaoTurma, p => p.Turma.Codigo);
                            mensagemColisao.Add($"{aluno.NumeroRegistroAcademico} - {aluno.NomeAluno} (Turma {codigoTurma})");
                        });
                });
            }

            return mensagemColisao.Distinct().ToList();
        }

        public List<string> ValidarColisaoHorarioSolicitacaoServico(List<EventoAulaVO> eventos)
        {
            long seqDivisaoTurma = eventos.FirstOrDefault().SeqDivisaoTurma;

            var periodoTurma = DivisaoTurmaDomainService.SearchProjectionByKey(seqDivisaoTurma, p => new
            {
                p.Turma.DataInicioPeriodoLetivo,
                p.Turma.DataFimPeriodoLetivo,
                p.Turma.SeqCicloLetivoInicio
            });

            var specSolicitacao = new SolicitacaoMatriculaFilterSpecification()
            {
                SeqCicloLetivoProcesso = periodoTurma.SeqCicloLetivoInicio,
                CategoriasSituacao = new[] { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento },
                SeqDivisaoTurma = seqDivisaoTurma,
                ItemAtivoDivisao = true
            };

            var solicitacoesCicloTurma = SolicitacaoMatriculaDomainService.SearchProjectionBySpecification(specSolicitacao, p => new
            {
                p.NumeroProtocolo,
                p.PessoaAtuacao.DadosPessoais.Nome,
                p.PessoaAtuacao.DadosPessoais.NomeSocial,
                ItensFinalizados = p
                    .Itens
                    .Where(w => !w.PertencePlanoEstudo && w
                        .HistoricosSituacao
                        .OrderByDescending(o => o.Seq)
                        .FirstOrDefault()
                        .SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                    .Select(s => s.SeqDivisaoTurma),
                ItensNaoAlterados = p
                    .Itens
                    .Where(w => w.PertencePlanoEstudo && w
                        .HistoricosSituacao
                        .OrderByDescending(o => o.Seq)
                        .FirstOrDefault()
                        .SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado)
                    .Select(s => s.SeqDivisaoTurma)
            }).ToList();

            var mensagemColisao = new SortedSet<(string nome, string protocolo, int turma)>();

            foreach (var eventoAula in eventos)
            {
                solicitacoesCicloTurma.ForEach(solicitacao =>
                {
                    var seqDivisoes = solicitacao.ItensFinalizados.Where(w => w.HasValue).Select(s => s.Value).ToList();
                    seqDivisoes.AddRange(solicitacao.ItensNaoAlterados.Where(w => w.HasValue).Select(s => s.Value));
                    var eventosSolicitacao = BuscarEventosDivisoes(seqDivisoes);

                    eventosSolicitacao
                        .Where(w => w.DiaSemana == eventoAula.DiaSemana &&
                                             w.SeqHorarioAgd == eventoAula.SeqHorarioAgd &&
                                             w.Data == eventoAula.Data)
                        .SMCForEach(evento =>
                        {
                            int codigoTurma = DivisaoTurmaDomainService.SearchProjectionByKey(evento.SeqDivisaoTurma, p => p.Turma.Codigo);
                            var nome = PessoaDadosPessoaisDomainService.FormatarNomeSocial(solicitacao.Nome, solicitacao.NomeSocial);
                            mensagemColisao.Add((nome, solicitacao.NumeroProtocolo, codigoTurma));
                        });
                });
            }

            return mensagemColisao.Select(s => $"{s.protocolo} - {s.nome} (Turma {s.turma})").Distinct().ToList();
        }

        /// <summary>
        /// Valida as colisões de horários entre as divisões de turma informadas
        /// </summary>
        /// <param name="seqsDivisaoTurma">Seq divisões de turma a serem avaliadas</param>
        /// <returns>Seqs das divisões que colidirem</returns>
        public List<long> ValidarColisaoHorariosDivisoes(List<long> seqsDivisaoTurma)
        {
            var eventos = BuscarEventosDivisoes(seqsDivisaoTurma);
            DateTime dataAtual = DateTime.Now;
            var colisoes = eventos
                .Where(w => w.Data.Add(TimeSpan.Parse(w.HoraFim)) >= dataAtual)
                .GroupBy(g => new { g.SeqHorarioAgd, g.Data })
                .Where(w => w.Count() > 1)
                .SelectMany(sm => sm.Select(s => s.SeqDivisaoTurma))
                .Distinct()
                .ToList();
            return colisoes;
        }

        /// <summary>
        /// Buscar eventos aula em lote
        /// </summary>
        /// <param name="filtro">Filtros da pesquisa</param>
        /// <returns>Lista de eventos aula lote</returns>
        public List<EventoAulaLoteVO> BuscarEventosAulaLote(EventoAulaFiltroVO filtro)
        {
            List<EventoAulaLoteVO> retorno = new List<EventoAulaLoteVO>();
            EventoAulaFilterSpecification spec = filtro.Transform<EventoAulaFilterSpecification>();

            var listaEventos = SearchProjectionBySpecification(spec, p => new EventoAulaVO
            {
                Seq = p.Seq,
                SeqDivisaoTurma = p.SeqDivisaoTurma,
                SeqTurma = p.DivisaoTurma.SeqTurma,
                DataLimiteApuracaoFrequencia = p.DataLimiteApuracaoFrequencia,
                Data = p.Data,
                HoraInicio = p.HoraInicio.ToString(),
                HoraFim = p.HoraFim.ToString(),
                Colaboradores = p.Colaboradores.Select(s => new EventoAulaColaboradorVO
                {
                    Seq = s.Seq,
                    SeqColaborador = s.SeqColaborador,
                    SeqColaboradorSubstituto = s.SeqColaboradorSubstituto,
                    NomeColaborador = s.Colaborador.DadosPessoais.Nome,
                    NomeSocialColaborador = s.Colaborador.DadosPessoais.NomeSocial,
                    NomeColaboradorSubstituto = s.ColaboradorSubstituto.DadosPessoais.Nome,
                    NomeSocialColaboradorSubstituto = s.ColaboradorSubstituto.DadosPessoais.NomeSocial,
                }).ToList(),
            }).ToList();

            listaEventos.ForEach(eventoAula =>
            {
                //valida se o usuario logado tem acesso a turma do evento
                var turma = this.TurmaDomainService.SearchByKey(eventoAula.SeqTurma);

                if (turma == null)
                {
                    return;
                }

                //Se situação da alula for não apurada irá verificar se esta dentro ou fora do periodo
                if (filtro.SituacaoApuracaoFrequencia == SituacaoApuracaoFrequencia.NaoApurada)
                {
                    DateTime dataLimite = DateTime.Now;
                    if (filtro.DentroPerido.GetValueOrDefault())
                    {
                        if (eventoAula.DataLimiteApuracaoFrequencia >= dataLimite)
                        {
                            retorno.Add(MontarEventoAulaLoteRetorno(eventoAula));
                        }
                    }
                    else
                    {
                        if (eventoAula.DataLimiteApuracaoFrequencia < dataLimite)
                        {
                            retorno.Add(MontarEventoAulaLoteRetorno(eventoAula));
                        }
                    }
                }
                else
                {
                    retorno.Add(MontarEventoAulaLoteRetorno(eventoAula));
                }
            });

            return retorno;
        }

        /// <summary>
        /// Liberar eventos aula para novas apurações, alterando para uma nova data valida
        /// </summary>
        /// <param name="seqsEventoAula">Sequenciais dos eventos aula</param>
        public void LiberarEventosAulaApuracao(List<long> seqsEventoAula)
        {
            seqsEventoAula.ForEach(seq =>
            {
                EventoAula eventoAula = this.SearchByKey(seq);

                eventoAula.DataLimiteApuracaoFrequencia = CalcularDataLimiteApuracaoFrequencia(DateTime.Now, eventoAula.SeqDivisaoTurma, eventoAula.DiaSemana, eventoAula.Turno);

                UpdateEntity(eventoAula, u => u.DataLimiteApuracaoFrequencia);
            });
        }

        /// <summary>
        /// Liberar eventos aula para correção, alterando o prazo de alteração
        /// </summary>
        /// <param name="seqsEventoAula">Sequenciais dos eventos aula</param>
        public void LiberarEventosAulaCorrecao(List<long> seqsEventoAula)
        {
            seqsEventoAula.ForEach(seq =>
            {
                EventoAula eventoAula = this.SearchByKey(seq);

                eventoAula.UsuarioPrimeiraApuracaoFrequencia = null;
                eventoAula.DataPrimeiraApuracaoFrequencia = DateTime.Now;

                SaveEntity(eventoAula);
            });
        }

        /// <summary>
        /// Alterar situação do evento aula para "não executada" ou "não apurada"
        /// </summary>
        /// <param name="seqsEventoAula">Sequenciais dos eventos aula</param>
        /// <param name="situacaoApuracaoFrequencia">Situação para a qual o evneto será alterada</param>
        public void AlterarEventosAulasNaoExecutadaOuNaoApurada(List<long> seqsEventoAula, SituacaoApuracaoFrequencia situacaoApuracaoFrequencia)
        {
            seqsEventoAula.ForEach(seq =>
            {
                EventoAula eventoAula = this.SearchByKey(seq);

                //Se não existirem faltas lançadas, alterar somente as a situação do evento
                eventoAula.SituacaoApuracaoFrequencia = situacaoApuracaoFrequencia;
                //Caso o esteja alterando para não apurada calcular novamente data limite de apuração
                if (situacaoApuracaoFrequencia == SituacaoApuracaoFrequencia.NaoApurada)
                {
                    eventoAula.DataLimiteApuracaoFrequencia = CalcularDataLimiteApuracaoFrequencia(DateTime.Now, eventoAula.SeqDivisaoTurma, eventoAula.DiaSemana, eventoAula.Turno);
                }
                eventoAula.UsuarioPrimeiraApuracaoFrequencia = null;
                eventoAula.DataPrimeiraApuracaoFrequencia = null;

                ApuracaoFrequenciaGradeFilterSpecification spec = new ApuracaoFrequenciaGradeFilterSpecification() { SeqEventoAula = seq };
                List<ApuracaoFrequenciaGrade> apuracoes = ApuracaoFrequenciaGradeDomainService.SearchBySpecification(spec).ToList();

                //AtualizarFrquenciaHistoricoPorEvento(eventoAula, apuracoes);

                //Apagar as faltas (que não tiverem ocorrência) das aulas selecionadas e alterar a situação delas para "Não apurada
                apuracoes.ForEach(apuracao =>
                {
                    if (!apuracao.OcorrenciaFrequencia.HasValue)
                    {
                        ApuracaoFrequenciaGradeDomainService.DeleteEntity(apuracao);
                    }
                });

                SaveEntity(eventoAula);
            });
        }

        /// <summary>
        /// Valida se todos os eventos da divisão informada estão sem colocaborador e local associadoas
        /// </summary>
        /// <param name="seqDivisaoTurma"></param>
        public bool ValidarEventosSemColaboradorELocal(long seqDivisaoTurma)
        {
            var specDivisao = new EventoAulaFilterSpecification() { SeqDivisaoTurma = seqDivisaoTurma };
            var specComColaborador = new EventoAulaFilterSpecification() { ComColaborador = true };
            var specComLocal = new EventoAulaFilterSpecification() { ComLocal = true };
            var specComColabodorOuLocal = new SMCOrSpecification<EventoAula>(specComColaborador, specComLocal);
            var specDivisaoComColaboradorOuLocal = new SMCAndSpecification<EventoAula>(specDivisao, specComColabodorOuLocal);
            return Count(specDivisaoComColaboradorOuLocal) == 0;
        }

        private void EditarColaboradoresPrincipaisEventos(List<EventoAulaColaborador> colaboradores, List<long> seqEventos, long seqEventoTemplate)
        {
            colaboradores = colaboradores ?? new List<EventoAulaColaborador>();
            var spec = new EventoAulaFilterSpecification() { Seqs = seqEventos };
            var eventos = SearchBySpecification(spec).ToList();

            //Busca colaboradores do template
            var colaboradoresEventoTemplateInDB = this.EventoAulaColaboradorDomainService
                                     .SearchBySpecification(new EventoAulaColaboradorFilterSpecification() { SeqEventoAula = seqEventoTemplate }).ToList();
            //Mapeia colaboradores do template que foram removidos e adicioandos
            var colaboradoresRemover = colaboradoresEventoTemplateInDB.Where(w => !colaboradores.Select(s => s.SeqColaborador).Contains(w.SeqColaborador) && w.SeqColaboradorSubstituto == null).ToList();
            var colaboradoresAdicionar = colaboradores.Where(w => !colaboradoresEventoTemplateInDB.Select(s => s.SeqColaborador).Contains(w.SeqColaborador)).ToList();

            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                foreach (var evento in eventos)
                {
                    evento.Colaboradores = new List<EventoAulaColaborador>();
                    var colaboradoresSubst = this.EventoAulaColaboradorDomainService
                                                    .SearchBySpecification(new EventoAulaColaboradorFilterSpecification() { SeqEventoAula = evento.Seq })
                                                    .Where(w => w.SeqColaboradorSubstituto.HasValue).ToList();
                    colaboradoresSubst.ForEach(substituto =>
                    {
                        evento.Colaboradores.Add(substituto);
                    });

                    //Busca colaboradores sem substituto
                    var colaboradoresSemSubstituto = this.EventoAulaColaboradorDomainService
                                                         .SearchBySpecification(new EventoAulaColaboradorFilterSpecification() { SeqEventoAula = evento.Seq })
                                                         .Where(w => !w.SeqColaboradorSubstituto.HasValue).ToList();

                    colaboradoresSemSubstituto.ForEach(semSubstituto =>
                    {
                        if (!colaboradoresRemover.Select(s => s.SeqColaborador).Contains(semSubstituto.SeqColaborador))
                        {
                            evento.Colaboradores.Add(semSubstituto);
                        }
                    });

                    colaboradoresAdicionar.ForEach(colaboradorAdicionar =>
                    {
                        if (!evento.Colaboradores.Select(s => s.SeqColaborador).Contains(colaboradorAdicionar.SeqColaborador) &&
                           (!colaboradoresSemSubstituto.Select(s => s.SeqColaborador).Contains(colaboradorAdicionar.SeqColaborador)))
                        {
                            evento.Colaboradores.Add(colaboradorAdicionar.SMCClone());
                        }
                    });
                    ValidarColaboradorAtivo(evento);
                    SaveEntity(evento);
                }
                AtualizarProfessoresDivisao(eventos.TransformList<EventoAulaVO>());
                transacao.Commit();
            }
        }

        private void EditarColaboradoresSubstitutosEventos(List<EventoAulaColaborador> colaboradores, List<long> seqEventos, long seqEventoTemplate)
        {
            colaboradores = colaboradores ?? new List<EventoAulaColaborador>();
            var spec = new EventoAulaFilterSpecification() { Seqs = seqEventos };
            var eventos = SearchBySpecification(spec).ToList();

            //Busca colaboradores do template
            var colaboradoresEventoTemplateInDB = EventoAulaColaboradorDomainService
                                     .SearchBySpecification(new EventoAulaColaboradorFilterSpecification() { SeqEventoAula = seqEventoTemplate }).ToList();
            //Mapeia colaboradores do template que foram removidos e adicioandos
            var colaboradoresUpdate = colaboradores
                .Where(w => colaboradoresEventoTemplateInDB.Any(a => w.SeqColaborador == a.SeqColaborador && w.SeqColaboradorSubstituto != a.SeqColaboradorSubstituto))
                .ToList();

            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                foreach (var evento in eventos)
                {
                    var colaboradoresPrincipais = EventoAulaColaboradorDomainService
                                                    .SearchBySpecification(new EventoAulaColaboradorFilterSpecification() { SeqEventoAula = evento.Seq })
                                                    .Where(w => !w.SeqColaboradorSubstituto.HasValue).ToList();
                    evento.Colaboradores = colaboradoresPrincipais;
                    var seqColaboradores = evento.Colaboradores.Select(s => s.SeqColaborador).ToArray();

                    //Busca colaboradores sem substituto
                    var colaboradoresSubstitutos = EventoAulaColaboradorDomainService
                                                         .SearchBySpecification(new EventoAulaColaboradorFilterSpecification() { SeqEventoAula = evento.Seq })
                                                         .Where(w => w.SeqColaboradorSubstituto.HasValue)
                                                         .Where(w => !seqColaboradores.Contains(w.SeqColaborador))
                                                         .ToList();

                    colaboradoresSubstitutos.ForEach(f => evento.Colaboradores.Add(f));

                    colaboradoresUpdate.ForEach(colaboradorAtualizar =>
                    {
                        var colaboradorEvento = evento.Colaboradores.FirstOrDefault(f => f.SeqColaborador == colaboradorAtualizar.SeqColaborador);
                        if (colaboradorEvento != null && colaboradorEvento.SeqColaboradorSubstituto != colaboradorAtualizar.SeqColaboradorSubstituto)
                        {
                            colaboradorEvento.SeqColaboradorSubstituto = colaboradorAtualizar.SeqColaboradorSubstituto;
                        }
                    });
                    ValidarColaboradorAtivo(evento);
                    SaveEntity(evento);
                }
                AtualizarProfessoresDivisao(eventos.TransformList<EventoAulaVO>());
                transacao.Commit();
            }
        }

        private List<EventoAulaVO> BuscarEventosDivisoes(List<long> seqsDivisaoTurma)
        {
            if (!seqsDivisaoTurma.SMCAny())
            {
                return new List<EventoAulaVO>();
            }
            var spec = new EventoAulaFilterSpecification() { SeqsDivisaoTurma = seqsDivisaoTurma };
            return SearchBySpecification(spec).TransformList<EventoAulaVO>();
        }

        private void RemoverColaboradorEventos(IEnumerable<EventoAulaVO> eventos, long seqColaborador)
        {
            foreach (var evento in eventos)
            {
                if (evento.Colaboradores.SMCAny())
                {
                    foreach (var colaborador in evento.Colaboradores)
                    {
                        if (colaborador.SeqColaboradorSubstituto == seqColaborador)
                        {
                            colaborador.SeqColaboradorSubstituto = null;
                            EventoAulaColaboradorDomainService.UpdateFields(colaborador, p => p.SeqColaboradorSubstituto);
                        }
                        else if (colaborador.SeqColaborador == seqColaborador)
                        {
                            EventoAulaColaboradorDomainService.DeleteEntity(colaborador.Seq);
                        }
                    }
                }
            }
        }

        private bool ValidarDivisao(EventoAulaDivisaoTurmaVO divisao)
        {
            return divisao.CargaHorariaGrade.GetValueOrDefault() > 0
                && divisao.AulaSabado.HasValue
                && divisao.TipoDistribuicaoAula.HasValue
                && divisao.TipoDistribuicaoAula != TipoDistribuicaoAula.Nenhum
                && divisao.TipoPulaFeriado.HasValue
                && divisao.TipoPulaFeriado != TipoPulaFeriado.Nenhum;
        }

        //FIX: Regra foi criada e posteriormente removida para avaliação da necessidade de reavaliação de historico escolar
        /*private void AtualizarFrquenciaHistoricoPorEvento(EventoAula eventoAula, List<ApuracaoFrequenciaGrade> apuracoes)
        {
            long seqTurma = DivisaoTurmaDomainService.SearchProjectionByKey(eventoAula.SeqDivisaoTurma, p => p.SeqTurma);
            List<DiarioTurmaAlunoVO> alunosDivisao = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, eventoAula.SeqDivisaoTurma, null, null).ToList();

            alunosDivisao.ForEach(aluno =>
            {
                int totalFaltasRemover = 0;
                if (aluno.SeqHistoricoEscolar.HasValue)
                {
                    totalFaltasRemover += apuracoes.Where(w => w.Frequencia == Frequencia.Presente && w.SeqAlunoHistoricoCicloLetivo == aluno.SeqAlunoHistoricoCicloLetivo)
                        .Where(w => w.OcorrenciaFrequencia == OcorrenciaFrequencia.Sancao)
                        .Count();

                    totalFaltasRemover += apuracoes.Where(w => w.Frequencia == Frequencia.Ausente && w.SeqAlunoHistoricoCicloLetivo == aluno.SeqAlunoHistoricoCicloLetivo)
                                            .Where(w => !w.OcorrenciaFrequencia.HasValue ||
                                                        w.OcorrenciaFrequencia != OcorrenciaFrequencia.AbonoRetificacao)
                                            .Count();

                    if (totalFaltasRemover > 0)
                    {
                        HistoricoEscolarCompletoVO historicoEscolarAtual = HistoricoEscolarDomainService.BuscarHistoricoEscolarTurma(seqTurma, aluno.SeqPessoaAtuacao);
                        SituacaoHistoricoEscolar situacaoAlunoAtual = historicoEscolarAtual.SituacaoHistoricoEscolar;
                        //Prepara para calcular a nova situação do aluno
                        int? faltasAtualizadas = aluno.Faltas - totalFaltasRemover;
                        var alunoHistoricoVO = aluno.Transform<HistoricoEscolarSituacaoFinalVO>();
                        alunoHistoricoVO.Faltas = (short)faltasAtualizadas;
                        var situacaoCalculada = HistoricoEscolarDomainService.CalcularSituacaoFinal(alunoHistoricoVO);
                        var percentualFrequenciaCalculada = 100 - (faltasAtualizadas / (decimal)aluno.CargaHoraria.GetValueOrDefault() * 100);

                        if (situacaoAlunoAtual != situacaoCalculada || historicoEscolarAtual.Faltas != faltasAtualizadas)
                        {
                            HistoricoEscolar historicoEscolar = new HistoricoEscolar
                            {
                                Seq = aluno.SeqHistoricoEscolar.Value,
                                SituacaoHistoricoEscolar = situacaoCalculada,
                                Faltas = (short)faltasAtualizadas,
                                PercentualFrequencia = percentualFrequenciaCalculada
                            };

                            HistoricoEscolarDomainService.UpdateFields(historicoEscolar, x => x.SituacaoHistoricoEscolar, x => x.Faltas, x => x.PercentualFrequencia);
                        }
                    }
                }
            });

            //verifico quem tem falta para validar se por ventura quem historico escolar fechado para este evento
            var faltas = apuracoes.Where(w => (w.Frequencia == Frequencia.Ausente && (w.OcorrenciaFrequencia == null || w.OcorrenciaFrequencia == OcorrenciaFrequencia.Sancao)) ||
                                                (w.Frequencia == Frequencia.Presente && w.OcorrenciaFrequencia == OcorrenciaFrequencia.Sancao)).ToList();
        }*/

        private DateTime CalcularDataLimiteApuracaoFrequencia(DateTime dataBase, long seqDivisaoTurma, TipoDiaSemana diaSemana, short turno)
        {
            DateTime retorno = new DateTime();

            var dadosTurma = DivisaoTurmaDomainService.SearchProjectionByKey(seqDivisaoTurma, p => new
            {
                SeqAgenda = p.Turma.SeqAgendaTurma ?? 0,
                p.SeqTurma,
                p.HistoricoConfiguracaoGradeAtual.AulaSabado,
                p.Turma.DataInicioPeriodoLetivo,
                p.Turma.DataFimPeriodoLetivo,
                SeqCursoOfertaLocalidade = p.Turma.ConfiguracoesComponente.SelectMany(s => s.RestricoesTurmaMatriz)
                                .FirstOrDefault(f => f.OfertaMatrizPrincipal)
                                .CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
            });

            int? codUnidadeSeo = EntidadeDomainService.RecuperarCodigoUnidadeSeo(dadosTurma.SeqCursoOfertaLocalidade);
            short diasUteis = InstituicaoNivelDomainService.BuscarInstituicaoNivelPorTurma(dadosTurma.SeqTurma).QuantidadeDiasPrazoApuracaoFrequencia;
            List<FeriadoData> feriados = AcademicoService.BuscarFeriados((long)codUnidadeSeo.GetValueOrDefault(), dadosTurma.DataInicioPeriodoLetivo, dadosTurma.DataFimPeriodoLetivo);
            List<HorarioCompletoData> tabelaHorarios = TabelaHorarioService.BuscarTabelaHorarioAgendaTurma(new TabelaHorarioAgendaTurmaFiltroData()
            {
                SeqAgenda = dadosTurma.SeqAgenda,
                DataInicioPeriodoLetivo = dadosTurma.DataInicioPeriodoLetivo,
                DataFimPeriodoLetivo = dadosTurma.DataFimPeriodoLetivo
            }).Horarios.Where(w => w.Turno == (Turno)turno).OrderByDescending(o => o.HoraFim).ToList();
            TimeSpan horarioFimTurno = tabelaHorarios.FirstOrDefault(f => f.DiaSemana == diaSemana).HoraFim;
            DateTime datahora = dataBase.Date + horarioFimTurno;

            retorno = AcademicoService.CalcularDataPorDiaUtil(datahora, diasUteis, feriados, dadosTurma.AulaSabado);

            return retorno;
        }

        private EventoAulaLoteVO MontarEventoAulaLoteRetorno(EventoAulaVO eventoAula)
        {
            EventoAulaLoteVO itemRetorno = new EventoAulaLoteVO();
            bool alunosHistoricoEscolar = false;

            List<DiarioTurmaAlunoVO> alunosDivisao = TurmaDomainService.BuscarDiarioTurmaAluno(eventoAula.SeqTurma, eventoAula.SeqDivisaoTurma, null, null).ToList();

            alunosHistoricoEscolar = alunosDivisao.Where(w => w.SeqHistoricoEscolar.HasValue).Count() > 0;

            var dadosDivisao = DivisaoTurmaDomainService.SearchProjectionByKey(eventoAula.SeqDivisaoTurma, p => new
            {
                DescricaoNivelEnsino = p.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal)
                                              .RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal)
                                              .CursoOfertaLocalidadeTurno
                                              .CursoOfertaLocalidade
                                              .CursoOferta
                                              .Curso
                                              .NivelEnsino.Descricao,
                SeqDivisaoTurma = p.Seq,
                CodigoTurma = p.Turma.Codigo,
                NumeroTurma = p.Turma.Numero,
                NumeroDivisaoComponente = p.DivisaoComponente.Numero,
                NumeroGrupoDivisao = p.NumeroGrupo
            });

            short prazoAlteracao = InstituicaoNivelDomainService.BuscarInstituicaoNivelPorTurma(eventoAula.SeqTurma).QuantidadeMinutosPrazoAlteracaoFrequencia;
            string codigoDivisaoTurma = DivisaoTurmaDomainService.FormatarCodigoDivisaoTurma(dadosDivisao.CodigoTurma,
                                                                                             dadosDivisao.NumeroTurma,
                                                                                             dadosDivisao.NumeroDivisaoComponente,
                                                                                             dadosDivisao.NumeroGrupoDivisao);

            itemRetorno.Seq = eventoAula.Seq;
            itemRetorno.DescricaoDivisaoTurma = DivisaoTurmaDomainService.ObterDescricaoDivisaoTurma(eventoAula.SeqDivisaoTurma).Replace("<br />", " - ");
            itemRetorno.DescricaoColaboradores = FormatarListaNomeColaboradores(eventoAula.Colaboradores);
            itemRetorno.DataHorario = $"{eventoAula.Data.SMCDataAbreviada()} {eventoAula.HoraInicio.Substring(0, 5)} - {eventoAula.HoraFim.Substring(0, 5)}";
            itemRetorno.QtdAlunoFalta = RetornarNumeroFaltasEventoAula(eventoAula.Seq, eventoAula.SeqDivisaoTurma, eventoAula.SeqTurma);
            itemRetorno.PrazoApuracao = eventoAula.DataLimiteApuracaoFrequencia.ToString();
            itemRetorno.AlunosHistoricoEscolar = alunosHistoricoEscolar;
            itemRetorno.DescricaoNivelEnsino = dadosDivisao.DescricaoNivelEnsino;
            itemRetorno.PrazoAlteracao = TimeSpan.FromMinutes(prazoAlteracao).ToString();
            itemRetorno.CodigoDivisaoTurma = codigoDivisaoTurma;
            return itemRetorno;
        }

        private string FormatarListaNomeColaboradores(List<EventoAulaColaboradorVO> colaboradores)
        {
            List<string> retorno = new List<string>();

            colaboradores.ForEach(colaborador =>
            {
                var descricaoColaborador = PessoaDadosPessoaisDomainService.FormatarNomeSocial(colaborador.NomeColaborador, colaborador.NomeSocialColaborador);

                if (colaborador.SeqColaboradorSubstituto.HasValue)
                {
                    var descricaoColaboradorSubstituto = PessoaDadosPessoaisDomainService
                        .FormatarNomeSocial(colaborador.NomeColaboradorSubstituto, colaborador.NomeSocialColaboradorSubstituto);
                    descricaoColaborador = $"{descricaoColaboradorSubstituto} (substituto(a) de {descricaoColaborador})";
                }

                retorno.Add(descricaoColaborador);
            });

            return string.Join(";", retorno);
        }

        private string RetornarNumeroFaltasEventoAula(long seqEventoAula, long seqDivisaoTurma, long seqTurma)
        {
            string retorno = string.Empty;
            int totalFaltas = 0;
            int totalAlunos = 0;

            ApuracaoFrequenciaGradeFilterSpecification spec = new ApuracaoFrequenciaGradeFilterSpecification() { SeqEventoAula = seqEventoAula };
            var apuracoes = ApuracaoFrequenciaGradeDomainService.SearchProjectionBySpecification(spec, p => new
            {
                p.Frequencia,
                p.OcorrenciaFrequencia,
                p.EventoAula.SeqDivisaoTurma,
                p.EventoAula.DivisaoTurma.SeqTurma
            }).ToList();

            totalAlunos = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, seqDivisaoTurma, null, null).Count();

            totalFaltas += apuracoes.Where(w => w.Frequencia == Frequencia.Presente)
                                .Where(w => w.OcorrenciaFrequencia == OcorrenciaFrequencia.Sancao)
                                .Count();

            totalFaltas += apuracoes.Where(w => w.Frequencia == Frequencia.Ausente)
                                .Where(w => !w.OcorrenciaFrequencia.HasValue ||
                                            w.OcorrenciaFrequencia != OcorrenciaFrequencia.AbonoRetificacao)
                                .Count();
            retorno = $"{totalAlunos} / {totalFaltas}";

            return retorno;
        }

        /// <summary>
        /// Verificar se o professor possui vinculo ativo na data do evento no qual esta sendo associado. Caso
        /// não possua, não gravar o professor nesta data do evento.A mesma regra deve ser verificada para o
        /// professor substituto.
        /// </summary>
        /// <param name="eventoAula">Evento a ser validado</param>
        private void ValidarColaboradorAtivo(EventoAula eventoAula)
        {

            if (!eventoAula.Colaboradores.SMCAny())
            {
                return;
            }

            var dadosTurma = DivisaoTurmaDomainService.SearchProjectionByKey(eventoAula.SeqDivisaoTurma, p => new
            {
                SeqTurma = p.SeqTurma,
                SeqCursoOfertaLocalidade = p.Turma.ConfiguracoesComponente.SelectMany(s => s.RestricoesTurmaMatriz)
                                            .FirstOrDefault(f => f.OfertaMatrizPrincipal)
                                            .CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
            });

            eventoAula.Colaboradores.ToList().ForEach(colaborador =>
            {
                if (colaborador.SeqColaboradorSubstituto.HasValue)
                {
                    var dadosColaborador = ColaboradorDomainService.BuscarColaboradoresAptoLecionarGrade(new ColaboradorFiltroVO()
                    {
                        Seq = colaborador.SeqColaboradorSubstituto,
                        SeqTurma = dadosTurma.SeqTurma,
                        //VinculoAtivo = true, //Conforme demanda 49686 passamos a trazer todos os colaboradores com vinculo no periodo letivo
                        AptoLecionarComponenteTurma = true,
                        TipoAtividade = TipoAtividadeColaborador.Aula,
                        SeqCursoOfertaLocalidade = dadosTurma.SeqCursoOfertaLocalidade
                    }).ToList();

                    bool colaboradorPossuiVinculoValido = false;
                    foreach (var dadoColaborador in dadosColaborador)
                    {
                        foreach (var vinculo in dadoColaborador.Vinculos)
                        {
                            if (eventoAula.Data >= vinculo.DataInicio && (!vinculo.DataFim.HasValue || eventoAula.Data <= vinculo.DataFim))
                            {
                                colaboradorPossuiVinculoValido = true;
                                break;
                            }
                        }

                    }

                    if (!colaboradorPossuiVinculoValido)
                    {
                        colaborador.SeqColaboradorSubstituto = null;
                    }
                }
                else
                {
                    var dadosColaborador = ColaboradorDomainService.BuscarColaboradoresAptoLecionarGrade(new ColaboradorFiltroVO()
                    {
                        Seq = colaborador.SeqColaborador,
                        SeqTurma = dadosTurma.SeqTurma,
                        AptoLecionarComponenteTurma = true,
                        TipoAtividade = TipoAtividadeColaborador.Aula,
                        SeqCursoOfertaLocalidade = dadosTurma.SeqCursoOfertaLocalidade
                    }).ToList();

                    bool colaboradorPossuiVinculoValido = false;
                    foreach (var dadoColaborador in dadosColaborador)
                    {
                        foreach (var vinculo in dadoColaborador.Vinculos)
                        {
                            if (eventoAula.Data >= vinculo.DataInicio && (!vinculo.DataFim.HasValue || eventoAula.Data <= vinculo.DataFim))
                            {
                                colaboradorPossuiVinculoValido = true;
                                break;
                            }
                        }
                    }

                    if (!colaboradorPossuiVinculoValido)
                    {
                        var colaboradorRemover = eventoAula.Colaboradores.FirstOrDefault(w => w.SeqColaborador == colaborador.SeqColaborador);
                        eventoAula.Colaboradores.Remove(colaboradorRemover);
                    }
                }
            });
        }

        private void AtualizarLocalEvento(EventoAula evento, int? codigoLocalSEF, string local)
        {
            evento.CodigoLocalSEF = codigoLocalSEF;
            evento.Local = local;
            UpdateFields(evento, f => f.CodigoLocalSEF, f => f.Local);
        }

        /// <summary>
        /// Valida se o local informado no evento que foi utilizado no compartilhamento
        /// </summary>
        /// <param name="evento">Evento a ser validado</param>
        /// <param name="seqsDivisoesCompartilhadas">Divisões de turma compartilhadas</param>
        /// <param name="seqCodigolocalSEFSerValidado">Sequencial do codigo local SEF a ser validado</param>
        /// <returns>True caso o local informado no evento ainda não esteja sendo utilizado no compartilhamento</returns>
        private bool ValidarCompartilhamentoEventoLocal(EventoAula evento, List<long> seqsDivisoesCompartilhadas, int? seqCodigolocalSEFSerValidado)
        {
            if (evento.CodigoLocalSEF.HasValue && !seqCodigolocalSEFSerValidado.HasValue)
            {
                return true;
            }
            var spec = new EventoAulaFilterSpecification()
            {
                SeqsDivisaoTurma = seqsDivisoesCompartilhadas,
                Data = evento.Data,
                SeqHorarioAgd = evento.SeqHorarioAgd,
                CodigoLocalSEF = seqCodigolocalSEFSerValidado
            };
            return Count(spec) > 0;
        }

        /// <summary>
        /// Grava um evento no AGD
        /// </summary>
        /// <param name="evento">Dados do evento</param>
        /// <param name="seqAgenda">Sequencial da agenda da turma</param>
        /// <param name="seqTipoEventoAgd">Sequencial do tipo de envento do AGD</param>
        /// <param name="descricaoEvento">Descrição do evento segundo a RN_GRD_020</param>
        private void SalvarEventoAGD(EventoAula evento, long seqAgenda, long seqTipoEventoAgd, string descricaoEvento)
        {
            var eventoAgd = EventoService.SalvarEvento(new EventoData()
            {
                Seq = long.Parse(evento.SeqEventoAgd),
                CodigoLocalSEF = evento.CodigoLocalSEF,
                DataInicio = evento.Data.Add(evento.HoraInicio),
                DataFim = evento.Data.Add(evento.HoraFim),
                EventoDiaInteiro = false,
                EventoParticular = false,
                Local = evento.Local,
                Nome = descricaoEvento,
                SeqAgenda = seqAgenda,
                SeqHorario = evento.SeqHorarioAgd,
                SeqTipoEvento = seqTipoEventoAgd
            });
            AGDHelper.TratarErroAGD(eventoAgd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evento"></param>
        /// <param name="seqsDivisoesCompartilhadas"></param>
        /// <param name="codigoLocalSEFCompartilhado"></param>
        /// <param name="seqCodigolocalSEFSerValidado"></param>
        private void AtualizarLocalEventoCompartilhamento(EventoAula evento, List<long> seqsDivisoesCompartilhadas, int? seqCodigolocalSEFSerValidado, long seqAgenda, long seqTipoEventoAgd, string descricaoEvento, out int? codigoLocalSEFCompartilhado)
        {
            if (evento.CodigoLocalSEF == seqCodigolocalSEFSerValidado)
            {
                codigoLocalSEFCompartilhado = seqCodigolocalSEFSerValidado;
                return;
            }

            //var eventoValidacao = evento.SMCClone();
            var eventoValidacao = evento.Transform<EventoAula>();
            eventoValidacao.CodigoLocalSEF = seqCodigolocalSEFSerValidado;
            ValidarLocalEventoCompartilhamentoProfessor(eventoValidacao);
            ValidarLocalEventoCompartilhamentoAluno(eventoValidacao);

            var eventoRaiz = EventoService.BuscarEvento(long.Parse(evento.SeqEventoAgd))?.CodigoLocalSEF.HasValue ?? false;
            if (eventoRaiz)
            {
                codigoLocalSEFCompartilhado = seqCodigolocalSEFSerValidado;
                AtualizarLocalRaizEventoCompartilhamento(evento, seqsDivisoesCompartilhadas, evento.CodigoLocalSEF, seqAgenda, seqTipoEventoAgd, descricaoEvento);
                var eventosCompartilhados = BuscarEventosDivisoes(seqsDivisoesCompartilhadas)
                    .Where(w => w.SeqHorarioAgd == evento.SeqHorarioAgd && w.Data == evento.Data)
                    .ToList();
                if (ValidarExisteEventoRaiz(eventosCompartilhados, evento.SeqHorarioAgd, evento.Data, seqCodigolocalSEFSerValidado))
                {
                    codigoLocalSEFCompartilhado = null;
                }
            }
            else
            {
                codigoLocalSEFCompartilhado =
                    ValidarCompartilhamentoEventoLocal(evento, seqsDivisoesCompartilhadas, seqCodigolocalSEFSerValidado) ?
                    null : seqCodigolocalSEFSerValidado;
            }
        }

        private void ValidarLocalEventoCompartilhamentoAluno(EventoAula evento)
        {
            var colisaoAlunos = ValidarColisaoHorarioAluno(new List<EventoAulaVO>() { evento.Transform<EventoAulaVO>() });
            if (colisaoAlunos.SMCAny())
            {
                throw new SMCApplicationException(string.Join("<br />", colisaoAlunos));
            }
        }

        private void ValidarLocalEventoCompartilhamentoProfessor(EventoAula evento)
        {
            if (!evento.Colaboradores.SMCAny())
            {
                return;
            }
            var validacaoColaborador = evento.Colaboradores.Select(s => new EventoAulaValidacaoColisaoColaboradorVO()
            {
                SeqColaborador = s.SeqColaboradorSubstituto ?? s.SeqColaborador,
                SeqDivisaoTurma = evento.SeqDivisaoTurma,
                CodigoLocalSEF = evento.CodigoLocalSEF,
                DataAula = evento.Data,
                HoraInicio = evento.HoraInicio.ToString(@"hh\:mm"),
                HoraFim = evento.HoraFim.ToString(@"hh\:mm")
            }).ToList();
            var colisaoColaborador = ValidarColisao(validacaoColaborador);
            if (!string.IsNullOrEmpty(colisaoColaborador))
            {
                throw new SMCApplicationException(colisaoColaborador.Replace("a divisão da turma", "ao compartilhamento com a divisão da turma"));
            }
        }

        private void AtualizarLocalRaizEventoCompartilhamento(EventoAula evento, List<long> seqsDivisoesCompartilhadas, int? codigoLocalSEFOriginal, long seqAgenda, long seqTipoEventoAgd, string descricaoEvento)
        {
            var specNovaRaiz = new EventoAulaFilterSpecification()
            {
                SeqsDivisaoTurma = seqsDivisoesCompartilhadas,
                Data = evento.Data,
                SeqHorarioAgd = evento.SeqHorarioAgd,
                CodigoLocalSEF = evento.CodigoLocalSEF
            };
            var novaRaizCompartilhamento = SearchBySpecification(specNovaRaiz).FirstOrDefault();
            if (novaRaizCompartilhamento != null)
            {
                evento.CodigoLocalSEF = null;
                SalvarEventoAGD(evento, seqAgenda, seqTipoEventoAgd, descricaoEvento);
                SalvarEventoAGD(novaRaizCompartilhamento, seqAgenda, seqTipoEventoAgd, descricaoEvento);
            }
        }
    }
}