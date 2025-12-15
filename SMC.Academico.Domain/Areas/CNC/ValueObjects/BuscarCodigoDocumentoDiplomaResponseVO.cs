using SMC.Academico.Common.Areas.CNC.Models;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class BuscarCodigoDocumentoDiplomaResponseVO : MensagemHttp
    {
        public long SeqDocumentoAcademico { get; set; }
        public string VersaoDiploma { get; set; }
    }
}
