using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class PeriodicoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqClassificacaoPeriodico { get; set; }

        public int? AnoInicio { get; set; }

        public int? AnoFim { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public string Descricao { get; set; }

        public string CodigoISSN { get; set; }

        public long SeqAreaAvaliacao { get; set; }

        public QualisCapes QualisCapes { get; set; }

        public string DescAreaAvaliacao { get; set; }
    }
}