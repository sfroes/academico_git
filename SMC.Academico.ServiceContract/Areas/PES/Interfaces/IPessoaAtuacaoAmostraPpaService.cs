using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IPessoaAtuacaoAmostraPpaService : ISMCService
    {
        /// <summary>
        /// Busca os dados da amostra PPA que ainda não foi preenchida para apresentar banner
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação a ser pesquisada</param>
        /// <param name="tipoAvaliacao">Tipo de avaliação a ser pesquisada</param>
        /// <returns>Dados da amostra PPA encontrada ou NULL</returns>
        int? BuscarPessoaAtuacaoAmostraPpaNaoPreenchida(long seqPessoaAtuacao, TipoAvaliacaoPpa tipoAvaliacao);

        PessoaAtuacaoAmostraPpaCabecalhoData BuscarCabecalhoPessoaAtuacaoAmostraPpa(long seqConfiguracaoAvaliacaoPpa, long? seqConfiguracaoAvaliacaoPpaTurma);

        SMCPagerData<PessoaAtuacaoAmostraPpaListaData> ListarAmostras(PessoaAtuacaoAmostraPpaFiltroData filtro);
    }
}