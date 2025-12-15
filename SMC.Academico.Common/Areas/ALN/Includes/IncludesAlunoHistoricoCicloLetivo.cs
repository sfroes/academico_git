using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesAlunoHistoricoCicloLetivo : int
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 1 << 00,

        [EnumMember]
        AlunoHistoricoSituacao = 1 << 01,

    }
}
