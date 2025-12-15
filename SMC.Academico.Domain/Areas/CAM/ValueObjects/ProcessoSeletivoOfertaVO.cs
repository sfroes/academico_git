using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class ProcessoSeletivoOfertaVO : ISMCMappable
    {
        public long Seq { get; set; }
                
        public long SeqProcessoSeletivo { get; set; }
                
        public long SeqCampanhaOferta { get; set; }

        public short QuantidadeVagas { get; set; }

        public long? SeqHierarquiaOfertaGpi { get; set; }

        public short QuantidadeVagasOcupadas { get; set; }

        public List<long> Ofertas { get; set; }

        public List<long> Convocacoes { get; set; }

        public bool ProcessoReservaVaga { get; set; }
    }
}
