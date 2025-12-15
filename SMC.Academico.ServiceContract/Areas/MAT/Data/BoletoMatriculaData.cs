using SMC.Financeiro.ServiceContract.BLT.Data;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    [DataContract(Namespace = "http://smc.com.br/financeiro/v4/model/", IsReference = true)]
    public class BoletoMatriculaData : BoletoData
    {
        [DataMember]
        public long SeqSolicitacaoMatricula { get; set; }

        [DataMember]
        public long SeqIngressante { get; set; }

        [DataMember]
        public string DescricaoVinculo { get; set; }

        [DataMember]
        public string DescricaoProcesso { get; set; }

        [DataMember]
        public string DescricaoParcela { get; set; }
    }
}