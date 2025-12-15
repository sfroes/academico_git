using SMC.Framework.Jobs;

namespace SMC.Academico.WebApi.Models
{
    public class CargaIngressanteSATModel : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long SeqChamada { get; set; }

        public long SeqEntidadeInstituicao { get; set; }

        public string SeqSolicitante { get; set; }

        public string NomeSolicitante { get; set; }
    }
}