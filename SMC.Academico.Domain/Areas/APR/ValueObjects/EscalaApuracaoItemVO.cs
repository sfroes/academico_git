using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class EscalaApuracaoItemVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEscalaApuracao { get; set; }

        public string Descricao { get; set; }

        public decimal? PercentualMinimo { get; set; }
        
        public decimal? PercentualMaximo { get; set; }

        public bool Aprovado { get; set; }
    }
}
