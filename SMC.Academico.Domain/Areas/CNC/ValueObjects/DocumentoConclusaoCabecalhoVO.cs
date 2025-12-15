using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoCabecalhoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string DescricaoTipoDocumentoAcademico { get; set; }

        public long? SeqDocumentoAcademicoHistoricoSituacaoAtual { get; set; }

        public string DescricaoSituacaoDocumentoAcademicoAtual { get; set; }

        public string NumeroProcesso { get; set; }

        public short NumeroViaDocumento { get; set; }

        public TipoRegistroDocumento? TipoRegistroDocumento { get; set; }

        public string OrgaoDeRegistro { get; set; }

        public string NumeroRegistro { get; set; }

        public DateTime? DataRegistro { get; set; }

        public string Livro { get; set; }

        public string Folha { get; set; }
    }
}
