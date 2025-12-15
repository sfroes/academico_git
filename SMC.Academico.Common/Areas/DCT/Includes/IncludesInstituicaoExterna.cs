using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.DCT.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoExterna
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        CategoriaInstituicaoEnsino = 1,
    }
}