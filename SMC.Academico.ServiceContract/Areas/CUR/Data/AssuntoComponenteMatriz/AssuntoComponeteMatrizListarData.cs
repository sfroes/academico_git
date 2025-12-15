using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class AssuntoComponeteMatrizListarData : ISMCMappable
    {
        public long seqGrupoCurricularComponente { get; set; }
        public string DescricaoGrupoCurricular { get; set; }
        public long SeqDivisaoMatrizCurricularComponente { get; set; }
        public long SeqMatrizCurricular { get; set; }
        public long SeqCurriculoCursoOferta { get; set; }
        public List<ComponenteCurricularListaData> Assuntos { get; set; }

    }
}
