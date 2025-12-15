using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class GrupoDocumentoRequeridoListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public string Descricao { get; set; }

        public short MinimoObrigatorio { get; set; }

        public bool UploadObrigatorio { get; set; }

        public List<GrupoDocumentoRequeridoItemListarVO> Itens { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }
    }
}
