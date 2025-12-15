using SMC.Framework.Jobs;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CargaIngressanteSATVO: ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long SeqChamada { get; set; }

        public long SeqEntidadeInstituicao { get; set; }

        public string SeqSolicitante { get; set; }

        public string NomeSolicitante { get; set; }
    }
}
