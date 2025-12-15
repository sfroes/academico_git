using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Common.Areas.MAT.Includes;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Resources;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Repositories;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class RequisitoDomainService : AcademicoContextDomain<Requisito>
    {
        #region [ DomainService ]

        private AlunoFormacaoDomainService AlunoFormacaoDomainService => Create<AlunoFormacaoDomainService>();

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();

        private DivisaoComponenteDomainService DivisaoComponenteDomainService => Create<DivisaoComponenteDomainService>();

        private ComponenteCurricularDomainService ComponenteCurricularDomainService => Create<ComponenteCurricularDomainService>();

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();

        private DivisaoCurricularItemDomainService DivisaoCurricularItemDomainService => Create<DivisaoCurricularItemDomainService>();

        private DivisaoMatrizCurricularComponenteDomainService DivisaoMatrizCurricularComponenteDomainService => Create<DivisaoMatrizCurricularComponenteDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private MatrizCurricularDomainService MatrizCurricularDomainService => Create<MatrizCurricularDomainService>();

        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => Create<MatrizCurricularOfertaDomainService>();

        private PessoaAtuacaoBeneficioDomainService PessoaAtuacaoBeneficioDomainService => Create<PessoaAtuacaoBeneficioDomainService>();

        private PessoaAtuacaoCondicaoObrigatoriedadeDomainService PessoaAtuacaoCondicaoObrigatoriedadeDomainService => Create<PessoaAtuacaoCondicaoObrigatoriedadeDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => Create<SolicitacaoMatriculaItemDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private TurmaConfiguracaoComponenteDomainService TurmaConfiguracaoComponenteDomainService => Create<TurmaConfiguracaoComponenteDomainService>();

        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService => Create<CurriculoCursoOfertaDomainService>();

        private GrupoCurricularDomainService GrupoCurricularDomainService => Create<GrupoCurricularDomainService>();

        private IAcademicoRepository AcademicoRepository => this.Create<IAcademicoRepository>();

        #endregion [ DomainService ]

        /// <summary>
        /// Verificar se o(s) componente(s) selecionados possui(em) pré-Requisito(s) atendidos(s) de acordo com a matriz curricular do aluno em questão
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqsDivisoesComponente">Sequencial das divisões a serem verificadas</param>
        /// <param name="seqsConfiguracaoComponente">Sequencial das configurações a serem verificadas</param>
        /// <param name="validaTipoGestao">Se é para validar o tipo de gestão e qual o tipo </param>
        /// <param name="seqSolicitacao">Para validar o có-requisito informar o seqSolicitacao</param>
        public (bool Valido, List<string> MensagensErro) ValidarPreRequisitos(long seqAluno, IEnumerable<long> seqsDivisoesComponente, IEnumerable<long> seqsConfiguracaoComponente, TipoGestaoDivisaoComponente? validaTipoGestao, long? seqSolicitacao, long? SeqProcessoEtapa = null)
        {
            if (seqAluno == 0 ||
                ((seqsDivisoesComponente == null || !seqsDivisoesComponente.Any()) && (seqsConfiguracaoComponente == null || !seqsConfiguracaoComponente.Any())))
                return (true, new List<string>());

            //Recuperar os dados de origem da pessoa atuação com a matriz curricular oferta
            var dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqAluno);

            if (!dadosVinculo.ExigeOfertaMatrizCurricular.GetValueOrDefault())
                return (true, new List<string>());

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            var matrizRequisitos = MatrizCurricularOfertaDomainService.SearchByKey(new SMCSeqSpecification<MatrizCurricularOferta>(dadosOrigem.SeqMatrizCurricularOferta),
                                                                                       IncludesMatrizCurricularOferta.MatrizCurricular_Requisitos_Requisito_Itens_ComponenteCurricular_Configuracoes
                                                                                     | IncludesMatrizCurricularOferta.MatrizCurricular_Requisitos_Requisito_Itens_ComponenteCurricular_Configuracoes_DivisoesComponente_TipoDivisaoComponente
                                                                                     | IncludesMatrizCurricularOferta.MatrizCurricular_Requisitos_Requisito_Itens_ComponenteCurricular_NiveisEnsino
                                                                                     | IncludesMatrizCurricularOferta.MatrizCurricular_Requisitos_Requisito_ComponenteCurricular_Configuracoes
                                                                                     | IncludesMatrizCurricularOferta.MatrizCurricular_Requisitos_Requisito_ComponenteCurricular_Configuracoes_DivisoesComponente_TipoDivisaoComponente
                                                                                     | IncludesMatrizCurricularOferta.MatrizCurricular_Requisitos_Requisito_ComponenteCurricular_NiveisEnsino);

            if (matrizRequisitos == null)
                return (true, new List<string>());

            var requisitos = new List<Requisito>();
            var seqsComponentesCurriculares = new List<long>();
            if (seqsDivisoesComponente != null && seqsDivisoesComponente.Any())
            {
                var specDivisaoComponente = new DivisaoComponenteFilterSpecification() { Seqs = seqsDivisoesComponente.ToArray() };

                seqsComponentesCurriculares = DivisaoComponenteDomainService.SearchProjectionBySpecification(specDivisaoComponente, d => d.ConfiguracaoComponente.SeqComponenteCurricular).ToList();

                //Recupera os requisitos apenas dos itens selecionados pelas divisões
                requisitos = matrizRequisitos.MatrizCurricular.Requisitos
                                                              .Where(w => seqsComponentesCurriculares.Any(s => s == w.Requisito.SeqComponenteCurricular)
                                                              || seqsComponentesCurriculares.Any(s => w.Requisito.Itens.Any(i => i.SeqComponenteCurricular == s && i.TipoRequisito == TipoRequisito.CoRequisito)))
                                                              .Select(s => s.Requisito).ToList();
            }
            else if (seqsConfiguracaoComponente != null && seqsConfiguracaoComponente.Any())
            {
                var specConfiguracao = new ConfiguracaoComponenteFilterSpecification() { SeqConfiguracoesComponentes = seqsConfiguracaoComponente.Select(s => (long?)s).ToArray() };
                seqsComponentesCurriculares = ConfiguracaoComponenteDomainService.SearchProjectionBySpecification(specConfiguracao, s => s.SeqComponenteCurricular).ToList();

                //Recupera os requisitos apenas dos itens selecionados pelas configurações
                requisitos = matrizRequisitos.MatrizCurricular.Requisitos
                                                              //.Where(w => w.Requisito.ComponenteCurricular.Configuracoes.Any(a => seqsConfiguracaoComponente.Contains(a.Seq)))
                                                              .Where(w => seqsComponentesCurriculares.Any(s => s == w.Requisito.SeqComponenteCurricular)
                                                              || seqsComponentesCurriculares.Any(s => w.Requisito.Itens.Any(i => i.SeqComponenteCurricular == s && i.TipoRequisito == TipoRequisito.CoRequisito)))
                                                              .Select(s => s.Requisito).ToList();
            }


            //Verificar grupos a serem desconsiderados pela formação ou benefício
            requisitos = ValidarGrupoRequisitoFormacaoBeneficio(requisitos, seqAluno, dadosOrigem.TipoAtuacao, seqSolicitacao);

            //Verifica se algum item de requisito possui pré requisito
            requisitos = requisitos.OrderBy(o => o.ComponenteCurricular.Descricao).ToList();

            return ValidarRequisitos(requisitos, seqsComponentesCurriculares, seqAluno, validaTipoGestao, seqSolicitacao, SeqProcessoEtapa);
        }

        /// <summary>
        /// Verificar se deve desconsiderar um grupo de acordo com a formação ou com o benefício
        /// </summary>
        /// <param name="requisitos">Lista de requisitos</param>
        /// <param name="seqAluno">Sequencial Pessoa Atuação</param>
        /// <param name="tipoAtuacao">Tipo Atuação</param>
        /// <returns></returns>
        public List<Requisito> ValidarGrupoRequisitoFormacaoBeneficio(List<Requisito> requisitos, long seqAluno, TipoAtuacao tipoAtuacao, long? seqSolicitacaoMatricula)
        {
            // Recupera o plano de estudo atual - no caso de alteração de plano para não validar o que ja esta no plano de estudo como não alterado
            if (seqSolicitacaoMatricula.HasValue)
            {
                var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
                {
                    SeqSolicitacaoMatricula = seqSolicitacaoMatricula.Value,
                    ClassificacaoSituacoesFinais = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.NaoAlterado },
                };

                var itensPlano = SolicitacaoMatriculaItemDomainService.SearchProjectionBySpecification(specSolicitacaoMatriculaItem, x =>
                                                                        x.ConfiguracaoComponente.SeqComponenteCurricular).ToList();

                if (itensPlano.Count() > 0)
                {
                    requisitos = requisitos.Where(w => !itensPlano.Contains(w.SeqComponenteCurricular.Value) || !itensPlano.Any(i => w.Itens.Any(a => a.SeqComponenteCurricular == i))).ToList();
                }
            }

            List<long> seqsFormacaoEspecificaGrupo = new List<long>();
            if (requisitos.Any(a => a.Itens.Any(i => i.TipoRequisitoItem == TipoRequisitoItem.GrupoCurricular)))
            {
                if (tipoAtuacao == TipoAtuacao.Ingressante)
                {
                    var dadosIngressanteFormacao = this.IngressanteDomainService.BuscarAssociacaoFormacoesEspecificasIngressante(seqAluno);
                    if (dadosIngressanteFormacao != null && dadosIngressanteFormacao.FormacoesEspecificas.Count() > 0)
                    {
                        if (dadosIngressanteFormacao.FormacoesEspecificas.First().Hierarquia.Count() > 0)
                        {
                            seqsFormacaoEspecificaGrupo.AddRange(dadosIngressanteFormacao.FormacoesEspecificas.First().Hierarquia.Select(s => s.Seq).ToList());
                        }
                        else
                        {
                            seqsFormacaoEspecificaGrupo.AddRange(dadosIngressanteFormacao.FormacoesEspecificas.Select(s => s.Seq).ToList());
                        }
                    }
                }
                else
                {
                    //Desconsidera Tipo Requisito Grupo quando tiver Formação Academica diferente do Aluno
                    var dadosAlunoFormacao = this.AlunoFormacaoDomainService.BuscarAssociacaoFormacaoEspecifica(seqAluno);
                    if (dadosAlunoFormacao != null && dadosAlunoFormacao.FormacoesEspecificas.Count() > 0)
                    {
                        if (dadosAlunoFormacao.FormacoesEspecificas.First().Hierarquia.Count() > 0)
                        {
                            seqsFormacaoEspecificaGrupo.AddRange(dadosAlunoFormacao.FormacoesEspecificas.First().Hierarquia.Select(s => s.Seq).ToList());
                        }
                        else
                        {
                            seqsFormacaoEspecificaGrupo.AddRange(dadosAlunoFormacao.FormacoesEspecificas.Select(s => s.Seq).ToList());
                        }
                    }
                }

                //Desconsidera Tipo Requisito Grupo quando tiver Beneficios diferente do Aluno
                var dadosAlunoBeneficios = this.PessoaAtuacaoBeneficioDomainService.BuscarPesssoasAtuacoesBeneficiosMatricula(seqAluno).Select(s => s.SeqBeneficio).ToList();
                var dadosAlunoCondicao = this.PessoaAtuacaoCondicaoObrigatoriedadeDomainService.BuscarSequenciaisCondicaoObrigatoriedadePessoaAtuacao(seqAluno);
                requisitos.ForEach(f =>
                {
                    List<long> seqsGrupoFormacao = new List<long>();
                    var grupoCurricular = f.Itens.Where(w => w.TipoRequisitoItem == TipoRequisitoItem.GrupoCurricular);
                    grupoCurricular.ToList().ForEach(g =>
                    {
                        seqsGrupoFormacao.Add(g.Seq);

                        var registroGrupoCurricular = this.GrupoCurricularDomainService.SearchProjectionByKey(new SMCSeqSpecification<GrupoCurricular>(g.SeqGrupoCurricular.Value),
                            x => new
                            {
                                x.SeqFormacaoEspecifica,
                                SeqBeneficios = x.Beneficios.Select(s => s.Seq).ToList(),
                                SeqCondicoes = x.CondicoesObrigatoriedade.Select(s => s.Seq).ToList()
                            });

                        if (registroGrupoCurricular.SeqFormacaoEspecifica != null && !seqsFormacaoEspecificaGrupo.Contains(registroGrupoCurricular.SeqFormacaoEspecifica.Value))
                            seqsGrupoFormacao.Remove(g.Seq);

                        if (registroGrupoCurricular.SeqBeneficios.Count() > 0 && !dadosAlunoBeneficios.Any(a => registroGrupoCurricular.SeqBeneficios.Contains(a)))
                            seqsGrupoFormacao.Remove(g.Seq);

                        if (registroGrupoCurricular.SeqCondicoes.Count() > 0 && !dadosAlunoCondicao.Any(a => registroGrupoCurricular.SeqCondicoes.Contains(a)))
                            seqsGrupoFormacao.Remove(g.Seq);


                        //if (registroGrupoCurricular.SeqFormacaoEspecifica != null && seqFormacaoEspecificaGrupo == registroGrupoCurricular.SeqFormacaoEspecifica)
                        //    seqsGrupoFormacao.Add(g.Seq);

                        //if (registroGrupoCurricular.SeqBeneficios.Count() > 0 && dadosAlunoBeneficios.Any(a => registroGrupoCurricular.SeqBeneficios.Contains(a)))
                        //    seqsGrupoFormacao.Add(g.Seq);

                        //if (registroGrupoCurricular.SeqCondicoes.Count() > 0 && dadosAlunoCondicao.Any(a => registroGrupoCurricular.SeqCondicoes.Contains(a)))
                        //    seqsGrupoFormacao.Add(g.Seq);

                    });

                    f.Itens = f.Itens.Where(w => w.TipoRequisitoItem != TipoRequisitoItem.GrupoCurricular || seqsGrupoFormacao.Contains(w.Seq)).ToList();
                });
            }

            return requisitos;
        }

        /// <summary>
        /// Verificar se o(s) requisito(s) foi(ram) atendidos(s) pelo aluno
        /// </summary>
        /// <param name="requisitos">Requisitos a serem validados</param>
        /// <param name="seqsComponentesCurriculares">Componentes selecionado na modal</param>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="validaTipoGestao">Informado quando é para validar o tipo de gestão</param>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matrícula para validar co-requisito</param>
        private (bool Valido, List<string> MensagensErro) ValidarRequisitos(List<Requisito> requisitos, List<long> seqsComponentesCurriculares, long seqAluno, TipoGestaoDivisaoComponente? validaTipoGestao, long? seqSolicitacaoMatricula, long? SeqProcessoEtapa = null)
        {
            // Mensagem de retorno
            var mensagemErro = new List<string>();

            // Não tem requisitos
            if (requisitos == null || requisitos.Count == 0)
                return (true, mensagemErro);

            // Recupera os dados de origem do aluno
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            // Ingressante não cumpriu nada de pré requisito
            if (dadosOrigem.TipoAtuacao == TipoAtuacao.Ingressante)
            {
                if (requisitos.Any(a => a.Itens.Any(i => i.TipoRequisito == TipoRequisito.PreRequisito)))
                {
                    RequisitoPreECoMensagem(mensagemErro, seqAluno, requisitos);
                    return (false, mensagemErro);
                }
            }

            // Itens da solicitação de serviço (caso seja informada)
            List<long> seqsComponentesCurricularesSolicitacao = new List<long>();
            List<long> seqsComponentesCurricularesAlteracao = new List<long>();

            // Caso tenha solicitação de matrícula informada, carrega os itens da solicitação
            if (seqSolicitacaoMatricula.HasValue)
            {
                var specSolicitacaoMatriculaItem = new SolicitacaoMatriculaItemFilterSpecification()
                {
                    SeqSolicitacaoMatricula = seqSolicitacaoMatricula.Value,
                    ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.FinalizadoComSucesso,
                    PertencePlanoEstudo = false,
                };

                seqsComponentesCurricularesSolicitacao = SolicitacaoMatriculaItemDomainService.SearchProjectionBySpecification(specSolicitacaoMatriculaItem, x =>
                                                                        x.ConfiguracaoComponente.SeqComponenteCurricular).ToList();

                var specSolicitacaoMatriculaItemAlteracao = new SolicitacaoMatriculaItemFilterSpecification()
                {
                    SeqSolicitacaoMatricula = seqSolicitacaoMatricula.Value,
                    ClassificacaoSituacoesFinais = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.FinalizadoSemSucesso, ClassificacaoSituacaoFinal.NaoAlterado },
                    PertencePlanoEstudo = true,
                };

                seqsComponentesCurricularesAlteracao = SolicitacaoMatriculaItemDomainService.SearchProjectionBySpecification(specSolicitacaoMatriculaItemAlteracao, x => x.ConfiguracaoComponente.SeqComponenteCurricular).ToList();
                seqsComponentesCurricularesSolicitacao.AddRange(seqsComponentesCurricularesAlteracao);

                if (dadosOrigem.TipoAtuacao == TipoAtuacao.Ingressante)
                    seqsComponentesCurricularesSolicitacao.AddRange(seqsComponentesCurriculares);

            }

            // Recupera o histórico de componentes cursados pelo aluno com situação aprovado ou dispensado
            var specHistorico = new HistoricoEscolarFilterSpecification()
            {
                SeqAluno = seqAluno,
                SeqCursoOfertaLocalidadeTurno = dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                SituacoesHistoricoEscolar = new SituacaoHistoricoEscolar[] { SituacaoHistoricoEscolar.Aprovado, SituacaoHistoricoEscolar.Dispensado }
            };
            var historicos = HistoricoEscolarDomainService.SearchBySpecification(specHistorico).ToList();

            bool requisitoAtendido = false;
            int countRequisito = 0;
            int countRequisitoAtendido = 0;
            // Para cada componente curricular, verificar se atende a algum grupo de requisitos
            foreach (var requisitosComponente in requisitos.GroupBy(r => r.SeqComponenteCurricular))
            {
                //Inicia o requisito como não atendido
                requisitoAtendido = false;

                //Verificar se todos o requisitos foram atendidos com pelo menos um item
                countRequisito++;

                // Verificar se pelo - um requisito foi completo para cada componente
                foreach (var requisito in requisitosComponente)
                {
                    // Verifica os itens de co-requisito caso tenha sido informada seq da solicitação de serviço
                    if (seqSolicitacaoMatricula.HasValue)
                    {
                        var coRequisitos = new List<RequisitoItem>();

                        if (validaTipoGestao.HasValue)
                        {
                            if (validaTipoGestao.Value == TipoGestaoDivisaoComponente.Turma &&
                                requisito.ComponenteCurricular.Configuracoes.SelectMany(s => s.DivisoesComponente).Any(a => a.TipoDivisaoComponente.TipoGestaoDivisaoComponente == TipoGestaoDivisaoComponente.Turma))
                            {
                                coRequisitos = requisito.Itens.Where(a => a.TipoRequisito == TipoRequisito.CoRequisito &&
                                                                          a.SeqComponenteCurricular != null &&
                                                                          a.ComponenteCurricular.Configuracoes.SelectMany(s => s.DivisoesComponente).Any(g => g.TipoDivisaoComponente.TipoGestaoDivisaoComponente == TipoGestaoDivisaoComponente.Turma)).ToList();
                            }
                            else if (validaTipoGestao.Value != TipoGestaoDivisaoComponente.Turma && requisito.ComponenteCurricular.Configuracoes.SelectMany(s => s.DivisoesComponente).Any(a => a.TipoDivisaoComponente.TipoGestaoDivisaoComponente != TipoGestaoDivisaoComponente.Turma))
                            {
                                coRequisitos = requisito.Itens.Where(a => a.TipoRequisito == TipoRequisito.CoRequisito &&
                                                                          a.SeqComponenteCurricular != null &&
                                                                          a.ComponenteCurricular.Configuracoes.SelectMany(s => s.DivisoesComponente).Any(g => g.TipoDivisaoComponente.TipoGestaoDivisaoComponente != TipoGestaoDivisaoComponente.Turma)).ToList();
                            }

                            var specSolicitacaoMatriculaItemAtividade = new SolicitacaoMatriculaItemFilterSpecification()
                            {
                                SeqSolicitacaoMatricula = seqSolicitacaoMatricula.Value,
                                ClassificacaoSituacoesFinais = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.FinalizadoComSucesso, ClassificacaoSituacaoFinal.NaoAlterado },
                                RegistroAtividade = true
                            };

                            var seqsComponentesCurricularesAtividade = SolicitacaoMatriculaItemDomainService.SearchProjectionBySpecification(specSolicitacaoMatriculaItemAtividade, x =>
                                                                                    x.ConfiguracaoComponente.SeqComponenteCurricular).ToList();

                            if (validaTipoGestao.Value == TipoGestaoDivisaoComponente.Turma &&
                                seqsComponentesCurricularesAtividade.Contains(requisito.SeqComponenteCurricular.GetValueOrDefault()))
                            {
                                requisitoAtendido = true;
                                countRequisitoAtendido++;
                                continue;
                            }
                        }
                        else
                        {
                            coRequisitos = requisito.Itens.Where(a => a.TipoRequisito == TipoRequisito.CoRequisito && a.TipoRequisitoItem == TipoRequisitoItem.ComponenteCurricular).ToList();
                        }

                        if (coRequisitos.Any())
                        {
                            // Verifica se algum componente de có requisito não está no histórico do aluno
                            var coRrequisitoSemHistorico = coRequisitos.Where(pr => !historicos.Any(h => h.SeqComponenteCurricular == pr.SeqComponenteCurricular));
                            if (coRrequisitoSemHistorico.Any())
                            {
                                // Verifica se foi informado solicitação de matrícula
                                if (seqSolicitacaoMatricula.HasValue)
                                {
                                    // Verifica nos itens da solicitação se possui o item co-requisito
                                    bool coRequisitoSemItemSolicitacaoMatricula = false;

                                    var componentePrincipal = requisito.SeqComponenteCurricular;

                                    if (seqsComponentesCurriculares.Contains(componentePrincipal.GetValueOrDefault()))
                                    {
                                        var seqscoRrequisito = coRrequisitoSemHistorico.Select(s => s.SeqComponenteCurricular);
                                        var correquisitoNaSolicitacao = seqscoRrequisito.Any(a => seqsComponentesCurricularesSolicitacao.Contains(a.GetValueOrDefault()));
                                        var correquisitoSelecionados = seqscoRrequisito.Any(a => seqsComponentesCurriculares.Contains(a.GetValueOrDefault()));

                                        if (correquisitoNaSolicitacao || correquisitoSelecionados)
                                        {
                                            coRequisitoSemItemSolicitacaoMatricula = false;
                                        }
                                        else
                                        {
                                            coRequisitoSemItemSolicitacaoMatricula = true;
                                        }
                                    }
                                    else if (SeqProcessoEtapa.HasValue)
                                    {
                                        var solicitacaoMatriculaItem = SolicitacaoMatriculaItemDomainService.SearchBySpecification(new SolicitacaoMatriculaItemFilterSpecification() { SeqSolicitacaoMatricula = seqSolicitacaoMatricula, PertencePlanoEstudo = true },
                                                                                           IncludesSolicitacaoMatriculaItem.ConfiguracaoComponente |
                                                                                           IncludesSolicitacaoMatriculaItem.HistoricosSituacao_SituacaoItemMatricula).ToList();

                                        var historicosSituacao = solicitacaoMatriculaItem.Where(w => w.ConfiguracaoComponente.SeqComponenteCurricular == componentePrincipal).SelectMany(s => s.HistoricosSituacao).ToList();
                                        var requisitoFinalizadoSemSucesso = historicosSituacao.Any(a => a.SituacaoItemMatricula.SeqProcessoEtapa == SeqProcessoEtapa && a.SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso);

                                        var corequisitoComponente = requisito.Itens.Where(w => w.TipoRequisito == TipoRequisito.CoRequisito).Select(s => s.SeqComponenteCurricular).ToList();

                                        var historicosSituacaoCorequisito = solicitacaoMatriculaItem.Where(w => corequisitoComponente.Contains(w.ConfiguracaoComponente.SeqComponenteCurricular)).SelectMany(s => s.HistoricosSituacao).ToList();
                                        var corequisitoFinalizadoComSucesso = historicosSituacaoCorequisito.Any(a => a.SituacaoItemMatricula.SeqProcessoEtapa == SeqProcessoEtapa && a.SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso);

                                        if (requisitoFinalizadoSemSucesso && corequisitoFinalizadoComSucesso)
                                            continue;
                                    }

                                    if (coRequisitoSemItemSolicitacaoMatricula)
                                        continue;
                                }
                                else
                                    continue;
                            }
                        }
                    }
                    // Verifica os itens que são do tipo pré requisito de componente curricular.
                    var prerequisitosHistorico = requisito.Itens.Where(a => a.TipoRequisito == TipoRequisito.PreRequisito && a.TipoRequisitoItem == TipoRequisitoItem.ComponenteCurricular).ToList();
                    if (prerequisitosHistorico.Any())
                    {
                        // Verifica se algum componente de pré requisito não está no histórico do aluno
                        var preRrequisitoSemHistorico = prerequisitosHistorico.Any(pr => !historicos.Any(h => h.SeqComponenteCurricular == pr.SeqComponenteCurricular));
                        if (preRrequisitoSemHistorico)
                            continue;
                    }

                    // Verifica itens que sejam de carga horária
                    var preRequisitoCargaHoraria = requisito.Itens.Where(w => w.TipoRequisito == TipoRequisito.PreRequisito && w.TipoRequisitoItem == TipoRequisitoItem.OutrosRequisitos && w.OutroRequisito == OutroRequisito.CargaHoraria);
                    if (preRequisitoCargaHoraria.Any())
                    {
                        // Conta a carga horária realizada pelo aluno
                        var historicoCargaHoraria = historicos.Where(w => w.CargaHorariaRealizada.HasValue).Sum(s => s.CargaHorariaRealizada);
                        if (preRequisitoCargaHoraria.Any(itemCargaHoraria => itemCargaHoraria.QuantidadeOutroRequisito > 0 && itemCargaHoraria.QuantidadeOutroRequisito > historicoCargaHoraria))
                            continue;
                    }

                    // Verifica itens que sejam de número de créditos
                    var preRequisitoCredito = requisito.Itens.Where(w => w.TipoRequisito == TipoRequisito.PreRequisito && w.TipoRequisitoItem == TipoRequisitoItem.OutrosRequisitos && w.OutroRequisito == OutroRequisito.Creditos);
                    if (preRequisitoCredito.Any())
                    {
                        // Conta o total de créditos cursados
                        var historicoCredito = historicos.Where(w => w.Credito.HasValue).Sum(s => s.Credito);
                        if (preRequisitoCredito.Any(itemCredito => itemCredito.QuantidadeOutroRequisito > 0 && itemCredito.QuantidadeOutroRequisito > historicoCredito))
                            continue;
                    }

                    // Verifica requisitos que sejam de grupo curricular
                    var preRequisitoGrupo = requisito.Itens.Where(w => w.TipoRequisito == TipoRequisito.PreRequisito && w.TipoRequisitoItem == TipoRequisitoItem.GrupoCurricular);
                    if (preRequisitoGrupo.Count() > 0)
                    {
                        // Busca o % de conclusão dos grupos do aluno
                        CalculoConclusaoCursoAlunoVO conclusao = AcademicoRepository.CalcularPercentualConclusaoCursoAluno(dadosOrigem.SeqAlunoHistoricoAtual);

                        // Para cada requisito de grupo, verifica o % de conclusão
                        if (preRequisitoGrupo.Any(itemGrupo =>
                        {
                            PercentualConclusaoGrupoVO percentGrupo = conclusao.PercentualGrupo.FirstOrDefault(g => g.SeqGrupoCurricular == itemGrupo.SeqGrupoCurricular.Value);
                            return percentGrupo != null && percentGrupo.PercentualConclusaoGrupo < 100;
                        }))
                            continue;


                    }

                    // Chegou até aqui quer dizer que atendeu ao requisito
                    // Atribui a propriedade igual a true e sai do loop pois não precisa verificar o próximo requisito
                    requisitoAtendido = true;
                    countRequisitoAtendido++;
                    break;
                }

                if (requisitoAtendido == false)
                {
                    // Gera a mensagem de erro do requisito
                    RequisitoPreECoMensagem(mensagemErro, seqAluno, requisitosComponente.ToList());
                }
            }

            // Caso tenha atendido o requisito, retorna OK
            if (countRequisito == countRequisitoAtendido)
                return (true, new List<string>());
            else
                return (false, mensagemErro);
        }

        /// <summary>
        /// Detalhes da mensagem de erro para saber se é componente ou turma
        /// </summary>
        private void RequisitoPreECoMensagem(List<string> mensagemErro, long seqPessoaAtuacao, List<Requisito> requisitos)
        {
            long? seqCicloSolicitacao = SolicitacaoServicoDomainService.BuscarCicloLetivoSolicitacaoPorPessoaAtuacao(seqPessoaAtuacao);
            foreach (var requisito in requisitos)
            {
                var dadosComponente = ComponenteCurricularDomainService.SearchProjectionByKey(requisito.SeqComponenteCurricular.Value, x => new
                {
                    x.Codigo,
                    x.Descricao,
                    x.CargaHoraria,
                    x.Credito,
                });

                var configuracaoComponente = ConfiguracaoComponenteDomainService.BuscarConfiguracaoComponenteCurricular(requisito.SeqComponenteCurricular.Value);
                var descricaoComponente = ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(dadosComponente.Codigo, dadosComponente.Descricao, dadosComponente.Credito, dadosComponente.CargaHoraria, configuracaoComponente.FormatoCargaHoraria);

                // Caso ja tenha adicionado a mensagem com o nome do componente curricular e seja o mesmo componente, adiciona OU....
                if (!mensagemErro.Any(m => m.Contains(descricaoComponente)))
                    mensagemErro.Add($"<br/>Há pendências nos requisitos para o componente {descricaoComponente}.<br />Pelo menos um dos grupos de requisito abaixo deve ser cumprido:");
                else
                    mensagemErro.Add("<b>OU</b>");

                // Gera a mensagem dos pré requisitos
                var preRequisitos = requisito.Itens.Where(i => i.TipoRequisito == TipoRequisito.PreRequisito);
                if (preRequisitos.Any())
                {
                    mensagemErro.Add("<ul><li>Pré-requisitos:<ul>");
                    foreach (var itemRequisito in preRequisitos)
                    {
                        switch (itemRequisito.TipoRequisitoItem)
                        {
                            case TipoRequisitoItem.ComponenteCurricular:
                                var descricaoPreRequisito = DescricaoRequisitoPreECoMensagem(itemRequisito.SeqComponenteCurricular, seqCicloSolicitacao, itemRequisito.ComponenteCurricular, true);
                                mensagemErro.Add($"<li>{descricaoPreRequisito.DescricaoRequisito}</li>");
                                break;

                            case TipoRequisitoItem.DivisaoMatriz:
                                break;

                            case TipoRequisitoItem.OutrosRequisitos:
                            case TipoRequisitoItem.GrupoCurricular:
                                mensagemErro.Add($"<li>{itemRequisito.Descricao}</li>");
                                break;
                        }
                    }
                    mensagemErro.Add("</ul></li></ul>");
                }

                // Gera a mensagem dos có requisitos
                var coRequisitos = requisito.Itens.Where(i => i.TipoRequisito == TipoRequisito.CoRequisito);
                if (coRequisitos.Any())
                {
                    mensagemErro.Add("<ul><li>Có-requisitos:<ul>");
                    foreach (var itemRequisito in coRequisitos)
                    {
                        if (itemRequisito.TipoRequisitoItem == TipoRequisitoItem.ComponenteCurricular)
                        {
                            var descricaoPreRequisito = DescricaoRequisitoPreECoMensagem(itemRequisito.SeqComponenteCurricular, seqCicloSolicitacao, itemRequisito.ComponenteCurricular, true);
                            mensagemErro.Add($"<li>{descricaoPreRequisito.DescricaoRequisito}</li>");
                        }
                    }
                    mensagemErro.Add("</ul></li></ul>");
                }
            }
        }

        private (string DescricaoRequisito, long RegistroTurma) DescricaoRequisitoPreECoMensagem(long? seqComponenteCurricular, long? seqCicloLetivo, ComponenteCurricular componenteCurricular, bool item)
        {
            string descricaoRequisito = string.Empty;

            var validacaoTurma = TurmaConfiguracaoComponenteDomainService.BuscarDescricaoTurmaPorComponenteCurricularCiclo(seqComponenteCurricular, seqCicloLetivo);

            switch (validacaoTurma.registroTurma)
            {
                //case 1:
                //    descricaoRequisito = validacaoTurma.descricaoTurma;
                //    break;
                case 2:
                    if (item)
                        descricaoRequisito = $"pelo menos uma turma do componente { ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(componenteCurricular)}";
                    else
                        descricaoRequisito = $"As turmas do componente {ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(componenteCurricular)}";
                    break;

                default:
                    descricaoRequisito = ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(componenteCurricular);
                    break;
            }

            return (descricaoRequisito, validacaoTurma.registroTurma);
        }

        /// <summary>
        /// Lista os pré requisitos de uma divisão de componente curricular
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <param name="seqDivisaoComponente">Sequencial da divisão de componente</param>
        /// <returns>Lista de sequenciais dos componentes curriculares de requisitos</returns>
        public List<long> BuscarSeqsPreRequisitosAtividadeComplementar(long seqMatrizCurricularOferta, long seqDivisaoComponente)
        {
            var matrizRequisitos = MatrizCurricularOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<MatrizCurricularOferta>(seqMatrizCurricularOferta), x =>
                                    x.MatrizCurricular.Requisitos.Where(w => w.Requisito.ComponenteCurricular.Configuracoes.Any(a => a.DivisoesComponente.Any(d => d.Seq == seqDivisaoComponente)))
                                                .SelectMany(s => s.Requisito.Itens.Where(i => i.TipoRequisito == TipoRequisito.PreRequisito && !i.SeqDivisaoCurricularItem.HasValue && i.SeqComponenteCurricular.HasValue).Select(i => i.SeqComponenteCurricular.Value)));

            //Verifica se algum item de requisito possui pré requisito
            return matrizRequisitos.ToList();
        }

        /// <summary>
        /// Buscar o requisito
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Objeto requisito com seus itens</returns>
        public RequisitoVO BuscarRequisito(long seq)
        {
            RequisitoVO requisitoVO = this.SearchByKey<Requisito, RequisitoVO>(seq, IncludesRequisito.Itens | IncludesRequisito.MatrizesCurriculares);

            foreach (var item in requisitoVO.Itens)
            {
                if (item.SeqDivisaoCurricularItem.HasValue)
                    item.DescricaoRequisitoDivisao = this.DivisaoCurricularItemDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoCurricularItem>(item.SeqDivisaoCurricularItem.Value), p => p.Numero + " - " + p.DivisaoCurricular.TipoDivisaoCurricular.Descricao);

                if (item.SeqComponenteCurricular.HasValue)
                    item.DescricaoRequisitoComponente = this.ComponenteCurricularDomainService.BuscarComponenteCurricularDescricaoCompleta(item.SeqComponenteCurricular.Value);

                if (item.OutroRequisito == OutroRequisito.CargaHoraria)
                    item.DescricaoRequisitoOutro = $"{OutroRequisito.CargaHoraria.SMCGetDescription()}";

                if (item.OutroRequisito == OutroRequisito.Creditos)
                    item.DescricaoRequisitoOutro = $"{OutroRequisito.Creditos.SMCGetDescription()}";

                if (item.SeqGrupoCurricular.HasValue)
                    item.DescricaoRequisitoGrupoCurricular = this.GrupoCurricularDomainService.BuscaGrupoCurricularDescricaoFormatada(item.SeqGrupoCurricular.Value);
            }

            return requisitoVO;
        }

        /// <summary>
        /// Busca os requisitos de acordo com os filtros
        /// </summary>
        /// <param name="filtros">Objeto requisito filtro</param>
        /// <returns>SMCPagerData com a lista de requisitos</returns>
        public SMCPagerData<RequisitoListaVO> BuscarRequisitos(RequisitoFilterSpecification filtros)
        {
            int total = 0;

            var formatoCargaHoraria = this.InstituicaoNivelTipoComponenteCurricularDomainService
                .SearchProjectionAll(p => new
                {
                    p.SeqTipoComponenteCurricular,
                    p.InstituicaoNivel.SeqNivelEnsino,
                    p.FormatoCargaHoraria,
                    p.InstituicaoNivel.SeqInstituicaoEnsino // Para forçar o filtro global
                })
                .ToList();

            // Caso seja para filtrar pelo componente, desconsidera a matriz
            if (filtros.SeqComponenteCurricular.HasValue)
                filtros.SeqDivisaoCurricularItem = null;

            var requisitos = this.SearchProjectionBySpecification(filtros,
                                                                p => new RequisitoListaVO()
                                                                {
                                                                    Seq = p.Seq,
                                                                    SeqMatrizCurricular = filtros.SeqMatrizCurricular,
                                                                    SeqCurriculoCursoOferta = filtros.SeqCurriculoCursoOferta,

                                                                    // Campos componente
                                                                    SeqComponenteCurricular = p.SeqComponenteCurricular,
                                                                    CodigoComponenteCurricular = p.ComponenteCurricular.Codigo,
                                                                    DescricaoComponenteCurricular = p.ComponenteCurricular.Descricao,
                                                                    CreditoComponenteCurricular = p.ComponenteCurricular.Credito,
                                                                    CargaHorariaComponenteCurricular = p.ComponenteCurricular.CargaHoraria,
                                                                    SeqTipoComponenteCurricular = p.ComponenteCurricular.SeqTipoComponenteCurricular,
                                                                    SeqNivelComponenteCurricular = p.ComponenteCurricular.NiveisEnsino.FirstOrDefault(f => f.Responsavel).SeqNivelEnsino,

                                                                    // Campos divisão
                                                                    SeqDivisaoCurricularItem = p.SeqDivisaoCurricularItem,
                                                                    //NumeroDivisaoCurricularItem = p.DivisaoCurricularItem.Numero,
                                                                    //DescricaoTipoDivisaoCurricular = p.DivisaoCurricularItem.DivisaoCurricular.TipoDivisaoCurricular.Descricao,
                                                                    DescricaoDivisaoCurricularItem = p.DivisaoCurricularItem.Descricao,

                                                                    UnicaMatrizAssociada = p.MatrizesCurriculares.Count == 1,
                                                                    Itens = p.Itens.Select(s => new RequisitoItemVO()
                                                                    {
                                                                        Seq = s.Seq,
                                                                        SeqDivisaoCurricularItem = s.SeqDivisaoCurricularItem,
                                                                        SeqComponenteCurricular = s.SeqComponenteCurricular,
                                                                        OutroRequisito = s.OutroRequisito,
                                                                        SeqGrupoCurricular = s.SeqGrupoCurricular,
                                                                        GrupoPertenceCurriculoMatrizOferta = p.MatrizesCurriculares.Any(a => a.SeqMatrizCurricular == filtros.SeqMatrizCurricular) && !p.MatrizesCurriculares.Any(a => a.MatrizCurricular.CurriculoCursoOferta.SeqCurriculo != s.GrupoCurricular.SeqCurriculo),
                                                                        QuantidadeOutroRequisito = s.QuantidadeOutroRequisito,
                                                                        TipoRequisito = s.TipoRequisito,
                                                                    }).ToList(),

                                                                    MatrizesCurriculares = p.MatrizesCurriculares.Select(s => new MatrizCurricularCabecalhoVO()
                                                                    {
                                                                        SeqMatrizCurricular = s.MatrizCurricular.Seq,
                                                                        CodigoMatrizCurricular = s.MatrizCurricular.Codigo,
                                                                        DescricaoMatrizCurricular = s.MatrizCurricular.Descricao,
                                                                        DescricaoComplementarMatrizCurricular = s.MatrizCurricular.DescricaoComplementar,
                                                                    }).ToList()
                                                                }, out total).ToList();

            foreach (var registro in requisitos)
            {
                registro.Associado = registro.MatrizesCurriculares.Count(c => c.SeqMatrizCurricular == filtros.SeqMatrizCurricular) > 0 ? TipoRequisitoAssociado.Associado : TipoRequisitoAssociado.NaoAssociado;
                foreach (var item in registro.Itens)
                {
                    if (item.SeqDivisaoCurricularItem.HasValue)
                        item.DescricaoRequisitoDivisao = $"{item.TipoRequisito.SMCGetDescription()} - {this.DivisaoCurricularItemDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoCurricularItem>(item.SeqDivisaoCurricularItem.Value), p => p.Numero + " - " + p.DivisaoCurricular.TipoDivisaoCurricular.Descricao)}";

                    if (item.SeqComponenteCurricular.HasValue)
                        item.DescricaoRequisitoComponente = $"{item.TipoRequisito.SMCGetDescription()} - {this.ComponenteCurricularDomainService.BuscarComponenteCurricularDescricaoCompleta(item.SeqComponenteCurricular.Value)}";

                    if (item.OutroRequisito == OutroRequisito.CargaHoraria)
                        item.DescricaoRequisitoOutro = $"{item.TipoRequisito.SMCGetDescription()} - {OutroRequisito.CargaHoraria.SMCGetDescription()} - {item.QuantidadeOutroRequisito} horas";

                    if (item.OutroRequisito == OutroRequisito.Creditos)
                        item.DescricaoRequisitoOutro = $"{item.TipoRequisito.SMCGetDescription()} - {OutroRequisito.Creditos.SMCGetDescription()} - {item.QuantidadeOutroRequisito}";

                    if (item.SeqGrupoCurricular.HasValue)
                        item.DescricaoRequisitoGrupoCurricular = $"{item.TipoRequisito.SMCGetDescription()} - {this.GrupoCurricularDomainService.BuscaGrupoCurricularDescricaoFormatada(item.SeqGrupoCurricular.Value)}";
                }

                if (registro.SeqComponenteCurricular.HasValue)
                {
                    var formato = formatoCargaHoraria.FirstOrDefault(f => f.SeqTipoComponenteCurricular == registro.SeqTipoComponenteCurricular
                                                                       && f.SeqNivelEnsino == registro.SeqNivelComponenteCurricular.GetValueOrDefault())
                                                                            .FormatoCargaHoraria;
                    registro.DescricaoComponenteCurricular =
                        ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(registro.CodigoComponenteCurricular,
                                                                                             registro.DescricaoComponenteCurricular,
                                                                                             registro.CreditoComponenteCurricular,
                                                                                             registro.CargaHorariaComponenteCurricular,
                                                                                             formato);
                }
                //else if (registro.SeqDivisaoCurricularItem.HasValue)
                //    registro.DescricaoDivisaoCurricularItem =
                //        DivisaoCurricularItemDomainService.GerarDescricaoDivisaoMatrizCurricularItem(registro.NumeroDivisaoCurricularItem.GetValueOrDefault(),
                //                                                                                     registro.DescricaoTipoDivisaoCurricular);
            }

            return new SMCPagerData<RequisitoListaVO>(requisitos, total);
        }

        /// <summary>
        /// Grava um requisito com seus respectivos itens
        /// </summary>
        /// <param name="requisito">Dados do requisito a ser gravado</param>
        /// <returns>Sequencial do requisito gravado</returns>
        public long SalvarRequisito(RequisitoVO requisito)
        {
            var registro = requisito.Transform<Requisito>();

            if (!ValidarItensIguais(registro))
                throw new RequisitoItensDupliacadosException();

            if (!ValidarRequisitoItens(registro))
                throw new RequisitoListaItensDuplicadosException();

            if (registro.Itens.SMCCount() == 0)
                throw new RequisitoSemItensException();

            if (requisito.SeqComponenteCurricular.HasValue)
                registro.SeqDivisaoCurricularItem = null;

            //Se for inclusão associa a matriz curricular do parâmetro
            if (requisito.Seq == 0)
            {
                registro.MatrizesCurriculares = new List<MatrizCurricularRequisito>();
                registro.MatrizesCurriculares.Add(new MatrizCurricularRequisito() { SeqMatrizCurricular = requisito.SeqMatrizCurricular });
            }
            else
            {
                var matrizes = this.SearchProjectionByKey(new SMCSeqSpecification<Requisito>(registro.Seq), p => p.MatrizesCurriculares);
                if (matrizes.Count > 1)
                    throw new RequisitoEdicaoNaoPermitidaException();
            }

            // Caso tenha algum pré requisito de componente do tipo outros,
            // valida a carga horária ou créditos não são maiores que a soma da divisão atual com as anteriores
            if (requisito.SeqComponenteCurricular.HasValue && requisito.Itens.Any(a => a.TipoRequisito == TipoRequisito.PreRequisito && a.TipoRequisitoItem == TipoRequisitoItem.OutrosRequisitos))
            {
                // Recupera o número da divisão do componente que será avaliado
                var spcecDivisaoComponenteRequisito = new DivisaoMatrizCurricularComponenteFilterSpecification()
                {
                    SeqComponenteCurricular = requisito.SeqComponenteCurricular,
                    SeqMatrizCurricular = requisito.SeqMatrizCurricular
                };
                var componenteRequisito = DivisaoMatrizCurricularComponenteDomainService.SearchProjectionByKey(spcecDivisaoComponenteRequisito, p => new
                {
                    NumeroDivisao = (short?)p.DivisaoMatrizCurricular.DivisaoCurricularItem.Numero,
                    p.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,
                    p.ConfiguracaoComponente.ComponenteCurricular.Credito
                });

                if (componenteRequisito == null || componenteRequisito.NumeroDivisao.GetValueOrDefault() == 0)
                    throw new RequisitoSemDivisaoException();

                var valoresCurriculo = MatrizCurricularDomainService.BuscarValoresGruposMatriz(requisito.SeqMatrizCurricular, true);
                var cargaHorariaCurriculo = valoresCurriculo.QuantidadeHoraAulaObrigatoria.GetValueOrDefault() + valoresCurriculo.QuantidadeHoraAulaOptativa.GetValueOrDefault() + valoresCurriculo.QuantidadeHoraRelogioObrigatoria.GetValueOrDefault() + valoresCurriculo.QuantidadeHoraRelogioOptativa.GetValueOrDefault();
                var creditosCurriculo = valoresCurriculo.QuantidadeCreditoObrigatorio.GetValueOrDefault() + valoresCurriculo.QuantidadeCreditoOptativo.GetValueOrDefault();

                // Valida se algum pré requisito do tipo outros têm a carga horária ou os créditos maiores que o valor do currículo (desconsiderando o valor do próprio componente)
                foreach (var item in requisito.Itens.Where(w => w.TipoRequisito == TipoRequisito.PreRequisito && w.TipoRequisitoItem == TipoRequisitoItem.OutrosRequisitos))
                {
                    var cargaHorariaCurriculoMenosComponenteRequisito = cargaHorariaCurriculo - componenteRequisito.CargaHoraria.GetValueOrDefault();
                    var creditoCurriculoMenosComponenteRequisito = creditosCurriculo - componenteRequisito.Credito.GetValueOrDefault();

                    #region Validação carga horaria curriculo menor que componente do requisito

                    //Validação para se dar negativo, tranforma para positivo
                    //carga horaria curriculo (15) - carga horaria componente (30) = -15
                    //O resultado ficaria 15 positivo, mas isso se puder considerar que a carga do componente
                    //pode ser maior que o curriculo, senão, se ele for maior que o curriculo já não vai deixar inserir carga no pre requisito

                    //cargaHorariaCurriculoMenosComponenteRequisito = cargaHorariaCurriculoMenosComponenteRequisito < 0 ? cargaHorariaCurriculoMenosComponenteRequisito * (-1) : cargaHorariaCurriculoMenosComponenteRequisito;
                    //creditoCurriculoMenosComponenteRequisito = creditoCurriculoMenosComponenteRequisito < 0 ? creditoCurriculoMenosComponenteRequisito * (-1) : creditoCurriculoMenosComponenteRequisito;

                    #endregion Validação carga horaria curriculo menor que componente do requisito

                    /*Validação para não deixar com número negativo pois é instável e tem momentos que a validação passa*/
                    cargaHorariaCurriculoMenosComponenteRequisito = cargaHorariaCurriculoMenosComponenteRequisito < 0 ? 0 : cargaHorariaCurriculoMenosComponenteRequisito;
                    creditoCurriculoMenosComponenteRequisito = creditoCurriculoMenosComponenteRequisito < 0 ? 0 : creditoCurriculoMenosComponenteRequisito;

                    if (item.OutroRequisito == OutroRequisito.CargaHoraria && cargaHorariaCurriculoMenosComponenteRequisito < item.QuantidadeOutroRequisito)
                        throw new RequisitoQuantidadeCargaHorariaInvalidaException();
                    else if (item.OutroRequisito == OutroRequisito.Creditos && creditoCurriculoMenosComponenteRequisito < item.QuantidadeOutroRequisito)
                        throw new RequisitoQuantidadeCreditoInvalidaException();
                }
            }

            foreach (var item in registro.Itens)
            {
                //Se foi selecionado um componente curricular, não pode ser selecionado grupo curricular que tenha o componente em questão
                if (requisito.SeqComponenteCurricular.HasValue && item.SeqGrupoCurricular.HasValue)
                {
                    var grupoCurricular = this.GrupoCurricularDomainService.SearchByKey(new SMCSeqSpecification<GrupoCurricular>(item.SeqGrupoCurricular.Value), x => x.ComponentesCurriculares);

                    if (grupoCurricular.ComponentesCurriculares.Any(a => a.SeqComponenteCurricular == requisito.SeqComponenteCurricular.Value))
                        throw new RequisitoGrupoCurricularComMesmoComponenteSelecionadoException();
                }

                //Só deverá ter a opção de có requisito somente para componentes com divisão de componente com tipo gestão turma
                if (item.TipoRequisito == TipoRequisito.CoRequisito)
                {
                    if (requisito.SeqComponenteCurricular.HasValue)
                    {
                        var componenteCurricular = this.ComponenteCurricularDomainService.SearchByKey(new SMCSeqSpecification<ComponenteCurricular>(requisito.SeqComponenteCurricular.Value), x => x.Configuracoes[0].DivisoesComponente[0].TipoDivisaoComponente);

                        //O tipo de todas as divisões das configurações daquele componente vão ter o mesmo tipo de gestão, por isso pode pegar a primeira configuração que encontrar
                        var tipoDivisaoComponente = componenteCurricular?.Configuracoes?.FirstOrDefault()?.DivisoesComponente?.FirstOrDefault()?.TipoDivisaoComponente;

                        if (tipoDivisaoComponente?.TipoGestaoDivisaoComponente != TipoGestaoDivisaoComponente.Turma)
                            throw new RequisitoCoRequisitoSemComponenteTipoGestaoTurmaException();
                    }
                    else
                        throw new RequisitoCoRequisitoSemComponenteTipoGestaoTurmaException();
                }

                switch (item.TipoRequisitoItem)
                {
                    case TipoRequisitoItem.ComponenteCurricular:
                        var specComponente = new SMCSeqSpecification<ComponenteCurricular>(item.SeqComponenteCurricular.Value);
                        var componente = this.ComponenteCurricularDomainService.SearchProjectionByKey(specComponente, p => new { p.Codigo, p.Descricao });
                        item.Descricao = $"{componente.Codigo} - {componente.Descricao}";
                        break;

                    case TipoRequisitoItem.DivisaoMatriz:
                        var specDivisao = new SMCSeqSpecification<DivisaoCurricularItem>(item.SeqDivisaoCurricularItem.Value);
                        item.Descricao = this.DivisaoCurricularItemDomainService.SearchProjectionByKey(specDivisao, p => p.Descricao);
                        break;

                    case TipoRequisitoItem.OutrosRequisitos:
                        item.Descricao = item.OutroRequisito == OutroRequisito.CargaHoraria ?
                            $"{SMCEnumHelper.GetDescription(item.OutroRequisito.Value)} - {item.QuantidadeOutroRequisito} {MessagesResource.Label_Horas}" :
                            $"{SMCEnumHelper.GetDescription(item.OutroRequisito.Value)} - {item.QuantidadeOutroRequisito}";

                        break;

                    case TipoRequisitoItem.GrupoCurricular:
                        item.Descricao = this.GrupoCurricularDomainService.BuscaGrupoCurricularDescricaoFormatada(item.SeqGrupoCurricular.Value);
                        break;

                    default:
                        item.Descricao = "";
                        break;
                }
            }

            this.SaveEntity(registro);

            return registro.Seq;
        }

        /// <summary>
        /// Associar um requisito a uma matriz curricular
        /// </summary>
        /// <param name="seq">Sequencial do requisito</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        public void AssociarRequisito(long seq, long seqMatrizCurricular)
        {
            var registro = this.SearchByKey(new SMCSeqSpecification<Requisito>(seq), IncludesRequisito.Itens | IncludesRequisito.MatrizesCurriculares);

            var matrizesAssociadas = registro.MatrizesCurriculares.ToList();

            //Associa uma registro
            if (matrizesAssociadas.Count(c => c.SeqMatrizCurricular == seqMatrizCurricular) == 0)
                matrizesAssociadas.Add(new MatrizCurricularRequisito() { SeqRequisito = seq, SeqMatrizCurricular = seqMatrizCurricular });

            registro.MatrizesCurriculares = matrizesAssociadas;

            this.SaveEntity(registro);
        }

        /// <summary>
        /// Desassociar um requisito a uma matriz curricular
        /// </summary>
        /// <param name="seq">Sequencial do requisito</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <param name="excluirRequisito">Excluir requisito após desassociar a matriz</param>
        public void DesassociarRequisito(long seq, long seqMatrizCurricular, bool excluirRequisito)
        {
            var registro = this.SearchByKey(new SMCSeqSpecification<Requisito>(seq), IncludesRequisito.Itens | IncludesRequisito.MatrizesCurriculares);

            var matrizesAssociadas = registro.MatrizesCurriculares.ToList();

            //Desassocia um registro
            matrizesAssociadas.RemoveAll(w => w.SeqMatrizCurricular == seqMatrizCurricular);

            registro.MatrizesCurriculares = matrizesAssociadas;

            if (excluirRequisito)
                this.DeleteEntity(registro);
            else
                this.SaveEntity(registro);
        }

        /// <summary>
        /// Excluir um requisito com seus respectivos itens
        /// </summary>
        /// <param name="seq">Sequencial do requisito</param>
        /// <param name="seqMatrizCurricular">Sequencial matriz curricular tela</param>
        public void ExcluirRequisito(long seq, long seqMatrizCurricular)
        {
            Requisito requisito = this.SearchByKey(new SMCSeqSpecification<Requisito>(seq), IncludesRequisito.Itens | IncludesRequisito.MatrizesCurriculares);

            if (requisito.MatrizesCurriculares.Count > 1)
                throw new RequisitoExclusaoNaoPermitidaException();

            if (requisito.MatrizesCurriculares.Count > 0 && requisito.MatrizesCurriculares.First().SeqMatrizCurricular != seqMatrizCurricular)
                throw new RequisitoExclusaoNaoPermitidaException();

            this.DeleteEntity(requisito);
        }

        /// <summary>
        /// Valida se ja existe um requisito com todos os itens iguais RN_CUR_058.1
        /// </summary>
        /// <param name="requisito">Dados do requisito a ser gravado</param>
        /// <returns>Retorna se o registro esta valido</returns>
        public bool ValidarRequisitoItens(Requisito requisito)
        {
            RequisitoFilterSpecification specQuantidade = new RequisitoFilterSpecification()
            {
                QuantidadeItensCadastrado = requisito.Itens.Count(),
                SeqComponenteCurricular = requisito.SeqComponenteCurricular,
                SeqDivisaoCurricularItem = requisito.SeqDivisaoCurricularItem
            };

            var registrosQuantidadeIgual = this.SearchProjectionBySpecification(specQuantidade, p => p.Seq).Where(w => w != requisito.Seq).Count();
            if (registrosQuantidadeIgual == 0)
                return true;

            foreach (var item in requisito.Itens)
            {

                SMCAndSpecification<Requisito> specFinal = new SMCAndSpecification<Requisito>(new RequisitoFilterSpecification(), specQuantidade);
                RequisitoFilterSpecification specItem = new RequisitoFilterSpecification()
                {
                    TipoRequisito = item.TipoRequisito,
                    TipoRequisitoItem = item.TipoRequisitoItem,
                    ItemSeqDivisaoCurricularItem = item.SeqDivisaoCurricularItem,
                    ItemSeqComponenteCurricular = item.SeqComponenteCurricular,
                    ItemSeqGrupoCurricular = item.SeqGrupoCurricular,
                    OutroRequisitoItem = item.OutroRequisito,
                    QuantidadeOutroRequisito = item.QuantidadeOutroRequisito
                };

                specFinal = new SMCAndSpecification<Requisito>(specFinal, specItem);
                var registrosItemIgual = this.SearchProjectionBySpecification(specFinal, p => p.Seq).Where(w => w != requisito.Seq).Count();
                if (registrosItemIgual == 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Valida se existe items do requisito iguais
        /// </summary>
        /// <param name="requisito">Dados do requisito a ser gravado</param>
        /// <returns>Retorna se o registro esta valido</returns>
        public bool ValidarItensIguais(Requisito requisito)
        {
            var agrupamentoItensIguais = requisito.Itens.GroupBy(g => new
            {
                g.TipoRequisito,
                g.TipoRequisitoItem,
                g.SeqDivisaoCurricularItem,
                g.SeqComponenteCurricular,
                g.SeqGrupoCurricular,
                g.OutroRequisito,
                g.QuantidadeOutroRequisito
            }).ToList();

            var result = !agrupamentoItensIguais.Any(a => a.Count() > 1);

            return result;
        }

        /// <summary>
		/// Buscando os tipos de requisito com base no componente curricular
		/// </summary>
		/// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
		/// <returns>Lista com os tipos de requisito</returns>
        public List<SMCDatasourceItem> BuscarTiposRequisitoSelect(long? seqComponenteCurricular)
        {
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            if (seqComponenteCurricular.HasValue)
            {
                var componenteCurricular = this.ComponenteCurricularDomainService.SearchByKey(new SMCSeqSpecification<ComponenteCurricular>(seqComponenteCurricular.Value), x => x.Configuracoes[0].DivisoesComponente[0].TipoDivisaoComponente);

                //O tipo de todas as divisões das configurações daquele componente vão ter o mesmo tipo de gestão, por isso pode pegar a primeira configuração que encontrar
                var tipoDivisaoComponente = componenteCurricular?.Configuracoes?.FirstOrDefault()?.DivisoesComponente?.FirstOrDefault()?.TipoDivisaoComponente;

                if (tipoDivisaoComponente?.TipoGestaoDivisaoComponente == TipoGestaoDivisaoComponente.Turma)
                {
                    foreach (var item in Enum.GetValues(typeof(TipoRequisito)).Cast<TipoRequisito>())
                    {
                        if ((long)item != 0)
                        {
                            retorno.Add(new SMCDatasourceItem() { Seq = (long)item, Descricao = SMCEnumHelper.GetDescription(item) });
                        }
                    }
                }
                else
                {
                    retorno.Add(new SMCDatasourceItem() { Seq = (long)TipoRequisito.PreRequisito, Descricao = SMCEnumHelper.GetDescription(TipoRequisito.PreRequisito) });
                }
            }
            else
            {
                retorno.Add(new SMCDatasourceItem() { Seq = (long)TipoRequisito.PreRequisito, Descricao = SMCEnumHelper.GetDescription(TipoRequisito.PreRequisito) });
            }

            return retorno;
        }
    }
}