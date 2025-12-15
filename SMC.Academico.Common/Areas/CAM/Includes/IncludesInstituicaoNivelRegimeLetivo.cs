using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoNivelRegimeLetivo
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        InstituicaoNivel = 1,

        [EnumMember]
        InstituicaoNivel_NivelEnsino = 2,

        [EnumMember]
        RegimeLetivo = 4
    }
}