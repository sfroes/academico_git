using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class GrupoCurricularComponenteListarVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string DescricaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public List<DivisaoMatrizCurricularComponenteListarVO> DivisaoMatrizCurricularComponentes { get; set; }
    }
}
