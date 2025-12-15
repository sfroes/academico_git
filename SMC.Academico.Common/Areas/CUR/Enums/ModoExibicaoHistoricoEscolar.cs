using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum ModoExibicaoHistoricoEscolar : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Aproveitamento de créditos")]
        AproveitamentoCreditos = 1,

        [EnumMember]
        [Description("Componentes Concluídos")]
        ComponentesConcluidos = 2
    }
}