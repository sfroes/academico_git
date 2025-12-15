using SMC.Academico.Common.Areas.CNC.Models;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class RetornoCriarHistoricoVO : MensagemHttp
    {
        public long SeqDocumentoHistorico { get; set; } 
    }
}
