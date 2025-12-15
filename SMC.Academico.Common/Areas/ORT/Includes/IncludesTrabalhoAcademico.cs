using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.ORT.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesTrabalhoAcademico : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TipoTrabalho = 1 << 0,

        [EnumMember]
        Autores = 1 << 1,

        [EnumMember]
        DivisoesComponente = 1 << 2,

        [EnumMember]
        InstituicaoEnsino = 1 << 3,

        [EnumMember]
        NivelEnsino = 1 << 4,

        [EnumMember]
        PublicacaoBdp = 1 << 5,

        [EnumMember]
        DivisoesComponente_DivisaoComponente = 1 << 6,

        [EnumMember]
        DivisoesComponente_OrigemAvaliacao = 1 << 7,
    }
}
