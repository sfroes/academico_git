using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoOfertaLocalidadeCabecalhoVO : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public string NomeCurso { get; set; }

        public string NomeUnidade { get; set; }
    }
}
