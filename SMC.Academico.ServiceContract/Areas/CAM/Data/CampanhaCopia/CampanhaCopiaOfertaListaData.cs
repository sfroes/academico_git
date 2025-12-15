using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaCopiaOfertaListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string TipoOferta { get; set; }
        public string Oferta { get; set; }

        public int Vagas { get; set; }
    }
}