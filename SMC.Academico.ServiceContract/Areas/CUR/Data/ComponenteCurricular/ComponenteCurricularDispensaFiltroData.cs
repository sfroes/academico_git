using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularDispensaFiltroData : SMCPagerFilterData, ISMCMappable
    {   
        [SMCKeyModel]
        public long[] Seqs { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public bool? DisciplinaIsolada { get; set; }

        public long? SeqOfertaCurso { get; set; }
    }
}
