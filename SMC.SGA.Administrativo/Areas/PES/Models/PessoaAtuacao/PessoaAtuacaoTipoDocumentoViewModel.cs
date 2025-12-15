using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoTipoDocumentoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public bool PermiteVarios { get; set; }

        public List<PessoaAtuacaoSolicitacaoServicoViewModel> Solicitacoes { get; set; }
    }
}