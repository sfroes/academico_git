using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class CondicaoObrigatoriedadeDomainService : AcademicoContextDomain<CondicaoObrigatoriedade>
    {
        #region [ Services ]

        private InstituicaoNivelCondicaoObrigatoriedadeDomainService InstituicaoNivelCondicaoObrigatoriedadeDomainService
        {
            get { return this.Create<InstituicaoNivelCondicaoObrigatoriedadeDomainService>(); }
        }

        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService
        {
            get { return this.Create<MatrizCurricularOfertaDomainService>(); }
        }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return this.Create<PessoaAtuacaoDomainService>(); }
        }

        #endregion [ Services ]

        /// <summary>
        /// Busca as condições de obrigatoriedade na instituição do nível de ensino informado
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados das condições de obrigatoriedade</returns>
        public List<SMCDatasourceItem> BuscarCondicoesObrigatoriedadePorNivelEnsino(long seqNivelEnsino)
        {
            var spec = new InstituicaoNivelCondicaoObrigatoriedadeFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };
            return this.InstituicaoNivelCondicaoObrigatoriedadeDomainService.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.SeqCondicaoObrigatoriedade,
                Descricao = p.CondicaoObrigatoriedade.Descricao
            }).ToList();
        }

        /// <summary>
        /// Busaca as condições de obigatoriedade configuradas nos grupos curriculares da currículo curso oferta da matriz informada
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da oferta de matriz currícular</param>
        /// <returns>Condições de obrigatoriedade</returns>
        public List<SMCDatasourceItem> BuscarCondicoesObrigatoriedadePorMatrizCurricularOferta(long seqMatrizCurricularOferta)
        {
            // Recupera todas condições de obrigatoriedade utilizadas nos grupos do currículo curso oferta da matriz informada
            var specMatrizOferta = new SMCSeqSpecification<MatrizCurricularOferta>(seqMatrizCurricularOferta);
            var seqsCondicoesObrigatoriedade = this.MatrizCurricularOfertaDomainService.SearchProjectionByKey(specMatrizOferta, p => p.MatrizCurricular.CurriculoCursoOferta.GruposCurriculares.SelectMany(s => s.GrupoCurricular.CondicoesObrigatoriedade.Select(c => c.Seq)))?.ToArray() ?? new long[] { };

            // Projeta pelos seqs das condições recuperadas para evitar duplicidade e poder aplicar a ordenação
            var specCondicao = new SMCContainsSpecification<CondicaoObrigatoriedade, long>(p => p.Seq, seqsCondicoesObrigatoriedade);
            specCondicao.SetOrderBy(o => o.Descricao);
            return this.SearchProjectionBySpecification(specCondicao, p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Descricao }).ToList();
        }
    }
}