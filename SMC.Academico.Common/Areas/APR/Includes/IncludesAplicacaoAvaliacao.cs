using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesAplicacaoAvaliacao
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Avaliacao = 1,

        [EnumMember]
        OrigemAvaliacao = 2,

        [EnumMember]
        MembrosBancaExaminadora = 3,

        [EnumMember]
        ApuracoesAvaliacao = 4
    }
}
