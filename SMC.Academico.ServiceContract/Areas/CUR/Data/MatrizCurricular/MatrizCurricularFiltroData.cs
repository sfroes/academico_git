using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCurso { get; set; }

        public long? SeqCurriculo { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqCurriculoCursoOferta { get; set; }

        public long? SeqModalidade { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }
    }
}
