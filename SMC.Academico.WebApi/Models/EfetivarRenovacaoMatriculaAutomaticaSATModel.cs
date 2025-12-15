using SMC.Framework.Jobs;

namespace SMC.Academico.WebApi.Models
{
    public class EfetivarRenovacaoMatriculaAutomaticaSATModel : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long? SeqProcesso { get; set; }
    }
}