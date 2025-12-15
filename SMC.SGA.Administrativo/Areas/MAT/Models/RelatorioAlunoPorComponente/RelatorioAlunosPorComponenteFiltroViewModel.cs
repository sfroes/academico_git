using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.MAT.Controllers;
using System.Collections.Generic;
using SMC.SGA.Administrativo.Areas.MAT.Views.RelatorioAlunosPorComponente.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class RelatorioAlunosPorComponenteFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCRequired]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [CursoOfertaLocalidadeLookup]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCRequired]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        [SMCSelect(AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(RelatorioAlunosPorComponenteController.BuscarTurnosPorCursoOfertaLocalidadeSelect), "RelatorioAlunosPorComponente", true)]
        public long SeqTurno { get; set; }

        [SMCSelect(UseCustomSelect =true)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(RelatorioAlunosPorComponenteController.BuscarTurmasPorCicloLetivoCursoOfertaLocalidadeTurnoSelect), "RelatorioAlunosPorComponente", true, includedProperties: new string[] { nameof(SeqCursoOfertaLocalidade), nameof(SeqTurno) })]
        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(RelatorioAlunosPorComponenteController.BuscarTurmasPorCicloLetivoCursoOfertaLocalidadeTurnoSelect), "RelatorioAlunosPorComponente", true, includedProperties: new string[] { nameof(SeqCicloLetivo), nameof(SeqTurno) })]
        [SMCDependency(nameof(SeqTurno), nameof(RelatorioAlunosPorComponenteController.BuscarTurmasPorCicloLetivoCursoOfertaLocalidadeTurnoSelect), "RelatorioAlunosPorComponente", true, includedProperties: new string[] { nameof(SeqCicloLetivo), nameof(SeqCursoOfertaLocalidade) })]
        [SMCConditionalReadonly(nameof(SeqCicloLetivo), SMCConditionalOperation.Equals, "", RuleName = "R1", PersistentValue = false)]
        [SMCConditionalReadonly(nameof(SeqCursoOfertaLocalidade), SMCConditionalOperation.Equals, "", RuleName = "R2", PersistentValue = false)]
        [SMCConditionalReadonly(nameof(SeqConfiguracaoComponente), SMCConditionalOperation.Equals, "", RuleName = "R3", PersistentValue = false)]
        [SMCConditionalRequired(nameof(SeqConfiguracaoComponente), SMCConditionalOperation.Equals, "")]
        [SMCConditionalRule("R1 || R2 || !R3")]
        public long? SeqTurma { get; set; }

        [SMCSelect(UseCustomSelect = true)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(RelatorioAlunosPorComponenteController.BuscarConfiguracoesComponentePorCicloLetivoCursoOfertaLocalidadeSelect), "RelatorioAlunosPorComponente", true, includedProperties: new string[] { nameof(SeqCursoOfertaLocalidade), nameof(SeqTurno) })]
        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(RelatorioAlunosPorComponenteController.BuscarConfiguracoesComponentePorCicloLetivoCursoOfertaLocalidadeSelect), "RelatorioAlunosPorComponente", true, includedProperties: new string[] { nameof(SeqCicloLetivo), nameof(SeqTurno) })]
        [SMCDependency(nameof(SeqTurno), nameof(RelatorioAlunosPorComponenteController.BuscarConfiguracoesComponentePorCicloLetivoCursoOfertaLocalidadeSelect), "RelatorioAlunosPorComponente", true, includedProperties: new string[] { nameof(SeqCicloLetivo), nameof(SeqCursoOfertaLocalidade) })]
        [SMCConditionalReadonly(nameof(SeqCicloLetivo), SMCConditionalOperation.Equals, "", RuleName = "R1", PersistentValue = false)]
        [SMCConditionalReadonly(nameof(SeqCursoOfertaLocalidade), SMCConditionalOperation.Equals, "", RuleName = "R2", PersistentValue = false)]
        [SMCConditionalReadonly(nameof(SeqTurma), SMCConditionalOperation.Equals, "", RuleName = "R3", PersistentValue = false)]
        [SMCConditionalRequired(nameof(SeqTurma), SMCConditionalOperation.Equals, "")]
        [SMCConditionalRule("R1 || R2 || !R3")]
        public long? SeqConfiguracaoComponente { get; set; }

        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCRequired]
        public bool? ExibirSolicitanteMatrículaNaoFinalizada { get; set; }
    }
}