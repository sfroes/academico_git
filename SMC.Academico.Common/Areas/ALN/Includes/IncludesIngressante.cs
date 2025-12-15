using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesIngressante : long
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        FormaIngresso = 1L << 00,

        [EnumMember]
        TipoVinculoAluno = 1L << 01,

        [EnumMember]
        MatrizCurricularOferta = 1L << 02,

        [EnumMember]
        CampanhaCicloLetivo = 1L << 03,

        [EnumMember]
        EntidadeResponsavel = 1L << 04,

        [EnumMember]
        GrupoEscalonamento = 1L << 05,

        [EnumMember]
        NivelEnsino = 1L << 06,

        [EnumMember]
        ProcessoSeletivo = 1L << 07,

        [EnumMember]
        FormacoesEspecificas = 1L << 08,

        [EnumMember]
        Ofertas = 1L << 09,

        [EnumMember]
        HistoricosSituacao = 1L << 10,

        [EnumMember]
        Pessoa = 1L << 11,

        [EnumMember]
        DadosPessoais = 1L << 12,

        [EnumMember]
        DadosPessoais_ArquivoFoto = 1L << 13,

        [EnumMember]
        Pessoa_Filiacao = 1L << 14,

        [EnumMember]
        Enderecos = 1L << 15,

        [EnumMember]
        Enderecos_PessoaEndereco = 1L << 16,

        [EnumMember]
        Enderecos_PessoaEndereco_Endereco = 1L << 17,

        [EnumMember]
        Telefones = 1L << 18,

        [EnumMember]
        Telefones_Telefone = 1L << 19,

        [EnumMember]
        EnderecosEletronicos = 1L << 20,

        [EnumMember]
        EnderecosEletronicos_EnderecoEletronico = 1L << 21,

        [EnumMember]
        Beneficios = 1L << 22,

        [EnumMember]
        Beneficios_ResponsaveisFinanceiro = 1L << 23,

        [EnumMember]
        Beneficios_ResponsaveisFinanceiro_PessoaJuridica = 1L << 24,

        [EnumMember]
        Ofertas_CampanhaOfertaItem = 1L << 25,

        [EnumMember]
        Ofertas_CampanhaOfertaItem_CursoOfertaLocalidadeTurno = 1L << 26,

        [EnumMember]
        Ofertas_CampanhaOfertaItem_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade = 1L << 27,

        [EnumMember]
        Ofertas_CampanhaOfertaItem_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_Modalidade = 1L << 28,

        [EnumMember]
        Ofertas_CampanhaOfertaItem_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_CursoOferta = 1L << 29,

        [EnumMember]
        Ofertas_CampanhaOfertaItem_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_CursoOferta_Curso = 1L << 30,

        [EnumMember]
        TermosIntercambio_Orientacao_TipoOrientacao = 1L << 31,

        [EnumMember]
        CondicoesObrigatoriedade = 1L << 32,

        [EnumMember]
        FormacoesEspecificas_FormacaoEspecifica = 1L << 33,

        [EnumMember]
        Ofertas_CampanhaOfertaItem_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_CursoOferta_Curso_TipoEntidade = 1L << 34,

        [EnumMember]
        MatrizCurricularOferta_CursoOfertaLocalidadeTurno = 1L << 35,

        [EnumMember]
        MatrizCurricularOferta_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade = 1L << 36,

        [EnumMember]
        Convocado = 1L << 37,

        [EnumMember]
        CampanhaCicloLetivo_CicloLetivo = 1L << 38,

        [EnumMember]
        CampanhaCicloLetivo_Campanha = 1L << 39,

        [EnumMember]
        TermosIntercambio_Orientacao_OrientacoesColaborador = 1L << 40,

        [EnumMember]
        SolicitacoesServico = 1L << 41,

        [EnumMember]
        CondicoesObrigatoriedade_CondicaoObrigatoriedade = 1L << 42,

        [EnumMember]
        Ofertas_CampanhaOferta = 1L << 43,

        [EnumMember]
        OrientacoesPessoaAtuacao_Orientacao_OrientacoesColaborador = 1L << 44,

        [EnumMember]
        OrientacoesPessoaAtuacao_Orientacao_OrientacoesColaborador_Colaborador_DadosPessoais = 1L << 45,

        [EnumMember]
        TermosIntercambio_TermoIntercambio_ParceriaIntercambioTipoTermo_TipoTermoIntercambio = 1L << 46,

        [EnumMember]
        TermosIntercambio_TermoIntercambio_Vigencias = 1L << 47,

        [EnumMember]
        TermosIntercambio_TermoIntercambio_ParceriaIntercambioInstituicaoExterna = 1L <<48
    }
}