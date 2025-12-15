using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class MensagemPessoaAtuacaoService : SMCServiceBase, IMensagemPessoaAtuacaoService
    {
        #region [ DomainServices ]

        private MensagemPessoaAtuacaoDomainService MensagemPessoaAtuacaoDomainService
        {
            get { return Create<MensagemPessoaAtuacaoDomainService>(); }
        }

        #endregion [ DomainServices ]

        /// <summary>
        /// Listar todas as mensagens dos dados
        /// </summary>
        /// <param name="filtro">Fitros de pesquisa</param>
        /// <returns>Lista de com as mensagens e seus dados</returns>
        public SMCPagerData<MensagemListaData> ListarMensagens(MensagemFiltroData filtro)
        {
            var filtroVO = filtro.Transform<MensagemFiltroVO>();
            var list = MensagemPessoaAtuacaoDomainService.ListarMensagens(filtroVO);
            var result = list.TransformList<MensagemListaData>();

            return new SMCPagerData<MensagemListaData>(result, list.Total);
        }

        /// <summary>
        /// Salva um registro de Mensagem e, em seguida,
        /// salva um registro em Mensagem Pessoa Atuação, relacioada à mensagem recém salva.
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns>Sequencial do Mensagem Pessoa Atuação.</returns>
        public long SalvarMensagem(MensagemData mensagem)
        {
            return MensagemPessoaAtuacaoDomainService.SalvarMensagem(mensagem.Transform<MensagemVO>());
        }

        /// <summary>
        /// Validar se assert referente a regras irá ser exibido
        /// </summary>
        /// <param name="mensagem">Dados da mensagem</param>
        /// <returns>Boleano permitindo ou não a exibiçaõ de assert</returns>
        public bool validarMensagemAssert(MensagemData mensagem)
        {
            return MensagemPessoaAtuacaoDomainService.validarMensagemAssert(mensagem.Transform<MensagemVO>());
        }

        /// <summary>
        /// Mensagem que será exibida no assert
        /// </summary>
        /// <param name="mensagem">Dados da mensagem</param>
        /// <returns>Mensagem que será exibida no assert de mensagem</returns>
        public string BuscarMensagemAssert(MensagemData mensagem)
        {
            return MensagemPessoaAtuacaoDomainService.BuscarMensagemAssert(mensagem.Transform<MensagemVO>());
        }

        /// <summary>
        /// Exclui o registro da tabela Mensagem Pessoa Atuacao e,
        /// em seguida, tenta excluir o registro da tabela Mensagem.
        /// Se o registro estiver relacionado a outros 'Mensagem Pessoa Atuação'
        /// a mensagem não será excluída.
        /// </summary>
        public void ExcluirMensagem(long seq)
        {
            MensagemPessoaAtuacaoDomainService.ExcluirMensagem(seq);
        }

        /// <summary>
        /// Recupera uma mensagem e algumas configurações desta
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação associada à mensagem</param>
        /// <param name="seq">Sequencial da mensagem</param>
        /// <returns>Dados da mensagem e algumas configurações</returns>
        public MensagemData BuscarMensagem(long seqPessoaAtuacao, long seq)
        {
            return MensagemPessoaAtuacaoDomainService.BuscarMensagem(seqPessoaAtuacao, seq).Transform<MensagemData>();
        }

        /// <summary>
        /// Verifica se a pessoa atuação informada é um aluno formado
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Mensagem com o campo AlunoFormado preenchido</returns>
        public MensagemData BuscarConfiguracaoMensagem(long seqPessoaAtuacao)
        {
            return MensagemPessoaAtuacaoDomainService.BuscarConfiguracaoMensagem(seqPessoaAtuacao).Transform<MensagemData>();
        }
    }
}