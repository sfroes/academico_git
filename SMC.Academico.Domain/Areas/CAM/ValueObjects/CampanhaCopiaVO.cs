using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaCopiaVO : ISMCMappable
    {
        public long SeqCampanhaOrigem { get; set; }

        public string DescricaoCampanhaOrigem { get; set; }

        public List<string> CiclosLetivosCampanhaOrigem { get; set; }

        public string DescricaoCampanhaDestino { get; set; }

        public List<CampanhaCopiaCicloLetivoItemVO> CiclosLetivosCampanhaDestino { get; set; }

        public List<CampanhaCopiaOfertaListaVO> CampanhaOfertas { get; set; }

        public List<object> GridCampanhaOferta { get; set; }

        public List<CampanhaCopiaProcessoSeletivoListaVO> ProcessosSeletivos { get; set; }

        public List<object> GridProcessoSeletivo { get; set; }
    }
}