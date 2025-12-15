using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesSolicitacaoDispensa
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Destinos = 1,

        [EnumMember]
        OrigensInternas = 2,

        [EnumMember]
        OrigensExternas = 4,
    }
}