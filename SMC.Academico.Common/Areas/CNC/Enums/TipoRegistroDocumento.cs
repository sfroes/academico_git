using SMC.DadosMestres.Common.Constants;
using SMC.Framework;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoRegistroDocumento : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Externo = 1,

        [EnumMember]
        Interno = 2
    }
}