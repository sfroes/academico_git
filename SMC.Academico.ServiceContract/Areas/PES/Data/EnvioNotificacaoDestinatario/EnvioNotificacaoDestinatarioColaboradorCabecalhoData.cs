using SMC.Framework.Mapper;
using SMC.Framework;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class EnvioNotificacaoDestinatarioColaboradorCabecalhoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public string CpfOuPassaporte { get; set; }

    }
}
