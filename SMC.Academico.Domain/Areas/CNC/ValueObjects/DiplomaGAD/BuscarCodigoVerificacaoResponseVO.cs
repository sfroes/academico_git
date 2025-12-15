using SMC.Academico.Common.Areas.CNC.Models;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class BuscarCodigoVerificacaoResponseVO : MensagemHttp
    {
        public string CodigoVerificacao { get; set; }
    }
}
