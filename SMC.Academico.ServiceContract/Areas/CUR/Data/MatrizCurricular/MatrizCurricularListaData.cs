using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoUnidade { get; set; }
                
        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public List<MatrizCurricularOfertaData> Ofertas { get; set; }

        public bool ContemOfertaAtiva { get; set; }

        public long SeqMatrizCurricular { get; set; }

        public long SeqEntidadeLocalidade { get; set; }

        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoComplementarMatrizCurricular { get; set; }
    }
}
