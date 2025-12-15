using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class ChancelaTurmaDivisoesViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCHidden]
        public long? SeqConfiguracaoComponente { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> DivisoesTurmas { get; set; }

        [SMCSelect(nameof(DivisoesTurmas), AutoSelectSingleItem = true)]
        public long? SeqDivisaoTurma { get; set; }

        public string DivisaoTurmaDescricao { get; set; }

        public bool PermitirGrupo { get; set; }
    }
}