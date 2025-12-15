using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Resources;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class GrupoCurricularDomainService : AcademicoContextDomain<GrupoCurricular>
    {
        #region [ DomainService ]

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();
        private CurriculoDomainService CurriculoDomainService => Create<CurriculoDomainService>();

        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService => Create<CurriculoCursoOfertaDomainService>();

        private DivisaoMatrizCurricularGrupoDomainService DivisaoMatrizCurricularGrupoDomainService => Create<DivisaoMatrizCurricularGrupoDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private TipoGrupoCurricularDomainService TipoGrupoCurricularDomainService => Create<TipoGrupoCurricularDomainService>();

        #endregion [ DomainService ]

        #region [ Queries ]

        #region [ _buscarTreeFormacoesEspecificaPorGrupo ]

        private string _buscarTreeFormacoesEspecificaPorGrupo =
                        @"  ;WITH X AS
                            (
                                SELECT FE.seq_formacao_especifica,
									   FE.seq_formacao_especifica_superior,
									   FE.dsc_formacao_especifica,
									   FE.seq_tipo_formacao_especifica,
									   TFE.dsc_tipo_formacao_especifica,
									   [level] = 0
                                FROM CSO.formacao_especifica FE
								INNER JOIN CUR.grupo_curricular GC ON FE.seq_formacao_especifica = GC.seq_formacao_especifica
                                INNER JOIN CSO.tipo_formacao_especifica TFE ON FE.seq_tipo_formacao_especifica = TFE.seq_tipo_formacao_especifica
                                WHERE seq_grupo_curricular IN ({0})
                                UNION ALL
                                SELECT FET.seq_formacao_especifica,
									   FET.seq_formacao_especifica_superior,
									   FET.dsc_formacao_especifica,
									   FET.seq_tipo_formacao_especifica,
									   TFET.dsc_tipo_formacao_especifica,
									   [level] = X.[level] + 1
                                FROM X INNER JOIN CSO.formacao_especifica AS FET ON FET.seq_formacao_especifica = X.seq_formacao_especifica_superior
                                INNER JOIN CSO.tipo_formacao_especifica TFET ON FET.seq_tipo_formacao_especifica = TFET.seq_tipo_formacao_especifica
                            )

							SELECT DISTINCT X.seq_formacao_especifica AS SeqFormacaoEspecifica,
									   X.seq_formacao_especifica_superior AS SeqFormacaoEspecificaSuperior,
									   X.dsc_formacao_especifica AS DescricaoFormacaoEspecifica,
									   X.seq_tipo_formacao_especifica AS SeqTipoFormacaoEspecifica,
									   X.dsc_tipo_formacao_especifica AS DescricaoTipoFormacaoEspecifica,
									   [level]
                            FROM X
							ORDER BY [level] DESC;
                         ";

        #endregion [ _buscarTreeFormacoesEspecificaPorGrupo ]

        #region [ _buscarInformacaoGrupoBeneficioCondicoes ]

        private string _buscarInformacaoGrupoBeneficioCondicoes =
                        @"
                            SELECT GC.seq_grupo_curricular AS SeqGrupoCurricular,
                                   BE.seq_beneficio AS SeqBeneficio,
                            	   BE.dsc_beneficio AS DescricaoBeneficio,
                            	   CO.seq_condicao_obrigatoriedade AS SeqCondicaoObrigatoriedade,
                            	   CO.dsc_condicao_obrigatoriedade AS DescricaoCondicaoObrigatoriedade,
                            	   NULL AS SeqFormacaoEspecifica,
                            	   NULL AS DescricaoFormacaoEspecifica
                             FROM [ACADEMICO].[CUR].[grupo_curricular] GC
                             LEFT JOIN [ACADEMICO].[CUR].[grupo_curricular_beneficio] GCB ON GC.seq_grupo_curricular = GCB.seq_grupo_curricular
                             LEFT JOIN [ACADEMICO].[FIN].[beneficio] BE ON GCB.seq_beneficio = BE.seq_beneficio
                             LEFT JOIN [ACADEMICO].[CUR].[grupo_curricular_condicao_obrigatoriedade] GCCB ON GC.seq_grupo_curricular = GCCB.seq_grupo_curricular
                             LEFT JOIN [ACADEMICO].[CUR].[condicao_obrigatoriedade] CO ON GCCB.seq_condicao_obrigatoriedade = CO.seq_condicao_obrigatoriedade
                             WHERE (BE.seq_beneficio IS NOT NULL OR CO.seq_condicao_obrigatoriedade IS NOT NULL) AND GC.seq_grupo_curricular IN ({0})
                             ORDER BY SeqGrupoCurricular;
                         ";

        #endregion [ _buscarInformacaoGrupoBeneficioCondicoes ]

        #region [ _buscarTreeGruposPorGruposFilho ]

        private string _buscarTreeGruposPorGruposFilho =
                        @"  ;WITH X AS
                            (
                                SELECT GC.seq_grupo_curricular,
                                       GC.seq_grupo_curricular_superior,
	                                   GC.dsc_grupo_curricular,
                                       GC.idt_dom_formato_configuracao_grupo,
	                                   GC.qtd_hora_aula,
	                                   GC.qtd_hora_relogio,
	                                   GC.qtd_creditos,
	                                   GC.qtd_itens,
                                       TCGC.dsc_tipo_configuracao_grupo_curricular,
                                       TCGC.dsc_token,
                                       [level] = 0
                                FROM CUR.grupo_curricular GC
                                INNER JOIN CUR.tipo_configuracao_grupo_curricular TCGC ON GC.seq_tipo_configuracao_grupo_curricular = TCGC.seq_tipo_configuracao_grupo_curricular
                                WHERE seq_grupo_curricular in ({0})
                                UNION ALL
                                SELECT GCT.seq_grupo_curricular,
                                       GCT.seq_grupo_curricular_superior,
	                                   GCT.dsc_grupo_curricular,
                                       GCT.idt_dom_formato_configuracao_grupo,
	                                   GCT.qtd_hora_aula,
	                                   GCT.qtd_hora_relogio,
	                                   GCT.qtd_creditos,
	                                   GCT.qtd_itens,
                                       TCGCT.dsc_tipo_configuracao_grupo_curricular,
                                       TCGCT.dsc_token,
                                       [level] = X.[level] + 1
                                FROM X INNER JOIN CUR.grupo_curricular AS GCT ON GCT.seq_grupo_curricular = X.seq_grupo_curricular_superior
                                INNER JOIN CUR.tipo_configuracao_grupo_curricular TCGCT ON GCT.seq_tipo_configuracao_grupo_curricular = TCGCT.seq_tipo_configuracao_grupo_curricular
                            )

                            SELECT DISTINCT seq_grupo_curricular AS SeqGrupoCurricular,
                                            seq_grupo_curricular_superior AS SeqGrupoCurricularSuperior,
	                                        dsc_grupo_curricular AS DescricaoGrupo,
                                            idt_dom_formato_configuracao_grupo AS FormatoConfiguracaoGrupo,
	                                        qtd_hora_aula AS HoraAulaGrupo,
	                                        qtd_hora_relogio AS HoraGrupo,
	                                        qtd_creditos AS CreditoGrupo,
	                                        qtd_itens AS ItensGrupo,
                                            dsc_tipo_configuracao_grupo_curricular AS DescricaoTipoConfiguracaoGrupo,
                                            dsc_token AS TokenTipoConfiguracaoGrupo
                            FROM X
                            ORDER BY SeqGrupoCurricular;
                         ";

        #endregion [ _buscarTreeGruposPorGruposFilho ]

        #endregion [ Queries ]

        /// <summary>
        /// Busca os dados do currículo e curso de um grupo curricular
        /// </summary>
        /// <param name="seqCurriculo">Sequencial do currículo</param>
        /// <returns>Dados do currículo e curso asociado</returns>
        public GrupoCurricularCabecalhoVO BuscarGrupoCurricularCabecalho(long seqCurriculo)
        {
            return this.CurriculoDomainService
                .SearchProjectionByKey(new SMCSeqSpecification<Curriculo>(seqCurriculo), curriculo => new GrupoCurricularCabecalhoVO()
                {
                    CodigoCurriculo = curriculo.Codigo,
                    DescricaoCurriculo = curriculo.Descricao,
                    CurriculoAtivo = curriculo.Ativo,
                    SiglaCurso = curriculo.Curso.Sigla,
                    NomeCurso = curriculo.Curso.Nome,
                });
        }

        /// <summary>
        /// Busca o Currículo com Curso para servir de configuração para o GrupoCurricular
        /// </summary>
        /// <param name="seqCurriculo">Sequencial do Currículo relacionado ao Curso</param>
        /// <param name="seqGrupoCurricularSuperior">Sequencial do grupo curricular superior, 0 quando for raiz</param>
        /// <returns>Novo Grupo Curricular com o Currículo e Curso associados</returns>
        public GrupoCurricular BuscarConfiguracoes(long seqCurriculo, long seqGrupoCurricularSuperior)
        {
            var configuracao = new GrupoCurricular();

            var specCurriculo = new SMCSeqSpecification<Curriculo>(seqCurriculo);
            configuracao.Curriculo = this.CurriculoDomainService.SearchByKey(specCurriculo, IncludesCurriculo.Curso);

            var specGrupoSuperior = new SMCSeqSpecification<GrupoCurricular>(seqGrupoCurricularSuperior);
            configuracao.GrupoCurricularSuperior = this.SearchByKey(specGrupoSuperior);

            return configuracao;
        }

        /// <summary>
        /// Busca os Grupos Curriculares de um Currículo com seus componentes e sequenciais convertidos em index para permitir dois tipos de objeto na mesma árvore
        /// </summary>
        /// <param name="seqCurriculo">Sequencial do Currículo</param>
        /// <param name="grupoFormatado">Quando setado o grupo será formadado conforme a regra RN_CUR_045 caso contrário terá apenas a descrição</param>
        /// <returns>Dados dos Grupos Curriculares e Componentes do Currículo</returns>
        public IEnumerable<GrupoCurricularListaVO> BuscarGruposCurricularesTree(long seqCurriculo, bool grupoFormatado = true)
        {
            var specCurriculo = new SMCSeqSpecification<Curriculo>(seqCurriculo);
            var seqNivelEnsino = this.CurriculoDomainService.SearchProjectionByKey(specCurriculo, p => p.Curso.SeqNivelEnsino);

            var spec = new GrupoCurricularFilterSpecification() { SeqCurriculo = seqCurriculo };
            spec.SetOrderBy(o => o.Descricao);

            var index = 1;
            var dicSeqsGrupos = new Dictionary<long, long>();
            var dicSeqsComponentes = new Dictionary<long, long>();
            var gruposCurriculares = this.SearchProjectionBySpecification(spec, grupo => new
            {
                grupo.Seq,
                grupo.SeqGrupoCurricularSuperior,
                grupo.FormatoConfiguracaoGrupo,
                grupo.QuantidadeCreditos,
                grupo.QuantidadeHoraAula,
                grupo.QuantidadeHoraRelogio,
                grupo.QuantidadeItens,
                grupo.Descricao,
                DescricaoTipoConfiguracaoGrupo = grupo.TipoConfiguracaoGrupoCurricular.Descricao,
                PermiteGrupos = grupo.TipoConfiguracaoGrupoCurricular.TiposConfiguracoesGrupoCurricularFilhos.Any(),
                ContemFormacaoEspecifica = grupo.SeqFormacaoEspecifica.HasValue,
                ContemBeneficios = grupo.Beneficios.Any(),
                ContemCondicoesObrigatoriedade = grupo.CondicoesObrigatoriedade.Any(),
                ComponentesCurriculares = grupo.ComponentesCurriculares.Select(grupoComponente => new
                {
                    grupoComponente.Seq,
                    grupoComponente.SeqGrupoCurricular,
                    grupoComponente.SeqComponenteCurricular,
                    grupoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                    grupoComponente.ComponenteCurricular.Codigo,
                    grupoComponente.ComponenteCurricular.Descricao,
                    grupoComponente.ComponenteCurricular.CargaHoraria,
                    grupoComponente.ComponenteCurricular.Credito
                }).ToList()
            }).ToList();

            var specFormato = new InstituicaoNivelTipoComponenteCurricularFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };
            var formatoTiposComponente = this.InstituicaoNivelTipoComponenteCurricularDomainService
                .SearchProjectionBySpecification(specFormato, p => new { p.SeqTipoComponenteCurricular, p.FormatoCargaHoraria, p.InstituicaoNivel.SeqInstituicaoEnsino })
                .ToList();

            // Monta os dicionários de seqs
            foreach (var grupoCurricular in gruposCurriculares)
            {
                dicSeqsGrupos.Add(grupoCurricular.Seq, index++);
                foreach (var componenteCurricular in grupoCurricular.ComponentesCurriculares)
                {
                    dicSeqsComponentes.Add(componenteCurricular.Seq, index++);
                }
            }

            // Popular o retorno com os grupos e seus componentes
            foreach (var grupoCurricular in gruposCurriculares)
            {
                var grupoCurricularVo = new GrupoCurricularListaVO()
                {
                    Seq = dicSeqsGrupos[grupoCurricular.Seq],
                    SeqPai = grupoCurricular.SeqGrupoCurricularSuperior.HasValue ? new long?(dicSeqsGrupos[grupoCurricular.SeqGrupoCurricularSuperior.Value]) : null,
                    SeqGrupoCurricular = grupoCurricular.Seq,
                    SeqGrupoCurricularSuperior = grupoCurricular.SeqGrupoCurricularSuperior,
                    SeqCurriculo = seqCurriculo,
                    SeqNivelEnsino = seqNivelEnsino,
                    FormatoConfiguracaoGrupo = grupoCurricular.FormatoConfiguracaoGrupo,
                    QuantidadeCreditos = grupoCurricular.QuantidadeCreditos,
                    QuantidadeHoraAula = grupoCurricular.QuantidadeHoraAula,
                    QuantidadeHoraRelogio = grupoCurricular.QuantidadeHoraRelogio,
                    QuantidadeItens = grupoCurricular.QuantidadeItens,
                    Descricao = grupoCurricular.Descricao,
                    TipoConfiguracaoDescricao = grupoCurricular.DescricaoTipoConfiguracaoGrupo,
                    ContemGrupos = gruposCurriculares.Any(a => a.SeqGrupoCurricularSuperior == grupoCurricular.Seq),
                    ContemComponentes = grupoCurricular.ComponentesCurriculares.SMCAny(),
                    PermiteGrupos = grupoCurricular.PermiteGrupos,
                    ContemFormacaoEspecifica = grupoCurricular.ContemFormacaoEspecifica,
                    ContemBeneficios = grupoCurricular.ContemBeneficios,
                    ContemCondicoesObrigatoriedade = grupoCurricular.ContemCondicoesObrigatoriedade
                };
                if (grupoFormatado)
                {
                    grupoCurricularVo.DescricaoFormatada = GerarDescricaoGrupoCurricular(
                        grupoCurricularVo.Descricao,
                        grupoCurricularVo.TipoConfiguracaoDescricao,
                        grupoCurricularVo.FormatoConfiguracaoGrupo,
                        grupoCurricularVo.QuantidadeHoraRelogio,
                        grupoCurricularVo.QuantidadeHoraAula,
                        grupoCurricularVo.QuantidadeCreditos,
                        grupoCurricularVo.QuantidadeItens);
                }
                else
                {
                    grupoCurricularVo.DescricaoFormatada = grupoCurricularVo.Descricao;
                }
                yield return grupoCurricularVo;

                foreach (var componenteCurricular in grupoCurricular.ComponentesCurriculares.OrderBy(o => o.Descricao))
                {
                    var componenteCurricularVo = new GrupoCurricularListaVO()
                    {
                        Seq = dicSeqsComponentes[componenteCurricular.Seq],
                        SeqPai = dicSeqsGrupos[componenteCurricular.SeqGrupoCurricular],
                        SeqGrupoComponenteCurricular = componenteCurricular.Seq,
                        SeqGrupoCurricularSuperior = componenteCurricular.SeqGrupoCurricular,
                        SeqComponenteCurricular = componenteCurricular.SeqComponenteCurricular,
                        SeqCurriculo = seqCurriculo,
                        SeqNivelEnsino = seqNivelEnsino,
                        SeqTipoComponenteCurricular = componenteCurricular.SeqTipoComponenteCurricular,
                        Codigo = componenteCurricular.Codigo,
                        Descricao = componenteCurricular.Descricao,
                        CargaHoraria = componenteCurricular.CargaHoraria,
                        Credito = componenteCurricular.Credito,
                        Formato = formatoTiposComponente
                            .SingleOrDefault(s => s.SeqTipoComponenteCurricular == componenteCurricular.SeqTipoComponenteCurricular)
                            ?.FormatoCargaHoraria ?? FormatoCargaHoraria.Nenhum
                    };
                    componenteCurricularVo.DescricaoFormatada = ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(
                        componenteCurricularVo.Codigo,
                        componenteCurricularVo.Descricao,
                        componenteCurricularVo.Credito,
                        componenteCurricularVo.CargaHoraria,
                        componenteCurricularVo.Formato);
                    yield return componenteCurricularVo;
                }
            }
        }

        /// <summary>
        /// Busca os Grupos Curriculares de um Currículo com seus componentes
        /// </summary>
        /// <param name="filtro">Objeto de filtro com sequencial do currículo curso oferta obrigatório</param>
        /// <returns>Dados dos Grupos Curriculares e Componentes do Currículo</returns>
        public IEnumerable<GrupoCurricularListaVO> BuscarGruposCurricularesLookup(GrupoCurricularFiltroVO filtro)
        {
            // Busca o curriculo curso oferta
            var curriculoCursoOferta = this.CurriculoCursoOfertaDomainService.BuscarCurriculoCursoOfertaGrupo(filtro.SeqCurriculoCursoOferta);

            List<long> seqsFormacoesHierarquia = null;
            if (filtro.FiltrarFormacoesEspecificasAluno && filtro.SeqPessoaAtuacao.HasValue)
            {
                seqsFormacoesHierarquia = new List<long>();
                var formacoesAssociadas = new AlunoFormacaoDomainService().BuscarAssociacaoFormacaoEspecifica(filtro.SeqPessoaAtuacao.Value);
                if (formacoesAssociadas != null && formacoesAssociadas.FormacoesEspecificas != null)
                {
                    // Percorre as formações específicas do aluno para pegar a hierarquia de cada uma
                    foreach (var formacaoAssociada in formacoesAssociadas.FormacoesEspecificas)
                        seqsFormacoesHierarquia.AddRange(FormacaoEspecificaDomainService.BuscarSeqsFormacoesEspecificasSuperiores(formacaoAssociada.Seq, true));
                }
            }

            // Filtra os grupos curriculares do curriculo-curso-oferta
            var spec = new GrupoCurricularFilterSpecification()
            {
                SeqCurriculo = curriculoCursoOferta.SeqCurriculo,
                DesconsiderarGruposTodosItensObrigatorios = filtro.DesconsiderarGruposTodosItensObrigatorios,
                SeqsFormacoesEspecificas = seqsFormacoesHierarquia
            };
            var includes = IncludesGrupoCurricular.ComponentesCurriculares_ComponenteCurricular_NiveisEnsino | IncludesGrupoCurricular.TipoConfiguracaoGrupoCurricular;
            var gruposCurriculares = this.SearchBySpecification(spec, includes).ToList();

            if (filtro.SeqPessoaAtuacao.HasValue)
            {
                // Busca os dados de origem do aluno
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(filtro.SeqPessoaAtuacao.Value);

                // Verifica se todos os tipos de componente do grupo permitem dispensa
                if (filtro.DesconsiderarItensQueNaoPermitemCadastroDispensa.GetValueOrDefault())
                {
                    var seqsTiposDispensa = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarTipoComponenteCurricularDispensa(dadosOrigem.SeqInstituicaoEnsino, dadosOrigem.SeqNivelEnsino);
                    gruposCurriculares = gruposCurriculares.Where(w => !w.ComponentesCurriculares.Any(a => !seqsTiposDispensa.Contains(a.ComponenteCurricular.SeqTipoComponenteCurricular))).ToList();
                }

                // Verifica no histórico escolar da pessoa atuação antes verificava sem filtro de pessoa e retornava com erro
                if (filtro.DesconsiderarItensCursadosAprovacaoOuDispensadosAluno.GetValueOrDefault())
                {
                    if (dadosOrigem.SeqAlunoHistoricoAtual > 0 && filtro.DesconsiderarItensCursadosAprovacaoOuDispensadosAluno.GetValueOrDefault())
                    {
                        //Recupera os dados do histórico escolar de acordo com a procedure ACADEMICO.APR.st_rel_componentes_historico_escolar
                        var dadosHistoricoEscolar = HistoricoEscolarDomainService.ComponentesCriteriosHistoricoEscolar(dadosOrigem.SeqAlunoHistoricoAtual, false);
                        List<long> seqsComponentesAprovados = dadosHistoricoEscolar.Where(w => w.SeqComponente.HasValue && !w.SeqComponenteCurricularAssunto.HasValue).Select(s => s.SeqComponente.Value).ToList();

                        // Remove os componentes curriculares que já foram dispensados ou aprovados
                        gruposCurriculares.ForEach(g =>
                        {
                            g.ComponentesCurriculares = g.ComponentesCurriculares.Where(c => !seqsComponentesAprovados.Contains(c.SeqComponenteCurricular)).ToList();
                        });
                    }
                }
            }

            if (filtro.DesconsiderarItensVinculadosAoCurriculoCursoOferta.GetValueOrDefault())
                gruposCurriculares = gruposCurriculares.Where(w => !curriculoCursoOferta.GruposCurriculares.Select(s => s.SeqGrupoCurricular).Contains(w.Seq)).ToList();
            else
                gruposCurriculares = gruposCurriculares.Where(w => curriculoCursoOferta.GruposCurriculares.Select(s => s.SeqGrupoCurricular).Contains(w.Seq)).ToList();

            #region [ Define o dicionário para preencher o sequencial de acordo com retorno ]

            //Definido qual sequencial é utilizado para selecionar porque são duas entidades em uma mesma treeview
            var dicSeqsGrupos = new Dictionary<long, long>();
            var dicSeqsComponentes = new Dictionary<long, long>();
            long valorMaximoGrupo = 0;

            if (filtro.SelecionarComponente == true)
            {
                valorMaximoGrupo = (gruposCurriculares == null || gruposCurriculares.Count == 0) ? 1 : gruposCurriculares.SelectMany(s => s.ComponentesCurriculares).Max(m => m.Seq) + 1;

                foreach (var grupoCurricular in gruposCurriculares)
                {
                    dicSeqsGrupos.Add(grupoCurricular.Seq, valorMaximoGrupo++);
                    foreach (var componenteCurricular in grupoCurricular.ComponentesCurriculares)
                    {
                        dicSeqsComponentes.Add(componenteCurricular.Seq, componenteCurricular.Seq);
                    }
                }
            }
            else
            {
                valorMaximoGrupo = (gruposCurriculares == null || gruposCurriculares.Count == 0) ? 1 : gruposCurriculares.Max(m => m.Seq) + 1;

                foreach (var grupoCurricular in gruposCurriculares)
                {
                    dicSeqsGrupos.Add(grupoCurricular.Seq, grupoCurricular.Seq);
                    foreach (var componenteCurricular in grupoCurricular.ComponentesCurriculares)
                    {
                        dicSeqsComponentes.Add(componenteCurricular.Seq, valorMaximoGrupo++);
                    }
                }
            }

            #endregion [ Define o dicionário para preencher o sequencial de acordo com retorno ]

            // Popular o retorno com os grupos e seus componentes
            foreach (var grupoCurricular in gruposCurriculares)
            {
                var grupoCurricularVo = new GrupoCurricularListaVO()
                {
                    Seq = dicSeqsGrupos[grupoCurricular.Seq],
                    SeqGrupoCurricular = dicSeqsGrupos[grupoCurricular.Seq],
                    Descricao = grupoCurricular.Descricao,
                    ContemGrupos = gruposCurriculares.Any(a => a.SeqGrupoCurricularSuperior == dicSeqsGrupos[grupoCurricular.Seq]),
                    ContemComponentes = grupoCurricular.ComponentesCurriculares.SMCCount() > 0,
                    TipoConfiguracaoDescricao = grupoCurricular.TipoConfiguracaoGrupoCurricular.Descricao,
                    FormatoConfiguracaoGrupo = grupoCurricular.FormatoConfiguracaoGrupo,
                    QuantidadeItens = grupoCurricular.QuantidadeItens,
                    QuantidadeHoraAula = grupoCurricular.QuantidadeHoraAula,
                    QuantidadeHoraRelogio = grupoCurricular.QuantidadeHoraRelogio,
                    QuantidadeCreditos = grupoCurricular.QuantidadeCreditos,
                    Folha = false
                };

                //Verifica se o grupo curricular Pai pertence ao curriculo curso oferta ,filho permanece como nó raiz para seleção
                if (grupoCurricular.SeqGrupoCurricularSuperior.HasValue && dicSeqsGrupos.ContainsKey(grupoCurricular.SeqGrupoCurricularSuperior.Value) && curriculoCursoOferta.GruposCurriculares.Any(a => a.SeqGrupoCurricular == grupoCurricular.SeqGrupoCurricularSuperior.Value))
                {
                    grupoCurricularVo.SeqPai = new long?(dicSeqsGrupos[grupoCurricular.SeqGrupoCurricularSuperior.Value]);
                    grupoCurricularVo.SeqGrupoCurricularSuperior = new long?(dicSeqsGrupos[grupoCurricular.SeqGrupoCurricularSuperior.Value]);
                }

                yield return grupoCurricularVo;

                foreach (var componenteCurricular in grupoCurricular.ComponentesCurriculares)
                {
                    var componenteCurricularVo = new GrupoCurricularListaVO()
                    {
                        //Os componentes curriculares não possuem filho
                        Seq = dicSeqsComponentes[componenteCurricular.Seq],
                        SeqPai = dicSeqsGrupos[componenteCurricular.SeqGrupoCurricular],
                        SeqGrupoComponenteCurricular = dicSeqsComponentes[componenteCurricular.Seq],
                        SeqGrupoCurricularSuperior = dicSeqsGrupos[componenteCurricular.SeqGrupoCurricular],
                        Codigo = componenteCurricular.ComponenteCurricular.Codigo,
                        Descricao = componenteCurricular.ComponenteCurricular.Descricao,
                        CargaHoraria = componenteCurricular.ComponenteCurricular.CargaHoraria,
                        Credito = componenteCurricular.ComponenteCurricular.Credito,
                        Formato = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
                        {
                            SeqNivelEnsino = componenteCurricular.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.SeqNivelEnsino).FirstOrDefault(),
                            SeqTipoComponenteCurricular = componenteCurricular.ComponenteCurricular.SeqTipoComponenteCurricular
                        }, i => i.FormatoCargaHoraria),
                        Folha = true
                    };
                    yield return componenteCurricularVo;
                }
            }
        }

        public List<SMCDatasourceItem> BuscarGruposCurricularesCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta, long? seqComponenteCurricular = null)
        {
            var gruposCurricularesCurriculoCursoOferta = new List<SMCDatasourceItem>();

            var specCurriculoCursoOferta = new SMCSeqSpecification<CurriculoCursoOferta>(seqCurriculoCursoOferta);

            var listagruposCurricularesCurriculoCursoOferta = this.CurriculoCursoOfertaDomainService.SearchByKey(specCurriculoCursoOferta, x => x.GruposCurriculares[0].GrupoCurricular.ComponentesCurriculares , y =>  y.GruposCurriculares[0].GrupoCurricular.TipoConfiguracaoGrupoCurricular)
                .GruposCurriculares
                .Where(w => w.GrupoCurricular.TipoConfiguracaoGrupoCurricular.Token == TOKEN_TIPO_CONFIGURACAO_GRUPO_CURRICULAR.TKN_TODOS_OBRIGATORIOS || w.GrupoCurricular.TipoConfiguracaoGrupoCurricular.Token == TOKEN_TIPO_CONFIGURACAO_GRUPO_CURRICULAR.TKN_MINIMO_A_CURSAR)
                .OrderBy(o => o.GrupoCurricular.Descricao)
                .ToList();

            listagruposCurricularesCurriculoCursoOferta.ForEach(x =>
            {
                if (!x.GrupoCurricular.SeqGrupoCurricularSuperior.HasValue && (!seqComponenteCurricular.HasValue || seqComponenteCurricular.HasValue && !x.GrupoCurricular.ComponentesCurriculares.Any(a => a.SeqComponenteCurricular == seqComponenteCurricular.Value)))
                    gruposCurricularesCurriculoCursoOferta.Add(new SMCDatasourceItem() { Seq = x.SeqGrupoCurricular, Descricao = x.GrupoCurricular.Descricao });
            });

            return gruposCurricularesCurriculoCursoOferta;
        }

        /// <summary>
        /// Busca os Grupos Curriculares de um Currículo com seus componentes
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do Currículo curso oferta</param>
        /// <returns>Dados dos Grupos Curriculares e Componentes do Currículo</returns>
        public IEnumerable<GrupoCurricularListaVO> BuscarGruposCurricularesTreeCurriculoCursoOferta(long seqCurriculoCursoOferta)
        {
            var curriculoCursoOferta = this.CurriculoCursoOfertaDomainService.BuscarCurriculoCursoOfertaGrupo(seqCurriculoCursoOferta);

            var spec = new GrupoCurricularFilterSpecification() { SeqCurriculo = curriculoCursoOferta.SeqCurriculo };

            var includes = IncludesGrupoCurricular.GruposCurricularesFilhos
                         | IncludesGrupoCurricular.ComponentesCurriculares_ComponenteCurricular_NiveisEnsino
                         | IncludesGrupoCurricular.ComponentesCurriculares_ComponenteCurricular_Configuracoes_DivisoesComponente_TipoDivisaoComponente_Modalidade
                         | IncludesGrupoCurricular.ComponentesCurriculares_ComponenteCurricular_Configuracoes_DivisoesComponente_TipoDivisaoComponente_TipoComponenteCurricular
                         | IncludesGrupoCurricular.TipoConfiguracaoGrupoCurricular;

            //Listar o grupo curricular que já foi associado ao curriculo curso oferta
            var gruposCurriculares = this.SearchBySpecification(spec, includes).Where(w => curriculoCursoOferta.GruposCurriculares.Select(s => s.SeqGrupoCurricular).Contains(w.Seq)).ToList();
            var seqsGruposCurriculares = gruposCurriculares.Select(s => s.Seq).ToList();
            var seqsGruposCurricularesObrigatorios = curriculoCursoOferta.GruposCurriculares.Where(w => w.Obrigatorio).Select(s => s.SeqGrupoCurricular).ToList();

            var valorMaximoGrupo = curriculoCursoOferta.GruposCurriculares.SMCCount() == 0 ? 0 : curriculoCursoOferta.GruposCurriculares.Max(m => m.Seq);

            // Popular o retorno com os grupos e seus componentes
            foreach (var grupoCurricular in gruposCurriculares)
            {
                var grupoCurricularVo = new GrupoCurricularListaVO()
                {
                    Seq = curriculoCursoOferta.GruposCurriculares.FirstOrDefault(s => s.SeqGrupoCurricular == grupoCurricular.Seq)?.Seq ?? 0,
                    SeqCurriculoCursoOferta = curriculoCursoOferta.Seq,
                    SeqPai = grupoCurricular.SeqGrupoCurricularSuperior.HasValue && seqsGruposCurriculares.Contains(grupoCurricular.SeqGrupoCurricularSuperior.Value) ? curriculoCursoOferta.GruposCurriculares.FirstOrDefault(s => s.SeqGrupoCurricular == grupoCurricular.SeqGrupoCurricularSuperior.Value)?.Seq : null,
                    //SeqPai = grupoCurricular.SeqGrupoCurricularSuperior.HasValue && seqsGruposCurriculares.Contains(grupoCurricular.SeqGrupoCurricularSuperior.Value) ? new long?(grupoCurricular.SeqGrupoCurricularSuperior.Value) : null,
                    SeqGrupoCurricular = grupoCurricular.Seq,
                    SeqGrupoCurricularSuperior = grupoCurricular.SeqGrupoCurricularSuperior,
                    Descricao = grupoCurricular.Descricao,
                    ContemGrupos = gruposCurriculares.Any(a => a.SeqGrupoCurricularSuperior == grupoCurricular.Seq),
                    ContemComponentes = grupoCurricular.ComponentesCurriculares.SMCCount() > 0,
                    SeqTipoConfiguracao = grupoCurricular.TipoConfiguracaoGrupoCurricular.Seq,
                    TipoConfiguracaoDescricao = grupoCurricular.TipoConfiguracaoGrupoCurricular.Descricao,
                    FormatoConfiguracaoGrupo = grupoCurricular.FormatoConfiguracaoGrupo,
                    QuantidadeItens = grupoCurricular.QuantidadeItens,
                    QuantidadeHoraAula = grupoCurricular.QuantidadeHoraAula,
                    QuantidadeHoraRelogio = grupoCurricular.QuantidadeHoraRelogio,
                    QuantidadeCreditos = grupoCurricular.QuantidadeCreditos,
                    Folha = false,
                    Obrigatorio = seqsGruposCurricularesObrigatorios.Contains(grupoCurricular.Seq)
                };
                grupoCurricularVo.DescricaoFormatada = GerarDescricaoGrupoCurricular(
                     grupoCurricularVo.Descricao,
                     grupoCurricularVo.TipoConfiguracaoDescricao,
                     grupoCurricularVo.FormatoConfiguracaoGrupo,
                     grupoCurricularVo.QuantidadeHoraRelogio,
                     grupoCurricularVo.QuantidadeHoraAula,
                     grupoCurricularVo.QuantidadeCreditos,
                     grupoCurricularVo.QuantidadeItens);

                yield return grupoCurricularVo;

                foreach (var componenteCurricular in grupoCurricular.ComponentesCurriculares.OrderBy(o => o.ComponenteCurricular.Descricao))
                {
                    var componenteCurricularVo = new GrupoCurricularListaVO()
                    {
                        //Os componentes curriculares não possuem filho
                        //para não da erro no sequencial todos os componentes terão sequencial somado ao maximo do sequencial de grupo
                        Seq = ++valorMaximoGrupo,
                        SeqPai = grupoCurricularVo.Seq,
                        SeqGrupoComponenteCurricular = componenteCurricular.Seq,
                        SeqGrupoCurricularSuperior = componenteCurricular.SeqGrupoCurricular,
                        Codigo = componenteCurricular.ComponenteCurricular.Codigo,
                        Descricao = componenteCurricular.ComponenteCurricular.Descricao,
                        CargaHoraria = componenteCurricular.ComponenteCurricular.CargaHoraria,
                        Credito = componenteCurricular.ComponenteCurricular.Credito,
                        Formato = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
                        {
                            SeqNivelEnsino = componenteCurricular.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.SeqNivelEnsino).FirstOrDefault(),
                            SeqTipoComponenteCurricular = componenteCurricular.ComponenteCurricular.SeqTipoComponenteCurricular
                        }, i => i.FormatoCargaHoraria),
                        Folha = true,
                        Obrigatorio = seqsGruposCurricularesObrigatorios.Contains(grupoCurricular.Seq),
                    };

                    componenteCurricularVo.ConfiguracoesComponentes = componenteCurricular.ComponenteCurricular.Configuracoes.TransformList<ConfiguracaoComponenteVO>();
                    componenteCurricularVo.DescricaoFormatada = ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(
                        componenteCurricularVo.Codigo,
                        componenteCurricularVo.Descricao,
                        componenteCurricularVo.Credito,
                        componenteCurricularVo.CargaHoraria,
                        componenteCurricularVo.Formato);

                    yield return componenteCurricularVo;
                }
            }
        }

        /// <summary>
        /// Busca o grupo curricular selecionado no lookup
        /// </summary>
        /// <param name="seqGrupoCurricular">Array de sequencial do grupo curricular</param>
        /// <returns>Array com todos os grupos curricularres selecionados</returns>
        public GrupoCurricularVO[] BuscarGruposCurricularesLookupSelecionado(long[] seqGruposCurriculares)
        {
            var gruposCurriculares = this.SearchBySpecification(new SMCTrueSpecification<GrupoCurricular>()).ToList();
            var specSelecionados = new SMCContainsSpecification<GrupoCurricular, long>(p => p.Seq, seqGruposCurriculares);
            var gruposSelecionadas = this.SearchBySpecification(specSelecionados, IncludesGrupoCurricular.TipoConfiguracaoGrupoCurricular);

            return gruposSelecionadas.TransformToArray<GrupoCurricularVO>();
        }

        /// <summary>
        /// Busca os grupos curriculares abaixo do grupo curricular selecionado para trazer toda a hierarquia
        /// </summary>
        /// <param name="seq">Sequencial do grupo curricular</param>
        /// <returns>Lista de ids de todos os grupos curriculares abaixo do selecionado</returns>
        public List<long> BuscarGruposCurricularesHierarquia(long seq)
        {
            List<long> listaIds = new List<long>();

            this.RecuperarIdsGruposCurricularesHierarquiaFilho(seq, ref listaIds);

            return listaIds;
        }

        /// <summary>
        /// Busca os filhos do grupo curriculare de maneira recursiva para trazer toda a hierarquia
        /// </summary>
        /// <param name="seq">Sequencial do grupo curricular</param>
        /// <param name="listaIds">Lista de Ids dos grupos curriculares abaixo na hierarquia</param>
        private void RecuperarIdsGruposCurricularesHierarquiaFilho(long seq, ref List<long> listaIds)
        {
            var includes = IncludesGrupoCurricular.GruposCurricularesFilhos;
            var gruposCurriculares = this.SearchByKey(new SMCSeqSpecification<GrupoCurricular>(seq), includes);
            listaIds.Add(gruposCurriculares.Seq);

            foreach (var item in gruposCurriculares.GruposCurricularesFilhos)
                this.RecuperarIdsGruposCurricularesHierarquiaFilho(item.Seq, ref listaIds);
        }

        /// <summary>
        /// Grava o grupo curricular
        /// </summary>
        /// <param name="grupoCurricularVo">Dados do grupo curricular</param>
        /// <returns>Sequencial do grupo curricular gravado</returns>
        /// <exception cref="GrupoCurricularAssociadoDivisaoMatrizException">Caso seja alterada a configuração de um grupo associado a uma divisão de matriz</exception>
        public long SalvarGrupoCurricular(GrupoCurricularVO grupoCurricularVo)
        {
            var grupoCurricular = grupoCurricularVo.Transform<GrupoCurricular>();

            if (!grupoCurricular.IsNew())
            {
                var specGrupo = new SMCSeqSpecification<GrupoCurricular>(grupoCurricular.Seq);
                var grupoCurricularBanco = this.SearchByKey(specGrupo);

                // Valida a alteração de tipo de grupo curricular RN_CUR_024
                if (grupoCurricular.SeqTipoGrupoCurricular != grupoCurricularBanco.SeqTipoGrupoCurricular)
                {
                    // Recupera os seqs dos tipos de componentes associados
                    var tiposComponenteAssociadosGrupo = this.SearchProjectionByKey(specGrupo, p =>
                        p.ComponentesCurriculares.Select(s => s.ComponenteCurricular.SeqTipoComponenteCurricular));
                    var specTipoGrupo = new SMCSeqSpecification<TipoGrupoCurricular>(grupoCurricular.SeqTipoGrupoCurricular);

                    // Recupera os seqs dos tipos de componentes esperados no novo tipo de grupo curricular
                    var tiposComponetesEsperadosNovoTipoGrupo = this.TipoGrupoCurricularDomainService.SearchProjectionByKey(specTipoGrupo, p =>
                        p.TiposComponenteCurricular.Select(s => s.Seq));

                    // Verifica se algum tipo de componente associado não está nos tipos esperados no novo tipo de grupo
                    if (tiposComponenteAssociadosGrupo.Except(tiposComponetesEsperadosNovoTipoGrupo).Any())
                        throw new GrupoCurricularAlteracaoTipoInvalidoException();
                }

                // Valida a alteração de configuração segundo a regra RN_CUR_027 na atualização
                if (grupoCurricular.SeqTipoConfiguracaoGrupoCurricular != grupoCurricularBanco.SeqTipoConfiguracaoGrupoCurricular)
                {
                    var specDivisao = new DivisaoMatrizCurricularGrupoFilterSpecification() { SeqGrupoCurricular = grupoCurricular.Seq };
                    if (this.DivisaoMatrizCurricularGrupoDomainService.Count(specDivisao) > 0)
                        throw new GrupoCurricularAssociadoDivisaoMatrizException();
                }
            }

            this.SaveEntity(grupoCurricular);

            return grupoCurricularVo.Index != 0 ? grupoCurricularVo.Index : grupoCurricular.Seq;
        }

        /// <summary>
        /// Busca o grupo curricular e gera a descrição formatada para ele
        /// </summary>
        /// <param name="seqGrupoCurricular">Sequencial do grupo curricular</param>
        /// <returns>Descricao formatada</returns>
        public string BuscaGrupoCurricularDescricaoFormatada(long seqGrupoCurricular)
        {
            var grupoCurricular = this.SearchProjectionByKey(new SMCSeqSpecification<GrupoCurricular>(seqGrupoCurricular), x => new
            {
                x.Descricao,
                TipoConfiguracaoDescricao = x.TipoConfiguracaoGrupoCurricular.Descricao,
                x.FormatoConfiguracaoGrupo,
                x.QuantidadeHoraRelogio,
                x.QuantidadeHoraAula,
                x.QuantidadeCreditos,
                x.QuantidadeItens,
            });

            var descricaoFormatada = GerarDescricaoGrupoCurricular(
                     grupoCurricular.Descricao,
                     grupoCurricular.TipoConfiguracaoDescricao,
                     grupoCurricular.FormatoConfiguracaoGrupo,
                     grupoCurricular.QuantidadeHoraRelogio,
                     grupoCurricular.QuantidadeHoraAula,
                     grupoCurricular.QuantidadeCreditos,
                     grupoCurricular.QuantidadeItens);

            return descricaoFormatada;
        }

        /// <summary>
        /// Gera a descrição do grupo curricular conforme a regra RN_CUR_045 no formato
        /// [Descrição do grupo curricular] + "-" + [Descrição do tipo de configuração] + ":" + [Carga horária/Créditos/Quantidade] +
        /// [Label: horas e horas-aulas/créditos/itens]*.
        /// </summary>
        /// <param name="descricaoGrupo">Descrição do grupo currícular</param>
        /// <param name="descricaoTipoConfiguracao">Descrição da configuração do grupo curricular</param>
        /// <param name="formatoConfiguracao">Formato da configuração do grupo curricular</param>
        /// <param name="cargaHorariaHoras">Carga horária em horas relógio do grupo curricular</param>
        /// <param name="cargaHorariaHorasAula"></param>
        /// <param name="creditos"></param>
        /// <param name="quantidadeItens"></param>
        /// <returns></returns>
        public static string GerarDescricaoGrupoCurricular(string descricaoGrupo, string descricaoTipoConfiguracao, FormatoConfiguracaoGrupo? formatoConfiguracao, short? cargaHorariaHoras, short? cargaHorariaHorasAula, short? creditos, short? quantidadeItens)
        {
            var descricao = new StringBuilder($"{descricaoGrupo} - {descricaoTipoConfiguracao}");
            switch (formatoConfiguracao)
            {
                case FormatoConfiguracaoGrupo.CargaHoraria:
                    descricao.Append(" - ");
                    descricao.Append(FormatarCargaHoraria(cargaHorariaHoras.GetValueOrDefault(), FormatoCargaHoraria.Hora));
                    descricao.Append(" - ");
                    descricao.Append(FormatarCargaHoraria(cargaHorariaHorasAula.GetValueOrDefault(), FormatoCargaHoraria.HoraAula));
                    return descricao.ToString();

                case FormatoConfiguracaoGrupo.Credito:
                    string labelCredito = creditos == 1 ? MessagesResource.Label_Credito : MessagesResource.Label_Creditos;
                    descricao.Append($" - {creditos} {labelCredito}");
                    return descricao.ToString();

                case FormatoConfiguracaoGrupo.Itens:
                    string labelItem = quantidadeItens == 1 ? MessagesResource.Label_Item : MessagesResource.Label_Itens;
                    descricao.Append($" - {quantidadeItens} {labelItem}");
                    return descricao.ToString();

                default:
                    return descricao.ToString();
            }
        }

        public int BuscarTotalDispensadoSolicitacaoDispensa(long seqGrupoCurricular)
        {
            var totalDispensado = 0;

            var specGrupoCurricular = new SMCSeqSpecification<GrupoCurricular>(seqGrupoCurricular);

            var grupoCurricular = this.SearchByKey(specGrupoCurricular);

            switch (grupoCurricular.FormatoConfiguracaoGrupo)
            {
                case FormatoConfiguracaoGrupo.CargaHoraria:
                    totalDispensado = (int)grupoCurricular.QuantidadeHoraAula;
                    break;

                case FormatoConfiguracaoGrupo.Credito:
                    totalDispensado = (int)grupoCurricular.QuantidadeCreditos;
                    break;

                case FormatoConfiguracaoGrupo.Itens:
                    totalDispensado = (int)grupoCurricular.QuantidadeItens * (int)grupoCurricular.GruposCurricularesFilhos.FirstOrDefault().QuantidadeHoraAula;
                    break;

                case FormatoConfiguracaoGrupo.Nenhum:

                    totalDispensado = (int)grupoCurricular.QuantidadeHoraAula + (int)grupoCurricular.QuantidadeHoraRelogio;

                    foreach (var item in grupoCurricular.GruposCurricularesFilhos)
                    {
                        totalDispensado += (int)item.QuantidadeHoraAula;
                    }

                    break;
            }

            return totalDispensado;
        }

        public bool ValidarFormatoConfiguracaoGrupoCurricular(long seqGrupoCurricular)
        {
            var specGrupoCurricular = new SMCSeqSpecification<GrupoCurricular>(seqGrupoCurricular);

            var grupoCurricular = this.SearchByKey(specGrupoCurricular);

            return grupoCurricular.FormatoConfiguracaoGrupo == FormatoConfiguracaoGrupo.Nenhum;
        }

        private static string FormatarCargaHoraria(short carga, FormatoCargaHoraria formato)
        {
            string label;
            if (formato == FormatoCargaHoraria.Hora)
            {
                label = carga == 1 ? MessagesResource.Label_Hora : MessagesResource.Label_Horas;
            }
            else
            {
                label = carga == 1 ? MessagesResource.Label_HoraAula : MessagesResource.Label_HorasAula;
            }
            return $"{carga} {label}";
        }

        /// <summary>
        /// Busca a estrutura de árvore dos grupos curriculares da divisão para a consulta de integralizacao
        /// </summary>
        /// <param name="seqGruposFilhos">Sequenciais dos grupos curriculares</param>
        /// <returns>Lista com a árvore de grupos curriculares</returns>
        public List<IntegralizacaoMatrizGrupoVO> BuscarTreeGruposPorGruposFilhos(List<long> seqGruposFilhos)
        {
            return RawQuery<IntegralizacaoMatrizGrupoVO>(string.Format(_buscarTreeGruposPorGruposFilho, string.Join(" , ", seqGruposFilhos)));
        }

        /// <summary>
        /// Busca as descrições de benefícios, condição de obrigatoriedade e formação específica do grupo curricular para observações de integralização curricular
        /// </summary>
        /// <param name="seqGrupos">Sequenciais dos grupos curriculares</param>
        /// <returns>Lista descrições de benefícios, condição de obrigatoriedade e formação específica</returns>
        public List<GrupoCurricularInformacaoVO> BuscarInformacaoGrupoBeneficioCondicoes(List<long> seqGrupos)
        {
            return RawQuery<GrupoCurricularInformacaoVO>(string.Format(_buscarInformacaoGrupoBeneficioCondicoes, string.Join(" , ", seqGrupos)));
        }

        /// <summary>
        /// Busca a estrutura de árvore das formações específicas do grupo
        /// </summary>
        /// <param name="seqGrupos">Sequenciais do grupos curriculares</param>
        /// <returns>Lista com a árvore de formações específicas do grupo</returns>
        public List<GrupoCurricularInformacaoFormacaoVO> BuscarInformacaoGrupoFormacaoEspecifica(List<long> seqGrupos)
        {
            return RawQuery<GrupoCurricularInformacaoFormacaoVO>(string.Format(_buscarTreeFormacoesEspecificaPorGrupo, string.Join(" , ", seqGrupos)));
        }

        /// <summary>
        /// Recupera o sequencial do curso e nível de ensino associados ao curriculo informado
        /// </summary>
        /// <param name="seq">Sequencial do grupo curricular</param>
        /// <returns>Dto de GrupoCurricular com sua formação específica, benefícios e bloqueios</returns>
        public GrupoCurricularDescricaoVO BuscarGrupoCurricularDescricao(long seq)
        {
            return this.SearchProjectionByKey(seq, grupo => new GrupoCurricularDescricaoVO()
            {
                Descricao = grupo.Descricao,
                DescricaoFormacaoEspecifica = grupo.FormacaoEspecifica == null ? "" :
                    "[ " + grupo.FormacaoEspecifica.TipoFormacaoEspecifica.Descricao + " ] " + grupo.FormacaoEspecifica.Descricao,
                DescricoesBeneficios = grupo.Beneficios.Select(s => s.Descricao).ToList(),
                DescricoesCondicoesObrigatoriedade = grupo.CondicoesObrigatoriedade.Select(s => s.Descricao).ToList()
            });
        }
    }
}