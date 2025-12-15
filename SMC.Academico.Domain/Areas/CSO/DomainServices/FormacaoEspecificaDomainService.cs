using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Notificacoes.Common.Areas.NTF.Enums;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class FormacaoEspecificaDomainService : AcademicoContextDomain<FormacaoEspecifica>
    {
        #region [ DomainService ]

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();
        private CursoDomainService CursoDomainService => Create<CursoDomainService>();
        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService => Create<HierarquiaEntidadeDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();
        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService => Create<HierarquiaEntidadeItemDomainService>();
        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();
        private InstituicaoTipoEntidadeFormacaoEspecificaDomainService InstituicaoTipoEntidadeFormacaoEspecificaDomainService => Create<InstituicaoTipoEntidadeFormacaoEspecificaDomainService>();
        private ProgramaDomainService ProgramaDomainService => Create<ProgramaDomainService>();
        private TipoOfertaDomainService TipoOfertaDomainService => Create<TipoOfertaDomainService>();
        private CursoFormacaoEspecificaDomainService CursoFormacaoEspecificaDomainService => Create<CursoFormacaoEspecificaDomainService>();
        private EntidadeConfiguracaoNotificacaoDomainService EntidadeConfiguracaoNotificacaoDomainService => Create<EntidadeConfiguracaoNotificacaoDomainService>();
        private TipoEntidadeDomainService TipoentidadeDomainService => Create<TipoEntidadeDomainService>();
        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();
        private DocumentoConclusaoFormacaoDomainService DocumentoConclusaoFormacaoDomainService => Create<DocumentoConclusaoFormacaoDomainService>();
        private TipoFormacaoEspecificaDomainService TipoFormacaoEspecificaDomainService => Create<TipoFormacaoEspecificaDomainService>();

        #endregion [ DomainService ]

        #region Serviços

        private INotificacaoService NotificacaoService => Create<INotificacaoService>();

        #endregion Serviços

        #region [ Queries ]

        #region [ _buscarTreeFormacoesEspecificaPorFormacaoFilho ]

        private string _buscarTreeFormacoesEspecificaPorFormacaoFilho =
                        @"  ;WITH X AS
							(
								SELECT FE.seq_formacao_especifica,
									   FE.seq_formacao_especifica_superior
								FROM CSO.formacao_especifica FE
								WHERE seq_formacao_especifica IN ({0})
								UNION ALL
								SELECT FET.seq_formacao_especifica,
									   FET.seq_formacao_especifica_superior
								FROM X INNER JOIN CSO.formacao_especifica AS FET ON FET.seq_formacao_especifica = X.seq_formacao_especifica_superior
							)

							SELECT DISTINCT X.seq_formacao_especifica
							FROM X;
						 ";

        #endregion [ _buscarTreeFormacoesEspecificaPorFormacaoFilho ]

        #region [ _buscarTreeFormacoesEspecificaPorFormacaoPai ]

        private string _buscarTreeFormacoesEspecificaPorFormacaoPai =
                        @"  ;WITH X AS
							(
								SELECT FE.seq_formacao_especifica,
									   FE.seq_formacao_especifica_superior
								FROM CSO.formacao_especifica FE
								WHERE seq_formacao_especifica_superior IN ({0})
								UNION ALL
								SELECT FET.seq_formacao_especifica,
									   FET.seq_formacao_especifica_superior
								FROM X INNER JOIN CSO.formacao_especifica AS FET ON FET.seq_formacao_especifica_superior = X.seq_formacao_especifica
							)

							SELECT DISTINCT X.seq_formacao_especifica
							FROM X;
						 ";

        #endregion [ _buscarTreeFormacoesEspecificaPorFormacaoPai ]

        #endregion [ Queries ]

        /// <summary>
        /// Busca as descrições de formações específicas de uma pessoa atuação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="desabilitarFiltro">Flag para desabilitar filtro de dados</param>
        /// <returns>Descrição e Seq da formação específica</returns>
        public List<(long SeqFormacaoEspecifica, string DescricaoFormacaoEspecifica, string TokenTipoFormacaoEspecifica)> BuscarDescricaoFormacaoEspecifica(long seqPessoaAtuacao, bool desabilitarFiltro = false, string tokenNivelEnsino = null)
        {
            // Verifica se deve desabilitar os filtros de dados.
            if (desabilitarFiltro)
            {
                AlunoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                IngressanteDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }
            List<long> seqsFormacoesEspecificas = new List<long>();
            long? seqEntidadeResponsavelFormacaoEspecifica = null;

            var tipoAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(seqPessoaAtuacao, x => x.TipoAtuacao);
            if (tipoAtuacao == TipoAtuacao.Ingressante)
            {
                var dados = IngressanteDomainService.SearchProjectionByKey(seqPessoaAtuacao, x => new
                {
                    SeqsFormacoesEspecificas = x.FormacoesEspecificas.Select(f => f.SeqFormacaoEspecifica).ToList(),
                    SeqEntidadeResponsavelFormacaoEspecifica = x.FormacoesEspecificas.Select(f => f.FormacaoEspecifica.SeqEntidadeResponsavel).FirstOrDefault(),
                });
                seqsFormacoesEspecificas = dados?.SeqsFormacoesEspecificas;
                seqEntidadeResponsavelFormacaoEspecifica = dados?.SeqEntidadeResponsavelFormacaoEspecifica;
            }
            else
            {
                var dados = AlunoDomainService.SearchProjectionByKey(seqPessoaAtuacao, x => new
                {
                    SeqsFormacoesEspecificas = x.Historicos.FirstOrDefault(h => h.Atual).Formacoes.Where(a => a.DataInicio <= DateTime.Now && (!a.DataFim.HasValue || a.DataFim >= DateTime.Now)).Select(a => a.SeqFormacaoEspecifica).ToList(),
                    SeqEntidadeResponsavelFormacaoEspecifica = x.Historicos.FirstOrDefault(h => h.Atual).Formacoes.FirstOrDefault(a => a.DataInicio <= DateTime.Now && (!a.DataFim.HasValue || a.DataFim >= DateTime.Now)).FormacaoEspecifica.SeqEntidadeResponsavel,
                });
                seqsFormacoesEspecificas = dados?.SeqsFormacoesEspecificas;
                seqEntidadeResponsavelFormacaoEspecifica = dados?.SeqEntidadeResponsavelFormacaoEspecifica;

                if (seqsFormacoesEspecificas.Count == 0 && !seqEntidadeResponsavelFormacaoEspecifica.HasValue)
                {
                    var novoSemestre = AlunoDomainService.SearchProjectionByKey(seqPessoaAtuacao, x => new
                    {
                        SeqsFormacoesEspecificas = x.Historicos.FirstOrDefault(h => h.Atual).Formacoes.Where(a => !a.DataFim.HasValue || a.DataFim >= DateTime.Now).Select(a => a.SeqFormacaoEspecifica).ToList(),
                        SeqEntidadeResponsavelFormacaoEspecifica = x.Historicos.FirstOrDefault(h => h.Atual).Formacoes.FirstOrDefault(a => !a.DataFim.HasValue || a.DataFim >= DateTime.Now).FormacaoEspecifica.SeqEntidadeResponsavel,
                    });
                }
                else if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO)
                {
                    var descricaoFormacaoEspecifica = BuscarFormacoesEspecificasHierarquia(dados.SeqsFormacoesEspecificas.ToArray());
                    var hierarquiasFormacao = descricaoFormacaoEspecifica.SelectMany(a => a.Hierarquia).ToList();

                    var retorno = new List<(long SeqFormacaoEspecifica, string DescricaoFormacaoEspecifica, string TokenTipoFormacaoEspecifica)>();
                    foreach (var hierarquiaFormacao in hierarquiasFormacao)
                    {
                        retorno.Add((hierarquiaFormacao.Seq, $"[{hierarquiaFormacao.DescricaoTipoFormacaoEspecifica}] {hierarquiaFormacao.Descricao}", string.Empty));
                    }
                    return retorno;
                }
            }

            // Verifica se deve habilitar os filtros de dados.
            if (desabilitarFiltro)
            {
                AlunoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                IngressanteDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            return BuscarDescricaoFormacaoEspecifica(seqsFormacoesEspecificas, seqEntidadeResponsavelFormacaoEspecifica, desabilitarFiltro);
        }

        /// <summary>
        /// Busca as descrições de formações específicas de uma pessoa atuação
        /// </summary>
        /// <param name="seqFormacaoEspecifica">Sequenciais das formações específicas</param>
        /// <param name="seqEntidadeResponsavelFormacaoEspecifica">Sequencial da entidade responsável pelas formações específicas</param>
        /// <param name="desabilitarFiltro">Flag para desabilitar filtro de dados</param>
        /// <returns>Descrição e Seq da formação específica</returns>
        public List<(long SeqFormacaoEspecifica, string DescricaoFormacaoEspecifica, string TokenTipoFormacaoEspecifica)> BuscarDescricaoFormacaoEspecifica(long seqFormacaoEspecifica, long? seqEntidadeResponsavelFormacaoEspecifica, bool desabilitarFiltro = false)
        {
            return BuscarDescricaoFormacaoEspecifica(new List<long> { seqFormacaoEspecifica }, seqEntidadeResponsavelFormacaoEspecifica);
        }

        /// <summary>
        /// Busca as descrições de formações específicas de uma pessoa atuação
        /// </summary>
        /// <param name="seqsFormacoesEspecificas">Sequenciais das formações específicas</param>
        /// <param name="seqEntidadeResponsavelFormacaoEspecifica">Sequencial da entidade responsável pelas formações específicas</param>
        /// <param name="desabilitarFiltro">Flag para desabilitar filtro de dados</param>
        /// <returns>Descrição e Seq da formação específica</returns>
        public List<(long SeqFormacaoEspecifica, string DescricaoFormacaoEspecifica, string TokenTipoFormacaoEspecifica)> BuscarDescricaoFormacaoEspecifica(List<long> seqsFormacoesEspecificas, long? seqEntidadeResponsavelFormacaoEspecifica, bool desabilitarFiltro = false)
        {
            // Verifica se deve desabilitar os filtros de dados.
            if (desabilitarFiltro)
            {
                AlunoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                IngressanteDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            List<FormacaoEspecificaNodeVO> formacoesHierarquia = new List<FormacaoEspecificaNodeVO>();
            if (seqEntidadeResponsavelFormacaoEspecifica.HasValue)
            {
                var listaSeqsEntidadesResponsaveis = new List<long>();
                listaSeqsEntidadesResponsaveis.Add(seqEntidadeResponsavelFormacaoEspecifica.Value);
                var formacoes = BuscarFormacoesEspecificasCabecalho(listaSeqsEntidadesResponsaveis);

                if (formacoes != null)
                    foreach (var formacao in seqsFormacoesEspecificas)
                        formacoesHierarquia.AddRange(GerarHierarquiaFormacaoEspecifica(formacao, formacoes));
            }

            // Verifica se deve habilitar os filtros de dados.
            if (desabilitarFiltro)
            {
                AlunoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                IngressanteDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            return formacoesHierarquia.Select(f => (f.Seq, $"[{f.DescricaoTipoFormacaoEspecifica}] {f.Descricao}", f.TokenTipoFormacaoEspecifica)).ToList();
        }

        /// <summary>
        /// Salva a uma formação específica
        /// </summary>
        /// <param name="model">Dados da formação específica para serem gravados</param>
        /// <returns>Sequencial da formação específica gravada</returns>
        public long SalvarFormacaoEspecifica(FormacaoEspecificaVO model)
        {
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var novaFormacaoEspecifica = model.Transform<FormacaoEspecifica>();

                    var formacoesEspecificasAtuais = this.BuscarFormacoesEspecificasEntidadeSuperior(new FormacaoEspecificaFiltroVO() { SeqsEntidadesResponsaveis = new long[] { model.SeqEntidadeResponsavel.GetValueOrDefault() } });

                    var formacaoEspecificaSuperior = this.SearchByKey(novaFormacaoEspecifica.SeqFormacaoEspecificaSuperior.GetValueOrDefault());

                    ValidarDescricaoFormacaoEspecificaPrograma(novaFormacaoEspecifica, formacoesEspecificasAtuais);

                    ValidarPaiInativoHierarquiaFormacaoEspecíficaPrograma(novaFormacaoEspecifica, formacaoEspecificaSuperior);

                    //Caso seja edição, realizar as validações conforme regra de negócios
                    //RN_CSO_008 - Cadastro - Formações Específicas do Programa
                    if (model.Seq > 0)
                    {
                        ValidarFilhoAtivoHierarquiaFormacaoEspecificaPrograma(novaFormacaoEspecifica, formacoesEspecificasAtuais);
                        ValidarAssociacaoCursoVigenteFormacaoEspecificaPrograma(novaFormacaoEspecifica);
                    }

                    //Salva a formação específica
                    this.SaveEntity(novaFormacaoEspecifica);

                    //Caso seja inclusão, envia notificação conforme regra de negócios
                    //RN_CSO_008 - Cadastro - Formações Específicas do Programa
                    if (model.Seq == 0)
                        EnviarNotificacaoNovaFormacaoEspecificaPrograma(model, novaFormacaoEspecifica);

                    unityOfWork.Commit();

                    return novaFormacaoEspecifica.Seq;
                }
                catch (Exception)
                {
                    unityOfWork.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Buscar formações específicas de acordo com o sequencial da entidade
        /// </summary>
        /// <param name="seqCurso">Sequencial da entidade responsável</param>
        /// <returns>Lista de foramação específica node</returns>
        public List<FormacaoEspecificaNodeVO> BuscarFormacoesEspecificas(FormacaoEspecificaFiltroVO filtro)
        {
            long? seqCursoFiltro = filtro.SeqCurso ?? 0;

            var specFormacoes = filtro.Transform<FormacaoEspecificaFilterSpecification>(new
            {
                Seq = filtro.SeqFormacaoEspecifica,
                SeqEntidades = filtro.SeqsEntidadesResponsaveis?.ToList()
            });

            IEnumerable<FormacaoEspecifica> formacoesEspecificas = new List<FormacaoEspecifica>();

            // Caso seja informado um programa, apenas retorna todas formações do programa
            if (filtro.SeqsEntidadesResponsaveis != null && filtro.SeqsEntidadesResponsaveis.Length == 1)
            {
                filtro.SelecaoNivelFolha = true;
                formacoesEspecificas = SearchBySpecification(specFormacoes,
                                                             IncludesFormacaoEspecifica.TipoFormacaoEspecifica |
                                                             IncludesFormacaoEspecifica.Cursos_GrauAcademico).ToList();
            }
            // Caso seja informado um curso, considera o filtro de formações por cursos e possivelmente a regra de exceção
            else if (filtro.SeqCurso.HasValue)
            {
                List<FormacaoEspecifica> formacoesEspecificasPrograma = BuscarFormacoesEspecificasEntidadeSuperior(filtro);

                // Caso seja informado um curso, será considerado se a formação está ou não ativa no curso
                formacoesEspecificas = BuscarFormacoesEspecificasCursoHirarquiaComTipo(filtro.SeqCurso.Value, filtro.Ativo, formacoesEspecificasPrograma);

                // Caso não encontre nenhuma formação específica da entidade superior, busca as formações do proprio curso (pode estar associado à um departamento e este pode não possuir formações específicas
                if (formacoesEspecificas == null || !formacoesEspecificas.Any())
                {
                    var spec = new CursoFormacaoEspecificaFilterSpecification { SeqCurso = filtro.SeqCurso };
                    var formacoesCurso = CursoFormacaoEspecificaDomainService.SearchBySpecification(spec,
                                                                                                    IncludesCursoFormacaoEspecifica.GrauAcademico
                                                                                                  | IncludesCursoFormacaoEspecifica.FormacaoEspecifica
                                                                                                  | IncludesCursoFormacaoEspecifica.FormacaoEspecifica_TipoFormacaoEspecifica).ToList();

                    formacoesEspecificas = formacoesCurso.Select(s => s.FormacaoEspecifica).TransformList<FormacaoEspecifica>();
                    formacoesEspecificasPrograma = formacoesEspecificas.ToList();
                }

                ///Quando informado o parâmetro ID da Formação Específica, será exibida as formações que estão associadas as
                ///formações enviadas como parâmetro no nível superior(pai) e inferior(filhos). Portanto nos seguintes casos a listagem
                ///das formações deverá ser realizada da seguinte forma: LK_CSO_003 - Formação Específica - NV04
                if (formacoesEspecificas.Any(a => a.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA))
                {
                    filtro.SeqTipoAreaTematicaExcessao = formacoesEspecificas.First(f => f.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA).SeqTipoFormacaoEspecifica;

                    var formacaoEspecificaCursoCompleta = VerificarArvoreCursoComArvorePrograma(formacoesEspecificasPrograma, formacoesEspecificas.ToList());

                    if (filtro.SeqFormacaoEspecifica.HasValue)
                    {
                        formacoesEspecificas = RegraExcecaoAreaTematica(filtro.SeqFormacaoEspecifica.Value, formacaoEspecificaCursoCompleta);
                        var seqsFormacoesSelecionadas = formacoesEspecificas.Select(s => s.Seq).ToArray();
                        formacoesEspecificas = RecuperarFormacoesEspecificasComFilhas(seqsFormacoesSelecionadas, formacoesEspecificasPrograma)
                            .Where(w => !filtro.Ativo.HasValue || w.Ativo == filtro.Ativo)
                            .SMCDistinct(d => d.Seq);
                    }
                    else
                    {
                        formacoesEspecificas = formacaoEspecificaCursoCompleta;
                    }
                }
                else if (filtro.SeqFormacaoEspecifica.HasValue)
                {
                    var seqsFormacoesSelecionadas = new[] { filtro.SeqFormacaoEspecifica.Value };
                    formacoesEspecificas = RecuperarFormacoesEspecificasComFilhas(seqsFormacoesSelecionadas, formacoesEspecificasPrograma)
                        .Where(w => !filtro.Ativo.HasValue || w.Ativo == filtro.Ativo)
                        .SMCDistinct(d => d.Seq);
                }

                if (filtro.SeqCursoOfertaLocalidade.HasValue)
                {
                    var specFormacoesCol = new FormacaoEspecificaFilterSpecification() { SeqCursoOfertaLocalidade = filtro.SeqCursoOfertaLocalidade };
                    var seqsFormacoesCursoOfertaLocalidade = SearchProjectionBySpecification(specFormacoesCol, p => p.Seq).ToList();
                    formacoesEspecificas = formacoesEspecificas.Where(w => seqsFormacoesCursoOfertaLocalidade.Contains(w.Seq));
                }

                // Caso tenha ocorrido algum filtro, reconstroi a hierarquia
                if (formacoesEspecificas.Count() != formacoesEspecificasPrograma.Count)
                {
                    var seqsFormacoesSelecionadas = formacoesEspecificas.Select(s => s.Seq).ToList();
                    formacoesEspecificas = RecuperarFormacoesEspecificasComSuperiores(seqsFormacoesSelecionadas, formacoesEspecificasPrograma);
                }
            }

            // Caso contrário, realiza apenas o filtro normal
            else
            {
                formacoesEspecificas = SearchBySpecification(specFormacoes,
                                                             IncludesFormacaoEspecifica.TipoFormacaoEspecifica |
                                                             IncludesFormacaoEspecifica.Cursos_GrauAcademico).ToList();
            }


            var voFormacoesEspecificas = formacoesEspecificas?.TransformList<FormacaoEspecificaNodeVO>()
                .OrderBy(o => o.DescricaoTipoFormacaoEspecifica)
                .ThenBy(o => o.Descricao)
                .ToList()
                ?? new List<FormacaoEspecificaNodeVO>();

            foreach (var voFormacaoEspecifica in voFormacoesEspecificas)
            {
                if (voFormacaoEspecifica.Cursos != null)
                    voFormacaoEspecifica.DescricaoGrauAcademico = voFormacaoEspecifica.Cursos.Where(w => w.SeqFormacaoEspecifica == voFormacaoEspecifica.Seq && w.SeqCurso == seqCursoFiltro && w.SeqGrauAcademico != null)
                                                                                             .Select(s => s.GrauAcademico.Descricao).FirstOrDefault();
            }

            ConfigurarSelecionaveis(voFormacoesEspecificas, filtro);

            ConfigurarCursoAssociado(voFormacoesEspecificas);

            ConfigurarItemFolha(voFormacoesEspecificas);

            return voFormacoesEspecificas.OrderByDescending(o => o.Ativo).ToList();
        }

        /// <summary>
        /// Valida se um formação especifica tem Sequencial de formacao especifica superior e retornar formação especifica superior
        /// </summary>
        /// <param name="formacoesEspecificas">Lista Formações especificas superiores</param>
        /// <returns>Lista de formações especificas superiores</returns>
        public List<long> ValidarRetornarFormacoesEspecificasSuperior(List<long> seqsFormacoesEspecificas)
        {
            List<long> retorno = new List<long>();

            foreach (var item in seqsFormacoesEspecificas)
            {
                var seqFormacaoEspecificaSuperior = this.SearchProjectionByKey(new SMCSeqSpecification<FormacaoEspecifica>(item), p => p.SeqFormacaoEspecificaSuperior);

                if (seqFormacaoEspecificaSuperior == null)
                {
                    retorno.Add(item);
                }
                else
                {
                    retorno.Add((long)seqFormacaoEspecificaSuperior);
                }
            }

            return retorno;
        }

        public List<FormacaoEspecifica> VerificarArvoreCursoComArvorePrograma(List<FormacaoEspecifica> listaFormacaoEspecificaPrograma, List<FormacaoEspecifica> listaFormacaoEspecificaCurso)
        {
            List<FormacaoEspecifica> retorno = new List<FormacaoEspecifica>();

            foreach (var formacaoCurso in listaFormacaoEspecificaCurso)
            {
                var formacaoPrograma = listaFormacaoEspecificaPrograma.FirstOrDefault(f => f.Seq == formacaoCurso.Seq);

                retorno.Add(formacaoPrograma);

                bool temSeqFormacaoEspecificaSuperior = formacaoPrograma.SeqFormacaoEspecificaSuperior.HasValue;

                long seqFormacaoEspecificaSuperior = temSeqFormacaoEspecificaSuperior ? formacaoPrograma.SeqFormacaoEspecificaSuperior.Value : 0;

                while (temSeqFormacaoEspecificaSuperior)
                {
                    var formacaoProgramaSuperior = listaFormacaoEspecificaPrograma.FirstOrDefault(f => f.Seq == formacaoPrograma.SeqFormacaoEspecificaSuperior);

                    if (!retorno.Any(a => a.Seq == seqFormacaoEspecificaSuperior))
                    {
                        retorno.Add(formacaoProgramaSuperior);
                    }

                    temSeqFormacaoEspecificaSuperior = formacaoProgramaSuperior.SeqFormacaoEspecificaSuperior.HasValue;

                    seqFormacaoEspecificaSuperior = temSeqFormacaoEspecificaSuperior ? formacaoProgramaSuperior.SeqFormacaoEspecificaSuperior.Value : 0;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Buscar formações específicas de acordo com o sequencial da entidade responsáveis para cabeçalho de ingressante
        /// </summary>
        /// <param name="SeqsEntidadesResponsaveis">Sequencial da entidade responsável</param>
        /// <returns>Lista de foramação específica node</returns>
        public List<FormacaoEspecificaNodeVO> BuscarFormacoesEspecificasCabecalho(List<long> seqsEntidadesResponsaveis)
        {
            IEnumerable<FormacaoEspecifica> formacoesEspecificas = null;
            FormacaoEspecificaFiltroVO filtro = new FormacaoEspecificaFiltroVO { SeqsEntidadesResponsaveis = seqsEntidadesResponsaveis.ToArray() };
            if (seqsEntidadesResponsaveis != null)
            {
                // Caso tenha sido informados ids de programas
                // Busca as formações das entidades informadas (vem todas as formações, logo, já possui os dados para montar a árvore)
                formacoesEspecificas = this.SearchBySpecification(new FormacaoEspecificaFilterSpecification
                {
                    SeqEntidades = seqsEntidadesResponsaveis,
                }, IncludesFormacaoEspecifica.TipoFormacaoEspecifica);
            }

            // Validações para buscar os pais ou filhos
            var voFormacoesEspecificas = formacoesEspecificas?.TransformList<FormacaoEspecificaNodeVO>() ?? new List<FormacaoEspecificaNodeVO>();

            return voFormacoesEspecificas;
        }

        private void ConfigurarCursoAssociado(List<FormacaoEspecificaNodeVO> formacoes)
        {
            var specFormacaoEspecifica = new FormacaoEspecificaFilterSpecification() { Seqs = formacoes.Select(f => f.Seq).ToList() };

            var formacoesEspecificas = SearchProjectionBySpecification(specFormacaoEspecifica, f => new
            {
                f.Seq,
                PossuiCursoAssociado = f.Cursos.Any(),
                PossuiCursoAssociadoFilhas = f.FormacoesEspecificasFilhas.Any(c => c.Cursos.Any()),
            }).ToList();

            foreach (var formacao in formacoes.OrderBy(o => o.Seq))
            {
                var formacaoEspecifica = formacoesEspecificas.FirstOrDefault(f => f.Seq == formacao.Seq);

                switch (formacao.TokenTipoFormacaoEspecifica)
                {
                    case TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO:
                        formacao.PossuiCursoAssociado = formacaoEspecifica.PossuiCursoAssociadoFilhas;
                        break;

                    default:
                        formacao.PossuiCursoAssociado = formacaoEspecifica.PossuiCursoAssociado;
                        break;
                }

                AlteraCursoAssociadoHierarquia(formacao, formacoes);
            }
        }

        private void AlteraCursoAssociadoHierarquia(FormacaoEspecificaNodeVO formacao, List<FormacaoEspecificaNodeVO> formacoes)
        {
            if (formacao.PossuiCursoAssociado && formacao.SeqFormacaoEspecificaSuperior.HasValue)
            {
                var formacaoPai = formacoes.FirstOrDefault(f => f.Seq == formacao.SeqFormacaoEspecificaSuperior);

                formacaoPai.PossuiCursoAssociado = formacao.PossuiCursoAssociado;

                AlteraCursoAssociadoHierarquia(formacaoPai, formacoes);
            }
        }

        private void ConfigurarItemFolha(List<FormacaoEspecificaNodeVO> formacoes)
        {
            formacoes.ForEach(f =>
            {
                f.TipoFormacaoEspecificaFolha = !formacoes.Any(w => w.SeqFormacaoEspecificaSuperior == f.Seq);
            });
        }

        private void ConfigurarSelecionaveis(List<FormacaoEspecificaNodeVO> formacoes, FormacaoEspecificaFiltroVO filtro)
        {
            ///Regra de seleção de nivel superior
            var hierarquiaTipoFormacao = InstituicaoTipoEntidadeFormacaoEspecificaDomainService.BuscarHierarquiaTipoFormacaoPorTipoEntidade(TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA);
            var dicNivelHirarquiaFormacao = hierarquiaTipoFormacao.ToDictionary(key => key.SeqTipoFormacaoEspecifica, value => RecuperarNivel(hierarquiaTipoFormacao, value.SeqTipoFormacaoEspecifica));
            var formacaoCursoOferta = formacoes.FirstOrDefault(s => s.Seq == filtro.SeqFormacaoEspecifica);

            var nivelSelecionavel = 0;

            if (formacaoCursoOferta != null && dicNivelHirarquiaFormacao.ContainsKey(formacaoCursoOferta.SeqTipoFormacaoEspecifica))
            {
                nivelSelecionavel = dicNivelHirarquiaFormacao[formacaoCursoOferta.SeqTipoFormacaoEspecifica];
            }

            foreach (var formacao in formacoes)
            {
                ///Regra para folha selecionada
                if (filtro.SelecaoNivelFolha.GetValueOrDefault())
                {
                    formacao.Selecionavel = !formacoes.Any(s => s.SeqFormacaoEspecificaSuperior == formacao.Seq);
                }///Regra para impedir seleção de nivel superior
				else if (filtro.SeqFormacaoEspecifica.HasValue && filtro.SelecaoNivelSuperior.HasValue && !filtro.SelecaoNivelSuperior.Value)
                {
                    formacao.Selecionavel = dicNivelHirarquiaFormacao[formacao.SeqTipoFormacaoEspecifica] >= nivelSelecionavel;
                }
                else
                {
                    formacao.Selecionavel = true;
                }

                if (filtro.SeqTipoFormacaoEspecifica.SMCAny())
                {
                    formacao.Selecionavel = formacao.Selecionavel && filtro.SeqTipoFormacaoEspecifica.Contains(formacao.SeqTipoFormacaoEspecifica);
                }

                ///Atendendo a regra de excessão onde todas areas tematicas seram sempre selecionaveis
                if (filtro.SeqTipoAreaTematicaExcessao.HasValue)
                {
                    formacao.Selecionavel = formacao.Selecionavel || formacao.SeqTipoFormacaoEspecifica == filtro.SeqTipoAreaTematicaExcessao.Value;
                }
            }
        }

        /// <summary>
        /// Recupera o ultimo nível dos tipos de formação informados e retorna estes com seus filhos segundo a hierarquia de tipo de formação por programa
        /// </summary>
        /// <param name="seqTipoFormacao">Tipos de formação específica</param>
        /// <returns>Lista com os tipos informados e seus filhos</returns>
        public List<long> BuscarTipoFormacaoUltimoNivelAtualPrograma(List<long> seqTipoFormacao)
        {
            var hierarquiaTipoFormacao = InstituicaoTipoEntidadeFormacaoEspecificaDomainService.BuscarHierarquiaTipoFormacaoPorTipoEntidade(TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA);
            var dicNivelHirarquiaFormacao = hierarquiaTipoFormacao.ToDictionary(key => key.SeqTipoFormacaoEspecifica, value => RecuperarNivel(hierarquiaTipoFormacao, value.SeqTipoFormacaoEspecifica));
            int nivel = 0;
            var nivelAtual = seqTipoFormacao.SMCAny() ? seqTipoFormacao.Select(s => dicNivelHirarquiaFormacao.TryGetValue(s, out nivel) ? nivel : -1).Max() : -1;
            return dicNivelHirarquiaFormacao.Where(w => w.Value >= nivelAtual).Select(s => s.Key).ToList();
        }

        /// <summary>
        /// Buscar formações específica de uma entidade responsável com suas hierarquias definidas
        /// </summary>
        /// <param name="seqFormacoesEspecificas">Sequencial de formação específica</param>
        /// <returns>Lista de formações específicas com as hierarquias definidas</returns>
        public List<FormacaoEspecificaHierarquiaVO> BuscarFormacoesEspecificasHierarquia(long[] seqFormacoesEspecificas)
        {
            var formacoesEspecificas = this.SearchBySpecification(new SMCTrueSpecification<FormacaoEspecifica>(),
                                                                  IncludesFormacaoEspecifica.TipoFormacaoEspecifica |
                                                                  IncludesFormacaoEspecifica.Cursos_GrauAcademico)
                .ToList();
            var specSelecionados = new SMCContainsSpecification<FormacaoEspecifica, long>(p => p.Seq, seqFormacoesEspecificas);
            var formacoesSelecionadas = this.SearchBySpecification(specSelecionados);
            var hierarquiaData = formacoesSelecionadas
                .OrderBy(o => o.Descricao)
                .Select(s => new FormacaoEspecificaHierarquiaVO() { Seq = s.Seq, Descricao = s.Descricao, Hierarquia = GerarHierarquiaFormacaoEspecifica(s.Seq, formacoesEspecificas) })
                .ToList();

            hierarquiaData = this.IngressanteDomainService.FiltrarItensHirarquia(hierarquiaData);

            // Replica os tipos para as raízes da hierarquia
            hierarquiaData.SMCForEach(f => f.DescricaoTipoFormacaoEspecifica = f.Hierarquia.FirstOrDefault(fh => fh.Seq == f.Seq)?.DescricaoTipoFormacaoEspecifica);

            return hierarquiaData;
        }

        /// <summary>
        /// Recupera as formações específicas do tipo "linha de pesquisa" associadas aos programas filhos de um grupo de programas
        /// </summary>
        /// <param name="seqGrupoPrograma">Sequencial do grupo de progrmas</param>
        /// <returns>Dados das formações específias que atendam aos critérios</returns>
        public List<SMCDatasourceItem> BuscarLinhasDePesquisaGrupoPrograma(long seqGrupoPrograma)
        {
            // Recupera o sequencial dos programas
            var specHierarquiaProgramas = new HierarquiaEntidadeItemFilterSpecification() { SeqEntidadeSuperior = seqGrupoPrograma };
            var seqsProgramas = this.HierarquiaEntidadeItemDomainService
                .SearchProjectionBySpecification(specHierarquiaProgramas, p => p.SeqEntidade)
                .ToList();

            // Caso não tenha nenhum programa devolve uma lista vazia
            if (seqsProgramas.Count == 0)
            {
                return new List<SMCDatasourceItem>();
            }

            // Lista todas formações específicas do tipo "linha de pesquisa" e filhas dos programas filhos do grupo seqGrupoProgrma
            var specLinhasPesquisaGrupoPrograma = new FormacaoEspecificaFilterSpecification()
            {
                SeqEntidades = seqsProgramas,
                TokenTipoFormacaoEspecifica = TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_PESQUISA
            };

            specLinhasPesquisaGrupoPrograma.SetOrderBy(o => o.Descricao);

            var linhasPesquisa = this.SearchProjectionBySpecification(specLinhasPesquisaGrupoPrograma, p =>
                new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.EntidadeResponsavel.Sigla + " - " + p.Descricao })
                .ToList();

            return linhasPesquisa;
        }

        public FormacaoEspecificaNodeVO BuscarPrimeiroNivelFormacaoEspecifica(long seqFormacaoEspecifica)
        {
            FormacaoEspecificaNodeVO formacaoEspecifica;
            do
            {
                formacaoEspecifica = SearchProjectionByKey(new SMCSeqSpecification<FormacaoEspecifica>(seqFormacaoEspecifica),
                                                    x => new FormacaoEspecificaNodeVO
                                                    {
                                                        Seq = x.Seq,
                                                        SeqFormacaoEspecificaSuperior = x.SeqFormacaoEspecificaSuperior,
                                                        Descricao = x.Descricao
                                                    });
                seqFormacaoEspecifica = formacaoEspecifica.SeqFormacaoEspecificaSuperior.GetValueOrDefault();
            } while (formacaoEspecifica.SeqFormacaoEspecificaSuperior.HasValue);

            return formacaoEspecifica;
        }

        public bool FormacaoEspefificaExigeGrau(long seqFormacaoEspecifica)
        {
            var exigeGrau = this.SearchProjectionByKey(seqFormacaoEspecifica, p => p.TipoFormacaoEspecifica.ExigeGrau);

            return exigeGrau;
        }

        /// <summary>
        /// Gerar uma lista de hierarquias de formação especifica de acordo com o ienumerable de formação específica
        /// </summary>
        /// <param name="seqFormacao">Sequencial da formação específica atual</param>
        /// <param name="formacoesEspecificas">Formações específicas da hierarquia</param>
        /// <returns></returns>
        public List<FormacaoEspecificaListaVO> GerarHierarquiaFormacaoEspecifica(long seqFormacao, IEnumerable<FormacaoEspecifica> formacoesEspecificas)
        {
            var hierarquia = new Stack<FormacaoEspecificaListaVO>();
            FormacaoEspecifica formacaoEspecificaAtual;

            do
            {
                formacaoEspecificaAtual = formacoesEspecificas.FirstOrDefault(s => s.Seq == seqFormacao);

                if (formacaoEspecificaAtual == null)
                    break;

                hierarquia.Push(new FormacaoEspecificaListaVO()
                {
                    Seq = formacaoEspecificaAtual.Seq,
                    Descricao = formacaoEspecificaAtual.Descricao,
                    DescricaoTipoFormacaoEspecifica = formacaoEspecificaAtual.TipoFormacaoEspecifica.Descricao,
                    TokenTipoFormacaoEspecifica = formacaoEspecificaAtual.TipoFormacaoEspecifica.Token,
                    SeqFormacaoEspecificaSuperior = formacaoEspecificaAtual.SeqFormacaoEspecificaSuperior,
                    SeqTipoFormacaoEspecifica = formacaoEspecificaAtual.SeqTipoFormacaoEspecifica,
                    DescricaoGrau = formacaoEspecificaAtual.TipoFormacaoEspecifica.ExibeGrauDescricaoFormacao ?
                    formacaoEspecificaAtual.Cursos.FirstOrDefault(f => f.SeqFormacaoEspecifica == formacaoEspecificaAtual.Seq && f.SeqGrauAcademico != null).GrauAcademico.Descricao : string.Empty
                });
                seqFormacao = formacaoEspecificaAtual.SeqFormacaoEspecificaSuperior ?? 0;
            } while (seqFormacao != 0);

            return hierarquia.ToList();
        }

        public List<FormacaoEspecificaNodeVO> GerarHierarquiaFormacaoEspecifica(long seqFormacao, IEnumerable<FormacaoEspecificaNodeVO> formacoesEspecificas)
        {
            var hierarquia = new Stack<FormacaoEspecificaNodeVO>();
            FormacaoEspecificaNodeVO formacaoEspecificaAtual;

            do
            {
                formacaoEspecificaAtual = formacoesEspecificas.FirstOrDefault(s => s.Seq == seqFormacao);
                hierarquia.Push(new FormacaoEspecificaNodeVO()
                {
                    Seq = formacaoEspecificaAtual.Seq,
                    Descricao = formacaoEspecificaAtual.Descricao,
                    DescricaoTipoFormacaoEspecifica = formacaoEspecificaAtual.DescricaoTipoFormacaoEspecifica,
                    SeqFormacaoEspecificaSuperior = formacaoEspecificaAtual.SeqFormacaoEspecificaSuperior,
                    TokenTipoFormacaoEspecifica = formacaoEspecificaAtual.TokenTipoFormacaoEspecifica,
                    SeqTipoFormacaoEspecifica = formacaoEspecificaAtual.SeqTipoFormacaoEspecifica
                });
                seqFormacao = formacaoEspecificaAtual.SeqFormacaoEspecificaSuperior ?? 0;
            } while (seqFormacao != 0);

            return hierarquia.ToList();
        }

        /// <summary>
        /// Gerar uma lista de hierarquias invertida de formação especifica de acordo com o ienumerable de formação específica
        /// </summary>
        /// <param name="seqFormacao">Sequencial da formação específica atual</param>
        /// <param name="formacoesEspecificas">Formações específicas da hierarquia</param>
        /// <returns></returns>
        public List<FormacaoEspecificaListaVO> GerarHierarquiaInvertidaFormacaoEspecifica(long seqFormacao, IEnumerable<FormacaoEspecifica> formacoesEspecificas)
        {
            var hierarquia = new List<FormacaoEspecificaListaVO>();
            long? seqPai = null;
            FormacaoEspecifica formacaoEspecificaAtual;
            do
            {
                formacaoEspecificaAtual = formacoesEspecificas.Single(s => s.Seq == seqFormacao);
                hierarquia.Add(new FormacaoEspecificaListaVO()
                {
                    Seq = formacaoEspecificaAtual.Seq,
                    Descricao = formacaoEspecificaAtual.Descricao,
                    DescricaoTipoFormacaoEspecifica = formacaoEspecificaAtual.TipoFormacaoEspecifica.Descricao,
                    SeqFormacaoEspecificaSuperior = seqPai
                });
                seqPai = seqFormacao;
                seqFormacao = formacaoEspecificaAtual.SeqFormacaoEspecificaSuperior ?? 0;
            } while (seqFormacao != 0);

            return hierarquia;
        }

        public bool BloquearCampoFormacaoEspecifica(long? seqTipoOferta, long? seqCursoOferta, List<long> seqsEntidadesResponsaveis)
        {
            //Se não foi selecionadoa tipo de oferta
            if (!seqTipoOferta.HasValue)
                return true; //bloquear o campo na tela

            //Recupera o tipo de oferta
            var tipoferta = this.TipoOfertaDomainService.SearchByKey(new SMCSeqSpecification<TipoOferta>(seqTipoOferta.Value));

            //Se o tipo de oferta tiver tipo formacao específica associado, então:
            if (tipoferta.SeqTipoFormacaoEspecifica.HasValue)
            {
                //Se foram selecionadas entidades responsáveis
                if (seqsEntidadesResponsaveis != null && seqsEntidadesResponsaveis.Count > 0)
                {
                    //Para cada entidade responsavel selecionada, verifica se é do tipo PROGRAMA
                    //caso alguma seja, retorna true
                    foreach (var item in seqsEntidadesResponsaveis)
                    {
                        //Caso retorne uma entidade, valida o token para saber se é um programa
                        var programa = this.EntidadeDomainService.BuscarEntidade(item);
                        if (programa != null && programa.TipoEntidade.Token == TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA)
                            return false; // liberar o campo na tela
                    }
                }

                //Se o tipo oferta tiver sido selecionado, retorna true
                if (seqCursoOferta.HasValue)
                    return false; // liberar o campo na tela
            }

            return true; //bloquer o campo na tela
        }

        /// <summary>
        /// Busca as formações específicas associadas ao curso e as formações específicas superiores associadas a entidade superior ao curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Formações específicas do curso e suas formações superiores</returns>
        public List<FormacaoEspecificaListaVO> BuscarFormacoesEspecificasCursoComHierarquia(long seqCurso)
        {
            var specCurso = new SMCSeqSpecification<Curso>(seqCurso);

            // Busca as formações especificas que estão associadas ao curso
            var formacaoEspecificaCurso = CursoDomainService.SearchProjectionByKey(specCurso,
                p => p.CursosFormacaoEspecifica.Select(f => new FormacaoEspecificaListaVO()
                {
                    Seq = f.SeqFormacaoEspecifica,
                    SeqFormacaoEspecificaSuperior = f.FormacaoEspecifica.SeqFormacaoEspecificaSuperior,
                    SeqTipoFormacaoEspecifica = f.FormacaoEspecifica.SeqTipoFormacaoEspecifica,
                    Descricao = f.FormacaoEspecifica.Descricao,
                    DescricaoTipoFormacaoEspecifica = f.FormacaoEspecifica.TipoFormacaoEspecifica.Descricao,
                    Vigente = f.DataInicioVigencia <= DateTime.Today && (!f.DataFimVigencia.HasValue || f.DataFimVigencia >= DateTime.Today)
                })).Where(w => w.Vigente).ToList();

            // Tipos de formações especificas da entidade responsável pelo curso do ingressante
            var formacoesEspecificaEntidadeResponsavelCurso = CursoDomainService.SearchProjectionByKey(specCurso,
                // Considera que existe apenas UMA entidade responsável pelo curso
                p => p.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade
                      .FormacoesEspecificasEntidade
                      .Select(s => new FormacaoEspecificaListaVO()
                      {
                          Seq = s.Seq,
                          SeqFormacaoEspecificaSuperior = s.SeqFormacaoEspecificaSuperior,
                          SeqTipoFormacaoEspecifica = s.SeqTipoFormacaoEspecifica,
                          Descricao = s.Descricao,
                          DescricaoTipoFormacaoEspecifica = s.TipoFormacaoEspecifica.Descricao
                      }))
                      .ToList();

            // As formações específicas do curso contêm apenas os itens folha da hierarquia de formações específicas do seu programa.
            // Completa a lista de formações específicas do curso com os itens da hierarquia do programa.
            do
            {
                var seqsFormacoesSuperioresNaoAssociadas = formacaoEspecificaCurso
                    .Where(s => s.SeqFormacaoEspecificaSuperior.HasValue)
                    .Select(s => s.SeqFormacaoEspecificaSuperior.Value)
                    .Except(formacaoEspecificaCurso.Select(s => s.Seq))
                    .ToList();

                if (!seqsFormacoesSuperioresNaoAssociadas.Any())
                    break;

                formacaoEspecificaCurso.AddRange(formacoesEspecificaEntidadeResponsavelCurso
                    .Where(w => seqsFormacoesSuperioresNaoAssociadas.Contains(w.Seq)));
            } while (true);

            return formacaoEspecificaCurso;
        }

        /// <summary>
        /// Busca as formações específicas associadas a uma entidade e aninha o resultado conforme a hierarquia
        /// </summary>
        /// <param name="seqEntidade">Sequencial da entidade</param>
        /// <returns>Formações epspecíficas aninhadas conforme sua hierarquia</returns>
        public List<FormacaoEspecificaNodeVO> BuscarFormacoesEspecificasHierarquia(long seqEntidade)
        {
            var formacoes = SearchProjectionBySpecification(
                new FormacaoEspecificaFilterSpecification() { SeqEntidadeResponsavel = seqEntidade },
                p => new FormacaoEspecificaNodeVO()
                {
                    Seq = p.Seq,
                    SeqTipoFormacaoEspecifica = p.SeqTipoFormacaoEspecifica,
                    SeqFormacaoEspecificaSuperior = p.SeqFormacaoEspecificaSuperior
                }).ToList();

            foreach (var formacao in formacoes)
            {
                formacao.FormacoesFilhas = new List<FormacaoEspecificaNodeVO>();
                foreach (var formacaoFilha in formacoes.Where(w => w.SeqFormacaoEspecificaSuperior == formacao.Seq))
                {
                    formacaoFilha.FormacaoPai = formacao;
                    formacao.FormacoesFilhas.Add(formacaoFilha);
                }
            }
            return formacoes.Where(w => !w.SeqFormacaoEspecificaSuperior.HasValue).ToList();
        }

        /// <summary>
        /// Recupera um item e todos seus itens superiores na hieraquia
        /// </summary>
        /// <param name="seqFormacaoEspecifica">Sequencial da formação especifica folha</param>
        /// <param name="hierarquia">Hirarquia completa das formações especificas</param>
        /// <returns>Item com o sequencial informado e todos seus itens superiores</returns>
        public IEnumerable<FormacaoEspecificaNodeVO> RecuperarFormacaoEspecificaComItensSuperiores(long seqFormacaoEspecifica, List<FormacaoEspecificaNodeVO> hierarquia)
        {
            var item = RecuperarFormacaoEspecificaNaHierarquia(seqFormacaoEspecifica, hierarquia);
            while (item != null)
            {
                yield return item;
                item = item.FormacaoPai;
            }
        }

        /// <summary>
        /// Recupera a formação específica informada navegando na hierarquia
        /// </summary>
        /// <param name="seqFormacaoEspecifica">Sequencial da formação específica</param>
        /// <param name="hierarquia">Hierarquia de formações</param>
        /// <returns>Formação com o sequencial informado ou null caso nenhuma seja encontrada</returns>
        public FormacaoEspecificaNodeVO RecuperarFormacaoEspecificaNaHierarquia(long seqFormacaoEspecifica, List<FormacaoEspecificaNodeVO> hierarquia)
        {
            if (!hierarquia.SMCAny())
                return null;
            foreach (var item in hierarquia)
            {
                if (item.Seq == seqFormacaoEspecifica)
                    return item;
                var itemFilho = RecuperarFormacaoEspecificaNaHierarquia(seqFormacaoEspecifica, item.FormacoesFilhas);
                if (itemFilho != null)
                    return itemFilho;
            }
            return null;
        }

        /// <summary>
        /// Recupera todas formações específicas associadas ao curso com e suas formações superiores
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Formações especificas com tipo</returns>
        public List<FormacaoEspecifica> BuscarFormacoesEspecificasCursoHirarquiaComTipo(long seqCurso, bool? ativo, IEnumerable<FormacaoEspecifica> formacoesPrograma)
        {
            // Recupera as formações associadas ao curso e a hierarquia completa de foramções do programa (entidade responsável pelo curso)
            var formacoesCurso = CursoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Curso>(seqCurso), p =>
                p.CursosFormacaoEspecifica.Select(s => new
                {
                    s.SeqFormacaoEspecifica,
                    s.DataInicioVigencia,
                    s.DataFimVigencia
                }).ToList());

            // Filtra apenas as formações associadas ao curso que atendam a regra "ativo"
            var seqsFormacoes = ativo.GetValueOrDefault() ?
                formacoesCurso.Where(w => (w.DataInicioVigencia <= DateTime.Today
                                          && (!w.DataFimVigencia.HasValue || DateTime.Today <= w.DataFimVigencia))
                                          || (w.DataInicioVigencia > DateTime.Today))
                              .Select(s => s.SeqFormacaoEspecifica) :
                formacoesCurso.Select(s => s.SeqFormacaoEspecifica);

            /*Se não existir formações específicas ativas, não será possível recuperar o item superior, e no método 
            AlteraCursoAssociadoHierarquia é gerado um erro sem tratamento*/
            if (!seqsFormacoes.ToList().Any())
                throw new CursoFormacaoEspecificaSemFormacaoAtivaException();

            return RecuperarFormacoesEspecificasComSuperiores(seqsFormacoes, formacoesPrograma).ToList();
        }

        /// <summary>
        /// Busca a estrutura de árvore das formações específicas da formação filho e pai para consulta de integralização
        /// </summary>
        /// <param name="seqFormacoes">Sequenciais das formações específicas filho e pai</param>
        /// <returns>Lista com a árvore de sequenciais das formações específicas</returns>
        public List<long> BuscarInformacaoTreeFormacaoEspecificaSequenciais(List<AlunoFormacaoVO> alunoFormacoes, long seqCurso)
        {
            List<long> Seqs = new List<long>();
            List<long> SeqFormacoes = alunoFormacoes.Select(s => s.SeqFormacaoEspecifica).ToList();

            var formacoesFilho = RawQuery<long>(string.Format(_buscarTreeFormacoesEspecificaPorFormacaoFilho, string.Join(" , ", SeqFormacoes)));
            Seqs.AddRange(formacoesFilho);

            var formacoesPai = RawQuery<long>(string.Format(_buscarTreeFormacoesEspecificaPorFormacaoPai, string.Join(" , ", SeqFormacoes)));
            Seqs.AddRange(formacoesPai);

            if ((alunoFormacoes.All(a => a.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA))
                || (!alunoFormacoes.Any(a => a.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA)))
            {
                var formacoesTreeCompletaCurso = BuscarFormacoesEspecificas(new FormacaoEspecificaFiltroVO() { SeqCurso = seqCurso, Ativo = true });

                if (alunoFormacoes.All(a => a.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA))
                {
                    var seqsDiferente = formacoesTreeCompletaCurso.Where(w => w.TokenTipoFormacaoEspecifica != TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA).Select(s => s.Seq).ToList();
                    Seqs.AddRange(seqsDiferente);
                }
                else
                {
                    var seqsIgual = formacoesTreeCompletaCurso.Where(w => w.TokenTipoFormacaoEspecifica == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA).Select(s => s.Seq).ToList();
                    Seqs.AddRange(seqsIgual);
                }
            }
            return Seqs;
        }

        private int RecuperarNivel(List<InstituicaoTipoEntidadeFormacaoEspecificaVO> hierarquia, long seqTipoFormacao, int nivelAnterior = 0)
        {
            var formacao = hierarquia.FirstOrDefault(f => f.SeqTipoFormacaoEspecifica == seqTipoFormacao);
            return !formacao.SeqPai.HasValue ? nivelAnterior : RecuperarNivel(hierarquia, formacao.SeqPai.Value, nivelAnterior + 1);
        }

        /// <summary>
        /// Recupera as formações específicas do programa informado no filtro ou do programa resposável pelo curso informado no filtro
        /// </summary>
        /// <param name="filtro">Dados do programa ou curso</param>
        /// <returns>Formações específicas do programa</returns>
        private List<FormacaoEspecifica> BuscarFormacoesEspecificasEntidadeSuperior(FormacaoEspecificaFiltroVO filtro)
        {
            List<FormacaoEspecifica> formacoesEspecificas;

            // Caso não seja passado nenhuma entidade superior, recupera o programa/departamento/etc do Curso ou CursoOfertaLocalidade informado
            long[] seqsEntidadesSuperiores = new long[] { };
            if (!filtro.SeqsEntidadesResponsaveis.SMCAny())
            {
                if (filtro.SeqCurso.HasValue)
                {
                    seqsEntidadesSuperiores = new long[] {
                        CursoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Curso>(filtro.SeqCurso.Value),
                                                                 p => p.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade)
                    };
                }
                else
                {
                    throw new SMCApplicationException("Curso, departamento ou programa não informado");
                }
            }
            else
            {
                seqsEntidadesSuperiores = filtro.SeqsEntidadesResponsaveis.ToArray();
            }
            // Recupera as formações de todos programas/departamentos/entidades superiores (normalmente é passado apenas um programa/departamento)
            var specSuperior = new SMCContainsSpecification<Entidade, long>(p => p.Seq, seqsEntidadesSuperiores);
            var superiores = EntidadeDomainService.SearchBySpecification(specSuperior,
                                                                         IncludesEntidade.FormacoesEspecificasEntidade_TipoFormacaoEspecifica |
                                                                         IncludesEntidade.FormacoesEspecificasEntidade_Cursos_GrauAcademico);
            formacoesEspecificas = new List<FormacaoEspecifica>();
            foreach (var superior in superiores)
            {
                formacoesEspecificas.AddRange(superior.FormacoesEspecificasEntidade);
            }

            return formacoesEspecificas;
        }

        /// <summary>
        /// Aplica a regra de exceção de formações específica para cursos com áreas temáticas
        /// </summary>
        /// <param name="SeqFormacaoEspecifica">Sequencial da formação específica ancora</param>
        /// <param name="formacaoEspecificaCursoCompleta">Formações específicas filtradas pelo curso</param>
        /// <returns>Formação específica do tipo da ancora e as formações do tipo oposto</returns>
        private static IEnumerable<FormacaoEspecifica> RegraExcecaoAreaTematica(long SeqFormacaoEspecifica, List<FormacaoEspecifica> formacaoEspecificaCursoCompleta)
        {
            ///Se houver formações especificas ele ira montar somente os dados avore com as folhas e posteriormente será montada o restante
            var formacaoCursoOferta = formacaoEspecificaCursoCompleta.FirstOrDefault(f => f.Seq == SeqFormacaoEspecifica);
            if (formacaoCursoOferta?.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA)
            {
                var areaTematicaSelecionada = formacaoCursoOferta;
                var areasConcentracao = formacaoEspecificaCursoCompleta.Where(w => w.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO).ToList();
                var formacoesAreaTematicaComAreasConcentracao = new List<FormacaoEspecifica>
                    {
                        areaTematicaSelecionada
                    };
                formacoesAreaTematicaComAreasConcentracao.AddRange(areasConcentracao);
                return formacoesAreaTematicaComAreasConcentracao;
            }
            else
            {
                var areaConentracaoSelecionada = formacaoCursoOferta;
                var areasTematicas = formacaoEspecificaCursoCompleta.Where(w => w.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA).ToList();
                var formacoesAreaConcentracaoComAreasTematicas = new List<FormacaoEspecifica>
                    {
                        areaConentracaoSelecionada
                    };
                formacoesAreaConcentracaoComAreasTematicas.AddRange(areasTematicas);
                return formacoesAreaConcentracaoComAreasTematicas;
            }
        }

        /// <summary>
        /// Recupera as formações com os sequenciais informados e todas suas superiores
        /// </summary>
        /// <param name="seqsFormacoes">Sequenciais de formações específicas</param>
        /// <param name="formacoesPrograma">Hierarquia completa de formações específicas do progroma</param>
        /// <returns>Formações específicas informadas e todas suas superiores</returns>
        private IEnumerable<FormacaoEspecifica> RecuperarFormacoesEspecificasComSuperiores(IEnumerable<long> seqsFormacoes, IEnumerable<FormacaoEspecifica> formacoesPrograma)
        {
            if (!seqsFormacoes.SMCAny())
                return new List<FormacaoEspecifica>();
            var formacoesInformadas = formacoesPrograma.Where(w => seqsFormacoes.Contains(w.Seq));
            var seqsSuperiores = formacoesInformadas.Where(w => w.SeqFormacaoEspecificaSuperior.HasValue).Select(s => s.SeqFormacaoEspecificaSuperior.Value);
            var formacoesSuperiores = RecuperarFormacoesEspecificasComSuperiores(seqsSuperiores, formacoesPrograma);
            return formacoesInformadas.Union(formacoesSuperiores).SMCDistinct(d => d.Seq);
        }

        /// <summary>
        /// Recupera as formações com os sequenciais informados e todas suas filhas
        /// </summary>
        /// <param name="seqsFormacoes">Sequenciais de formações específicas</param>
        /// <param name="formacoesPrograma">Hierarquia completa de formações específicas do progroma</param>
        /// <returns>Formações específicas informadas e todas suas filhas</returns>
        private IEnumerable<FormacaoEspecifica> RecuperarFormacoesEspecificasComFilhas(IEnumerable<long> seqsFormacoes, IEnumerable<FormacaoEspecifica> formacoesPrograma)
        {
            if (!seqsFormacoes.SMCAny())
                return new List<FormacaoEspecifica>();
            var formacoesInformadas = formacoesPrograma.Where(w => seqsFormacoes.Contains(w.Seq));
            var seqsFormacoesFilhas = formacoesPrograma.Where(w => w.SeqFormacaoEspecificaSuperior.HasValue && seqsFormacoes.Contains(w.SeqFormacaoEspecificaSuperior.Value)).Select(s => s.Seq);
            var formacoesFilhas = RecuperarFormacoesEspecificasComFilhas(seqsFormacoesFilhas, formacoesPrograma);
            return formacoesInformadas.Union(formacoesFilhas).SMCDistinct(d => d.Seq);
        }

        /* Não será permitido associar Formações Específicas com a mesma Descrição, mais de uma vez para o mesmo Programa.
		 * Em caso de violação, exibir mensagem: "Não é permitido associar Formações Específicas iguais para o mesmo programa."
		 */

        private void ValidarDescricaoFormacaoEspecificaPrograma(FormacaoEspecifica novaFormacaoEspeficica, List<FormacaoEspecifica> formacoesEspecificasAtuais)
        {
            if (formacoesEspecificasAtuais.Where(f => f.Seq != novaFormacaoEspeficica.Seq).Any(f => f.Descricao.ToUpper().Trim() == novaFormacaoEspeficica.Descricao.ToUpper().Trim()))
                throw new FormacaoEspecificaMesmaDescricaoProgramaException();
        }

        /*Não será permitido desativar uma Formação-Específica, se na hierarquia de Formação do programa em questão,
		  houver pelo menos 1(um) um filho que esteja ativo. Em caso de violação, exibir a seguinte mensagem impeditiva:
		  "Não é permitido desativar uma formação específica que possua pelo menos um item folha que esteja ativo." */

        private void ValidarFilhoAtivoHierarquiaFormacaoEspecificaPrograma(FormacaoEspecifica novaFormacaoEspecifica, List<FormacaoEspecifica> formacoesEspecificasAtuais)
        {
            //Se está desativando
            if (!novaFormacaoEspecifica.Ativo.GetValueOrDefault())
            {
                if (formacoesEspecificasAtuais.Where(f => f.SeqFormacaoEspecificaSuperior == novaFormacaoEspecifica.Seq).Any(f => f.Ativo.GetValueOrDefault()))
                    throw new FormacaoEspecificaItemFolhaAtivoException();
            }
        }

        /*Não será permitido ativar uma Formação-Específica, se na hierarquia de Formação do programa em questão,
		 o respectivo pai estiver inativo. Em caso de violação, exibir a seguinte memsagem impeditiva:
		 "Não é permitido ativar uma formação específica que a formação pai esteja inativa."*/

        private void ValidarPaiInativoHierarquiaFormacaoEspecíficaPrograma(FormacaoEspecifica novaFormacaoEspecifica, FormacaoEspecifica formacaoEspecificaSuperior)
        {
            //Se está ativando
            if (novaFormacaoEspecifica.Ativo.GetValueOrDefault() && formacaoEspecificaSuperior != null)
            {
                if (!formacaoEspecificaSuperior.Ativo.GetValueOrDefault())
                    throw new FormacaoEspecificaItemPaiInativoException();
            }
        }

        /*Não será permitido desativar uma Formação-Específica, se essa Formação estiver associada a pelo menos
		  1(um) curso e com o período vigente. Em caso de violação exibir a seguinte mensagem impeditiva:
		  "Não é permitido desativar uma formação específica do Programa, quando essa formação está vigente em um Curso." */

        private void ValidarAssociacaoCursoVigenteFormacaoEspecificaPrograma(FormacaoEspecifica novaFormacaoEspecifica)
        {
            //se está desativando
            if (!novaFormacaoEspecifica.Ativo.GetValueOrDefault())
            {
                var cursosFormacoesEspecificas = this.CursoFormacaoEspecificaDomainService.SearchProjectionBySpecification(new CursoFormacaoEspecificaFilterSpecification() { SeqFormacaoEspecifica = novaFormacaoEspecifica.Seq }, c => new
                {
                    c.Seq,
                    c.SeqCurso,
                    c.SeqFormacaoEspecifica,
                    c.DataInicioVigencia,
                    c.DataFimVigencia
                }).ToList();

                if (cursosFormacoesEspecificas.Any(c => DateTime.Today >= c.DataInicioVigencia && (!c.DataFimVigencia.HasValue || DateTime.Today <= c.DataFimVigencia.GetValueOrDefault())))
                    throw new FormacaoEspecificaVigenteEmUmCursoException();
            }
        }

        /// <summary>
        /* Ao incluir uma formação específica, enviar a notificação correspondente ao tipo COMUNICADO_NOVA_FORMACAO_ESPECIFICA_PROGRAMA parametrizada para o programa em questão, conforme a parametrização de notificação por entidade. Substituir as tags do e-mail de acordo com os seguintes critérios:
		   [PROGRAMA] = retornar o nome reduzido do programa
		   [ID_FORMACAO_ESPECIFICA] = buscar o ID da nova formação específica
		   [DESCRICAO_FORMACAO_ESPECIFICA] = retornar a descrição da nova formação específica no seguinte formato: [Tipo da Formação] - [Descrição da Formação]
		   [USUARIO_CRIACAO] = retornar a descrição do usuário responsável pela criação da formação específica.
		   [DATA_CRIACAO] =  retornar a data e hora de criação da formação específica.
		   [FORMACAO_ESPECIFICA_ATIVA] = retornar a situação da formação (ind_ativo) com os valores: Sim ou Não.
		   </summary>
		   <param name="formacaoEspecifica"></param>*/

        private void EnviarNotificacaoNovaFormacaoEspecificaPrograma(FormacaoEspecificaVO formacaoEspecificaVO, FormacaoEspecifica novaFormacaoEspecifica)
        {
            // Busca o sequencial da configuração a ser enviada
            var seqConfiguracaoNotificacao = EntidadeConfiguracaoNotificacaoDomainService.BuscarSeqConfiguracaoNotificacaoAtivo(formacaoEspecificaVO.SeqInstituicaoEnsino, TOKEN_TIPO_NOTIFICACAO.COMUNICADO_NOVA_FORMACAO_ESPECIFICA_PROGRAMA);

            // Verifica se encontrou a configuração, se não encontrou, erro
            if (seqConfiguracaoNotificacao == 0)
                throw new EntidadeConfiguracaoNotificacaoNaoEncotradaException();

            // Busca os dados do programa
            var nomeReduzidoPrograma = this.ProgramaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Programa>(formacaoEspecificaVO.SeqEntidadeResponsavel.GetValueOrDefault()), p => p.NomeReduzido);

            // Monta os dados para merge
            Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
            dadosMerge.Add("{{PROGRAMA}}", nomeReduzidoPrograma);
            dadosMerge.Add("{{ID_FORMACAO_ESPECIFICA}}", novaFormacaoEspecifica.Seq.ToString());
            dadosMerge.Add("{{DESCRICAO_FORMACAO_ESPECIFICA}}", novaFormacaoEspecifica.Descricao);
            dadosMerge.Add("{{USUARIO_CRIACAO}}", novaFormacaoEspecifica.UsuarioInclusao);
            dadosMerge.Add("{{DATA_CRIACAO}}", novaFormacaoEspecifica.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss"));
            dadosMerge.Add("{{FORMACAO_ESPECIFICA_ATIVA}}", novaFormacaoEspecifica.Ativo.GetValueOrDefault() ? "Sim" : "Não");

            // Prepara o envio da notificação
            NotificacaoEmailData data = new NotificacaoEmailData()
            {
                SeqConfiguracaoNotificacao = seqConfiguracaoNotificacao,
                DadosMerge = dadosMerge,
                DataPrevistaEnvio = DateTime.Now,
                PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
            };

            // Chama o serviço de envio de notificação
            long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

            // Busca o sequencial da notificação-email-destinatário enviada
            var envioDestinatario = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
            if (envioDestinatario.Count == 0)
                throw new FormacaoEspecificaInclusaoEnvioNotificacaoException(TOKEN_TIPO_NOTIFICACAO.COMUNICADO_NOVA_FORMACAO_ESPECIFICA_PROGRAMA);
        }

        public List<FormacaoEspecificaVO> BuscarFormacoesEspecificasPorDocumentoConclusao(long seqDocumentoConclusao)
        {
            var formacoesEspecificas = this.DocumentoConclusaoFormacaoDomainService.SearchProjectionBySpecification(new DocumentoConclusaoFormacaoFilterSpecification() { SeqDocumentoConclusao = seqDocumentoConclusao }, x => new FormacaoEspecificaVO()
            {
                Seq = x.AlunoFormacao.FormacaoEspecifica.Seq,
                SeqFormacaoEspecificaSuperior = x.AlunoFormacao.FormacaoEspecifica.SeqFormacaoEspecificaSuperior,
                SeqTipoFormacaoEspecifica = x.AlunoFormacao.FormacaoEspecifica.SeqTipoFormacaoEspecifica,
                Descricao = x.AlunoFormacao.FormacaoEspecifica.Descricao,
                SeqEntidadeResponsavel = x.AlunoFormacao.FormacaoEspecifica.SeqEntidadeResponsavel,
                SeqGrauAcademico = x.AlunoFormacao.FormacaoEspecifica.Cursos.FirstOrDefault(c => c.SeqCurso == x.AlunoFormacao.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso).SeqGrauAcademico,
                SeqCursoOferta = x.AlunoFormacao.FormacaoEspecifica.SeqCursoOferta,
                Ativo = x.AlunoFormacao.FormacaoEspecifica.Ativo,
                TipoFormacaoEspecifica = new TipoFormacaoEspecificaVO()
                {
                    Seq = x.AlunoFormacao.FormacaoEspecifica.TipoFormacaoEspecifica.Seq,
                }
            }).ToList();

            formacoesEspecificas.ForEach(a => a.TipoFormacaoEspecifica = this.TipoFormacaoEspecificaDomainService.BuscarTipoFormacaoEspecifica(a.TipoFormacaoEspecifica.Seq));

            return formacoesEspecificas;
        }

        /// <summary>
        /// Busca os seqs das formações específicas superiores
        /// </summary>
        /// <param name="seqFormacaoEspecifica">Sequencial da formação específica atual</param>
        /// <param name="incluirSeqFormacaoInformada">Incluir o sequencial atual na lista de retorno ou não</param>
        /// <returns>Lista com a hierarquia</returns>
        public List<long> BuscarSeqsFormacoesEspecificasSuperiores(long seqFormacaoEspecifica, bool incluirSeqFormacaoInformada)
        {
            List<long> ret = new List<long>();
            long? atual = seqFormacaoEspecifica;

            while (atual.HasValue)
            {
                if (atual.HasValue && ((atual == seqFormacaoEspecifica && incluirSeqFormacaoInformada) || (atual != seqFormacaoEspecifica)))
                    ret.Add(atual.Value);

                atual = BuscarSeqFormacaoEspecificaSuperior(atual.Value);
            }
            return ret;
        }

        /// <summary>
        /// Busca o sequencial da formação superior
        /// </summary>
        /// <param name="seqFormacaoEspecifica">Sequencial da formação atual</param>
        /// <returns>Sequencial da formação superior</returns>
        public long? BuscarSeqFormacaoEspecificaSuperior(long seqFormacaoEspecifica)
        {
            return this.SearchProjectionByKey(seqFormacaoEspecifica, x => x.SeqFormacaoEspecificaSuperior);
        }

        public bool FormacaoEspefificaExibeTitulacao(long seqFormacaoEspecifica)
        {
            return this.SearchProjectionByKey(seqFormacaoEspecifica, p => p.TipoFormacaoEspecifica.PermiteTitulacao);
        }
    }
}