using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Models
{
    public class SolicitacaoMatriculaItemDivisoesViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> DivisoesTurmas { get; set; }

        [SMCSelect(nameof(DivisoesTurmas), AutoSelectSingleItem = true)]
        public long? SeqDivisaoTurma { get; set; }

        public string DivisaoTurmaDescricao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string Situacao { get; set; }

        public MotivoSituacaoMatricula Motivo { get; set; }

        public string SituacaoMotivo
        {
            get
            {
                if (string.IsNullOrEmpty(Motivo.SMCGetDescription()))
                    return Situacao;

                return $"{Situacao} - {Motivo.SMCGetDescription()}";
            }
        }
    }
}