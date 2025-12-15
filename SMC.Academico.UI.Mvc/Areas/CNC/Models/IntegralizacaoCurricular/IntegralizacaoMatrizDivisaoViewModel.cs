using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Models
{
    public class IntegralizacaoMatrizDivisaoViewModel : SMCViewModelBase
    {
        public short NumeroDivisao { get; set; }

        public string DescricaoDivisao { get; set; }

        public List<IntegralizacaoMatrizGrupoViewModel> Grupos { get; set; }
    }
}
