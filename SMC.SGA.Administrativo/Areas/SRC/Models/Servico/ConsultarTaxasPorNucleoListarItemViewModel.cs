using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConsultarTaxasPorNucleoListarItemViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public int CodigoNucleo { get; set; }

        [SMCHidden]
        public string NomeNucleo { get; set; }

        public string DescricaoNucleo
        {
            get
            {
                return $"{CodigoNucleo} - {NomeNucleo}";
            }
        }

        public decimal ValorTaxa { get; set; }

        public DateTime DataInicioValidade { get; set; }

        public DateTime? DataFimValidade { get; set; }
    }
}