using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoCurricularComponenteListarData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string DescricaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public List<DivisaoMatrizCurricularComponenteListaData> DivisaoMatrizCurricularComponentes { get; set; }
    }
}
