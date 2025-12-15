using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class BeneficioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSource

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IBeneficioService), nameof(IBeneficioService.BuscarTipoBeneficioSelect))]
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposBeneficio { get; set; }

        #endregion DataSource

        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid3_24)]
        public long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCMaxLength(100)]
        [SMCDescription]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(2)]
        [SMCSelect(nameof(TiposBeneficio))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public long SeqTipoBeneficio { get; set; }

        #region Propriedades para ordenação default

        [SMCHidden]
        [SMCSortable(true, true, "TipoBeneficio.Descricao")]
        public string DescricaoTipoBeneficio { get; set; }

        #endregion
    }
}