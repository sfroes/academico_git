using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum CategoriaEnderecoEletronico : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Secretaria = 1,

        [EnumMember]
        [Description("Coordenação")]
        Coordenacao = 2,

        [EnumMember]
        [Description("Matrículas")]
        Matriculas = 3
    }
}