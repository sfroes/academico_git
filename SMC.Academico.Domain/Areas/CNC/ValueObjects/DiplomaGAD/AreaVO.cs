using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class AreaVO : ISMCMappable
    {
        public string Codigo { get; set; } // O elemento Codigo deve remeter ao código especificado no curriculo
        public string Nome { get; set; }
    }
}
