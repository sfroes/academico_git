using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class EscalaApuracaoData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public bool ApuracaoFinal { get; set; }

        public bool ApuracaoAvaliacao { get; set; }

        public TipoEscalaApuracao TipoEscalaApuracao { get; set; }

        public List<EscalaApuracaoItemData> Itens { get; set; }

        public bool UtilizadoPorCriterioAprovacao { get; set; }
    }
}
