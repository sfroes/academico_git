using SMC.Framework.Mapper;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Data
{
    [DataContract]
    public class FormaIngressoFiltroData : ISMCMappable
    {
        [DataMember]
        public long? SeqNivelEnsino { get; set; }

        [DataMember]
        public List<long> SeqsNivelEnsino { get; set; }

        [DataMember]
        public long? SeqTipoVinculoAluno { get; set; }

        [DataMember]
        public long? SeqTipoProcessoSeletivo { get; set; }

        [DataMember]
        public long? SeqProcessoSeletivo { get; set; }
    }
}