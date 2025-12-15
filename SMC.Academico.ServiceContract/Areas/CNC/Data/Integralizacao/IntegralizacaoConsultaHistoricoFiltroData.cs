using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class IntegralizacaoConsultaHistoricoFiltroData : ISMCMappable
    {
        public string FiltroDescricaoConfiguracao { get; set; }

        public SituacaoComponenteIntegralizacao? FiltroSituacaoConfiguracao { get; set; }
    }
}
