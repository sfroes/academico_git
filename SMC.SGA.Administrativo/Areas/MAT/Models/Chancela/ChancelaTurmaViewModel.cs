using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class ChancelaTurmaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqItem { get; set; }

        [SMCSize(SMCSize.Grid9_24)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid9_24)]
        public List<ChancelaTurmaDivisoesViewModel> TurmaMatriculaDivisoes { get; set; }

        [SMCSelect("SituacoesItens", StorageType = SMCStorageType.TempData)]
        public long SeqSituacaoItemMatricula { get; set; }

        public string TurmaFormatado { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }

        public AssociacaoOfertaMatriz AssociacaoOfertaMatrizTipoTurma { get; set; }

        public string DescricaoTipoTurma { get; set; }
    }
}