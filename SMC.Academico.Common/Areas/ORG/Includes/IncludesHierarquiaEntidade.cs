using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesHierarquiaEntidade
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TipoHierarquiaEntidade = 1,

        [EnumMember]
        InstituicaoEnsino = 2,

        [EnumMember]
        Itens = 4,
    }
}
