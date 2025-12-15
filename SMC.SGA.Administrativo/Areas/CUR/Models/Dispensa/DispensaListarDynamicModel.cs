using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DispensaListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        public List<DispensaComponenteCurricularViewModel> GrupoOrigens { get; set; }

        public List<DispensaComponenteCurricularViewModel> GrupoDispensados { get; set; }

        public MatrizExcecaoDispensa Associado { get; set; }

        public ModoExibicaoHistoricoEscolar ModoExibicaoHistoricoEscolar { get; set; }
    }
}