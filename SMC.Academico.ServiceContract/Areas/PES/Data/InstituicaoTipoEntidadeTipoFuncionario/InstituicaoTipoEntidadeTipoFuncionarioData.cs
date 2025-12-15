using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class InstituicaoTipoEntidadeTipoFuncionarioData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoTipoEntidade { get; set; }

        public long SeqTipoFuncionario { get; set; }
    }
}
