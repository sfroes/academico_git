using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaDivisoesDetailViewModel : SMCViewModelBase, ISMCMappable
    {

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqOrigemAvaliacao { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid2_24)]
        public string Turma { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid2_24)]
        public string DivisaoDescricao { get; set; }

        [SMCMask("000")]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid2_24)]
        public string GrupoNumero { get; set; }

        //[SMCRequired]
        [SMCConditionalReadonly(nameof(TurmaDynamicModel.HabilitarLocalidades), SMCConditionalOperation.Equals, "true", PersistentValue = true)]
        [SMCSelect(nameof(TurmaDynamicModel.DivisoesLocalidades), StorageType = SMCStorageType.TempData)]
        [SMCSize(SMCSize.Grid7_24)]
        public long SeqLocalidade { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24)]
        public short? DivisaoVagas { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid2_24)]
        [SMCValueEmpty("0")]
        public short? QuantidadeVagasOcupadas { get; set; }

        [SMCConditionalReadonly(nameof(TurmaDynamicModel.HabilitarInformacoesAdicionais), SMCConditionalOperation.Equals, "true", PersistentValue = true)]
        [SMCSize(SMCSize.Grid5_24)]
        public string InformacoesAdicionais { get; set; }
    }
}