using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        public List<CampanhaCicloLetivoData> CiclosLetivos { get; set; }
    }
}