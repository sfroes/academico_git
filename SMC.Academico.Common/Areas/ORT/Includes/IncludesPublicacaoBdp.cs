using SMC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.ORT.Includes
{
    [Flags]
    public enum IncludesPublicacaoBdp
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 0,

        [EnumMember]
        HistoricoSituacoes = 1 << 1,

        [EnumMember]
        InformacoesIdioma = 1 << 2,

        [EnumMember]
        InformacoesIdioma_PalavrasChave = 1 << 3,

        [EnumMember]
        TrabalhoAcademico = 1 << 4,

        [EnumMember]
        TrabalhoAcademico_Autores = 1 << 5,

        [EnumMember]
        TrabalhoAcademico_TipoTrabalho = 1 << 6,

        [EnumMember]
        TrabalhoAcademico_DivisoesComponente = 1 << 7,
        
        [EnumMember]
        TrabalhoAcademico_DivisoesComponente_OrigemAvaliacao = 1 << 8,

        [EnumMember]
        TrabalhoAcademico_DivisoesComponente_OrigemAvaliacao_AplicacoesAvaliacao = 1 << 9,

        [EnumMember]
        TrabalhoAcademico_DivisoesComponente_OrigemAvaliacao_AplicacoesAvaliacao_Avaliacao = 1 << 10,

        [EnumMember]
        TrabalhoAcademico_DivisoesComponente_OrigemAvaliacao_AplicacoesAvaliacao_MembrosBancaExaminadora = 1 << 11,

        [EnumMember]
        TrabalhoAcademico_DivisoesComponente_OrigemAvaliacao_AplicacoesAvaliacao_MembrosBancaExaminadora_InstituicaoExterna = 1 << 12,

        [EnumMember]
        TrabalhoAcademico_DivisoesComponente_OrigemAvaliacao_AplicacoesAvaliacao_MembrosBancaExaminadora_Colaborador = 1 << 13,

        [EnumMember]
        TrabalhoAcademico_DivisoesComponente_OrigemAvaliacao_AplicacoesAvaliacao_MembrosBancaExaminadora_Colaborador_DadosPessoais = 1 << 14,

        [EnumMember]
        TrabalhoAcademico_InstituicaoEnsino = 1 << 15,

        [EnumMember]
        TrabalhoAcademico_NivelEnsino = 1 << 16,

        [EnumMember]
        TrabalhoAcademico_Autores_Aluno = 1 << 17,

        [EnumMember]
        Arquivos = 1 << 18,

    }
}
