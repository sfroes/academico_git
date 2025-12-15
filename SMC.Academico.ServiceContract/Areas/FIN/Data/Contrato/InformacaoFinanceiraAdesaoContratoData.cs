using SMC.Academico.Common.Constants;
using SMC.Framework.Mapper;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    [DataContract(Namespace = NAMESPACES.MODEL, IsReference = true)]
    public class InformacaoFinanceiraAdesaoContratoData : ISMCMappable
    {
        [DataMember]
        public decimal ValorParcela { get; set; }

        [DataMember]
        public decimal ValorTotalCurso { get; set; }

        [DataMember]
        public int QuantidadeParcelas { get; set; }

        [DataMember]
        public int DiaVencimentoParcelas { get; set; }

        [DataMember]
        public string DescricaoServico { get; set; }

        [DataMember]
        public DateTime DataInicioParcela { get; set; }

        [DataMember]
        public bool ServicoAdicional { get; set; }
    }
}