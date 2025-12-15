using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class AssuntoComponeteMatrizListarVO : ISMCMappable
    {
        public long seqGrupoCurricularComponente { get; set; }
        public string DescricaoGrupoCurricular { get; set; }
        public long SeqDivisaoMatrizCurricularComponente { get; set; }
        public long SeqMatrizCurricular { get; set; }
        public long SeqCurriculoCursoOferta { get; set; }
        public List<ComponenteCurricularListaVO> Assuntos { get; set; }
    }
}
