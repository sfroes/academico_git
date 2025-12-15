using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.DCT.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesColaboradorVinculoCurso
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 0,

        [EnumMember]
        CursoOfertaLocalidade = 1 << 1,

        [EnumMember]
        ColaboradorVinculo = 1 << 2,

        [EnumMember]
        Atividades = 1 << 3,
    }
}