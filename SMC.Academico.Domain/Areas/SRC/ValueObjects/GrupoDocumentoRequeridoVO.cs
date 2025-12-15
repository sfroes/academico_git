using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class GrupoDocumentoRequeridoVO : ISMCMappable
    {             
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public string DescricaoConfiguracaoEtapa { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public string Descricao { get; set; }

        public short MinimoObrigatorio { get; set; }

        public bool? UploadObrigatorio { get; set; }

        public List<GrupoDocumentoRequeridoItemVO> Itens { get; set; }

        public string Mensagem { get; set; }
    }
}
