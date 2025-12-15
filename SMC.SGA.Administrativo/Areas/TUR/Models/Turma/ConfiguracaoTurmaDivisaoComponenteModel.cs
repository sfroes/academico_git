using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class ConfiguracaoTurmaDivisaoComponenteModel : SMCViewModelBase
    {
        #region [DataSource] 
        [SMCDataSource(storageType: SMCStorageType.None)]
        public List<SMCDatasourceItem> ListaEscalaApuracao { get; set; }
        #endregion [DataSource]    

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDivisaoComponente { get; set; }

        [SMCHidden]
        public TipoGestaoDivisaoComponente TipoGestaoDivisaoComponente { get; set; }

        [SMCHidden]
        public long SeqOrigemAvaliacao { get; set; }

        [SMCHidden]
        public long SeqTurma { get; set; }

        [SMCHidden]
        public long? SeqCriterioAprovacao { get; set; }

        [SMCHidden]
        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid9_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCMask("999")]
        [SMCMinValue(0)]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCConditionalReadonly(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.NotEqual, TipoGestaoDivisaoComponente.Turma)]
        public short? QuantidadeGrupos { get; set; }

        [SMCMask("999")]
        [SMCMinValue(0)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCConditionalReadonly(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.NotEqual, TipoGestaoDivisaoComponente.Turma)]
        public short? QuantidadeProfessores { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        [SMCConditionalReadonly(nameof(SeqEscalaApuracao), SMCConditionalOperation.NotEqual, "")]
        public short? NotaMaxima { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid5_24)]
        public bool? ApurarFrequencia { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid5_24)]
        public bool? MateriaLecionadaObrigatoria { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(nameof(ListaEscalaApuracao))]
        [SMCConditionalReadonly(nameof(NotaMaxima), SMCConditionalOperation.NotEqual, "")]
        public long? SeqEscalaApuracao { get; set; }
    }
}