using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoOfertaLocalidadeCabecalhoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        [SMCMapProperty("NomeCurso")]
        public string Curso { get; set; }

        [SMCMapProperty("NomeUnidade")]
        public string Unidade { get; set; }
    }
}