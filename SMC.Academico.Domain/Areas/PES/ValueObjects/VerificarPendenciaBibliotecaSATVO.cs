using SMC.Framework.Jobs;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class VerificarPendenciaBibliotecaSATVO : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long? SeqPessoaAtuacao { get; set; }
    }
}