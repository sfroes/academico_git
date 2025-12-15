using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.DCT.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesColaboradorVinculo
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        ColaboradoresResponsaveis_ColaboradorResponsavel = 1,

        [EnumMember]
        Cursos_Atividades = 2,

        [EnumMember]
        Cursos_CursoOfertaLocalidade_HierarquiasEntidades_ItemSuperior_Entidade = 4,

        [EnumMember]
        EntidadeVinculo_TipoEntidade = 8,

        [EnumMember]
        FormacoesEspecificas = 16,

        [EnumMember]
        Colaborador_Professores = 32
    }
}