using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConsultaPosicaoGeralFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [ DataSource ]

        public List<SMCSelectListItem> Servicos { get; set; }

        public List<SMCSelectListItem> EntidadesResponsaveis { get; set; }

        #endregion [ DataSource ]

        [SMCRequired]
        [SMCSelect(nameof(Servicos))]
        [SMCSize(SMCSize.Grid6_24)]
        public long[] SeqsServicos { get; set; }

        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid6_24)]
        public long[] SeqsEntidadesResponsaveis { get; set; }

        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid6_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCDependency(nameof(SeqCicloLetivo), nameof(ConsultaPosicaoGeralController.BuscarProcessos), "ConsultaPosicaoGeral", false, new[] { nameof(SeqsEntidadesResponsaveis), nameof(SeqsServicos) })]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis), nameof(ConsultaPosicaoGeralController.BuscarProcessos), "ConsultaPosicaoGeral", true, new[] { nameof(SeqsServicos), nameof(SeqCicloLetivo) })]
        [SMCDependency(nameof(SeqsServicos), nameof(ConsultaPosicaoGeralController.BuscarProcessos), "ConsultaPosicaoGeral", true, new[] { nameof(SeqsEntidadesResponsaveis), nameof(SeqCicloLetivo) })]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24)]
        public long[] SeqsProcesso { get; set; }
    }
}