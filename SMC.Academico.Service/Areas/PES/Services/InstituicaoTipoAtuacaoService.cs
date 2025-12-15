using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class InstituicaoTipoAtuacaoService : SMCServiceBase, IInstituicaoTipoAtuacaoService
    {
        #region [ DomainService ]

        private InstituicaoTipoAtuacaoDomainService InstituicaoTipoAtuacaoDomainService
        {
            get { return this.Create<InstituicaoTipoAtuacaoDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca os tipos de atuações de uma instituição de ensino
        /// </summary>
        /// <param name="filtros">Filtro com sequencial da instituição de ensino selecionada</param>
        /// <returns>Lista paginada com os tipos de atuações de uma instituição de ensino</returns>
        public SMCPagerData<InstituicaoTipoAtuacaoListaData> BuscarInstituicoesTiposAtuacoes(InstituicaoTipoAtuacaoFiltroData filtros)
        {
            var instituicaoTipoAtuacao = InstituicaoTipoAtuacaoDomainService.BuscarInstituicoesTiposAtuacoes(filtros.Transform<InstituicaoTipoAtuacaoFilterSpecification>());
            return instituicaoTipoAtuacao.Transform<SMCPagerData<InstituicaoTipoAtuacaoListaData>>();
        }

        /// <summary>
        /// Busca o tipo de atuação
        /// </summary>
        /// <param name="seq">Sequencial do tipo de atuação</param>
        /// <returns>Dados do tipo de atuação</returns>
        public InstituicaoTipoAtuacaoListaData BuscarInstituicaoTipoAtuacao(long seq)
        {
            return InstituicaoTipoAtuacaoDomainService.BuscarInstituicaoTipoAtuacao(seq).Transform<InstituicaoTipoAtuacaoListaData>();
        }

        /// <summary>
        /// Salva um tipo de atuação
        /// </summary>
        /// <param name="tipoAtuacao">Dados do tipo de atuação</param>
        /// <returns>Sequencial do tipo de atuação</returns>
        public long SalvarInstituicaoTipoAtuacao(InstituicaoTipoAtuacaoData instituicao)
        {
            return InstituicaoTipoAtuacaoDomainService.SalvarInstituicaoTipoAtuacao(instituicao.Transform<InstituicaoTipoAtuacao>());
        }

        /// <summary>
        /// Exclui um tipo atuação
        /// </summary>
        /// <param name="seqInstituicaoTipoAtuacao">Sequencial do tipo atuação para exclusão</param>
        public void ExcluirInstituicaoTipoAtuacao(long seq)
        {
            InstituicaoTipoAtuacaoDomainService.ExcluirInstituicaoTipoAtuacao(seq);
        }
    }
}
