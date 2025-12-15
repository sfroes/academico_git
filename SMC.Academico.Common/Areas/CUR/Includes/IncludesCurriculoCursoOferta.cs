using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCurriculoCursoOferta
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        CursoOferta = 1,

        [EnumMember]
        CursoOferta_Curso = 2,

        [EnumMember]
        CursoOferta_CursosOfertaLocalidade_HierarquiasEntidades_ItemSuperior = 4,

        [EnumMember]
        CursoOferta_CursosOfertaLocalidade_CursoUnidade_HierarquiasEntidades_ItemSuperior = 8,

        [EnumMember]
        CursoOferta_CursosOfertaLocalidade_Modalidade = 16,

        [EnumMember]
        GruposCurriculares = 32,

        [EnumMember]
        GruposCurriculares_GrupoCurricular_GruposCurricularesFilhos = 64,

        [EnumMember]
        GruposCurriculares_GrupoCurricular_ComponentesCurriculares_ComponenteCurricular_NiveisEnsino = 128,

        [EnumMember]
        GruposCurriculares_GrupoCurricular_TipoConfiguracaoGrupoCurricular = 256,

        [EnumMember]
        Curriculo = 512,

        [EnumMember]
        Curriculo_Curso = 1024,
    }
}