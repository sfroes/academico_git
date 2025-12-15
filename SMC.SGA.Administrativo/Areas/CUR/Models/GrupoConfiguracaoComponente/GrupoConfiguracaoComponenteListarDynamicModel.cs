using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoConfiguracaoComponenteListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid4_24)]
        public bool Ativo { get; set; }

        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid4_24)]
        public TipoGrupoConfiguracaoComponente TipoGrupoConfiguracaoComponente { get; set; }

        [SMCDetail(SMCDetailType.Block, min: 2)]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<GrupoConfiguracaoComponenteListarItemViewModel> Itens { get; set; }
    }
}