using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoUsoMensagem : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Declaração específica")]
        DeclaracaoEspecifica = 1,

        [EnumMember]
        Diploma = 2,

        [EnumMember]
        [Description("Histórico escolar")]
        HistoricoEscolar = 3
    }
}