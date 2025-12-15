using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class GrupoDocumentoRequeridoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }
         
        public string DescricaoConfiguracaoEtapa { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public string Descricao { get; set; }

        public short MinimoObrigatorio { get; set; }

        public bool? UploadObrigatorio { get; set; }

        public List<GrupoDocumentoRequeridoItemData> Itens { get; set; }

        public string Mensagem { get; set; }
    }
}
