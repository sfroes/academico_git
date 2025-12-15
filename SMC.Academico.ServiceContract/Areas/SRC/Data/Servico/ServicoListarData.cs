using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ServicoListarData : ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqTipoServico { get; set; }

        public string DescTipoServico { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public string Descricao { get; set; }

        public bool EsconderBotaoConsultarTaxas { get; set; }
    }
}
