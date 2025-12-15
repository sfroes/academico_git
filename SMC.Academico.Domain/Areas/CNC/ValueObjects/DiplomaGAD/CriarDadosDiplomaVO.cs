using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class CriarDadosDiplomaVO : ISMCMappable
    {
        public DadosDiplomaVO DadosDiploma { get; set; }
        public DadosRegistroVO DadosRegistro { get; set; }
    }
}
