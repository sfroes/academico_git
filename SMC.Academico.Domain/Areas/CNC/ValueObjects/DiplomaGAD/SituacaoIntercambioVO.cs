using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SituacaoIntercambioVO : ISMCMappable
    {
        public string Instituicao { get; set; }
        public string Pais { get; set; }
        public string NomeProgramaIntercambio { get; set; }
    }
}
