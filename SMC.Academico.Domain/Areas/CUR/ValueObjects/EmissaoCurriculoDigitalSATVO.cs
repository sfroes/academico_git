using SMC.Framework.Jobs;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class EmissaoCurriculoDigitalSATVO : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long? SeqCurriculo { get; set; }

        public int? CodigoCurriculoMigracao { get; set; }
    }
}
