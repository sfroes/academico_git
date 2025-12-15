using System;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoViaAnteriorListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqDocumentoAcademicoViaAnterior { get; set; }

        public string NumeroViaDocumentoAnterior { get; set; }

        public string DescricaoTipoDocumentoAnterior { get; set; }

        public string OrgaoDeRegistroAnterior { get; set; }

        public string NumeroRegistroAnterior { get; set; }

        public DateTime? DataRegistroAnterior { get; set; }

        public string NumeroProcessoAnterior { get; set; }

        public string DescricaoSituacaoDocumentoAcademico { get; set; }
    }
}
