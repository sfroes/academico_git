using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class HierarquiaEntidadeListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCSortable(true, false)]
        public override long Seq { get; set; }

        [SMCInclude("TipoHierarquiaEntidade")]
        [SMCMapProperty("TipoHierarquiaEntidade.Descricao")]
        [SMCOrder(2)]
        [SMCSortable(true, false, "TipoHierarquiaEntidade.Descricao")]
        public string DescricaoTipoHierarquiaEntidade { get; set; }

        [SMCDescription]
        [SMCOrder(1)]
        [SMCSortable(true, false)]
        public string Descricao { get; set; }

        [SMCOrder(3)]
        [SMCSortable(true, false)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCOrder(4)]
        public DateTime? DataFimVigencia { get; set; }

        [SMCIgnoreProp]
        public long SeqTipoHierarquiaEntidade { get; set; }
    }
}