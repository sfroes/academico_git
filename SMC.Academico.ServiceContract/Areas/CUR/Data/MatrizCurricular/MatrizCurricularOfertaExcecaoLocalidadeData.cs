using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularOfertaExcecaoLocalidadeData : ISMCMappable
    {
        public long Seq { get; set; }
        
        public long SeqMatrizCurricularOferta { get; set; }
        
        public long SeqEntidadeLocalidade { get; set; }
       
        public string DescricaoLocalidade { get; set; }
    }
}
