using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum OrigemSolicitacaoServico : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Automático/JOB")]
        Automatico = 1,

        [EnumMember]
        [Description("Portal Aluno")]
        PortalAluno = 2,

        [EnumMember]
        Presencial = 3
    }
}