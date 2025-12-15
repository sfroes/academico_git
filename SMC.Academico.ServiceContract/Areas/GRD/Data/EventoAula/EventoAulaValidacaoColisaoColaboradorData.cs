using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class EventoAulaValidacaoColisaoColaboradorData : ISMCMappable
    {
        public long SeqColaborador { get; set; }
        public long SeqDivisaoTurma { get; set; }
        public int? CodigoLocalSEF { get; set; }
        public DateTime DataAula { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }
    }
}
