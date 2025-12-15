using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.MAT.Models
{
    public class SolicitacaoMatriculaSeletorViewModel : SMCViewModelBase
    {
        /// <summary>
        /// Processo selecionado pelo usuário
        /// </summary>
        [SMCSize(SMCSize.Grid24_24)]
        [SMCSelect(nameof(Solicitacoes), NameDescriptionField = nameof(DescricaoProcesso))]
        public long? SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public string DescricaoProcesso { get; set; }

        /// <summary>
        /// Lista de ingressantes disponíveis para o usuário
        /// </summary>
        public List<SolicitacaoMatriculaListaViewModel> Solicitacoes { get; set; }
    }
}