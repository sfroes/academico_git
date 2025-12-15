using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ReplicaFormacaoEspecificaProgramaTitulacaoViewModel : SMCViewModelBase, ISMCMappable
    {
        #region [DataSources]

        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCSelectListItem> DataSourceTitulacoes { get; set; }

        public List<SMCSelectListItem> DataSourceGrauAcademico { get; set; }

        #endregion

        [SMCHidden]
        public long SeqCurso { get; set; }

        [SMCHidden]
        public bool ExigeGrau { get; set; }

        [SMCSelect(nameof(DataSourceGrauAcademico), autoSelectSingleItem: true)]
        [SMCConditional(SMCConditionalBehavior.Visibility | SMCConditionalBehavior.Required, nameof(ExigeGrau), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid7_24)]
        public long? SeqGrauAcademico { get; set; }

        [SMCSelect(nameof(DataSourceTitulacoes), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCRequired]
        public long SeqTitulacao { get; set; }

        [SMCReadOnly]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid5_24)]
        public bool Ativo { get; set; }
    }
}