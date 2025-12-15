using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class GrupoEscalonamentoItemLookupViewModel : SMCViewModelBase, ISMCMappable
    {
        public string ItemGrupoEscalonamento
        {
            get
            {
                if (DataFim.HasValue)
                {
                    return DescricaoEtapa + " (" + DataInicio.ToString("d") + " a " + DataFim.Value.ToString("d") + ")";
                }
                else
                {
                    return DescricaoEtapa + " (" + DataInicio.ToString("d") + ")";
                }
            }
        }

        [SMCHidden]
        public string DescricaoEtapa { get; set; }

        [SMCHidden]
        public DateTime DataInicio { get; set; }

        [SMCHidden]
        public DateTime? DataFim { get; set; }
    }
}