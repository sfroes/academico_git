using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    [DataContract]
    public class MensagemFiltroData : SMCPagerFilterData, ISMCMappable
    {
        [DataMember]
        public long SeqPessoaAtuacao { get; set; }

        [DataMember]
        public long? SeqTipoMensagem { get; set; }

        [DataMember]
        public bool? MensagensValidas { get; set; }

        [DataMember]
        public CategoriaMensagem? CategoriaMensagem { get; set; }
    }
}
