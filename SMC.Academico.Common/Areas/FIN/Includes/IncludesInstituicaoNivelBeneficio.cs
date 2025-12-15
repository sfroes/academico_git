using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.FIN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoNivelBeneficio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        BeneficiosHistoricosValoresAuxilio = 1,

        [EnumMember]
        ConfiguracoesBeneficio = 2,

        [EnumMember]
        Beneficio = 4,

        [EnumMember]
        InstituicaoNivel_NivelEnsino = 8
    }
}
