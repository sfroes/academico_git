using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.TUR.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.GRD.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class GradeHorariaCompartilhadaItemViewModel : SMCViewModelBase
    {
        #region [ DataSource ]

        [SMCIgnoreProp]
        [SMCDataSource]
        public List<SMCDatasourceItem> DivisoesTurma { get; set; }

        #endregion [ DataSource ]

        [TurmaLookup]
        [SMCDependency(nameof(GradeHorariaCompartilhadaDynamicModel.SeqCicloLetivo))]
        [SMCDependency(nameof(GradeHorariaCompartilhadaDynamicModel.SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(TurmaSituacaoNaoCancelada))]
        [SMCDependency(nameof(TurmasPeriodoEncerrado))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCConditionalReadonly(nameof(GradeHorariaCompartilhadaDynamicModel.SeqCicloLetivo), "", RuleName = "R1")]
        [SMCConditionalReadonly(nameof(GradeHorariaCompartilhadaDynamicModel.SeqsEntidadesResponsaveis), "", RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        public TurmaLookupViewModel SeqTurma { get; set; }

        [SMCDependency(nameof(SeqTurma), nameof(GradeHorariaCompartilhadaController.BuscarDivisoesTurmaGradeHorariaCompartilhada), "GradeHorariaCompartilhada", false)]
        [SMCRequired]
        [SMCSelect(nameof(DivisoesTurma))]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCConditionalReadonly(nameof(SeqTurma), "")]
        [SMCUnique]
        public long SeqDivisaoTurma { get; set; }

        #region Campos de apoio
        [SMCHidden]
        public bool TurmasPeriodoEncerrado => false;

        [SMCHidden]
        public bool TurmaSituacaoNaoCancelada => true;
        #endregion
    }
}