using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SituacaoEtapaJSonVO : ISMCMappable
    {
        public int? num_ordem_etapa { get; set; }

        public long? seq_situacao_etapa { get; set; }

        public bool? ind_situacao_inicial_etapa { get; set; }

        public bool? ind_situacao_final_etapa { get; set; }

        public short? idt_dom_classificacao_situacao_final { get; set; }

    }
}