using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoConclusao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Componentes Concluídos")]
        ComponenteConcluido = 1,

        [EnumMember]
        [Description("Aproveitamento de Créditos")]
        AproveitamentoCredito = 2,

        [EnumMember]
        [Description("Componentes Sem Apuração")]
        CursadoSemHistoricoEscolar = 3,

        [EnumMember]
        Exame = 4
    }
}
