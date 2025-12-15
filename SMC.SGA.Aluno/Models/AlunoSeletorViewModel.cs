using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Models
{
    public class AlunoSeletorViewModel : SMCViewModelBase
    {
        /// <summary>
        /// Instituição de ensino selecionada pelo usuário
        /// </summary>
        [SMCSize(SMCSize.Grid24_24)]
        [SMCSelect(nameof(InstituicoesEnsino), AutoSelectSingleItem = true)]
        [SMCRequired]
        public long? SeqInstituicaoEnsino { get; set; }

        /// <summary>
        /// Lista de instituições de ensino disponíveis para o usuário
        /// </summary>
        [SMCDataSource]
        public List<SMCDatasourceItem> InstituicoesEnsino { get; set; }

        /// <summary>
        /// Aluno selecionado pelo usuário
        /// </summary>
        [SMCSize(SMCSize.Grid24_24)]
        [SMCSelect(nameof(Alunos), AutoSelectSingleItem = true)]
        [SMCConditionalReadonly(nameof(SeqInstituicaoEnsino), SMCConditionalOperation.Equals, null)]
        [SMCDependency(nameof(SeqInstituicaoEnsino), "BuscarAlunosInstituicao", "Home", true)]
        [SMCRequired]
        public long? SeqAluno { get; set; }

        /// <summary>
        /// Lista de alunos disponíveis para o usuário
        /// </summary>
        [SMCDataSource]
        public List<SMCDatasourceItem> Alunos { get; set; }

        /// <summary>
        /// Flag para habilitar ou não a seleção da aluno
        /// </summary>
        public bool HabilitarSelecao { get; set; }
    }
}