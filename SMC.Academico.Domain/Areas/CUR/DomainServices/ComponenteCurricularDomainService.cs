using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Resources;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.Validators;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Helpers;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class ComponenteCurricularDomainService : AcademicoContextDomain<ComponenteCurricular>
    {
        #region [ DomainService ]

        private SolicitacaoDispensaDomainService SolicitacaoDispensaDomainService => Create<SolicitacaoDispensaDomainService>();
        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();
        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => this.Create<MatrizCurricularOfertaDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => this.Create<PessoaAtuacaoDomainService>();

        private AlunoDomainService AlunoDomainService => this.Create<AlunoDomainService>();

        private AlunoHistoricoDomainService AlunoHistoricoDomainService => this.Create<AlunoHistoricoDomainService>();

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => this.Create<ConfiguracaoComponenteDomainService>();

        private ComponenteCurricularOrganizacaoDomainService ComponenteCurricularOrganizacaoDomainService => this.Create<ComponenteCurricularOrganizacaoDomainService>();

        private DivisaoMatrizCurricularComponenteDomainService DivisaoMatrizCurricularComponenteDomainService => this.Create<DivisaoMatrizCurricularComponenteDomainService>();

        private DivisaoMatrizCurricularGrupoDomainService DivisaoMatrizCurricularGrupoDomainService => this.Create<DivisaoMatrizCurricularGrupoDomainService>();

        private GrupoCurricularComponenteDomainService GrupoCurricularComponenteDomainService => this.Create<GrupoCurricularComponenteDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => this.Create<HistoricoEscolarDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => this.Create<InstituicaoNivelDomainService>();

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => this.Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        private MatrizCurricularDomainService MatrizCurricularDomainService => this.Create<MatrizCurricularDomainService>();

        private TipoDivisaoComponenteDomainService TipoDivisaoComponenteDomainService => this.Create<TipoDivisaoComponenteDomainService>();

        private TurmaConfiguracaoComponenteDomainService TurmaConfiguracaoComponenteDomainService => this.Create<TurmaConfiguracaoComponenteDomainService>();

        private AlunoFormacaoDomainService AlunoFormacaoDomainService => this.Create<AlunoFormacaoDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => this.Create<FormacaoEspecificaDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Retorna o próximo código de componente da instituição
        /// </summary>
        /// <returns>Retorna o último código utilizado mais um</returns>
        public string BuscarNovoCodigoComponenteCurricular()
        {
            long ultimoCodigo = 0;

            try
            {
                // Desliga o filtro de nível de ensino (o de instituição têm que contiunar ativo)
                this.DisableFilter(FILTER.NIVEL_ENSINO);
                // Recupera os códigos de componente da instituição
                var codigosComponente = this.SearchProjectionAll(p => p.Codigo).ToList();
                // Caso tenha componentes na instituição, converte os códigos para long para recuperar o maior. Em caso de falha ou caso não encontre nenhum componente, assume 0
                ultimoCodigo = codigosComponente.Any() ? codigosComponente.Select(s => { long codigo; return long.TryParse(s, out codigo) ? codigo : 0; }).Max() : 0;
            }
            finally
            {
                this.EnableFilter(FILTER.NIVEL_ENSINO);
            }
            // Retorna o último código ++
            return (ultimoCodigo + 1).ToString();
        }

        /// <summary>
        /// Buscar os dados de um componente curricular
        /// </summary>
        /// <param name="seq">Sequencial do componente curricular</param>
        /// <returns>Informações do componente curricular</returns>
        public ComponenteCurricularVO BuscarComponenteCurricular(long seq)
        {
            var includes = IncludesComponenteCurricular.InstituicaoEnsino
                         | IncludesComponenteCurricular.TipoComponente_TiposDivisao
                         | IncludesComponenteCurricular.OrgaosReguladores
                         | IncludesComponenteCurricular.Ementas
                         | IncludesComponenteCurricular.EntidadesResponsaveis
                         | IncludesComponenteCurricular.NiveisEnsino
                         | IncludesComponenteCurricular.Organizacoes
                         | IncludesComponenteCurricular.Configuracoes_DivisoesComponente
                         | IncludesComponenteCurricular.Configuracoes_DivisoesMatrizCurricularComponente_ComponentesCurricularSubstitutos
                         | IncludesComponenteCurricular.NiveisEnsino_NivelEnsino;

            var registro = this.SearchByKey<ComponenteCurricular, ComponenteCurricularVO>(seq, includes);

            if (registro == null)
                return null;

            long seqNivelEnsino = registro.NiveisEnsino.Where(w => w.Responsavel == true).First().SeqNivelEnsino;
            var configuracao = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(seqNivelEnsino, registro.SeqTipoComponenteCurricular);
            registro.PossuiAssociacaoDivisaoMatrizes = DivisaoMatrizCurricularComponenteDomainService
                                                .BuscarAssossiacaoDivisaoMatrizCurricularComponente(seq).Any();
            registro.PossuiAssociacaoAssuntoConfiguracaoComponenteMatriz = DivisaoMatrizCurricularComponenteDomainService
                                                .BuscarComponentesCurricularSubstitutosNaDivisaoMatrizCurricularComponente(seq).Any();
            registro.SeqInstituicaoNivelResponsavel = seqNivelEnsino;
            registro.SeqInstituicaoEnsino = configuracao.InstituicaoNivel.SeqInstituicaoEnsino;
            registro.DescricaoReduzidaObrigatorio = configuracao.NomeReduzidoObrigatorio;
            registro.SiglaObrigatorio = configuracao.SiglaObrigatoria;
            registro.CargaHorariaDisplay = configuracao.ExibeCargaHoraria;
            registro.CargaHorariaObrigatorio = configuracao.ExigeCargaHoraria;
            registro.CreditoDisplay = configuracao.ExibeCredito;
            registro.CreditoObrigatorio = configuracao.ExigeCredito;
            registro.QuantidadeHorasCredito = configuracao.QuantidadeHorasPorCredito;
            registro.EmentaObrigatorio = configuracao.PermiteEmenta;
            registro.EmentaDisplay = configuracao.PermiteEmenta;
            registro.EntidadesResponsavel = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarEntidadesPorTipoComponenteSelect(seqNivelEnsino, registro.SeqTipoComponenteCurricular);
            registro.TipoOrganizacaoDisplay = configuracao.PermiteSubdivisaoOrganizacao;
            registro.OrganizacoesDisplay = configuracao.PermiteSubdivisaoOrganizacao;
            registro.NiveisEnsino = registro.NiveisEnsino.Where(w => !w.Responsavel).ToList();
            registro.RegistroTipoOrgaoRegulador = configuracao.InstituicaoNivel.TipoOrgaoRegulador.HasValue ? configuracao.InstituicaoNivel.TipoOrgaoRegulador.Value : TipoOrgaoRegulador.Nenhum;
            registro.OrgaoReguladorDisplay = configuracao.InstituicaoNivel.TipoOrgaoRegulador != null && configuracao.InstituicaoNivel.TipoOrgaoRegulador != TipoOrgaoRegulador.Nenhum;
            registro.EntidadesResponsaveisObrigatorio = configuracao.EntidadeResponsavelObrigatoria;
            registro.PermiteAssuntoComponente = configuracao.PermiteAssuntoComponente;
            registro.ConfiguracaoTipoComponenteCurricular = configuracao;

            return registro;
        }

        public List<SMCDatasourceItem> BuscarQuantidadesSemanasSelect()
        {
            var lista = new List<SMCDatasourceItem>();

            foreach (var quantidadeSemanas in SMCEnumHelper.GenerateKeyValuePair<QuantidadeSemanas>())
            {
                lista.Add(new SMCDatasourceItem() { Seq = (long)quantidadeSemanas.Key, Descricao = quantidadeSemanas.Value });
            }

            return lista;
        }

        /// <summary>
        /// Busca a descrição completa de um componente com seu tipo e tipo de organização
        /// </summary>
        /// <param name="seq">Sequencial do componente curricular</param>
        /// <returns>Descrição completa, tipo do componente e tipo da sua organização</returns>
        public ComponenteCurricularCabecalhoVO BuscarComponenteCurricularCabecalho(long seq)
        {
            var componente = this.SearchByKey(new SMCSeqSpecification<ComponenteCurricular>(seq),
                IncludesComponenteCurricular.NiveisEnsino |
                IncludesComponenteCurricular.TipoComponente);
            var componenteVo = componente.Transform<ComponenteCurricularCabecalhoVO>();
            componenteVo.DescricaoCompleta = this.GerarDescricaoComponenteCurricular(componente);
            return componenteVo;
        }

        /// <summary>
        /// Buscar a descrição completa de um componente curricular para listagem
        /// </summary>
        /// <param name="seq">Sequencial do componente curricular</param>
        /// <returns>Descrição completa do componente curricular</returns>
        public string BuscarComponenteCurricularDescricaoCompleta(long seq)
        {
            var componenteCurricular = this.SearchByKey(new SMCSeqSpecification<ComponenteCurricular>(seq), IncludesComponenteCurricular.NiveisEnsino);
            return this.GerarDescricaoComponenteCurricular(componenteCurricular);
        }

        /// <summary>
        /// Buscar os componentes curricular que atendam os filtros informados
        /// </summary>
        /// <param name="filtroVO">Filtros da listagem de componentes curricular</param>
        /// <param name="obrigatorio">Se os filtros SeqInstituicaoNivelResponsavel e SeqTipoComponenteCurricular forem obrigatorios para pesquisa</param>
        /// <returns>SMCPagerData de componentes curricular</returns>
        public SMCPagerData<ComponenteCurricularListaVO> BuscarComponentesCurriculares(ComponenteCurricularFiltroVO filtroVO, bool obrigatorio)
        {
            var filtros = filtroVO.Transform<ComponenteCurricularFilterSpecification>();

            if (filtros.SeqInstituicaoNivelResponsavel == 0)
                filtros.SeqInstituicaoNivelResponsavel = null;

            if (!filtros.SeqInstituicaoNivelResponsavel.HasValue && obrigatorio)
                return new SMCPagerData<ComponenteCurricularListaVO>();

            if (!filtros.SeqTipoComponenteCurricular.HasValue && obrigatorio)
                return new SMCPagerData<ComponenteCurricularListaVO>();

            if (filtros.SeqTipoComponentesCurriculares.SMCCount(s => s == 0) == filtros.SeqTipoComponentesCurriculares.SMCCount())
                filtros.SeqTipoComponentesCurriculares = new long[] { };

            // Caso seja para buscar os assuntos de um componente numa matriz curricular
            if (filtros.SeqMatrizCurricular.HasValue && filtroVO.SeqComponenteCurricular.HasValue)
            {
                var specDivisao = new DivisaoMatrizCurricularComponenteFilterSpecification()
                {
                    SeqMatrizCurricular = filtros.SeqMatrizCurricular,
                    SeqComponenteCurricular = filtroVO.SeqComponenteCurricular
                };
                filtros.SeqComponentesCurriculares = DivisaoMatrizCurricularComponenteDomainService
                    .SearchProjectionByKey(specDivisao, p => p.ComponentesCurricularSubstitutos.Select(s => s.Seq))?.ToArray();
            }

            // Caso seja a busca por seqs para o retorno do lookup, ignora os demais parâmetros do filtro
            if (filtros.SeqComponentesCurriculares.SMCCount() > 0)
                filtros = new ComponenteCurricularFilterSpecification() { SeqComponentesCurriculares = filtros.SeqComponentesCurriculares };

            // Caso seja para recuperar a matriz curricualr de um aluno
            if (filtroVO.SeqAluno.HasValue && filtroVO.SeqCicloLetivo.HasValue)
            {
                filtros.SeqMatrizCurricular = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(filtroVO.SeqAluno.Value), p =>
                    p.Historicos.Where(w => w.Atual).FirstOrDefault()
                        .HistoricosCicloLetivo.Where(w => w.SeqCicloLetivo == filtroVO.SeqCicloLetivo).FirstOrDefault()
                        .PlanosEstudo.FirstOrDefault()
                        .MatrizCurricularOferta.SeqMatrizCurricular);
            }

            if (filtroVO.SeqGrupoCurricular.HasValue)
            {
                filtros.SeqTipoComponentesCurriculares = InstituicaoNivelTipoComponenteCurricularDomainService
                    .BuscarTipoComponenteCurricularPorGrupoSelect(filtroVO.SeqGrupoCurricular.Value)
                    .Select(s => s.Seq).ToArray();
            }

            var configuracao = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(filtros.SeqInstituicaoNivelResponsavel.GetValueOrDefault(), filtros.SeqTipoComponenteCurricular.GetValueOrDefault());
            var permiteConfiguracaoComponente = configuracao?.PermiteConfiguracaoComponente ?? false;

            filtros.SetOrderBy(o => o.Descricao);

            int total = 0;
            var componentes = this.SearchProjectionBySpecification(filtros,
                                x => new ComponenteCurricularListaVO
                                {
                                    Seq = x.Seq,
                                    Codigo = x.Codigo,
                                    Descricao = x.Descricao,
                                    CargaHoraria = x.CargaHoraria,
                                    Credito = x.Credito,
                                    Ativo = x.Ativo,
                                    TipoOrganizacao = x.TipoOrganizacao,
                                    DescricaoTipoComponenteCurricular = x.TipoComponente.Descricao,
                                    SeqTipoComponenteCurricular = x.TipoComponente.Seq,
                                    ConfiguracaoComponenteCurricular = x.Configuracoes.Count > 0 ? ConfiguracaoComponenteCurricular.PossuiConfiguracao : ConfiguracaoComponenteCurricular.NaoPossuiConfiguracao,
                                    NiveisEnsino = x.NiveisEnsino.Select(s => new ComponenteCurricularNivelEnsinoVO
                                    {
                                        Seq = s.Seq,
                                        SeqComponenteCurricular = s.SeqComponenteCurricular,
                                        SeqNivelEnsino = s.SeqNivelEnsino,
                                        Responsavel = s.Responsavel,
                                        NivelEnsinoDescricao = s.NivelEnsino.Descricao
                                    }).ToList(),
                                    EntidadesResponsaveis = x.EntidadesResponsaveis.Select(s => new ComponenteCurricularEntidadeResponsavelVO
                                    {
                                        Seq = s.Seq,
                                        SeqComponenteCurricular = s.SeqComponenteCurricular,
                                        SeqEntidade = s.SeqEntidade,
                                        NomeEntidade = s.Entidade.Nome
                                    }).ToList(),
                                    ComponentesLegado = x.ComponentesLegado.Select(s => new ComponenteCurricularLegadoVO()
                                    {
                                        CodigoComponenteLegado = s.CodigoComponenteLegado,
                                        BancoLegado = s.BancoLegado
                                    }).ToList(),
                                    PermiteConfiguracaoComponente = permiteConfiguracaoComponente
                                }
                               , out total
                               );

            // Recupera todos os formatos para tipos de componente por nível para evitar a consulta item a item que era realizada.
            var formatosCargaHoraria = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionAll(p => new
            {
                p.InstituicaoNivel.SeqNivelEnsino,
                p.SeqTipoComponenteCurricular,
                p.FormatoCargaHoraria
            }).ToList();
            List<ComponenteCurricularListaVO> listResult = new List<ComponenteCurricularListaVO>();
            foreach (var item in componentes.ToList())
            {
                item.SeqNivelEnsino = item.NiveisEnsino.Where(s => s.Responsavel == true).FirstOrDefault()?.SeqNivelEnsino ?? 0;
                item.DescricaoNivelEnsino = item.NiveisEnsino.Where(s => s.Responsavel == true).FirstOrDefault()?.NivelEnsinoDescricao;

                item.FormatoCargaHoraria = formatosCargaHoraria
                    .FirstOrDefault(f => f.SeqNivelEnsino == item.SeqNivelEnsino
                                      && f.SeqTipoComponenteCurricular == item.SeqTipoComponenteCurricular)
                    ?.FormatoCargaHoraria;

                item.DescricaoCompleta = GerarDescricaoComponenteCurricular(
                    item.Codigo,
                    item.Descricao,
                    item.Credito,
                    item.CargaHoraria,
                    item.FormatoCargaHoraria);

                listResult.Add(item);
            }

            return new SMCPagerData<ComponenteCurricularListaVO>(listResult, total);
        }

        /// <summary>
        /// Buscar as organizações cadastradas para o componente que podem ser selecionadas no cadastro de configuração - divisão
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Lista com as organizações</returns>
        public List<SMCDatasourceItem> BuscarComponenteOrganizacoesSelect(long seqComponenteCurricular)
        {
            return this.ComponenteCurricularOrganizacaoDomainService.BuscarOrganizacoesComponenteCurricularSelect(seqComponenteCurricular);
        }

        /// <summary>
        /// Buscar a lista de componentes curriculares que pertencem ao grupo curricular da matriz e no parâmetro permitem requisito
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <param name="seqDivisaoCurricularItem">Sequencial da divisão da matriz curricular</param>
        /// <param name="tipoRequisito">PreRequisito permite apenas componentes com a mesma divisão ou divisão anterior, caso contrário serão listadaos apenas os componentes com a divisão informada ou todos se não for informada uma divisão</param>
        /// <returns>Lista com os omponentes curriculares</returns>
        public List<SMCDatasourceItem> BuscarComponenteCurricularPorMatrizRequisitoSelect(long seqMatrizCurricular, long? seqDivisaoCurricularItem, TipoRequisito? tipoRequisito = null)
        {
            var consultaMatriz = this.MatrizCurricularDomainService.SearchProjectionByKey(new SMCSeqSpecification<MatrizCurricular>(seqMatrizCurricular), matriz =>
                new
                {
                    Divisoes = matriz.DivisoesMatrizCurricular.Select(divisao => new
                    {
                        Seq = divisao.Seq,
                        NumeroDivisaoCurricularItem = divisao.DivisaoCurricularItem.Numero,
                        DescricaoDivisaoCurricularItem = divisao.DivisaoCurricularItem.Descricao,
                        ConfiguracoesComponentes = divisao.ConfiguracoesComponentes.Select(componente => new ComponenteCurricularDetalheVO()
                        {
                            Seq = componente.ConfiguracaoComponente.ComponenteCurricular.Seq,
                            Codigo = componente.ConfiguracaoComponente.ComponenteCurricular.Codigo,
                            Descricao = componente.ConfiguracaoComponente.ComponenteCurricular.Descricao,
                            CargaHoraria = componente.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,
                            Credito = componente.ConfiguracaoComponente.ComponenteCurricular.Credito,
                            SeqNivelEnsino = componente.ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.NivelEnsino.Seq).FirstOrDefault(),
                            SeqTipoComponente = componente.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular
                        }),
                        ConfiguracoesGrupos = divisao.ConfiguracoesGrupos.Select(s => s.CurriculoCursoOfertaGrupo.SeqGrupoCurricular)
                    }),
                    GruposCurriculares = matriz.CurriculoCursoOferta.GruposCurriculares.Select(s => s.SeqGrupoCurricular),
                    ComponentesCurriculares = matriz.ConfiguracoesComponente.Where(w => w.SeqDivisaoMatrizCurricular != null).Select(componente => new ComponenteCurricularDetalheVO()
                    {
                        Seq = componente.ConfiguracaoComponente.ComponenteCurricular.Seq,
                        Codigo = componente.ConfiguracaoComponente.ComponenteCurricular.Codigo,
                        Descricao = componente.ConfiguracaoComponente.ComponenteCurricular.Descricao,
                        CargaHoraria = componente.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,
                        Credito = componente.ConfiguracaoComponente.ComponenteCurricular.Credito,
                        SeqNivelEnsino = componente.ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.NivelEnsino.Seq).FirstOrDefault(),
                        SeqTipoComponente = componente.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular
                    }),
                });

            List<ComponenteCurricularDetalheVO> listComponentesCurriculares = new List<ComponenteCurricularDetalheVO>();
            List<long> listGruposCompletos = new List<long>();
            if (seqDivisaoCurricularItem.HasValue)
            {
                // TODO: Refactory, criar vo para a consulta anterior para permitir dividir este metodo e tambem evitar o codigo duplicado abaixo
                if (tipoRequisito == TipoRequisito.PreRequisito)
                {
                    listComponentesCurriculares.AddRange(consultaMatriz.Divisoes.Where(w => w.NumeroDivisaoCurricularItem <= seqDivisaoCurricularItem).SelectMany(s => s.ConfiguracoesComponentes));
                    listGruposCompletos.AddRange(consultaMatriz.Divisoes.Where(w => w.NumeroDivisaoCurricularItem <= seqDivisaoCurricularItem).SelectMany(s => s.ConfiguracoesGrupos));
                }
                else
                {
                    listComponentesCurriculares.AddRange(consultaMatriz.Divisoes.Where(w => w.NumeroDivisaoCurricularItem == seqDivisaoCurricularItem).SelectMany(s => s.ConfiguracoesComponentes));
                    listGruposCompletos.AddRange(consultaMatriz.Divisoes.Where(w => w.NumeroDivisaoCurricularItem == seqDivisaoCurricularItem).FirstOrDefault()?.ConfiguracoesGrupos ?? new List<long>());
                }
            }
            else
            {
                listComponentesCurriculares.AddRange(consultaMatriz.Divisoes.SelectMany(s => s.ConfiguracoesComponentes));
                listComponentesCurriculares.AddRange(consultaMatriz.ComponentesCurriculares);
                listGruposCompletos.AddRange(consultaMatriz.Divisoes.SelectMany(s => s.ConfiguracoesGrupos));
                listGruposCompletos.AddRange(consultaMatriz.GruposCurriculares);
            }

            if (listGruposCompletos.Count > 0)
            {
                //Recuperar todos os componentes associados ao grupo curricular da matriz
                var specGrupos = new GrupoCurricularComponenteFilterSpecification() { SeqGruposCurriculares = listGruposCompletos };
                var componenteCurricularGrupo = this.GrupoCurricularComponenteDomainService.SearchProjectionBySpecification(specGrupos,
                    p => new ComponenteCurricularDetalheVO()
                    {
                        Seq = p.ComponenteCurricular.Seq,
                        Codigo = p.ComponenteCurricular.Codigo,
                        Descricao = p.ComponenteCurricular.Descricao,
                        CargaHoraria = p.ComponenteCurricular.CargaHoraria,
                        Credito = p.ComponenteCurricular.Credito,
                        SeqNivelEnsino = p.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.NivelEnsino.Seq).FirstOrDefault(),
                        SeqTipoComponente = p.ComponenteCurricular.SeqTipoComponenteCurricular
                    });

                listComponentesCurriculares.AddRange(componenteCurricularGrupo);
            }
            List<SMCDatasourceItem> listResult = new List<SMCDatasourceItem>();
            foreach (var item in listComponentesCurriculares.OrderBy(o => o.Descricao).ToList())
            {
                //Recupera os parâmetros de acordo com o nivel de ensino e tipo do componente
                var parametros = this.BuscarConfiguracaoComponenteCurricular(item.SeqNivelEnsino, item.SeqTipoComponente);

                //Verifica se permite o cadastro de requisito
                if (parametros.PermiteCadastroRequisito || tipoRequisito.HasValue)
                {
                    item.FormatoCargaHoraria = parametros.FormatoCargaHoraria;
                    string descricao = this.GerarDescricaoComponenteCurricular(item);

                    listResult.Add(new SMCDatasourceItem() { Seq = item.Seq, Descricao = descricao });
                }
            }

            //Recuperar todos os componentes associados ao grupo curricular da matriz
            return listResult.SMCDistinct(s => s.Seq).ToList();
        }

        /// <summary>
        /// Buscar a lista de componentes curriculares que pertencem ao grupo curricular da matriz e no parâmetro permitem requisito
        /// </summary>
        /// <param name="tipoRequisito">Tipo do requisito selecionado</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular selecionada</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Lista com os omponentes curriculares</returns>
        public List<SMCDatasourceItem> BuscarComponenteCurricularPorComponenteRequisitoSelect(TipoRequisito tipoRequisito, long seqMatrizCurricular, long? seqComponenteCurricular)
        {
            if (!seqComponenteCurricular.HasValue)
                return new List<SMCDatasourceItem>();

            //Recupera a lista de sequencial dos grupos associado ao curriculo curso oferta da matri curricular selecionada
            //Se informado a divisão curricular recupera os grupos associados da divisão selecionada
            List<long> sequencialGruposMatriz = new List<long>();

            var specDivisao = new DivisaoMatrizCurricularComponenteFilterSpecification()
            {
                SeqMatrizCurricular = seqMatrizCurricular,
                SeqComponenteCurricular = seqComponenteCurricular.Value
            };

            // Recupera o número da divisão do componente
            var divisaoCurricularItem = this.DivisaoMatrizCurricularComponenteDomainService.SearchProjectionBySpecification(specDivisao, p => p.DivisaoMatrizCurricular.DivisaoCurricularItem).FirstOrDefault();
            short numeroDivisao = divisaoCurricularItem != null ? Convert.ToInt16(divisaoCurricularItem.Numero) : (Int16)0;

            //short numeroDivisao = this.DivisaoMatrizCurricularComponenteDomainService.SearchProjectionBySpecification(specDivisao, p => p.DivisaoMatrizCurricular.DivisaoCurricularItem.Numero).FirstOrDefault();

            // Caso o componente não esteja configurado, tenta encontrar uma configuração do grupo
            if (numeroDivisao == 0)
            {
                var specGrupo = new DivisaoMatrizCurricularGrupoFilterSpecification() { SeqComponenteCurricular = seqComponenteCurricular.Value };
                var numeroDivisoes = this.DivisaoMatrizCurricularGrupoDomainService.SearchProjectionBySpecification(specGrupo, p => p.DivisaoMatrizCurricular.DivisaoCurricularItem.Numero).ToList();
                if (numeroDivisoes.Count > 0)
                {
                    // Caso encontre e seja co requisito, combina os resultados de todas as divisões configuradas para o grupo
                    if (tipoRequisito == TipoRequisito.CoRequisito)
                    {
                        var componentes = new List<SMCDatasourceItem>();
                        foreach (var divisao in numeroDivisoes)
                            componentes.AddRange(this.BuscarComponenteCurricularPorMatrizRequisitoSelect(seqMatrizCurricular, divisao, tipoRequisito));

                        // Retorna todos os componentes distintos que estiverem em todas as divisões da configuração do grupo do componente informado
                        // e remove o próprio componente do resultado
                        return componentes.Where(w => w.Seq != seqComponenteCurricular)
                            .SMCDistinct(d => d.Seq)
                            .ToList();
                    }
                    // Caso contrário, assume o número da divisão como o maior número
                    else
                        numeroDivisao = numeroDivisoes.Max();
                }
            }

            // Busca todos os componentes da mesma matriz que sejam da mesma divisão do componente informado (ou também das anteriores no pré requisito)
            // e remove o próprio componente do resultado
            return this.BuscarComponenteCurricularPorMatrizRequisitoSelect(seqMatrizCurricular, numeroDivisao, tipoRequisito)
                .Where(w => w.Seq != seqComponenteCurricular)
                .ToList();
        }

        /// <summary>
        /// Recupera um componente curricular por um grupo curricular
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo curricular</param>
        /// <returns>Dados do componente curricular</returns>
        public ComponenteCurricular BuscarComponenteCurricularPorGrupoCurricularComponente(long seqGrupoCurricularComponente)
        {
            return this.GrupoCurricularComponenteDomainService
                .SearchByKey(new SMCSeqSpecification<GrupoCurricularComponente>(seqGrupoCurricularComponente), IncludesGrupoCurricularComponente.ComponenteCurricular_EntidadesResponsaveis)
                .ComponenteCurricular;
        }

        /// <summary>
        /// Busca os dados de um componente curricular com os detalhes para tela de visualização
        /// </summary>
        /// <param name="seq">Sequencial do componente curricular</param>
        /// <returns>Informações do componente curricular</returns>
        public ComponenteCurricularDetalheVO BuscarComponenteCurricularDetalhe(long seq)
        {
            //Recupera o nível ensino e o tipo componente para consultar o formato cadastrado
            var nivelTipo = this.SearchProjectionByKey(new SMCSeqSpecification<ComponenteCurricular>(seq),
            p => new ComponenteCurricularDetalheVO()
            {
                Seq = p.Seq,
                SeqNivelEnsino = p.NiveisEnsino.Where(w => w.Responsavel == true).Select(s => s.SeqNivelEnsino).FirstOrDefault(),
                SeqTipoComponente = p.SeqTipoComponenteCurricular
            });

            //Recupera o formato carga horaria dos componentes e suas divisões
            var configuracoes = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqNivelEnsino = nivelTipo.SeqNivelEnsino,
                SeqTipoComponenteCurricular = nivelTipo.SeqTipoComponente
            }, i => new { i.FormatoCargaHoraria, i.PermiteEmenta });

            var formatoCargaHoraria = configuracoes == null ? FormatoCargaHoraria.Nenhum : configuracoes.FormatoCargaHoraria;
            var permiteEmenta = configuracoes == null ? false : configuracoes.PermiteEmenta;

            var registro = this.SearchProjectionByKey(new SMCSeqSpecification<ComponenteCurricular>(seq),
            p => new ComponenteCurricularDetalheVO()
            {
                Seq = p.Seq,
                SeqNivelEnsino = p.NiveisEnsino.Where(w => w.Responsavel == true).Select(s => s.SeqNivelEnsino).FirstOrDefault(),
                SeqTipoComponente = p.SeqTipoComponenteCurricular,
                DescricaoTipoComponente = p.TipoComponente.Descricao,
                Codigo = p.Codigo,
                Descricao = p.Descricao,
                FormatoCargaHoraria = formatoCargaHoraria,
                CargaHoraria = p.CargaHoraria,
                Credito = p.Credito,
                Ativo = p.Ativo,
                DescricaoTipoOrganizacao = p.TipoOrganizacao,
                NiveisResponsavel = p.NiveisEnsino.Where(w => w.Responsavel).Select(s => new ComponenteCurricularNivelEnsinoVO
                {
                    Seq = s.Seq,
                    SeqComponenteCurricular = s.SeqComponenteCurricular,
                    SeqNivelEnsino = s.SeqNivelEnsino,
                    Responsavel = s.Responsavel,
                    NivelEnsinoDescricao = s.NivelEnsino.Descricao
                }).ToList(),
                NiveisEnsino = p.NiveisEnsino.Where(w => !w.Responsavel).Select(s => new ComponenteCurricularNivelEnsinoVO
                {
                    Seq = s.Seq,
                    SeqComponenteCurricular = s.SeqComponenteCurricular,
                    SeqNivelEnsino = s.SeqNivelEnsino,
                    Responsavel = s.Responsavel,
                    NivelEnsinoDescricao = s.NivelEnsino.Descricao
                }).ToList(),
                EntidadesResponsaveis = p.EntidadesResponsaveis.Select(s => new ComponenteCurricularEntidadeResponsavelVO
                {
                    Seq = s.Seq,
                    SeqComponenteCurricular = s.SeqComponenteCurricular,
                    SeqEntidade = s.SeqEntidade,
                    NomeEntidade = s.Entidade.Nome
                }).ToList(),
                Observacao = p.Observacao,
                PermiteEmenta = permiteEmenta,
                Ementas = p.Ementas,
                Configuracoes = p.Configuracoes.Select(s => new ComponenteCurricularDetalheConfiguracoesVO()
                {
                    Seq = s.Seq,
                    Codigo = s.Codigo,
                    Descricao = s.Descricao,
                    DescricaoComplementar = s.DescricaoComplementar,
                    FormatoCargaHoraria = formatoCargaHoraria,
                    CargaHoraria = s.ComponenteCurricular.CargaHoraria,
                    Credito = s.ComponenteCurricular.Credito,
                    EntidadesSigla = s.ComponenteCurricular.EntidadesResponsaveis.Select(e => e.Entidade.Sigla),
                    Divisoes = s.DivisoesComponente.Select(d => new ComponenteCurricularDetalheDivisoesVO()
                    {
                        Seq = d.Seq,
                        Numero = d.Numero,
                        TipoDivisao = d.TipoDivisaoComponente.Descricao,
                        FormatoCargaHoraria = formatoCargaHoraria,
                        CargaHoraria = d.CargaHoraria,
                        CargaHorariaGrade = d.CargaHorariaGrade,
                        DescricaoComponenteCurricularOrganizacao = d.ComponenteCurricularOrganizacao.Descricao
                    }).OrderBy(o => o.Numero).ToList()
                }).OrderBy(o => o.Codigo).ToList()
            });

            registro.DescricaoCompleta = this.GerarDescricaoComponenteCurricular(registro);

            return registro;
        }

        /// <summary>
        /// Salvar o componente curricular com seus filhos
        /// </summary>
        /// <param name="componenteCurricularVO"></param>
        /// <returns>Sequencial do Componente Curricular</returns>
        /// <exception cref="ComponenteCurricularAlteracaoEntidadesResponsaveisAssuntoComponentesException">RN_CUR_033.2.2 validação de associação com componente substituto</exception>
        /// <exception cref="ComponenteCurricularAlteracaoEntidadesResponsaveisGruposCurricularesException">RN_CUR_033.2.1 validação de associação com grupo curricular</exception>
        /// <exception cref="ComponenteCurricularAlteracaoEntidadesResponsaveisTurmasException">RN_CUR033.2.3 validação de associação com turma</exception>
        /// <exception cref="ComponenteCurricularConfiguracaoAssociadoDivisaoMatrizCurricularException">RN_CUR_036 Alteração componente curricular</exception>
        /// <exception cref="ComponenteCurricularQuantidadeMaximaEntidadesResponsaveisExcedidaException">RN_CUR_033.1 validação de total de entidades resposáveis</exception>
        /// <exception cref="ComponenteCurricularSemOrganizacaoException">UC_CUR_002_01_02.NV18 Caso seja selecionado um tipo de organização e não seja informada nenhuma organização</exception>
        public long SalvarComponenteCurricular(ComponenteCurricularVO componenteCurricularVO)
        {
            using (var transaction = SMCUnitOfWork.Begin())
            {
                //Cadastrar o Nível Responsavel definido no primeiro step
                if (componenteCurricularVO.SeqInstituicaoNivelResponsavel > 0)
                {
                    ComponenteCurricularNivelEnsino nivelResponsavel = new ComponenteCurricularNivelEnsino();
                    nivelResponsavel.SeqNivelEnsino = componenteCurricularVO.SeqInstituicaoNivelResponsavel;
                    nivelResponsavel.Responsavel = true;
                    if (componenteCurricularVO.NiveisEnsino == null)
                        componenteCurricularVO.NiveisEnsino = new List<ComponenteCurricularNivelEnsino>();

                    componenteCurricularVO.NiveisEnsino.Add(nivelResponsavel);
                }

                //Preenche o valor definido no parâmetro para todos os detalhes da lista
                componenteCurricularVO.OrgaosReguladores.SMCForEach(f =>
                {
                    f.TipoOrgaoRegulador = componenteCurricularVO.RegistroTipoOrgaoRegulador;
                });

                var componenteCurricular = componenteCurricularVO.Transform<ComponenteCurricular>();

                // Valida o componente curricular
                var results = new ComponenteCurricularValidator().Validate(componenteCurricular);
                if (!results.IsValid)
                {
                    var errorList = new List<SMCValidationResults>();
                    errorList.Add(results);
                    throw new SMCInvalidEntityException(errorList);
                }

                // Verificar se já existe algum componente com as mesmas configurações
                // Não é permitido incluir/alterar componentes com mesmo tipo de componente, descrição, carga-horária, qtd de créditos e entidade responsável
                var specDiferente = new ComponenteCurricularIgualFilterSpecification
                {
                    Seq = componenteCurricular.Seq,
                    Descricao = componenteCurricular.Descricao,
                    CargaHoraria = componenteCurricular.CargaHoraria,
                    Credito = componenteCurricular.Credito,
                    SeqsEntidadesResponsaveis = componenteCurricular.EntidadesResponsaveis.OrderBy(o => o.SeqEntidade).Select(s => s.SeqEntidade).ToList(),
                    SeqTipoComponenteCurricular = componenteCurricular.SeqTipoComponenteCurricular,
                    NumeroVersaoCarga = componenteCurricular.NumeroVersaoCarga
                };

                var existe = this.Count(specDiferente) > 0;
                if (existe)
                    throw new SMCApplicationException("Alteração não permitida. Já existe componente com mesmo tipo, descrição, carga-horária, crédito e entidade responsável");

                InstituicaoNivelTipoComponenteCurricular configuracaoTipoComponenteCurricular;

                // Caso seja edição, recupera o componente curricular salvo no banco e verifica se possui matriz curricular componente associado. Caso sim, não permite edição de alguns campos.
                if (componenteCurricular.Seq > 0)
                {
                    var componenteCurricularBanco = BuscarComponenteCurricular(componenteCurricular.Seq);

                    configuracaoTipoComponenteCurricular = componenteCurricularBanco.ConfiguracaoTipoComponenteCurricular;

                    if (CargaHorariaCreditosTipoOrgNivelEnsinoAlterados(componenteCurricularVO.SeqInstituicaoNivelResponsavel, componenteCurricular, componenteCurricularBanco))
                    {
                        // Exibe mensagem de validação RN_CUR_036 Alteração componente curricular
                        if (componenteCurricularBanco.PossuiAssociacaoDivisaoMatrizes)
                        {
                            //Componente curricular possui configurações associadas a uma ou mais divisões de matrizes curriculares
                            throw new ComponenteCurricularConfiguracaoAssociadoDivisaoMatrizCurricularException();
                        }
                        if (componenteCurricularBanco.PossuiAssociacaoAssuntoConfiguracaoComponenteMatriz)
                        {
                            //Componente curricular associado como assunto de alguma configuração de componente da matriz
                            throw new ComponenteCurricularAssociadoComoAssuntoNaMatrizException();
                        }
                    }

                    // Caso tenha alterado a descrição
                    if (componenteCurricular.Descricao != componenteCurricularBanco.Descricao)
                    {
                        // Busca as turmas que tem esse componente
                        var turmasComponente = TurmaDomainService.SearchProjectionBySpecification(new TurmaFilterSpecification
                        {
                            SeqComponenteCurricular = componenteCurricular.Seq
                        }, x => new
                        {
                            x.Seq,
                            DiarioFechado = x.HistoricosFechamentoDiario.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DiarioFechado,
                            TurmaConfiguracoesComponente = x.ConfiguracoesComponente.Select(c => new
                            {
                                c.Seq,
                                c.Descricao
                            }).ToList(),
                        }).ToList();

                        // Caso positivo, não permite a alteração.
                        if (turmasComponente.Any(t => t.DiarioFechado))
                            throw new ComponenteCurricularAlteracaoDescricaoTurmaDiarioFechadoException();

                        // Caso negativo (todos os diários abertos), alterar a descrição do componente,
                        // da configuração de componente e das configurações de turma
                        // FIX: Usei o mesmo recurso que já estava implementado anteriormente de replace. O ideal é arrumar para fazer a descrição corretamente de acordo com a regra de geração da descrição concatenada.

                        ///Sempre que a descrição do componente for alterada pelo usuário, as descrições de suas configurações devem ser alteradas também,
                        ///pois estas são criadas automaticamente a partir da descrição do componente.
                        //var componenteCurricularInDB = this.BuscarComponenteCurricular(componenteCurricular.Seq);
                        var regexReplace = new Regex($"{componenteCurricularBanco.Descricao} - ");
                        var descNova = $"{componenteCurricular.Descricao} - ";

                        componenteCurricularBanco.Configuracoes.SMCForEach(c =>
                        {
                            ConfiguracaoComponenteDomainService.UpdateFields(new ConfiguracaoComponente
                            {
                                Seq = c.Seq,
                                Descricao = regexReplace.Replace(c.Descricao, descNova, 1)
                            }, x => x.Descricao);
                        });

                        turmasComponente.SelectMany(t => t.TurmaConfiguracoesComponente).SMCForEach(t =>
                        {
                            TurmaConfiguracaoComponenteDomainService.UpdateFields(new TurmaConfiguracaoComponente
                            {
                                Seq = t.Seq,
                                Descricao = regexReplace.Replace(t.Descricao, descNova, 1)
                            }, x => x.Descricao);
                        });
                    }

                    // Validações da regra RN_CUR_033.2
                    // RN_CUR_033.2 validações de alterações das entidades responsáveis
                    var seqsEntidadesResponsaveisBanco = componenteCurricularBanco.EntidadesResponsaveis.OrderBy(o => o.SeqEntidade).Select(s => s.SeqEntidade);
                    var seqsEntidadesResponsaveisVo = componenteCurricular.EntidadesResponsaveis.OrderBy(o => o.SeqEntidade).Select(s => s.SeqEntidade);
                    if (!seqsEntidadesResponsaveisBanco.SequenceEqual(seqsEntidadesResponsaveisVo))
                    {
                        // RN_CUR_033.2.1 validação de associação com grupo curricular
                        var seqsComponentes = new List<long>();
                        seqsComponentes.Add(componenteCurricularBanco.Seq);
                        var specGruposCuricularesComponetes = new GrupoCurricularComponenteFilterSpecification() { SeqComponentesCurriculares = seqsComponentes };
                        if (GrupoCurricularComponenteDomainService.Count(specGruposCuricularesComponetes) > 0)
                        {
                            throw new ComponenteCurricularAlteracaoEntidadesResponsaveisGruposCurricularesException();
                        }

                        // RN_CUR_033.2.2 validação de associação com componente substituto
                        var specComponentesSubstitutos = new DivisaoMatrizCurricularComponenteFilterSpecification() { SeqComponenteCurricularAssunto = componenteCurricularBanco.Seq };
                        if (this.DivisaoMatrizCurricularComponenteDomainService.Count(specComponentesSubstitutos) > 0)
                        {
                            throw new ComponenteCurricularAlteracaoEntidadesResponsaveisAssuntoComponentesException();
                        }

                        // RN_CUR033.2.3 validação de associação com turma
                        var seqsConfiguracoes = componenteCurricularBanco.Configuracoes.Select(s => s.Seq).ToArray();
                        var specTurmas = new SMCContainsSpecification<TurmaConfiguracaoComponente, long>(p => p.SeqConfiguracaoComponente, seqsConfiguracoes);
                        if (this.TurmaConfiguracaoComponenteDomainService.Count(specTurmas) > 0)
                        {
                            throw new ComponenteCurricularAlteracaoEntidadesResponsaveisTurmasException();
                        }
                    }

                    // RN_CUR_036.2 inativa as configurações de um componente inativo
                    var specConfiguracoes = new ConfiguracaoComponenteFilterSpecification() { SeqComponenteCurricular = componenteCurricular.Seq };
                    componenteCurricular.Configuracoes = this.ConfiguracaoComponenteDomainService.SearchBySpecification(specConfiguracoes).ToList();
                    if (!componenteCurricular.Ativo)
                    {
                        componenteCurricular.Configuracoes.SMCForEach(c =>
                        {
                            c.Ativo = false;
                        });
                    }
                }
                else
                {
                    componenteCurricular.Codigo = this.BuscarNovoCodigoComponenteCurricular();
                    // Caso não seja uma edição recupera a configuração para validar a quantidade de entidades responsáveis
                    configuracaoTipoComponenteCurricular = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(componenteCurricular.RecuperarSeqNivelEnsinoResponsavel(), componenteCurricular.SeqTipoComponenteCurricular);
                }

                // RN_CUR_033.1 validação de obrigatoriedade de entidades resposáveis
                if (componenteCurricular.EntidadesResponsaveis.SMCCount() == 0 && configuracaoTipoComponenteCurricular.EntidadeResponsavelObrigatoria)
                {
                    throw new ComponenteCurricularObrigatorioEntidadeResponsavelException();
                }

                // RN_CUR_033.1 validação de total de entidades resposáveis
                if (componenteCurricular.EntidadesResponsaveis.SMCCount() > configuracaoTipoComponenteCurricular.QuantidadeMaximaEntidadeResponsavel.GetValueOrDefault())
                {
                    throw new ComponenteCurricularQuantidadeMaximaEntidadesResponsaveisExcedidaException();
                }

                // UC_CUR_002_01_02.NV18
                if (componenteCurricular.TipoOrganizacao.HasValue && componenteCurricular.Organizacoes.SMCCount() == 0)
                {
                    throw new ComponenteCurricularSemOrganizacaoException();
                }

                this.SaveEntity(componenteCurricular);

                var listDivisao = TipoDivisaoComponenteDomainService.BuscarTipoDivisaoComponentePorComponenteSelect(componenteCurricular.Seq);

                // Se possuir apenas um tipo de divisão disponivel criar o registro configuração e divisão
                // Preenchimento dos campos da Configuração do Componente:
                if (listDivisao.Count == 1)
                {
                    var seqCodigo = ConfiguracaoComponenteDomainService.ConfiguracaoComponenteSequencial(componenteCurricular.Seq);

                    // Verifica se já possui configuração para o registro
                    if (seqCodigo == 1)
                    {
                        ConfiguracaoComponenteVO configuracao = new ConfiguracaoComponenteVO();
                        configuracao.SeqComponenteCurricular = componenteCurricular.Seq;
                        configuracao.Codigo = string.Format("{0}.{1}", componenteCurricular.Codigo, seqCodigo);
                        configuracao.Descricao = componenteCurricular.Descricao;
                        configuracao.Ativo = true;

                        ConfiguracaoComponenteDivisaoVO divisao = new ConfiguracaoComponenteDivisaoVO();
                        divisao.Numero = 1;
                        divisao.SeqTipoDivisaoComponente = listDivisao[0].Seq;
                        if (componenteCurricular.CargaHoraria.GetValueOrDefault() > 0)
                            divisao.CargaHoraria = componenteCurricular.CargaHoraria.GetValueOrDefault();
                        divisao.PermiteGrupo = false;

                        configuracao.DivisoesComponente = new List<ConfiguracaoComponenteDivisaoVO>();
                        configuracao.DivisoesComponente.Add(divisao);

                        //Salvar a configuração do componente
                        ConfiguracaoComponenteDomainService.SalvarConfiguracaoComponente(configuracao);
                    }
                }

                transaction.Commit();

                return componenteCurricular.Seq;
            }
        }


        /// <summary>
        /// Valida se os campos Carga horária, crédito, tipo organização ou Instituição nível responsável foram alterados
        /// </summary>
        /// <param name="SeqInstituicaoNivelResponsavel">PK da Instituição Nível Responsável</param>
        /// <param name="componenteCurricular">Componente curricular preenchido na tela</param>
        /// <param name="componenteCurricularBanco">Componente curricular recuperado da base</param>
        /// <returns>True se um dos campos tiver sido alterado</returns>
        private static bool CargaHorariaCreditosTipoOrgNivelEnsinoAlterados(long SeqInstituicaoNivelResponsavel, ComponenteCurricular componenteCurricular, ComponenteCurricularVO componenteCurricularBanco)
        {
            return componenteCurricular.CargaHoraria != componenteCurricularBanco.CargaHoraria ||
                   componenteCurricular.Credito != componenteCurricularBanco.Credito ||
                   componenteCurricular.TipoOrganizacao != componenteCurricularBanco.TipoOrganizacao ||
                   SeqInstituicaoNivelResponsavel != componenteCurricularBanco.SeqInstituicaoNivelResponsavel;
        }
        /// <summary>
        /// Busca as configurações do componente curricular na instituição atual com o nível e tipo informados
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino do componente curricular</param>
        /// <param name="seqTipoComponenteCurricular">Sequencial do tipo do componente curricular</param>
        /// <returns>Configurações para o componente com o tipo e nível de ensino informados</returns>
        private InstituicaoNivelTipoComponenteCurricular BuscarConfiguracaoComponenteCurricular(long seqNivelEnsino, long seqTipoComponenteCurricular)
        {
            var spec = new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsino,
                SeqTipoComponenteCurricular = seqTipoComponenteCurricular
            };
            return this.InstituicaoNivelTipoComponenteCurricularDomainService.SearchByKey(spec);
        }

        /// <summary>
        /// Gera a descrição do componente curricular segundo a regra RN_CUR_040 com
        /// </summary>
        /// <param name="componenteCurricular">Componente com seus níveis de ensino</param>
        /// <returns>Descrição do componente com a máscara [Código do componente] + "-" + [Descrição] + "-" + [Carga horária] + [label parametrizado] + "-" + [Créditos] + "Créditos"</returns>
        public string GerarDescricaoComponenteCurricular(ComponenteCurricular componenteCurricular)
        {
            var configuracao = this.BuscarConfiguracaoComponenteCurricular(
                componenteCurricular.RecuperarSeqNivelEnsinoResponsavel(),
                componenteCurricular.SeqTipoComponenteCurricular);

            return GerarDescricaoComponenteCurricular(
                componenteCurricular.Codigo,
                componenteCurricular.Descricao,
                componenteCurricular.Credito,
                componenteCurricular.CargaHoraria,
                configuracao.FormatoCargaHoraria);
        }

        /// <summary>
        /// Gera a descrição do componente curricular segundo a regra RN_CUR_040 com
        /// </summary>
        /// <param name="componenteCurricular">Componente com o formato de carga horária</param>
        /// <returns>Descrição do componente com a máscara [Código do componente] + "-" + [Descrição] + "-" + [Carga horária] + [label parametrizado] + "-" + [Créditos] + "Créditos"</returns>
        private string GerarDescricaoComponenteCurricular(ComponenteCurricularDetalheVO componenteCurricular)
        {
            if (!componenteCurricular.FormatoCargaHoraria.HasValue)
            {
                componenteCurricular.FormatoCargaHoraria = this.BuscarConfiguracaoComponenteCurricular(
                    componenteCurricular.SeqNivelEnsino,
                    componenteCurricular.SeqTipoComponente)
                        .FormatoCargaHoraria;
            }

            return GerarDescricaoComponenteCurricular(
                componenteCurricular.Codigo,
                componenteCurricular.Descricao,
                componenteCurricular.Credito,
                componenteCurricular.CargaHoraria,
                componenteCurricular.FormatoCargaHoraria);
        }

        /// <summary>
        /// Gera a descrição do componente curricular segundo a regra RN_CUR_040 com
        /// </summary>
        /// <param name="codigo">Código do componente</param>
        /// <param name="descricao">Descrição do componente</param>
        /// <param name="creditos">Créditos do componente</param>
        /// <param name="cargaHoraria">Carga horária do componente</param>
        /// <param name="formatoCargaHoraria">Formato de carga horária na instituição para o nível de ensino e tipo do componente</param>
        /// <returns>Descrição do componente com a máscara [Código do componente] + "-" + [Descrição] + "-" + [Carga horária] + [label parametrizado] + "-" + [Créditos] + "Créditos"</returns>
        public static string GerarDescricaoComponenteCurricular(string codigo, string descricao, short? creditos, short? cargaHoraria, FormatoCargaHoraria? formatoCargaHoraria)
        {
            // Prepara os labels para o formato e quantidade
            string labelHoraAula = null;
            if (formatoCargaHoraria == FormatoCargaHoraria.Hora)
            {
                labelHoraAula = cargaHoraria == 1 ? MessagesResource.Label_Hora : MessagesResource.Label_Horas;
            }
            else
            {
                labelHoraAula = cargaHoraria == 1 ? MessagesResource.Label_HoraAula : MessagesResource.Label_HorasAula;
            }
            string labelCredito = creditos == 1 ? MessagesResource.Label_Credito : MessagesResource.Label_Creditos;

            // Retorna a descrição completa com os labels aplicados aos campos que tenham valor
            if (cargaHoraria.HasValue && creditos.HasValue)
            {
                return $"{codigo} - {descricao} - {cargaHoraria} {labelHoraAula} - {creditos} {labelCredito}";
            }
            if (cargaHoraria.HasValue)
            {
                return $"{codigo} - {descricao} - {cargaHoraria} {labelHoraAula}";
            }
            if (creditos.HasValue)
            {
                return $"{codigo} - {descricao} - {creditos} {labelCredito}";
            }
            return $"{codigo} - {descricao}";
        }

        public List<SMCDatasourceItem> BuscarComponenteCurricularSelect(ComponenteCurricularFiltroVO filtroVO)
        {
            //FIX: Remover ao corrigir como o dynamic passa parâmetros para os datasources
            if (filtroVO.Seq == 0)
                filtroVO.Seq = null;

            var filtro = filtroVO.Transform<ComponenteCurricularFilterSpecification>();
            if (filtroVO.SeqAluno.HasValue)
            {
                var specAluno = new SMCSeqSpecification<Aluno>(filtroVO.SeqAluno.Value);
                var seqMatrizAluno = AlunoDomainService.SearchProjectionByKey(specAluno,
                    p => p.Historicos.Where(w => w.Atual).FirstOrDefault()
                          .HistoricosCicloLetivo.FirstOrDefault()
                          .PlanosEstudo.FirstOrDefault()
                          .MatrizCurricularOferta.SeqMatrizCurricular);

                filtro.SeqMatrizCurricular = seqMatrizAluno;
            }

            filtro.SetOrderBy(o => o.Descricao);
            return SearchProjectionBySpecification(filtro, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }).ToList();
        }

        /// <summary>
        /// Busca um componente curricular sem suas dependências
        /// </summary>
        /// <param name="seq">Sequencial do componente curricular</param>
        /// <returns>Dados do componente curricular</returns>
        public ComponenteCurricular BuscarComponenteCurricularSemDependencias(long seq)
        {
            return SearchByKey(new SMCSeqSpecification<ComponenteCurricular>(seq));
        }

        /// <summary>
        /// Buscar os Assuntos de Componente Curricular, a busca pode ser feita pelo GrupoComponenteCurricular ou direto pelo ComponenteCurricular
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo componente curricular</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno</param>
        /// <param name="ignorarFormadosDispensados">Ignora os itens que já formados ou dispensados pelo aluno</param>
        /// <returns>Lista com os Assuntos de Componentes Curriculares</returns>
        public List<SMCDatasourceItem> BuscarAssuntosComponenteCurricularSelect(long? seqGrupoCurricularComponente, long? seqComponenteCurricular, long seqCicloLetivo, long seqPessoaAtuacao, bool ignorarFormadosDispensados)
        {
            var lista = new List<SMCDatasourceItem>();
            var result = new List<ComponenteCurricularDetalheVO>();

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);
            if (seqGrupoCurricularComponente.GetValueOrDefault() > 0)
            {
                var specGrupoCurricularComponente = new SMCSeqSpecification<GrupoCurricularComponente>(seqGrupoCurricularComponente.Value);

                result = this.GrupoCurricularComponenteDomainService.SearchProjectionByKey(specGrupoCurricularComponente, c =>
                    c.ComponenteCurricular
                     .Configuracoes
                     .SelectMany(w => w.DivisoesMatrizCurricularComponente.Where(d => d.SeqMatrizCurricular == dadosOrigem.SeqMatrizCurricular))
                     .SelectMany(q => q.ComponentesCurricularSubstitutos)
                     .Select(w => new ComponenteCurricularDetalheVO
                     {
                         Seq = w.Seq,
                         Codigo = w.Codigo,
                         Descricao = w.Descricao,
                         Credito = w.Credito,
                         CargaHoraria = w.CargaHoraria,
                         FormatoCargaHoraria = w.TipoComponente.InstituicoesNivelTipoComponenteCurricular.FirstOrDefault(i => i.SeqTipoComponenteCurricular == w.SeqTipoComponenteCurricular).FormatoCargaHoraria
                     })).ToList();
            }

            if (seqComponenteCurricular.GetValueOrDefault() > 0)
            {
                var specComponenteCurricular = new SMCSeqSpecification<ComponenteCurricular>(seqComponenteCurricular.Value);

                result = this.SearchProjectionByKey(specComponenteCurricular, c =>
                    c.Configuracoes
                     .SelectMany(w => w.DivisoesMatrizCurricularComponente.Where(d => d.SeqMatrizCurricular == dadosOrigem.SeqMatrizCurricular))
                     .SelectMany(q => q.ComponentesCurricularSubstitutos)
                     .Select(w => new ComponenteCurricularDetalheVO
                     {
                         Seq = w.Seq,
                         Codigo = w.Codigo,
                         Descricao = w.Descricao,
                         Credito = w.Credito,
                         CargaHoraria = w.CargaHoraria,
                         FormatoCargaHoraria = w.TipoComponente.InstituicoesNivelTipoComponenteCurricular.FirstOrDefault(i => i.SeqTipoComponenteCurricular == w.SeqTipoComponenteCurricular).FormatoCargaHoraria
                     })).ToList();
            }

            //Recupera os dados do histórico escolar de acordo com a procedure ACADEMICO.APR.st_rel_componentes_historico_escolar
            List<long> seqsComponentesAprovados = null;

            if (ignorarFormadosDispensados)
            {
                var dadosHistoricoEscolar = HistoricoEscolarDomainService.ComponentesCriteriosHistoricoEscolar(dadosOrigem.SeqAlunoHistoricoAtual, false);
                seqsComponentesAprovados = dadosHistoricoEscolar.Where(w => w.SeqComponenteCurricularAssunto.HasValue).Select(s => s.SeqComponenteCurricularAssunto.Value).ToList();
            }

            foreach (var item in result)
            {
                // Remove os já formados ou dispensados pelo aluno
                if (seqsComponentesAprovados != null && seqsComponentesAprovados.Contains(item.Seq))
                    continue;

                lista.Add(new SMCDatasourceItem()
                {
                    Seq = item.Seq,
                    Descricao = GerarDescricaoComponenteCurricular(item.Codigo, item.Descricao, item.Credito, item.CargaHoraria, item.FormatoCargaHoraria)
                });
            }

            return lista.SMCDistinct(x => x.Seq).ToList();
        }

        /// <summary>
        /// Valida se o componente curricular foi parâmetrizado para exigir assunto, a validação pode ser feita pelo GrupoComponenteCurricular ou direto pelo ComponenteCurricular
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo componente curricular</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>True para exigir assunto</returns>
        public bool ValidarComponenteCurricularExigeAssunto(long? seqGrupoCurricularComponente, long? seqComponenteCurricular)
        {
            if (seqGrupoCurricularComponente.GetValueOrDefault() > 0)
            {
                var specGrupoCurricularComponente = new SMCSeqSpecification<GrupoCurricularComponente>(seqGrupoCurricularComponente.Value);

                return this.GrupoCurricularComponenteDomainService.SearchProjectionByKey(specGrupoCurricularComponente, c => c.ComponenteCurricular.ExigeAssuntoComponente).GetValueOrDefault();
            }

            if (seqComponenteCurricular.GetValueOrDefault() > 0)
            {
                var specComponenteCurricular = new SMCSeqSpecification<ComponenteCurricular>(seqComponenteCurricular.Value);

                return this.SearchProjectionByKey(specComponenteCurricular, c => c.ExigeAssuntoComponente).GetValueOrDefault();
            }

            return false;
        }

        /// <summary>
        /// Buscar os componentes curricular que atendam os filtros informados no lookup de dispensa baseado no histórico escolar do aluno
        /// </summary>
        /// <param name="filtroVO">Filtros da listagem de componentes curricular dispensa</param>
        /// <returns>SMCPagerData de componentes curricular do historico escolar</returns>
        public SMCPagerData<ComponenteCurricularListaVO> BuscarComponentesCurricularesDispensaLookup(ComponenteCurricularDispensaFiltroVO filtroVO)
        {
            try
            {
                // Desabilita os filtros de dados
                AlunoDomainService.DisableFilter(FILTER.NIVEL_ENSINO);
                AlunoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                AlunoHistoricoDomainService.DisableFilter(FILTER.NIVEL_ENSINO);
                AlunoHistoricoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

                // Busca a origem do aluno
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(filtroVO.SeqPessoaAtuacao.Value);

                // Busca as formações específicas do aluno
                var dadosFormacoesPessoaAtuacao = AlunoFormacaoDomainService.BuscarSequenciaisFormacoesAlunoHistorico(dadosOrigem.SeqAlunoHistoricoAtual);
                List<long> seqsDadosFormacoesPessoaAtuacao = new List<long>();
                if (dadosFormacoesPessoaAtuacao.Count > 0)
                    seqsDadosFormacoesPessoaAtuacao = FormacaoEspecificaDomainService.BuscarInformacaoTreeFormacaoEspecificaSequenciais(dadosFormacoesPessoaAtuacao, dadosOrigem.SeqCurso);

                // Busca os componentes da matriz do aluno
                var filtroMatrizOferta = new MatrizCurricularOfertaIntegralizacaoFiltroVO()
                {
                    Seq = dadosOrigem.SeqMatrizCurricularOferta,
                    DesconsiderarFiltroBeneficios = true,
                    DesconsiderarFiltroCondicoes = true,
                    SeqsFormacoes = seqsDadosFormacoesPessoaAtuacao
                };
                var componentesMatrizAluno = MatrizCurricularOfertaDomainService.BuscarIntegralizacaoConfiguracoesMatrizCurricularOferta(filtroMatrizOferta);

                // Recuperar todos registros de pessoa atuação tipo aluno da pessoa correspondente a pessoa atuação informada
                List<long> seqsPessoaAtuacao = AlunoDomainService.BuscarSequenciaisPessoaAtuacaoAlunoPessoa(filtroVO.SeqPessoaAtuacao.Value);

                // Recuperar todos os registro de histótico aluno independente se é atual
                AlunoHistoricoFilterSpecification spec = new AlunoHistoricoFilterSpecification() { SeqsPessoaAtuacao = seqsPessoaAtuacao };

                if (filtroVO.DisciplinaIsolada.HasValue)
                {
                    if (filtroVO.DisciplinaIsolada.Value)
                    {
                        // Se for disciplina isolada listar aluno histórico com vínculo "Regime de disciplina isolada".
                        spec.TipoVinculoAlunoFinanceiro = TipoVinculoAlunoFinanceiro.RegimeDisciplinaIsolada;
                    }
                    else
                    {
                        // Se for curso regular listar de acordo com a oferta do curso
                        spec.SeqCursoOferta = filtroVO.SeqOfertaCurso;
                    }
                }

                // Recuperar todos os aluno-históricos
                List<long> historicosAlunos = AlunoHistoricoDomainService.SearchBySpecification(spec).Select(s => s.Seq).ToList();
                if (historicosAlunos.Count == 0)
                    return new SMCPagerData<ComponenteCurricularListaVO>();

                // Recupera os históricos escolares aprovados
                HistoricoEscolarFilterSpecification filtro = new HistoricoEscolarFilterSpecification()
                {
                    Seqs = filtroVO.Seqs,
                    SeqsAlunoHistorico = historicosAlunos,
                    SituacaoHistoricoEscolar = SituacaoHistoricoEscolar.Aprovado,
                };
                filtro.SetPageSetting(filtroVO.PageSettings);
                filtro.SetOrderBy("CicloLetivo.AnoNumeroCicloLetivo");
                filtro.SetOrderBy("ComponenteCurricular.Descricao");
                filtro.MaxResults = int.MaxValue;

                //BUG 27875 - Conversado com Luciana não validar se o curriculo possui os componente do histórico
                //var curriculoAluno = AlunoHistoricoDomainService.SearchProjectionBySpecification(spec, p => p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Curriculos.Select(s => s.Seq)).SelectMany(s => s).ToList();
                //if (curriculoAluno != null && curriculoAluno.Count > 0)
                //    filtro.SeqsCurriculo = curriculoAluno;

                int total = 0;
                var componentes = HistoricoEscolarDomainService.SearchProjectionBySpecification(filtro,
                                    x => new ComponenteCurricularListaVO
                                    {
                                        Seq = x.Seq,
                                        SeqComponenteCurricular = x.SeqComponenteCurricular,
                                        SeqComponenteCurricularAssunto = x.SeqComponenteCurricularAssunto,
                                        SeqPessoaAtuacao = x.AlunoHistorico.SeqAluno,
                                        DescricaoOfertaCurso = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                                        DescricaoCicloLetivo = x.CicloLetivo.Descricao,
                                        Codigo = x.ComponenteCurricular.Codigo,
                                        Descricao = x.ComponenteCurricular.Descricao,
                                        CargaHoraria = x.ComponenteCurricular.CargaHoraria,
                                        Credito = x.ComponenteCurricular.Credito,
                                        SeqTipoComponenteCurricular = x.ComponenteCurricular.SeqTipoComponenteCurricular,
                                        NiveisEnsino = x.ComponenteCurricular.NiveisEnsino.Select(s => new ComponenteCurricularNivelEnsinoVO
                                        {
                                            Seq = s.Seq,
                                            SeqComponenteCurricular = s.SeqComponenteCurricular,
                                            SeqNivelEnsino = s.SeqNivelEnsino,
                                            Responsavel = s.Responsavel,
                                        }).ToList(),
                                        DescricaoAssuntoCompleta = x.ComponenteCurricularAssunto.Descricao,
                                        DisciplinaIsolada = x.AlunoHistorico.Aluno.TipoVinculoAluno.TipoVinculoAlunoFinanceiro == TipoVinculoAlunoFinanceiro.RegimeDisciplinaIsolada
                                    }, out total);

                // Recupera todos os formatos para tipos de componente por nível para evitar a consulta item a item que era realizada.
                var formatosCargaHoraria = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionAll(p => new
                {
                    p.InstituicaoNivel.SeqNivelEnsino,
                    p.SeqTipoComponenteCurricular,
                    p.FormatoCargaHoraria
                }).ToList();

                // Task 37778 - Retirar do lookup os componentes que estejam associados à uma solicitação de dispensa com situação deferida
                // Recupera os templates de processo das solicitações de dispensa do aluno
                var seqsTemplatesProcessos = SolicitacaoDispensaDomainService.SearchProjectionBySpecification(new SolicitacaoDispensaFilterSpecification
                {
                    //SeqsPessoaAtuacao = seqsPessoaAtuacao
                    SeqPessoaAtuacao = filtroVO.SeqPessoaAtuacao
                }, x => x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf).Distinct().ToList();

                // Recupera os seqs das situações deferidas
                List<long> seqsSituacoesDeferidas = new List<long>();
                seqsTemplatesProcessos.SMCForEach(seqTemplateProcessoSgf =>
                {
                    // Busca as situações do template de processo de dispensa
                    var etapas = SGFHelper.BuscarEtapasSGFCache(seqTemplateProcessoSgf);
                    var seqSituacaoEtapaDeferida = etapas.OrderByDescending(x => x.Ordem).FirstOrDefault().Situacoes.FirstOrDefault(x => x.SituacaoFinalProcesso && x.SituacaoFinalEtapa && x.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso).Seq;
                    seqsSituacoesDeferidas.Add(seqSituacaoEtapaDeferida);
                });

                // Busca os itens deferidos
                var dadosSolicitacoesDispensa = SolicitacaoDispensaDomainService.SearchProjectionBySpecification(new SolicitacaoDispensaFilterSpecification
                {
                    //SeqsPessoaAtuacao = seqsPessoaAtuacao,
                    SeqPessoaAtuacao = filtroVO.SeqPessoaAtuacao,
                    SeqsSituacaoEtapaAtual = seqsSituacoesDeferidas
                }, x => new
                {
                    SeqSolicitacaoDispensa = x.Seq,
                    ComponentesDispensados = x.OrigensInternas.Where(d => d.HistoricoEscolar.SeqComponenteCurricular.HasValue).Select(d => new
                    {
                        SeqComponenteCurricular = d.HistoricoEscolar.SeqComponenteCurricular.Value,
                        SeqComponenteCurricularAssunto = d.HistoricoEscolar.SeqComponenteCurricularAssunto
                    }).ToList()
                }).ToList();
                var componentesDispensados = dadosSolicitacoesDispensa.SelectMany(d => d.ComponentesDispensados).ToList();

                var comp = componentes.ToList();
                List<ComponenteCurricularListaVO> listResult = new List<ComponenteCurricularListaVO>();
                foreach (var item in comp)
                {
                    // Não adiciona itens já dispensados em outras solicitações
                    var itemJaDispensado = componentesDispensados.Any(c => c.SeqComponenteCurricular == item.SeqComponenteCurricular && c.SeqComponenteCurricularAssunto == item.SeqComponenteCurricularAssunto);
                    if (itemJaDispensado)
                        continue;

                    if ((item.SeqPessoaAtuacao == filtroVO.SeqPessoaAtuacao &&
                        // Não faz parte da matriz do aluno OU faz parte da matriz do aluno mas Permite ser origem de dispensa no mesmo currículo = SIM
                        (item.DisciplinaIsolada ||
                         !componentesMatrizAluno.Any(c => c.SeqComponenteCurricular == item.SeqComponenteCurricular) ||
                          componentesMatrizAluno.Any(c => c.SeqComponenteCurricular == item.SeqComponenteCurricular && c.PermiteOrigemDispensaMesmoCurriculo)))
                        || item.SeqPessoaAtuacao != filtroVO.SeqPessoaAtuacao)
                    {
                        item.SeqNivelEnsino = item.NiveisEnsino.Where(s => s.Responsavel == true).FirstOrDefault()?.SeqNivelEnsino ?? 0;

                        item.FormatoCargaHoraria = formatosCargaHoraria
                            .FirstOrDefault(f => f.SeqNivelEnsino == item.SeqNivelEnsino
                                              && f.SeqTipoComponenteCurricular == item.SeqTipoComponenteCurricular)
                            ?.FormatoCargaHoraria;

                        item.DescricaoCompleta = GerarDescricaoComponenteCurricular(
                            item.Codigo,
                            item.Descricao,
                            item.Credito,
                            item.CargaHoraria,
                            item.FormatoCargaHoraria);

                        if (!string.IsNullOrEmpty(item.DescricaoAssuntoCompleta))
                            item.DescricaoCompleta += $" ({item.DescricaoAssuntoCompleta})";

                        listResult.Add(item);
                    }
                }
                total = listResult.Count;
                return new SMCPagerData<ComponenteCurricularListaVO>(listResult, total);
            }
            finally
            {
                AlunoDomainService.EnableFilter(FILTER.NIVEL_ENSINO);
                AlunoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                AlunoHistoricoDomainService.EnableFilter(FILTER.NIVEL_ENSINO);
                AlunoHistoricoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }
        }

        /// <summary>
        /// Buscar os componentes curricular que atendam os filtros informados no lookup de dispensa baseado no histórico escolar do aluno
        /// </summary>
        /// <param name="filtroVO">Filtros da listagem de componentes curricular dispensa</param>
        /// <returns>SMCPagerData de componentes curricular do historico escolar</returns>
        public List<ComponenteCurricularListaVO> BuscarComponentesCurricularesDispensa(long[] seqs)
        {
            var filtro = new SMCContainsSpecification<HistoricoEscolar, long>(a => a.Seq, seqs);
            filtro.SetOrderBy("CicloLetivo.AnoNumeroCicloLetivo");
            filtro.SetOrderBy("ComponenteCurricular.Descricao");

            int total = 0;
            var componentes = HistoricoEscolarDomainService.SearchProjectionBySpecification(filtro,
                                x => new ComponenteCurricularListaVO
                                {
                                    Seq = x.Seq,
                                    DescricaoOfertaCurso = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                                    DescricaoCicloLetivo = x.CicloLetivo.Descricao,
                                    Codigo = x.ComponenteCurricular.Codigo,
                                    Descricao = x.ComponenteCurricular.Descricao,
                                    CargaHoraria = x.ComponenteCurricular.CargaHoraria,
                                    Credito = x.ComponenteCurricular.Credito,
                                    SeqTipoComponenteCurricular = x.ComponenteCurricular.SeqTipoComponenteCurricular,
                                    NiveisEnsino = x.ComponenteCurricular.NiveisEnsino.Select(s => new ComponenteCurricularNivelEnsinoVO
                                    {
                                        Seq = s.Seq,
                                        SeqComponenteCurricular = s.SeqComponenteCurricular,
                                        SeqNivelEnsino = s.SeqNivelEnsino,
                                        Responsavel = s.Responsavel,
                                    }).ToList(),
                                    DescricaoAssuntoCompleta = x.ComponenteCurricularAssunto.Descricao,
                                }, out total);

            // Recupera todos os formatos para tipos de componente por nível para evitar a consulta item a item que era realizada.
            var formatosCargaHoraria = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionAll(p => new
            {
                p.InstituicaoNivel.SeqNivelEnsino,
                p.SeqTipoComponenteCurricular,
                p.FormatoCargaHoraria
            }).ToList();

            List<ComponenteCurricularListaVO> listResult = new List<ComponenteCurricularListaVO>();
            foreach (var item in componentes.ToList())
            {
                item.SeqNivelEnsino = item.NiveisEnsino.Where(s => s.Responsavel == true).FirstOrDefault()?.SeqNivelEnsino ?? 0;

                item.FormatoCargaHoraria = formatosCargaHoraria
                    .FirstOrDefault(f => f.SeqNivelEnsino == item.SeqNivelEnsino
                                      && f.SeqTipoComponenteCurricular == item.SeqTipoComponenteCurricular)
                    ?.FormatoCargaHoraria;

                item.DescricaoCompleta = GerarDescricaoComponenteCurricular(
                    item.Codigo,
                    item.Descricao,
                    item.Credito,
                    item.CargaHoraria,
                    item.FormatoCargaHoraria);

                if (!string.IsNullOrEmpty(item.DescricaoAssuntoCompleta))
                    item.DescricaoCompleta += $" ({item.DescricaoAssuntoCompleta})";

                listResult.Add(item);
            }

            return listResult;
        }

        /// <summary>
        /// Calcular os valores totais dos componentes informados
        /// </summary>
        /// <param name="seqsComponentesCurriculares">Lista de sequenciais dos componentes curriculares</param>
        /// <returns>Objeto com o total de hora, hora-aula e créditos</returns>
        public TotalHoraCreditoVO CalculaHoraCreditoComponenteCurricular(long seqInstituicaoEnsino, List<long> seqsComponentesCurriculares)
        {
            float totalHoras = 0;
            float totalHorasAula = 0;
            float totalCreditos = 0;
            float totalCreditosPorHora = 0;
            float totalHorasPorCredito = 0;

            // Quantidade de carga horária para cálculo caso não encontre no componente curricular específico
            // Quando não encontrar componente curricular com parametrização, deve usar a parametrização do tipo Disciplina (Regra da Janice)
            var quantidadeHorasPorCreditoDisciplina = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarQuantidadeHorasPorCreditoDisciplina(seqInstituicaoEnsino);

            seqsComponentesCurriculares = seqsComponentesCurriculares ?? new List<long>();

            var componentesGrupoAssunto = seqsComponentesCurriculares.GroupBy(g => g).ToList();

            var specComponenteCurricular = new SMCContainsSpecification<ComponenteCurricular, long>(g => g.Seq, seqsComponentesCurriculares.ToArray());

            var componentesCurriculares = this.SearchProjectionBySpecification(specComponenteCurricular, g => new
            {
                g.Seq,
                g.CargaHoraria,
                g.Credito,
                g.TipoComponente.InstituicoesNivelTipoComponenteCurricular.FirstOrDefault(i => i.SeqTipoComponenteCurricular == g.SeqTipoComponenteCurricular).FormatoCargaHoraria,
                g.TipoComponente.InstituicoesNivelTipoComponenteCurricular.FirstOrDefault(i => i.SeqTipoComponenteCurricular == g.SeqTipoComponenteCurricular).QuantidadeHorasPorCredito,
            }).ToList();

            foreach (var componenteCurricular in componentesCurriculares)
            {
                //Agrupamento necessario para corrigir o calculo quando são selecionados dois componentes iguais com assuntos diferentes
                float contador = componentesGrupoAssunto.First(f => f.Key == componenteCurricular.Seq).Count();

                totalCreditos += (componenteCurricular.Credito.GetValueOrDefault() * contador);

                switch (componenteCurricular.FormatoCargaHoraria)
                {
                    case FormatoCargaHoraria.Hora:
                        totalHorasAula += ((componenteCurricular.CargaHoraria.GetValueOrDefault() * 60F) / 50F) * contador;
                        totalHoras += (componenteCurricular.CargaHoraria.GetValueOrDefault() * contador);
                        break;

                    case FormatoCargaHoraria.HoraAula:
                        totalHorasAula += (componenteCurricular.CargaHoraria.GetValueOrDefault() * contador);
                        totalHoras += ((componenteCurricular.CargaHoraria.GetValueOrDefault() * 50F) / 60F) * contador;
                        break;
                }

                // Atribui o valor específico do componente ou o do tipo disciplina (regra da Janice)
                var quantidadeHorasCreditoConsiderar = componenteCurricular.QuantidadeHorasPorCredito ?? quantidadeHorasPorCreditoDisciplina;

                if (quantidadeHorasCreditoConsiderar.HasValue && quantidadeHorasCreditoConsiderar.Value > 0)
                {
                    totalCreditosPorHora += (componenteCurricular.CargaHoraria.GetValueOrDefault() / quantidadeHorasCreditoConsiderar.Value);
                    totalHorasPorCredito += (componenteCurricular.Credito.GetValueOrDefault() * quantidadeHorasCreditoConsiderar.Value);
                }

                //totalHoras += (componenteCurricular.CargaHoraria.GetValueOrDefault() * contador);
            }

            return new TotalHoraCreditoVO() { TotalHoras = Convert.ToDecimal(totalHoras), TotalHorasAula = Convert.ToDecimal(totalHorasAula), TotalCreditos = Convert.ToDecimal(totalCreditos), TotalCreditosPorHora = Convert.ToDecimal(totalCreditosPorHora), TotalHorasPorCredito = Convert.ToDecimal(totalHorasPorCredito) };
        }

        /// <summary>
        /// Buscar os componentes curricular somente com os tipos de componente para consulta
        /// </summary>
        /// <param name="seqs">Array de sequencial de componente curricular</param>
        /// <returns>Lista de componente com tipo para integralização</returns>
        public List<IntegralizacaoTipoComponenteCurricularVO> BuscarComponentesCurricularesComTipoComponenteIntegralizacao(long[] seqs)
        {
            var filtro = new ComponenteCurricularFilterSpecification() { SeqComponentesCurriculares = seqs };
            filtro.MaxResults = Int32.MaxValue;

            var componentes = this.SearchProjectionBySpecification(filtro,
                                x => new IntegralizacaoTipoComponenteCurricularVO
                                {
                                    SeqComponenteCurricular = x.Seq,
                                    SeqTipoComponenteCurricular = x.SeqTipoComponenteCurricular,
                                    Descricao = x.TipoComponente.Descricao,
                                    Sigla = x.TipoComponente.Sigla
                                }).ToList();

            return componentes;
        }

        public string BuscarDescricaoComponenteCurricularAssunto(long? seqComponenteCurricularAssunto)
        {
            if (seqComponenteCurricularAssunto.HasValue)
            {
                return this.SearchProjectionByKey(seqComponenteCurricularAssunto.Value,
                        x => x.Descricao.Trim()
                    );
            }
            return string.Empty;
        }

        public string BuscarDescricaoSimplesComponenteCurricular(long seqComponenteCurricular, long? seqComponenteCurricularAssunto)
        {
            string descricaoComponenteCurricular = this.SearchProjectionByKey(seqComponenteCurricular, x => x.Descricao.Trim());
            
            if (seqComponenteCurricularAssunto.HasValue)
            {
                string descricaoComponenteAssunto =  this.SearchProjectionByKey(seqComponenteCurricularAssunto.Value, x => x.Descricao.Trim());
                descricaoComponenteCurricular = $"{descricaoComponenteCurricular} ({descricaoComponenteAssunto}) "; 
            }
            return descricaoComponenteCurricular;
        }

        public List<SMCDatasourceItem> BuscarOrganizacoesComponenteCurricularSelect(long seqComponenteCurricular)
        {
            var spec = new ComponenteCurricularOrganizacaoFilterSpecification() { Ativo = true, SeqComponenteCurricular = seqComponenteCurricular };

            var organizacoes = this.ComponenteCurricularOrganizacaoDomainService.SearchProjectionBySpecification(spec, o => new SMCDatasourceItem()
            {
                Seq = o.Seq,
                Descricao = o.Descricao
            }).ToList();

            return organizacoes;
        }

        public bool VerificarQuantidadeSemanasComponentePreenchida(long seqComponenteCurricular)
        {
            var quantidadeSemanasComponente = this.SearchProjectionByKey(seqComponenteCurricular, c => c.QuantidadeSemanas);

            return quantidadeSemanasComponente.HasValue && quantidadeSemanasComponente.Value > 0;
        }

        public List<SMCDatasourceItem> BuscarComponenteCurricularPorMatrizSelect(long seqMatrizCurricular)
        {
            //Recupera todos os grupos da matriz
            var specMatriz = new SMCSeqSpecification<MatrizCurricular>(seqMatrizCurricular);
            var consultaMatriz = this.MatrizCurricularDomainService.SearchProjectionByKey(specMatriz, p => new
            {
                SeqsGruposMatriz = p.CurriculoCursoOferta
                        .GruposCurriculares
                        .Select(s => s.GrupoCurricular.Seq),
            });

            //Spec para recuperar todos grupos curriculares da matriz
            SMCSpecification<GrupoCurricularComponente> specComponentes =
                new SMCContainsSpecification<GrupoCurricularComponente, long>(p => p.SeqGrupoCurricular, consultaMatriz.SeqsGruposMatriz.ToArray());

            //Recupera todos componentes que atendam aos filtros informados
            var componentesCurriculares = this.GrupoCurricularComponenteDomainService
             .SearchProjectionBySpecification(specComponentes, p => new ComponenteCurricularDetalheVO()
             {
                 Seq = p.ComponenteCurricular.Seq,
                 Codigo = p.ComponenteCurricular.Codigo,
                 Descricao = p.ComponenteCurricular.Descricao,
                 CargaHoraria = p.ComponenteCurricular.CargaHoraria,
                 Credito = p.ComponenteCurricular.Credito,
                 SeqNivelEnsino = p.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.NivelEnsino.Seq).FirstOrDefault(),
                 SeqTipoComponente = p.ComponenteCurricular.SeqTipoComponenteCurricular

             }).ToList();

            List<SMCDatasourceItem> listResult = new List<SMCDatasourceItem>();

            foreach (var item in componentesCurriculares.OrderBy(o => o.Descricao).ToList())
            {
                //Recupera os parâmetros de acordo com o nivel de ensino e tipo do componente
                var parametros = this.BuscarConfiguracaoComponenteCurricular(item.SeqNivelEnsino, item.SeqTipoComponente);
                item.FormatoCargaHoraria = parametros.FormatoCargaHoraria;

                string descricao = this.GerarDescricaoComponenteCurricular(item);

                listResult.Add(new SMCDatasourceItem() { Seq = item.Seq, Descricao = descricao });
            }

            var retorno = listResult.SMCDistinct(s => s.Seq).ToList();
            return retorno;
        }

    }
}