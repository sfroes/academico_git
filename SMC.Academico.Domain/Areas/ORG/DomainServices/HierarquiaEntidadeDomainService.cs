using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Validators;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Validation;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class HierarquiaEntidadeDomainService : AcademicoContextDomain<HierarquiaEntidade>
    {
        #region [ DomainService ]

        private EntidadeDomainService EntidadeDomainService
        {
            get { return this.Create<EntidadeDomainService>(); }
        }

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService
        {
            get { return this.Create<HierarquiaEntidadeItemDomainService>(); }
        }

        private TipoEntidadeDomainService TipoEntidadeDomainService
        {
            get { return this.Create<TipoEntidadeDomainService>(); }
        }

        private TipoHierarquiaEntidadeDomainService TipoHierarquiaEntidadeDomainService
        {
            get { return this.Create<TipoHierarquiaEntidadeDomainService>(); }
        }

        private TipoHierarquiaEntidadeItemDomainService TipoHierarquiaEntidadeItemDomainService
        {
            get { return this.Create<TipoHierarquiaEntidadeItemDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca as hierarquia de entidade  de acordo com o filtro
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de hierarquias de entidades</returns>
        public SMCPagerData<HierarquiaEntidade> BuscarHierarquiasEntidade(HierarquiaEntidadeFiltroVO filtros)
        {
            int total = 0;
            IEnumerable<HierarquiaEntidade> lista = null;

            // Monta o specification com os filtros informados
            SMCSpecification<HierarquiaEntidade> spec = filtros.Transform<HierarquiaEntidadeFilterSpecification>();
            var includes = IncludesHierarquiaEntidade.TipoHierarquiaEntidade;

            // Se é para busca somente as ativas, acrescenta o specification de vigente
            if (filtros.SomenteAtivas.HasValue && filtros.SomenteAtivas.Value)
            {
                HierarquiaEntidadeVigenteSpecification vigente = new HierarquiaEntidadeVigenteSpecification();
                spec = new SMCAndSpecification<HierarquiaEntidade>(spec, vigente);
            }

            try
            {
                if (filtros.PageSettings != null)
                {
                    List<SMCSortInfo> listaOrdenacao = filtros.PageSettings.SortInfo;

                    if (!listaOrdenacao.Any())
                    {
                        //ORDENAÇÃO DEFAULT PELO TIPO DE HIERARQUIA DE ENTIDADE, E DEPOIS A DESCRIÇÃO.
                        //COLOCANDO A ANOTAÇÃO SMCSORTABLE A ORDEM ESTÁ DESCRIÇÃO E DEPOIS TIPO, ENTÃO ORDENA ERRADO,
                        //E A LISTAGEM DEVE SER NESSA ORDEM
                        spec.SetOrderBy(o => o.TipoHierarquiaEntidade.Descricao)
                            .SetOrderBy(o => o.Descricao);
                    }
                }

                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES);
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);
                lista = this.SearchBySpecification(spec, out total, includes);
            }
            finally
            {
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES);
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);
            }

            return new SMCPagerData<HierarquiaEntidade>(lista, total);
        }

        /// <summary>
        /// Salva HierarquiaEntidade no banco de dados
        /// </summary>
        /// <returns>Sequencial HierarquiaEntidade</returns>
        public long SalvarHierarquiaEntidade(HierarquiaEntidade hierarquiaEntidade)
        {
            // Realiza a validação
            HierarquiaEntidadeValidator validator = new HierarquiaEntidadeValidator();
            var results = validator.Validate(hierarquiaEntidade);
            if (!results.IsValid)
            {
                List<SMCValidationResults> listaErros = new List<SMCValidationResults>();
                listaErros.Add(results);
                throw new SMCInvalidEntityException(listaErros);
            }

            // Busca o tipo de hierarquia
            var specTipo = new SMCSeqSpecification<TipoHierarquiaEntidade>(hierarquiaEntidade.SeqTipoHierarquiaEntidade);
            var tipoHierarquia = this.TipoHierarquiaEntidadeDomainService.SearchByKey(specTipo);

            // Se o tipo de hierarquia possui visão informada
            if (tipoHierarquia.TipoVisao.HasValue)
            {
                // Verifica duplicidade de hierarquias VIGENTES por tipo de visão
                HierarquiaEntidadeDuplicidadeTipoHierarquiaSpecification duplicidadeHierarquiaSpec = new HierarquiaEntidadeDuplicidadeTipoHierarquiaSpecification
                (
                     tipoHierarquia.TipoVisao.Value,
                     hierarquiaEntidade.DataInicioVigencia,
                     hierarquiaEntidade.DataFimVigencia
                );

                HierarquiaEntidade[] itensMesmaVisao = this.SearchBySpecification(duplicidadeHierarquiaSpec).ToArray();

                //Condição do Any é para o registro corrente não ser considerado na condição (caso seja uma edição)
                if (itensMesmaVisao.Any(h => h.Seq != hierarquiaEntidade.Seq))
                {
                    throw new HierarquiaEntidadeVisaoDuplicadaException(tipoHierarquia.TipoVisao.SMCGetDescription());
                }
            }

            //Garantia para que a data esteja com a hora zerada
            hierarquiaEntidade.DataInicioVigencia = hierarquiaEntidade.DataInicioVigencia.Date;
            if (hierarquiaEntidade.DataFimVigencia.HasValue)
                hierarquiaEntidade.DataFimVigencia = hierarquiaEntidade.DataFimVigencia.Value.Date;

            //Validações de alteração
            if (!hierarquiaEntidade.IsNew())
            {
                var hierarquiaEntidadeBanco = this.SearchProjectionByKey(new SMCSeqSpecification<HierarquiaEntidade>(hierarquiaEntidade.Seq), p => new
                {
                    p.SeqTipoHierarquiaEntidade,
                    ContemItens = p.Itens.Count > 0
                });

                if (hierarquiaEntidadeBanco.ContemItens && hierarquiaEntidade.SeqTipoHierarquiaEntidade != hierarquiaEntidadeBanco.SeqTipoHierarquiaEntidade)
                {
                    throw new HierarquiaEntidadeTipoAlteradoException();
                }
            }

            // Cadastrar a entidade
            this.SaveEntity(hierarquiaEntidade);
            return hierarquiaEntidade.Seq;
        }

        /// <summary>
        /// Busca as possíveis entidades superiores para um tipo de entidade em uma visão.
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo de entidade</param>
        /// <param name="visao">Visão para ser avaliada</param>
        /// <returns>Lista de entidades</returns>
        public List<SMCDatasourceItem> BuscarEntidadeSuperiorSelect(long seqTipoEntidade, TipoVisao visao, bool apenasAtivos = false, bool usarNomeReduzido = false, bool usarSeqEntidade = false, bool RemoveEntidadePai = false, bool considerarGrupoPrograma = false)
        {
            try
            {
                HierarquiaEntidadeItemDomainService.DesativarFiltrosHierarquiaItem();
                HierarquiaEntidadeItemDomainService.AtivarFiltroHierarquiaItem(visao);

                // Busca a hierarquia na visão vigente da instituição de ensino
                var hierarquia = this.BuscarHierarquiaVigente(visao);

                // Se não encontrou a hierarquia, erro
                if (hierarquia == null)
                {
                    throw new HierarquiaEntidadeNaoConfiguradaException(visao.SMCGetDescription());
                }

                // Verifica no tipo da hierarquia, os itens que podem ser pai do tipo de entidade
                var specTipo = new TipoHierarquiaEntidadeItemFilterSpecification()
                {
                    SeqTipoHierarquiaEntidade = hierarquia.SeqTipoHierarquiaEntidade,
                    SeqTipoEntidade = seqTipoEntidade
                };

                var itens = TipoHierarquiaEntidadeItemDomainService.SearchBySpecification(specTipo).ToList();
                List<long> seqItensSuperior = itens.Where(i => i.SeqItemSuperior.HasValue).Select(i => i.SeqItemSuperior.Value).ToList();

                if (seqItensSuperior.SMCCount() <= 0)
                {
                    throw new TipoHierarquiaEntidadeNaoConfigurouTipoException(visao.SMCGetDescription());
                }

                // Busca nos itens da hierarquia aqueles que podem ser pai do tipo de entidade
                var spec = new HierarquiaEntidadeItemFilterSpecification()
                {
                    SeqHierarquiaEntidade = hierarquia.Seq,
                    SeqsTipoHierarquiaEntidadeItem = seqItensSuperior
                };
                List<HierarquiaEntidadeItem> entidades;

                if (usarNomeReduzido)
                {
                    entidades = HierarquiaEntidadeItemDomainService.SearchBySpecification(spec,
                                        IncludesHierarquiaEntidadeItem.Entidade_HistoricoSituacoes_SituacaoEntidade |
                                        IncludesHierarquiaEntidadeItem.Entidade_TipoEntidade |
                                        IncludesHierarquiaEntidadeItem.ItemSuperior
                                    ).ToList();
                }
                else
                {
                    var teste = HierarquiaEntidadeItemDomainService.SearchProjectionBySpecification(spec, x => x.SeqEntidade).ToList();

                    entidades = HierarquiaEntidadeItemDomainService.SearchBySpecification(spec,
                                        IncludesHierarquiaEntidadeItem.Entidade_HistoricoSituacoes_SituacaoEntidade |
                                        IncludesHierarquiaEntidadeItem.Entidade_TipoEntidade |
                                        IncludesHierarquiaEntidadeItem.ItemSuperior
                                    ).ToList();
                }

                if (RemoveEntidadePai)//regra NV05 e NV10 do documento UC_CSO_001_02 - Curso Unidade
                {
                    var entidadePai = entidades.Where(w => w.SeqItemSuperior != null).Select(s => s.SeqItemSuperior).Distinct().ToList();
                    entidades.RemoveAll(r => entidadePai.Any(a => a == r.Seq));
                }

                // Monta a lista de retorno
                List<SMCDatasourceItem> lista = new List<SMCDatasourceItem>();
                foreach (var ent in entidades)
                {
                    //Verifica se é para considerar o grupo de programa ao invés do programa. 
                    //Os programas são divididos em Academico e Profissional, e tem locais como no aluno, que é associado 
                    //o grupo de programa, e não o programa
                    //Exemplo: Programa em Pós Graduação em Direito, que é geral, ao invés de associar se é acadêmico ou profissional
                    //Ajuste realizado para a task 43843, pois do jeito que estava, iria listar programas e departamentos
                    //como entidades responsáveis, mas os alunos estão vinculados em grupos de programa
                    if (considerarGrupoPrograma && ent.Entidade.TipoEntidade.Token == TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA)
                    {
                        // Transforma o Programa em Grupo de programa
                        var specEntidade = new EntidadeFilterSpecification() { Seq = ent.ItemSuperior.SeqEntidade };
                        var entGrupo = EntidadeDomainService.SearchByKey(specEntidade,
                                                                         IncludesEntidade.HistoricoSituacoes_SituacaoEntidade |
                                                                         IncludesEntidade.TipoEntidade);
                        if (!apenasAtivos || (entGrupo.SituacaoAtual.CategoriaAtividade == CategoriaAtividade.Ativa ||
                                entGrupo.SituacaoAtual.CategoriaAtividade == CategoriaAtividade.EmAtivacao))
                        {
                            lista.Add(new SMCDatasourceItem(usarSeqEntidade ? entGrupo.Seq : ent.Seq, usarNomeReduzido ? entGrupo.NomeReduzido : entGrupo.Nome)
                            {
                                DataAttributes = new List<SMCKeyValuePair>()
                                {
                                    new SMCKeyValuePair() { Key = "tipoentidade", Value = entGrupo.TipoEntidade.Token }
                                }
                            });
                        }
                    }
                    else
                    {
                        if (!apenasAtivos || (ent.Entidade.SituacaoAtual.CategoriaAtividade == CategoriaAtividade.Ativa ||
                                ent.Entidade.SituacaoAtual.CategoriaAtividade == CategoriaAtividade.EmAtivacao))
                        {
                            lista.Add(new SMCDatasourceItem(usarSeqEntidade ? ent.SeqEntidade : ent.Seq, usarNomeReduzido ? ent.Entidade.NomeReduzido : ent.Entidade.Nome)
                            {
                                DataAttributes = new List<SMCKeyValuePair>()
                                {
                                    new SMCKeyValuePair() { Key = "tipoentidade", Value = ent.Entidade.TipoEntidade.Token }
                                }
                            });
                        }
                    }
                }

                if (lista.Count == 0)
                {
                    throw new TipoHierarquiaEntidadeSemArvoreException(visao.SMCGetDescription());
                }

                return lista.OrderBy(x => x.Descricao).ToList();
            }
            finally
            {
                HierarquiaEntidadeItemDomainService.AtivarFiltrosHierarquiaItem();
            }
        }

        /// <summary>
        /// Busa todas as entidades superiores às entidades informadas, ex:
        /// Faculdade Puc
        /// - Departamento ICEI
        /// -- Curso Computação
        /// Buscando por computação devolveria ICEI e Puc
        /// </summary>
        /// <param name="seqsEntiade">Sequenciais das entidades</param>
        /// <returns>Lista de entidades superiores à entidade informada</returns>
        public List<SMCDatasourceItem> BuscarEntidadesSuperioresSelect(List<long> seqsEntidade, TipoVisao visao)
        {
            try
            {
                HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);

                // Busca a hierarquia na visão vigente da instituição de ensino
                var hierarquia = this.BuscarHierarquiaVigente(visao);

                // Se não encontrou a hierarquia, erro
                if (hierarquia == null)
                {
                    throw new HierarquiaEntidadeNaoConfiguradaException(visao.SMCGetDescription());
                }

                var specHierarquiaItem = new HierarquiaEntidadeItemFilterSpecification() { SeqHierarquiaEntidade = hierarquia.Seq };
                // Recupera a hierarquia da visão solicitada
                var hierarquiaItem = HierarquiaEntidadeItemDomainService.SearchProjectionBySpecification(specHierarquiaItem, p => new
                {
                    p.Seq,
                    p.SeqEntidade,
                    p.Entidade.Nome,
                    SeqEntidadeSuperior = (long?)p.ItemSuperior.SeqEntidade
                }).ToList();

                // Recupera todos os itens superiores à entidade informada
                var itensSuperiores = new List<SMCDatasourceItem>();

                foreach (var seqEntidade in seqsEntidade)
                {
                    long? seqEntidadeAtual = seqEntidade;
                    do
                    {
                        var item = hierarquiaItem.FirstOrDefault(f => f.SeqEntidade == seqEntidadeAtual);
                        if (item != null)
                        {
                            if (item.SeqEntidade != seqEntidade)
                            {
                                itensSuperiores.Add(new SMCDatasourceItem(item.SeqEntidade, item.Nome));
                            }
                        }
                        seqEntidadeAtual = item?.SeqEntidadeSuperior;
                    } while (seqEntidadeAtual.HasValue);
                }

                return itensSuperiores.SMCDistinct(p => p.Seq).ToList();
            }
            finally
            {
                HierarquiaEntidadeItemDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                HierarquiaEntidadeItemDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);
            }
        }

        /// <summary>
        /// Busca a hierarquia de entidades vigente em uma determinada visão
        /// </summary>
        /// <param name="visao">Visão</param>
        /// <returns>Hierquia vigente</returns>
        public HierarquiaEntidade BuscarHierarquiaVigente(TipoVisao visao)
        {
            // Busca a hierarquia na visão vigente da instituição de ensino
            var specVisao = new HierarquiaEntidadeFilterSpecification() { Visao = visao };

            specVisao.SetOrderBy(p => p.TipoHierarquiaEntidade.Descricao)
                     .SetOrderBy(s => s.Descricao);

            var specVigente = new HierarquiaEntidadeVigenteSpecification();
            var specHierarquia = new SMCAndSpecification<HierarquiaEntidade>(specVisao, specVigente);
            return this.SearchByKey(specHierarquia);
        }

        /// <summary>
        /// Busca as entidades folhas para um tipo de entidade em uma visão.
        /// </summary>
        /// <param name="visao">Visão para ser avaliada</param>
        /// <param name="apenasAtivos">Apenas entidades ativas</param>
        /// <param name="usarSeqEntidade">Por padrão utiliza o Seq do item de hierarquia que representa a entidade. Quando setado devolve o seq da entidade</param>
        /// <returns>Lista de entidades</returns>
        public List<SMCDatasourceItem> BuscarEntidadesFolhaVisaoSelect(TipoVisao visao, string tokenEntidade, bool apenasAtivos = false, bool usarSeqEntidade = false)
        {
            // Busca a hierarquia na visão vigente da instituição de ensino
            var hierarquia = this.BuscarHierarquiaVigente(visao);

            // Se não encontrou a hierarquia, erro
            if (hierarquia == null)
                throw new HierarquiaEntidadeNaoConfiguradaException(visao.SMCGetDescription());

            TipoEntidade tipoEntidade = TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(tokenEntidade);

            // Verifica no tipo da hierarquia, os itens que podem ser pai do tipo de entidade
            var specTipo = new TipoHierarquiaEntidadeItemFilterSpecification()
            {
                SeqTipoHierarquiaEntidade = hierarquia.SeqTipoHierarquiaEntidade,
                SeqTipoEntidade = tipoEntidade.Seq
            };
            var itens = TipoHierarquiaEntidadeItemDomainService.SearchProjectionBySpecification(specTipo, x => x.Seq).ToList();

            if (itens.Count <= 0)
            {
                throw new TipoHierarquiaEntidadeNaoConfigurouTipoException(visao.SMCGetDescription());
            }

            // Busca nos itens da hierarquia aqueles que podem ser pai do tipo de entidade
            var specSeq = new HierarquiaEntidadeItemFilterSpecification() { SeqHierarquiaEntidade = hierarquia.Seq };
            var specItem = new SMCContainsSpecification<HierarquiaEntidadeItem, long>(h => h.SeqTipoHierarquiaEntidadeItem, itens.ToArray());
            var spec = new SMCAndSpecification<HierarquiaEntidadeItem>(specSeq, specItem);

            spec.SetOrderBy(x => x.Entidade.Nome);

            List<HierarquiaEntidadeItem> entidades = null;
            try
            {
                // Desconsidera os filtros de hierarquia que não sejam da visão atual
                HierarquiaEntidadeItemDomainService.DesativarFiltrosHierarquiaItem();
                HierarquiaEntidadeItemDomainService.AtivarFiltroHierarquiaItem(visao);
                entidades = HierarquiaEntidadeItemDomainService.SearchBySpecification(spec, IncludesHierarquiaEntidadeItem.Entidade_HistoricoSituacoes_SituacaoEntidade).ToList();
            }
            finally
            {
                HierarquiaEntidadeItemDomainService.AtivarFiltrosHierarquiaItem();
            }

            // Monta a lista de retorno
            List<SMCDatasourceItem> lista = new List<SMCDatasourceItem>();
            foreach (var ent in entidades)
            {
                if (!lista.Any(z => z.Seq == (usarSeqEntidade ? ent.SeqEntidade : ent.Seq)) && (!apenasAtivos || (ent.Entidade.SituacaoAtual.CategoriaAtividade == CategoriaAtividade.Ativa || ent.Entidade.SituacaoAtual.CategoriaAtividade == CategoriaAtividade.EmAtivacao)))
                    lista.Add(new SMCDatasourceItem(usarSeqEntidade ? ent.SeqEntidade : ent.Seq, ent.Entidade.Nome));
            }

            if (lista.Count == 0)
            {
                throw new TipoHierarquiaEntidadeSemArvoreException(visao.SMCGetDescription());
            }

            return lista?.OrderBy(o => o.Descricao).ToList();
        }

        public long BuscarEntidadeSuperior(long seqEntidadeCurso, TipoVisao visao)
        {
            try
            {
                HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);

                var hierarquia = this.BuscarHierarquiaVigente(visao);
                var specHierarquiaItem = new HierarquiaEntidadeItemFilterSpecification() { SeqHierarquiaEntidade = hierarquia.Seq };
                var hierarquiaItem = HierarquiaEntidadeItemDomainService.SearchProjectionBySpecification(specHierarquiaItem, p => new
                {
                    p.Seq,
                    p.SeqEntidade,
                    p.Entidade.Nome, 
                    SeqEntidadeSuperior = (long?)p.ItemSuperior.SeqEntidade,
                    p.TipoHierarquiaEntidadeItem.TipoEntidade.Descricao
                }).ToList();

                long seqEntidade = 0;
                long? seqEntidadeAtual = seqEntidadeCurso;
                do
                {
                    var item = hierarquiaItem.FirstOrDefault(f => f.SeqEntidade == seqEntidadeAtual);

                    seqEntidadeAtual = item?.SeqEntidadeSuperior;
                    seqEntidade = item.SeqEntidade;
                } while (seqEntidadeAtual.HasValue);

                return seqEntidade;
            }
            finally
            {
                HierarquiaEntidadeItemDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                HierarquiaEntidadeItemDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);
            }
        }
    }
}