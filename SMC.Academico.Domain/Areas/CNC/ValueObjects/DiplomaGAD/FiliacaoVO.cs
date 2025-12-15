using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class FiliacaoVO : ISMCMappable
    {
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public string Sexo { get; set; } //enum F, M
    }
}
