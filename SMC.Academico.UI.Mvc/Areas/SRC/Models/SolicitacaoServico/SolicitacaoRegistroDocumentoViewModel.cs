using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoRegistroDocumentoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public long? SeqConfiguracaoEtapa { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public string BackUrl { get; set; }

        [SMCHidden]
        public bool ExibeLegenda { get; set; }

        [SMCHidden]
        public bool? GrupoObrigatorio { get; set; }

        [SMCHidden]
        public long? GrupoDocumento { get; set; }

        public List<SolicitacaoDocumentoViewModel> Documentos { get; set; }

        public List<string> DocumentosEntreguesAnteriormente { get; set; }

        public bool TokenServicoEntregaDocumentacao { get; set; }
    } 
}