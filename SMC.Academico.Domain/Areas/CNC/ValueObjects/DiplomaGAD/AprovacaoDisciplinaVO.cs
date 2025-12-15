using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class AprovacaoDisciplinaVO : ISMCMappable
    {
        public string FormaIntegralizacao { get; set; } //enum Cursado, Validado, Aproveitado
        public string OutraFormaIntegralizacao { get; set; }
    }
}