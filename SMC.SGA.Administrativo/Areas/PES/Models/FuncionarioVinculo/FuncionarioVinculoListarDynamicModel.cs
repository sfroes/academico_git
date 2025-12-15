using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class FuncionarioVinculoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        public long? SeqTipoFuncionario { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid16_24)]
        public string DescricaoTipoFuncionario { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public DateTime DataInicio { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public DateTime? DataFim { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCDescription]
        public string TipoEntidadeCadastrada { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public string DescricaoEntidadeCadastrada { get; set; }



    }
}