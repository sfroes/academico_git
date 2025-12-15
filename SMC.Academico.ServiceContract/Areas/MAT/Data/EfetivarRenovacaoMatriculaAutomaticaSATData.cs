using SMC.Framework.Jobs;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class EfetivarRenovacaoMatriculaAutomaticaSATData : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long? SeqProcesso { get; set; }
    }
}