using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class DocumentoConclusaoViaAnteriorListarData : ISMCMappable
    {
        public long Seq { get; set; }

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
