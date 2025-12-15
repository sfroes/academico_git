using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoViaAnteriorListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long? SeqDocumentoConclusaoViaAnterior { get; set; }

        public string NumeroViaDocumentoAnterior { get; set; }

        public string DescricaoTipoDocumentoAnterior { get; set; }

        public string OrgaoDeRegistroAnterior { get; set; }

        public string NumeroRegistroAnterior { get; set; }

        public DateTime? DataRegistroAnterior { get; set; }

        public string NumeroProcessoAnterior { get; set; }

        public string DescricaoSituacaoDocumentoAcademico { get; set; }
    }
}