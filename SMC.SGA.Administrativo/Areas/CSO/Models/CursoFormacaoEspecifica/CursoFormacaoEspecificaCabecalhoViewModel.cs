using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoFormacaoEspecificaCabecalhoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        [SMCMapProperty("Nome")]
        public string NomeCurso { get; set; }

        [SMCMapProperty("NivelEnsino.Descricao")]
        public string DescricaoNivelEnsino { get; set; }
    }
}