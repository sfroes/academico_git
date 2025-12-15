using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCampanhaOferta
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TipoOferta = 1,

        [EnumMember]
        Itens = 2,

        [EnumMember]
        Itens_Turma = 3,

        [EnumMember]
        Itens_Turma_DivisoesTurma = 4,

        [EnumMember]
        Campanha = 5,
       
        [EnumMember]
        Campanha_EntidadeResponsavel = 6,

        [EnumMember]
        Campanha_EntidadeResponsavel_TipoEntidade = 7,

        [EnumMember]
        Campanha_CiclosLetivos = 8,

        [EnumMember]
        Campanha_CiclosLetivos_CicloLetivo = 9,
    }
}