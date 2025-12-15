using SMC.Academico.UI.Mvc.Models;
using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoServicoPaginaFiltroViewModel : SMCViewModelBase
    {
        public bool RedirecionarHome { get; set; }

        public SMCEncryptedLong SeqConfiguracaoEtapa { get; set; }

        public SMCEncryptedLong SeqConfiguracaoEtapaPagina { get; set; }

        public SMCEncryptedLong SeqPessoaAtuacao { get; set; }

        public SMCEncryptedLong SeqSolicitacaoServico { get; set; }

        public SMCEncryptedLong SeqSolicitacaoServicoEtapa { get; set; }

        public ConfiguracaoEtapaPaginaViewModel ConfiguracaoEtapaPagina { get; set; }

        public ConfiguracaoEtapaViewModel ConfiguracaoEtapa { get; set; }

        public bool EtapaFinalizada { get; set; }

        public SMCEncryptedLong SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public SMCEncryptedLong SeqEntidadeResponsavel { get; set; }

        public SMCEncryptedLong SeqEntidadeCompartilhada { get; set; }

        public string DescricaoEtapa { get; set; }

        public SMCEncryptedLong SeqServico { get; set; }

        public string DescricaoServico { get; set; }

        public string TokenServico { get; set; }

        public string Protocolo { get; set; }

        public string TokenEtapa { get; set; }

        public bool Orientador { get; set; }
    }
}