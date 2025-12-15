using SMC.Framework.Jobs;

namespace SMC.Academico.WebApi.Models
{
    public class EmissaoCurriculoDigitalSATModel : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long? SeqCurriculo { get; set; }

        public int? CodigoCurriculoMigracao { get; set; }
    }
}