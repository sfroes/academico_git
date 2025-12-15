using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum ObrigatorioOptativo : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        /// <summary>
        /// Mapeado como falso no domínio
        /// </summary>
        [EnumMember]
        OB = 1,

        /// <summary>
        /// Mapeado como verdadeiro no domínio
        /// </summary>
        [EnumMember]
        OP = 2
    }
}