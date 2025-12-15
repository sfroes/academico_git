using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class EscalonamentoListarData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqProcesso { get; set; }

        public List<ProcessoEtapaDetalheData> Etapas { get; set; }
    }
}