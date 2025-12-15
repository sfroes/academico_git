using SMC.Framework.Mapper;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Data
{
    [DataContract]
    public class TipoEventoFiltroData : ISMCMappable
    {
        [DataMember]
        public long? SeqInstituicaoEnsino { get; set; }

        [DataMember]
        public long? SeqTipoAgenda { get; set; }

        [DataMember]
        [SMCMapProperty("SeqUnidadeResponsavel")]
        public long? SeqUnidadeResponsavelAgd { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        [SMCMapProperty("ApenasAtivos")]
        public bool? Ativo { get; set; }

        [DataMember]
        public long? SeqCicloLetivo { get; set; }
    }
}