using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoChamada : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Aguardando carga de ingressantes")]
        AguardandoCargaIngressantes = 1,

        [EnumMember]
        [Description("Aguardando liberação para matrícula")]
        AguardandoLiberacaoParaMatricula = 2,

        [EnumMember]
        [Description("Chamada encerrada")]
        ChamadaEncerrada = 3,

        [EnumMember]
        [Description("Aguardando execução")]
        AguardandoExecucao = 4,

        [EnumMember]
        [Description("Em execução")]
        EmExecucao = 5

    }
}