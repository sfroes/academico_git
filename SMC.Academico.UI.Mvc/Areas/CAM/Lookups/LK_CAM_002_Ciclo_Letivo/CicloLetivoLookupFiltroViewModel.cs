using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CicloLetivoLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSources ]

        public List<SMCDatasourceItem> RegimesLetivos { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCMask("0000")]
        [SMCMapProperty("Ano")]
        public short? AnoCiclo { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCMask("99")]
        [SMCMapProperty("Numero")]
        public short? NumeroCiclo { get; set; }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCSelect("RegimesLetivos")]
        public long? SeqRegimeLetivo { get; set; }

        [SMCDescription]
        [SMCHidden]
        public string Descricao { get; set; }

        [SMCHidden]
        public long? SeqNivel { get; set; }

        /// <summary>
        /// Sequenciais dos níveis de ensino stricto sensu para filtro do lookup
        /// </summary>
        [SMCIgnoreProp]
        public List<long> SeqsNiveisEnsino { get { return SeqsNiveisEnsinoStr?.Split(',').Select(x => long.Parse(x)).ToList(); } set { SeqsNiveisEnsinoStr = value != null ? string.Join(",", value.ToArray()) : null; } }

        /// <summary>
        /// Concatena os sequenciais para enviar como dependency do lookup
        /// </summary>
        [SMCHidden]
        public string SeqsNiveisEnsinoStr { get; set; }
    }
}