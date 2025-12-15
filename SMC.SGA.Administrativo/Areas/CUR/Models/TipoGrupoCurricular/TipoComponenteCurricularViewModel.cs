using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class TipoComponenteCurricularViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSelect("TipoComponenteCurricular", "Seq", "Descricao")]
        [SMCRequired]
        [SMCKey]
        [SMCUnique]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICURDynamicService))]
        public List<SMCDatasourceItem> TipoComponenteCurricular { get; set; }
    }
}