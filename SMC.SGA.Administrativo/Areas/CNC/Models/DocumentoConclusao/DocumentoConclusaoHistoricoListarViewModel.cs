using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoHistoricoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDocumentoConclusao { get; set; }

        [SMCHidden]
        public long? SeqSituacaoDocumentoAcademico { get; set; }

        public string DescricaoSituacaoDocumentoAcademico { get; set; }

        public DateTime DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }

        public string MotivoInvalidadeObservacao { get; set; }
    }
}