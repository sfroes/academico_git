using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.ORT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoPesquisaTrabalhoAcademico : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Autor = 1,

        [EnumMember]
        Orientador = 2,

        [EnumMember]
        Coorientador = 3
    }
}
