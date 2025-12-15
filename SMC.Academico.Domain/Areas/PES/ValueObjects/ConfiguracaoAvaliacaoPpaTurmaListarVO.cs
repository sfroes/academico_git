using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class ConfiguracaoAvaliacaoPpaTurmaListarVO : ISMCMappable
    {
        public long SeqConfiguracaoAvaliacaoPpa { get; set; }
        public long SeqConfiguracaoAvaliacaoPpaTurma { get; set; }

        public long SeqTurma { get; set; }

        public long CodigoTurma { get; set; }

        public int NumeroTurma { get; set; }

        public string Turma { get; set; }

        public int? CodigoInstrumento { get; set; }

        public string DescricaoTurma { get; set; }
    }
}
