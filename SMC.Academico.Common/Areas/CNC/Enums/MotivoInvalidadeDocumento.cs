using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum MotivoInvalidadeDocumento : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Danificado = 1,

        [EnumMember]
        Extraviado = 2,

        [EnumMember]
        Descartado = 3,

        [EnumMember]
        [Description("Indeferido pelo aluno")]
        IndeferidoPeloAluno = 4,

        [EnumMember]
        [Description("Revisão dos dados")]
        DadosInconsistentes = 5
    }
}