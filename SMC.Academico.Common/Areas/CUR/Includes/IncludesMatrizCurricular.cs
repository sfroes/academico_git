using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesMatrizCurricular
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        DivisoesMatrizCurricular = 1,

        [EnumMember]
        ConfiguracoesComponente = 2,

        [EnumMember]
        Ofertas = 4,

        [EnumMember]
        Ofertas_CursoOfertaLocalidadeTurno = 8,

        [EnumMember]
        Ofertas_CursoOfertaLocalidadeTurno_Turno = 16,

        [EnumMember]
        CurriculoCursoOferta_GruposCurriculares_GrupoCurricular = 32,

        [EnumMember]
        CurriculoCursoOferta_GruposCurriculares_GrupoCurricular_ComponentesCurriculares = 64,

        [EnumMember]
        CurriculoCursoOferta_GruposCurriculares_GrupoCurricular_ComponentesCurriculares_ComponenteCurricular = 128,

        [EnumMember]
        DivisoesMatrizCurricular_DivisaoCurricularItem = 256,

        [EnumMember]
        Ofertas_HistoricosSituacao = 512,

        [EnumMember]
        Ofertas_ExcecoesLocalidade = 1024,

        [EnumMember]
        ConfiguracoesComponente_GrupoCurricularComponente = 2048,

        [EnumMember]
        CurriculoCursoOferta = 4096,

        [EnumMember]
        CurriculoCursoOferta_CursoOfertaLocalidadeTurno = 8192,

        [EnumMember]
        CurriculoCursoOferta_Curriculo = 16384
    }
}