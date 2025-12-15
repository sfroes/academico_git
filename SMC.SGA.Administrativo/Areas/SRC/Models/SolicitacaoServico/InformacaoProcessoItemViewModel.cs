using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class InformacaoProcessoItemViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqProcesso { get; set; }

        public string DescricaoEtapa { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public TipoPrazoEtapa? TipoPrazoEtapa { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataInicio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataFim { get; set; }

        public short? NumeroDiasPrazoEtapa { get; set; }
    }
}