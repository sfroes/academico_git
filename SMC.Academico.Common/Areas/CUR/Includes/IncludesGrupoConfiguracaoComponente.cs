using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesGrupoConfiguracaoComponente
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Itens = 1,

        [EnumMember]
        Itens_ConfiguracaoComponente = 2,

        [EnumMember]
        Itens_ConfiguracaoComponente_ComponenteCurricular = 4,

        [EnumMember]
        Itens_ConfiguracaoComponente_ComponenteCurricular_NiveisEnsino = 8,

        [EnumMember]
        Itens_ConfiguracaoComponente_ComponenteCurricular_EntidadesResponsaveis_Entidade = 16,
    }
}