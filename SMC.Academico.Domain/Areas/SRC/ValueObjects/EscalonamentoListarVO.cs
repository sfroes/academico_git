using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class EscalonamentoListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcesso { get; set; }

        public List<ProcessoEtapaDetalheVO> Etapas { get; set; }
    }
}