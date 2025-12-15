using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class TermoResponsabilidadeVO : ISMCMappable
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cargo { get; set; }
        public string AtoDesignacao { get; set; } //Conteúdo do arquivo em formato Base 64.
    }
}
