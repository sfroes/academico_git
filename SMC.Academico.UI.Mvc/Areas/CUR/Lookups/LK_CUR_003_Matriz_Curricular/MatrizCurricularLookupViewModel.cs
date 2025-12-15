using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class MatrizCurricularLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public List<MatrizCurricularOfertasLookupViewModel> Ofertas { get; set; }

        [SMCHidden]
        public bool ContemOfertaAtiva { get; set; }
    }
}
