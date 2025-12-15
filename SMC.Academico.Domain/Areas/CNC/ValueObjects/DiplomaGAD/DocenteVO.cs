using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocenteVO : ISMCMappable
    {
        public string Nome { get; set; }
        public string Titulacao { get; set; } //enum Tecnologo, Graduacao, Especializacao, Mestrado, Doutorado
        public string Lattes { get; set; }
        public string Cpf { get; set; }
    }
}
