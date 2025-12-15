using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConsultarTaxasPorNucleoListarItemData : ISMCMappable
    {
        public int CodigoNucleo { get; set; }

        public string NomeNucleo { get; set; }

        public decimal ValorTaxa { get; set; }

        public DateTime DataInicioValidade { get; set; }

        public DateTime? DataFimValidade { get; set; }
    }
}
