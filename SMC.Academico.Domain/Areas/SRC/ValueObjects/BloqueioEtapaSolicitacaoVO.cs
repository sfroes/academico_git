using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class BloqueioEtapaSolicitacaoVO : ISMCMappable
    {
        public string Etapa { get; set; }

        public List<BloqueioEtapaSolicitacaoItemVO> Bloqueios { get; set; }
    }
}