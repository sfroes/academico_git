using SMC.Framework.Jobs;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class EmissaoCurriculoDigitalSATData : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long? SeqCurriculo { get; set; }

        public int? CodigoCurriculoMigracao { get; set; }
    }
}
