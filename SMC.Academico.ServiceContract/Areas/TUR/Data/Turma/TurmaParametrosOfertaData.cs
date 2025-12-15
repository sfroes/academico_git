using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaParametrosOfertaData : ISMCMappable
    {
        public bool Selecionado { get; set; }

        public long SeqOfertaMatriz { get; set; }

        public string DescricaoOfertaMatriz { get; set; }

        public short DivisaoMatrizCurricularNumero { get; set; }

        public string DivisaoMatrizCurricularDescricao { get; set; }

        public long SeqCriterioAprovacao { get; set; }

        public string CriterioNotaMaxima { get; set; }

        public string CriterioPercentualNotaAprovado { get; set; }

        public string CriterioPercentualFrequenciaAprovado { get; set; }

        public long SeqEscalaApuracao { get; set; }

        public string CriterioDescricaoEscalaApuracao { get; set; }

        public List<TurmaParametrosDetalheData> DivisoesComponente { get; set; }

        public bool ApurarFrequencia { get; set; }
    }
}
