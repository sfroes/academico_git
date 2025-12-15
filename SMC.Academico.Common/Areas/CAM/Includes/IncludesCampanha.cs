using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCampanha
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        CiclosLetivos_CicloLetivo = 1 << 0,

        [EnumMember]
        ProcessosSeletivos = 1 << 1,

        [EnumMember]
        ProcessosSeletivos_TipoProcessoSeletivo = 1 << 2,

        [EnumMember]
        ProcessosSeletivos_Convocacoes = 1 << 3,

        [EnumMember]
        CiclosLetivos = 1 << 4,

        [EnumMember]
        ProcessosSeletivos_Convocacoes_Chamadas_Convocados_Ofertas = 1 << 5,

        [EnumMember]
        ProcessosSeletivos_Ofertas = 1 << 6,

        [EnumMember]
        Ofertas = 1 << 7,

        [EnumMember]
        EntidadeResponsavel_TipoEntidade = 1 << 8,

        [EnumMember]
        ProcessosSeletivos_Convocacoes_Ofertas = 1 << 9,

    }
}