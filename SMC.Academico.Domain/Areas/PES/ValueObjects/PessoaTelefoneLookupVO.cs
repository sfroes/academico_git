using SMC.DadosMestres.Common.Areas.PES.Enums;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaTelefoneLookupVO
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }

        public long SeqTelefone { get; set; }

        public int CodigoPais { get; set; }
        
        public int CodigoArea { get; set; }
       
        public string Numero { get; set; }
        
        public TipoTelefone TipoTelefone { get; set; }
    }
}
