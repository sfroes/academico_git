using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesParceriaIntercambioInstituicaoExterna
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0, 

        [EnumMember]
        InstituicaoExterna = 1, 

    }
}

 