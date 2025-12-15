using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ConfiguracaoComponeteMatrizListarVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        public long SeqMatrizCurricular { get; set; }
        public long SeqCurriculoCursoOferta { get; set; }
        public long SeqComponenteCurricular { get; set; }
        public string DescricaoComponente { get; set; }
        public List<DivisaoMatrizCurricularComponenteListarVO> DivisaoMatrizCurricularComponentes { get; set; }
        public List<string> DescricoesGrupoCurricularComponente { get; set; }
    }
}
