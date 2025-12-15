using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularDispensaFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long[] Seqs { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public bool? DisciplinaIsolada { get; set; }

        public long? SeqOfertaCurso { get; set; }
    }
}
