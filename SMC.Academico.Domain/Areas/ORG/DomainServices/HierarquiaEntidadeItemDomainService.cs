using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class HierarquiaEntidadeItemDomainService : AcademicoContextDomain<HierarquiaEntidadeItem>
    {
        #region [ DomainService ]

        public HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService
        {
            get { return this.Create<HierarquiaEntidadeDomainService>(); }
        }

        public TipoHierarquiaEntidadeItemDomainService TipoHierarquiaEntidadeItemDomainService
        {
            get { return this.Create<TipoHierarquiaEntidadeItemDomainService>(); }
        }

        public TipoHierarquiaEntidadeDomainService TipoHierarquiaEntidadeDomainService
        {
            get { return this.Create<TipoHierarquiaEntidadeDomainService>(); }
        }

        public EntidadeDomainService EntidadeDomainService
        {
            get { return this.Create<EntidadeDomainService>(); }
        }

        public TipoEntidadeDomainService TipoEntidadeDomainService
        {
            get { return this.Create<TipoEntidadeDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Valida se um nó poderá ser inserido em um local específico da árvore de acordo com seu tipo
        /// </summary>
        /// <param name="HierarquiaEntidadeItem"></param>
        /// <returns></returns>
        protected void ValidarAssociacaoNode(HierarquiaEntidadeItem hierarquiaEntidadeItem)
        {
            // Buscar a entidade
            var includes = IncludesEntidade.HistoricoSituacoes | IncludesEntidade.HistoricoSituacoes_SituacaoEntidade | IncludesEntidade.TipoEntidade;
            Entidade entidade = EntidadeDomainService.SearchByKey<Entidade, Entidade>(hierarquiaEntidadeItem.SeqEntidade, includes);

            // Verifica se a entidade está ativa
            if (entidade.CategoriaAtividadeSituacaoAtual == CategoriaAtividade.Inativa)
                throw new HierarquiaEntidadeItemAssociacaoEntidadeInativaException();

            // Tipo a ser obedecido na hierarquia da árvore
            TipoHierarquiaEntidadeItem TipoHierarquiaEntidadeItem = TipoHierarquiaEntidadeItemDomainService.SearchByKey<TipoHierarquiaEntidadeItem, TipoHierarquiaEntidadeItem>(hierarquiaEntidadeItem.SeqTipoHierarquiaEntidadeItem);

            //*** Valida a associação de acordo com o tipo (mesmo a tela tratando no select - front-end)
            if (entidade.SeqTipoEntidade != TipoHierarquiaEntidadeItem.SeqTipoEntidade)
                throw new HierarquiaEntidadeItemAssociacaoTipoEntidadeIncompativelException();

            // HierarquiaEntidade no qual o item será vinculado
            HierarquiaEntidade HierarquiaEntidade = HierarquiaEntidadeDomainService.SearchByKey<HierarquiaEntidade, HierarquiaEntidade>(hierarquiaEntidadeItem.SeqHierarquiaEntidade);

            // Tipo que a HierarquiaEntidade deverá obedecer
            TipoHierarquiaEntidade TipoHierarquiaEntidade = TipoHierarquiaEntidadeDomainService.SearchByKey<TipoHierarquiaEntidade, TipoHierarquiaEntidade>(HierarquiaEntidade.SeqTipoHierarquiaEntidade);

            //*** Valida a questão da externação
            // Se o tipo da HierarquiaEntidade estiver associado a uma visão e a TipoEntidade for externado o node não poderá ser inserido
            if (TipoHierarquiaEntidade.TipoVisao.HasValue && entidade.TipoEntidade.EntidadeExternada)
                throw new HierarquiaEntidadeItemAssociacaoEntidadeExternadaException();
        }

        /// <summary>
        /// Salva as informações da hierarquia entidade item
        /// </summary>
        /// <param name="HierarquiaEntidadeItem">Hierarquia entidade item a ser salvo</param>
        /// <returns>Sequencial da hierarquia entidade item salvo</returns>
        public long SalvarHierarquiaEntidadeItem(HierarquiaEntidadeItem HierarquiaEntidadeItem)
        {
            // Valida a associação do nó
            ValidarAssociacaoNode(HierarquiaEntidadeItem);

            // Save
            this.SaveEntity(HierarquiaEntidadeItem);
            return HierarquiaEntidadeItem.Seq;
        }

        /// <summary>
        /// Monta o select a partir da seqHierarquiaEntidade e seqTipoHierarquiaEntidadeItem informados
        /// </summary>
        /// <param name="seqHierarquiaEntidade">Sequencial da hierarquia de entidade</param>
        /// <param name="seqTipoHierarquiaEntidadeItem">Quando informado, o mesmo será considerado pai</param>
        /// <returns>Lista de tipo hierarquia entidade item para o combo</returns>
        public List<SMCDatasourceItem> BuscarTipoHierarquiaEntidadeItemSelect(long seqHierarquiaEntidade, long? seqTipoHierarquiaEntidadeItem)
        {
            // Retorno
            List<SMCDatasourceItem> listaSelectItemRetorno = new List<SMCDatasourceItem>();

            BuscarTipoHierarquiaEntidadeItem(seqHierarquiaEntidade, seqTipoHierarquiaEntidadeItem)
                .ToList()
                .ForEach(item =>
                {
                    listaSelectItemRetorno.Add(new SMCDatasourceItem(item.Seq, item.TipoEntidade.Descricao));
                });

            return listaSelectItemRetorno;
        }

        /// <summary>
        /// Monta um Enumerable a partir da seqHierarquiaEntidade e seqTipoHierarquiaEntidadeItem informados
        /// </summary>
        /// <param name="seqHierarquiaEntidade">Sequencial da hierarquia de entidade</param>
        /// <param name="seqTipoHierarquiaEntidadeItem">Quando informado, o mesmo será considerado pai</param>
        /// <returns>Enumerable com os resultados do tipo de hierarquia entidade item</returns>
        public IEnumerable<TipoHierarquiaEntidadeItem> BuscarTipoHierarquiaEntidadeItem(long seqHierarquiaEntidade, long? seqTipoHierarquiaEntidadeItem)
        {
            // Busca a HierarquiaEntidade
            var includes = IncludesHierarquiaEntidade.TipoHierarquiaEntidade;
            HierarquiaEntidade hierarquiaEntidade = HierarquiaEntidadeDomainService.SearchByKey<HierarquiaEntidade, HierarquiaEntidade>(seqHierarquiaEntidade, includes);
            if (hierarquiaEntidade == null)
                throw new HierarquiaEntidadeInvalidaException();

            // Specification para buscar o TipoHierarquiaEntidadeItem
            TipoHierarquiaEntidadeItemFilterSpecification spec = new TipoHierarquiaEntidadeItemFilterSpecification()
            {
                SeqTipoHierarquiaEntidade = hierarquiaEntidade.SeqTipoHierarquiaEntidade,
                SeqItemSuperior = seqTipoHierarquiaEntidadeItem,
                Raiz = seqTipoHierarquiaEntidadeItem.HasValue ? (bool?)null : true
            };

            // Se a hierarquia é de um tipo que tem visão, não permite selecionar as entidades externalizadas
            if (hierarquiaEntidade.TipoHierarquiaEntidade.TipoVisao.HasValue)
            {
                spec.EntidadeExternada = false;
            }

            // Retorno
            return TipoHierarquiaEntidadeItemDomainService.SearchBySpecification(spec, IncludesTipoHierarquiaEntidadeItem.TipoEntidade);
        }

        /// <summary>
        /// Busca a árvore de acordo com o SeqHierarquiaEntidade
        /// </summary>
        /// <param name="SeqHierarquiaEntidade">Sequencial da hierarquia de entidade</param>
        /// <returns>Dados dos nós da hierarquia</returns>
        public HierarquiaEntidadeItemNodeVO[] BuscarHierarquiaEntidadeItens(long seqHierarquiaEntidade)
        {
            TipoVisao? tipoVisaoHierarquia = HierarquiaEntidadeDomainService
                .SearchProjectionByKey(new SMCSeqSpecification<HierarquiaEntidade>(seqHierarquiaEntidade), p => p.TipoHierarquiaEntidade.TipoVisao);

            //Obtendo a Árvore
            HierarquiaEntidadeItemFilterSpecification specification = new HierarquiaEntidadeItemFilterSpecification() { SeqHierarquiaEntidade = seqHierarquiaEntidade };
            specification.SetOrderBy(o => o.TipoHierarquiaEntidadeItem.TipoEntidade.Descricao)
                         .SetOrderBy(o => o.Entidade.Nome);

            var includesHierarquiaEntidadeItem = IncludesHierarquiaEntidadeItem.Entidade_TipoEntidade                                // Utilizado para identificar entidades externadas
                                               | IncludesHierarquiaEntidadeItem.Entidade_HistoricoSituacoes_SituacaoEntidade         // Utilizado para identificar entidades ativas
                                               | IncludesHierarquiaEntidadeItem.ItemSuperior                                         // Utilizado para recuperar o tipo de hierarquia do item superior
                                               | IncludesHierarquiaEntidadeItem.TipoHierarquiaEntidadeItem_ItensFilhos_TipoEntidade; // Utilizado para identificar itens folha segundo a hierarquia de tipo de entidade
            var arvoreDomain = this.SearchBySpecification(specification, includesHierarquiaEntidadeItem).ToList();
            var arvoreVo = arvoreDomain.TransformToArray<HierarquiaEntidadeItemNodeVO>();

            int quantidadeItens = arvoreVo.Length;
            for (int i = 0; i < quantidadeItens; i++)
            {
                var itemDomain = arvoreDomain[i];
                var itemVo = arvoreVo[i];

                // Considera como folha itens que não possam ter filhos segundo a hierarquia de tipo
                itemVo.TipoClassificacaoFolha = itemDomain.TipoHierarquiaEntidadeItem.ItensFilhos.SMCCount(c => !c.TipoEntidade.EntidadeExternada) == 0;

                itemVo.TipoVisaoHierarquia = tipoVisaoHierarquia;
            }

            return arvoreVo;
        }

        /// <summary>
        /// Busca a árvore de acordo com o filtro para o lookup
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Array de hierarquia entidade item para árvore</returns>
        public HierarquiaEntidadeItemNodeVO[] BuscarHierarquiaEntidadeItemLookup(HierarquiaEntidadeItemFilterSpecification filtro)
        {
            HierarquiaEntidadeItemNodeVO[] arvore = this.SearchBySpecification(filtro, IncludesHierarquiaEntidadeItem.Entidade | IncludesHierarquiaEntidadeItem.Entidade_TipoEntidade | IncludesHierarquiaEntidadeItem.ItemSuperior).TransformToArray<HierarquiaEntidadeItemNodeVO>();

            int quantidadeItens = arvore.Length;
            for (int i = 0; i < quantidadeItens; i++)
            {
                HierarquiaEntidadeItemNodeVO item = arvore[i];

                HierarquiaEntidadeItemFilterSpecification specFolha = new HierarquiaEntidadeItemFilterSpecification() { SeqHierarquiaEntidadeSuperior = item.Seq, TipoVisaoHierarquia = filtro.TipoVisaoHierarquia };

                //Se o item correspondente não puder ter itens vinculados com base nas regras do UC, então, o mesmo é um nó folha
                item.TipoClassificacaoFolha = this.SearchProjectionBySpecification(specFolha, p => p.Seq).Count() == 0;

                item.TipoVisaoHierarquia = filtro.TipoVisaoHierarquia;
            }

            return arvore;
        }

        /// <summary>
        /// Recupera uma hierarquia de entidade considerando o nó informado como raiz
        /// </summary>
        /// <param name="seqItemRaiz">Raiz da hierarquia a ser recuperada</param>
        /// <param name="includesHierarquia">Includes dos itens de hierarquia (por padrão são incluídas apenas as entidades que os itens representam)</param>
        /// <returns>Lista de todos os nós de HierarquiaEntidadeItem com suas entidades</returns>
        public List<HierarquiaEntidadeItem> BuscarHierarquiaEntidadeItens(long seqItemRaiz, IncludesHierarquiaEntidadeItem includesHierarquia = IncludesHierarquiaEntidadeItem.Entidade)
        {
            return BuscarHierarquiaEntidadeItens(new long[] { seqItemRaiz }, includesHierarquia);
        }

        /// <summary>
        /// Recupera uma hierarquia de entidade considerando o nó informado como raiz
        /// </summary>
        /// <param name="seqItemRaiz">Raizes das das hierarquias a serem recuperadas</param>
        /// <param name="includesHierarquia">Includes dos itens de hierarquia (por padrão são incluídas apenas as entidades que os itens representam)</param>
        /// <returns>Lista de todos os nós de HierarquiaEntidadeItem com suas entidades</returns>
        public List<HierarquiaEntidadeItem> BuscarHierarquiaEntidadeItens(IEnumerable<long> seqItensRaiz, IncludesHierarquiaEntidadeItem includesHierarquia = IncludesHierarquiaEntidadeItem.Entidade)
        {
            var hierarquia = new List<HierarquiaEntidadeItem>();

            // Adiciona os itens informados no retorno
            var seqsRaizes = seqItensRaiz?.ToArray() ?? new long[] { };
            hierarquia.AddRange(SearchBySpecification(new SMCContainsSpecification<HierarquiaEntidadeItem, long>(p => p.Seq, seqsRaizes), includesHierarquia));
            // Recupera todos os filhos
            var seqsAtuais = seqsRaizes.Select(s => new long?(s)).ToArray();
            while (seqsAtuais.Length > 0)
            {
                var specProximoNivel = new SMCContainsSpecification<HierarquiaEntidadeItem, long?>(p => p.SeqItemSuperior, seqsAtuais);
                var itensProximoNivel = this.SearchBySpecification(specProximoNivel, includesHierarquia);

                hierarquia.AddRange(itensProximoNivel);
                seqsAtuais = itensProximoNivel.Select(s => new long?(s.Seq)).ToArray();
            }

            return hierarquia;
        }

        /// <summary>
        /// Recupera uma hierarquia de entidade pelo seq da entidade
        /// </summary>
        /// <param name="seqEntidade">Sequencial da entidade</param>
        /// <param name="tokenTipoEntidade">Tipo dos filhos que devem ser retornados</param>
        /// <returns>Lista de todos os sequenciais de curso filhos da entidade</returns>
        public List<long> BuscarHierarquiaEntidadeItensPorEntidadeVisaoOganizacional(long seqEntidade, string tokenTipoEntidade)
        {
            var hierarquia = new List<HierarquiaEntidadeItem>();
            var seqsEntidadesFilhas = new List<long>();

            try
            {
                DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                HierarquiaEntidadeItemFilterSpecification specPrincipal = new HierarquiaEntidadeItemFilterSpecification()
                {
                    SeqEntidade = seqEntidade,
                    TipoVisaoHierarquia = TipoVisao.VisaoOrganizacional
                };

                var raiz = this.SearchBySpecification(specPrincipal, IncludesHierarquiaEntidadeItem.Entidade_TipoEntidade).FirstOrDefault();

                if (raiz == null)
                    throw new SMCApplicationException($"Não foi encontrada hierarquia para a entidade informada ({seqEntidade}).");

                hierarquia.Add(raiz);

                var seqsAtuais = new long?[] { raiz.Seq };
                do
                {
                    var specProximoNivel = new SMCContainsSpecification<HierarquiaEntidadeItem, long?>(p => p.SeqItemSuperior, seqsAtuais);
                    var itensProximoNivel = this.SearchBySpecification(specProximoNivel, IncludesHierarquiaEntidadeItem.Entidade_TipoEntidade);

                    hierarquia.AddRange(itensProximoNivel);
                    seqsAtuais = itensProximoNivel.Select(s => new long?(s.Seq)).ToArray();
                } while (seqsAtuais.Length > 0);

                seqsEntidadesFilhas = hierarquia.Where(w => w.Entidade.TipoEntidade.Token == tokenTipoEntidade).Select(s => s.SeqEntidade).ToList();
            }
            finally
            {
                EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }
            return seqsEntidadesFilhas;
        }

        /// <summary>
        /// Excluí um item de hierarquia e seus filhos caso este não seja pai de itens externalizados conforme a regra RN_ORG_012.
        /// </summary>
        /// <param name="seq">Sequencial do item a ser excluído</param>
        /// <exception cref="HierarquiaEntidadeItemExclusaoExternalizadaException">Caso o item ou algum de seus filhos seja externalizado</exception>
        public void ExcluirHierarquiaEntidadeItem(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                // Recupera o item a ser excluído e seus filhos
                var hierarquia = this.BuscarHierarquiaEntidadeItens(seq, IncludesHierarquiaEntidadeItem.Entidade_TipoEntidade);
                if (hierarquia.Any(a => a.Entidade.TipoEntidade.EntidadeExternada))
                {
                    throw new HierarquiaEntidadeItemExclusaoExternalizadaException(hierarquia.First().Entidade.Nome);
                }

                this.DeleteEntity(seq);

                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Busca as entidades rensposaveis superiores.
        /// </summary>
        /// <param name="visao">Visão para ser avaliada</param>
        /// <param name="apenasAtivos">Apenas entidades ativas</param>
        /// <param name="usarSeqEntidade">Por padrão utiliza o Seq do item de hierarquia que representa a entidade. Quando setado devolve o seq da entidade</param>
        /// <returns>Lista de entidades</returns>
        public List<SMCDatasourceItem> BuscarEntidadesPai(TipoVisao visao, string tokenEntidade, bool apenasAtivos = false, bool usarSeqEntidade = false)
        {
            // Busca a hierarquia na visão vigente da instituição de ensino
            var hierarquia = this.HierarquiaEntidadeDomainService.BuscarHierarquiaVigente(visao);

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
            var tipoItensSuperiores = TipoHierarquiaEntidadeItemDomainService.SearchProjectionBySpecification(specTipo, x => x.SeqItemSuperior).Where(w => w.HasValue).Select(s => s.Value).ToList();

            if (tipoItensSuperiores.Count <= 0)
                throw new TipoHierarquiaEntidadeNaoConfigurouTipoException(visao.SMCGetDescription());

            // Busca nos itens da hierarquia aqueles que podem ser pai do tipo de entidade
            var specSeq = new HierarquiaEntidadeItemFilterSpecification() { SeqHierarquiaEntidade = hierarquia.Seq };
            var specItem = new SMCContainsSpecification<HierarquiaEntidadeItem, long>(h => h.SeqTipoHierarquiaEntidadeItem, tipoItensSuperiores.ToArray());
            var spec = new SMCAndSpecification<HierarquiaEntidadeItem>(specSeq, specItem);

            spec.SetOrderBy(x => x.Entidade.Nome);

            List<HierarquiaEntidadeItem> entidades = null;
            try
            {
                // Desconsidera os filtros de hierarquia que não sejam da visão atual
                this.DesativarFiltrosHierarquiaItem();
                this.AtivarFiltroHierarquiaItem(visao);
                entidades = this.SearchBySpecification(spec, IncludesHierarquiaEntidadeItem.Entidade_HistoricoSituacoes_SituacaoEntidade).ToList();
            }
            finally
            {
                this.AtivarFiltrosHierarquiaItem();
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

            return lista;
        }

        /// <summary>
        /// Buscar filhos da entidade na visão sem recursividade, sendo somente o primeiro nivel
        /// </summary>
        /// <param name="visao">Tipo da visão</param>
        /// <param name="seqEntidade">Sequencial da ENTIDADE</param>
        /// <returns>Sequenciais das entidades filhas</returns>
        public List<long> BuscarHierarquiaEntidadesItensFilhas(TipoVisao visao, long seqEntidade)
        {
            // Busca a hierarquia na visão vigente da instituição de ensino
            var hierarquia = this.HierarquiaEntidadeDomainService.BuscarHierarquiaVigente(visao);

            // Se não encontrou a hierarquia, erro
            if (hierarquia == null)
            {
                throw new HierarquiaEntidadeNaoConfiguradaException(visao.SMCGetDescription());
            }

            var spec = new HierarquiaEntidadeItemFilterSpecification()
            {
                SeqEntidadeSuperior = seqEntidade,
                SeqHierarquiaEntidade = hierarquia.Seq
            };

            var retorno = this.SearchProjectionBySpecification(spec, p => p.Seq).ToList();

            return retorno;
        }

        /// <summary>
        /// Buscar sequencial da hierarquia item por entidade em uma visão
        /// </summary>
        /// <param name="seqEntidade">Sequencial da ENTIDADE</param>
        /// <returns>Sequenciais da hierarquia item</returns>
        public long BuscarHierarquiaEntidadesItemPorEntidade(TipoVisao visao, long seqEntidade)
        {
            // Busca a hierarquia na visão vigente da instituição de ensino
            var hierarquia = this.HierarquiaEntidadeDomainService.BuscarHierarquiaVigente(visao);

            // Se não encontrou a hierarquia, erro
            if (hierarquia == null)
            {
                throw new HierarquiaEntidadeNaoConfiguradaException(visao.SMCGetDescription());
            }

            var spec = new HierarquiaEntidadeItemFilterSpecification()
            {
                SeqEntidade = seqEntidade,
                SeqHierarquiaEntidade = hierarquia.Seq
            };

            var retorno = this.SearchProjectionBySpecification(spec, p => p.Seq).FirstOrDefault();

            return retorno;
        }

        /// <summary>
        /// Busca um item de hierarquia sem dependências pela chave
        /// </summary>
        /// <param name="seq">Sequencial do item</param>
        /// <returns>Dados do item</returns>
        public HierarquiaEntidadeItem BuscarHierarquiaEntidadeItem(long seq)
        {
            return SearchByKey(new SMCSeqSpecification<HierarquiaEntidadeItem>(seq));
        }

        public void DesativarFiltrosHierarquiaItem()
        {
            DesativarFiltrosHierarquiaItem(this);
        }

        public void DesativarFiltrosHierarquiaItem<TEntity>(SMCDomainServiceBase<TEntity> domainService) where TEntity : class
        {
            domainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES);
            domainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            domainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS);
            domainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);
        }

        public void AtivarFiltrosHierarquiaItem()
        {
            AtivarFiltrosHierarquiaItem(this);
        }

        public void AtivarFiltrosHierarquiaItem<TEntity>(SMCDomainServiceBase<TEntity> domainService) where TEntity : class
        {
            domainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES);
            domainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            domainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS);
            domainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);
        }

        public void AtivarFiltroHierarquiaItem(TipoVisao tipoVisao)
        {
            AtivarFiltroHierarquiaItem(tipoVisao, this);
        }

        public void AtivarFiltroHierarquiaItem<TEntity>(TipoVisao tipoVisao, SMCDomainServiceBase<TEntity> domainService = null) where TEntity : class
        {
            string token = "";
            switch (tipoVisao)
            {
                case TipoVisao.VisaoLocalidades: token = FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES; break;
                case TipoVisao.VisaoOrganizacional: token = FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL; break;
                case TipoVisao.VisaoPolosVirtuais: token = FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS; break;
                case TipoVisao.VisaoUnidade: token = FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA; break;
            }
            domainService.EnableFilter(token);
        }

        /// <summary>
        /// Busca as entidades filhas da entidade responsavel na visa organizacional
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial entidade responsavel</param>
        /// <returns>
        /// Caso a entidade seja um grupo de programa retorna hierarquia item das filhas da entidade
        /// Caso contrario retorna a hierarquia item dela mesma
        /// </returns>
        public List<long> BuscarHierarquiaEntidadesItemFilhasEntidadeVinculo(long seqEntidadeVinculo)
        {
            var tokenEntidade = this.EntidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<Entidade>(seqEntidadeVinculo), p => p.TipoEntidade.Token);
            List<long> retorno = new List<long>();

            ///Se a entidade selecionada for do tipo "Grupo Programa":
            ///Listar as ofertas de cursos dos programas cuja a entidade responsável é o
            ///grupo selecionado e de acordo com os filtros de dados descritos pelas regras
            ///RN_USG_004 - Filtro por Nível de Ensino, RN_USG_005 -Filtro por Entidade Responsável e  RN_USG_007 - Filtro por Localidade.
            if (tokenEntidade == TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA)
            {
                retorno = this.BuscarHierarquiaEntidadesItensFilhas(TipoVisao.VisaoOrganizacional, seqEntidadeVinculo);
                return retorno;
            }

            ///Caso contrário,
            ///Listar as ofertas de cursos cuja a entidade responsável é a selecionado e de acordo com os
            ///filtros de dados descritos pelas regras RN_USG_004 - Filtro por Nível de Ensino e RN_USG_007 -Filtro por Localidade.
            var seqHierarquiaEntidadeItem = this.BuscarHierarquiaEntidadesItemPorEntidade(TipoVisao.VisaoOrganizacional, seqEntidadeVinculo);

            if (seqEntidadeVinculo == 0)
            {
                throw new SMCApplicationException("Entidade não confiurada na visão organizacional");
            }

            retorno.Add(seqHierarquiaEntidadeItem);

            return retorno;
        }

        ///// <summary>
        ///// Recupera as entidades filhas da entidade da entidade informada
        ///// </summary>
        ///// <param name="seqEntidade">Sequencial da "raiz" da hierarquia</param>
        ///// <param name="tokenTipoFilho">Tipo das entidades filhas que devem ser retornadas</param>
        ///// <param name="tipoVisao">Tipo da visão que deve ser considerada</param>
        ///// <param name="usarSeqEntidade">Quando setado usa o seq da entidade, caso contrário o seq da hierarquia item</param>
        ///// <param name="ativo">Quando setado devolve apenas as entidades filhas que estejam ativas</param>
        ///// <returns>Todas as entidades do tipo informado que estjam abaixo da raiz (seqEntidade) na hierarquia com o tipo de visão</returns>
        //public List<SMCDatasourceItem> BuscarHierarquiaEntidadeItensPorTipo(long seqEntidade, string tokenTipoFilho, TipoVisao tipoVisao, bool usarSeqEntidade = false, bool? ativo = null)
        //{
        //    var seqsFilhos = BuscarHierarquiaEntidadeItensPorEntidade(seqEntidade);

        //    var spec = new HierarquiaEntidadeItemFilterSpecification()
        //    {
        //        SeqsEntidade = seqsFilhos,
        //        TipoVisaoHierarquia = tipoVisao,
        //        EntidadeAtiva = ativo,
        //        TokenTipoEntidade = tokenTipoFilho
        //    };
        //    spec.SetOrderBy(o => o.Entidade.Nome);
        //    var entidadesFilhas = SearchProjectionBySpecification(spec, p => new
        //    {
        //        p.Seq,
        //        p.SeqEntidade,
        //        p.Entidade.Nome
        //    }).ToList();

        //    return entidadesFilhas
        //        .Select(s => new SMCDatasourceItem(usarSeqEntidade ? s.SeqEntidade : s.Seq, s.Nome))
        //        .ToList();
        //}
    }
}