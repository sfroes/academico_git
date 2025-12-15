using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesDivisaoCurricular
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        InstituicaoEnsino = 1,

        [EnumMember]
        NivelEnsino = 2,

        [EnumMember]
        RegimeLetivo = 4,

        [EnumMember]
        TipoDivisaoCurricular = 8,

        [EnumMember]
        Itens = 16
    }
}