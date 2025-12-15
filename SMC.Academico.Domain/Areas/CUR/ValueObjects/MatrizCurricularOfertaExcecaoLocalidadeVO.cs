using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class MatrizCurricularOfertaExcecaoLocalidadeVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqMatrizCurricularOferta { get; set; }

        public long SeqEntidadeLocalidade { get; set; }

        public string DescricaoLocalidade { get; set; }
    }
}
