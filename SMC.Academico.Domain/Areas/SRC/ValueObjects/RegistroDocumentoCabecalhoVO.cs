using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class RegistroDocumentoCabecalhoVO : ISMCMappable
    {
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public string Cpf { get; set; }
        public string NumeroPassaporte { get; set; }
        public string DescricaoProcesso { get; set; }
    }
}