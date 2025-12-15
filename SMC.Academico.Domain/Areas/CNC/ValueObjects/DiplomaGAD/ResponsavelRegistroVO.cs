using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class ResponsavelRegistroVO : ISMCMappable
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string IDouNumeroMatricula { get; set; }
    }
}
