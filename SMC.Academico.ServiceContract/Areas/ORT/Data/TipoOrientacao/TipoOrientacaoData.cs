using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class TipoOrientacaoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public bool TrabalhoConclusaoCurso { get; set; }

        public bool PermiteManutencaoManual { get; set; }

        public bool OrientacaoTurma { get; set; }

        public short? NumeroPrioridadeChancelaMatricula { get; set; }
    }
}
