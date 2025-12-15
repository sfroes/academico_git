using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCursoOfertaLocalidadeTurno : int
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,
         
        [EnumMember]
        Turno = 1,
         
        [EnumMember]
        CursoOfertaLocalidade = 2,
         
        [EnumMember]
        CursoOfertaLocalidade_CursoOferta = 4
    }
}
