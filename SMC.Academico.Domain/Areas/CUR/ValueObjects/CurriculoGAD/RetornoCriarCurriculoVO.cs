using SMC.Academico.Common.Areas.CNC.Models;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class RetornoCriarCurriculoVO : MensagemHttp
    {
        public long SeqDocumentoCurriculo { get; set; }
    }
}
