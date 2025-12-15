using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoVisualizacaoDocumentoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public List<PessoaAtuacaoTipoDocumentoViewModel> TiposDocumento { get; set; }
    }
}