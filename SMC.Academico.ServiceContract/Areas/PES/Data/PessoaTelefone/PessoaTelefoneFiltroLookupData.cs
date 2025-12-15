using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaTelefoneFiltroLookupData : SMCPagerFilterData, ISMCMappable
    {
        [SMCKeyModel]
        public long[] Seqs { get; set; }

        public long SeqPessoa { get; set; }

        public TipoTelefone? TipoTelefone { get; set; }
    }
}