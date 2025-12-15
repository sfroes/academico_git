using SMC.Framework.Jobs;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    /// <summary>
    /// DTO para importação de ingressantes do GPI.
    /// </summary>
    public class CargaIngressanteSATData : ISMCWebJobFilterModel
    {
        /// <summary>
        /// Sequencial do histórico do agendamento.
        /// </summary>
        public long SeqHistoricoAgendamento { get; set; }

        /// <summary>
        /// Sequencial da chamada.
        /// </summary>
        public long SeqChamada { get; set; }

        /// <summary>
        /// Sequencial da instituição de ensino.
        /// </summary>
        public long SeqEntidadeInstituicao { get; set; }

        /// <summary>
        /// Sequencial do usuario que solicitou a  carga no academico
        /// </summary>
        public string SeqSolicitante { get; set; }

        /// <summary>
        /// Nome do usuario que solicitou a  carga no academico
        /// </summary>
        public string NomeSolicitante { get; set; }
    }
}
