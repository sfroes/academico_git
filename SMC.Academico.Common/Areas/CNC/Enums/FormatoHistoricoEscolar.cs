using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum FormatoHistoricoEscolar : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        [Description("Não informar")]
        NaoInformar = 1,

        [EnumMember]
        [Description("Informar sem matriz curricular")]
        InformarSemMatrízCurricular = 2,

        [EnumMember]
        [Description("Informar com matriz curricular")]
        InformarComMatrizCurricular = 3
    }
}