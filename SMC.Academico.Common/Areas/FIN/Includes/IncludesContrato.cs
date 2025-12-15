using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.FIN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesContrato : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        ArquivoAnexado = 1,

        [EnumMember]
        InstituicaoEnsino = 2,

        [EnumMember]
        Cursos = 4,

        [EnumMember]
        Cursos_Curso = 8, 

        [EnumMember]
        NiveisEnsino = 16,

        [EnumMember]
        NiveisEnsino_NivelEnsino = 32,

        [EnumMember]
        TermosAdesao = 64
    }
}

 