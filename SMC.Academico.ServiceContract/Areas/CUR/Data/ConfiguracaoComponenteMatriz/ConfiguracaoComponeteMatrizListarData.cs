using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConfiguracaoComponeteMatrizListarData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        public long SeqMatrizCurricular { get; set; }
        public long SeqCurriculoCursoOferta { get; set; }
        public long SeqComponenteCurricular { get; set; }
        public string DescricaoComponente { get; set; }
        public List<DivisaoMatrizCurricularComponenteListaData> DivisaoMatrizCurricularComponentes { get; set; }
        public List<string> DescricoesGrupoCurricularComponente { get; set; }
    }
}
