using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class HistoricoEscolarTurmaVO : ISMCMappable
    {
        public long? SeqComponenteCurricular { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long? SeqTurma { get; set; }

        public int? CodigoTurma { get; set; }

        public short? NumeroTurma { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public string CodigoTurmaFormatado { get { return $"{CodigoTurma}.{NumeroTurma}"; } }

        public List<PessoaDadosPessoais> ColaboradoresBanco { get; set; }

        public List<string> Professores { get; set; }

        public bool? GeraOrientacao { get; set; }

        public long? SeqOrientacao { get; set; }

        public List<PessoaDadosPessoais> ColaboradoresOrientacao { get; set; }
    }
}
