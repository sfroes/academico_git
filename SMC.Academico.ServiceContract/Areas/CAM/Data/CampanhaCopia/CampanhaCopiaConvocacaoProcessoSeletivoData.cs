using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaCopiaConvocacaoProcessoSeletivoData : ISMCMappable
    {
        public long SeqProcessoSeletivo { get; set; }

        public List<CampanhaCopiaConvocacaoProcessoSeletivoItemData> Convocacoes { get; set; }
    }
}