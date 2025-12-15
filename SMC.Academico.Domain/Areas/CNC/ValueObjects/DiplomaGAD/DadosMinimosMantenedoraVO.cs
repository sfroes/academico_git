using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DadosMinimosMantenedoraVO : ISMCMappable
    {
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
    }
}
