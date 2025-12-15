using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoSolicitacaoServicoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        public string DescricaoSolicitacaoServico { get; set; }

        public List<PessoaAtuacaoAnexoViewModel> Anexos { get; set; }
    }
}