using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class SituacaoMatriculaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        public long SeqSituacaoMatricula { get; set; } 
    }
}
