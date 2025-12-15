using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum ModalidadeOferta : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Oferta aberta")]
        OfertaAberta = 1,

        [EnumMember]
        [Description("Oferta fechada")]
        OfertaFechada = 2,

        [EnumMember]
        [Description("Oferta mista")]
        OfertaMista = 3
    }
}