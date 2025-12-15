using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaTelefoneVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }

        public long SeqTelefone { get; set; }

        [SMCMapProperty("Telefone.CodigoPais")]
        public int CodigoPais { get; set; }

        [SMCMapProperty("Telefone.CodigoArea")]
        public int CodigoArea { get; set; }

        [SMCMapProperty("Telefone.Numero")]
        public string Numero { get; set; }

        [SMCMapProperty("Telefone.TipoTelefone")]
        public TipoTelefone TipoTelefone { get; set; }
    }
}
