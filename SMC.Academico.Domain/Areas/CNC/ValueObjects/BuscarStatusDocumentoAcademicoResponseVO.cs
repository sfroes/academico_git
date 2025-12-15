using SMC.Academico.Common.Areas.CNC.Models;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class BuscarStatusDocumentoAcademicoResponseVO : MensagemHttp
    {
        public string StatusDocumentoAcademico { get; set; }
    }
}
