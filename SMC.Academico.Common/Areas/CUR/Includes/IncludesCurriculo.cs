using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCurriculo
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Curso = 1,

        [EnumMember]
        CursosOferta = 2,

        [EnumMember]
        GruposCurriculares = 4,

        [EnumMember]
        GruposCurriculares_CondicoesObrigatoriedade = 8
    }
}