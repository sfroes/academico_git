using SMC.Academico.Common.Areas.CNC.Models;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class RetornoCriarDiplomaVO : MensagemHttp
    {
        public long SeqDocumentoDiploma { get; set; }

        public long? SeqDocumentoHistorico { get; set; }
    }
}
