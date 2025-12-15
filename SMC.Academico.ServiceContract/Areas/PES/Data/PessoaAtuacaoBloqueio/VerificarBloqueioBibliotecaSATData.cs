using SMC.Framework.Jobs;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class VerificarBloqueioBibliotecaSATData : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long? SeqPessoaAtuacao { get; set; }
    }
}