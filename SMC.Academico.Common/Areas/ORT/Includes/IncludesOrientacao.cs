using SMC.Academico.Common.Constants;
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
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesOrientacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        OrientacoesColaborador = 1 << 0,

        [EnumMember]
        OrientacoesPessoaAtuacao = 1 << 1,

        [EnumMember]
        OrientacoesPessoaAtuacao_PessoaAtuacao = 1 << 2,

        [EnumMember]
        OrientacoesPessoaAtuacao_PessoaAtuacao_DadosPessoais = 1 << 3,

        [EnumMember]
        InstituicaoEnsino = 1 << 4,

        [EnumMember]
        NivelEnsino = 1 << 5,

        [EnumMember]
        TipoOrientacao = 1 << 6,

        [EnumMember]
        OrientacoesColaborador_Colaborador = 1 << 7,

        [EnumMember]
        OrientacoesColaborador_Colaborador_DadosPessoais = 1 << 8,
    }
}
