using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaConsultarCandidatoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long SeqCampanha { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqTipoProcessoSeletivo { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public long? SeqConvocacao { get; set; }

        public TipoChamada? TipoChamada { get; set; }

        public long? SeqChamada { get; set; }

        public string OfertaCampanha { get; set; }

        public bool? Exportado { get; set; }
    }
}