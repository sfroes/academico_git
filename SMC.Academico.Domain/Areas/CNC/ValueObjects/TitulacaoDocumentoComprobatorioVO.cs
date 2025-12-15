using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class TitulacaoDocumentoComprobatorioVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long? SeqTipoDocumento { get; set; }


    }
}