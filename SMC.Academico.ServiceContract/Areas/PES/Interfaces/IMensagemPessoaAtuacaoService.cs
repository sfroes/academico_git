using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IMensagemPessoaAtuacaoService : ISMCService
    {
        /// <summary>
        /// Listar todas as mensagens dos dados
        /// </summary>
        /// <param name="filtro">Fitros de pesquisa</param>
        /// <returns>Lista de com as mensagens e seus dados</returns>
        SMCPagerData<MensagemListaData> ListarMensagens(MensagemFiltroData filtro);

        /// <summary>
        /// Salva um registro de Mensagem e, em seguida,
        /// salva um registro em Mensagem Pessoa Atuação, relacioada à mensagem recém salva.
        /// </summary>
        /// <param name="Mensagem"></param>
        /// <returns>Sequencial do Mensagem Pessoa Atuação.</returns>
        long SalvarMensagem(MensagemData Mensagem);

        /// <summary>
        /// Validar se assert referente a regras irá ser exibido
        /// </summary>
        /// <param name="mensagem">Dados da mensagem</param>
        /// <returns>Boleano permitindo ou não a exibiçaõ de assert</returns>
        bool validarMensagemAssert(MensagemData mensagem);

        /// <summary>
        /// Mensagem que será exibida no assert
        /// </summary>
        /// <param name="mensagem">Dados da mensagem</param>
        /// <returns>Mensagem que será exibida no assert de mensagem</returns>
        string BuscarMensagemAssert(MensagemData mensagem);

        /// <summary>
        /// Exclui o registro da tabela Mensagem Pessoa Atuacao e,
        /// em seguida, tenta excluir o registro da tabela Mensagem.
        /// Se o registro estiver relacionado a outros 'Mensagem Pessoa Atuação'
        /// a mensagem não será excluída.
        /// </summary>
        /// <param name="seqMensagemPessoaAtuacao">Sequencial do Mensagem Pessoa Atuação que seré excluído.</param>
        void ExcluirMensagem(long seq);

        /// <summary>
        /// Recupera uma mensagem e algumas configurações desta
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação associada à mensagem</param>
        /// <param name="seq">Sequencial da mensagem</param>
        /// <returns>Dados da mensagem e algumas configurações</returns>
        MensagemData BuscarMensagem(long seqPessoaAtuacao, long seq);

        /// <summary>
        /// Verifica se a pessoa atuação informada é um aluno formado
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Mensagem com o campo AlunoFormado preenchido</returns>
        MensagemData BuscarConfiguracaoMensagem(long seqPessoaAtuacao);
    }
}