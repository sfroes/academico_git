using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class TipoConfiguracaoGrupoCurricularFilhoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSelect("TipoConfiguracaoGrupoCurricular", "Seq", "Descricao")]
        [SMCMapProperty("Seq")]
        [SMCKey]
        public long SeqSubgrupo { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICURDynamicService))]
        [SMCDataSource]
        [SMCHidden]
        public List<SMCDatasourceItem> TipoConfiguracaoGrupoCurricular { get; set; }
    }
}