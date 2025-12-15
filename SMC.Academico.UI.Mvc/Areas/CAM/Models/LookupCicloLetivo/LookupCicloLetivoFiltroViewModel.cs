using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Models
{
    public class LookupCicloLetivoFiltroViewModel : SMCLookupFilterViewModel
	{
        public long? Seq { get; set; }
        public short? Ano { get; set; }
        public short? Numero { get; set; }
        public long? SeqRegimeLetivo { get; set; }
        public string Descricao { get; set; }
    }
}