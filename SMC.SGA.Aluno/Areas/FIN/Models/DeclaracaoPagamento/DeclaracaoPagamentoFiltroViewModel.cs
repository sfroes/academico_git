using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Aluno.Areas.FIN.Models
{
    public class DeclaracaoPagamentoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        /// <summary>
        /// NV01
        /// A data de inicio informada deve ser anterior a data de fim.
        /// Ambas as datas devem ser anteriores a data atual.
        /// </summary>
        [SMCRequired]
        [SMCMaxDate(nameof(DataFim))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime DataInicio { get; set; }

        /// <summary>
        /// NV01
        /// A data de inicio informada deve ser anterior a data de fim.
        /// Ambas as datas devem ser anteriores a data atual.
        /// </summary>
        [SMCRequired]
        [SMCMaxDate(SMCUnitOfTime.Day, -1)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime DataFim { get; set; }
    }
}