using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ProcessoSeletivoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoSelect))]
        public List<SMCSelectListItem> NiveisEnsino { get; set; }

        #endregion DataSources

        [SMCKey]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24)]
        public long? Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCampanha { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid20_24)]
        public string Descricao { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid4_24)]
        public long? SeqNivelEnsino { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCDependency(nameof(SeqNivelEnsino), nameof(TipoProcessoSeletivoController.BuscarTipoProcessoPorNivelEnsino), "TipoProcessoSeletivo", false)]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqTipoProcessoSeletivo { get; set; }
    }
}