using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Formularios.UI.Mvc.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoServicoPaginaViewModelBase : TemplatePaginaViewModel
    {
        [SMCHidden]
        public SMCEncryptedLong SeqSolicitacaoHistoricoNavegacao { get; set; }

        [SMCHidden]
        public SMCEncryptedLong SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public virtual string ActionSalvarEtapa { get; set; }

        [SMCHidden]
        public bool EtapaFinalizada { get; set; }

        [SMCHidden]
        public SMCEncryptedLong SeqSolicitacaoServicoEtapa { get; set; }

        [SMCHidden]
        public SMCEncryptedLong SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public SMCEncryptedLong SeqProcesso { get; set; }

        [SMCHidden]
        public SMCEncryptedLong SeqServico { get; set; }

        public List<PessoaAtuacaoBloqueioViewModel> Bloqueios { get; set; }

        [SMCHidden]
        public string backUrl { get; set; }

        [SMCHidden]
        public bool ExibirIntegralizacao { get; set; }

        [SMCHidden]
        public bool ExistemDocumentosPendentesSemPrazoEntrega { get; set; }

        #region Menu das páginas

        [SMCHidden]
        public virtual string DescricaoEtapa { get; set; }

        [SMCHidden]
        public string TokenServico { get; set; }

        [SMCHidden]
        public string DescricaoServico { get; set; }

        [SMCHidden]
        public SMCEncryptedLong SeqSituacaoFinalSucesso { get; set; }

        [SMCHidden]
        public SMCEncryptedLong SeqSituacaoFinalSemSucesso { get; set; }

        [SMCHidden]
        public SMCEncryptedLong SeqSituacaoFinalCancelada { get; set; }

        [SMCHidden]
        public virtual string Protocolo { get; set; }

        [SMCHidden]
        public bool HabilitarEfetivacaoMatricula { get; set; }
        #endregion Menu das páginas
    }
}