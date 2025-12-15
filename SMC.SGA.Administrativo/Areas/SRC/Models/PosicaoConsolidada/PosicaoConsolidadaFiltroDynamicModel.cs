using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class PosicaoConsolidadaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCHidden]
        [SMCParameter]
        public long? SeqProcesso { get; set; }

        [SMCDataSource(SMCStorageType.Session)]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IGrupoEscalonamentoService), nameof(IGrupoEscalonamentoService.BuscarGruposEscalonamentoSelect), values: new string[] { nameof(SeqProcesso) })]
        public List<SMCDatasourceItem> GruposEscalonamentos { get; set; } = new List<SMCDatasourceItem>();
         
        [SMCSize(SMCSize.Grid10_24)]
        [SMCSelect(nameof(GruposEscalonamentos))]
        public long? SeqGrupoEscalonamento { get; set; }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new ProcessoNavigationGroup(this);
        }

    }
}