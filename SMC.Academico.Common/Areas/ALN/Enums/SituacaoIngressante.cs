using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoIngressante : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Aguardando liberação para matrícula")]
        AguardandoLiberacaMatricula = 1,

        [EnumMember]
        [Description("Apto para matrícula")]
        AptoMatricula = 2,

        [EnumMember]
        [Description("Cancelado (Prouni)")]
        Cancelado = 3,

        [EnumMember]
        Desistente = 4,

        [EnumMember]
        Matriculado = 5

    }
}