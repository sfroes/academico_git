using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class MotivoBloqueioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCSize(Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid3_24)]
        [SMCFilter(true, true)]
        public long? Seq { get; set; }
                       
        [SMCOrder(1)]
        [SMCSelect("TiposBloqueio")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCFilter(true, true)]
        public long SeqTipoBloqueio { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource("TipoBloqueio")]
        [SMCServiceReference(typeof(IPESDynamicService))]
        public List<SMCDatasourceItem> TiposBloqueio { get; set; }

        [SMCOrder(2)]
        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid14_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid14_24, Framework.SMCSize.Grid8_24)]
        [SMCFilter(true, true)]
        public string Descricao { get; set; }


    }
}