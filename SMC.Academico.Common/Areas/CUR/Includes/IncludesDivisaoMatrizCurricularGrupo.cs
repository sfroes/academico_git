using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesDivisaoMatrizCurricularGrupo
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        CurriculoCursoOfertaGrupo = 1,

        [EnumMember]
        DivisaoMatrizCurricular_DivisaoCurricularItem = 2,

        [EnumMember]
        CurriculoCursoOfertaGrupo_GrupoCurricular = 4,

        [EnumMember]
        CurriculoCursoOfertaGrupo_GrupoCurricular_TipoConfiguracaoGrupoCurricular = 8
    }
}