using SMC.Framework.Jobs;

namespace SMC.Academico.WebApi.Models
{
    public class VerificarBloqueioBibliotecaSATModel : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long? SeqPessoaAtuacao { get; set; }
    }
}