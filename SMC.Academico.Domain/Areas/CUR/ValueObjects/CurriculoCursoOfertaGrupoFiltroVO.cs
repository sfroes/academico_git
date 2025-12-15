using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class CurriculoCursoOfertaGrupoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqAluno { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqCurriculo { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public long? SeqGrupoCurricular { get; set; }
    }
}