using SMC.Framework.Jobs;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class RealizarEnvioNotificacaoSATData : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long SeqEnvioNotificacao { get; set; }
    }
}
