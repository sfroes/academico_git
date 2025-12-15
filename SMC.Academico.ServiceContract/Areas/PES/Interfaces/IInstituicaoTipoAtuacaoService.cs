using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IInstituicaoTipoAtuacaoService : ISMCService
    {
        /// <summary>
        /// Busca os tipos de atuações de uma instituição de ensino
        /// </summary>
        /// <param name="filtros">Filtro com sequencial da instituição de ensino selecionada</param>
        /// <returns>Lista paginada com os tipos de atuações de uma instituição de ensino</returns>
        SMCPagerData<InstituicaoTipoAtuacaoListaData> BuscarInstituicoesTiposAtuacoes(InstituicaoTipoAtuacaoFiltroData filtros);

        /// <summary>
        /// Busca o tipo de atuação
        /// </summary>
        /// <param name="seq">Sequencial do tipo de atuação</param>
        /// <returns>Dados do tipo de atuação</returns>
        InstituicaoTipoAtuacaoListaData BuscarInstituicaoTipoAtuacao(long seq);
        
        /// <summary>
        /// Salva um tipo de atuação
        /// </summary>
        /// <param name="tipoAtuacao">Dados do tipo de atuação</param>
        /// <returns>Sequencial do tipo de atuação</returns>
        long SalvarInstituicaoTipoAtuacao(InstituicaoTipoAtuacaoData instituicao);
        
        /// <summary>
        /// Exclui um tipo atuação
        /// </summary>
        /// <param name="seqInstituicaoTipoAtuacao">Sequencial do tipo atuação para exclusão</param>
        void ExcluirInstituicaoTipoAtuacao(long seqInstituicao);       
    }
}
