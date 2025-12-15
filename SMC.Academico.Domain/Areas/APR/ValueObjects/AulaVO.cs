using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class AulaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDivisaoTurma { get; set; }

        public DateTime? DataAula { get; set; }

        public List<ApuracaoFrequenciaVO> ApuracoesFrequencia { get; set; }

        public List<AulaOfertaVO> Ofertas { get; set; }
    }
}