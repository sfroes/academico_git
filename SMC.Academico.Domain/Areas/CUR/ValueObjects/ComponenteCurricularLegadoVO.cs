using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularLegadoVO : ISMCMappable
    {
        public int CodigoComponenteLegado { get; set; }

        public string BancoLegado { get; set; }
    }
}