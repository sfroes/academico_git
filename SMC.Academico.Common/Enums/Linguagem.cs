using SMC.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Enums
{
    //
    // Summary:
    //     Enum que contém os idiomas suportados pela cultura C#,a descrição de cada idioma
    //     é o nome do idioma escrito no idioma
    public enum Linguagem : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Português")]
        Portuguese = 1,

        [EnumMember]
        [Description("Inglês")]
        English = 2,

        [EnumMember]
        [Description("Francês")]
        French = 3,

        [EnumMember]
        [Description("Espanhol")]
        Spanish = 4,

        [EnumMember]
        [Description("Alemão")]
        German = 5,

        [EnumMember]
        [Description("Italiano")]
        Italian = 6
    }
}
