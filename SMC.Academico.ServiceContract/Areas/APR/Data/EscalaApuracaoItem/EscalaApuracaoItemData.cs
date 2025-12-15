using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class EscalaApuracaoItemData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public decimal? PercentualMinimo { get; set; }

        public decimal? PercentualMaximo { get; set; }

        public bool Aprovado { get; set; }
    }
}