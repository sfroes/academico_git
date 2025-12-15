using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesAtoNormativoEntidade : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Entidade = 1,

        [EnumMember]
        GrauAcademico = 2,

        [EnumMember]
        Entidade_TipoEntidade = 4
    }
}
