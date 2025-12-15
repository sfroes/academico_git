using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class GrupoEscalonamentoLookupListaViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [SMCHidden]
        public List<GrupoEscalonamentoItemLookupViewModel> Itens { get; set; }

        //TODO: Alterar em todas camadas
        public List<string> ItensDescricao { get => Itens?.Select(s => s.ItemGrupoEscalonamento).ToList(); }
    }
}