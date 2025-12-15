using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class ConcedenteEstagioVO : ISMCMappable
    {
        public string Tipo { get; set; } // enum PessoaJuridica, PessoaFisica
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }
}