using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaOfertaAssociacaoVO : ISMCMappable
    {
        public long SeqCampanha { get; set; }

        public List<CampanhaOfertaVO> OfertasCampanha { get; set; }

        public long[] SeqsProcessosSeletivos { get; set; }

        public long[] SeqsConvocacoes { get; set; }
    }
}
