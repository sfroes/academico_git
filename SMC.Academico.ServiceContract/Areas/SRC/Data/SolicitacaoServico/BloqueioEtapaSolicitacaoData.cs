using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class BloqueioEtapaSolicitacaoData : ISMCMappable
    {
        public string Etapa { get; set; }

        public List<BloqueioEtapaSolicitacaoItemData> Bloqueios { get; set; }
    }
}