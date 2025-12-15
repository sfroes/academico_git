using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCursoFormacaoEspecifica : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Curso = 1,

        [EnumMember]
        FormacaoEspecifica = 2,

        [EnumMember]
        FormacaoEspecifica_CursoOfertaFormacao = 4,

        [EnumMember]
        FormacaoEspecifica_TipoFormacaoEspecifica = 8,

        [EnumMember]
        FormacaoEspecifica_CursosOfertas = 16,

        [EnumMember]
        Titulacoes = 32,

        [EnumMember]
        Titulacoes_Titulacao = 64,

        [EnumMember]
        GrauAcademico = 128
    }
}