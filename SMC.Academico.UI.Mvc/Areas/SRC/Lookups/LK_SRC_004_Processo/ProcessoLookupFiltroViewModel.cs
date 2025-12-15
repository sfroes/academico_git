using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class ProcessoLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSources ]

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Servicos { get; set; }

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Entidades { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCSize(SMCSize.Grid4_24)]
        public long? Seq { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect]
        public TipoAtuacao? TipoAtuacao { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCDependency(nameof(TipoAtuacao), nameof(ProcessoController.BuscarServicos), "ProcessoRoute", "", false)]
        [SMCSelect(nameof(Servicos))]
        public long? SeqServico { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCSelect(nameof(Entidades))]
        public long? SeqEntidade { get; set; }

        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid8_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(ProcessoController.PreencherDataInicio), "ProcessoRoute", "", false)]
        public DateTime? DataInicio { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(ProcessoController.PreencherDataFim), "ProcessoRoute", "", false)]
        public DateTime? DataFim { get; set; }
    }
}