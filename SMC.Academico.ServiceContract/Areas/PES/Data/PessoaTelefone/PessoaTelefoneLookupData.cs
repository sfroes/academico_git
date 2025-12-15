using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaTelefoneLookupData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        [SMCMapProperty("SeqPessoa")]
        public long SeqPessoaTelefone { get; set; }

        public long SeqTelefone { get; set; }

        [SMCMapProperty("Telefone.TipoTelefone")]
        public TipoTelefone TipoTelefone { get; set; }

        [SMCMapProperty("Telefone.CodigoPais")]
        public int? CodigoPais { get; set; }

        [SMCMapProperty("Telefone.CodigoArea")]
        public int? CodigoArea { get; set; }

        [SMCMapProperty("Telefone.Numero")]
        public string Numero { get; set; }
    }
}