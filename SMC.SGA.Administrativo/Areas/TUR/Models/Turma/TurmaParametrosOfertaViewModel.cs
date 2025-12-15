using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaParametrosOfertaViewModel : SMCViewModelBase
    {
        public long SeqOrigemAvaliacao { get; set; }

        [SMCRadioButton]
        public long SeqOfertaMatriz { get; set; }

        public bool Selecionado { get; set; }

        public string DescricaoOfertaMatriz { get; set; }

        public short DivisaoMatrizCurricularNumero { get; set; }

        public string DivisaoMatrizCurricularDescricao { get; set; }

        public string DivisaoMatrizCurricularCompleto { get { return $"{DivisaoMatrizCurricularNumero} - {DivisaoMatrizCurricularDescricao}"; } }

        [SMCRequired]
        [SMCSelect(nameof(TurmaDynamicModel.CriteriosAprovacao), StorageType = SMCStorageType.TempData)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid7_24)]
        public long SeqCriterioAprovacao { get; set; }

        [SMCDependency(nameof(SeqCriterioAprovacao), "BuscarCriterioNota", "DivisaoMatrizCurricularComponente", "CUR", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public string CriterioNotaMaxima { get; set; }

        [SMCDependency(nameof(SeqCriterioAprovacao), "BuscarAprovacaoPercentual", "DivisaoMatrizCurricularComponente", "CUR", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public string CriterioPercentualNotaAprovado { get; set; }

        [SMCDependency(nameof(SeqCriterioAprovacao), "BuscarPresencaPercentual", "DivisaoMatrizCurricularComponente", "CUR", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid12_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public string CriterioPercentualFrequenciaAprovado { get; set; }

        [SMCHidden]
        public long? SeqEscalaApuracao { get; set; }

        [SMCDependency(nameof(SeqCriterioAprovacao), "BuscarEscalaApuracao", "DivisaoMatrizCurricularComponente", "CUR", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid12_24, SMCSize.Grid5_24, SMCSize.Grid4_24)]
        public string CriterioDescricaoEscalaApuracao { get; set; }

        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid24_24)]
        public List<TurmaParametrosDetalheViewModel> DivisoesComponente { get; set; }

        [SMCHidden]
        public bool ApurarFrequencia { get; set; }
    }
}