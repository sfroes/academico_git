using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class IntegralizacaoConsultaHistoricoFiltroVO : ISMCMappable
    {    
        public string FiltroDescricaoConfiguracao { get; set; }

        public SituacaoComponenteIntegralizacao? FiltroSituacaoConfiguracao { get; set; }
    }
}
