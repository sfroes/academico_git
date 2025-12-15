using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoAtuacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Aluno")]
        Aluno = 1,

        [EnumMember]
        [Description("Professor/ Pesquisador")]
        Colaborador = 2,

        [EnumMember]
        [Description("Ingressante")]
        Ingressante = 3,

        [EnumMember]
        [Description("Funcionário")]
        Funcionario = 4
    }
}
