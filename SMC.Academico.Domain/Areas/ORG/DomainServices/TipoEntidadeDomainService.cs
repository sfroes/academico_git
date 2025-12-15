using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class TipoEntidadeDomainService : AcademicoContextDomain<TipoEntidade>
    {
        private TipoHierarquiaEntidadeItemDomainService TipoHierarquiaEntidadeItemDomainService => this.Create<TipoHierarquiaEntidadeItemDomainService>();

        /// <summary>
        /// Busca o tipo de uma entidade na instituição atual
        /// </summary>
        /// <param name="tokenTipoEntidade">Token do tipo da entidade</param>
        /// <returns>Tipo da entidade na instituição atual</returns>
        public TipoEntidade BuscarTipoEntidadeNaInstituicao(string tokenTipoEntidade)
        {
            var specTipo = new TipoEntidadeFilterSpecification() { Token = tokenTipoEntidade };

            specTipo.SetOrderBy(X => X.Descricao);

            return this.SearchByKey(specTipo);
        }

        /// <summary>
        /// Busca o tipos de entidade
        /// </summary>
        /// <param name="tokenTipoEntidade">Filtro</param>
        /// <returns>Tipos de entidade</returns>
        public List<TipoEntidade> BuscarTiposEntidade(TipoEntidadeFilterSpecification spec)
        {
            spec.SetOrderBy(X => X.Descricao);

            return this.SearchBySpecification(spec).ToList();
        }

   

        public List<SMCDatasourceItem> BuscarTiposEntidadeComInstituicao(bool permiteAtoNormativo, long seqInstituicaoEnsino)
        {
            var spec = new TipoEntidadeFilterSpecification() { PermiteAtoNormativo = permiteAtoNormativo };
            var retornoPermiteAtoNormativo = this.SearchBySpecification(spec);
            spec.SeqInstituicaoEnsino = seqInstituicaoEnsino;
            var retornoIntituicao = this.SearchBySpecification(spec);
            var temInstituicao = retornoPermiteAtoNormativo.Where(w => w.Token == TOKEN_TIPO_ENTIDADE_EXTERNADA.INSTITUICAO_ENSINO);

            return retornoIntituicao.Union(temInstituicao)
                                    .Distinct()
                                    .Select(s => new SMCDatasourceItem { Seq = s.Seq, Descricao = s.Descricao })
                                    .OrderBy(o => o.Descricao).ToList();
        }

        public bool PermiteAtoNormativo(long seq, string tokenTipoEntidade)
        {
            var retorno = this.SearchByKey(new SMCSeqSpecification<TipoEntidade>(seq));

            if (!string.IsNullOrEmpty(tokenTipoEntidade))
                return retorno.PermiteAtoNormativo && retorno.Token.Equals(tokenTipoEntidade);

            return retorno.PermiteAtoNormativo;
        }

        public string BuscarTokenTipoEntidade(TipoEntidadeFilterSpecification spec)
        {
            return this.SearchByKey(spec).Token;
        }

        public List<SMCDatasourceItem> BuscarTipoEntidadeResponsavelPorVisao (long seqInstituicaoEnsino, TipoVisao visao)
        {
            var spec = new TipoHierarquiaEntidadeItemFilterSpecification()
            {
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                TipoVisao = visao,
                Responsavel = true
            };
            var lista = TipoHierarquiaEntidadeItemDomainService.SearchProjectionBySpecification(spec, t => new
            { 
                Seq = t.SeqTipoEntidade, 
                Descricao = t.TipoEntidade.Descricao
            }).Distinct().OrderBy(t => t.Descricao).ToList();

            var retorno = new List<SMCDatasourceItem>();
            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
            }
            return retorno;
        }
    }
}