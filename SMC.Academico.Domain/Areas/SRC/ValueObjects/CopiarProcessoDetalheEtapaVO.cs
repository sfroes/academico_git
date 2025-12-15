using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class CopiarProcessoDetalheEtapaVO : ISMCMappable
    {       
        public long Seq { get; set; }
     
        public string DescricaoEtapa { get; set; }

        public long SeqEtapaSgf { get; set; }

        public bool Obrigatoria { get; set; }
       
        public bool? Associar { get; set; }
       
        public bool CopiarConfiguracoes { get; set; }

        public TipoPrazoEtapa TipoPrazoEtapa { get; set; }
       
        public short? NumeroDiasPrazoEtapa { get; set; }
        
        public DateTime? DataInicio { get; set; }
        
        public DateTime? DataFim { get; set; }
       
        public SituacaoEtapa SituacaoEtapa { get; set; }
    }
}
