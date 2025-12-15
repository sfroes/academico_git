using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Localidades.Common.Areas.LOC.Enums;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaEnderecoFiltroLookupData : SMCPagerFilterData, ISMCMappable
    {
        [SMCKeyModel]
        public long[] Seqs { get; set; }

        public long SeqPessoa { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public TipoEndereco? TipoEndereco { get; set; }
    }
}