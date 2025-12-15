using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ServicoListarVO : ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqTipoServico { get; set; }

        public string DescTipoServico { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public string Descricao { get; set; }

        public bool EsconderBotaoConsultarTaxas { get; set; }
    }
}
