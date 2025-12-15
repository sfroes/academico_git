using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConvocacaoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        public long SeqCampanha { get; set; }

        public string Descricao { get; set; }

        public string DescCampanhaCicloLetivo { get; set; }

        public short QuantidadeChamadasRegulares { get; set; }

        public List<ChamadaViewModel> Chamadas { get; set; }
    }
}