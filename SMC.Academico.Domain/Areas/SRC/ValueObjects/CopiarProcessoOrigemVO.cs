using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class CopiarProcessoOrigemVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqServico { get; set; }

        [SMCMapProperty("CicloLetivo.Descricao")]
        public string DescricaoCicloLetivo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public decimal? ValorPercentualServicoAdicional { get; set; }
    }
}
