using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class CondicaoPagamentoAcademicoData : ISMCMappable
    {
        public int SeqCondicaoPagamento { get; set; }

        public string DescricaoCondicaoPagamento { get; set; }

        public decimal ValorTotalCurso { get; set; }

        public int QuantidadeParcelas { get; set; }

        public decimal ValorParcelas { get; set; }

        public int DiaVencimentoParcelas { get; set; }

        public DateTime DataVencimentoParcelas { get; set; }

        public int NumeroParcela { get; set; }
    }
}
