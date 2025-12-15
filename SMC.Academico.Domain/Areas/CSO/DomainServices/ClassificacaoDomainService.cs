using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class ClassificacaoDomainService : AcademicoContextDomain<Classificacao>
    {
        #region DomainService

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService => Create<HierarquiaEntidadeItemDomainService>();
        private HierarquiaClassificacaoDomainService HierarquiaClassificacaoDomainService => Create<HierarquiaClassificacaoDomainService>();

        private TipoClassificacaoDomainService TipoClassificacaoDomainService => Create<TipoClassificacaoDomainService>();

        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();

        #endregion DomainService

        public List<long> BuscarHierarquiaInferiorClassificacao(long seqClassificacao)
        {
            var retorno = new List<long>() { seqClassificacao };

            void Loop(long seqClassificacaoPai)
            {
                var filhos = SearchProjectionByKey(new SMCSeqSpecification<Classificacao>(seqClassificacaoPai), x => x.ClassificacoesFilhas.Select(f => f.Seq).ToList());
                foreach (var filho in filhos)
                {
                    Loop(filho);
                }
                retorno.AddRange(filhos);
            }

            Loop(seqClassificacao);

            return retorno;
        }

        /// <summary>
        /// Busca as classificações de uma hierarquia
        /// </summary>
        /// <param name="SeqHierarquiaClassificacao"></param>
        /// <returns>Dados da hierarquia de classificação</returns>
        public ClassificacaoVO[] BuscarClassificacaoPorHierarquiaClassificacaoLookup(long[] seq)
        {
            return BuscarClassificacaoPorHierarquiaClassificacao(new ClassificacaoFiltroVO() { Seqs = seq });
        }

        /// <summary>
        /// Busca as classificações de uma hierarquia
        /// </summary>
        /// <param name="filtroVO">Dados do fitlro</param>
        /// <returns>Dados da hierarquia de classificação</returns>
        public ClassificacaoVO[] BuscarClassificacaoPorHierarquiaClassificacao(ClassificacaoFiltroVO filtroVO)
        {
            var specClassificacao = filtroVO.Transform<ClassificacaoFilterSpecification>();
            //Tem filtro automático por 10 registros, que não corresponde com a regra
            specClassificacao.MaxResults = int.MaxValue;
            var classificacoesHierarquia = SearchProjectionBySpecification(specClassificacao, p => new ClassificacaoVO()
            {
                Seq = p.Seq,
                SeqHierarquiaClassificacao = p.SeqHierarquiaClassificacao,
                SeqClassificacaoSuperior = p.SeqClassificacaoSuperior,
                SeqTipoClassificacao = p.SeqTipoClassificacao,
                Descricao = p.Descricao,
                DescricaoTipoClassificacao = p.TipoClassificacao.Descricao,
                CodigoExterno = p.CodigoExterno,
            }).ToArray();

            //FIX: Dependency para array nulo
            if (filtroVO.SeqsHierarquiaEntidadeItem.SMCAny(a => a != 0))
            {
                // Recupera as entidades responsáveis de acordo com os SeqHierarquiaEntidadeItem informados
                var specHierarquiaEntidadeItem = new SMCContainsSpecification<HierarquiaEntidadeItem, long>(p => p.Seq, filtroVO.SeqsHierarquiaEntidadeItem);
                var seqsEntidadesResponsaveis = HierarquiaEntidadeItemDomainService.SearchProjectionBySpecification(specHierarquiaEntidadeItem, x => x.SeqEntidade).ToArray();

                if (seqsEntidadesResponsaveis.Any())
                {
                    var specEntidadesResponsaveis = new SMCContainsSpecification<Entidade, long>(p => p.Seq, seqsEntidadesResponsaveis);
                    var seqClassificacoesEntidadesResponsaveis = EntidadeDomainService
                        .SearchProjectionBySpecification(specEntidadesResponsaveis, p => p.Classificacoes.Select(s => s.SeqClassificacao))
                        .SelectMany(s => s)
                        .Distinct()
                        .ToList();
                    var nos = classificacoesHierarquia.Where(w => seqClassificacoesEntidadesResponsaveis.Contains(w.Seq));
                    var hierarquiaFiltrada = RecuperarRamos(classificacoesHierarquia, nos);
                    classificacoesHierarquia = hierarquiaFiltrada.OrderBy(o => o.Descricao).ToArray();
                }
            }

            foreach (var classificacao in classificacoesHierarquia)
            {
                classificacao.TipoClassificacaoSelecionavel = !filtroVO.SeqTipoClassificacao.HasValue || classificacao.SeqTipoClassificacao == filtroVO.SeqTipoClassificacao;
                classificacao.Descricao = string.IsNullOrEmpty(classificacao.CodigoExterno) ?
                    $"[{classificacao.DescricaoTipoClassificacao}] - {classificacao.Descricao}" :
                    $"[{classificacao.DescricaoTipoClassificacao}] - {classificacao.CodigoExterno} - {classificacao.Descricao}";
            }

            return classificacoesHierarquia;
        }

        private IEnumerable<ClassificacaoVO> RecupearPais(IEnumerable<ClassificacaoVO> hierarquia, IEnumerable<ClassificacaoVO> filhos)
        {
            return hierarquia.SMCFindRelated(filhos, (filho, pai) => filho.SeqClassificacaoSuperior == pai.Seq);
        }

        private IEnumerable<ClassificacaoVO> RecuperarFilhos(IEnumerable<ClassificacaoVO> hierarquia, IEnumerable<ClassificacaoVO> pais)
        {
            return hierarquia.SMCFindRelated(pais, (pai, filho) => filho.SeqClassificacaoSuperior == pai.Seq);
        }

        private IEnumerable<ClassificacaoVO> RecuperarRamos(IEnumerable<ClassificacaoVO> hieraquia, IEnumerable<ClassificacaoVO> nos)
        {
            return RecupearPais(hieraquia, nos).Union(RecuperarFilhos(hieraquia, nos)).Union(nos);
        }
    }
}