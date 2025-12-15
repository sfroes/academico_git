using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesSolicitacaoServicoEtapa
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0, 

        [EnumMember]
        HistoricosSituacao = 1,

        [EnumMember]
        SolicitacaoServico = 2
    }
}
 