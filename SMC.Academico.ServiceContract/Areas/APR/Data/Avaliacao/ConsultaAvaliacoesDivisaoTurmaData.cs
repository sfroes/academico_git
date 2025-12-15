using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class ConsultaAvaliacoesDivisaoTurmaData : ISMCMappable
    {
        public long SeqDivisaoTurma { get; set; }

        public string Descricao { get; set; }

        public string Nota { get; set; }

        public List<DetalhesAvaliacaoData> Avaliacoes { get; set; }
    }
}
