using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ChamadaConvocacaoData : ISMCMappable
    {
        public long? SeqAgendamento { get; set; }

        public string Campanha { get; set; }

        public string ProcessoSeletivo { get; set; }

        public int NumeroChamada { get; set; }

        public TipoChamada TipoChamada { get; set; }

        public SituacaoChamada SituacaoChamada { get; set; }

        public string Convocacao { get; set; }

        public string DescricaoCicloLetivo { get; set; }
    }
}
