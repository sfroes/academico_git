using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class HistoricoEscolarTurmaData : ISMCMappable
    {
        public long? SeqComponenteCurricular { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long? SeqTurma { get; set; }

        public int? CodigoTurma { get; set; }

        public short? NumeroTurma { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public string CodigoTurmaFormatado { get; set; }

        public List<string> Professores { get; set; }
    }
}
