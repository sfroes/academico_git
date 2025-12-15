using System;
using SMC.Academico.Common.Constants;
using System.Runtime.Serialization;
using SMC.Framework;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCursoOfertaLocalidadeFormacao : int
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        FormacaoEspecifica = 1,

        [EnumMember]
        CursoOfertaLocalidade = 2,

        [EnumMember]
        CursoOfertaLocalidade_CursoOferta = 4,

        [EnumMember]
        FormacaoEspecifica_Cursos = 8,

        [EnumMember]
        FormacaoEspecifica_Cursos_GrauAcademico = 16        
    }
}
