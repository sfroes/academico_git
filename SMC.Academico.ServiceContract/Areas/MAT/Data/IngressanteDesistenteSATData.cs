using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class IngressanteDesistenteSATData : ISMCMappable
    {
        /// <summary>
        /// Sequencial do histórico do agendamento do SAT.
        /// </summary>
        public long SeqHistoricoAgendamento { get; set; }
    }
}
