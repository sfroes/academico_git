using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class ConvocacaoListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("CampanhaCicloLetivo.SeqCampanha")]
        public long SeqCampanha { get; set; }

        public string Descricao { get; set; }

        [SMCMapProperty("CampanhaCicloLetivo.CicloLetivo.Descricao")]
        public string DescCampanhaCicloLetivo { get; set; }

        public short QuantidadeChamadasRegulares { get; set; }

        public List<Chamada> Chamadas { get; set; }
    }
}