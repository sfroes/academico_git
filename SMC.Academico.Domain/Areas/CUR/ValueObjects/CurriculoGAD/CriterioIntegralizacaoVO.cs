using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class CriterioIntegralizacaoVO : ISMCMappable
    {
        public string Tipo { get; set; } 
        public RotulosIntegralizacaoVO Rotulos { get; set; }
        public ExpressaoIntegralizacaoVO Expressao { get; set; }
    }
}
