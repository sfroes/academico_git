using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Extensions;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class InstituicaoTipoAtuacaoDomainService : AcademicoContextDomain<InstituicaoTipoAtuacao>
    {
        /// <summary>
        /// Busca os tipos de atuações de uma instituição de ensino
        /// </summary>
        /// <param name="filtros">Filtro com sequencial da instituição de ensino selecionada</param>
        /// <returns>Lista paginada com os tipos de atuações de uma instituição de ensino</returns>
        public SMCPagerData<InstituicaoTipoAtuacao> BuscarInstituicoesTiposAtuacoes(InstituicaoTipoAtuacaoFilterSpecification filtros)
        {
            try
            {
                this.DisableFilter(FILTER.INSTITUICAO_ENSINO);

                int total = 0;

                var tiposAtuacoes = this.SearchBySpecification(filtros, out total, IncludesInstituicaoTipoAtuacao.InstituicaoEnsino);

                return new SMCPagerData<InstituicaoTipoAtuacao>(tiposAtuacoes, total);
            }
            finally
            {
                this.EnableFilter(FILTER.INSTITUICAO_ENSINO);
            }           
        }

        /// <summary>
        /// Busca o tipo de atuação
        /// </summary>
        /// <param name="seq">Sequencial do tipo de atuação</param>
        /// <returns>Dados do tipo de atuação</returns>
        public InstituicaoTipoAtuacao BuscarInstituicaoTipoAtuacao(long seq)
        {
            try
            {
                this.DisableFilter(FILTER.INSTITUICAO_ENSINO);
              
                return this.SearchByKey(new SMCSeqSpecification<InstituicaoTipoAtuacao>(seq), IncludesInstituicaoTipoAtuacao.InstituicaoEnsino);
            }
            finally
            {
                this.EnableFilter(FILTER.INSTITUICAO_ENSINO);
            }           
        }

        /// <summary>
        /// Salva um tipo de atuação
        /// </summary>
        /// <param name="tipoAtuacao">Dados do tipo de atuação</param>
        /// <returns>Sequencial do tipo de atuação</returns>
        public long SalvarInstituicaoTipoAtuacao(InstituicaoTipoAtuacao instituicao)
        {
            try
            {
                this.DisableFilter(FILTER.INSTITUICAO_ENSINO);

                this.SaveEntity(instituicao);

                return instituicao.Seq;
            }
            finally
            {
                this.EnableFilter(FILTER.INSTITUICAO_ENSINO);
            }
        }

        /// <summary>
        /// Exclui um tipo atuação
        /// </summary>
        /// <param name="seqInstituicaoTipoAtuacao">Sequencial do tipo atuação para exclusão</param>
        public void ExcluirInstituicaoTipoAtuacao(long seq)
        {
            try
            {
                this.DisableFilter(FILTER.INSTITUICAO_ENSINO);

                this.DeleteEntity(seq);
            }
            finally
            {
                this.EnableFilter(FILTER.INSTITUICAO_ENSINO);
            }
        }
    }
}
