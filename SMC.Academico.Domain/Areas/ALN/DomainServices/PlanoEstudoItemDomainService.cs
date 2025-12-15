using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class PlanoEstudoItemDomainService : AcademicoContextDomain<PlanoEstudoItem>
    {
        #region [ DomainService ]

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService => Create<ProcessoSeletivoOfertaDomainService>();

        private RestricaoTurmaMatrizDomainService RestricaoTurmaMatrizDomainService => Create<RestricaoTurmaMatrizDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Libera a vaga ocupada em uma turma
        /// </summary>
        /// <param name="seqPlanoEstudoItem">Sequencial do item do plano de ensino a ter a vaga liberada</param>
        public void LiberarVagaPlanoEstudoItem(long seqPlanoEstudoItem)
        {
            // Busca o item do plano de estudo
            var specItem = new PlanoEstudoItemFilterSpecification() { Seq = seqPlanoEstudoItem };
            var item = this.SearchProjectionByKey(specItem, i => new
            {
                SeqDivisaoTurma = i.SeqDivisaoTurma,
                QuantidadeVagasOcupadas = i.DivisaoTurma.QuantidadeVagasOcupadas
            });

            // Se o item não tem divisão, não precisa fazer nada
            if (!item.SeqDivisaoTurma.HasValue)
                return;
            else
            {
                // Libera uma vaga na divisão de turma
                int qtdVagasOcupadas = item.QuantidadeVagasOcupadas.GetValueOrDefault();
                if (qtdVagasOcupadas > 0)
                    qtdVagasOcupadas = qtdVagasOcupadas - 1;
                DivisaoTurmaDomainService.AtualizarQuantidadeVagasOcupadas(item.SeqDivisaoTurma.Value, (short)qtdVagasOcupadas);
            }
        }

        /// <summary>
        /// Buscar plano de estudo por aluno
        /// </summary>
        /// <param name="seqAluno"></param>
        /// <returns>Retorna turmas Matricula por aluno</returns>
        public SMCPagerData<TurmaMatriculaListarVO> BuscarTurmasItensPlanoEstudo(long seqAluno)
        {
            //Buscar os dados de origem
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            var spec = new PlanoEstudoItemFilterSpecification() { SeqAluno = seqAluno, SomenteTurma = true, PlanoEstudoAtual = true };

            var dadosPlanoEstudo = this.SearchProjectionBySpecification(spec,
                p => new
                {
                    Seq = p.DivisaoTurma.SeqTurma,
                    SeqNivelEnsino = p.ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino,
                    Codigo = p.DivisaoTurma.Turma.Codigo,
                    Numero = p.DivisaoTurma.Turma.Numero,
                    NumeroGrupo = p.DivisaoTurma.NumeroGrupo,
                    SeqTipoComponenteCurricular = p.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                    QuantidadeVagas = (short?)p.DivisaoTurma.Turma.QuantidadeVagas,
                    DescricaoTipoTurma = p.DivisaoTurma.Turma.TipoTurma.Descricao,
                    AssociacaoOfertaMatrizTipoTurma = p.DivisaoTurma.Turma.TipoTurma.AssociacaoOfertaMatriz,
                    DescricaoCicloLetivoInicio = p.DivisaoTurma.Turma.CicloLetivoInicio.Descricao,
                    DescricaoCicloLetivoFim = p.DivisaoTurma.Turma.CicloLetivoFim.Descricao,
                    SeqDivisaoTurma = p.SeqDivisaoTurma,
                    SeqConfiguracaoComponentePrincipal = p.SeqConfiguracaoComponente.Value,
                    DescricaoConfiguracaoComponente = p.ConfiguracaoComponente.Descricao,
                    DescricaoConfiguracaoComponenteTurma = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).Descricao,
                    DescricaoConfiguracaoComponenteTurmaPrincipal = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).Descricao,
                    Credito = (short?)p.ConfiguracaoComponente.ComponenteCurricular.Credito,
                    CreditoPrincipal = (short?)p.DivisaoTurma.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.Credito,
                    DescricaoLocalidadeAluno = p.DivisaoTurma.Turma.ConfiguracoesComponente.SelectMany(w => w.RestricoesTurmaMatriz).FirstOrDefault(r => r.SeqCursoOfertaLocalidadeTurno == dadosOrigem.SeqCursoOfertaLocalidadeTurno).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                    DescricaoLocalidadePrincipal = p.DivisaoTurma.Turma.ConfiguracoesComponente.SelectMany(w => w.RestricoesTurmaMatriz).FirstOrDefault(r => r.OfertaMatrizPrincipal).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                    DescricaoTurnoLocalidadeAluno = p.DivisaoTurma.Turma.ConfiguracoesComponente.SelectMany(w => w.RestricoesTurmaMatriz).FirstOrDefault(r => r.SeqCursoOfertaLocalidadeTurno == dadosOrigem.SeqCursoOfertaLocalidadeTurno).CursoOfertaLocalidadeTurno.Turno.Descricao,
                    DescricaoTurnoLocalidadePrincipal = p.DivisaoTurma.Turma.ConfiguracoesComponente.SelectMany(w => w.RestricoesTurmaMatriz).FirstOrDefault(r => r.OfertaMatrizPrincipal).CursoOfertaLocalidadeTurno.Turno.Descricao,
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
                        },
                    }).ToList(),
                    TurmaDivisoesItem = new TurmaDivisoesVO()
                    {
                        Seq = p.Seq,
                        SeqConfiguracaoComponente = p.SeqConfiguracaoComponente ?? 0,
                        SeqDivisaoComponente = (long?)p.DivisaoTurma.SeqDivisaoComponente ?? 0,
                        TipoDivisaoDescricao = p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.Descricao,
                        GerarOrientacao = (bool?)p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.GeraOrientacao ?? false,
                        SeqTipoOrientacao = p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.SeqTipoOrientacao,
                        Numero = (short?)p.DivisaoTurma.DivisaoComponente.Numero ?? 0,
                        CargaHoraria = p.DivisaoTurma.DivisaoComponente.CargaHoraria,
                        DescricaoComponenteCurricularOrganizacao = p.DivisaoTurma.DivisaoComponente.ComponenteCurricularOrganizacao.Descricao

                    },
                    /*TurmaDivisoesItemPrincipal = p.ConfiguracaoComponente.DivisoesComponente.Where(w => w.Seq == p.DivisaoTurma.SeqDivisaoComponente).Select(s => new TurmaDivisoesVO()
					{
						Seq = p.Seq,
						SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
						SeqDivisaoComponente = s.Seq,
						TipoDivisaoDescricao = s.TipoDivisaoComponente.Descricao,
						GerarOrientacao = s.TipoDivisaoComponente.GeraOrientacao,
						SeqTipoOrientacao = s.TipoDivisaoComponente.SeqTipoOrientacao,
						Numero = s.Numero,
						CargaHoraria = s.CargaHoraria,
					}).FirstOrDefault(),*/
                    SeqsConfiguracoesComponentes = p.DivisaoTurma.Turma.ConfiguracoesComponente.Select(s => s.SeqConfiguracaoComponente).ToList()
                }).ToList();

            //TurmaMatriculaListarVO
            var registros = new List<TurmaMatriculaListarVO>();
            dadosPlanoEstudo?.ForEach(d =>
            {
                var registro = SMCMapperHelper.Create<TurmaMatriculaListarVO>(d);
                registro.Credito = registro.Credito ?? d.CreditoPrincipal;
                registro.DescricaoConfiguracaoComponenteTurma = registro.DescricaoConfiguracaoComponenteTurma ?? d.DescricaoConfiguracaoComponenteTurmaPrincipal;
                registros.Add(registro);
            });

            foreach (var item in registros.Where(x => x.TurmaDivisoesItem != null))
            {
                var tiposComponenteNivel = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(item.SeqNivelEnsino, item.SeqTipoComponenteCurricular);

                item.TurmaMatriculaDivisoes = new List<TurmaMatriculaListarDetailVO>();

                var matriculaDivisao = new TurmaMatriculaListarDetailVO();

                if (tiposComponenteNivel != null)
                    item.TurmaDivisoesItem.FormatoCargaHoraria = tiposComponenteNivel.FormatoCargaHoraria;

                var divisaoItemDetalhe = item.DivisoesTurma.Where(w => w.SeqDivisaoComponente == item.TurmaDivisoesItem.SeqDivisaoComponente && w.NumeroGrupo == item.NumeroGrupo).FirstOrDefault();
                if (divisaoItemDetalhe != null)
                {
                    item.TurmaDivisoesItem.TurmaCodigoFormatado = item.CodigoFormatado;
                    item.TurmaDivisoesItem.NumeroDivisaoComponente = divisaoItemDetalhe.NumeroDivisaoComponente;
                    item.TurmaDivisoesItem.NumeroGrupo = divisaoItemDetalhe.NumeroGrupo;
                }

                matriculaDivisao.DivisaoTurmaDescricao = item.TurmaDivisoesItem.DescricaoFormatada;
                matriculaDivisao.DivisaoTurmaRelatorioDescricao = item.TurmaDivisoesItem.DivisaoTurmaRelatorioDescricao;
                matriculaDivisao.PermitirGrupo = item.TurmaDivisoesItem.PermitirGrupo;
                matriculaDivisao.SeqConfiguracaoComponente = item.TurmaDivisoesItem.SeqConfiguracaoComponente;
                matriculaDivisao.Seq = item.TurmaDivisoesItem.Seq;

                //matriculaDivisao.DivisoesTurmas = new List<SMCDatasourceItem>();
                //matriculaDivisao.DivisoesTurmas
                //	.AddRange(
                //	  item.DivisoesTurma.Where(w => w.SeqDivisaoComponente == item.TurmaDivisoesItem.SeqDivisaoComponente)
                //						.Select(s => new SMCDatasourceItem() { Seq = s.Seq, Descricao = $"{item.CodigoFormatado}.{s.NumeroDivisaoComponente.ToString()}.{s.NumeroGrupo.ToString().PadLeft(3, '0')} - {s.QuantidadeVagasDisponiveis} vagas disponíveis" })
                //	);

                if (item.SeqDivisaoTurma.HasValue)
                    matriculaDivisao.SeqDivisaoTurma = item.SeqDivisaoTurma;// matriculaDivisao.DivisoesTurmas[0].Seq;

                item.TurmaMatriculaDivisoes.Add(matriculaDivisao);
            }

            List<TurmaMatriculaListarVO> retorno = new List<TurmaMatriculaListarVO>();
            registros.GroupBy(g => g.Seq).SMCForEach(f =>
            {
                var primeiro = f.First();
                primeiro.TurmaMatriculaDivisoes = f.Where(k => k.TurmaMatriculaDivisoes != null).SelectMany(s => s.TurmaMatriculaDivisoes)?.OrderBy(o => o.DivisaoTurmaDescricao).ToList();
                retorno.Add(primeiro);
            });

            return new SMCPagerData<TurmaMatriculaListarVO>(retorno.OrderBy(o => o.DescricaoConfiguracaoComponente).ToList());
        }

        /// <summary>
        /// Buscar itens do plano de estudo atual por aluno
        /// </summary>
        /// <param name="seqAluno"></param>
        /// <param name="seqCicloLetivo"></param>
        /// <returns>Retorna SeqDivisaoTurma e SeqConfiguracaoComponente dos itens do plano</returns>
        public List<PlanoEstudoItemVO> BuscarSeqsItensPlanoEstudoAtualAluno(long seqAluno, long? seqCicloLetivo)
        {
            //Buscar os dados de origem
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            var spec = new PlanoEstudoItemFilterSpecification() { SeqAluno = seqAluno, SeqCicloLetivo = seqCicloLetivo, PlanoEstudoAtual = true };

            var dadosPlanoEstudo = this.SearchProjectionBySpecification(spec,
                p => new PlanoEstudoItemVO
                {
                    SeqTurma = p.DivisaoTurma.SeqTurma,
                    SeqDivisaoTurma = p.SeqDivisaoTurma,
                    SeqComponenteCurricular = p.ConfiguracaoComponente.SeqComponenteCurricular,
                    SeqConfiguracaoComponente = p.SeqConfiguracaoComponente.Value
                }).ToList();

            return dadosPlanoEstudo.ToList();
        }

        public List<PlanoEstudoItemVO> BuscarSolicitacaoMatriculaAtividadesItens(long seqAluno, long? seqCicloLetivo)
        {
            var spec = new PlanoEstudoItemFilterSpecification() { SeqAluno = seqAluno, SeqCicloLetivo = seqCicloLetivo, SomenteTurma = false, PlanoEstudoAtual = true };

            var registros = this.SearchProjectionBySpecification(spec, p => new PlanoEstudoItemVO
            {
                CicloLetivo = p.PlanoEstudo.AlunoHistoricoCicloLetivo.CicloLetivo.Descricao,
                SeqConfiguracaoComponente = p.SeqConfiguracaoComponente
            }).ToList();

            if (registros.Count > 0)
            {
                var filtro = new ConfiguracaoComponenteFilterSpecification() { SeqConfiguracoesComponentes = registros.Select(s => s.SeqConfiguracaoComponente).ToArray() };
                filtro.MaxResults = int.MaxValue;
                filtro.SetOrderBy(s => s.Descricao);

                var listaRegistro = ConfiguracaoComponenteDomainService.BuscarConfiguracoesComponentes(filtro, true);

                foreach (var item in registros)
                {
                    var registroValida = listaRegistro.First(w => w.Seq == item.SeqConfiguracaoComponente);

                    item.DescricaoFormatada = registroValida.DescricaoFormatada;

                    item.DescricaoOrdenacao = registroValida.Descricao;

                    item.Credito = registroValida.ComponenteCurricularCredito;
                }
            }

            return registros.OrderBy(o => o.DescricaoOrdenacao).ToList();
        }

        /// <summary>
        /// Buscar todas os Planos de estudos de uma divisao de turma
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial de uma divisão de turma</param>
        /// <returns>Todas os itens de um plano de estudo por divisao de turma</returns>
        public List<PlanoEstudoItem> BuscarAlunosPlanoEstudoItemPorDivisaoTurma(PlanoEstudoItemFilterSpecification filtros)
        {
            //var spec = new PlanoEstudoItemFilterSpecification() { SeqDivisaoTurma = seqDivisaoTurma, PlanoEstudoAtual = true };

            var includes = IncludesPlanoEstudoItem.DivisaoTurma_Turma_ConfiguracoesComponente
                           | IncludesPlanoEstudoItem.Orientacao
                           | IncludesPlanoEstudoItem.Orientacao_OrientacoesColaborador
                           | IncludesPlanoEstudoItem.Orientacao_OrientacoesColaborador_Colaborador
                           | IncludesPlanoEstudoItem.Orientacao_OrientacoesColaborador_Colaborador_DadosPessoais
                           | IncludesPlanoEstudoItem.PlanoEstudo
                           | IncludesPlanoEstudoItem.PlanoEstudo_AlunoHistoricoCicloLetivo
                           | IncludesPlanoEstudoItem.PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico
                           | IncludesPlanoEstudoItem.PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_Aluno
                           | IncludesPlanoEstudoItem.PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_Aluno_DadosPessoais
                           | IncludesPlanoEstudoItem.PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_CursoOfertaLocalidadeTurno
                           | IncludesPlanoEstudoItem.PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade
                           | IncludesPlanoEstudoItem.PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades
                           | IncludesPlanoEstudoItem.PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades_ItemSuperior;

            var retorno = this.SearchBySpecification(filtros, includes).ToList();

            return retorno;
        }

        /// <summary>
        /// Buscar turmas dos componentes currículares do plano de estudo item para listar na integralização.
        /// </summary>
        /// <param name="seqsAlunoHistorico">Lista de sequenciais do aluno historico atual</param>
        /// <param name="seqsCicloLetivo">Lista de sequencial do ciclo letivo do historico escolar</param>
        /// <returns>Lista dos itens com códigos da turma</returns>
        public List<HistoricoEscolarTurmaVO> BuscarIntegralizacaoComponentePlanoEstudoTurma(List<long> seqsAlunoHistorico, List<long> seqsCicloLetivo)
        {
            var spec = new PlanoEstudoItemFilterSpecification() { SeqsAlunosHistoricos = seqsAlunoHistorico, SeqsCicloLetivos = seqsCicloLetivo, PlanoEstudoAtual = true };

            var dadosPlanoEstudoItem = this.SearchProjectionBySpecification(spec, p => new
            {
                SeqComponenteCurricular = (long?)p.ConfiguracaoComponente.SeqComponenteCurricular,
                SeqDivisaoTurma = (long?)p.SeqDivisaoTurma,
                SeqTurma = (long?)p.DivisaoTurma.SeqTurma,
                CodigoTurma = (int?)p.DivisaoTurma.Turma.Codigo,
                NumeroTurma = (short?)p.DivisaoTurma.Turma.Numero,
                GeraOrientacao = (bool?)p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.GeraOrientacao,
                SeqOrientacao = (long?)p.SeqOrientacao,
                ColaboradoresOrientacao = p.Orientacao.OrientacoesColaborador.Select(s => s.Colaborador.DadosPessoais).ToList(),
                ColaboradoresBanco = p.DivisaoTurma.Colaboradores.Select(s => s.Colaborador.DadosPessoais).ToList(),

                SeqComponenteCurricularAssunto = (long?)p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Seq == p.SeqConfiguracaoComponente)
                                                                       .RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal)
                                                                       .SeqComponenteCurricularAssunto,
                SeqComponenteCurricularAssuntoPrincipal = (long?)p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal)
                                                                       .RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal)
                                                                       .SeqComponenteCurricularAssunto,
            }).ToList();

            var registros = new List<HistoricoEscolarTurmaVO>();
            dadosPlanoEstudoItem?.ForEach(d =>
            {
                var item = SMCMapperHelper.Create<HistoricoEscolarTurmaVO>(d);
                item.SeqComponenteCurricularAssunto = item.SeqComponenteCurricularAssunto ?? d.SeqComponenteCurricularAssuntoPrincipal;

                registros.Add(item);
            });

            registros.SMCForEach(f =>
            {
                ///Se o tipo da divisão da configuração da divisão da turma em questão estiver marcada para gerar orientação, verificar se a pessoa - atuação foi passada como parâmetro.
                if (f.GeraOrientacao == true)
                {
                    ///Caso não exista orientação ou não exista item do plano de estudos para a divisão, não exibir colaborador.
                    if (f.SeqOrientacao > 0)
                    {
                        if (f.ColaboradoresOrientacao != null && f.ColaboradoresOrientacao.Count > 0)
                        {
                            f.ColaboradoresBanco = f.ColaboradoresOrientacao;
                        }
                    }
                }

                if (f.ColaboradoresBanco != null && f.ColaboradoresBanco.Count > 0)
                {
                    f.Professores = new List<string>();
                    f.ColaboradoresBanco.SMCForEach(b =>
                    {
                        var nomeCompleto = string.IsNullOrEmpty(b.NomeSocial) ? b.Nome : $"{b.NomeSocial} ({b.Nome})";

                        // RN_PES_023 - Nome e Nome Social - Visão Administrativo
                        f.Professores.Add(nomeCompleto);
                    });
                }
            });

            return registros;
        }

        /// <summary>
        /// Buscar todas orientações dos planos de estudos de uma divisao de turma atual
        /// </summary>
        /// <param name="seqPlanoEstudoAtual">Sequencial do plano estudo atual</param>
        /// <param name="seqDivisaoTurma">Sequencial da divisao da turma no plano de estudo</param>
        /// <returns>Todas os itens de um plano de estudo atual do tipo turma com os orientadores</returns>
        public List<PlanoEstudoItem> BuscarPlanoEstudoItemOrientacoesTurmas(long? seqPlanoEstudoAtual, long? seqDivisaoTurma)
        {
            var spec = new PlanoEstudoItemFilterSpecification()
            {
                SeqPlanoEstudo = seqPlanoEstudoAtual,
                SeqDivisaoTurma = seqDivisaoTurma,
                OrientacaoTurma = true,
                PlanoEstudoAtual = true,
                SomenteTurma = true
            };

            var includes = IncludesPlanoEstudoItem.Orientacao
                           | IncludesPlanoEstudoItem.Orientacao_OrientacoesColaborador;

            var retorno = this.SearchBySpecification(spec, includes).ToList();

            return retorno;
        }

        /// <summary>
        /// Busca o plano de estudo item da configuração em curso para montar a modal de histórico escolar
        /// </summary>
        /// <param name="seqPlanoEstudo">Sequencial do plano de estudo em curso</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração de componente</param>
        /// <returns>Dados do plano de estudo item do aluno</returns>
        public List<HistoricoEscolarListaVO> BuscarPlanoEstudoItemIntegralizacaoConfiguracao(long seqPlanoEstudo, long? seqComponenteCurricular, long? seqConfiguracaoComponente)
        {
            PlanoEstudoItemFilterSpecification spec = new PlanoEstudoItemFilterSpecification();
            spec.SeqPlanoEstudo = seqPlanoEstudo;
            spec.SeqComponenteCurricular = seqComponenteCurricular;
            spec.SeqConfiguracaoComponente = seqConfiguracaoComponente;
            spec.SetOrderByDescending(o => o.Seq);
            spec.MaxResults = int.MaxValue;

            var planoestudo = SearchProjectionBySpecification(spec, p => new HistoricoEscolarListaVO()
            {
                Seq = p.Seq,
                SeqAlunoHistorico = p.PlanoEstudo.AlunoHistoricoCicloLetivo.SeqAlunoHistorico,
                SeqCicloLetivo = p.PlanoEstudo.AlunoHistoricoCicloLetivo.SeqCicloLetivo,
                SeqComponenteCurricular = p.ConfiguracaoComponente.SeqComponenteCurricular,
                SeqComponenteCurricularAssunto = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqComponenteCurricularAssunto,
                CodigoComponenteCurricular = p.ConfiguracaoComponente.ComponenteCurricular.Codigo,
                DescricaoComponenteCurricular = p.ConfiguracaoComponente.ComponenteCurricular.Descricao,
                SiglaComponente = p.ConfiguracaoComponente.ComponenteCurricular.TipoComponente.Sigla,
                DescricaoAssunto = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).ComponenteCurricularAssunto.Descricao,
                DescricaoCicloLetivo = p.SeqDivisaoTurma.HasValue ? p.DivisaoTurma.Turma.CicloLetivoInicio.Descricao : p.PlanoEstudo.AlunoHistoricoCicloLetivo.CicloLetivo.Descricao,
                DescricaoConceito = p.DivisaoTurma.OrigemAvaliacao.EscalaApuracao.Descricao,
            }).ToList();

            return planoestudo;
        }

        /// <summary>
        /// Busca no plano de estudo do aluno os itens de turma (divisão de turma) em um período
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="dataInicio">Data de inicio</param>
        /// <param name="dataFim">Data de fim</param>
        /// <returns>Lista de divisões de turma que o aluno está matrículado no período</returns>
        public virtual List<PlanoEstudoItemTurmasAlunoVO> BuscarTurmasAlunoNoPeriodo(long seqAluno, DateTime dataInicio, DateTime? dataFim)
        {
            // Busca os dados de origem da pessoa atuação
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            //Se por ventura não houver uma data fim de periodo ele pegará o ciclo atual
            if (!dataFim.HasValue)
            {
                dataFim = DateTime.Now.Date;
            }

            // Recupera o ciclo letivo na data de inicio/fim informada
            long seqCicloInicio = ConfiguracaoEventoLetivoDomainService.BuscarSeqCicloLetivo(dataInicio, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
            long seqCicloFim = ConfiguracaoEventoLetivoDomainService.BuscarSeqCicloLetivo(dataFim.Value, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            // Monta a lista de ciclos entre o inicio e o fim do periodo
            List<long> listaCiclos = new List<long>();
            listaCiclos.Add(seqCicloInicio);
            if (seqCicloInicio != seqCicloFim)
            {
                long? proximoCiclo = CicloLetivoDomainService.BuscarProximoCicloLetivo(seqCicloInicio);
                if (proximoCiclo.HasValue)
                    listaCiclos.Add(proximoCiclo.Value);
                while (proximoCiclo.HasValue && proximoCiclo.Value != seqCicloFim)
                {
                    proximoCiclo = CicloLetivoDomainService.BuscarProximoCicloLetivo(proximoCiclo.Value);
                    if (proximoCiclo.HasValue)
                        listaCiclos.Add(proximoCiclo.Value);
                }
            }

            // Specification para pesquisa
            var spec = new PlanoEstudoItemFilterSpecification()
            {
                SeqAluno = seqAluno,
                SeqsCicloLetivos = listaCiclos,
                PlanoEstudoAtual = true,
                SomenteTurma = true
            };

            // Busca as informações
            var turmas = this.SearchProjectionBySpecification(spec, p => new PlanoEstudoItemTurmasAlunoVO
            {
                SeqDivisaoTurma = p.SeqDivisaoTurma,
                SeqAlunoHistoricoCicloLetivo = p.PlanoEstudo.SeqAlunoHistoricoCicloLetivo
            }).ToList();

            return turmas;

        }

        /// <summary>
        /// Buscar componentes do plano de estudo atual do aluno
        /// </summary>
        /// <param name="seqAluno"></param>
        /// <param name="seqCicloLetivo"></param>
        /// <returns>Retorna item1 = seqComponenteCurricular, item2 seqComponenteCurricularAssunto</returns>
        public List<Tuple<long, long?>> BuscarSeqsComponenteCurricularItensPlanoEstudoAtualAluno(long seqAluno, long? seqCicloLetivo)
        {
            //Buscar os dados de origem
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            var spec = new PlanoEstudoItemFilterSpecification() { SeqAluno = seqAluno, SeqCicloLetivo = seqCicloLetivo, PlanoEstudoAtual = true };
            List<Tuple<long, long?>> retorno = new List<Tuple<long, long?>>();
            var dadosPlanoEstudo = this.SearchProjectionBySpecification(spec, p => new
            {
                SeqComponenteCurricular = p.ConfiguracaoComponente.SeqComponenteCurricular,
                SeqComponenteCurricularAssunto = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqComponenteCurricularAssunto
            }).ToList();
            dadosPlanoEstudo.SMCForEach(s => retorno.Add(new Tuple<long, long?>(s.SeqComponenteCurricular, s.SeqComponenteCurricularAssunto))).ToList();
            return retorno;
        }
    }
}