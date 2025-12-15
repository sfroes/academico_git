using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class VerificarPendenciaBibliotecaAutomaticaVO : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public string NumeroProtocolo { get; set; }

        public string NomePessoa { get; set; }
    }
}