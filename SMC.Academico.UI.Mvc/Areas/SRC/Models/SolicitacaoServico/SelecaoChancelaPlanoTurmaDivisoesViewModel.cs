using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SelecaoChancelaPlanoTurmaDivisoesViewModel
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

        public long SeqPessoaAtuacao { get; set; }

        public MatriculaPertencePlanoEstudo SituacaoPlanoEstudo { get; set; }

        [SMCSelect("SituacoesItens", StorageType = SMCStorageType.TempData)]
        [SMCRequired]
        public long SeqSituacaoItemMatricula { get; set; }

        public MotivoSituacaoMatricula Motivo { get; set; }

        public bool PertencePlanoEstudo { get; set; }

        public long SeqDivisaoComponente { get; set; }
    }
}