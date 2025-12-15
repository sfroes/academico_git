using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.MAT.Includes
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    [Flags]
    public enum IncludesSolicitacaoMatriculaItem
    {

        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        DivisaoTurma = 1,

        [EnumMember]
        HistoricosSituacao = 2,

        [EnumMember]
        SolicitacaoMatricula = 4,

        [EnumMember]
        SolicitacaoMatricula_PessoaAtuacao_Pessoa_DadosPessoais = 8,

        [EnumMember]
        DivisaoTurma_Turma_ConfiguracoesComponente = 16,

        [EnumMember]
        HistoricosSituacao_SituacaoItemMatricula = 32,

        [EnumMember]
        ConfiguracaoComponente = 64,
    }
}