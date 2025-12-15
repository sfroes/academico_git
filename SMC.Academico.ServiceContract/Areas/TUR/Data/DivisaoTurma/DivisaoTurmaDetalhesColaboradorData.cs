using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class DivisaoTurmaDetalhesColaboradorData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqColaborador { get; set; }

        public string NomeFormatado { get; set; }

        public short QuantidadeCargaHoraria { get; set; }
    }
}
