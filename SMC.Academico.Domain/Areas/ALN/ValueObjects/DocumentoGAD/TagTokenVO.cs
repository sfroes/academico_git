using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class TagTokenVO : ISMCMappable
    {
        public string Token { get; set; }
        public string Descricao { get; set; }
    }
}
