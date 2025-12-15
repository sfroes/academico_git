using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaCopiaData : ISMCMappable
    {
        public long SeqCampanhaOrigem { get; set; }

        public string DescricaoCampanhaOrigem { get; set; }

        public List<string> CiclosLetivosCampanhaOrigem { get; set; }

        public string DescricaoCampanhaDestino { get; set; }

        public List<CampanhaCopiaCicloLetivoItemData> CiclosLetivosCampanhaDestino { get; set; }

        public List<CampanhaCopiaOfertaListaData> CampanhaOfertas { get; set; }

        public List<object> GridCampanhaOferta { get; set; }

        public List<CampanhaCopiaProcessoSeletivoListaData> ProcessosSeletivos { get; set; }

        public List<object> GridProcessoSeletivo { get; set; }
    }
}