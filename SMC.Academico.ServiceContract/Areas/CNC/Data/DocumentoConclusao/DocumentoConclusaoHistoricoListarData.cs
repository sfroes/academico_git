using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class DocumentoConclusaoHistoricoListarData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqDocumentoConclusao { get; set; }

        public long? SeqSituacaoDocumentoAcademico { get; set; }

        public string DescricaoSituacaoDocumentoAcademico { get; set; }

        public DateTime DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }

        public string MotivoInvalidadeObservacao { get; set; }
    }
}
