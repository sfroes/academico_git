using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoCurricularComponenteListarItemViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string DescricaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public List<DivisaoMatrizCurricularComponenteListarItemViewModel> DivisaoMatrizCurricularComponentes { get; set; }
    }
}