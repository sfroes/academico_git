using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaCicloLetivoData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("CicloLetivo.Ano")]
        public short AnoCicloLetivo { get; set; }

        [SMCMapProperty("CicloLetivo.Numero")]
        public short NumeroCicloLetivo { get; set; }

        [SMCMapProperty("CicloLetivo.Descricao")]
        public string Descricao { get; set; }
    }
}