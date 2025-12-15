using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesPlanoEstudoItem : int
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 1 << 00,

        [EnumMember]
        PlanoEstudo = 1 << 01,

        [EnumMember]
        DivisaoTurma = 1 << 02,

        [EnumMember]
        ConfiguracaoComponente = 1 << 03,

        [EnumMember]
        Orientacao = 1 << 04,

        [EnumMember]
        PlanoEstudo_AlunoHistoricoCicloLetivo = 1 << 05,

        [EnumMember]
        PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico = 1 << 06,

        [EnumMember]
        PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_Aluno = 1 << 07,

        [EnumMember]
        PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_Aluno_DadosPessoais = 1 << 08,

        [EnumMember]
        Orientacao_OrientacoesColaborador = 1 << 09,

        [EnumMember]
        Orientacao_OrientacoesColaborador_Colaborador = 1 << 10,

        [EnumMember]
        Orientacao_OrientacoesColaborador_Colaborador_DadosPessoais = 1 << 11,

        [EnumMember]
        PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_CursoOfertaLocalidadeTurno = 1 << 12,

        [EnumMember]
        PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade = 1 << 13,

        [EnumMember]
        PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades = 1 << 14,

        [EnumMember]
        PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades_ItemSuperior = 1 << 15,

        [EnumMember]
        DivisaoTurma_Turma_ConfiguracoesComponente = 1 << 16,

        [EnumMember]
        PlanoEstudo_SolicitacaoMatricula_PessoaAtuacao_Pessoa_DadosPessoais = 1 << 17,

        [EnumMember]
        PlanoEstudo_SolicitacaoServico = 1 << 18,

        [EnumMember]
        PlanoEstudo_SolicitacaoServico_PessoaAtuacao_Pessoa_DadosPessoais = 1 << 19,
    }
}