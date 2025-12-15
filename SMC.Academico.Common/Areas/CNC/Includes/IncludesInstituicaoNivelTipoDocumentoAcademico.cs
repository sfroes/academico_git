using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoNivelTipoDocumentoAcademico : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        FormacoesEspecificas_TipoFormacaoEspecifica = 1,

        [EnumMember]
        TipoDocumentoAcademico = 2,

        [EnumMember]
        ModelosRelatorio = 4,

        [EnumMember]
        ModelosRelatorio_ArquivoModelo = 8,

        [EnumMember]
        TiposFuncionario = 16
    }
}
