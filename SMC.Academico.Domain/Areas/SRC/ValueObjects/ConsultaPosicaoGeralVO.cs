using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConsultaPosicaoGeralVO : ISMCMappable
    {
        public int QuantidadeSolicitacoesTotal { get; set; }

        public SMCPagerData<PosicaoConsolidadaListarVO> Processos { get; set; }
    }
}