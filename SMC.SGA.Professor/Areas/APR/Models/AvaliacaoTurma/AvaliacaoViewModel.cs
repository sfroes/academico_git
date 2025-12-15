using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Calendarios.UI.Mvc.Areas.ESF.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Professor.Areas.APR.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class AvaliacaoViewModel : SMCViewModelBase
    {
        #region [Datasource]

        public List<SMCDatasourceItem<TipoAvaliacao>> TiposAvaliacoes { get; set; }

        public List<SMCDatasourceItem> GradesInicio { get; set; }

        public List<SMCDatasourceItem> GradesFim { get; set; }

        #endregion

        #region Auxiliares

        [SMCDependency(nameof(HorarioGrade), "VerificarTurmaIntegracaoSEF", "Avaliacao", "APR", false, includedProperties: new[] { nameof(DisponibilizarGrade), nameof(SeqAgendaTurma) })]
        [SMCDependency(nameof(EntregaWeb), "AlterouEntregaWeb", "Avaliacao", false, nameof(TipoOrigemAvaliacao), nameof(TemConfiguracaoGrade), nameof(SeqOrigemAvaliacao), nameof(SeqAgendaTurma))]
        [SMCHidden]
        public bool? TurmaIntegracaoSEF { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(EntregaWeb), "AlterouEntregaWeb", "Avaliacao", false, nameof(TipoOrigemAvaliacao), nameof(TemConfiguracaoGrade), nameof(SeqOrigemAvaliacao), nameof(SeqAgendaTurma))]
        public bool? DisponibilizarGrade { get; set; }

        [SMCHidden]
        public long? SeqAgendaTurma { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(HorarioGrade), "BuscarLocalTurmaSEFHorarioLivre", "Avaliacao", false, nameof(TurmaIntegracaoSEF))]
        public int? CodigoUnidade { get; set; }
               
        [SMCHidden]
        public TipoOrigemAvaliacao TipoOrigemAvaliacao { get; set; }

        [SMCHidden]
        public bool? EntregaWebInBD { get; set; }

        [SMCHidden]
        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }

        [SMCHidden]
        public bool TemConfiguracaoGrade { get; set; }
        [SMCHidden]
        public DateTime? DataInicioLimiteAvaliacao { get; set; }
        [SMCHidden]
        public DateTime? DataFimLimiteAvaliacao { get; set; }

        #endregion

        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqOrigemAvaliacao { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(TiposAvaliacoes))]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.GreaterThen, 0, PersistentValue = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public TipoAvaliacao? TipoAvaliacao { get; set; }

        [SMCReadOnly]
        [SMCRequired]
        [SMCDependency(nameof(TipoAvaliacao), nameof(AvaliacaoController.ProximaSigla), "Avaliacao", true, new[] { nameof(SeqOrigemAvaliacao) })]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public string Sigla { get; set; }

        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCConditionalDisplay(nameof(TipoEscalaApuracao), SMCConditionalOperation.NotEqual, Academico.Common.Areas.APR.Enums.TipoEscalaApuracao.AprovadoReprovado)]
        [SMCConditionalRequired(nameof(TipoEscalaApuracao), SMCConditionalOperation.NotEqual, Academico.Common.Areas.APR.Enums.TipoEscalaApuracao.AprovadoReprovado)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid12_24, SMCSize.Grid2_24, SMCSize.Grid4_24)]
        public short? Valor { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public bool? EntregaWeb { get; set; }

        [SMCConditionalReadonly(nameof(DisponibilizarGrade), false, PersistentValue = true)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid12_24, SMCSize.Grid10_24, SMCSize.Grid8_24)]
        [SMCDependency(nameof(EntregaWeb), "AlterouEntregaWeb", "Avaliacao", false, nameof(TipoOrigemAvaliacao), nameof(TemConfiguracaoGrade), nameof(SeqOrigemAvaliacao), nameof(SeqAgendaTurma))]
        public bool HorarioGrade { get; set; }

        [SMCConditionalDisplay(nameof(HorarioGrade), SMCConditionalOperation.NotEqual, true)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCMinDate(nameof(DataInicioLimiteAvaliacao))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime? DataInicioAplicacaoAvaliacao { get; set; }

        [SMCConditionalDisplay(nameof(HorarioGrade), SMCConditionalOperation.NotEqual, true)]
        [SMCConditionalRequired(nameof(EntregaWeb), true)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCMinDate(nameof(DataInicioAplicacaoAvaliacao))]
        [SMCMaxDate(nameof(DataFimLimiteAvaliacao))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime? DataFimAplicacaoAvaliacao { get; set; }

        [SMCSelect(nameof(GradesInicio))]
        [SMCConditionalDisplay(nameof(HorarioGrade), true)]
        [SMCConditionalRequired(nameof(HorarioGrade), true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqInicioGradeAvaliacao { get; set; }

        [SMCSelect(nameof(GradesFim),AutoSelectSingleItem = true)]
        [SMCConditionalDisplay(nameof(HorarioGrade), true)]
        [SMCConditionalRequired(nameof(HorarioGrade), true)]
        [SMCDependency(nameof(SeqInicioGradeAvaliacao), "BuscarFimGradeAvaliacaoSelect", "Avaliacao", "APR", false)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqFimGradeAvaliacao { get; set; }

        [SMCConditionalRequired(nameof(EntregaWeb), true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public short? QuantidadeMaximaPessoasGrupo { get; set; }

        [SMCConditionalDisplay(nameof(TipoAvaliacao), Academico.Common.Areas.APR.Enums.TipoAvaliacao.Trabalho, RuleName = "CDI1")]
        [SMCConditionalDisplay(nameof(EntregaWeb), true, RuleName = "CDI2")]
        [SMCConditionalRule("CDI1 || CDI2")]
        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        public string Instrucao { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexadoInstrucao { get; set; }

        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCConditionalDisplay(nameof(TipoAvaliacao), Academico.Common.Areas.APR.Enums.TipoAvaliacao.Trabalho, RuleName = "CDAN1")]
        [SMCConditionalDisplay(nameof(EntregaWeb), true, RuleName = "CDAN2")]
        [SMCConditionalRule("CDAN1 || CDAN2")]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        public SMCUploadFile ArquivoAnexadoInstrucao { get; set; }

        [LocalSEFLookup(DisplayTreeviewClosed = true)]
        [SMCConditionalDisplay(nameof(TurmaIntegracaoSEF), true, RuleName = "LK1")]
        [SMCConditionalDisplay(nameof(HorarioGrade), false, RuleName = "LK2")]
        [SMCConditionalRule("LK1 && LK2")]
        [SMCDependency(nameof(CodigoUnidade))]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid18_24)]
        public LocalSEFLookupViewModel LocalSEF { get; set; }

        [SMCConditionalDisplay(nameof(TurmaIntegracaoSEF), false, RuleName = "LK11")]
        [SMCConditionalDisplay(nameof(HorarioGrade), true, RuleName = "LK12")]
        [SMCConditionalRule("LK11 || LK12")]
        [SMCConditionalReadonly(nameof(HorarioGrade), true)]
        [SMCDependency(nameof(SeqInicioGradeAvaliacao), "BuscarLocalGradeAvaliacaoSelect", "Avaliacao", "APR", false, includedProperties: new[] { nameof(HorarioGrade), nameof(Local) })]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid18_24)]
        public string Local { get; set; }

        [SMCHidden]
        public long SeqAplicacaoAvaliacao { get; set; }

        [SMCHidden]
        public long? SeqEventoAgd { get; set; }

    }
}