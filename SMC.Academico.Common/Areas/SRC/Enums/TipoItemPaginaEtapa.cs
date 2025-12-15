using SMC.Academico.Common.Constants;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoItemPaginaEtapa : short
    {
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("pagina")]
        Pagina = 1,      

        [EnumMember]
        [Description("texto")]
        Secao = 2,

        [EnumMember]
        [Description("arquivo")]
        Arquivo = 3
    }
}
