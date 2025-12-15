using SMC.Academico.Common.Areas.CNC.Models;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class BuscarManifestoAssinaturaDiplomaResponseViewModel : MensagemHttp
    {
        public string Nome { get; set; }
        public byte[] Conteudo { get; set; }
        public string Tipo { get; set; }
    }
}