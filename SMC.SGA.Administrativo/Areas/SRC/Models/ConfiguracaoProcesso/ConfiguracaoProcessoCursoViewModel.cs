using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoProcessoCursoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoProcesso { get; set; }

        [SMCHidden]
        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        [SMCRequired]
        [CursoOfertaLocalidadeLookup]
        [SMCDependency(nameof(ConfiguracaoProcessoViewModel.ListarDepartamentosGruposProgramas))]
        [SMCDependency(nameof(ConfiguracaoProcessoViewModel.SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(ConfiguracaoProcessoViewModel.SeqsNiveisEnsino))]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid11_24)]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }
                   
        [SMCRequired]
        [SMCSelect(nameof(ConfiguracaoProcessoViewModel.TurnosDataSource), NameDescriptionField = nameof(DescricaoTurno))]
        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(ConfiguracaoProcessoController.BuscarTurnosPorOfertaCursoLocalidadeSelect), "ConfiguracaoProcesso", true)]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid11_24)]
        public long SeqTurno { get; set; }

        [SMCHidden]
        public string DescricaoTurno { get; set; }
    }
}