using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CampanhaLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSources ]

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> UnidadesResponsaveis { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid16_24)]
        public string Descricao { get; set; }

        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivoLookup { get; set; }

        [SMCIgnoreProp]
        public long? SeqCicloLetivo
        {
            get => SeqCicloLetivoLookup?.Seq;
            set => SeqCicloLetivoLookup = new CicloLetivoLookupViewModel() { Seq = value };
        }

        [SMCSelect(nameof(UnidadesResponsaveis))]
        [SMCSize(SMCSize.Grid18_24)]
        public long? SeqEntidadeResponsavel { get; set; }
    }
}