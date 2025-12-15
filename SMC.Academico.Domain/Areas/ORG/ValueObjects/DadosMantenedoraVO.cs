using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class DadosMantenedoraVO : ISMCMappable
    {
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public EnderecoVO Endereco { get; set; }
    }
}
