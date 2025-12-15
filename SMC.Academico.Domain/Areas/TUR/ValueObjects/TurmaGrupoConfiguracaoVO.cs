using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaGrupoConfiguracaoVO : ISMCMappable, ISMCSeq
    {     
        public long Seq { get; set; }

        public long SeqTurma { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public string Descricao { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public bool Selecionado { get; set; }

        public bool Principal { get; set; }

        public List<RestricaoTurmaMatrizVO> RestricoesTurmaMatriz { get; set; }
    }
}
