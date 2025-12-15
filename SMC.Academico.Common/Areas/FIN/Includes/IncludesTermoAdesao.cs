using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.FIN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesTermoAdesao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TipoVinculoAluno = 1,

        [EnumMember]
        Contrato = 2,

        [EnumMember]
        Servico = 4
    }
}

 