using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class CabecalhoDocumentoConclusaoApostilamentoDocumentoViewModel : ISMCMappable
    {
        public long Seq { get; set; }

        public string DescricaoTipoDocumentoAcademico { get; set; }

        [SMCHidden]
        public long? SeqDocumentoAcademicoHistoricoSituacaoAtual { get; set; }

        [SMCValueEmpty("-")] 
        public string DescricaoSituacaoDocumentoAcademicoAtual { get; set; }

        [SMCValueEmpty("-")]
        public string NumeroProcesso { get; set; }

        public short NumeroViaDocumento { get; set; }

        [SMCValueEmpty("-")]
        public TipoRegistroDocumento? TipoRegistroDocumento { get; set; }

        [SMCValueEmpty("-")]
        public string OrgaoDeRegistro { get; set; }

        [SMCValueEmpty("-")]
        public string NumeroRegistro { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCValueEmpty("-")]
        public DateTime? DataRegistro { get; set; }

        [SMCValueEmpty("-")]
        public string Livro { get; set; }

        [SMCValueEmpty("-")]
        public string Folha { get; set; }
    }
}