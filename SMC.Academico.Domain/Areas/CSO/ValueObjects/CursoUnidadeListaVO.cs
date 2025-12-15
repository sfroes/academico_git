using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoUnidadeListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoEntidade { get; set; }

        [SMCMapMethod("RecuperarNomeUnidade")]
        public string NomeUnidade { get; set; }

        public string Nome { get; set; }

        public List<CursoOfertaLocalidadeListaVO> CursosOfertaLocalidade { get; set; }
    }
}