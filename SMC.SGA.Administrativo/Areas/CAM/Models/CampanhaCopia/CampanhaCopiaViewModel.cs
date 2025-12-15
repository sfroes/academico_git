using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaCopiaViewModel : SMCWizardViewModel, ISMCWizardViewModel, ISMCStatefulView
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource(SMCStorageType.ViewData)]
        public List<SMCDatasourceItem> CiclosLetivos { get; set; }

        #endregion DataSources

        #region Campanha

        [SMCSize(SMCSize.Grid2_24)]
        public long SeqCampanhaOrigem { get; set; }

        [SMCSize(SMCSize.Grid14_24)]
        public string DescricaoCampanhaOrigem { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public List<string> CiclosLetivosCampanhaOrigem { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCRequired]
        [SMCMaxLength(100)]
        public string DescricaoCampanhaDestino { get; set; }

        [SMCDetail(min: 1)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<CampanhaCopiaCicloLetivoItemViewModel> CiclosLetivosCampanhaDestino { get; set; }

        #endregion Campanha

        #region Ofertas da Campanha

        public SMCPagerModel<CampanhaCopiaOfertaListaViewModel> CampanhaOfertas { get; set; }

        public List<object> GridCampanhaOferta { get; set; }

        #endregion Ofertas da Campanha

        #region Processos Seletivos

        public List<CampanhaCopiaProcessoSeletivoListaViewModel> ProcessosSeletivos { get; set; }

        public List<object> GridProcessoSeletivo { get; set; }

        #endregion Processos Seletivos
    }
}