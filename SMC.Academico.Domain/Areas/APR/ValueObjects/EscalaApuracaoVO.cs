using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class EscalaApuracaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public bool ApuracaoFinal { get; set; }

        public bool ApuracaoAvaliacao { get; set; }

        public TipoEscalaApuracao TipoEscalaApuracao { get; set; }

        public List<CriterioAprovacaoVO> CriteriosAprovacao { get; set; }

        public IList<EscalaApuracaoItemVO> Itens { get; set; }

        public bool UtilizadoPorCriterioAprovacao { get { return this.CriteriosAprovacao?.Count > 0; } }
    }
}
