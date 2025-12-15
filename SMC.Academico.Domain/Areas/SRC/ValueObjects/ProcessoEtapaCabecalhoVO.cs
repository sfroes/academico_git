using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ProcessoEtapaCabecalhoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public long SeqEtapaSgf { get; set; }

        public string DescricaoEtapaSgf { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }
    }
}