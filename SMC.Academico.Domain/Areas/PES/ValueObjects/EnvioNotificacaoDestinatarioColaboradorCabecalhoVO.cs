using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class EnvioNotificacaoDestinatarioColaboradorCabecalhoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public string CpfOuPassaporte { get; set; }

    }
}
