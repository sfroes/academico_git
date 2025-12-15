using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class DownloadMaterialViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public TipoOrigemMaterial? TipoOrigemMaterial { get; set; }

        [SMCHidden]
        public TipoAtuacao? TipoAtuacao { get; set; }

        [SMCHidden]
        public string SeqsOrigemMaterial { get; set; }

        public List<SMCTreeViewNode<MaterialListarDynamicModel>> Materiais { get; set; }

        [SMCHidden]
        public long SeqOrigemMaterial { get; set; }
    }
}