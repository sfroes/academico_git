using SMC.Academico.Common.Enums;
using SMC.Framework.Mapper;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    [DataContract]
    public class CredenciaisAcessoBotaoData : ISMCMappable
    {
        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public string Link { get; set; }

        [DataMember]
        public TipoBotao TipoBotao { get; set; }
    }
}