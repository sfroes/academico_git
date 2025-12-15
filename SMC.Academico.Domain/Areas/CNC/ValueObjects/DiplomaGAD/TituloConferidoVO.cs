using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class TituloConferidoVO : ISMCMappable
    {
        public string Titulo { get; set; } //enum Licenciado, Tecnólogo, Bacharel, Médico, Técnico, Psicólogo
        public string OutroTitulo { get; set; }
    }
}
