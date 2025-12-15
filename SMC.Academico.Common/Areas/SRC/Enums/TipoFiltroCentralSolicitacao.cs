using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoFiltroCentralSolicitacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Etapa/Situação atual da solicitação")]
        EtapaSituacaoAtualSolicitacao = 1,

        [EnumMember]
        [Description("Situação da etapa selecionada")]
        SituacaoEtapaSelecionada = 2
    }
}