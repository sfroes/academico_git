using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesFormacaoEspecifica : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TipoFormacaoEspecifica = 1,

        [EnumMember]
        FormacoesEspecificasFilhas_TipoFormacaoEspecifica = 2,

        [EnumMember]
        Cursos_GrauAcademico = 4
    }
}