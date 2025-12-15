using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCurriculoCursoOfertaGrupo
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        GrupoCurricular = 1,

        [EnumMember]
        GrupoCurricular_TipoConfiguracaoGrupoCurricular = 2,
    }
}