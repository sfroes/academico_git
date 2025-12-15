using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularLegadoData : ISMCMappable
    {
        public int CodigoComponenteLegado { get; set; }

        public string BancoLegado { get; set; }
    }
}