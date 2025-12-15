using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConvocacaoListaData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("CampanhaCicloLetivo.SeqCampanha")]
        public long SeqCampanha { get; set; }

        public string Descricao { get; set; }

        [SMCMapProperty("CampanhaCicloLetivo.CicloLetivo.Descricao")]
        public string DescCampanhaCicloLetivo { get; set; }

        public short QuantidadeChamadasRegulares { get; set; }

        public List<ChamadaData> Chamadas { get; set; }
    }
}