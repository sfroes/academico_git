using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.TUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesDivisaoTurmaColaborador : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        DivisoesOrganizacao = 1 << 0,

        [EnumMember]
        DivisoesOrganizacao_DivisaoTurmaOrganizacao = 1 << 1,

    }
}
