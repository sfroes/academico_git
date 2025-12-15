using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaCopiaOfertaListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string TipoOferta { get; set; }

        public string Oferta { get; set; }

        public int Vagas { get; set; }
    }
}