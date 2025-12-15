using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.TUR.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class ConfiguracaoTurmaDynamicModel : SMCDynamicViewModel
    {
        #region [DataSource] 
        [SMCDataSource]
        public List<SMCDatasourceItem> ListaCriterioAprovacao { get; set; }
        #endregion [DataSource] 

        [SMCIgnoreProp]
        public TurmaCabecalhoViewModel Cabecalho { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqCriterioAprovacao), nameof(TurmaController.BuscarApuracaoFrequencia), "Turma", true)]
        public bool ApuracaoFrequencia { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqCriterioAprovacao), nameof(TurmaController.BuscarApuracaoNota), "Turma", true)]
        public bool ApuracaoNota { get; set; }

        [SMCHidden]
        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }

        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqOrigemAvaliacao { get; set; }

        [SMCHidden]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoComponente { get; set; }

        [SMCHidden]
        public long? SeqMatrizCurricularOferta { get; set; }

        [SMCHidden]
        public bool OcorreuAlteracaoManual { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCSelect(nameof(ListaCriterioAprovacao))]
        public long? SeqCriterioAprovacao { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqCriterioAprovacao), nameof(TurmaController.BuscarSeqEscalaApuracao), "Turma", true)]
        public long? SeqEscalaApuracao { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid2_24)]
        [SMCDependency(nameof(SeqCriterioAprovacao), nameof(TurmaController.BuscarCriterioNota), "Turma", true)]
        public short? NotaMaxima { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCDependency(nameof(SeqCriterioAprovacao), nameof(TurmaController.BuscarAprovacaoPercentual), "Turma", true)]
        public short? PercentualNotaAprovado { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCDependency(nameof(SeqCriterioAprovacao), nameof(TurmaController.BuscarPresencaPercentual), "Turma", true)]
        public short? PercentualFrequenciaAprovado { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCDependency(nameof(SeqCriterioAprovacao), nameof(TurmaController.BuscarEscalaApuracao), "Turma", true)]
        public string DescricaoEscalaApuracao { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid4_24)]
        public bool? PermiteAvaliacaoParcial { get; set; }

        public List<ConfiguracaoTurmaDivisaoComponenteModel> DivisaoComponente { get; set; }
    }
}