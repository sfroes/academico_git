using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoUnidadeListaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTipoEntidade { get; set; }

        public string NomeUnidade { get; set; }

        public string Nome { get; set; }

        public List<CursoOfertaLocalidadeListaData> CursosOfertaLocalidade { get; set; }
    }
}
