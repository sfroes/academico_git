using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class PessoaAtuacaoTermoIntercambioDomainService : AcademicoContextDomain<PessoaAtuacaoTermoIntercambio>
    {
        #region [ DomainServices ]

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();

        private AlunoHistoricoPrevisaoConclusaoDomainService AlunoHistoricoPrevisaoConclusaoDomainService => Create<AlunoHistoricoPrevisaoConclusaoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        public ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private PlanoEstudoDomainService PlanoEstudoDomainService => Create<PlanoEstudoDomainService>();

        private PeriodoIntercambioDomainService PeriodoIntercambioDomainService => Create<PeriodoIntercambioDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        private ParceriaIntercambioDomainService ParceriaIntercambioDomainService => Create<ParceriaIntercambioDomainService>();

        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService => Create<TrabalhoAcademicoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private TermoIntercambioDomainService TermoIntercambioDomainService => Create<TermoIntercambioDomainService>();

        private TipoTermoIntercambioDomainService TipoTermoIntercambioDomainService => Create<TipoTermoIntercambioDomainService>();
        private AlunoHistoricoCicloLetivoDomainService AlunoHistoricoCicloLetivoDomainService => Create<AlunoHistoricoCicloLetivoDomainService>();

        #endregion

        public SMCPagerData<PessoaAtuacaoTermoIntercambioListaVO> BuscarListaPessoaAtuacaoTermoIntercambio(PessoaAtuacaoTermoIntercambioFilterSpecification filtros)
        {
            filtros.SetOrderBy(p => (p.PessoaAtuacao.DadosPessoais.NomeSocial ?? "") + p.PessoaAtuacao.DadosPessoais.Nome);
            filtros.SetOrderBy(f => (f.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(a => a.Atual).EntidadeVinculo.Nome);

            var alunos = this.SearchProjectionBySpecification(filtros, p => new PessoaAtuacaoTermoIntercambioListaVO()
            {
                Seq = p.Seq,
                SeqPessoaAtuacao = p.SeqPessoaAtuacao,
                SeqTermoIntercambio = p.SeqTermoIntercambio,
                SeqTipoVinculo = (p.PessoaAtuacao as Aluno).TipoVinculoAluno.Seq,
                SeqEntidadeResponsavel = (p.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(a => a.Atual).EntidadeVinculo.Seq,
                SeqNivelEnsino = (p.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual).NivelEnsino.Seq,
                Nome = p.PessoaAtuacao.DadosPessoais.Nome,
                NomeSocial = p.PessoaAtuacao.DadosPessoais.NomeSocial,
                TipoAtuacao = p.PessoaAtuacao.TipoAtuacao,
                NomeEntidadeResponsavel = (p.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.Nome,
                DescricaoNivelEnsino = (p.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual).NivelEnsino.Descricao,
                TipoVinculoAlunoDescricao = (p.PessoaAtuacao as Aluno).TipoVinculoAluno.Descricao,
                DescricaoTipoTermo = p.PessoaAtuacao.TermosIntercambio.FirstOrDefault(f => f.SeqTermoIntercambio == p.SeqTermoIntercambio).TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,
                DescricaoTermoIntercambio = p.PessoaAtuacao.TermosIntercambio.FirstOrDefault(f => f.SeqTermoIntercambio == p.SeqTermoIntercambio).TermoIntercambio.Descricao,
                InstituicaoExternaNome = p.PessoaAtuacao.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome,
                TipoMobilidade = p.TipoMobilidade,
                PeriodosBanco = p.Periodos
            }, out int total).ToList();

            foreach (var aluno in alunos)
            {
                if (aluno.PeriodosBanco.Count() > 0)
                {
                    aluno.Periodos = aluno.PeriodosBanco.ToList().TransformList<PeriodoIntercambioVO>();
                }
                else
                {
                    aluno.Periodos = new List<PeriodoIntercambioVO>();
                }

                if (aluno.TipoAtuacao == TipoAtuacao.Ingressante)
                {
                    var dadosIngressante = BuscarDadosIngressante(aluno.SeqPessoaAtuacao, out bool matriculado);

                    aluno.DescricaoNivelEnsino = dadosIngressante.NivelEnsino.Descricao;
                    aluno.TipoVinculoAlunoDescricao = dadosIngressante.TipoVinculoAluno.Descricao;
                    aluno.NomeEntidadeResponsavel = dadosIngressante.EntidadeResponsavel.Nome;
                    aluno.SeqEntidadeResponsavel = dadosIngressante.EntidadeResponsavel.Seq;

                    //[ UC_ALN_004_03 ] - NV06 -> Só é possível alterar o período intercâmbio se a última situação for diferente de "Matriculado"
                    aluno.Periodos.All(x => x.PermiteEdicao = !matriculado);
                }
                else
                {
                    aluno.Periodos.All(x => x.PermiteEdicao = true);
                }

                if (!string.IsNullOrEmpty(aluno.NomeSocial))
                    aluno.Nome = $"{aluno.NomeSocial} ({aluno.Nome})";
            }

            return new SMCPagerData<PessoaAtuacaoTermoIntercambioListaVO>(alunos, total);
        }

        public PessoaAtuacaoTermoIntercambioPeriodoVO BuscarPeriodoIntercambio(long seq)
        {
            var seqPessoaAtuacaoTermoIntercambio = PeriodoIntercambioDomainService.SearchByKey(seq).SeqPessoaAtuacaoTermoIntercambio;

            var periodoAluno = this.SearchProjectionByKey(seqPessoaAtuacaoTermoIntercambio, p => new PessoaAtuacaoTermoIntercambioPeriodoVO()
            {
                Seq = p.Periodos.FirstOrDefault(f => f.Seq == seq).Seq,
                SeqPessoaAtuacao = p.SeqPessoaAtuacao,
                SeqParceriaIntercambio = p.PessoaAtuacao.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioInstituicaoExterna.SeqParceriaIntercambio,
                SeqAluno = (p.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual).SeqAluno,
                Nome = p.PessoaAtuacao.DadosPessoais.Nome,
                NomeSocial = p.PessoaAtuacao.DadosPessoais.NomeSocial,
                TipoAtuacao = p.PessoaAtuacao.TipoAtuacao,
                TipoVinculoAlunoDescricao = (p.PessoaAtuacao as Aluno).TipoVinculoAluno.Descricao,
                NomeEntidadeResponsavel = (p.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.Nome,
                DescricaoNivelEnsino = (p.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual).NivelEnsino.Descricao,
                DescricaoTermoIntercambio = p.PessoaAtuacao.TermosIntercambio.FirstOrDefault().TermoIntercambio.Descricao,
                DescricaoTipoTermo = p.PessoaAtuacao.TermosIntercambio.FirstOrDefault(f => f.SeqTermoIntercambio == p.SeqTermoIntercambio).TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,
                InstituicaoExternaNome = p.PessoaAtuacao.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome,
                TipoMobilidade = p.TipoMobilidade,
                SeqCicloLetivo = (p.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual).CicloLetivo.Seq,
                DataInicio = p.Periodos.FirstOrDefault(f => f.Seq == seq).DataInicio,
                DataFim = p.Periodos.FirstOrDefault(f => f.Seq == seq).DataFim,
                SeqCursoOfertaLocalidadeTurno = (p.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual).SeqCursoOfertaLocalidadeTurno
            });

            if (periodoAluno.TipoAtuacao == TipoAtuacao.Ingressante)
            {
                var dadosIngressante = BuscarDadosIngressante(periodoAluno.SeqPessoaAtuacao, out bool matriculado);

                periodoAluno.DescricaoNivelEnsino = dadosIngressante.NivelEnsino.Descricao;
                periodoAluno.TipoVinculoAlunoDescricao = dadosIngressante.TipoVinculoAluno.Descricao;
                periodoAluno.NomeEntidadeResponsavel = dadosIngressante.EntidadeResponsavel.Nome;
            }

            if (!string.IsNullOrEmpty(periodoAluno.NomeSocial))
                periodoAluno.Nome = $"{periodoAluno.NomeSocial} ({periodoAluno.Nome})";

            periodoAluno.SeqPessoaAtuacaoTermoIntercambio = seqPessoaAtuacaoTermoIntercambio;

            return periodoAluno;
        }

        public long SalvarPeriodoIntercambio(PeriodoIntercambioVO periodoVO)
        {
            ValidarConsistenciaAlteracao(periodoVO, out PeriodoIntercambio periodoDadosAntigos);

            var periodo = periodoVO.Transform<PeriodoIntercambio>();

            PeriodoIntercambioDomainService.SaveEntity(periodo);

            AtualizarSituacaoMatricula(periodoVO, periodoDadosAntigos);

            return periodo.Seq;
        }

        private Ingressante BuscarDadosIngressante(long seqPessoaAtuacao, out bool matriculado)
        {
            var dadosIngressante = IngressanteDomainService.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seqPessoaAtuacao), x => new
            {
                x.NivelEnsino,
                x.TipoVinculoAluno,
                x.EntidadeResponsavel,
                x.HistoricosSituacao
            });

            var situacaoMatricula = dadosIngressante.HistoricosSituacao.OrderByDescending(c => c.DataInclusao).FirstOrDefault().SituacaoIngressante;

            if (situacaoMatricula == SituacaoIngressante.Matriculado)
                matriculado = true;
            else
                matriculado = false;

            Ingressante dados = new Ingressante()
            {
                NivelEnsino = dadosIngressante.NivelEnsino,
                TipoVinculoAluno = dadosIngressante.TipoVinculoAluno,
                EntidadeResponsavel = dadosIngressante.EntidadeResponsavel
            };

            return dados;
        }

        /// <summary>
        /// Validação de consistência da alteração baseado na regra RN_SRC_083 - Solicitação - Consistência alteração data fim de intercâmbio
        /// </summary>        
        private void ValidarConsistenciaAlteracao(PeriodoIntercambioVO periodoVO, out PeriodoIntercambio periodoDadosAntigos)
        {
            var periodoIntercambio = PeriodoIntercambioDomainService.SearchByKey(periodoVO.Seq);
            long seqCursoOfertaLocalidadeTurno = (long)periodoVO.SeqCursoOfertaLocalidadeTurno;
            var alunoHistorico = AlunoHistoricoDomainService.BuscarHistoricoAtualAluno(periodoVO.SeqPessoaAtuacao, IncludesAlunoHistorico.PrevisoesConclusao | IncludesAlunoHistorico.HistoricosCicloLetivo);
            var termoIntercambio = this.SearchProjectionByKey(periodoVO.SeqPessoaAtuacaoTermoIntercambio.Value, x => new { x.TermoIntercambio }).TermoIntercambio;

            var dadosTermoIntercambio = TermoIntercambioDomainService.BuscarDadosTermoIntercambio(termoIntercambio.Seq);

            var instituicaoNivelTipoVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(periodoVO.SeqPessoaAtuacao);

            bool tipoTermoConcedeFormacao = TipoTermoIntercambioDomainService.TipoTermoIntercambioConcedeFormacao(dadosTermoIntercambio.SeqTipoTermoIntercambio, instituicaoNivelTipoVinculo.Seq);


            var cicloDataInicioNova = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoVO.DataInicio, periodoVO.DataInicio, seqCursoOfertaLocalidadeTurno, TipoAluno.Veterano).FirstOrDefault();
            var cicloDataFimNova = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoVO.DataFim, periodoVO.DataFim, seqCursoOfertaLocalidadeTurno, TipoAluno.Veterano).FirstOrDefault();

            var cicloDataInicioAntiga = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoIntercambio.DataInicio, periodoIntercambio.DataInicio, seqCursoOfertaLocalidadeTurno, TipoAluno.Veterano).FirstOrDefault();
            var cicloDataFimAntiga = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoIntercambio.DataFim, periodoIntercambio.DataFim, seqCursoOfertaLocalidadeTurno, TipoAluno.Veterano).FirstOrDefault();

            #region [ Item 1 ]
            // Período de intercambio deverá estar dentro da data de admissão e a data de previsão

            // Somente se o vínculo ou o tipo de termo do termo associado a pessoa-atuação estiver parametrizado
            // por Instituição-Nível de Ensino-tipo de vinculo para conceder formação de acordo com a Instituição de
            // Ensino o nível de ensino e o vínculo do aluno.
            if ((bool)instituicaoNivelTipoVinculo.ConcedeFormacao || tipoTermoConcedeFormacao)
            {
                var dataPrevisaoConclusao = alunoHistorico.PrevisoesConclusao;

                // [ Item 1.1 ]
                // Verificar se a nova data início de intercâmbio é menor que a data de admissão do aluno histórico
                if (periodoVO.DataInicio.Date < alunoHistorico.DataAdmissao.Date)
                {
                    // Caso seja, abortar a operação e exibir a seguinte mensagem de erro:
                    // "Alteração não permitida. A nova data início não pode ser menor que a data de admissão do aluno."
                    throw new PeriodoIntercambioDataInicioInvalidaException();
                }

                // [ Item 1.2 ]
                // Verificar se a nova data fim de intercâmbio é maior que a data de previsão de conclusão mais atual do aluno histórico
                var ultimaDataPrevisao = dataPrevisaoConclusao.OrderByDescending(c => c.DataPrevisaoConclusao).FirstOrDefault().DataPrevisaoConclusao;
                if (dataPrevisaoConclusao != null && periodoVO.DataFim.Date > ultimaDataPrevisao.Date)
                {
                    // Caso seja, abortar operação e exibir a mensagem de erro:
                    // "Alteração não permitida. A nova data fim não pode ser maior que a data de previsão de conclusão do aluno."
                    throw new PeriodoIntercambioDataFimMenorConclusaoInvalidaException();
                }
            }
            #endregion [ Item 1 ]

            #region [ Item 2 ]
            // Data início de intercambio deverá estar dentro do ciclo letivo de ingresso

            // Somente se o vínculo e o tipo de termo do termo associado a pessoa-atuação estiver parametrizado
            // por Instituição-Nível de Ensino-tipo de vinculo para NÃO conceder formação de acordo com a
            // Instituição de Ensino o nível de ensino e o vínculo do aluno
            if (!(bool)instituicaoNivelTipoVinculo.ConcedeFormacao && !tipoTermoConcedeFormacao)
            {
                // Verificar se a data início informada está dentro do ciclo letivo correpondente a data início antiga
                if (cicloDataInicioAntiga?.SeqCicloLetivo != cicloDataInicioNova?.SeqCicloLetivo)
                {
                    // Caso não esteja, abortar a operação e enviar a seguinte mensagem de erro:
                    var AnoNumerocicloLetivoDataInicio = CicloLetivoDomainService.BuscarDescricaoFormatadaCicloLetivo(cicloDataInicioAntiga.SeqCicloLetivo);

                    // "Alteração não permitida. A nova da início não pode pertencer a um ciclo letivo diferente do ciclo letivo de ingresso: <ciclo letivo de ingresso>."
                    throw new PeriodoIntercambioDataInicioNaoPodeSerDiferenteDeCicloLetivoIngresso(AnoNumerocicloLetivoDataInicio);
                }
            }
            #endregion [ Item 2 ]

            #region [ Item 3 ]
            // Período de intercâmbio deverá estar dentro do período da parceria de intercâmbio

            var vigenciaParceriaIntercambio = ParceriaIntercambioDomainService.SearchProjectionByKey(new SMCSeqSpecification<ParceriaIntercambio>(periodoVO.SeqParceriaIntercambio), x => new
            {
                x.Vigencias
            });

            var dataInicioVigencia = vigenciaParceriaIntercambio.Vigencias.FirstOrDefault().DataInicio;
            var dataFimVigencia = vigenciaParceriaIntercambio.Vigencias.FirstOrDefault().DataFim;

            // A nova data início de intercâmbio deverá ser maior ou igual a data início da parceria de intercâmbio do
            // termo E a nova data fim de intercâmbio deverá ser memor ou igual a data fim da parceria de
            // intercâmbio do termo.
            if (periodoVO.DataInicio.Date < dataInicioVigencia || periodoVO.DataFim.Date > dataFimVigencia)
            {
                // Em caso de violação, abortar operação e exibir a m ensagem de erro:
                // "Alteração não permitida. A nova data deverá estar entre a data inicio e a data fim da parceria de intercâmbio"
                throw new PeriodoIntercambioNovaDataInvalidaParceriaIntercambioException();
            }

            #endregion [ Item 3 ]

            #region [ Item 4 ]
            // O periodo de intercambio não pode conter partes de um outro periodo de intercambio para o mesmo termo

            var specPeriodos = new PessoaAtuacaoTermoIntercambioFilterSpecification() { SeqPessoaAtuacao = periodoVO.SeqPessoaAtuacao };
            var periodosAluno = this.SearchProjectionBySpecification(specPeriodos, p => p.Periodos).FirstOrDefault().Where(c => c.Seq != periodoVO.Seq).ToList();

            // Se existir mais periodo de intercâmbio para o mesmo termo de intercâmbio associado a pessoa-atuação, verificar se pelo menos um período de datas coincide
            if (periodosAluno.Count() > 0)
            {
                foreach (var periodo in periodosAluno)
                {
                    bool datasCoincidem = periodoVO.DataInicio <= periodo.DataFim && periodo.DataInicio <= periodoVO.DataFim;

                    if (datasCoincidem)
                    {
                        // Caso ocorra, abortar a operação e exibir a seguinte mensagem de erro:
                        // "Alteração nao permitida. Para esse termo de intercâmbio associado ao aluno, existe outro período de intercambio que coincide com o periodo informado."
                        throw new PeriodoIntercambioDataCoincideException();
                    }
                }
            }

            #endregion [ Item 4 ]

            #region [ Item 5 ]
            // O aluno não pode conter pelo menos uma das situações de matrícula abaixo durante o período de intercambio

            List<string> listaSituacoesFixas = new List<string>(new string[] { "CANCELADO", "FORMADO", "TRANCADO" });

            // [ Item 5.1 ]
            // Se existir ciclo letivo referente à nova data início e existir ciclo letivo referente a nova data fim
            if (cicloDataInicioNova != null && cicloDataFimNova != null)
            {
                // Entre o ciclo letivo referente à nova data início de intercâmbio e o ciclo letivo referente a nova data fim
                // de intercâmbio, verificar se existe pelo menos uma situação de matrícula com o tipo de situação igual a
                // "CANCELADO"/"TRANCADO"/ "FORMADO" com a data início entre a nova data início de intercâmbio e a nova data fim de intercâmbio


                var situacoesEntreCiclos = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoEntreCiclosLetivos(periodoVO.SeqAluno, cicloDataInicioNova.SeqCicloLetivo, cicloDataFimNova.SeqCicloLetivo);

                // Recupera todas as situações do aluno com a data início
                //var situacoesCicloLetivoDataInicio = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoNoCicloLetivo(periodoVO.SeqAluno, cicloDataInicioNova.SeqCicloLetivo);
                //// Recupera todas as situações do aluno com a data fim
                //var situacoesCicloLetivoDataFim = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoNoCicloLetivo(periodoVO.SeqAluno, cicloDataFimNova.SeqCicloLetivo);

                //var listaSituacoes = situacoesCicloLetivoDataFim.Union(situacoesCicloLetivoDataInicio).ToList();

                bool situacaoMatriculaIrregular = situacoesEntreCiclos.Any(x => listaSituacoesFixas.Contains(x.TokenTipoSituacaoMatricula.ToUpper()));

                if (situacaoMatriculaIrregular)
                {
                    // Caso exista, abortar a operação e exibir a seguinte mensagem de erro:
                    // "Alteração não permitida. Entre o período de intercâmbio informado, o(a) aluno(a) possui alguma situação de matrícula que não permite a mobilidade."
                    throw new PeriodoIntercambioPessoaAtuacaoSItuacaoMatriculaException();
                }
            }

            // [ Item 5.2 ]
            // Se existir ciclo letivo referente a nova data início e NÃO existir ciclo letivo referente a nova data fim
            if (cicloDataInicioNova != null && cicloDataFimNova == null)
            {
                // Nos ciclos letivos existentes a partir do ciclo letivo referente a nova data início, verificar se existe pelo
                // menos uma situação de matrícula com o tipo de situação igual a
                // "CANCELADO"/"TRANCADO"/"FORMADO" com a data início entre a nova data início de intercâmbio e a nova data fim de intercâmbio

                // Recupera todas as situações do aluno com a data início
                var situacoesEntreCiclos = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoEntreCiclosLetivos(periodoVO.SeqAluno, cicloDataInicioNova.SeqCicloLetivo, cicloDataFimNova.SeqCicloLetivo);

                bool situacaoMatriculairregular = situacoesEntreCiclos.Any(x => listaSituacoesFixas.Contains(x.TokenTipoSituacaoMatricula.ToUpper()));

                if (situacaoMatriculairregular)
                {
                    // Caso exista, abortar a operação e exibir a seguinte mensagem de erro:
                    // "Alteração não permitida. Entre o período de intercâmbio informado, o(a) aluno(a) possui alguma situação de matrícula que não permite a mobilidade."

                    throw new PeriodoIntercambioPessoaAtuacaoSItuacaoMatriculaException();
                }
            }

            // [ Item 5.3 ]
            // Se NÃO existir ciclo letivo referente a nova data início, fazer nada.
            #endregion [ Item 5 ]

            #region [ Item 6 ]
            // O aluno não poderá ser matriculado, pois o processo de renovação foi encerrado

            // Somente se o vínculo e o tipo de termo do termo associado a pessoa-atuação estiver parametrizado
            // por Instituição-Nível de Ensino-tipo de vinculo para NÃO conceder formação de acordo com a
            // Instituição de Ensino o nível de ensino e o vínculo do aluno.
            if (periodoIntercambio.DataFim.Date != periodoVO.DataFim.Date)
            {
                if (!(bool)instituicaoNivelTipoVinculo.ConcedeFormacao && !tipoTermoConcedeFormacao)
                {
                    if (cicloDataFimAntiga != null)
                    {

                        //Se o ciclo letivo da antiga data fim for diferente da nova data fim E SOMENTE se NÃO existir pelo menos uma situação de matrícula
                        //diferente de "APTO_MATRICULA" no ciclo letivo da nova data fim.
                        if (cicloDataFimNova != null)
                        {
                            var alunoHistoricoDiferenteApto = RecuperarListaHistoricoSituacao(periodoVO.SeqPessoaAtuacao, cicloDataFimNova.SeqCicloLetivo)
                                                                 .Where(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA)
                                                                 .ToList();

                            if (cicloDataFimAntiga.SeqCicloLetivo != cicloDataFimNova.SeqCicloLetivo && !alunoHistoricoDiferenteApto.Any())
                            {
                                var spec = new ProcessoFilterSpecification()
                                {
                                    SeqCicloLetivo = cicloDataFimNova.SeqCicloLetivo,
                                    TokenServico = TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU,
                                    SeqsEntidadesResponsaveis = new long[] { alunoHistorico.SeqEntidadeVinculo }
                                };

                                //Verificar se no ciclo letivo da nova data fim existe um processo do serviço "SOLICITAÇAO_RENOVACAO_MATRICULA_STRICTO_SENSU",
                                //para a entidade da pessoa - atuação em questão, com data de encerramento
                                var datasProcesso = ProcessoDomainService.SearchProjectionBySpecification(spec, p => p.DataEncerramento).ToList();

                                bool existeData = datasProcesso.Any(c => c != null);

                                if (existeData)
                                {
                                    // Caso exista, abortar a operação e exibir a seguinte mensagem de erro:
                                    // "Alteração não permitida. Não é possivel matricular o aluno na data fim informada, pois o processo de renovação de matrícula foi encerrado."
                                    throw new PeriodoIntercambioProcessoRenovacaoEncerradoException();
                                }
                            }
                        }
                    }
                }
            }

            #endregion [ Item 6 ]

            periodoDadosAntigos = periodoIntercambio;
        }

        /// <summary>
        /// Validação da regra RN_ALN_063 da documentação
        /// </summary>
        private void AtualizarSituacaoMatricula(PeriodoIntercambioVO periodoVO, PeriodoIntercambio periodoDadosAntigos)
        {
            var seqCursoOfertaLocalidadeTurno = (long)periodoVO.SeqCursoOfertaLocalidadeTurno;
            var instituicaoNivelTipoVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(periodoVO.SeqPessoaAtuacao);

            var dadosMatriculaAluno = AlunoDomainService.BuscarDadosMatriculaAluno(periodoVO.SeqAluno);
            var dadosOrigemAluno = PessoaAtuacaoDomainService.RecuperaDadosOrigem(periodoVO.SeqPessoaAtuacao);
            var tipoAluno = dadosOrigemAluno.TipoAtuacao == TipoAtuacao.Ingressante ? TipoAluno.Calouro : TipoAluno.Veterano;
            var dataDeposito = TrabalhoAcademicoDomainService.BuscarDatasDepositoDefesaTrabalho(periodoVO.SeqPessoaAtuacao).DataDeposito;
            var alunoHistorico = AlunoHistoricoDomainService.BuscarHistoricoAtualAluno(periodoVO.SeqPessoaAtuacao, IncludesAlunoHistorico.PrevisoesConclusao);

            var cicloLetivoDataInicioNova = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoVO.DataInicio, periodoVO.DataInicio, seqCursoOfertaLocalidadeTurno, TipoAluno.Veterano).FirstOrDefault();
            var cicloLetivoDataFimNova = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoVO.DataFim, periodoVO.DataFim, seqCursoOfertaLocalidadeTurno, TipoAluno.Veterano).FirstOrDefault();

            var cicloLetivoDataInicioAntiga = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoDadosAntigos.DataInicio, periodoDadosAntigos.DataInicio, seqCursoOfertaLocalidadeTurno, TipoAluno.Veterano).FirstOrDefault();
            var cicloLetivoDataFimAntiga = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoDadosAntigos.DataFim, periodoDadosAntigos.DataFim, seqCursoOfertaLocalidadeTurno, TipoAluno.Veterano).FirstOrDefault();

            var termoIntercambio = this.SearchProjectionByKey(periodoVO.SeqPessoaAtuacaoTermoIntercambio.Value, x => new { x.TermoIntercambio }).TermoIntercambio;
            var dadosTermoIntercambio = TermoIntercambioDomainService.BuscarDadosTermoIntercambio(termoIntercambio.Seq);
            bool tipoTermoConcedeFormacao = TipoTermoIntercambioDomainService.TipoTermoIntercambioConcedeFormacao(dadosTermoIntercambio.SeqTipoTermoIntercambio, instituicaoNivelTipoVinculo.Seq);

            #region [ Concede formação ]
            if ((bool)instituicaoNivelTipoVinculo.ConcedeFormacao || tipoTermoConcedeFormacao)
            {
                #region [ Item 1 ]
                // Se data início antecipada
                if (periodoVO.DataInicio.Date < periodoDadosAntigos.DataInicio.Date)
                {
                    long seqHistoricoSituacaoInserido = 0;

                    // [ Item 1.1.1 ]
                    // Se o ciclo letivo correspondente a nova data início não for encontrado 
                    // não Retornar a mensagem de erro de ciclo letivo nao existente e prosseguir (1.2, 1.3 e 1.4).

                    // [ Item 1.1.2 ]
                    if (cicloLetivoDataInicioNova != null)
                    {
                        seqHistoricoSituacaoInserido = IncluirNovaSituacaoMatriculadoMobilidade(dadosMatriculaAluno.SeqAluno, cicloLetivoDataInicioNova.SeqCicloLetivo, periodoDadosAntigos.Seq, periodoVO.DataInicio, periodoVO);
                    }

                    // [ Item 1.2 ]
                    // Excluir a situação “MATRICULADO_MOBILIDADE” correspondente a data início antiga.
                    // Desconsiderar situação com data de exclusão. <Data de exclusão validado ao incluir o Histórico>
                    ExcluirSituacaoMatriculadoMobilidadeDataInicio(dadosMatriculaAluno.SeqAluno, cicloLetivoDataInicioAntiga.SeqCicloLetivo, periodoDadosAntigos.DataInicio);

                    // [ Item 1.2.1 ]
                    //Se existir, excluir as demais situações correspondentes a data início antiga, que sejam
                    //diferentes de “APTO_MATRICULA”, desconsiderar situação com data de exclusão.
                    ExcluirSituacaoDiferenteAptoMatriculaDataInicio(dadosMatriculaAluno.SeqAluno, cicloLetivoDataInicioAntiga.SeqCicloLetivo, periodoDadosAntigos.DataInicio);

                    // [ Item 1.3 ]
                    // Excluir todas as situações que estão entre a data início nova e a data início antiga, exceto a
                    // situação “APTO_MATRICULA” e as demais situações com data de exclusão.
                    ExcluirSituacoesEntreDatasInicio(periodoVO, periodoDadosAntigos, seqHistoricoSituacaoInserido);

                    // [ Item 1.4 ]
                    // Verificar se os ciclos letivos referentes a nova data inicio e data inicio antiga existem.

                    // [ Item 1.4.1 ] 
                    // Se o ciclo letivo referente a nova data início e o ciclo letivo referente a data início antiga não existirem, 
                    // não retornar a mensagem de erro de ciclo letivo não existente e não fazer nada.

                    // [ Item 1.4.2 ]
                    // Se pelo menos um ciclo letivo existir, verificar se a data início nova e a data início antiga estão em ciclos letivos diferentes
                    if (cicloLetivoDataInicioNova != null || cicloLetivoDataInicioAntiga != null)
                    {
                        if (cicloLetivoDataInicioNova?.SeqCicloLetivo != cicloLetivoDataInicioAntiga?.SeqCicloLetivo)
                        {
                            // [ Item 1.4.2.1 ]
                            // Se o ciclo letivo referente a data início antiga não existir
                            if (cicloLetivoDataInicioAntiga == null)
                            {
                                // Não retornar a mensagem de erro de ciclo letivo não existente e realizar o item I em todos os ciclos letivos existentes maiores que o ciclo
                                // letivo da data início nova SOMENTE se existir pelo menos uma situação de matrícula diferente de "APTO_MATRICULA".
                                var ciclosMaioresQueDataInicioAntiga = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoVO.DataInicio, null, seqCursoOfertaLocalidadeTurno, tipoAluno)
                                                                                                            .Where(c => c.DataInicio > periodoVO.DataInicio).ToList();

                                foreach (var cicloLetivo in ciclosMaioresQueDataInicioAntiga)
                                {
                                    var listaHistoricoSituacao = RecuperarListaHistoricoSituacao(dadosMatriculaAluno.SeqAluno, cicloLetivo.SeqCicloLetivo)
                                                                 .Where(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA)
                                                                 .ToList();

                                    if (listaHistoricoSituacao.Count > 0)
                                    {
                                        // Item I
                                        IncluirSituacaoMatriculadoMobilidade(dadosMatriculaAluno.SeqAluno, cicloLetivo.SeqCicloLetivo, periodoDadosAntigos.Seq, cicloLetivo.DataInicio);
                                    }
                                }
                            }

                            // [ Item 1.4.2.2 ]
                            // Se o ciclo letivo referente a antiga data início existir
                            if (cicloLetivoDataInicioAntiga != null)
                            {
                                // Em todos os ciclos letivos maiores que o ciclo letivo da data início nova, até o ciclo letivo da data início antiga (inclusive ele), 
                                // e SOMENTE se existir pelo menos uma situação de matrícula, diferente de "APTO_MATRICULA", realizar o item I.

                                var listaCiclosEntreDatas = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoVO.DataInicio, periodoDadosAntigos.DataInicio, seqCursoOfertaLocalidadeTurno, tipoAluno);

                                if (cicloLetivoDataInicioNova != null)
                                {
                                    listaCiclosEntreDatas = listaCiclosEntreDatas.Where(c => c.SeqCicloLetivo != cicloLetivoDataInicioNova.SeqCicloLetivo).ToList();
                                }

                                foreach (var cicloLetivo in listaCiclosEntreDatas)
                                {
                                    var listaHistoricoSituacao = RecuperarListaHistoricoSituacao(dadosMatriculaAluno.SeqAluno, cicloLetivo.SeqCicloLetivo)
                                                                 .Where(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA)
                                                                 .ToList();

                                    if (listaHistoricoSituacao.Count > 0)
                                    {
                                        // Item I
                                        IncluirSituacaoMatriculadoMobilidade(dadosMatriculaAluno.SeqAluno, cicloLetivo.SeqCicloLetivo, periodoDadosAntigos.Seq, cicloLetivo.DataInicio);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region [ Item 2 ]
                // Se data de início for prorrogada
                else if (periodoVO.DataInicio.Date > periodoDadosAntigos.DataInicio.Date)
                {
                    long seqHistoricoSituacaoInserido = 0;

                    // [ Item 2.1 ]
                    // Se o ciclo letivo correspondente a nova data início informada 
                    // [Item 2.1.1] 
                    // Não for encontrado - não retornar a mensagem de erro de ciclo letivo nao existente e prosseguir (2.2., 2.3.e 2.4)

                    // [ Item 2.1.2 ]
                    if (cicloLetivoDataInicioNova != null)
                    {
                        seqHistoricoSituacaoInserido = IncluirNovaSituacaoMatriculadoMobilidade(dadosMatriculaAluno.SeqAluno, cicloLetivoDataInicioNova.SeqCicloLetivo, periodoDadosAntigos.Seq, periodoVO.DataInicio, periodoVO);
                    }

                    // [ Item 2.2 ]
                    // Excluir a situação “MATRICULADO_MOBILIDADE” correspondente a data início antiga.
                    ExcluirSituacaoMatriculadoMobilidadeDataInicio(dadosMatriculaAluno.SeqAluno, cicloLetivoDataInicioAntiga.SeqCicloLetivo, periodoDadosAntigos.DataInicio);

                    // [ Item 2.3 ]
                    // Excluir todas as situações que estão entre a data início nova e a data início antiga, exceto
                    // a situação “APTO_MATRICULA” e as demais situações com data de exclusão.
                    //incluindo 1 minuto na data inicio que chega de parametro ao salvar o intercambio
                    ExcluirSituacoesEntreDatasInicio(periodoVO, periodoDadosAntigos, seqHistoricoSituacaoInserido, false);

                    // [ Item 2.4 ]
                    // Verificar se os ciclos letivos referente a nova data início e a data início antiga existem
                    // [ Item 2.4.1 ]
                    // Se o ciclo letivo referente a data início antiga não existir, não retornar a mensagem de erro de ciclo letivo nao existente e não fazer nada.

                    // [ Item 2.4.2 ]
                    // Caso contrário, se pelo menos um ciclo letivo existir, verificar se a data início nova e a data início antiga estão em ciclos letivos diferentes.
                    if (cicloLetivoDataInicioNova != null || cicloLetivoDataInicioAntiga != null)
                    {
                        if (cicloLetivoDataInicioNova?.SeqCicloLetivo != cicloLetivoDataInicioAntiga?.SeqCicloLetivo)
                        {
                            // [ Item 2.4.2.1 ]
                            // Se o ciclo letivo referente a nova data início não existir
                            if (cicloLetivoDataInicioNova == null)
                            {
                                // Realizar os itens I e II no ciclos letivos existentes a partir do ciclo letivo referente a data início antiga.
                                var ciclosMaioresQueDataInicioAntiga = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(cicloLetivoDataInicioAntiga.DataInicio, null, seqCursoOfertaLocalidadeTurno, tipoAluno);

                                foreach (var cicloLetivo in ciclosMaioresQueDataInicioAntiga)
                                {
                                    //Item I e II
                                    IncluirMatriculadoProvavelFormando(cicloLetivo, dataDeposito, dadosMatriculaAluno.SeqAluno, periodoVO.Seq);
                                }
                            }

                            // [ Item 2.4.2.2 ]
                            // Se o ciclo letivo referente a nova data início existir
                            if (cicloLetivoDataInicioNova != null)
                            {
                                // Em todos os ciclos letivos maiores que o ciclo letivo da data início antiga, até o ciclo letivo da data início nova (inclusive ele)
                                var listaCiclosEntreDatasInicio = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(cicloLetivoDataInicioAntiga.DataInicio, cicloLetivoDataInicioNova.DataInicio, seqCursoOfertaLocalidadeTurno, tipoAluno)
                                                                                                       .Where(c => c.SeqCicloLetivo != cicloLetivoDataInicioAntiga.SeqCicloLetivo)
                                                                                                       .ToList();

                                // realizar os itens I e II
                                foreach (var cicloLetivo in listaCiclosEntreDatasInicio)
                                {
                                    IncluirMatriculadoProvavelFormando(cicloLetivo, dataDeposito, dadosMatriculaAluno.SeqAluno, periodoVO.Seq);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region [ Item 3 ]
                // Se a data fim for antecipada
                if (periodoVO.DataFim.Date < periodoDadosAntigos.DataFim.Date)
                {
                    long seqSituacaoIncluida = 0;
                    // [ Item 3.1 ]
                    // Se o ciclo letivo correspondente a nova data fim informada
                    // [ Item 3.1.1 ]
                    // Não for encontrado, não retornar a mensagem de erro de ciclo letivo nao existente e prosseguir (3.2, 3.3, 3.4 e 3.5)

                    // [ Item 3.1.2 ]
                    // For encontrado
                    if (cicloLetivoDataFimNova != null)
                    {
                        seqSituacaoIncluida = IncluirNovaSituacaoMatriculadoProvavelFormandoDataFim(dadosMatriculaAluno.SeqAluno, cicloLetivoDataFimNova.SeqCicloLetivo, dataDeposito, cicloLetivoDataFimAntiga.DataFim, periodoVO.DataFim);
                    }

                    // [ Item 3.2 ]
                    ExcluirSituacaoMatriculadoDataFim(dadosMatriculaAluno.SeqAluno, cicloLetivoDataFimAntiga.SeqCicloLetivo, periodoDadosAntigos.DataFim);

                    //[ Item 3.3 ]
                    ExcluirTodasSituacoesEntreDatasFim(periodoVO, periodoDadosAntigos, seqSituacaoIncluida);

                    // [ Item 3.4 ]
                    // Verificar se os ciclos letivos referente a nova data fim e a data fim antiga existem

                    // [ Item 3.4.1 ]
                    // Se o ciclo letivo refente a nova data fim e o ciclo letivo referente a data fim antiga não existirem, não retornar a mensagem de erro de ciclo letivo nao existente e não fazer nada.

                    // [ Item 3.4.2 ] 
                    // Caso contrário, se pelo menos um ciclo letivo existir, verificar se a nova data fim e a data fim antiga estão em ciclos diferentes.
                    if (cicloLetivoDataFimNova != null || cicloLetivoDataFimAntiga != null)
                    {
                        if (cicloLetivoDataFimNova?.SeqCicloLetivo != cicloLetivoDataFimAntiga?.SeqCicloLetivo)
                        {
                            // [ Item 3.4.2.1 ]
                            // Se o ciclo letivo referente a data fim antiga não existir
                            if (cicloLetivoDataFimAntiga == null)
                            {
                                // Realizar os itens I e II no ciclos letivos existentes a partir do ciclo letivo referente a nova data início.
                                var ciclosMaioresQueDataInicioNova = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(cicloLetivoDataInicioNova.DataInicio, null, seqCursoOfertaLocalidadeTurno, tipoAluno);

                                foreach (var cicloLetivo in ciclosMaioresQueDataInicioNova)
                                {
                                    //Item I e II
                                    IncluirMatriculadoProvavelFormando(cicloLetivo, dataDeposito, dadosMatriculaAluno.SeqAluno, periodoVO.Seq);
                                }
                            }

                            // [ Item 3.4.2.2 ]
                            // Se o ciclo letivo referente a data fim antiga existir
                            if (cicloLetivoDataFimAntiga != null)
                            {
                                // Em todos os ciclos letivos maiores que o ciclo letivo da nova data fim até o ciclo letivo da data fim antiga (inclusive ele), realizar os itens I e II.
                                var listaCiclosEntreDatasFim = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(cicloLetivoDataFimNova.DataInicio, cicloLetivoDataFimAntiga.DataInicio, seqCursoOfertaLocalidadeTurno, tipoAluno)
                                    .Where(x => x.SeqCicloLetivo != cicloLetivoDataFimNova.SeqCicloLetivo);



                                // realizar os itens I e II
                                foreach (var cicloLetivo in listaCiclosEntreDatasFim)
                                {
                                    var listaHistoricoSituacao = RecuperarListaHistoricoSituacao(dadosMatriculaAluno.SeqAluno, cicloLetivo.SeqCicloLetivo)
                                                                 .Where(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA)
                                                                 .ToList();


                                    if (listaHistoricoSituacao != null && listaHistoricoSituacao.Count > 0)
                                    {
                                        IncluirMatriculadoProvavelFormando(cicloLetivo, dataDeposito, dadosMatriculaAluno.SeqAluno, periodoVO.Seq);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region [ Item 4 ]
                // Se a data fim for prorrogada
                if (periodoVO.DataFim.Date > periodoDadosAntigos.DataFim.Date)
                {
                    long seqSituacaoIncluida = 0;

                    // [ Itens 4.1 e 4.1.1 ]
                    // Se o ciclo letivo correspondente a nova data fim informada
                    // Não for encontrado, não retornar a mensagem de erro de ciclo letivo nao existente e prosseguir (4.2, 4.3 e 4.4).

                    // [ Item 4.1.2 ]
                    // For encontrado
                    if (cicloLetivoDataFimNova != null)
                    {
                        var situacoesMatricula = RecuperarListaHistoricoSituacao(dadosMatriculaAluno.SeqAluno, cicloLetivoDataFimNova.SeqCicloLetivo);

                        // SOMENTE se existir pelo menos uma situação de matrícula, diferente de "APTO_MATRICULA" no ciclo letivo referente a nova data fim
                        var existeSituacao = situacoesMatricula.Any(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA);

                        if (existeSituacao)
                        {
                            //Verificar se a data fim informada é menor que a data fim do ciclo letivo correspondente.
                            if (periodoVO.DataFim.Date <= cicloLetivoDataFimNova.DataFim.Date)
                            {
                                var primeiroRegistroMatriculado = situacoesMatricula.Where(c => c.DataExclusao == null &&
                                                                                           c.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.MATRICULADO)
                                                                                    .FirstOrDefault();

                                // [ Item 4.1.2.1 ]
                                // Se for menor, verificar se a data informada é menor que a data do primeiro registro 
                                // do tipo de situação “MATRICULADO”. Desconsiderar situação com data de exclusão.                                
                                // [ Item 4.1.2.1.1 ] 
                                //Se for, não fazer nada

                                // [ Item 4.1.2.1.2 ]
                                // Se for maior ou igual
                                if (periodoVO.DataFim.Date >= primeiroRegistroMatriculado?.DataInicioSituacao.Date)
                                {
                                    var dataPrevisaoConclusaoCurso = alunoHistorico.PrevisoesConclusao.OrderByDescending(c => c.DataPrevisaoConclusao)
                                                                                                      .FirstOrDefault();
                                    // [ Item 4.1.2.1.2.1 ]
                                    // Se a data fim informada for menor que a data de previsão de conclusão verificar se o aluno possui data de depósito
                                    if (periodoVO.DataFim.Date <= dataPrevisaoConclusaoCurso?.DataPrevisaoConclusao.Date)
                                    {
                                        IncluirAlunoHistoricoSituacaoVO novaSituacao = new IncluirAlunoHistoricoSituacaoVO()
                                        {
                                            SeqAluno = alunoHistorico.SeqAluno,
                                            SeqCicloLetivo = cicloLetivoDataFimNova.SeqCicloLetivo,
                                            SeqPeriodoIntercambio = periodoVO.Seq
                                        };

                                        if (dataDeposito != null)
                                        {
                                            // [ Item 4.1.2.1.2.1.1 ]
                                            // Se possuir, verificar se a data fim informada na tela +1 dia é maior ou igual a data de depósito.
                                            if (periodoVO.DataFim.Date.AddDays(1) >= dataDeposito.Value.Date)
                                            {
                                                // [ Item 4.1.2.1.2.1.1.1 ] 
                                                // Se for, incluir a situação “PROVAVEL_FORMANDO”, com a data inicio igual a data fim informada na tela + 1 dia.
                                                novaSituacao.TokenSituacao = TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO;
                                                novaSituacao.DataInicioSituacao = periodoVO.DataFim.AddDays(1);
                                            }
                                            // [ Item 4.1.2.1.2.1.1.2 ]
                                            // Caso contrário incluir a situação “MATRICULADO”, com a data inicio igual a data fim informada na tela + 1 dia.
                                            else
                                            {
                                                novaSituacao.TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO;
                                                novaSituacao.DataInicioSituacao = periodoVO.DataFim.AddDays(1);
                                            }
                                        }
                                        // [ Item 4.1.2.1.2.1.2 ]
                                        // Se não possuir data de deposito
                                        else
                                        {
                                            // Incluir a situação de matrícula “MATRICULADO”, com a data início igual a data fim informada na tela + 1 dia.
                                            novaSituacao.TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO;
                                            novaSituacao.DataInicioSituacao = periodoVO.DataFim.AddDays(1);
                                        }
                                        //Inclusão de histórico situação
                                        seqSituacaoIncluida = AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacaoRetornaSeq(novaSituacao);
                                    }
                                    // [ Item 4.1.2.1.2.2 ] 
                                    // Caso contrário, não fazer nada.
                                }
                            }
                        }
                    }

                    // [ Item 4.2 ]
                    ExcluirSituacaoMatriculadoDataFim(dadosMatriculaAluno.SeqAluno, cicloLetivoDataFimAntiga.SeqCicloLetivo, periodoDadosAntigos.DataFim);

                    // [ Item 4.3 ]
                    ExcluirSituacoesEntreDatas(periodoDadosAntigos.DataFim, periodoVO.DataFim, dadosMatriculaAluno.SeqAluno, seqCursoOfertaLocalidadeTurno, tipoAluno, seqSituacaoIncluida);

                    // [ Item 4.4 ]
                    // Verificar se os ciclos letivos referente a nova data fim e a data fim antiga existem
                    // [ Item 4.4.1 ]
                    // Se o ciclo letivo refente a nova data fim e o ciclo letivo referente a data fim antiga não existirem, não retornar a mensagem de erro de ciclo letivo nao existente e não fazer nada.

                    // [ Item 4.4.2 ]
                    // Caso contrário, se pelo menos um ciclo letivo existir, verificar se a nova data fim e a data fim antiga estão em ciclos letivos* diferentes.
                    if (cicloLetivoDataFimNova != null || cicloLetivoDataFimAntiga != null)
                    {
                        if (cicloLetivoDataFimNova?.SeqCicloLetivo != cicloLetivoDataFimAntiga?.SeqCicloLetivo)
                        {
                            // [ Item 4.4.2.1 ] 
                            // Se o ciclo letivo referente a nova data fim não existir
                            if (cicloLetivoDataFimNova == null)
                            {
                                // Não retornar a mensagem de erro de ciclo letivo não existente e realizar o item I nos ciclos letivos existentes 
                                // a partir do ciclo letivo referente a data fim antiga.
                                var ciclosMaioresQueDataFimAntiga = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoDadosAntigos.DataFim, periodoVO.DataFim, seqCursoOfertaLocalidadeTurno, tipoAluno)
                                                                                                              .Where(c => c.DataInicio > periodoVO.DataInicio).ToList();

                                foreach (var cicloLetivo in ciclosMaioresQueDataFimAntiga)
                                {
                                    var listaHistoricoSituacao = RecuperarListaHistoricoSituacao(dadosMatriculaAluno.SeqAluno, cicloLetivo.SeqCicloLetivo)
                                                                 .Where(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA)
                                                                 .ToList();

                                    InserirSituacaoInicioCicloLetivo(cicloLetivo.SeqCicloLetivo, alunoHistorico.SeqAluno, periodoDadosAntigos.Seq, cicloLetivo.DataInicio);
                                }
                            }

                            // [ Item 4.4.2.2 ]
                            // Se o ciclo letivo referente a nova data fim existir
                            if (cicloLetivoDataFimNova != null)
                            {

                                var ciclosMaioresQueDataFimAntiga = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoDadosAntigos.DataFim, periodoVO.DataFim, seqCursoOfertaLocalidadeTurno, tipoAluno)
                                                                                                            .Where(c => c.DataInicio >= periodoVO.DataInicio).ToList();




                                if (cicloLetivoDataInicioNova != null)
                                {
                                    ciclosMaioresQueDataFimAntiga = ciclosMaioresQueDataFimAntiga.Where(c => c.SeqCicloLetivo != cicloLetivoDataFimAntiga.SeqCicloLetivo).ToList();
                                }


                                foreach (var cicloLetivo in ciclosMaioresQueDataFimAntiga)
                                {

                                    var listaHistoricoSituacao = RecuperarListaHistoricoSituacao(dadosMatriculaAluno.SeqAluno, cicloLetivo.SeqCicloLetivo)
                                                                 .Where(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA)
                                                                 .ToList();

                                    if (listaHistoricoSituacao.Count > 0)
                                        IncluirSituacaoMatriculadoMobilidade(dadosMatriculaAluno.SeqAluno, cicloLetivo.SeqCicloLetivo, periodoDadosAntigos.Seq, cicloLetivo.DataInicio);
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            #endregion [ Concede Formação ]

            #region [ Não concede formação ]
            else
            {
                string observacaoExclusao = "Excluído devido a alteração do período de intercâmbio.";

                #region [ Item 1 ]
                // Se data Início antecipada
                if (periodoVO.DataInicio.Date < periodoDadosAntigos.DataInicio.Date)
                {
                    //long seqHistoricoSituacaoInserido = 0;

                    // [ Item 1.1 ]
                    // Incluir a situação “MATRICULADO” na data início (Horário 00:00) informada no ciclo letivo correspondente a ela.
                    //IncluirAlunoHistoricoSituacaoVO novaSituacaomatriculado = new IncluirAlunoHistoricoSituacaoVO()
                    //{
                    //    SeqAluno = periodoVO.SeqAluno,
                    //    SeqCicloLetivo = cicloLetivoDataInicioNova.SeqCicloLetivo,
                    //    SeqPeriodoIntercambio = periodoVO.Seq,
                    //    DataInicioSituacao = periodoVO.DataInicio,
                    //    TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO
                    //};

                    //seqHistoricoSituacaoInserido = AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacaoRetornaSeq(novaSituacaomatriculado);

                    // [ Item 1.2 ]
                    // Atualizar a data de admissão do aluno para ficar igual a nova data início informada.
                    var alunoHistoricoAtual = AlunoHistoricoDomainService.BuscarHistoricoAtualAluno(periodoVO.SeqPessoaAtuacao, IncludesAlunoHistorico.Nenhum);
                    alunoHistoricoAtual.DataAdmissao = periodoVO.DataInicio;

                    AlunoHistoricoDomainService.UpdateEntity(alunoHistoricoAtual, x => x.DataAdmissao);

                    // [ Item 1.3 ]
                    // Excluir a situação “MATRICULADO” com a data início maior que a nova data início e menor ou igual a data fim de intercâmbio.
                    // desconsiderar situação de matrícula com data de exclusão.
                    //ExcluirSituacaoMatriculaEntreDatasInicioFimIntercambio(alunoHistorico.SeqAluno, cicloLetivoDataInicioAntiga.SeqCicloLetivo, periodoVO.DataInicio, periodoVO.DataFim);

                    // [ Item 1.4 ]
                    // Excluir todas as situações que estão entre a nova data início e a data início antiga, exceto situações com data de exclusão.
                    //var ciclosEntreDatasInicio = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoVO.DataInicio, periodoDadosAntigos.DataInicio, seqCursoOfertaLocalidadeTurno, tipoAluno);

                    //foreach (var cicloLetivo in ciclosEntreDatasInicio)
                    //{
                    //    var situacoesHistorico = RecuperarListaHistoricoSituacao(alunoHistorico.SeqAluno, cicloLetivo.SeqCicloLetivo)
                    //                            .Where(c => c.DataExclusao == null &&
                    //                                        c.SeqAlunoHistoricoSituacao != seqHistoricoSituacaoInserido &&
                    //                                        c.DataInicioSituacao.Date > periodoVO.DataInicio.Date &&
                    //                                        c.DataInicioSituacao.Date < periodoDadosAntigos.DataInicio.Date)
                    //                            .ToList();

                    //    foreach (var situacao in situacoesHistorico)
                    //    {
                    //        AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, observacaoExclusao, null);
                    //    }
                    //}
                }
                #endregion [ Item 1 ]

                #region [ Item 2 ]
                // Se data início prorrogada
                if (periodoVO.DataInicio.Date > periodoDadosAntigos.DataInicio.Date)
                {
                    //long seqHistoricoSituacaoInserido = 0;

                    //IncluirAlunoHistoricoSituacaoVO novaSituacaomatriculado = new IncluirAlunoHistoricoSituacaoVO()
                    //{
                    //    SeqAluno = periodoVO.SeqAluno,
                    //    SeqCicloLetivo = cicloLetivoDataInicioNova.SeqCicloLetivo,
                    //    SeqPeriodoIntercambio = periodoVO.Seq,
                    //    DataInicioSituacao = periodoVO.DataInicio,
                    //    TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO
                    //};

                    // [ Item 2.1 ]
                    // Incluir a situação “MATRICULADO” na data início (Horário 00:00) informada no ciclo letivo correspondente a ela.
                    //seqHistoricoSituacaoInserido = AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacaoRetornaSeq(novaSituacaomatriculado);

                    // [ Item 2.2 ]
                    // Atualizar a data de admissão do aluno para ficar igual a nova data início informada.
                    var alunoHistoricoAtual = AlunoHistoricoDomainService.BuscarHistoricoAtualAluno(periodoVO.SeqPessoaAtuacao, IncludesAlunoHistorico.Nenhum);
                    alunoHistoricoAtual.DataAdmissao = periodoVO.DataInicio;
                    AlunoHistoricoDomainService.UpdateEntity(alunoHistoricoAtual, x => x.DataAdmissao);

                    // [ Item 2.3 ]
                    // Excluir a situação “MATRICULADO” com a data inicio menor que a nova data de intercambio informada, desconsiderar situação de matrícula com a data de exclusão.
                    //ExcluirSituacaoMatriculaAnteriorANovaDataInicio(alunoHistorico.SeqAluno, cicloLetivoDataInicioAntiga.SeqCicloLetivo, periodoVO.DataInicio);

                    // [ Item 2.4 ]
                    // Excluir todas as situações que estão entre a data início nova e a data início antiga, exceto situações com data de exclusão.
                    //// Ignorar situação que acabou de ser incluída
                    //var ciclosEntreDatasInicio = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoVO.DataInicio, periodoDadosAntigos.DataInicio, seqCursoOfertaLocalidadeTurno, tipoAluno);

                    //foreach (var cicloLetivo in ciclosEntreDatasInicio)
                    //{
                    //    var situacoesHistorico = RecuperarListaHistoricoSituacao(alunoHistorico.SeqAluno, cicloLetivo.SeqCicloLetivo)
                    //                            .Where(c => c.DataExclusao == null &&
                    //                                        c.SeqAlunoHistoricoSituacao != seqHistoricoSituacaoInserido &&
                    //                                        c.DataInicioSituacao.Date > periodoVO.DataFim.Date &&
                    //                                        c.DataInicioSituacao.Date < periodoDadosAntigos.DataFim.Date)
                    //                            .ToList();

                    //    foreach (var situacao in situacoesHistorico)
                    //    {
                    //        AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, observacaoExclusao, null);
                    //    }
                    //}
                }
                #endregion [ Fim Item 2 ]

                #region [ Item 3 ]
                // Se data fim for antecipada
                if (periodoVO.DataFim.Date < periodoDadosAntigos.DataFim.Date)
                {
                    long seqHistoricoSituacaoInserido = 0;

                    // [ Item 3.1 ]
                    // Se o ciclo letivo correspondente a nova data fim informada

                    // [ Item 3.1.1 ]
                    // Não for encontrado, não retornar a mensagem de erro de ciclo letivo não existente e prosseguir (3.2, 3.3 e 3.4)

                    // [ Item 3.1.2 ] 
                    // For encontrado
                    if (cicloLetivoDataFimNova != null)
                    {
                        var situacoesMatricula = RecuperarListaHistoricoSituacao(dadosMatriculaAluno.SeqAluno, cicloLetivoDataFimNova.SeqCicloLetivo);

                        // SOMENTE se existir pelo menos uma situação de matrícula diferente de "APTO_MATRICULA" no ciclo letivo referente a nova data fim
                        var existeSituacao = situacoesMatricula.Any(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA);

                        if (existeSituacao)
                        {
                            // Incluir a situação “TERMINO_INTERCAMBIO” na data fim informada +1 dia, no ciclo letivo correspondente a data fim informada.
                            IncluirAlunoHistoricoSituacaoVO novaSituacao = new IncluirAlunoHistoricoSituacaoVO()
                            {
                                SeqAluno = alunoHistorico.SeqAluno,
                                SeqCicloLetivo = cicloLetivoDataFimNova.SeqCicloLetivo,
                                SeqPeriodoIntercambio = periodoVO.Seq,
                                DataInicioSituacao = periodoVO.DataFim.AddDays(1),
                                TokenSituacao = TOKENS_SITUACAO_MATRICULA.TERMINO_INTERCAMBIO
                            };

                            seqHistoricoSituacaoInserido = AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacaoRetornaSeq(novaSituacao);
                        }
                    }

                    var alunoHistoricoAtual = AlunoHistoricoDomainService.BuscarHistoricoAtualAluno(periodoVO.SeqPessoaAtuacao, IncludesAlunoHistorico.PrevisoesConclusao);

                    var previsaoConclusao = alunoHistoricoAtual.PrevisoesConclusao.FirstOrDefault();

                    // [ Item 3.2 ]
                    // Atualizar a data de previsão de conclusão do aluno para ficar igual a nova data fim informada.
                    previsaoConclusao.DataPrevisaoConclusao = periodoVO.DataFim;
                    previsaoConclusao.DataLimiteConclusao = periodoVO.DataFim;

                    AlunoHistoricoPrevisaoConclusaoDomainService.UpdateEntity(previsaoConclusao,
                                                                                x => x.DataPrevisaoConclusao,
                                                                                x => x.DataLimiteConclusao);


                    // [ Item 3.3 ]
                    // Excluir as situações de matrícula existentes com data início maior ou igual a nova data fim informada + 1 dia.
                    // não excluir a situação que acabou de ser incluída.
                    // Desconsiderar situação de matrícula com data de exclusão.
                    var situacoesMatriculaPorDataInicio = RecuperarSituacoesAlunoPorDataInicio(dadosMatriculaAluno.SeqAluno)
                                            .Where(c => c.DataExclusao == null &&
                                                        c.SeqAlunoHistoricoSituacao != seqHistoricoSituacaoInserido &&
                                                        c.DataInicioSituacao.Date >= periodoVO.DataFim.Date.AddDays(1))
                                            .ToList();

                    if (situacoesMatriculaPorDataInicio != null && situacoesMatriculaPorDataInicio.Count() > 0)
                        foreach (var situacao in situacoesMatriculaPorDataInicio)
                            AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, observacaoExclusao, null);

                    List<DatasEventoLetivoVO> listaCiclosDataInicio;

                    if (periodoVO.DataInicio.Date < periodoDadosAntigos.DataInicio.Date)
                    {
                        listaCiclosDataInicio = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoVO.DataInicio, null, seqCursoOfertaLocalidadeTurno, tipoAluno);
                    }
                    else
                    {
                        listaCiclosDataInicio = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoDadosAntigos.DataInicio, null, seqCursoOfertaLocalidadeTurno, tipoAluno);
                    }

                    // [ Item 3.4 ]
                    // Verificar se algum ciclo letivo do aluno histórico em questão está com todas as situações de matrícula com data de exclusão setada.
                    foreach (var cicloLetivo in listaCiclosDataInicio)
                    {
                        var listaSituacoes = RecuperarListaHistoricoSituacao(dadosMatriculaAluno.SeqAluno, cicloLetivo.SeqCicloLetivo);
                        var countSituacoesExcluidas = listaSituacoes.Count(c => c.DataExclusao != null);

                        if (countSituacoesExcluidas == listaSituacoes.Count())
                        {
                            // [ Item 3.4.1 ]
                            // Se estiver para os planos de estudos de todos ciclos letivos encontrados nessas condições, realizar as seguintes ações:

                            var spec = new AlunoHistoricoSituacaoFilterSpecification()
                            {
                                SeqPessoaAtuacaoAluno = periodoVO.SeqAluno,
                                SeqCicloLetivo = cicloLetivo.SeqCicloLetivo
                            };


                            var divisoesTurma = DivisaoTurmaDomainService.BuscarDivisoesTurmaCicloAtualAluno(periodoVO.SeqAluno);

                            // [ Item 3.4.1.1 ] 
                            // Para cada divisão de turma existente no plano de estudo ATUAL do [ciclo letivo], subtrair
                            // 1 da qtd_vagas_ocupadas.
                            foreach (var divisaoTurma in divisoesTurma)
                            {
                                if (divisaoTurma != null)
                                {
                                    DivisaoTurmaDomainService.AtualizarQuantidadeVagasOcupadas(divisaoTurma.Seq, (int)divisaoTurma.QuantidadeVagasOcupadas--);
                                }
                            }
                            // Itens 3.4.1.2, 3.4.1.3 e 3.4.1.4
                            IncluirNovoPlanoEstudoSemItem(cicloLetivo.SeqCicloLetivo, periodoVO.SeqPessoaAtuacao);
                        }
                    }
                }
                #endregion [ Fim Item 3 ]

                #region [ Item 4 ]
                // Se data fim prorrogada
                if (periodoVO.DataFim.Date > periodoDadosAntigos.DataFim.Date)
                {
                    long seqHistoricoSituacaoInserido = 0;

                    // [ Item  4.1 ] 
                    // Se o ciclo letivo correspondente a nova data fim informada
                    // [ Item 4.1.1 ]
                    // Não for encontrado, não retornar a mensagem de erro de ciclo letivo não existente e prosseguir (4.2 e 4.3)

                    // [ Item 4.1.2 ] 
                    // For encontrado
                    if (cicloLetivoDataFimNova != null)
                    {
                        // SOMENTE se existir pelo menos uma situação de matrícula, diferente de "APTO_MATRICULA"
                        // no ciclo letivo referente a nova data fim 
                        var situacoesMatricula = RecuperarListaHistoricoSituacao(dadosMatriculaAluno.SeqAluno, cicloLetivoDataFimNova.SeqCicloLetivo);
                        var existeSituacao = situacoesMatricula.Any(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA);

                        if (existeSituacao)
                        {
                            // incluir a situação “TERMINO_INTERCAMBIO” na data fim informada +1 dia, no ciclo letivo correspondente a nova data fim informada.
                            IncluirAlunoHistoricoSituacaoVO novaSituacao = new IncluirAlunoHistoricoSituacaoVO()
                            {
                                SeqAluno = alunoHistorico.SeqAluno,
                                SeqCicloLetivo = cicloLetivoDataFimNova.SeqCicloLetivo,
                                SeqPeriodoIntercambio = periodoVO.Seq,
                                DataInicioSituacao = periodoVO.DataFim.AddDays(1),
                                TokenSituacao = TOKENS_SITUACAO_MATRICULA.TERMINO_INTERCAMBIO
                            };

                            seqHistoricoSituacaoInserido = AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacaoRetornaSeq(novaSituacao);
                        }
                    }

                    var alunoHistoricoAtual = AlunoHistoricoDomainService.BuscarHistoricoAtualAluno(periodoVO.SeqPessoaAtuacao, IncludesAlunoHistorico.PrevisoesConclusao);

                    // [ Item 4.2 ]
                    // Atualizar a data de previsão de conclusão do aluno para ficar igual a nova data fim informada.                    
                    var previsaoConclusao = alunoHistoricoAtual.PrevisoesConclusao.FirstOrDefault();
                    previsaoConclusao.DataPrevisaoConclusao = periodoVO.DataFim;
                    previsaoConclusao.DataLimiteConclusao = periodoVO.DataFim;

                    AlunoHistoricoPrevisaoConclusaoDomainService.UpdateEntity(previsaoConclusao,
                                                                                x => x.DataPrevisaoConclusao,
                                                                                x => x.DataLimiteConclusao);


                    // Excluir as situações de matrícula com data início maior que a data fim antiga e menor que a data fim informada, desconsiderar situações com data de exclusão.
                    // Obs: não está excluindo o último histórico situação inserido.
                    //var ciclosEntreDataFimNova = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoDadosAntigos.DataFim, periodoVO.DataFim, seqCursoOfertaLocalidadeTurno, tipoAluno);

                    //foreach (var cicloLetivo in ciclosEntreDataFimNova)
                    //{
                    //    var situacoesMatricula = RecuperarListaHistoricoSituacao(dadosMatriculaAluno.SeqAluno, cicloLetivo.SeqCicloLetivo)
                    //                            .Where(c => c.DataExclusao == null &&
                    //                                        c.SeqAlunoHistoricoSituacao != seqHistoricoSituacaoInserido &&
                    //                                        c.DataInicioSituacao.Date > periodoDadosAntigos.DataFim.Date &&
                    //                                        c.DataInicioSituacao.Date < periodoVO.DataFim.Date)
                    //                            .ToList();

                    //    foreach (var situacao in situacoesMatricula)
                    //    {
                    //        AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, observacaoExclusao, null);
                    //    }
                    //}

                    // [ Item 4.3 ]  
                    //Excluir a situação “TERMINO_INTERCAMBIO” correspondente a data início antiga + 1 dia.
                    //Data de exclusão: data atual(do sistema).
                    //Usuário de exclusão: usuário logado
                    //Observação de exclusão: "Excluído devido a alteração do período de intercâmbio."

                    var situacaoTerminoIntercambioSpec = new AlunoHistoricoSituacaoFilterSpecification
                    {
                        TokenSituacao = TOKENS_SITUACAO_MATRICULA.TERMINO_INTERCAMBIO,
                        SeqPessoaAtuacaoAluno = periodoVO.SeqAluno,
                        DataInicioSituacao = periodoDadosAntigos.DataFim.AddDays(1)
                    };

                    var situacaoTerminoIntercambio = AlunoHistoricoSituacaoDomainService.SearchByKey(situacaoTerminoIntercambioSpec);
                    if (situacaoTerminoIntercambio != null)
                        AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacaoTerminoIntercambio.Seq, observacaoExclusao, null);

                    //[ Item 4.4 ]  
                    var seqProximoCiclo = CicloLetivoDomainService.BuscarProximoCicloLetivo(cicloLetivoDataFimAntiga.SeqCicloLetivo);
                    long? proxCiclo = null;

                    if (periodoDadosAntigos.DataFim != null)
                    {
                        proxCiclo = CicloLetivoDomainService.BuscarProximoCicloLetivo(cicloLetivoDataFimAntiga.SeqCicloLetivo);
                    }

                    while (proxCiclo != null)
                    {
                        var sitProximoCiclo = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoNoCicloLetivo(periodoVO.SeqAluno, proxCiclo.Value);
                        var cicloDataFim = true;
                        if (sitProximoCiclo != null)
                            cicloDataFim = proxCiclo == cicloLetivoDataFimNova.SeqCicloLetivo;


                        //4.4.Do ciclo letivo posterior a data fim antiga ao ciclo letivo da nova data fim, verificar para cada ciclo letivo encontrado se existe situação de matricula diferente de "APTO_MATRICULA"
                        if (sitProximoCiclo.Any(x => x.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA))
                        {
                            //4.4.1.Se existir:
                            var situacaoAptoExcluidaSpec = new AlunoHistoricoSituacaoFilterSpecification
                            {
                                Excluido = true,
                                TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA,
                                SeqPessoaAtuacaoAluno = periodoVO.SeqAluno
                            };

                            var situacaoAptoExcluida = AlunoHistoricoSituacaoDomainService.SearchBySpecification(situacaoAptoExcluidaSpec).FirstOrDefault();



                            if (situacaoAptoExcluida != null)
                            {
                                //4.4.1.1.Incluir a situação de "APTO_MATRICULA" com a data inicio igual a data da situação de "APTO_MATRICULA" com data de exclusão.
                                var situacaoApto = new IncluirAlunoHistoricoSituacaoVO()
                                {
                                    SeqAluno = periodoVO.SeqAluno,
                                    SeqCicloLetivo = proxCiclo.Value,
                                    TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA,
                                    DataInicioSituacao = situacaoAptoExcluida.DataInicioSituacao,
                                };

                                AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoApto);
                            }

                            var situacaoMatriculadoExcluidaSpec = new AlunoHistoricoSituacaoFilterSpecification
                            {
                                Excluido = true,
                                SeqCicloLetivo = proxCiclo.Value,
                                TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO,
                                SeqPessoaAtuacaoAluno = periodoVO.SeqAluno
                            };

                            var situacaoMatriculadoExcluida = AlunoHistoricoSituacaoDomainService.SearchBySpecification(situacaoMatriculadoExcluidaSpec).FirstOrDefault();
                            if (situacaoMatriculadoExcluida != null)
                            {
                                //4.4.1.2.Incluir a situação de "MATRICULADO" com a data inicio igual a data da situação de "MATRICULADO" com data de exclusão.
                                var situacaoMatriculado = new IncluirAlunoHistoricoSituacaoVO()
                                {
                                    SeqAluno = periodoVO.SeqAluno,
                                    SeqCicloLetivo = proxCiclo.Value,
                                    TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO,
                                    DataInicioSituacao = situacaoMatriculadoExcluida.DataInicioSituacao,
                                };

                                AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoMatriculado);
                            }
                        }

                        //Garantindo que o loop pare exatamente no ciclo letivo correspondente a data fim nova.
                        if (!cicloDataFim)
                            proxCiclo = CicloLetivoDomainService.BuscarProximoCicloLetivo(proxCiclo.Value);
                        else
                            proxCiclo = null;
                    }
                    //4.4.2.Se não existir, não fazer nada.
                }
                #endregion [ Item 4 ]
            }
            #endregion [ Não concede formação]
        }

        #region [ Métodos regra RN_ALN_063 ]

        /// <summary>
        /// Método usado para incluir situação histórico 'matriculado mobilidade'
        /// Usado na regra RN_ALN_063 - Item 1.1.2 e 2.1.2
        /// </summary>      
        private long IncluirNovaSituacaoMatriculadoMobilidade(long seqAluno, long seqCicloLetivo, long seqPeriodoIntercambio, DateTime dataInicio, PeriodoIntercambioVO periodoVO)
        {
            long seqHistoricoSituacaoInserido = 0;
            var listaHistoricoAluno = RecuperarListaHistoricoSituacao(seqAluno, seqCicloLetivo);

            // Somente se existir pelo menos uma situação de matrícula diferente de 'APTO_MATRICULA' no ciclo letivo referente a nova data início.
            bool existeEntrada = listaHistoricoAluno.Any(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA);

            if (existeEntrada)
            {
                // Incluir a situação 'MATRICULADO_MOBILIDADE' na data início (horário 00:00) informada no ciclo letivo correspondente a ela.
                // Data início inserida posteriormente
                IncluirAlunoHistoricoSituacaoVO novoHistoricoMatriculadoMobilidade = new IncluirAlunoHistoricoSituacaoVO()
                {
                    SeqAluno = seqAluno,
                    SeqCicloLetivo = seqCicloLetivo,
                    SeqPeriodoIntercambio = seqPeriodoIntercambio,
                    TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE
                };

                // Se existir outra situação de matricula na mesma data, incluir a situação com a hora da situação existente +1 minuto.
                // Desconsiderar situação com data de exclusão.
                var situacoesNaMesmaData = listaHistoricoAluno.Where(c => c.DataInicioSituacao.Date == dataInicio.Date && c.DataExclusao == null).ToList();

                if (situacoesNaMesmaData.Count > 0)
                {
                    novoHistoricoMatriculadoMobilidade.DataInicioSituacao = situacoesNaMesmaData.FirstOrDefault().DataInicioSituacao.AddMinutes(1);
                    periodoVO.DataInicio = novoHistoricoMatriculadoMobilidade.DataInicioSituacao;
                }
                else
                {
                    novoHistoricoMatriculadoMobilidade.DataInicioSituacao = dataInicio;
                }

                seqHistoricoSituacaoInserido = AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacaoRetornaSeq(novoHistoricoMatriculadoMobilidade);
            }

            return seqHistoricoSituacaoInserido;
        }

        /// <summary>
        /// Excluir a situação “MATRICULADO_MOBILIDADE” correspondente a data início antiga, desconsiderar situação com data de exclusão. 
        /// Usado na regra RN_ALN_063 - Itens 1.2 e 2.2
        /// </summary>
        private void ExcluirSituacaoMatriculadoMobilidadeDataInicio(long seqAluno, long seqCicloLetivo, DateTime dataInicio)
        {
            string observacaoExclusao = "Excluído devido a alteração do período de intercâmbio.";

            // Excluir a situação “MATRICULADO_MOBILIDADE” correspondente a data início antiga, desconsiderando situação com data de exclusão.
            var listaHistoricoSituacaoMatriculadoMobilidade = RecuperarListaHistoricoSituacao(seqAluno, seqCicloLetivo)
                                                             .Where(c => c.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE)
                                                             .ToList();

            var situacoesParaExcluir = listaHistoricoSituacaoMatriculadoMobilidade
                                      .Where(c => c.DataExclusao == null && c.DataInicioSituacao.Date == dataInicio.Date).ToList();

            foreach (var situacao in situacoesParaExcluir)
            {
                AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, observacaoExclusao, null);
            }
        }

        /// <summary>
        /// Excluir as situações diferentes de “APTO_MATRICULA” correspondente a data início antiga, desconsiderar situação com data de exclusão. 
        /// Usado na regra RN_ALN_063 - Item 1.2.1
        /// </summary>
        private void ExcluirSituacaoDiferenteAptoMatriculaDataInicio(long seqAluno, long seqCicloLetivo, DateTime dataInicio)
        {
            string observacaoExclusao = "Excluído devido a alteração do período de intercâmbio.";

            // Excluir a situação “MATRICULADO_MOBILIDADE” correspondente a data início antiga, desconsiderando situação com data de exclusão.
            var listaHistoricoSituacaoDiferenteAptoMatricula = RecuperarListaHistoricoSituacao(seqAluno, seqCicloLetivo)
                                                             .Where(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA)
                                                             .ToList();

            var situacoesParaExcluir = listaHistoricoSituacaoDiferenteAptoMatricula
                                      .Where(c => c.DataExclusao == null && c.DataInicioSituacao.Date == dataInicio.Date).ToList();

            if (situacoesParaExcluir != null && situacoesParaExcluir.Count() > 0)
                foreach (var situacao in situacoesParaExcluir)
                {
                    AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, observacaoExclusao, null);
                }
        }

        /// <summary>
        /// Excluir todas as situações que estão entre a data início nova e a data início antiga, exceto a situação “APTO_MATRICULA” 
        /// e as demais situações com data de exclusão. 
        /// Usado na regra RN_ALN_063 - Iten 1.3 e 2.3
        /// </summary>
        private void ExcluirSituacoesEntreDatasInicio(PeriodoIntercambioVO periodoVO, PeriodoIntercambio periodoIntercambioDatasAntigas, long seqHistoricoSituacaoInserido, bool dataInicioAntecipada = true)
        {
            string observacaoExclusao = "Excluído devido a alteração do período de intercâmbio.";

            var listaCiclosEntreDatas = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoIntercambioDatasAntigas.DataInicio, periodoVO.DataInicio, (long)periodoVO.SeqCursoOfertaLocalidadeTurno, TipoAluno.Veterano);

            if (dataInicioAntecipada)
                listaCiclosEntreDatas = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoVO.DataInicio, periodoIntercambioDatasAntigas.DataInicio, (long)periodoVO.SeqCursoOfertaLocalidadeTurno, TipoAluno.Veterano);

            foreach (var ciclo in listaCiclosEntreDatas)
            {
                var listaHistoricoSituacaoParaExcluir = RecuperarListaHistoricoSituacao(periodoVO.SeqAluno, ciclo.SeqCicloLetivo)
                                                       .Where(c => c.DataExclusao == null &&
                                                                   c.SeqAlunoHistoricoSituacao != seqHistoricoSituacaoInserido &&
                                                                   c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA)
                                                       .ToList();

                if (dataInicioAntecipada)
                {
                    listaHistoricoSituacaoParaExcluir = listaHistoricoSituacaoParaExcluir.Where(c =>
                                                       c.DataInicioSituacao > periodoVO.DataInicio &&
                                                       c.DataInicioSituacao < periodoIntercambioDatasAntigas.DataInicio)
                        .ToList();
                }
                else
                {
                    listaHistoricoSituacaoParaExcluir = listaHistoricoSituacaoParaExcluir.Where(c =>
                                                       c.DataInicioSituacao > periodoIntercambioDatasAntigas.DataInicio &&
                                                       c.DataInicioSituacao < periodoVO.DataInicio)
                        .ToList();
                }

                foreach (var situacao in listaHistoricoSituacaoParaExcluir)
                {
                    AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, observacaoExclusao, null);
                }
            }
        }

        /// <summary>
        /// Incluir historico situação MATRICULADO_MOBILIDADE
        /// Usado na regra RN_ALN_063 - Item I da regra 1.4.2 e 1.4.3 
        /// </summary>
        private void IncluirSituacaoMatriculadoMobilidade(long seqAluno, long seqCicloLetivo, long seqPeriodoIntercambio, DateTime dataInicio)
        {
            IncluirAlunoHistoricoSituacaoVO novoHistoricoMatriculadoMobilidade = new IncluirAlunoHistoricoSituacaoVO()
            {
                SeqAluno = seqAluno,
                SeqCicloLetivo = seqCicloLetivo,
                SeqPeriodoIntercambio = seqPeriodoIntercambio,
                TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE,
                DataInicioSituacao = dataInicio
            };

            AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(novoHistoricoMatriculadoMobilidade);
        }

        /// <summary>
        /// Incluir nova situação de provável formando ou matriculado, dependendo da data de depósito
        /// Usado na regra RN_ALN_063 - Itens I e II das regras 2 e 3
        /// </summary>
        private void IncluirMatriculadoProvavelFormando(DatasEventoLetivoVO cicloLetivo, DateTime? dataDeposito, long seqAluno, long seqPeriodoIntercambio)
        {
            var listaHistoricoAluno = RecuperarListaHistoricoSituacaoAnteciparData(seqAluno, cicloLetivo.SeqCicloLetivo);

            // [ Item I ]
            if (dataDeposito != null)
            {
                var registroMatriculadoExcluido = listaHistoricoAluno.Where(c => c.TokenTipoSituacaoMatricula == TOKENS_TIPO_SITUACAO_MATRICULA.MATRICULADO && c.DataExclusao != null).FirstOrDefault();

                if (registroMatriculadoExcluido != null)
                {

                    IncluirAlunoHistoricoSituacaoVO novaSituacao = new IncluirAlunoHistoricoSituacaoVO()
                    {
                        SeqAluno = seqAluno,
                        SeqCicloLetivo = cicloLetivo.SeqCicloLetivo,
                        SeqPeriodoIntercambio = seqPeriodoIntercambio,
                        DataInicioSituacao = registroMatriculadoExcluido.DataInicioSituacao
                    };

                    // Verificar se a data do primeiro registro do tipo de situação “MATRICULADO” (que foi excluído anteriormente) é maior ou igual a data de depósito.
                    if (registroMatriculadoExcluido?.DataInicioSituacao.Date >= dataDeposito.Value.Date)
                    {
                        // a) Se for incluir a situação “PROVAVEL_FORMANDO”, com a data do primeiro registro 
                        // do tipo de situação “MATRICULADO” (que foi excluído anteriormente).
                        novaSituacao.TokenSituacao = TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO;
                    }
                    else
                    {
                        // b) Caso contrário, incluir a situação “MATRÍCULADO”, com a data do primeiro registro 
                        // do tipo de situação “MATRICULADO” (que foi excluído anteriormente).
                        novaSituacao.TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO;
                    }

                    AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(novaSituacao);
                }
            }
            // [ Item II ]
            else
            {
                // Se não possuir data de deposito, incluir a situação “MATRÍCULADO”, com a data do primeiro
                // registro do tipo de situação “MATRICULADO” (que foi excluído anteriormente).
                IncluirAlunoHistoricoSituacaoVO novaSituacao = new IncluirAlunoHistoricoSituacaoVO()
                {
                    SeqAluno = seqAluno,
                    SeqCicloLetivo = cicloLetivo.SeqCicloLetivo,
                    SeqPeriodoIntercambio = seqPeriodoIntercambio,
                    DataInicioSituacao = listaHistoricoAluno.FirstOrDefault(c => c.TokenTipoSituacaoMatricula == TOKENS_TIPO_SITUACAO_MATRICULA.MATRICULADO && c.DataExclusao.HasValue).DataInicioSituacao,
                    TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO
                };

                AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(novaSituacao);
            }
        }

        /// <summary>
        /// Inclui uma nova situação histórico, sendo 'Matriculado' ou 'Provavel formando'
        /// Usado na regra Regra RN_ALN_063 - Item 3.1.2
        /// </summary>
        private long IncluirNovaSituacaoMatriculadoProvavelFormandoDataFim(long seqAluno, long seqCicloLetivo, DateTime? dataDeposito, DateTime dataFimCicloLetivo, DateTime dataFimNova)
        {
            long seqHistoricoIncluido = 0;
            var listaHistoricoAluno = RecuperarListaHistoricoSituacao(seqAluno, seqCicloLetivo);

            // [ Item 3.1.2 ]
            // SOMENTE se existir pelo menos uma situação de matrícula, diferente de "APTO_MATRICULA", no ciclo letivo referente a nova data fim
            bool existeSituacao = listaHistoricoAluno.Any(c => c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA);

            if (existeSituacao)
            {
                // Verificar se a data fim informada é menor que a data fim do ciclo letivo correspondente.
                if (dataFimNova.Date < dataFimCicloLetivo.Date)
                {
                    IncluirAlunoHistoricoSituacaoVO novoHistorico = new IncluirAlunoHistoricoSituacaoVO()
                    {
                        SeqAluno = seqAluno,
                        SeqCicloLetivo = seqCicloLetivo
                    };

                    var registroMatriculado = listaHistoricoAluno.Where(c => c.DataExclusao == null && c.TokenTipoSituacaoMatricula == TOKENS_TIPO_SITUACAO_MATRICULA.MATRICULADO).FirstOrDefault();

                    // [ Item 3.1.2.1 ]
                    // Se for menor, verificar se a data informada é menor que a data do primeiro registro do tipo de
                    // situação “MATRICULADO”, desconsiderar situação com data de exclusão
                    if (dataFimNova.Date < registroMatriculado?.DataInicioSituacao.Date)
                    {

                        // [ Item 3.1.2.1.1 ]
                        // Se for, verificar se a data do primeiro registro do tipo de situação “MATRICULADO” 
                        // é maior ou igual a data de depósito.
                        if (dataDeposito != null)
                        {
                            // [ Item 3.1.2.1.1.1 ]
                            if (registroMatriculado?.DataInicioSituacao.Date >= dataDeposito.Value.Date)
                            {
                                // Se for incluir a situação “PROVAVEL_FORMANDO”, com a data do primeiro
                                // registro do tipo de situação “MATRICULADO”.
                                novoHistorico.TokenSituacao = TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO;
                                novoHistorico.DataInicioSituacao = registroMatriculado.DataInicioSituacao;
                            }
                            // [ Item 3.1.2.1.1.2 ]
                            else
                            {
                                // Caso contrário, incluir a situação “MATRÍCULADO”, com a data do primeiro registro
                                // do tipo de situação “MATRICULADO”.
                                novoHistorico.TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO;
                                novoHistorico.DataInicioSituacao = registroMatriculado.DataInicioSituacao;
                            }
                        }
                        // [ Item 3.1.2.1.1.3 ]
                        else
                        {
                            // Se não possuir data de deposito, incluir a situação “MATRÍCULADO”, com a data do
                            // primeiro registro do tipo de situação “MATRICULADO”.
                            novoHistorico.TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO;
                            novoHistorico.DataInicioSituacao = registroMatriculado.DataInicioSituacao;
                        }
                    }
                    // [ Item 3.1.2.1.2 ]
                    else
                    {
                        if (dataDeposito != null)
                        {
                            // Se não for menor, verificar se a data fim informada +1 dia é maior ou igual a data de depósito.
                            if (dataFimNova.Date.AddDays(1) >= dataDeposito.Value.Date)
                            {
                                // 3.1.2.1.2.1. Se for incluir a situação “PROVAVEL_FORMANDO”, com a data fim informada + 1 dia.
                                novoHistorico.TokenSituacao = TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO;
                                novoHistorico.DataInicioSituacao = dataFimNova.AddDays(1);
                            }
                            else
                            {
                                // 3.1.2.1.2.2.Caso contrário, incluir a situação “MATRICULADO”, com a data fim informada +1 dia.
                                novoHistorico.TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO;
                                novoHistorico.DataInicioSituacao = dataFimNova.AddDays(1);
                            }
                        }
                        // 3.1.2.1.2.3. Se não possuir data de deposito, incluir a situação “MATRÍCULADO”, com a data fim informada + 1 dia.
                        else
                        {
                            novoHistorico.TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO;
                            novoHistorico.DataInicioSituacao = dataFimNova.AddDays(1);
                        }
                    }

                    seqHistoricoIncluido = AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacaoRetornaSeq(novoHistorico);
                }
                // 3.1.2.2. Caso contrário, não fazer nada.
            }

            return seqHistoricoIncluido;
        }

        /// <summary>
        /// Método que exclui a situação “MATRICULADO” correspondente a data fim antiga + 1 dia, caso exista. Desconsiderando situação com data de exclusão.
        /// Usado na regra regra RN_ALN_063 - Itens 3.2 e 4.2
        /// </summary>
        private void ExcluirSituacaoMatriculadoDataFim(long seqAluno, long seqCicloLetivo, DateTime dataFimAntiga)
        {
            string observacaoExclusao = "Excluído devido a alteração do período de intercâmbio.";
            var listaHistoricoSituacao = RecuperarListaHistoricoSituacao(seqAluno, seqCicloLetivo);

            // [ Itens 3.2 e 4.2 ]
            // Excluir a situação “MATRICULADO” correspondente a data fim antiga +1 dia, caso exista.
            // Desconsiderar situação com data de exclusão.
            var situacaoMatriculadoParaExcluir = listaHistoricoSituacao.Where(c => c.DataExclusao == null &&
                                                                                   (c.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.MATRICULADO
                                                                                   || c.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO)
                                                                                   && c.DataInicioSituacao.Date == dataFimAntiga.Date.AddDays(1))
                                                                       .FirstOrDefault();

            if (situacaoMatriculadoParaExcluir != null)
            {
                AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacaoMatriculadoParaExcluir.SeqAlunoHistoricoSituacao, observacaoExclusao, null);
            }
        }

        /// <summary>
        /// Excluir todas as situações que estão entre a data fim nova e a data fim antiga, exceto a situação
        /// “APTO_MATRÍCULA” (não excluir as situações que acabaram de ser incluídas) e as demais situações com data de exclusão.
        /// Usado na regra RN_ALN_063 - Item 3.3
        /// </summary>        
        private void ExcluirTodasSituacoesEntreDatasFim(PeriodoIntercambioVO periodoVO, PeriodoIntercambio periodoIntercambio, long seqHistoricoSituacaoIncluido)
        {
            string observacaoExclusao = "Excluído devido a alteração do período de intercâmbio.";

            var listaCiclosEntreDatas = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(periodoVO.DataFim, periodoIntercambio.DataFim, (long)periodoVO.SeqCursoOfertaLocalidadeTurno, TipoAluno.Veterano);

            foreach (var ciclo in listaCiclosEntreDatas)
            {
                var listaHistoricoSituacaoParaExcluir = RecuperarListaHistoricoSituacao(periodoVO.SeqAluno, ciclo.SeqCicloLetivo)
                                                       .Where(c => c.DataExclusao == null &&
                                                                   c.SeqAlunoHistoricoSituacao != seqHistoricoSituacaoIncluido &&
                                                                   c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA &&
                                                                   c.DataInicioSituacao > periodoVO.DataFim &&
                                                                   c.DataInicioSituacao < periodoIntercambio.DataFim
                                                                   )
                                                       .ToList();

                foreach (var situacao in listaHistoricoSituacaoParaExcluir)
                {
                    AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, observacaoExclusao, null);
                }
            }
        }

        /// <summary>
        /// Excluir as situações de matrícula com data início maior que a data fim antiga e menor 
        /// que a data fim informadaa, exceto a situação “APTO_MATRICULA”.
        /// (não excluir as situações que acabaram de ser incluídas) desconsiderar situação com data de exclusão.
        /// Usado na regra RN_ALN_063 - Item 4.3
        /// </summary>
        private void ExcluirSituacoesEntreDatas(DateTime dataInicio, DateTime dataFim, long seqAluno, long seqCursoOfertaLocalidade, TipoAluno tipoAluno, long seqHistoricoIncluido)
        {
            string observacaoExclusao = "Excluído devido a alteração do período de intercâmbio.";
            var listaCiclosEntreDatasFim = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(dataInicio, dataFim, seqCursoOfertaLocalidade, tipoAluno);

            foreach (var ciclo in listaCiclosEntreDatasFim)
            {
                var listaAlunoHistoricoSituacao = RecuperarListaHistoricoSituacao(seqAluno, ciclo.SeqCicloLetivo)
                                                 .Where(c => c.DataExclusao == null &&
                                                             c.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA &&
                                                             c.SeqAlunoHistoricoSituacao != seqHistoricoIncluido && //(não excluir as situações que acabaram de ser incluídas)
                                                             c.DataInicioSituacao > dataInicio &&
                                                             c.DataInicioSituacao <= dataFim)
                                                 .ToList();

                foreach (var situacao in listaAlunoHistoricoSituacao)
                {
                    AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, observacaoExclusao, null);
                }
            }
        }

        /// <summary>
        /// Incluir a situação “MATRICULADO_MOBILIDADE” na data início do ciclo letivo. Desconsiderar situação com data de exclusão.
        /// Usado na regra RN_ALN_063 - Item I da regra 4.4
        /// </summary>
        /// <param name="seqCicloLetivo"></param>
        /// <param name="seqAluno"></param>
        /// <param name="datainicio"></param>
        private void InserirSituacaoInicioCicloLetivo(long seqCicloLetivo, long seqAluno, long seqPeriodoIntercambio, DateTime dataInclusao)
        {
            IncluirAlunoHistoricoSituacaoVO novoHistorico = new IncluirAlunoHistoricoSituacaoVO()
            {
                SeqAluno = seqAluno,
                SeqCicloLetivo = seqCicloLetivo,
                SeqPeriodoIntercambio = seqPeriodoIntercambio,
                DataInicioSituacao = dataInclusao,
                TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE
            };

            AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(novoHistorico);
        }

        /// <summary>
        /// Excluir a situação “MATRICULADO” com a data início maior que a nova data início
        /// e menor ou igual a data fim de intercâmbio, desconsiderar situação de matrícula com data de exclusão.
        /// Usado na regra RN_ALN_063 - Iten 1.3 - não conceder formação
        /// </summary>
        private void ExcluirSituacaoMatriculaEntreDatasInicioFimIntercambio(long seqAluno, long seqCicloLetivo, DateTime dataInicio, DateTime dataFimIntercambio)
        {
            string observacaoExclusao = "Excluído devido a alteração do período de intercâmbio.";
            var listaHistoricoSituacao = RecuperarListaHistoricoSituacao(seqAluno, seqCicloLetivo);

            // Excluir a situação “MATRICULADO” com a data início maior que a nova data início
            // e menor ou igual a data fim de intercâmbio, desconsiderando situação de matrícula com data de exclusão.            
            var situacaoMatriculadoParaExcluir = listaHistoricoSituacao.Where(c => c.DataExclusao == null &&
                                                                                   c.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.MATRICULADO &&
                                                                                   c.DataInicioSituacao.Date > dataInicio.Date &&
                                                                                   c.DataInicioSituacao.Date <= dataFimIntercambio.Date)
                                                                       .FirstOrDefault();

            if (situacaoMatriculadoParaExcluir != null)
            {
                AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacaoMatriculadoParaExcluir.SeqAlunoHistoricoSituacao, observacaoExclusao, null);
            }
        }

        /// <summary>
        /// Excluir a situação “MATRICULADO” com a data inicio menor que a nova data de intercambio informada, desconsiderar situação de matrícula com a data de exclusão.
        /// Usado na regra RN_ALN_063 - Item 2.3 - não conceder formação
        /// </summary>
        private void ExcluirSituacaoMatriculaAnteriorANovaDataInicio(long seqAluno, long seqCicloLetivo, DateTime dataInicio)
        {
            string observacaoExclusao = "Excluído devido a alteração do período de intercâmbio.";
            var listaHistoricoSituacao = RecuperarListaHistoricoSituacao(seqAluno, seqCicloLetivo);

            // [ Item 2.3 ]
            var situacaoMatriculadoParaExcluir = listaHistoricoSituacao.Where(c => c.DataExclusao == null &&
                                                                                   c.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.MATRICULADO &&
                                                                                   c.DataInicioSituacao.Date < dataInicio.Date)
                                                                       .FirstOrDefault();

            if (situacaoMatriculadoParaExcluir != null)
            {
                AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacaoMatriculadoParaExcluir.SeqAlunoHistoricoSituacao, observacaoExclusao, null);
            }
        }

        private List<HistoricoSituacaoFimDataInicio> RecuperarListaHistoricoSituacao(long seqAluno, long seqCicloLetivo)
        {
            var specDataInicio = new AlunoHistoricoSituacaoFilterSpecification()
            {
                SeqPessoaAtuacaoAluno = seqAluno,
                SeqCicloLetivo = seqCicloLetivo
            };

            specDataInicio.SetOrderBy(s => s.DataInicioSituacao);

            var listaHistoricoSituacaoFimDataInicio = AlunoHistoricoSituacaoDomainService.SearchProjectionBySpecification(specDataInicio,
            s => new HistoricoSituacaoFimDataInicio()
            {
                SeqAlunoHistoricoSituacao = s.Seq,
                SeqSituacao = s.SeqSituacaoMatricula,
                SituacaoMatricula = s.SituacaoMatricula.Descricao,
                TipoSituacaoMatriculaVinculoAlunoAtivo = s.SituacaoMatricula.TipoSituacaoMatricula.VinculoAlunoAtivo,
                TokenSituacaoMatricula = s.SituacaoMatricula.Token,
                TokenTipoSituacaoMatricula = s.SituacaoMatricula.TipoSituacaoMatricula.Token,
                SeqPeriodoIntercambio = s.SeqPeriodoIntercambio,
                DataInclusao = s.DataInclusao,
                DataExclusao = s.DataExclusao,
                ObservacaoExclusao = s.ObservacaoExclusao,
                DataInicioSituacao = s.DataInicioSituacao
            }).ToList();

            return listaHistoricoSituacaoFimDataInicio;
        }

        private void IncluirNovoPlanoEstudoSemItem(long seqCicloLetivo, long seqPessoaAtuacao)
        {
            var ajustePlanoEstudo = new AjustePlanoEstudoVO()
            {
                SeqCicloLetivoReferencia = seqCicloLetivo,
                Observacao = "Plano de estudo sem componente curricular, devido à alteração do período de intercâmbio do aluno",
                SeqPessoaAtuacao = seqPessoaAtuacao,
                DataFimOrientacao = DateTime.Now
            };

            PlanoEstudoDomainService.AjustarPlanoEstudoIntercambio(ajustePlanoEstudo);
        }

        #endregion [ Métodos regra RN_ALN_063 ]


        private List<HistoricoSituacaoFimDataInicio> RecuperarSituacoesAlunoPorDataInicio(long seqAluno)
        {
            var specDataInicio = new AlunoHistoricoSituacaoFilterSpecification()
            {
                SeqPessoaAtuacaoAluno = seqAluno
            };

            specDataInicio.SetOrderBy(s => s.DataInicioSituacao);

            var listaHistoricoSituacaoFimDataInicio = AlunoHistoricoSituacaoDomainService.SearchProjectionBySpecification(specDataInicio,
            s => new HistoricoSituacaoFimDataInicio()
            {
                SeqAlunoHistoricoSituacao = s.Seq,
                SeqSituacao = s.SeqSituacaoMatricula,
                SituacaoMatricula = s.SituacaoMatricula.Descricao,
                TipoSituacaoMatriculaVinculoAlunoAtivo = s.SituacaoMatricula.TipoSituacaoMatricula.VinculoAlunoAtivo,
                TokenSituacaoMatricula = s.SituacaoMatricula.Token,
                TokenTipoSituacaoMatricula = s.SituacaoMatricula.TipoSituacaoMatricula.Token,
                SeqPeriodoIntercambio = s.SeqPeriodoIntercambio,
                DataInclusao = s.DataInclusao,
                DataExclusao = s.DataExclusao,
                ObservacaoExclusao = s.ObservacaoExclusao,
                DataInicioSituacao = s.DataInicioSituacao
            }).ToList();

            return listaHistoricoSituacaoFimDataInicio;
        }

        private List<HistoricoSituacaoFimDataInicio> RecuperarListaHistoricoSituacaoAnteciparData(long seqAluno, long seqCicloLetivo)
        {
            var specDataInicio = new AlunoHistoricoSituacaoFilterSpecification()
            {
                SeqPessoaAtuacaoAluno = seqAluno,
                SeqCicloLetivo = seqCicloLetivo
            };

            specDataInicio.SetOrderBy(s => s.DataInclusao);

            var listaHistoricoSituacaoFimDataInicio = AlunoHistoricoSituacaoDomainService.SearchProjectionBySpecification(specDataInicio,
            s => new HistoricoSituacaoFimDataInicio()
            {
                SeqAlunoHistoricoSituacao = s.Seq,
                SeqSituacao = s.SeqSituacaoMatricula,
                SituacaoMatricula = s.SituacaoMatricula.Descricao,
                TipoSituacaoMatriculaVinculoAlunoAtivo = s.SituacaoMatricula.TipoSituacaoMatricula.VinculoAlunoAtivo,
                TokenSituacaoMatricula = s.SituacaoMatricula.Token,
                TokenTipoSituacaoMatricula = s.SituacaoMatricula.TipoSituacaoMatricula.Token,
                SeqPeriodoIntercambio = s.SeqPeriodoIntercambio,
                DataInclusao = s.DataInclusao,
                DataExclusao = s.DataExclusao,
                ObservacaoExclusao = s.ObservacaoExclusao,
                DataInicioSituacao = s.DataInicioSituacao
            }).ToList();

            return listaHistoricoSituacaoFimDataInicio;
        }
    }

    /// <summary>
    /// Objeto criado para mapear um tipo anônimo que é recuperado várias vezes nos métodos de atualização de matrícula do aluno
    /// </summary>
    class HistoricoSituacaoFimDataInicio
    {
        public long SeqAlunoHistoricoSituacao { get; set; }
        public long SeqSituacao { get; set; }
        public string SituacaoMatricula { get; set; }
        public bool TipoSituacaoMatriculaVinculoAlunoAtivo { get; set; }
        public string TokenSituacaoMatricula { get; set; }
        public string TokenTipoSituacaoMatricula { get; set; }
        public long? SeqPeriodoIntercambio { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public string ObservacaoExclusao { get; set; }
        public DateTime DataInicioSituacao { get; set; }
    }
}