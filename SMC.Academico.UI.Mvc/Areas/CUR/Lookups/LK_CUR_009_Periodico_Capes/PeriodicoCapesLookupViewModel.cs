using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class PeriodicoCapesLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public string DescricaoAreaAvaliacao { get; set; }

        public QualisCapes QualisCapes { get; set; }

        [SMCDescription]
        public string Texto
        {
            get
            {
                if (Descricao != null && DescricaoAreaAvaliacao != null)
                {
                    return $"{Descricao.Trim()} - {DescricaoAreaAvaliacao.Trim()} - {QualisCapes}";
                }
                return null;
            }
        }
    }
}
