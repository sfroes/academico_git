using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum AcaoLiberacaoTrabalho : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Abortar processo se existir solicitação não finalizada")]
        AbortarProcessoSeExistirSolicitacaoNaoFinalizada = 1,

        [EnumMember]
        [Description("Cancelar solicitações não finalizadas")]
        CancelarSolicitacoesNaoFinalizadas = 2

    }
}