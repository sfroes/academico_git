using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesReferenciaFamiliar
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,
        
        [EnumMember]
        Enderecos = 1,
                               
        [EnumMember]
        Telefones = 2,
                
        [EnumMember]
        EnderecosEletronicos = 4,

    }
}
