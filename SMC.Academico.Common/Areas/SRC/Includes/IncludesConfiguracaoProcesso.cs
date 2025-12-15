using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesConfiguracaoProcesso
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 0,

        [EnumMember]
        Cursos = 1 << 1,

        [EnumMember]
        Cursos_CursoOfertaLocalidadeTurno = 1 << 2,

        [EnumMember]
        Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade = 1 << 3,

        [EnumMember]
        Cursos_CursoOfertaLocalidadeTurno_Turno = 1 << 4,

        [EnumMember]
        NiveisEnsino = 1 << 5,

        [EnumMember]
        NiveisEnsino_NivelEnsino = 1 << 6,

        [EnumMember]
        TiposVinculoAluno = 1 << 7,

        [EnumMember]
        TiposVinculoAluno_TipoVinculoAluno = 1 << 8,      
    }
}
