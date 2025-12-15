using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.ORG.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoNivelCalendario
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TiposEvento = 2,

        [EnumMember]
        InstituicaoNivel = 4,

        [EnumMember]
        InstituicaoNivel_InstituicaoEnsino = 8,

        [EnumMember]
        InstituicaoNivel_NivelEnsino = 16

    }
}
