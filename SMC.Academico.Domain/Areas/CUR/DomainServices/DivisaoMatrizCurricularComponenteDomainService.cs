using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Includes;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.Validators;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Helpers;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using SMC.Framework.Validation;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class DivisaoMatrizCurricularComponenteDomainService : AcademicoContextDomain<DivisaoMatrizCurricularComponente>
    {
        #region [ DomainServices ]

        private ComponenteCurricularDomainService ComponenteCurricularDomainService
        {
            get { return this.Create<ComponenteCurricularDomainService>(); }
        }
        private CriterioAprovacaoDomainService CriterioAprovacaoDomainService
        {
            get { return this.Create<CriterioAprovacaoDomainService>(); }
        }
        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService
        {
            get { return this.Create<CurriculoCursoOfertaDomainService>(); }
        }
        private CurriculoCursoOfertaGrupoDomainService CurriculoCursoOfertaGrupoDomainService
        {
            get { return this.Create<CurriculoCursoOfertaGrupoDomainService>(); }
        }
        private DivisaoComponenteDomainService DivisaoComponenteDomainService
        {
            get { return this.Create<DivisaoComponenteDomainService>(); }
        }
        private DivisaoMatrizCurricularDomainService DivisaoMatrizCurricularDomainService
        {
            get { return this.Create<DivisaoMatrizCurricularDomainService>(); }
        }
        private DivisaoMatrizCurricularGrupoDomainService DivisaoMatrizCurricularGrupoDomainService
        {
            get { return this.Create<DivisaoMatrizCurricularGrupoDomainService>(); }
        }
        private EscalaApuracaoDomainService EscalaApuracaoDomainService
        {
            get { return this.Create<EscalaApuracaoDomainService>(); }
        }
        private GrupoCurricularComponenteDomainService GrupoCurricularComponenteDomainService
        {
            get { return this.Create<GrupoCurricularComponenteDomainService>(); }
        }
        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService
        {
            get { return this.Create<InstituicaoNivelTipoComponenteCurricularDomainService>(); }
        }
        private InstituicaoNivelTipoDivisaoComponenteDomainService InstituicaoNivelTipoDivisaoComponenteDomainService
        {
            get { return this.Create<InstituicaoNivelTipoDivisaoComponenteDomainService>(); }
        }
        private MatrizCurricularDomainService MatrizCurricularDomainService
        {
            get { return this.Create<MatrizCurricularDomainService>(); }
        }
        private TipoDivisaoComponenteDomainService TipoDivisaoComponenteDomainService
        {
            get { return this.Create<TipoDivisaoComponenteDomainService>(); }
        }
        private TurnoDomainService TurnoDomainService
        {
            get { return this.Create<TurnoDomainService>(); }
        }
        private AlunoDomainService AlunoDomainService
        {
            get { return this.Create<AlunoDomainService>(); }
        }
        private TurmaConfiguracaoComponenteDomainService TurmaConfiguracaoComponenteDomainService
        {
            get { return this.Create<TurmaConfiguracaoComponenteDomainService>(); }
        }
        private TurmaDomainService TurmaDomainService
        {
            get { return this.Create<TurmaDomainService>(); }
        }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return this.Create<PessoaAtuacaoDomainService>(); }
        }
        private InstituicaoNivelTipoTrabalhoDomainService InstituicaoNivelTipoTrabalhoDomainService
        {
            get { return this.Create<InstituicaoNivelTipoTrabalhoDomainService>(); }
        }

        #endregion [ DomainServices ]

        #region [ Queries ]

        private string _inserirDivisaoMatrizCurricularComponenteExcecaoTurno =
                        @" INSERT INTO cur.divisao_matriz_curricular_componente_excecao_turno 
                           (seq_divisao_matriz_curricular_componente, seq_turno) 
                           VALUES (@seqDivisaoMatrizCurricularComponente, @seqTurno)";

        private string _inserirDivisaoMatrizCurricularComponenteComponenteSubstituto =
                        @" INSERT INTO cur.divisao_matriz_curricular_componente_substituto 
                           (seq_divisao_matriz_curricular_componente, seq_componente_curricular) 
                           VALUES (@seqDivisaoMatrizCurricularComponente, @seqComponenteCurricular)";

        #endregion Queries

        /// <summary>
        /// Busca o cabeçalho de um componente da matriz curricular
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo curricular</param>
        /// <returns>Dados da matriz e do componente para o cabeçalho</returns>
        public DivisaoMatrizCurricularComponenteCabecalhoVO DivisaoMatrizCurricularComponenteCabecalho(long seqMatrizCurricular, long seqGrupoCurricularComponente)
        {
            // Recupera a matriz curricular
            var specMatriz = new SMCSeqSpecification<MatrizCurricular>(seqMatrizCurricular);
            var matrizCurricular = this.MatrizCurricularDomainService
                                    .SearchProjectionByKey(specMatriz, p => new
                                    {
                                        p.Codigo,
                                        p.Descricao,
                                        p.DescricaoComplementar,
                                        SeqsGruposMatriz = p.CurriculoCursoOferta
                                            .GruposCurriculares
                                            .Select(s => s.GrupoCurricular.Seq),
                                    });

            // Recupera o grupo curricular componente com o componente e níveis de ensino
            var specGrupo = new SMCSeqSpecification<GrupoCurricularComponente>(seqGrupoCurricularComponente);
            var includesGrupo = IncludesGrupoCurricularComponente.ComponenteCurricular_NiveisEnsino;
            var grupoCurricularComponente = this.GrupoCurricularComponenteDomainService.SearchByKey(specGrupo, includesGrupo);

            // Transforma a matriz no vo do header e mesca com os dados do grupo
            var divisaoComponenteVo = new DivisaoMatrizCurricularComponenteCabecalhoVO()
            {
                CodigoMatriz = matrizCurricular.Codigo,
                DescricaoMatriz = matrizCurricular.Descricao,
                DescricaoMatrizComplementar = matrizCurricular.DescricaoComplementar,
                CodigoComponente = grupoCurricularComponente.ComponenteCurricular.Codigo,
                DescricaoComponente = grupoCurricularComponente.ComponenteCurricular.Descricao,
                CargaHorariaComponente = grupoCurricularComponente.ComponenteCurricular.CargaHoraria,
                CreditosComponente = grupoCurricularComponente.ComponenteCurricular.Credito
            };

            // Recupera o formato do mesmo nível e tipo do componente curricular
            var formato = this.InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqNivelEnsino = grupoCurricularComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel == true).FirstOrDefault().SeqNivelEnsino,
                SeqTipoComponenteCurricular = grupoCurricularComponente.ComponenteCurricular.SeqTipoComponenteCurricular
            }, i => i.FormatoCargaHoraria);

            if (formato.HasValue && formato != FormatoCargaHoraria.Nenhum)
                divisaoComponenteVo.Formato = SMCEnumHelper.GetDescription(formato.Value);

            GrupoCurricularComponenteFilterSpecification specGrupos = new GrupoCurricularComponenteFilterSpecification()
            {
                SeqComponentesCurriculares = new List<long>() { grupoCurricularComponente.SeqComponenteCurricular },
                SeqGruposCurriculares = matrizCurricular.SeqsGruposMatriz.ToList()

            };

            divisaoComponenteVo.GruposPertecentes = GrupoCurricularComponenteDomainService.SearchProjectionBySpecification(specGrupos, p => p.GrupoCurricular.Descricao).Distinct().ToList();

            return divisaoComponenteVo;
        }

        public SMCPagerData<ConfiguracaoComponeteMatrizListarVO> BuscarDivisaoMatrizCurricularGruposComponentes(DivisaoMatrizCurricularComponenteFiltroVO filtrosVO)
        {
            DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var filtros = filtrosVO.Transform<DivisaoMatrizCurricularComponenteFilterSpecification>();
            //Recupera todos os grupos da matriz
            var specMatriz = new SMCSeqSpecification<MatrizCurricular>(filtros.SeqMatrizCurricular.Value);
            var consultaMatriz = this.MatrizCurricularDomainService
                .SearchProjectionByKey(specMatriz, p => new
                {
                    SeqCurriculoCursoOferta = p.SeqCurriculoCursoOferta,
                    SeqsGruposMatriz = p.CurriculoCursoOferta
                        .GruposCurriculares
                        .Select(s => s.GrupoCurricular.Seq),
                    GruposCurriculo = p.CurriculoCursoOferta
                        .Curriculo
                        .GruposCurriculares
                        .Select(s => new DivisaoMatrizCurricularComponenteGrupoTreeVO()
                        {
                            Seq = s.Seq,
                            SeqPai = s.SeqGrupoCurricularSuperior,
                            DescricaoGrupo = s.Descricao
                        })
                });

            GrupoCurricularComponenteFilterSpecification specComponentes = new GrupoCurricularComponenteFilterSpecification();
            specComponentes.DescricaoGrupoCurricular = filtros.DescricaoGrupoCurricular;
            specComponentes.SeqGruposCurriculares = consultaMatriz.SeqsGruposMatriz.ToList();
            specComponentes.SeqTipoComponente = filtros.SeqTipoComponente;

            //Caso seja informada a divisão ou o componente no filtro, filtra por esses campos
            if (filtros.SeqDivisaoMatrizCurricular.HasValue && filtros.SeqComponenteCurricular.HasValue)
            {
                //Recupera os seqs das configurações das divisões de componente da matriz
                var specDivisoes = new DivisaoMatrizCurricularFilterSpecification
                {
                    SeqMatrizCurricular = filtros.SeqMatrizCurricular,
                    Seq = filtros.SeqDivisaoMatrizCurricular,
                    SeqComponenteCurricular = filtros.SeqComponenteCurricular
                };

                var seqsGrupoCurricularComponente = this.DivisaoMatrizCurricularDomainService
                          .SearchProjectionBySpecification(specDivisoes, p => p.ConfiguracoesComponentes.Where(a => a.ConfiguracaoComponente.SeqComponenteCurricular == filtros.SeqComponenteCurricular.Value).Select(s => s.SeqGrupoCurricularComponente))
                          .SelectMany(s => s)
                          .Distinct()
                          .ToList();

                //Caso não encontre nenhum Grupo curriclar matriz componente não existe nenhum componente
                if (seqsGrupoCurricularComponente.Count() == 0)
                {
                    return new SMCPagerData<ConfiguracaoComponeteMatrizListarVO>(new List<ConfiguracaoComponeteMatrizListarVO>(), 0);
                }

                specComponentes.seqsGrupoCurricularComponente = seqsGrupoCurricularComponente;
            }
            else if (filtros.SeqDivisaoMatrizCurricular.HasValue)
            {
                //Recupera os seqs das configurações das divisões de componente da matriz
                var specDivisoes = new DivisaoMatrizCurricularFilterSpecification
                {
                    SeqMatrizCurricular = filtros.SeqMatrizCurricular,
                    Seq = filtros.SeqDivisaoMatrizCurricular
                };

                var seqsGrupoCurricularComponente = this.DivisaoMatrizCurricularDomainService
                            .SearchProjectionBySpecification(specDivisoes, p => p.ConfiguracoesComponentes.Select(s => s.SeqGrupoCurricularComponente))
                            .SelectMany(s => s)
                            .Distinct()
                            .ToList();

                specComponentes.seqsGrupoCurricularComponente = seqsGrupoCurricularComponente;
            }
            else if (filtros.SeqComponenteCurricular.HasValue)
            {
                var specConfiguracoesComponente = new GrupoCurricularComponenteFilterSpecification() { SeqComponentesCurriculares = new List<long>() { filtros.SeqComponenteCurricular.Value } };
                var seqsGrupoCurricularComponente = this.GrupoCurricularComponenteDomainService.SearchProjectionBySpecification(specConfiguracoesComponente, p => p.Seq).Distinct().ToList();

                specComponentes.seqsGrupoCurricularComponente = seqsGrupoCurricularComponente;
            }

            //Recupera paginação
            specComponentes.Merge(filtros);
            specComponentes.SetOrderBy(o => o.ComponenteCurricular.Descricao);
            specComponentes.MaxResults = filtros.MaxResults = int.MaxValue;

            //Recupera todos componentes que atendam aos filtros informados
            List<DivisaoMatrizCurricularComponenteListarVO> componentesCurriculares = this.GrupoCurricularComponenteDomainService
                .SearchProjectionBySpecification(specComponentes, componente => new DivisaoMatrizCurricularComponenteListarVO
                {
                    SeqGrupoCurricularComponente = componente.Seq,
                    SeqGrupoCurricular = componente.SeqGrupoCurricular,
                    SeqComponenteCurricular = componente.SeqComponenteCurricular,
                    SeqTipoComponenteCurricular = componente.ComponenteCurricular.SeqTipoComponenteCurricular,
                    SeqNivelEnsino = componente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel == true).FirstOrDefault().SeqNivelEnsino,
                    Codigo = componente.ComponenteCurricular.Codigo,
                    DescricaoComponente = componente.ComponenteCurricular.Descricao,
                    Credito = componente.ComponenteCurricular.Credito,
                    CargaHoraria = componente.ComponenteCurricular.CargaHoraria,
                    ExigeAssuntoComponente = componente.ComponenteCurricular.ExigeAssuntoComponente
                }).ToList();

            //Recupera as divisões já configuradas
            var includesDivisao = IncludesDivisaoMatrizCurricularComponente.DivisaoMatrizCurricular_DivisaoCurricularItem
                                | IncludesDivisaoMatrizCurricularComponente.GrupoCurricularComponente_ComponenteCurricular
                                | IncludesDivisaoMatrizCurricularComponente.ConfiguracaoComponente_ComponenteCurricular_NiveisEnsino
                                | IncludesDivisaoMatrizCurricularComponente.ConfiguracaoComponente_ComponenteCurricular_EntidadesResponsaveis_Entidade;
            var divisoesMatrizCurricular = this.SearchBySpecification(filtros, includesDivisao).ToList();

            //Recupera os formatos de carga horária por tipo de componente e nível de ensino
            var formatos = this.InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionAll(p => new
            {
                p.SeqTipoComponenteCurricular,
                p.InstituicaoNivel.SeqNivelEnsino,
                p.FormatoCargaHoraria
            }).ToList();

            var listaAgrupamento = new List<DivisaoMatrizCurricularComponenteGrupoListarVO>();
            List<long> listaSeqsGruposCurriculares = componentesCurriculares.Select(a => a.SeqGrupoCurricular).Distinct().ToList();

            //Faz o agruapamento e monta as devidas descrições
            foreach (var seqGrupoCurricular in listaSeqsGruposCurriculares)
            {
                DivisaoMatrizCurricularComponenteGrupoListarVO itemListaRetorno = new DivisaoMatrizCurricularComponenteGrupoListarVO()
                {
                    Seq = seqGrupoCurricular,
                    SeqMatrizCurricular = filtros.SeqMatrizCurricular.Value,
                    SeqCurriculoCursoOferta = consultaMatriz.SeqCurriculoCursoOferta,
                    GrupoCurricularComponente = consultaMatriz.GruposCurriculo.Where(w => w.Seq == seqGrupoCurricular).Select(d => d.DescricaoGrupo).FirstOrDefault()
                };

                var listaGrupoCurricularComponenteOrigem = componentesCurriculares.Where(a => a.SeqGrupoCurricular == seqGrupoCurricular).ToList();
                var listaGrupoCurricularComponenteRetorno = new List<GrupoCurricularComponenteListarVO>();

                foreach (var itemGrupoCurricularComponente in listaGrupoCurricularComponenteOrigem)
                {
                    string auxiliarDescricaoComponente = ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(
                            itemGrupoCurricularComponente.Codigo,
                            itemGrupoCurricularComponente.DescricaoComponente,
                            itemGrupoCurricularComponente.Credito,
                            itemGrupoCurricularComponente.CargaHoraria,
                            itemGrupoCurricularComponente.Formato);

                    var grupoCurricularComponente = new GrupoCurricularComponenteListarVO()
                    {
                        Seq = itemGrupoCurricularComponente.SeqGrupoCurricularComponente,
                        DescricaoComponente = auxiliarDescricaoComponente,
                        SeqComponenteCurricular = itemGrupoCurricularComponente.SeqComponenteCurricular
                    };

                    var listaDivisaoMatrizCurricularComponenteOrigem = divisoesMatrizCurricular.Where(d => d.SeqGrupoCurricularComponente == itemGrupoCurricularComponente.SeqGrupoCurricularComponente).ToList();
                    var listaDivisaoMatrizCurricularComponenteRetorno = new List<DivisaoMatrizCurricularComponenteListarVO>();

                    foreach (var itemDivisaoMatrizCurricularComponente in listaDivisaoMatrizCurricularComponenteOrigem)
                    {
                        var divisaoMatrizCurricularComponente = new DivisaoMatrizCurricularComponenteListarVO()
                        {
                            SeqGrupoCurricularComponente = itemGrupoCurricularComponente.SeqGrupoCurricularComponente,
                            SeqGrupoCurricular = itemGrupoCurricularComponente.SeqGrupoCurricular,
                            SeqComponenteCurricular = itemGrupoCurricularComponente.SeqComponenteCurricular,
                            SeqTipoComponenteCurricular = itemGrupoCurricularComponente.SeqTipoComponenteCurricular,
                            SeqNivelEnsino = itemGrupoCurricularComponente.SeqNivelEnsino,
                            Codigo = itemGrupoCurricularComponente.Codigo,
                            DescricaoComponente = itemGrupoCurricularComponente.DescricaoComponente,
                            Credito = itemGrupoCurricularComponente.Credito,
                            CargaHoraria = itemGrupoCurricularComponente.CargaHoraria,
                            ExigeAssuntoComponente = itemGrupoCurricularComponente.ExigeAssuntoComponente.GetValueOrDefault()
                        };

                        divisaoMatrizCurricularComponente.Seq = itemDivisaoMatrizCurricularComponente.Seq;
                        divisaoMatrizCurricularComponente.Formato = formatos.SingleOrDefault(s => s.SeqTipoComponenteCurricular == itemGrupoCurricularComponente.SeqTipoComponenteCurricular
                                                                        && s.SeqNivelEnsino == itemGrupoCurricularComponente.SeqNivelEnsino)
                                                                        ?.FormatoCargaHoraria;

                        divisaoMatrizCurricularComponente.DescricaoComponente = auxiliarDescricaoComponente;

                        var configuracao = itemDivisaoMatrizCurricularComponente?.ConfiguracaoComponente;

                        if (configuracao != null)
                        {
                            divisaoMatrizCurricularComponente.DescricaoConfiguracaoComponente = ConfiguracaoComponenteDomainService.GerarDescricaoConfiguracaoComponenteCurricular(
                                configuracao.Codigo,
                                configuracao.Descricao,
                                configuracao.DescricaoComplementar,
                                configuracao.ComponenteCurricular.Credito,
                                configuracao.ComponenteCurricular.CargaHoraria,
                                divisaoMatrizCurricularComponente.Formato,
                                configuracao.ComponenteCurricular.EntidadesResponsaveis.Select(e => e.Entidade.Sigla).ToList()
                                );
                        }

                        divisaoMatrizCurricularComponente.DescricaoDivisao = itemDivisaoMatrizCurricularComponente
                                  ?.DivisaoMatrizCurricular
                                  ?.DivisaoCurricularItem
                                  .Descricao;

                        listaDivisaoMatrizCurricularComponenteRetorno.Add(divisaoMatrizCurricularComponente);
                    }

                    grupoCurricularComponente.DivisaoMatrizCurricularComponentes = listaDivisaoMatrizCurricularComponenteRetorno.OrderBy(o => o.DescricaoConfiguracaoComponente).ToList();
                    listaGrupoCurricularComponenteRetorno.Add(grupoCurricularComponente);
                }

                itemListaRetorno.GruposCurricularesComponentes = listaGrupoCurricularComponenteRetorno;
                listaAgrupamento.Add(itemListaRetorno);
            }

            listaAgrupamento = listaAgrupamento.OrderBy(o => o.GrupoCurricularComponente).ToList();

            //Aplica o filtro de componentes com e sem configurção
            if (filtrosVO.SomenteComponentesSemConfiguracao == ComponentesConfiguracaoCadastrada.SemConfiguracao)
            {
                listaAgrupamento.ForEach(item =>
                {
                    item.GruposCurricularesComponentes.RemoveAll(r => r.DivisaoMatrizCurricularComponentes.Count() > 0);
                });
            }

            //Prepara a lista de retorno com os dados que serão exibido na tela
            var seqsComponentes = listaAgrupamento.SelectMany(sm => sm.GruposCurricularesComponentes).Select(s => s.SeqComponenteCurricular).Distinct().ToList();

            var listaretorno = new List<ConfiguracaoComponeteMatrizListarVO>();

            seqsComponentes.ForEach(seqCompoennte =>
            {
                var componente = listaAgrupamento.SelectMany(sm => sm.GruposCurricularesComponentes).FirstOrDefault(f => f.SeqComponenteCurricular == seqCompoennte);

                List<string> descricoesgrupos = listaAgrupamento.Where(w => w.GruposCurricularesComponentes.Any(a => a.SeqComponenteCurricular == seqCompoennte)).Select(s => s.GrupoCurricularComponente).ToList();

                ConfiguracaoComponeteMatrizListarVO itemRetornar = new ConfiguracaoComponeteMatrizListarVO();

                itemRetornar.Seq = componente.Seq;
                itemRetornar.DescricaoComponente = componente.DescricaoComponente;
                itemRetornar.DescricoesGrupoCurricularComponente = descricoesgrupos;
                itemRetornar.SeqComponenteCurricular = seqCompoennte;
                itemRetornar.SeqCurriculoCursoOferta = filtros.SeqCurriculoCursoOferta.Value;
                itemRetornar.SeqMatrizCurricular = filtros.SeqMatrizCurricular.Value;
                itemRetornar.DivisaoMatrizCurricularComponentes = new List<DivisaoMatrizCurricularComponenteListarVO>();

                var divisoes = listaAgrupamento.SelectMany(sm => sm.GruposCurricularesComponentes)
                                               .Where(w => w.SeqComponenteCurricular == seqCompoennte)
                                               .SelectMany(sm => sm.DivisaoMatrizCurricularComponentes).ToList();

                //Lista de codigos dos componentes curriculares
                var codigosDivisoes = divisoes.Select(s => s.Codigo).Distinct();

                //Buscar dentro de todas as DivisaoMatrizCurricularComponentes baseado na lista de codigos dando um distinct pela descrição tem em vista que 
                //é o campo comun que conseguiremos diferenciar as configurações de componete. Exemplo 3263.1 e 3263.2
                codigosDivisoes.SMCForEach(codigo =>
                {
                    itemRetornar.DivisaoMatrizCurricularComponentes.AddRange(divisoes.Where(w => w.Codigo == codigo)
                                                                   .SMCDistinct(d => d.DescricaoConfiguracaoComponente));
                });

                listaretorno.Add(itemRetornar);
            });

            EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return new SMCPagerData<ConfiguracaoComponeteMatrizListarVO>(listaretorno, listaretorno.SMCCount());
        }

        public DivisaoMatrizCurricularComponenteVO BuscarConfiguracaoNovaDivisaoMatrizCurricularComponente(DivisaoMatrizCurricularComponenteFilterSpecification filtro)
        {
            FilterHelper.DesativarFiltros(this);

            var registroVO = new DivisaoMatrizCurricularComponenteVO()
            {
                SeqGrupoCurricularComponente = filtro.SeqGrupoCurricularComponente.GetValueOrDefault(),
                SeqMatrizCurricular = filtro.SeqMatrizCurricular.GetValueOrDefault(),
                SeqCurriculoCursoOferta = filtro.SeqCurriculoCursoOferta.GetValueOrDefault()
            };

            var infoGrupoCurricular = GrupoCurricularComponenteDomainService.SearchProjectionByKey(
                                    new SMCSeqSpecification<GrupoCurricularComponente>(filtro.SeqGrupoCurricularComponente.GetValueOrDefault()),
                                    p => new
                                    {
                                        p.SeqGrupoCurricular,
                                        p.SeqComponenteCurricular,
                                        p.ComponenteCurricular.ExigeAssuntoComponente,
                                        p.ComponenteCurricular.SeqTipoComponenteCurricular,
                                        p.ComponenteCurricular.NiveisEnsino.FirstOrDefault(f => f.Responsavel).SeqNivelEnsino
                                    });

            //Recupera os sequencias do curriculo curso oferta grupo para verificar se o grupo em questão possui divisão associada
            var seqCurriculoCursoOfertaGrupo = CurriculoCursoOfertaGrupoDomainService.SearchProjectionBySpecification(
                                        new CurriculoCursoOfertaGrupoFilterSpecification() { SeqCurriculoCursoOferta = filtro.SeqCurriculoCursoOferta, SeqGrupoCurricular = infoGrupoCurricular?.SeqGrupoCurricular },
                                        p => p.Seq).FirstOrDefault();

            var divisaoGrupo = DivisaoMatrizCurricularGrupoDomainService.SearchProjectionBySpecification(
                                new DivisaoMatrizCurricularGrupoFilterSpecification() { SeqCurriculoCursoOfertaGrupo = seqCurriculoCursoOfertaGrupo },
                                p => p.Seq).ToList();

            registroVO.GrupoCurricularDivisaoCadastrada = divisaoGrupo.SMCCount() > 0;

            if (filtro?.SeqCurriculoCursoOferta.HasValue ?? false)
            {
                registroVO.SeqCurriculoCursoOferta = filtro?.SeqCurriculoCursoOferta.Value ?? 0;
                registroVO.Turnos = TurnoDomainService.BuscarTurnosNivelEnsinoPorCurriculoCursoOfertaSelect(filtro.SeqCurriculoCursoOferta.Value);
            }

            // Recupera a configuração de componente por tipo de componente e nível de ensino
            var specConfigTipoComponente = new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqNivelEnsino = infoGrupoCurricular?.SeqNivelEnsino,
                SeqTipoComponenteCurricular = infoGrupoCurricular?.SeqTipoComponenteCurricular
            };
            var configTipoComponenteNivel = this.InstituicaoNivelTipoComponenteCurricularDomainService.SearchByKey(specConfigTipoComponente);

            if (infoGrupoCurricular != null)
            {
                registroVO.GrupoCurricularComponenteExigeAssunto = infoGrupoCurricular.ExigeAssuntoComponente.GetValueOrDefault();
                registroVO.SeqComponenteCurricular = infoGrupoCurricular.SeqComponenteCurricular;
            }

            //registroVO.SeqCriterioAprovacao = configTipoComponenteNivel.SeqCriterioAprovacaoPadrao;

            // Configurações do tipo de componente por nível de ensino
            registroVO.CriterioAprovacaoObrigatorio = configTipoComponenteNivel?.CriterioAprovacaoObrigatorio ?? false;
            registroVO.QuantidadeVagasObrigatoria = configTipoComponenteNivel?.QuantidadeVagasPrevistasObrigatorio ?? false;

            FilterHelper.AtivarFiltros(this);

            return registroVO;
        }

        /// <summary>
        /// Busca uma divisão matriz curricular compoenete pelo seu grupo curricular compomente
        /// </summary>
        /// <param name="filtro">Dados do sequencial da matriz curricular e grupo componente curricular</param>
        /// <returns>Dados da divisão matriz curricular componente</returns>
        public DivisaoMatrizCurricularComponenteVO BuscarDivisaoMatrizCurricularComponente(DivisaoMatrizCurricularComponenteFilterSpecification filtro)
        {
            FilterHelper.DesativarFiltros(this);
            var includes = IncludesDivisaoMatrizCurricularComponente.ComponentesCurricularSubstitutos_EntidadesResponsaveis_Entidade
                         | IncludesDivisaoMatrizCurricularComponente.ComponentesCurricularSubstitutos_NiveisEnsino_NivelEnsino
                         | IncludesDivisaoMatrizCurricularComponente.ComponentesCurricularSubstitutos_TipoComponente
                         | IncludesDivisaoMatrizCurricularComponente.GrupoCurricularComponente_ComponenteCurricular
                         | IncludesDivisaoMatrizCurricularComponente.MatrizCurricular
                         | IncludesDivisaoMatrizCurricularComponente.DivisoesComponente
                         | IncludesDivisaoMatrizCurricularComponente.TurnosExcecao
                         | IncludesDivisaoMatrizCurricularComponente.DivisaoMatrizCurricular_DivisaoCurricularItem;

            //Recupera o registro do banco com a associação de turnos exceção
            var registro = this.SearchByKey(filtro, includes);

            //Transforma o registro mantendo o original para consulta de lista de turnos exceção
            var registroVO = registro.Transform<DivisaoMatrizCurricularComponenteVO>() ?? new DivisaoMatrizCurricularComponenteVO();

            var infoGrupoCurricular = GrupoCurricularComponenteDomainService.SearchProjectionByKey(
                                    new SMCSeqSpecification<GrupoCurricularComponente>(filtro.SeqGrupoCurricularComponente.GetValueOrDefault()),
                                    p => new
                                    {
                                        p.SeqGrupoCurricular,
                                        p.SeqComponenteCurricular,
                                        p.ComponenteCurricular.ExigeAssuntoComponente,
                                        p.ComponenteCurricular.SeqTipoComponenteCurricular,
                                        p.ComponenteCurricular.NiveisEnsino.FirstOrDefault(f => f.Responsavel).SeqNivelEnsino
                                    });

            //Recupera os sequencias do curriculo curso oferta grupo para verificar se o grupo em questão possui divisão associada
            var seqCurriculoCursoOfertaGrupo = CurriculoCursoOfertaGrupoDomainService.SearchProjectionBySpecification(
                                        new CurriculoCursoOfertaGrupoFilterSpecification() { SeqCurriculoCursoOferta = filtro.SeqCurriculoCursoOferta, SeqGrupoCurricular = infoGrupoCurricular?.SeqGrupoCurricular },
                                        p => p.Seq).FirstOrDefault();

            var divisaoGrupo = DivisaoMatrizCurricularGrupoDomainService.SearchProjectionBySpecification(
                                new DivisaoMatrizCurricularGrupoFilterSpecification() { SeqCurriculoCursoOfertaGrupo = seqCurriculoCursoOfertaGrupo },
                                p => p.Seq);

            registroVO.GrupoCurricularDivisaoCadastrada = divisaoGrupo.SMCCount() > 0;

            if (filtro?.SeqCurriculoCursoOferta.HasValue ?? false)
            {
                registroVO.SeqCurriculoCursoOferta = filtro?.SeqCurriculoCursoOferta.Value ?? 0;
                registroVO.Turnos = TurnoDomainService.BuscarTurnosNivelEnsinoPorCurriculoCursoOfertaSelect(filtro.SeqCurriculoCursoOferta.Value);
            }

            // Recupera a configuração de componente por tipo de componente e nível de ensino
            var specConfigTipoComponente = new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqNivelEnsino = infoGrupoCurricular?.SeqNivelEnsino,
                SeqTipoComponenteCurricular = infoGrupoCurricular?.SeqTipoComponenteCurricular
            };
            var configTipoComponenteNivel = this.InstituicaoNivelTipoComponenteCurricularDomainService.SearchByKey(specConfigTipoComponente);

            //Associação do turnos exceção e detalhes do critério de aprovação
            if (registro != null)
            {
                var turnos = registro.TurnosExcecao.Select(s => s.Seq).ToList();
                registroVO.TurnosExcecao = turnos;

                var criterio = this.CriterioAprovacaoDomainService.BuscarCriterioAprovacao(registro.SeqCriterioAprovacao.GetValueOrDefault());
                if (criterio != null)
                {
                    registroVO.CriterioNotaMaxima = criterio.NotaMaxima.HasValue ? criterio.NotaMaxima.ToString() : string.Empty;
                    registroVO.CriterioPercentualNotaAprovado = criterio.PercentualNotaAprovado.HasValue ? criterio.PercentualNotaAprovado.ToString() : string.Empty;
                    registroVO.CriterioPercentualFrequenciaAprovado = criterio.PercentualFrequenciaAprovado.HasValue ? criterio.PercentualFrequenciaAprovado.ToString() : string.Empty;
                    registroVO.CriterioDescricaoEscalaApuracao = criterio.EscalaApuracao?.Descricao;
                    registroVO.ApuracaoFrequencia = criterio.ApuracaoFrequencia;
                    registroVO.SeqEscalaApuracao = criterio.SeqEscalaApuracao;
                }
            }
            else
            {
                if (infoGrupoCurricular != null)
                    registroVO.GrupoCurricularComponenteExigeAssunto = infoGrupoCurricular.ExigeAssuntoComponente.GetValueOrDefault();

                registroVO.SeqCriterioAprovacao = configTipoComponenteNivel.SeqCriterioAprovacaoPadrao;
            }

            if (infoGrupoCurricular != null)
            {
                registroVO.SeqComponenteCurricular = infoGrupoCurricular.SeqComponenteCurricular;
            }

            // Configurações do tipo de componente por nível de ensino
            registroVO.CriterioAprovacaoObrigatorio = configTipoComponenteNivel?.CriterioAprovacaoObrigatorio ?? false;
            registroVO.QuantidadeVagasObrigatoria = configTipoComponenteNivel?.QuantidadeVagasPrevistasObrigatorio ?? false;

            registroVO.DivisoesComponente.SMCForEach(f =>
            {
                f.TipoComponenteCurricularTurma = this.TipoDivisaoComponenteDomainService.BuscarTipoDivisaoComponentePorDivisaoComponente(f.SeqDivisaoComponente).TipoGestaoDivisaoComponente == TipoGestaoDivisaoComponente.Turma;
            });
            FilterHelper.AtivarFiltros(this);
            return registroVO;
        }

        /// <summary>
        /// Busca uma divisão matriz curricular compoenete pelo seu grupo curricular compomente
        /// </summary>
        /// <param name="filtro">Dados do sequencial da matriz curricular e grupo componente curricular</param>
        /// <returns>Dados da divisão matriz curricular componente</returns>
        public DivisaoMatrizCurricularComponenteVO BuscarDivisaoMatrizCurricularComponenteDetalhesNumero(DivisaoMatrizCurricularComponenteFilterSpecification filtro)
        {
            var includes = IncludesDivisaoMatrizCurricularComponente.DivisaoMatrizCurricular_DivisaoCurricularItem;

            //Recupera o registro do banco com a associação de turnos exceção
            var registro = this.SearchByKey(filtro, includes);

            //Transforma o registro mantendo o original para consulta de lista de turnos exceção
            var registroVO = registro.Transform<DivisaoMatrizCurricularComponenteVO>() ?? new DivisaoMatrizCurricularComponenteVO();

            return registroVO;
        }

        /// <summary>
        /// Valida se vai exibir o assert ao salvar uma configuração
        /// </summary>
        /// <param name="divisaoMatrizCurricularComponente">Dados da divisão a ser validada</param>
        /// <returns>Retorno se vai exibir o assert, e lista de grupos curriculares do componente da configuração</returns>
        public (bool ExibirAssert, List<GrupoCurricularComponente> ListaGruposCurricularesComponente) ValidarAssertSalvar(DivisaoMatrizCurricularComponenteVO divisaoMatrizCurricularComponenteVO)
        {
            /*IRÁ EXIBIR O ASSERT AO SALVAR UMA CONFIGURAÇÃO DE COMPONENTE SE O COMPONENTE EM QUESTÃO ESTIVER EM 
            OUTROS GRUPOS CURRICULARES DA MATRIZ*/

            var specMatriz = new SMCSeqSpecification<MatrizCurricular>(divisaoMatrizCurricularComponenteVO.SeqMatrizCurricular);
            var consultaMatriz = this.MatrizCurricularDomainService
                .SearchProjectionByKey(specMatriz, p => new
                {
                    SeqsGruposMatriz = p.CurriculoCursoOferta
                        .GruposCurriculares
                        .Select(s => s.GrupoCurricular.Seq)
                });

            SMCSpecification<GrupoCurricularComponente> specComponentes =
             new SMCContainsSpecification<GrupoCurricularComponente, long>(p => p.SeqGrupoCurricular, consultaMatriz.SeqsGruposMatriz.ToArray());

            var gruposCurricularesComponenteMatriz = this.GrupoCurricularComponenteDomainService.SearchBySpecification(specComponentes, x => x.DivisoesMatrizCurricularComponente).ToList();

            var grupoCurricularComponenteDivisao = this.GrupoCurricularComponenteDomainService.SearchByKey(new SMCSeqSpecification<GrupoCurricularComponente>(divisaoMatrizCurricularComponenteVO.SeqGrupoCurricularComponente), x => x.ComponenteCurricular);
            //var gruposCurricularesComponenteMesmoComponenteDivisao = gruposCurricularesComponenteMatriz.Where(a => a.SeqComponenteCurricular == grupoCurricularComponenteDivisao.SeqComponenteCurricular && a.Seq != grupoCurricularComponenteDivisao.Seq).ToList();
            var gruposCurricularesComponenteMesmoComponenteDivisao = gruposCurricularesComponenteMatriz.Where(a => a.SeqComponenteCurricular == grupoCurricularComponenteDivisao.SeqComponenteCurricular).ToList();

            return (gruposCurricularesComponenteMesmoComponenteDivisao.Count > 1, gruposCurricularesComponenteMesmoComponenteDivisao);
        }

        /// <summary>
        /// Grava a divisão matriz curricular componente com suas divisões e componentes subistitutos
        /// </summary>
        /// <param name="divisaoMatrizCurricularComponente">Dados da divisão a ser gravada</param>
        /// <returns>Sequencial da divisão matriz curricular gravada</returns>
        public long SalvarDivisaoMatrizCurricularComponente(DivisaoMatrizCurricularComponenteVO divisaoMatrizCurricularComponenteVO)
        {
            var testeDivisoesComponente = this.SearchBySpecification(new DivisaoMatrizCurricularComponenteFilterSpecification() { SeqComponenteCurricular = divisaoMatrizCurricularComponenteVO.SeqComponenteCurricular });
            var seqsGruposCurricular = testeDivisoesComponente.Select(a => a.GrupoCurricularComponente.SeqGrupoCurricular);

            var registro = divisaoMatrizCurricularComponenteVO.Transform<DivisaoMatrizCurricularComponente>();

            //Associa os turnos exceção a divisão matriz curricular componente, como é um checkboxlist a associação é feita manual
            if (divisaoMatrizCurricularComponenteVO.TurnosExcecao != null)
            {
                registro.TurnosExcecao = new List<Turno>();
                foreach (var item in divisaoMatrizCurricularComponenteVO.TurnosExcecao)
                    registro.TurnosExcecao.Add(new Turno() { Seq = item });
            }

            // Recupera dependências utilizadas na validação
            registro.CriterioAprovacao = this.CriterioAprovacaoDomainService
                .SearchByKey(new SMCSeqSpecification<CriterioAprovacao>(registro.SeqCriterioAprovacao.GetValueOrDefault()), IncludesCriterioAprovacao.EscalaApuracao);
            var seqsEscalas = registro.DivisoesComponente
                .Where(w => w.SeqEscalaApuracao.HasValue)
                .Select(s => s.SeqEscalaApuracao.Value)
                .ToArray();
            var escalasApuracao = this.EscalaApuracaoDomainService
                .SearchBySpecification(new SMCContainsSpecification<EscalaApuracao, long>(p => p.Seq, seqsEscalas))
                .ToList();
            foreach (var divisao in registro.DivisoesComponente)
                divisao.EscalaApuracao = escalasApuracao.SingleOrDefault(s => s.Seq == divisao.SeqEscalaApuracao);

            var componenteCurricular = this.ComponenteCurricularDomainService.SearchByKey(divisaoMatrizCurricularComponenteVO.SeqComponenteCurricular);
            registro.GrupoCurricularComponente = new GrupoCurricularComponente() { ComponenteCurricular = componenteCurricular };

            // Limpa os relacionamentos dos componentes substitutos preservando apenas os seqs
            //registro.ComponentesCurricularSubstitutos = BuscarAssuntosComponenteMatrizPorConfiguracaoComponente(registro.Seq)
            //                                            .Select(s => s.Assuntos)
            //                                            .TransformList<ComponenteCurricular>();
            //registro.ComponentesCurricularSubstitutos = registro.ComponentesCurricularSubstitutos?.Select(s => new ComponenteCurricular()
            //{
            //    Seq = s.Seq,
            //    CargaHoraria = s.CargaHoraria,
            //    Credito = s.Credito
            //}).ToList();

            #region Antiga validação usando a DivisaoMatrizCurricularComponenteValidator

            //var validator = new DivisaoMatrizCurricularComponenteValidator();
            //var results = validator.Validate(registro);
            //if (!results.IsValid)
            //{
            //    var errorList = new List<SMCValidationResults>();
            //    errorList.Add(results);
            //    throw new SMCInvalidEntityException(errorList);
            //}

            #endregion

            ValidarModelo(registro);

            this.SaveEntity(registro);

            var modeloAssert = ValidarAssertSalvar(divisaoMatrizCurricularComponenteVO);

            /*Início da consistência de um mesmo componente em mais de um grupo curricular, validação para que todos os 
            registros de DivisaoMatrizCurricularComponente, referentes à uma mesma configuração de componente fiquem 
            iguais em todos os grupos curriculares daquela matriz*/
            var gruposCurricularesComponenteMesmoComponenteDivisao = modeloAssert.ListaGruposCurricularesComponente;

            foreach (var grupoCurricularComponente in gruposCurricularesComponenteMesmoComponenteDivisao)
            {
                long sequencialDivisao = 0;

                /*Busca os registros de DivisaoMatrizCurricularComponente dos grupos curriculares componentes que tem o 
                mesmo componente da divisão, e verifica se já tem algum com aquela configuração de componente, se tiver 
                altera para ficar igual, se não tiver inclui*/
                if (grupoCurricularComponente.DivisoesMatrizCurricularComponente.Any(a => a.SeqConfiguracaoComponente == divisaoMatrizCurricularComponenteVO.SeqConfiguracaoComponente))
                {
                    var divisaoAuxiliar = grupoCurricularComponente.DivisoesMatrizCurricularComponente.FirstOrDefault(a => a.SeqConfiguracaoComponente == divisaoMatrizCurricularComponenteVO.SeqConfiguracaoComponente);

                    var divisaoAlterada = registro.Transform<DivisaoMatrizCurricularComponente>();
                    divisaoAlterada.Seq = divisaoAuxiliar.Seq;
                    divisaoAlterada.SeqGrupoCurricularComponente = grupoCurricularComponente.Seq;

                    foreach (var divisaoComponente in divisaoAlterada.DivisoesComponente.ToList())
                    {
                        divisaoComponente.Seq = 0;
                        divisaoComponente.SeqDivisaoMatrizCurricularComponente = 0;
                    }

                    divisaoAlterada.GrupoCurricularComponente = this.GrupoCurricularComponenteDomainService
                       .SearchByKey(new SMCSeqSpecification<GrupoCurricularComponente>(divisaoAlterada.SeqGrupoCurricularComponente), IncludesGrupoCurricularComponente.ComponenteCurricular);

                    this.SaveEntity(divisaoAlterada);
                    sequencialDivisao = divisaoAlterada.Seq;
                }
                else
                {
                    var novaDivisao = registro.Transform<DivisaoMatrizCurricularComponente>();
                    novaDivisao.Seq = 0;
                    novaDivisao.SeqGrupoCurricularComponente = grupoCurricularComponente.Seq;
                    novaDivisao.TurnosExcecao = new List<Turno>();
                    novaDivisao.ComponentesCurricularSubstitutos = new List<ComponenteCurricular>();

                    foreach (var divisaoComponente in novaDivisao.DivisoesComponente.ToList())
                    {
                        divisaoComponente.Seq = 0;
                        divisaoComponente.SeqDivisaoMatrizCurricularComponente = 0;
                    }

                    novaDivisao.GrupoCurricularComponente = this.GrupoCurricularComponenteDomainService
                       .SearchByKey(new SMCSeqSpecification<GrupoCurricularComponente>(novaDivisao.SeqGrupoCurricularComponente), IncludesGrupoCurricularComponente.ComponenteCurricular);

                    this.SaveEntity(novaDivisao);
                    sequencialDivisao = novaDivisao.Seq;
                }

                if (divisaoMatrizCurricularComponenteVO.TurnosExcecao != null)
                {
                    foreach (var item in divisaoMatrizCurricularComponenteVO.TurnosExcecao)
                    {
                        var paramDivisaoTurno = new List<SMCFuncParameter>()
                                {
                                    new SMCFuncParameter("seqDivisaoMatrizCurricularComponente", sequencialDivisao),
                                    new SMCFuncParameter("seqTurno", item)
                                };

                        ExecuteSqlCommand(_inserirDivisaoMatrizCurricularComponenteExcecaoTurno, paramDivisaoTurno);
                    }
                }
            }
            return registro.Seq;
        }

        public void ValidarModelo(DivisaoMatrizCurricularComponente modelo)
        {
            if (modelo.CriterioAprovacao != null)
            {
                if (modelo.CriterioAprovacao.SeqEscalaApuracao.HasValue && !modelo.CriterioAprovacao.NotaMaxima.HasValue)
                {
                    var divisoesComEscalaCriterio = modelo.DivisoesComponente
                        .Where(a => a.SeqEscalaApuracao.GetValueOrDefault() == modelo.CriterioAprovacao.SeqEscalaApuracao).ToList();

                    //Se o critério tem só escala, todas as divisões só podem ter escala, e pelo menos uma divisão tem que ter a escala do critério
                    if (modelo.DivisoesComponente.Any(a => !a.SeqEscalaApuracao.HasValue || a.NotaMaxima.HasValue) || !divisoesComEscalaCriterio.Any())
                        throw new DivisaoMatrizCurricularComponenteComApenasEscalaException();
                }

                if ((!modelo.CriterioAprovacao.SeqEscalaApuracao.HasValue && modelo.CriterioAprovacao.NotaMaxima.HasValue) || modelo.CriterioAprovacao.ApuracaoNota)
                {
                    var divisoesComNotaMaxima = modelo.DivisoesComponente
                        .Where(w => w.NotaMaxima.GetValueOrDefault() > 0).ToList();

                    var divisoesSemNotaMaximaComEscalaConceito = modelo.DivisoesComponente
                        .Except(divisoesComNotaMaxima)
                        .Where(a => a.EscalaApuracao?.TipoEscalaApuracao == TipoEscalaApuracao.Conceito).ToList();

                    //Se o critério tem só nota ou existe apuração de nota, pelo menos uma das divisões tem que ter nota, e se tiver escala tem que ser diferente de conceito 
                    if (!divisoesComNotaMaxima.Any() || divisoesSemNotaMaximaComEscalaConceito.Any())
                        throw new DivisaoMatrizCurricularComponenteApuracaoNotaException();

                    if (divisoesComNotaMaxima.Sum(s => s.NotaMaxima ?? 0) != modelo.CriterioAprovacao.NotaMaxima)
                        throw new DivisaoMatrizCurricularComponenteNotaMaximaException();
                }

                if (modelo.CriterioAprovacao.SeqEscalaApuracao.HasValue && modelo.CriterioAprovacao.NotaMaxima.HasValue)
                {
                    var divisoesComEscalaConceito = modelo.DivisoesComponente
                        .Where(a => a.EscalaApuracao?.TipoEscalaApuracao == TipoEscalaApuracao.Conceito).ToList();

                    //Se o critério tem nota e escala, cada divisão tem que ter ou nota ou escala, e a que tiver de escala tem que ser diferente de conceito 
                    if (modelo.DivisoesComponente.Any(a => (a.SeqEscalaApuracao.HasValue && a.NotaMaxima.HasValue) || (!a.SeqEscalaApuracao.HasValue && !a.NotaMaxima.HasValue)) || divisoesComEscalaConceito.Any())
                        throw new DivisaoMatrizCurricularComponenteComEscalaNotaException();
                }

                if (!modelo.CriterioAprovacao.ApuracaoNota)
                {
                    var divisoesComEscalaConceito = modelo.DivisoesComponente
                        .Where(a => a.EscalaApuracao?.TipoEscalaApuracao == TipoEscalaApuracao.Conceito).ToList();

                    if (modelo.CriterioAprovacao.EscalaApuracao?.TipoEscalaApuracao != TipoEscalaApuracao.Conceito && divisoesComEscalaConceito.Any())
                        throw new DivisaoMatrizCurricularComponenteConceitoException();
                }

                if (!modelo.CriterioAprovacao.NotaMaxima.HasValue && !modelo.CriterioAprovacao.SeqEscalaApuracao.HasValue)
                {
                    //Se o critério não tem nem nota nem escala, nenhuma divisão pode ter nota nem escala
                    if (modelo.DivisoesComponente.Any(a => a.SeqEscalaApuracao.HasValue || a.NotaMaxima.HasValue))
                        throw new DivisaoMatrizCurricularComponenteCriterioSemNotaEscalaException();
                }
                else
                {
                    //Se o critério tem nota máxima ou tem escala, cada divisão deve ter nota ou escala, não pode estar vazio
                    if (modelo.DivisoesComponente.Any(a => !a.SeqEscalaApuracao.HasValue && !a.NotaMaxima.HasValue))
                        throw new DivisaoMatrizCurricularComponenteNotaOuEscalaObrigatorioException();
                }

                //Para cada divisão da configuração do componente, permitir cadastrar ou a nota máxima ou a escala de apuração, nunca os dois
                if (modelo.DivisoesComponente.Any(a => a.SeqEscalaApuracao.HasValue && a.NotaMaxima.HasValue))
                    throw new DivisaoMatrizCurricularComponenteNotaEscalaSelecionadosException();

                var divisoesComApuracaoFrequencia = modelo.DivisoesComponente
                  .Where(a => a.ApurarFrequencia).ToList();

                if (modelo.CriterioAprovacao.ApuracaoFrequencia && !divisoesComApuracaoFrequencia.Any())
                    throw new DivisaoMatrizCurricularComponenteApuracaoFrequenciaException();
            }
            else
            {
                //Se não tiver critério selecionado, não pode colocar nota nem escala (vai salvar sem critério, sem nota e sem escala)
                if (modelo.DivisoesComponente.Any(a => a.SeqEscalaApuracao.HasValue || a.NotaMaxima.HasValue))
                    throw new DivisaoMatrizCurricularComponenteSemCriterioSelecionadoException();
            }

            if (modelo.Seq > 0)
            {
                DivisaoMatrizCurricularComponente divisaoMatrizCurricularComponenteOld = this.SearchByKey(new SMCSeqSpecification<DivisaoMatrizCurricularComponente>(modelo.Seq), x => x.ComponentesCurricularSubstitutos);

                /*Não deve ser permitido alterar a configuração de componente se esta estiver vinculada a uma turma
                de uma oferta de matriz da matriz em questão.*/
                if (divisaoMatrizCurricularComponenteOld.SeqConfiguracaoComponente != modelo.SeqConfiguracaoComponente)
                {
                    var specTurmaConfiguracaoComponente = new TurmaConfiguracaoComponenteFilterSpecification()
                    {
                        SeqConfiguracaoComponente = divisaoMatrizCurricularComponenteOld.SeqConfiguracaoComponente,
                        SeqMatrizCurricular = modelo.SeqMatrizCurricular,
                        SituacaoTurmaAtual = SituacaoTurma.Ofertada
                    };

                    var dadosAssociacaoConfiguracaoTurma = this.TurmaConfiguracaoComponenteDomainService.SearchProjectionBySpecification(specTurmaConfiguracaoComponente, x => new
                    {
                        CodigoTurma = x.Turma.Codigo,
                        CursoAPartirOferta = x.RestricoesTurmaMatriz.FirstOrDefault(a => a.MatrizCurricularOferta.SeqMatrizCurricular == modelo.SeqMatrizCurricular).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                        CursoAPartirUnidade = x.RestricoesTurmaMatriz.FirstOrDefault(a => a.MatrizCurricularOferta.SeqMatrizCurricular == modelo.SeqMatrizCurricular).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.Curso.Nome,
                        Unidade = x.RestricoesTurmaMatriz.FirstOrDefault(a => a.MatrizCurricularOferta.SeqMatrizCurricular == modelo.SeqMatrizCurricular).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                        Localidade = x.RestricoesTurmaMatriz.FirstOrDefault(a => a.MatrizCurricularOferta.SeqMatrizCurricular == modelo.SeqMatrizCurricular).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                        Turno = x.RestricoesTurmaMatriz.FirstOrDefault(a => a.MatrizCurricularOferta.SeqMatrizCurricular == modelo.SeqMatrizCurricular).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.Turno.Descricao

                    }).ToList();

                    if (dadosAssociacaoConfiguracaoTurma.Any())
                        throw new DivisaoMatrizCurricularComponenteJaAssociadaTurmaException(dadosAssociacaoConfiguracaoTurma.First().CursoAPartirUnidade, dadosAssociacaoConfiguracaoTurma.First().Unidade, dadosAssociacaoConfiguracaoTurma.First().Turno);
                }

                /*Os critérios de aprovação selecionados para os níveis de ensino devem ter a mesma parametrização
                para "Existe apuração por frequência?" e "Existe apuração por nota?"
                Implementação realizada para atender o bug 41132, anotado para referencias posteriores, caso seja necessario poder alterar criterios

                Descrição do problema:
                Existiu uma consistência impedindo a alteração de critério de aprovação para um critério com  valor diferente para "Existe apuração 
                por frequência?" ou "Existe apuração por nota?"
                Não foi identificada a necessidade dessa regra pois existia a possibilidade de exclusão e reinclusão com novos parâmetros. 
                Em reunião no dia 08/09/2021 com Janice, Serafim, Ana Luiza e Sirley decidimos retirar a consistência. Se for necessário 
                retornar favor comunicar BUG envolvidos: 41132, 42822 e 43853
                */
                //if (divisaoMatrizCurricularComponenteOld.SeqCriterioAprovacao.HasValue && modelo.SeqCriterioAprovacao.HasValue && divisaoMatrizCurricularComponenteOld.SeqCriterioAprovacao.Value != modelo.SeqCriterioAprovacao.Value)
                //{
                //    var criterioAprovacaoOld = this.CriterioAprovacaoDomainService.SearchByKey(new SMCSeqSpecification<CriterioAprovacao>(divisaoMatrizCurricularComponenteOld.SeqCriterioAprovacao.Value));

                //    if (criterioAprovacaoOld.ApuracaoFrequencia != modelo.CriterioAprovacao.ApuracaoFrequencia || criterioAprovacaoOld.ApuracaoNota != modelo.CriterioAprovacao.ApuracaoNota)
                //        throw new DivisaoMatrizCurricularComponenteCriterioAprovacaoException();
                //}
            }
        }

        /// <summary>
        /// Monta a hierarquia de grupos para exibição na lista detalhada de componentes da matriz
        /// </summary>
        /// <param name="grupos">Todos grupos de componentes da matriz</param>
        /// <param name="seqGrupo">Sequencial do grupo com os componentes que será listado na matriz</param>
        /// <returns>Dados a hierarquia de componente onde a folha é o grupo com os componentes curriculares</returns>
        private List<DivisaoMatrizCurricularComponenteGrupoTreeVO> MontarHierarquiaGrupo(IEnumerable<DivisaoMatrizCurricularComponenteGrupoTreeVO> grupos, long seqGrupo)
        {
            var hierarquiaGrupo = new List<DivisaoMatrizCurricularComponenteGrupoTreeVO>();
            long? seqGrupoAtual = seqGrupo;
            do
            {
                var grupoAtual = grupos.Single(s => s.Seq == seqGrupoAtual);
                hierarquiaGrupo.Add(grupoAtual);
                seqGrupoAtual = grupoAtual.SeqPai;
            } while (seqGrupoAtual.HasValue);
            return hierarquiaGrupo;
        }

        /// <summary>
        /// Busca as divisoes de uma divisão matriz curricular compoenete
        /// </summary>
        /// <param name="seq">Sequencial da  divisão matriz curricular compoenete</param>
        /// <returns>ados da matriz curricular divisão componente</returns>
        public List<MatrizCurricularDivisaoComponente> BuscarDivisaoMatrizCurricularComponenteDivisoes(long seq)
        {
            var includes = IncludesDivisaoMatrizCurricularComponente.DivisoesComponente_DivisaoComponente;

            //Recupera as divisões configuradas para matriz divisao
            var registro = this.SearchByKey(new SMCSeqSpecification<DivisaoMatrizCurricularComponente>(seq), includes);

            return registro.DivisoesComponente.ToList();
        }

        /// <summary>
        /// Busca a lista de componente curricular assunto de acordo com as ofertas de matriz e componentes selecionados
        /// </summary>
        /// <param name="seqsMatrizCurricular">Sequenciais das matrizes curriculares</param>
        /// <returns>Lista de componentes assunto</returns>
        public List<SMCDatasourceItem> BuscarDivisaoComponenteAssuntoSelect(List<long> seqsMatrizCurricularOferta)
        {
            {
                var includes = IncludesDivisaoMatrizCurricularComponente.ComponentesCurricularSubstitutos;

                DivisaoMatrizCurricularComponenteFilterSpecification filtro = new DivisaoMatrizCurricularComponenteFilterSpecification();
                filtro.SeqsMatrizCurricularOferta = seqsMatrizCurricularOferta;

                //Recupera o registro do banco com a associação de turnos exceção
                var registro = this.SearchBySpecification(filtro, includes).SelectMany(s => s.ComponentesCurricularSubstitutos).Distinct();

                var retorno = new List<SMCDatasourceItem>();
                registro.SMCForEach(f =>
                {
                    retorno.Add(new SMCDatasourceItem() { Seq = f.Seq, Descricao = f.Descricao });
                });

                return retorno;
            }
        }

        public List<DivisaoMatrizCurricularComponente> BuscarConfiguracaoComponentesCurricularesAluno(long seqAluno)
        {
            var specAluno = new SMCSeqSpecification<Aluno>(seqAluno);
            var listConfiguracaoComponente = AlunoDomainService.SearchProjectionByKey(specAluno,
                p => p.Historicos.Where(w => w.Atual).FirstOrDefault()
                      .HistoricosCicloLetivo.FirstOrDefault()
                      .PlanosEstudo.Where(w => w.Atual).FirstOrDefault()
                      .MatrizCurricularOferta.MatrizCurricular.ConfiguracoesComponente);

            return listConfiguracaoComponente.ToList();
        }

        public List<SMCDatasourceItem> BuscarDivisaoComponenteCurricularSelect(DivisaoComponenteCurricularFiltroVO filtro)
        {
            if (filtro.SeqAluno == 0)
                return new List<SMCDatasourceItem>();

            //Listar a descrição da configuração do componente:
            //Cujo tipo de divisão está configurado para o tipo de trabalho selecionado por Instituição logada, 
            //nível de ensino selecionado e tipo de componente(cur.instituicao_nivel_tipo_divisao_componente).
            //E que estejam associados a matriz curricular do plano de estudo atual do aluno.

            var specInstituicaoNivelTipoDivisaoComponente = filtro.Transform<InstituicaoNivelTipoDivisaoComponenteFilterSpecification>();

            var seqTipoDivisaoComponente = InstituicaoNivelTipoDivisaoComponenteDomainService.SearchProjectionByKey(specInstituicaoNivelTipoDivisaoComponente, i => i.SeqTipoDivisaoComponente);

            var specAluno = new SMCSeqSpecification<Aluno>(filtro.SeqAluno);

            var listConfiguracaoComponente = AlunoDomainService.SearchProjectionByKey(specAluno,
                    a =>
                        a.Historicos.FirstOrDefault(f => f.Atual)
                         .HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault()
                         .PlanosEstudo.FirstOrDefault(f => f.Atual)
                         .MatrizCurricularOferta.MatrizCurricular.ConfiguracoesComponente.Where(w => w.ConfiguracaoComponente.DivisoesComponente
                                        .Any(f => f.SeqTipoDivisaoComponente == seqTipoDivisaoComponente))
                                        .Select(f => new SMCDatasourceItem
                                        {
                                            Seq = f.ConfiguracaoComponente.DivisoesComponente.FirstOrDefault().Seq,
                                            Descricao = f.ConfiguracaoComponente.Descricao
                                        }
                    ));

            return listConfiguracaoComponente != null ? listConfiguracaoComponente.ToList() : new List<SMCDatasourceItem>();
        }

        /// <summary>
        /// Buscar os assuntos de componentes ativos que existem cadastrados em TODAS as ofertas de matizes associadas à turma. Ou seja,
        /// se a turma for compartilhada, o assunto a ser escolhido deve estar cadastrado em todas as ofertas de matrizes.
        /// A descrição do assunto deverá ser conforme RN_CUR_040 - Exibição descrição componente curricular, em ordem alfabética.
        /// </summary>
        /// <param name="seqsMatrizCurricularOferta">Sequenciais das matrizes curriculares oferta</param>
        /// <param name="seqsConfiguracoesComponente">Sequenciais das configurações componentes da turma (Principal + Compartilhadas)</param>
        /// <returns>Lista de componentes assunto</returns>
        public List<SMCDatasourceItem> BuscarAssuntosComponentesOfertasMatrizesTurma(List<long> seqsMatrizCurricularOferta, List<long> seqsConfiguracoesComponente)
        {
            var includes = IncludesDivisaoMatrizCurricularComponente.ComponentesCurricularSubstitutos_NiveisEnsino_NivelEnsino;

            var filtro = new DivisaoMatrizCurricularComponenteFilterSpecification() { SeqsConfiguracoesComponente = seqsConfiguracoesComponente, SeqsMatrizCurricularOferta = seqsMatrizCurricularOferta };

            var todosAssuntos = this.SearchBySpecification(filtro, includes).SelectMany(s => s.ComponentesCurricularSubstitutos).Where(c => c.Ativo).OrderBy(o => o.Descricao).ToList();

            if (!todosAssuntos.SMCAny()) { return new List<SMCDatasourceItem>(); }

            // Se existe apenas uma oferta de matriz, retorno todos assuntos vinculados a ela
            if (seqsMatrizCurricularOferta.SMCAny() && seqsMatrizCurricularOferta.Count == 1)
            {
                return TratarAssuntosDuplicados(todosAssuntos);
            }

            // Caso contrário
            // Retorno os assuntos filtrando apenas assuntos comuns à todas as ofertas de Matriz.
            return FiltrarAssuntosEmComum(seqsMatrizCurricularOferta, seqsConfiguracoesComponente, todosAssuntos);
        }

        /// <summary>
        /// Busca o cabeçalho da associação assunto pela configuração de componente
        /// </summary>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial divisão matriz curricular componente</param>
        /// <returns>Dados matriz e configuração de compoente para o cabeçalho</returns>
        public AssuntoComponeteMatrizCabecalhoVO BusacarAssuntoComponenteMatrizCabecalho(long seqDivisaoMatrizCurricularComponente)
        {
            AssuntoComponeteMatrizCabecalhoVO retorno = new AssuntoComponeteMatrizCabecalhoVO();
            var dados = this.SearchProjectionByKey(seqDivisaoMatrizCurricularComponente, p => new
            {
                CodigoMatriz = p.MatrizCurricular.Codigo,
                DescricaoMatriz = p.MatrizCurricular.Descricao,
                DescricaoComplementarMatriz = p.MatrizCurricular.DescricaoComplementar,
                CodigoConfiguracao = p.ConfiguracaoComponente.Codigo,
                DescricaoConfiguracao = p.ConfiguracaoComponente.Descricao,
                DescricaoComplementarConfiguracao = p.ConfiguracaoComponente.DescricaoComplementar,
                CreditoComponenteCurricular = p.ConfiguracaoComponente.ComponenteCurricular.Credito,
                CargaHorariaComponenteCurricualr = p.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,
                NivelEnsinoComponenteCurricular = p.GrupoCurricularComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel == true).FirstOrDefault().SeqNivelEnsino,
                SeqTipoComponenteCurricular = p.GrupoCurricularComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                Siglas = p.ConfiguracaoComponente.ComponenteCurricular.EntidadesResponsaveis.Select(s => s.Entidade.Sigla).ToList()
            });

            // Recupera o formato do mesmo nível e tipo do componente curricular
            var formato = this.InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqNivelEnsino = dados.NivelEnsinoComponenteCurricular,
                SeqTipoComponenteCurricular = dados.SeqTipoComponenteCurricular
            }, p => p.FormatoCargaHoraria);

            //Montar a descrição da configuração do componente RN_CUR_042 - Exibição descrição
            retorno.DescricaoConfiguracaoCompoente = ConfiguracaoComponenteDomainService.GerarDescricaoConfiguracaoComponenteCurricular(dados.CodigoConfiguracao,
                                                                                                                                   dados.DescricaoConfiguracao,
                                                                                                                                   dados.DescricaoComplementarConfiguracao,
                                                                                                                                   dados.CreditoComponenteCurricular,
                                                                                                                                   dados.CargaHorariaComponenteCurricualr,
                                                                                                                                   formato,
                                                                                                                                   dados.Siglas);
            //Montar a descrição da matriz RN_CUR_015
            retorno.DescricaoMatriz = dados.DescricaoMatriz;
            if (!string.IsNullOrEmpty(dados.DescricaoComplementarMatriz))
            {
                retorno.DescricaoMatriz += $" - {dados.DescricaoComplementarMatriz}";
            }

            return retorno;
        }

        /// <summary>
        /// Listar grupos curriculares associados ao componente curricular da configuração de componente em questão, na
        /// matriz curricular em questão com seus devidos assuntos
        /// </summary>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial divisão matriz curricular componente</param>
        /// <returns>Lista de grupos com assunto</returns>
        public List<AssuntoComponeteMatrizListarVO> BuscarAssuntosComponenteMatrizPorConfiguracaoComponente(long seqDivisaoMatrizCurricularComponente)
        {
            List<AssuntoComponeteMatrizListarVO> retorno = new List<AssuntoComponeteMatrizListarVO>();

            var dados = this.SearchProjectionByKey(seqDivisaoMatrizCurricularComponente, p => new
            {
                p.SeqMatrizCurricular,
                p.ConfiguracaoComponente.SeqComponenteCurricular,
                p.MatrizCurricular.SeqCurriculoCursoOferta
            });

            List<DivisaoMatrizCurricularComponente> divisoesComponente = this.SearchBySpecification(new DivisaoMatrizCurricularComponenteFilterSpecification()
            {
                SeqComponenteCurricular = dados.SeqComponenteCurricular,
                SeqMatrizCurricular = dados.SeqMatrizCurricular
            }).ToList();
            List<long> seqsGruposCurricularComponente = divisoesComponente.Select(s => s.SeqGrupoCurricularComponente).ToList();

            var gruposCurriculares = GrupoCurricularComponenteDomainService
                                    .SearchProjectionBySpecification(new GrupoCurricularComponenteFilterSpecification() { seqsGrupoCurricularComponente = seqsGruposCurricularComponente },
                                    p => new
                                    {
                                        p.Seq,
                                        p.GrupoCurricular.Descricao,
                                        Divisoes = p.DivisoesMatrizCurricularComponente.Select(s => new
                                        {
                                            s.Seq,
                                            s.ComponentesCurricularSubstitutos
                                        }).ToList(),
                                    }).ToList();

            gruposCurriculares.ForEach(grupo =>
            {
                grupo.Divisoes.ForEach(divisao =>
                {
                    AssuntoComponeteMatrizListarVO itemRetornar = new AssuntoComponeteMatrizListarVO();
                    itemRetornar.seqGrupoCurricularComponente = grupo.Seq;
                    itemRetornar.DescricaoGrupoCurricular = grupo.Descricao;
                    itemRetornar.SeqCurriculoCursoOferta = dados.SeqCurriculoCursoOferta;
                    itemRetornar.SeqDivisaoMatrizCurricularComponente = divisao.Seq;
                    itemRetornar.SeqMatrizCurricular = dados.SeqMatrizCurricular;
                    itemRetornar.Assuntos = divisao.ComponentesCurricularSubstitutos.TransformList<ComponenteCurricularListaVO>().ToList();
                    retorno.Add(itemRetornar);
                });
            });

            return retorno;
        }

        /// <summary>
        /// Excluir configuracao de componnente
        /// </summary>
        /// <param name="seq">Sequencial da configuração de componente</param>
        public void ExcluirConfiguracaoComponente(long seq)
        {
            ValidarAoExcluir(seq);

            /* Busca os registros de DivisaoMatrizCurricularComponente dos grupos curriculares componentes que tem o 
            mesmo componente da divisão, e verifica se já tem algum com aquela configuração de componente, exclui todos.
            Utilização da função utilizada para duplicação ao salvar e alterar para manter as mesmas regras*/
            DivisaoMatrizCurricularComponenteVO divisaoMatrizCurricularComponenteVO = this.SearchByKey(seq).Transform<DivisaoMatrizCurricularComponenteVO>();
            List<long> seqsDivisaooMatrizCurricularComponente = ValidarAssertSalvar(divisaoMatrizCurricularComponenteVO).ListaGruposCurricularesComponente
                                                                                                                .SelectMany(s => s.DivisoesMatrizCurricularComponente)
                                                                                                                .Where(w => w.SeqConfiguracaoComponente == divisaoMatrizCurricularComponenteVO.SeqConfiguracaoComponente)
                                                                                                                .Select(s => s.Seq).ToList();
            seqsDivisaooMatrizCurricularComponente.ForEach(seqDivisao =>
            {
                this.DeleteEntity(seqDivisao);
            });
        }

        private void ValidarAoExcluir(long seq)
        {
            DivisaoMatrizCurricularComponente divisaoMatrizCurricularComponente = this.SearchByKey(new SMCSeqSpecification<DivisaoMatrizCurricularComponente>(seq));

            var specTurmaConfiguracaoComponente = new TurmaConfiguracaoComponenteFilterSpecification()
            {
                SeqConfiguracaoComponente = divisaoMatrizCurricularComponente.SeqConfiguracaoComponente,
                SeqMatrizCurricular = divisaoMatrizCurricularComponente.SeqMatrizCurricular,
                SituacaoTurmaAtual = SituacaoTurma.Ofertada
            };

            var dadosConfiguracaoTurma = this.TurmaConfiguracaoComponenteDomainService.SearchProjectionBySpecification(specTurmaConfiguracaoComponente, x => new
            {
                CodigoTurma = x.Turma.Codigo,
                CursoAPartirUnidade = x.RestricoesTurmaMatriz.FirstOrDefault(a => a.MatrizCurricularOferta.SeqMatrizCurricular == divisaoMatrizCurricularComponente.SeqMatrizCurricular).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.Curso.Nome,
                Unidade = x.RestricoesTurmaMatriz.FirstOrDefault(a => a.MatrizCurricularOferta.SeqMatrizCurricular == divisaoMatrizCurricularComponente.SeqMatrizCurricular).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                Turno = x.RestricoesTurmaMatriz.FirstOrDefault(a => a.MatrizCurricularOferta.SeqMatrizCurricular == divisaoMatrizCurricularComponente.SeqMatrizCurricular).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.Turno.Descricao

            }).ToList();

            if (dadosConfiguracaoTurma.Any())
                throw new DivisaoMatrizCurricularComponenteAssociadaTurmaException(dadosConfiguracaoTurma.First().CursoAPartirUnidade, dadosConfiguracaoTurma.First().Unidade, dadosConfiguracaoTurma.First().Turno);

        }

        /// <summary>
        /// Salvar Assunto do grupo curricular
        /// </summary>
        /// <param name="seq">Sequencial do assunto</param>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial da Divisao matriz curricular componente</param>
        public void SalvarAssunto(long seq, long seqDivisaoMatrizCurricularComponente)
        {
            DivisaoMatrizCurricularComponente divisaoMatrizCurricularComponente = this.SearchByKey(seqDivisaoMatrizCurricularComponente, x => x.ComponentesCurricularSubstitutos);

            ComponenteCurricular componenteCurricular = ComponenteCurricularDomainService.SearchByKey(seq);

            //RN_CUR_085 Ao associar um componente do tipo assunto a uma configuração, este deverá ter a mesma carga
            //horária e/ ou a mesma quantidade de crédito da configuração.Em caso de violação, abortar a operação
            //e exibir mensagem:
            if (divisaoMatrizCurricularComponente.ComponentesCurricularSubstitutos.SMCCount(c => c.CargaHoraria != componenteCurricular.CargaHoraria || c.Credito != componenteCurricular.Credito) > 0)
            {
                throw new DivisaoMatrizCurricularComponenteSubstitutoException();
            }

            //RN_CUR_085 O mesmo assunto só pode ser associado uma única vez por configuração de componente e grupo curricular
            if (divisaoMatrizCurricularComponente.ComponentesCurricularSubstitutos.Where(w => w.Seq == seq).Count() > 0)
            {
                throw new DivisaoMatrizCurricularComponenteSubstitutoRepeditosMesmoGrupoCurricularException();
            }

            var paramDivisaoComponenteSubstituto = new List<SMCFuncParameter>()
                {
                    new SMCFuncParameter("seqDivisaoMatrizCurricularComponente", seqDivisaoMatrizCurricularComponente),
                    new SMCFuncParameter("seqComponenteCurricular", seq)
                };

            ExecuteSqlCommand(_inserirDivisaoMatrizCurricularComponenteComponenteSubstituto, paramDivisaoComponenteSubstituto);
        }

        /// <summary>
        /// Excluir Assunto do grupo curricular
        /// </summary>
        /// <param name="seq">Sequencial do assunto</param>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial da Divisao matriz curricular componente</param>
        public void ExcluirAssunto(long seq, long seqDivisaoMatrizCurricularComponente)
        {
            DivisaoMatrizCurricularComponente divisaoMatrizCurricularComponente = this.SearchByKey(seqDivisaoMatrizCurricularComponente, x => x.ComponentesCurricularSubstitutos);

            var specTurmaConfiguracaoComponente = new TurmaConfiguracaoComponenteFilterSpecification()
            {
                SeqConfiguracaoComponente = divisaoMatrizCurricularComponente.SeqConfiguracaoComponente,
                SeqMatrizCurricular = divisaoMatrizCurricularComponente.SeqMatrizCurricular,
                SeqAssunto = seq,
                SituacaoTurmaAtual = SituacaoTurma.Ofertada
            };

            var dadosAssociacaoConfiguracaoTurma = this.TurmaConfiguracaoComponenteDomainService.SearchProjectionBySpecification(specTurmaConfiguracaoComponente, x => new
            {
                CursoAPartirOferta = x.RestricoesTurmaMatriz.FirstOrDefault(a => a.MatrizCurricularOferta.SeqMatrizCurricular == divisaoMatrizCurricularComponente.SeqMatrizCurricular)
                                                            .MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                Unidade = x.RestricoesTurmaMatriz.FirstOrDefault(a => a.MatrizCurricularOferta.SeqMatrizCurricular == divisaoMatrizCurricularComponente.SeqMatrizCurricular)
                                                 .MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                Turno = x.RestricoesTurmaMatriz.FirstOrDefault(a => a.MatrizCurricularOferta.SeqMatrizCurricular == divisaoMatrizCurricularComponente.SeqMatrizCurricular)
                                               .MatrizCurricularOferta.CursoOfertaLocalidadeTurno.Turno.Descricao
            }).ToList();

            if (dadosAssociacaoConfiguracaoTurma.Any())
            {
                throw new AssuntoJaOfertadoEmTurmaException(dadosAssociacaoConfiguracaoTurma.First().CursoAPartirOferta,
                                                            dadosAssociacaoConfiguracaoTurma.First().Unidade,
                                                            dadosAssociacaoConfiguracaoTurma.First().Turno);
            }

            divisaoMatrizCurricularComponente.ComponentesCurricularSubstitutos = divisaoMatrizCurricularComponente.ComponentesCurricularSubstitutos.Where(w => w.Seq != seq).ToList();

            SaveEntity(divisaoMatrizCurricularComponente);
        }

        /// <summary>
        /// Buscar lista do Enum Comprovante Artigo em ordem conferme regra
        /// </summary>
        /// <returns>Lista Enum ordenada</returns>
        public List<SMCDatasourceItem> BuscarComprovacaoArtigoOrdenada()
        {
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            retorno.Add(new SMCDatasourceItem() { Seq = (long)ComprovacaoArtigo.NaoSubmetido, Descricao = SMCEnumHelper.GetDescription(ComprovacaoArtigo.NaoSubmetido) });
            retorno.Add(new SMCDatasourceItem() { Seq = (long)ComprovacaoArtigo.Submetido, Descricao = SMCEnumHelper.GetDescription(ComprovacaoArtigo.Submetido) });
            retorno.Add(new SMCDatasourceItem() { Seq = (long)ComprovacaoArtigo.Publicado, Descricao = SMCEnumHelper.GetDescription(ComprovacaoArtigo.Publicado) });

            return retorno;
        }

        /// <summary>
        /// Retorno os assuntos filtrando apenas assuntos comuns à todas as ofertas de Matriz.
        /// </summary>
        /// <param name="seqsMatrizCurricularOferta"></param>
        /// <param name="seqsConfiguracoesComponente"></param>
        /// <param name="todosAssuntos"></param>
        /// <returns></returns>
        private List<SMCDatasourceItem> FiltrarAssuntosEmComum(List<long> seqsMatrizCurricularOferta, List<long> seqsConfiguracoesComponente, List<ComponenteCurricular> todosAssuntos)
        {
            using (MiniProfiler.Current.Step("FiltrarAssuntosEmComum"))
            {
                var filtro = new DivisaoMatrizCurricularComponenteFilterSpecification
                {
                    SeqsMatrizCurricularOferta = seqsMatrizCurricularOferta
                };

                var assuntosVinculados = this.SearchProjectionBySpecification(filtro, x => new
                {
                    SeqConfiguracaoComponente = x.SeqConfiguracaoComponente,
                    SeqsComponentesCurricularSubstitutos = x.ComponentesCurricularSubstitutos.Where(c => c.Ativo).Select(c => c.Seq),
                    SeqMatrizCurricular = x.SeqMatrizCurricular
                }).Where(a => seqsConfiguracoesComponente.Contains(a.SeqConfiguracaoComponente.Value)).ToList();


                var assuntos = new List<SMCDatasourceItem>();
                foreach (var assunto in todosAssuntos)
                {
                    if (assunto == null || !seqsMatrizCurricularOferta.SMCAny() || !seqsConfiguracoesComponente.SMCAny())
                        continue;

                    var ehComum = true;
                    foreach (var seqMatriz in seqsMatrizCurricularOferta)
                    {
                        if (!assuntosVinculados.Any(a => a.SeqMatrizCurricular == seqMatriz && seqsConfiguracoesComponente.Contains(a.SeqConfiguracaoComponente.Value) && a.SeqsComponentesCurricularSubstitutos.Contains(assunto.Seq)))
                        {
                            ehComum = false;
                            break;
                        }
                    }

                    // Se assunto é comum para todas as outras Matrizes
                    if (ehComum && !assuntos.Any(a => a.Seq == assunto.Seq))
                    {
                        assuntos.Add(CriarDatasourceItemAssunto(assunto));
                    }
                }

                return assuntos;
            }
        }

        /// <summary>
        /// Instancia o assunto com a descrição conforme RN_CUR_040 - Exibição descrição componente curricular.
        /// </summary>
        /// <param name="assunto"></param>
        /// <returns></returns>
        private SMCDatasourceItem CriarDatasourceItemAssunto(ComponenteCurricular assunto)
        {
            return new SMCDatasourceItem()
            {
                Seq = assunto.Seq,
                // A descrição do assunto deverá ser conforme RN_CUR_040 - Exibição descrição componente curricular, em ordem alfabética.
                Descricao = ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(assunto)
            };
        }

        private List<SMCDatasourceItem> TratarAssuntosDuplicados(List<ComponenteCurricular> todosAssuntos)
        {
            var assuntos = new List<SMCDatasourceItem>();

            if (!todosAssuntos.SMCAny()) { return assuntos; }

            foreach (var assunto in todosAssuntos.SMCDistinct(x => x.Seq))
            {
                assuntos.Add(CriarDatasourceItemAssunto(assunto));
            }

            return assuntos;
        }
        public List<SMCDatasourceItem> BuscarDivisaoComponenteCurricularProjetoQualificacao(DivisaoComponenteCurricularFiltroVO filtro)
        {
            if (filtro.SeqAluno == 0)
                return new List<SMCDatasourceItem>();

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(filtro.SeqAluno);
            var specTipoTrabalho = new InstituicaoNivelTipoTrabalhoFilterSpecification()
            {
                SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                TrabalhoQualificacao = true
            };
            var tipoTrabalho = InstituicaoNivelTipoTrabalhoDomainService.SearchByKey(specTipoTrabalho);

            //Listar a descrição da configuração do componente:
            //Cujo tipo de divisão está configurado para o tipo de trabalho selecionado por Instituição logada, 
            //nível de ensino selecionado e tipo de componente(cur.instituicao_nivel_tipo_divisao_componente).
            //E que estejam associados a matriz curricular do plano de estudo atual do aluno.

            var specInstituicaoNivelTipoDivisaoComponente = new InstituicaoNivelTipoDivisaoComponenteFilterSpecification()
            {
                SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                SeqTipoTrabalho = tipoTrabalho.SeqTipoTrabalho
            };

            var seqTipoDivisaoComponente = InstituicaoNivelTipoDivisaoComponenteDomainService.SearchProjectionByKey(specInstituicaoNivelTipoDivisaoComponente, i => i.SeqTipoDivisaoComponente);

            var specAluno = new SMCSeqSpecification<Aluno>(filtro.SeqAluno);

            var listConfiguracaoComponente = AlunoDomainService.SearchProjectionByKey(specAluno,
                    a =>
                        a.Historicos.FirstOrDefault(f => f.Atual)
                         .HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault()
                         .PlanosEstudo.FirstOrDefault(f => f.Atual)
                         .MatrizCurricularOferta.MatrizCurricular.ConfiguracoesComponente.Where(w => w.ConfiguracaoComponente.DivisoesComponente
                                        .Any(f => f.SeqTipoDivisaoComponente == seqTipoDivisaoComponente))
                                        .Select(f => new SMCDatasourceItem
                                        {
                                            Seq = f.ConfiguracaoComponente.DivisoesComponente.FirstOrDefault().Seq,
                                            Descricao = f.ConfiguracaoComponente.Descricao
                                        }
                    ));

            return listConfiguracaoComponente != null ? listConfiguracaoComponente.ToList() : new List<SMCDatasourceItem>();
        }
        public List<DivisaoMatrizCurricularComponente> BuscarComponentesCurricularSubstitutosNaDivisaoMatrizCurricularComponente(long seqComponenteCurricularAssunto)
        {
            DivisaoMatrizCurricularComponenteFilterSpecification spec = new DivisaoMatrizCurricularComponenteFilterSpecification()
            {
                SeqComponenteCurricularAssunto = seqComponenteCurricularAssunto
            };
            var componentesCurricularSubstitutos = this.SearchBySpecification(spec, x => x.ComponentesCurricularSubstitutos).ToList();
            return componentesCurricularSubstitutos ?? new List<DivisaoMatrizCurricularComponente>();
        }
        public List<DivisaoMatrizCurricularComponente> BuscarAssossiacaoDivisaoMatrizCurricularComponente(long seqComponenteCurricular)
        {
            DivisaoMatrizCurricularComponenteFilterSpecification spec = new DivisaoMatrizCurricularComponenteFilterSpecification()
            {
                SeqComponenteCurricular = seqComponenteCurricular
            };
            var divisaoMatrizCurricularComponente = this.SearchBySpecification(spec).ToList();
            return divisaoMatrizCurricularComponente ?? new List<DivisaoMatrizCurricularComponente>();
        }
    }
}