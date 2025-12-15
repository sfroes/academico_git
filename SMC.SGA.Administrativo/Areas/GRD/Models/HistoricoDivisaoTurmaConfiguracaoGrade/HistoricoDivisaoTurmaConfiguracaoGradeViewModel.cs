using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.GRD.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class HistoricoDivisaoTurmaConfiguracaoGradeViewModel : SMCViewModelBase
    {
        #region Datasource

        public List<SMCDatasourceItem> TiposPagamento { get; set; }

        public List<SMCDatasourceItem> TiposPulaFeriado { get; set; }

        #endregion

        [SMCReadOnly]
        [SMCKey]
        [SMCRequired]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDivisaoTurma { get; set; }

        [SMCRequired]
        [SMCReadOnly]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime DataInicio { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public TipoDistribuicaoAula TipoDistribuicaoAula { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(TiposPulaFeriado))]
        [SMCDependency(nameof(TipoDistribuicaoAula), nameof(HistoricoDivisaoTurmaConfiguracaoGradeController.BuscarTiposPulaFeriadoSelect), "HistoricoDivisaoTurmaConfiguracaoGrade", true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public TipoPulaFeriado TipoPulaFeriado { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public bool? AulaSabado { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(TiposPagamento))]
        [SMCDependency(nameof(TipoDistribuicaoAula), nameof(HistoricoDivisaoTurmaConfiguracaoGradeController.BuscarTiposPagamentoSelect), "HistoricoDivisaoTurmaConfiguracaoGrade", true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public TipoPagamentoAula TipoPagamentoAula { get; set; }
    }
}