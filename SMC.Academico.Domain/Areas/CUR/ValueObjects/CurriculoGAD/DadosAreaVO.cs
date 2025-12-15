using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DadosAreaVO : ISMCMappable
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
    }
}
