using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoOfertaLocalidadeCabecalhoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public string NomeCurso { get; set; }

        public string NomeUnidade { get; set; }
    }
}
