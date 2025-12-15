using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Models
{
    public class SelecaoTurmaOfertaDivisaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCHidden]
        public long SeqTurmaControle { get; set; }

        [SMCHidden]
        public long? SeqConfiguracaoComponente { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> DivisoesTurmas { get; set; }

        [SMCConditionalReadonly(nameof(PreRequisito),SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSelect(nameof(DivisoesTurmas), AutoSelectSingleItem = true)]
        public long? SeqDivisaoTurma { get; set; }

        [SMCDependency(nameof(SeqDivisaoTurma), "TurmaDivisaoComGrupoSelecionado", "SelecaoTurmaRoute", "", false,new[] { nameof(PermitirGrupo), nameof(SeqDivisaoComponente), nameof(SeqTurmaControle) })]
        [SMCHidden]
        public long SeqDivisaoTurmaTemp { get; set; }

        [SMCHidden]
        public long SeqDivisaoComponente { get; set; }

        public string DivisaoTurmaDescricao { get; set; }

        public bool PermitirGrupo { get; set; }

        public int QuantidadeVagasDisponiveis { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public bool PreRequisito { get; set; }

        [SMCHidden]
        public string Situacao { get; set; }
    }
}
