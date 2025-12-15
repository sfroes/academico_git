using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class MantenedoraVO : ISMCMappable
    {
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public EnderecoVO Endereco { get; set; }
    }
}
