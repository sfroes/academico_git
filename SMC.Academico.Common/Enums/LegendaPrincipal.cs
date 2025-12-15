using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum LegendaPrincipal : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Configuração Principal")]
        [SMCLegendItem("smc-sga-legenda-configuracao-principal", "Configuração Principal" )]
        Principal = 1
    }
}
