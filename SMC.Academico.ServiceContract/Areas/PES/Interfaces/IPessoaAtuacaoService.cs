using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IPessoaAtuacaoService : ISMCService
    {
        SMCPagerData<PessoaAtuacaoListaData> BuscarPessoaAtuacoes(PessoaAtuacaoFiltroData filtro);

        PessoaAtuacaoData BuscarPessoaAtuacao(long seq);

        /// <summary>
        /// Busca os dados de cabeçalho de uma pessoa atuação
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        PessoaAtuacaoCabecalhoData BuscarPessoaAtuacaoCabecalho(long seq);

        PessoaAtuacaoVisualizacaoDocumentoData BuscarDocumentosPessoaAtuacao(long seqPessoaAtuacao);

        /// <summary>
        /// Prepara o modelo para registro de documentos da pessoa atuação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqTipoDocumento">Sequencial do tipo de documento</param>
        /// <param name="seqsSolicitacoesServico">Sequenciais das solicitações de serviço</param>
        /// <returns>Objeto preparado para o registro de documentos</returns>
        PessoaAtuacaoRegistroDocumentoData PrepararModeloRegistroDocumento(long seqPessoaAtuacao, long seqTipoDocumento, List<long> seqsSolicitacoesServico);

        /// <summary>
        /// Busa a descricao do tipo de document pelo sequencial
        /// </summary>
        /// <param name="seqTipoDocumento">Sequenciao do tipo de documento</param>
        /// <returns>descricao do tipo de documento</returns>
        string BuscarDescricaoTipoDocumento(long seqTipoDocumento);

        /// <summary>
        /// Salva o registro de documentos da pessoa atuação
        /// </summary>
        /// <param name="model">Modelo a ser persistido</param>
        void SalvarRegistroDocumento(PessoaAtuacaoRegistroDocumentoData model);

        /// <summary>
        /// Busca os alunos e colaboradores para emissão da identidade estudantil pelos seqs informados
        /// </summary>
        /// <param name="seqsAlunos">Seqs dos alunos para pesquisa</param>
        /// <param name="seqsColaboradores">Seqs dos colaboradores para pesquisa</param>
        /// <returns>Lista de alunos e colaboradores para emissão da identidade estudantil</returns>
        List<RelatorioIdentidadeEstudantilData> BuscarPessoaAtuacaoIdentidadeEstudantil(List<long> seqsAlunos, List<long> seqsColaboradores);

        PessoaAtuacaoDadosOrigemData RecuperaDadosOrigem(long seqAluno, bool desativarFiltroDados = false);

        /// <summary>
        /// Busca os dados da pessoa atuação para header da tela de mensagem
        /// </summary>
        /// <param name="seq">Sequencial da pessoa atuação</param>
        /// <returns>Dados da pessoa atuação</returns>
        PessoaAtuacaoMensagemHeaderData BuscarPessoaAtuacaoHeaderMensagem(long seq);
    }
}