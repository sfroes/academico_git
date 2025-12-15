using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class CurriculoCursoOfertaGrupoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqAluno { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqComponenteCurricular { get; set; }
    }
}