using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [Flags] //Utiliza flags no enum porque é SMCCheckBoxList em filtro
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoDispensa : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Componente")]
        Componente = 1,

        [Description("Dispensado por")]
        [EnumMember]
        DispensadoPor = 2,

        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Todos = Componente | DispensadoPor
    }
}