using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConsultaPosicaoGeralData : ISMCMappable
    {
        public int QuantidadeSolicitacoesTotal { get; set; }

        public SMCPagerData<PosicaoConsolidadaListarData> Processos { get; set; }
    }
}