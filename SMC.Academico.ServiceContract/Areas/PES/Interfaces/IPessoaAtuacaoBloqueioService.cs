using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IPessoaAtuacaoBloqueioService : ISMCService
    {
        SMCPagerData<PessoaAtuacaoBloqueioListaData> BuscarPessoasAtuacoesBloqueios(PessoaAtuacaoBloqueioFiltroData filtros);

        PessoaAtuacaoBloqueioData PreencherModeloInserirPessoaAtuacaoBloqueio();

        PessoaAtuacaoBloqueioData PreencherModeloAlterarPessoaAtuacaoBloqueio(long seqPessoaAtuacaoBloqueio);

        PessoaAtuacaoBloqueioDesbloqueioData PreencherModeloDesbloquearPessoaAtuacaoBloqueio(long seqPessoaAtuacaoBloqueio);

        PessoaAtuacaoBloqueioCabecalhoData BuscarCabecalhoPessoaAtuacaoBloqueio(long seqPessoaAtuacaoBloqueio);

        long SalvarPessoaAtuacaoBloqueioDesbloqueio(PessoaAtuacaoBloqueioDesbloqueioData modelo);

        List<PessoaAtuacaoBloqueioData> BuscarPessoaAtuacaoBloqueios(long seqPessoaAtuacao, long seqConfiguracaoEtapa, bool bloqueioFimetapa);

        long SalvarPessoaAtuacaoBloqueio(PessoaAtuacaoBloqueioData modelo);

        /// <summary>
        /// Realiza a verificação de pendências de material da biblioteca de forma automática
        /// </summary>
        /// <param name="filtro">Filtros para verificação</param>
        void VerificarBloqueioBibliotecaAutomatica(VerificarBloqueioBibliotecaSATData filtro);
    }
}