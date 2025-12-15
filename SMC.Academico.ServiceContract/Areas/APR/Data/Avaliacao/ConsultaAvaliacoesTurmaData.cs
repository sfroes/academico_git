using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class ConsultaAvaliacoesTurmaData : ISMCMappable
    {
        public long SeqTurma { get; set; }

        public string Descricao { get; set; }

        public int Falta { get; set; }

        public string NotaTotal { get; set; }

        public string NotaTotalReavaliacao { get; set; }

        public string Situacao { get; set; }

        public bool DiarioFechado { get; set; }

        public bool PossuiApuracaoFrequencia { get; set; }

        public List<DetalhesAvaliacaoData> Avaliacoes { get; set; }

        public List<ConsultaAvaliacoesDivisaoTurmaData> DivisoesTurma { get; set; }
    }
}
