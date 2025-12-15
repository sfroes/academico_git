using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework.Exceptions;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class RegistroDocumentosAtendimentoViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        public override string Token => TOKEN_SOLICITACAO_SERVICO.PARECER_ENTREGA_DOCUMENTACAO;
        public bool NecessitaConfirmacaoEntregaDocumentos { get; set; }
        public List<string> DocumentosAlterados { get; set; }
        public List<RegistroDocumentosAtendimentoDocumentoViewModel> Documentos { get; set; }

    }
}