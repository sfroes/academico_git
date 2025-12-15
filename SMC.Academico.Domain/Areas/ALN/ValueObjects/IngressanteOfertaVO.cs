using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IngressanteOfertaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCampanhaOferta { get; set; }

        public long? SeqCampanhaOfertaOrigem { get; set; }

        public long? SeqInscricaoOfertaGpi { get; set; }

        public long? SeqCampanhaOfertaItem { get; set; }

        public bool? InteressePermanecerOfertaOrigem { get; set; }
    }
}