using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class InformacaoProcessoItemVO : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public string DescricaoEtapa { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public TipoPrazoEtapa? TipoPrazoEtapa { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public short? NumeroDiasPrazoEtapa { get; set; }
    }
}