using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.MAT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum MotivoSituacaoMatricula : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Pela instituição")]
        PelaInstituicao = 1,

        [EnumMember]
        [Description("Pelo solicitante")]
        PeloSolicitante = 2,

        [EnumMember]
        [Description("Exigência de requisitos não satisfeitos")]
        ExigenciaRequisitosNaoSatisfeitos = 3,

        [EnumMember]
        [Description("Existência de bloqueio")]
        ExistenciaBloqueio = 4,

        [EnumMember]
        [Description("Item cancelado")]
        ItemCancelado = 5,

        [EnumMember]
        [Description("Vagas excedidas")]
        VagasExcedidas = 6,

        [EnumMember]
        [Description("Por indeferimento")]
        PorIndeferimento = 7,

        [EnumMember]
        [Description("Etapa não finalizada")]
        EtapaNaoFinalizada = 8,

        [EnumMember]
        [Description("Solicitação cancelada")]
        SolicitacaoCancelada = 9,

        [EnumMember]
        [Description("Por troca de grupo")]
        PorTrocaDeGrupo = 10,

        [EnumMember]
        [Description("Por dispensa/aprovação")]
        PorDispensaAprovacao = 11
    }
}